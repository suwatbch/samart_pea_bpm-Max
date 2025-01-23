using System;
using System.Collections.Generic;
using System.Text;
using Avanade.ACA.Batch;
using PEA.BPM.Integration.ACABatchService;
using PEA.BPM.Integration.BPMIntegration.Interface.Services;
using PEA.BPM.Integration.BPMIntegration.BS;
using PEA.BPM.Integration.BPMIntegration.BPMServerBatch;
using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;
using System.Globalization;
using System.IO;

namespace PEA.BPM.Integration.BPMIntegration.BranchServerBatch
{
    public class DL140_EXPORT_RECON_JOB : IJob
    {
        private const string DL_FULL_NAME = "DL140_EXPORT_RECON_JOB";
        private const string INTERFACE_NAME = "PAID";

        private SAPPort _sapPort;
        private ACADepLineMgr _depMgr;
        private ACABatchLogger _logger;
        private IBpmSAPService _bpmSAPService;

        public DL140_EXPORT_RECON_JOB()
        {
            _sapPort = new SAPPort();
            _bpmSAPService = new BPMSAPService();
            _depMgr = new ACADepLineMgr();
            _logger = ACABatchLogger.GetInstance(DL_FULL_NAME);
        }

        private void ExportData<T>(List<T> exportList, string branchId, string fileName) where T : IListUtility<T>, new()
        {
            _sapPort.OutFileName = BPMServerBatchHelper.GetReconnectionPath();
            _sapPort.SendList<T>(exportList, branchId, fileName);
        }

        #region IJob Members

        public void Execute(JobExecutionContext context)
        {
            
            context.Status = StatusCode.Executing;
            context.Commit();
            Guid batchKey = context.BatchExecutionContext.BatchExecutionContextData.Key;

            //Test block----------------------------------------------------------------------------------------------------------------------------
            string branchId = context.BatchExecutionContext.RequestParameters[0].Value.ToString();
            _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessData, "Prepare to export agency compensation for Branch : " + branchId, 0);
            //End test block------------------------------------------------------------------------------------------------------------------------

            string ssuggt = "รายการับชำระเงินค่าไฟฟ้าและค่าต่อกลับ";
            IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("en-US");
            string fileName = string.Format("PAID_{1}_{2}.txt", branchId, DateTime.Now.ToString("yyyyMMdd_HHmm", formatDate));

            try
            {
                ACABatchParam param = new ACABatchParam();
                param.BatchKey = batchKey;
                param.BranchId = branchId;

                if (Directory.Exists(BPMServerBatchHelper.GetReconnectionPath()))
                {
                    //start early the day
                    DateTime now = DateTime.Now;
                    DateTime startDt = new DateTime(now.Year, now.Month, now.Year, 0, 0, 0);
                    List<BillInfo> reconList = _bpmSAPService.GetReconnection(branchId, startDt, now);

                    if (reconList.Count > 0)
                    {
                        ExportData<BillInfo>(reconList, branchId, fileName);

                        _logger.WriteLog(batchKey, ACABatchLogger.LogType.Success,
                            string.Format("Export {0} {1} Done at {2} with {3} rows in file", ssuggt, fileName, DateTime.Now.ToLongTimeString(), reconList.Count));
                    }
                    else
                        _logger.WriteLog(batchKey, ACABatchLogger.LogType.Success, "There is 0 record to export.", 0);

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
                _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, e.ToString(), string.Format("เกิดปัญหาการ Export Text File สำหรับ {0}", ssuggt));
                context.Status = StatusCode.Failed;
                context.Commit();
            }
        }
        #endregion

    }
}
