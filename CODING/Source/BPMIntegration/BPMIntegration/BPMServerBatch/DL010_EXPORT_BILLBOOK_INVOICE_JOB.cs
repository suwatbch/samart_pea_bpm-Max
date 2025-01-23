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
namespace PEA.BPM.Integration.BPMIntegration.BPMServerBatch
{
    /// <summary>
    /// Export BillBook to SAP (action[create/cancel], invoice, ca, billbookId)
    /// Author: Hut (Wittawat Lohwithee)
    /// </summary>
    public class DL010_EXPORT_BILLBOOK_INVOICE_JOB : IJob
    {
        private const string DL_FULL_NAME = "DL010_EXPORT_BILLBOOK_INVOICE";

        private SAPPort _sapPort;
        private ACADepLineMgr _depMgr;
        private ACABatchLogger _logger;
        private IBpmSAPService _bpmSAPService;
        private string _filePool;

        public DL010_EXPORT_BILLBOOK_INVOICE_JOB()
        {
            _sapPort = new SAPPort();
            _bpmSAPService = new BPMSAPService();
            _depMgr = new ACADepLineMgr();
            _logger = ACABatchLogger.GetInstance(DL_FULL_NAME);
            _filePool = BPMServerBatchHelper.GetDataProcessingPath(DL_FULL_NAME);
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
            BPMServerBatchHelper.MoveFileToBillBookOutbound(DL_FULL_NAME, interfaceName);
        }

        #region IJob Members

        public void Execute(JobExecutionContext context)
        {
            string interfaceName = "";
            string outputFileName = "";
            string onlyFileName = "";
            string suggt = "";
            context.Status = StatusCode.Executing;
            context.Commit();
            Guid batchKey = context.BatchExecutionContext.BatchExecutionContextData.Key;
            try
            {
                //string billBookId = context.BatchExecutionContext.RequestParameters[0].Value.ToString();
                //string modifiedBy = context.BatchExecutionContext.RequestParameters[1].Value.ToString();
                _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessData, "Prepare to export BillBook", 0);
                int rowCount = 0;
                int bookCount = 0;
                string latestBA = "";
                ACABatchParam param = new ACABatchParam();
                param.BatchKey = batchKey;

                if (BPMServerBatchHelper.IsMappedDriveExist(DL_FULL_NAME, interfaceName))
                {
                    List<AG_BillBook> billBookList = _bpmSAPService.GetBillBookForExport();
                    foreach (AG_BillBook bb in billBookList)
                    {
                        bookCount++;
                        List<AG_BillBookInvoice> exportList1 = _bpmSAPService.Export_BillBookInvoice(bb.BillBookId, param);
                        if (exportList1.Count > 0)
                        {
                            rowCount += exportList1.Count;
                            interfaceName = "TRP0060A";
                            onlyFileName = _bpmSAPService.GetExportFileNameTimestamp(interfaceName, bb.BillBookId);
                            outputFileName = string.Format(@"{0}\{1}\{2}", _filePool, interfaceName, onlyFileName);
                            _sapPort.OutFileName = outputFileName;
                            _sapPort.SendList<AG_BillBookInvoice>(exportList1);
                            _logger.WriteLog(batchKey, ACABatchLogger.LogType.Success,
                                string.Format(@"Export BillBookId: {0} Done",bb.BillBookId ),onlyFileName,exportList1.Count);
                            _logger.InterfaceId = interfaceName;
                            //Pause process to protect problem of overlap with the same time same BA in file name,
                            //But different BA can run fastest
                            if (latestBA == bb.BillBookId.Substring(0, 4))
                                Thread.Sleep(1);
                            latestBA = bb.BillBookId.Substring(0, 4);
                        }
                        else
                            _logger.WriteLog(batchKey, ACABatchLogger.LogType.Success, string.Format("BillBookId: {0} - There is 0 record to export.",bb.BillBookId), 0);
                    }
                    if(rowCount>0)
                        SendFileToOutBound(interfaceName, batchKey);//Move file from process path to outbound
                    _logger.WriteLog(batchKey, ACABatchLogger.LogType.Success, string.Format( "Success to export {0} books.",bookCount), rowCount);

                    context.Status = StatusCode.Success;
                    context.Commit();
                }
                else
                {
                    throw new Exception("Couldn't connect mapped drive. Please check your connection.");
                }
            }
            catch (Exception e)
            {
                _logger.Severity = Severity.Level9;
                _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, e.ToString(), "เกิดปัญหาการ Export Text File สำหรับ {0}");
                context.Status = StatusCode.Failed;
                context.Commit();
            }
        }
        #endregion
    }
}
