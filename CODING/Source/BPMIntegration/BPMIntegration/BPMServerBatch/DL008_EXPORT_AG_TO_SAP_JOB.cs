using System;
using System.Collections.Generic;
using System.Text;
using Avanade.ACA.Batch;
using PEA.BPM.Integration.ACABatchService;
using System.Configuration;
using PEA.BPM.Integration.BPMIntegration.BS;
using PEA.BPM.Integration.BPMIntegration.Interface.Services;
using PEA.BPM.Integration.BPMIntegration.Interface.ExportEntities;
using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;
using BPMSAPConnector;
using System.ComponentModel;
using System.Threading;
using Avanade.ACA.Batch.Configuration;
using System.IO;
using System.Collections;
using PEA.BPM.Integration.BPMIntegration.Interface.Constants;
using System.Text.RegularExpressions;

namespace PEA.BPM.Integration.BPMIntegration.BPMServerBatch
{
    /// <summary>
    /// AgencyCommission (comm, fine),PaymentData(join tbs)
    /// </summary>
    public class DL008_EXPORT_AG_TO_SAP_JOB : IJob
    {
        private const string DL_FULL_NAME = "DL008_EXPORT_AG_TO_SAP";

        private SAPPort _sapPort;
        private ACADepLineMgr _depMgr;
        private ACABatchLogger _logger;
        private IBpmSAPService _bpmSAPService;
        private string _filePool;

        public DL008_EXPORT_AG_TO_SAP_JOB()
        {
            _sapPort = new SAPPort();
            _bpmSAPService = new BPMSAPService();
            _depMgr = new ACADepLineMgr();
            _logger = ACABatchLogger.GetInstance(DL_FULL_NAME);
            _filePool = BPMServerBatchHelper.GetDataProcessingPath(DL_FULL_NAME);
        }

        private string TriggerSAP(string fileName, Guid batchKey, ArrayList aList)
        {
            try
            {
                ZTABTEXT z = new ZTABTEXT();

                for (int i = 0; i < aList.Count; i++)
                    z.Add(aList[i].ToString());

                string msg = "";
                string conn = ConfigurationManager.AppSettings["SAP_CONN"];                                
                BPMSAPProxy submit = new BPMSAPProxy(conn);
                submit.Timeout = 5 * 60 * 1000;                
                submit.Zpos_Submit_Post(fileName, z, out msg);
                return msg;
                
            }
            catch(Exception e)
            {
                _logger.Severity = Severity.Level7;
                _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, e.ToString(), string.Format("เกิดปัญหาการเรียกฟังก์ชันในการ trigger SAP สำหรับไฟล์ - {0}", fileName));
                throw;
            }
        }

        private string ExportData<T>(List<T> exportList, string interfaceName,
            decimal? actualAmount, decimal? amount, decimal? overUnder) where T : IListUtility<T>, new()
        {
            string outputFileName = string.Format(@"{0}\{1}\{2}", _filePool, interfaceName, _bpmSAPService.GetExportFileName(interfaceName));
            _sapPort.OutFileName = outputFileName;
            _sapPort.SendList<T>(exportList, actualAmount, amount, overUnder);

            return outputFileName;
        }

        private void SendFileToOutBound(string interfaceName, Guid batchKey)
        {
            BPMServerBatchHelper.MoveFileToAGOutbound(DL_FULL_NAME, interfaceName);
        }

        private void SendFileToOutBound(string interfaceName, Guid batchKey, string fileName)
        {
            BPMServerBatchHelper.MoveFileToAGOutbound(DL_FULL_NAME, interfaceName, fileName);
        }

        private void SendFileToFailedOutBound(string interfaceName, Guid batchKey, string fileName)
        {
            BPMServerBatchHelper.MoveFileToFailedOutbound(DL_FULL_NAME, interfaceName, fileName);
        }

        #region IJob Members

        public void Execute(JobExecutionContext context)
        {
            string interfaceName = "";
            string outputFileName = "";
            string sapFileName = "";
            string bpmFileName = "";
            string suggt = "";
            context.Status = StatusCode.Executing;
            context.Commit();
            Guid batchKey = context.BatchExecutionContext.BatchExecutionContextData.Key;

            //Get BranchId
            string branchId = context.BatchExecutionContext.RequestParameters[0].Value.ToString();
            if (!(new Regex("[^0-9]").IsMatch(branchId))) { branchId = "000000"; }
            _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessData, "Export agency compensation for " + branchId +".", 0);

            try
            {
                ACABatchParam param = new ACABatchParam();
                param.BatchKey = batchKey;
                param.BranchId = branchId;

                if (BPMServerBatchHelper.IsMappedDriveExist(DL_FULL_NAME, interfaceName))
                {

                    suggt = "รายการค่าปรับและค่าตอบแทนที่การไฟฟ้าต้องจ่ายให้ตัวแทนเก็บเงิน";
                    List<AG_Compensation> exportList1 = _bpmSAPService.Export_AgencyCompensation(branchId, param);
                    if (exportList1.Count > 0)
                    {
                        interfaceName = "TRP0010";
                        string dateTimeStr = _bpmSAPService.GetDBDateTime("yyyyMMdd_HHmmsss");
                        bpmFileName = _bpmSAPService.GetExportFileNameWithoutExtension(interfaceName);
                        sapFileName = bpmFileName + ".txt";
                        bpmFileName = String.Format("{0}_{1}_{2}.txt", bpmFileName, dateTimeStr, branchId);
                        outputFileName = string.Format(@"{0}\{1}\{2}", _filePool, interfaceName, bpmFileName);

                        _sapPort.OutFileName = outputFileName;
                        //Thread.Sleep(5000);

                        ArrayList aList = new ArrayList();
                        aList = _sapPort.SendListAG<AG_Compensation>(exportList1);

                        SendFileToOutBound(interfaceName, batchKey, bpmFileName);

                        _logger.WriteLog(batchKey, ACABatchLogger.LogType.Success,
                            string.Format("Export {0} for {1} Done at {2} with {3} rows in file", interfaceName, branchId, DateTime.Now.ToLongTimeString(), aList.Count), bpmFileName, 0);

                        //Trigger SAP to get AG Compensation                    
                        _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessData,
                            "Attempt to trigger SAP", bpmFileName, 0);
                        string result = TriggerSAP(sapFileName, batchKey, aList);
                        aList = null;

                        _logger.InterfaceId = interfaceName;
                        _logger.WriteLog(batchKey, ACABatchLogger.LogType.Success, "BPM export complete.", 0);
                        //_logger.WriteLog(batchKey, ACABatchLogger.LogType.Success, "Triggering Done. " + result, 0);
                        context.Status = StatusCode.Success;
                        context.Commit();
                    }
                    else
                    {
                        _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, "There is 0 record to export.", 0);
                        context.Status = StatusCode.Failed;
                        context.Commit();
                    }
                   
                }
                else
                {
                    throw new Exception("Couldn't connect mapped drive. Please check your connection.");
                }
            }
            catch (Exception e)
            {
                //outputFileName = string.Format(@"{0}\{1}\{2}", _filePool, interfaceName, bpmFileName);
                SendFileToFailedOutBound(interfaceName, batchKey, bpmFileName);

                _logger.Severity = Severity.Level9;
                _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, e.ToString(), string.Format("เกิดปัญหาการ Export Text File สำหรับ {0}", suggt));
                context.Status = StatusCode.Failed;
                context.Commit();
            }
        }
        #endregion
    }
}
