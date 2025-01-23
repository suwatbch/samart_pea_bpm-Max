using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Configuration;
using Avanade.ACA.Batch;
using PEA.BPM.Integration.ACABatchService;
using PEA.BPM.Integration.BPMIntegration.Interface.Services;
using PEA.BPM.Integration.BPMIntegration.BS;
using PEA.BPM.Integration.BPMIntegration.SG;
using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;
using PEA.BPM.Integration.BPMIntegration.Interface.Constants;


namespace PEA.BPM.Integration.BPMIntegration.BranchServerBatch
{
    public class DL040_AR_JOB : IJob
    {
        private const string DL_NAME_ID = "DL040";
        private const string DL_FULL_NAME = "DL040_AR";
        private const string BRANCH_ID = "BRANCH_ID";
        private string _activeFileKey;
        private ACADepLineMgr _depMgr;
        private ACABatchLogger _logger;

        private IBpmBranchService _bpmBranchService;
        private IBpmServerService _bpmServerSG;
        private string _entityName;
        private DateTime _lastModifiedDt;
        private DateTime _serverDate;
        private string _branchId;
        private string _jobType;

        public DL040_AR_JOB()
        {
            _depMgr = new ACADepLineMgr();
            _logger = ACABatchLogger.GetInstance(DL_FULL_NAME);

            _bpmBranchService = new BPMBranchService();
            _bpmServerSG = new BpmServerDownloadSG();
            _branchId = ConfigurationManager.AppSettings[BRANCH_ID];
            _jobType = "DL";
            _logger.BranchId = _branchId;
        }

        #region Additional Members

        //wrapper method
        private bool ImportData(Guid batchKey, string tb, bool overwrite)
        {
            //set _hasSkipError 
            bool ret = false;
            ACABatchParam param = new ACABatchParam();
            param.Overwrite = overwrite;
            param.BatchKey = batchKey;
            _logger.InterfaceId = tb;

            try
            {
                switch (tb)
                {
                    case "AR":
                        {
                            param.FileName = "AR";
                            ret = DownloadAR(param);
                        }
                        break;
                    case "Disconnection":
                        {
                            param.FileName = "Disconnection";
                            ret = DownloadDisconnection(param);
                        }
                        break;
                    case "RTDisconnectionDocCaDoc":
                        {
                            param.FileName = "RTDisconnectionDocCaDoc";
                            ret = DownloadRTDisconnectionDocCaDoc(param);
                        }
                        break;
                     case "AP":
                        {
                            param.FileName = "AP";
                            ret = DownloadAP(param);
                        }
                        break;
                    case "APChequePayment":
                        {
                            param.FileName = "APChequePayment";
                            ret = DownloadAPChequePayment(param);
                        }
                        break;
                    default:
                        {
                            _logger.Severity = Severity.Level8;
                            _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, string.Format("Dependency Line {0} ไม่รองรับการนำเข้าข้อมูลในตารางนี้", DL_NAME_ID), tb);
                            return false;
                        }
                }

                return ret;
            }
            catch (Exception e)
            {
                _logger.Severity = Severity.Level9;
                _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, e.ToString(), string.Format("Downloading data failed, {0}", tb));
                return false;
            }
        }

        private bool HandleError(Guid batchKey, string key, string tb)
        {
            ACADepLineInfo dep = new ACADepLineInfo();
            dep.DLId = DL_NAME_ID;
            dep.FileKey = key;
            dep.LastFailTb = tb;
            dep.Status = "1"; //fail

            try
            {
                _depMgr.SetStatus(dep, false); //create new record

                return true;
            }
            catch (Exception e)
            {
                _logger.Severity = Severity.Level6;
                _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, e.Message, "มีปัญหาเกี่ยวกับฟังก์ชันในการ HandleError");
                return false;
            }
        }

        private bool HandleSuccess(Guid batchKey, string tb)
        {
            try
            {
                _depMgr.ResetLastFailTb(DL_NAME_ID, tb);

                return true;
            }
            catch (Exception e)
            {
                _logger.Severity = Severity.Level6;
                _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, e.Message, "มีปัญหาเกี่ยวกับฟังก์ชันในการ HandleSuccess");
                return false;
            }
        }     

        private bool DownloadAR(ACABatchParam param)
        {
            _entityName = typeof(AR).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<AR> ars = new List<AR>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [AR]");
            ars = _bpmServerSG.GetUpdateAR(_branchId, _lastModifiedDt, _serverDate);

            if (ars.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [AR]", "Total rows = " + ars.Count.ToString());
                bool result = _bpmBranchService.UpdateAR(ars, param);

                if (result)
                {
                    Scheduler sch = new Scheduler();
                    sch.EntityName = _entityName;
                    sch.LastUpdateDt = _serverDate;

                    //limit download by selecting top 50000 * in ta_syncs_ar, wait to deploy
                    //sch.LastUpdateDt = ars[ars.Count - 1].ModifiedDt.Value;
                    
                    sch.JobType = _jobType;
                    result = ACABatchScheduler.UpdateScheduler(sch);
                    return true;
                }
                else
                    return false;
            }
            else
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "AR");
                return true;
            }
        }

        private bool DownloadDisconnection(ACABatchParam param)
        {
            _entityName = typeof(DisconnectionDocInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<DisconnectionDocInfo> list = new List<DisconnectionDocInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [DisconnectionDoc]");
            list = _bpmServerSG.GetUpdateDisconnectionDoc(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [DisconnectionDoc]", "Total rows = " + list.Count.ToString());
                bool result = _bpmBranchService.UpdateDisconnectionDoc(list, param);

                if (result)
                {
                    Scheduler sch = new Scheduler();
                    sch.EntityName = _entityName;
                    sch.LastUpdateDt = _serverDate;
                    sch.JobType = _jobType;
                    result = ACABatchScheduler.UpdateScheduler(sch);
                    return true;
                }
                else
                    return false;
            }
            else
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "DisconnectionDoc");
                return true;
            }
        }

        private bool DownloadRTDisconnectionDocCaDoc(ACABatchParam param)
        {
            _entityName = typeof(RTDisconnectionDocCaDocInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<RTDisconnectionDocCaDocInfo> list = new List<RTDisconnectionDocCaDocInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [RTDisconnectionDocCaDoc]");
            list = _bpmServerSG.GetUpdateRTDisconnectionDocCaDoc(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [RTDisconnectionDocCaDoc]", "Total rows = " + list.Count.ToString());
                bool result = _bpmBranchService.UpdateRTDisconnectionDocCaDoc(list, param);

                if (result)
                {
                    Scheduler sch = new Scheduler();
                    sch.EntityName = _entityName;
                    sch.LastUpdateDt = _serverDate;
                    sch.JobType = _jobType;
                    result = ACABatchScheduler.UpdateScheduler(sch);
                    return true;
                }
                else
                    return false;
            }
            else
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "RTDisconnectionDocCaDoc");
                return true;
            }
        }

        private bool DownloadAP(ACABatchParam param)
        {
            _entityName = typeof(APInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<APInfo> list = new List<APInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [AP]");
            list = _bpmServerSG.GetUpdateAP(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [AP]", "Total rows = " + list.Count.ToString());
                bool result = _bpmBranchService.UpdateAP(list, param);

                if (result)
                {
                    Scheduler sch = new Scheduler();
                    sch.EntityName = _entityName;
                    sch.LastUpdateDt = _serverDate;
                    sch.JobType = _jobType;
                    result = ACABatchScheduler.UpdateScheduler(sch);
                    return true;
                }
                else
                    return false;
            }
            else
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "AP");
                return true;
            }
        }

        private bool DownloadAPChequePayment(ACABatchParam param)
        {
            _entityName = typeof(APChequePaymentInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<APChequePaymentInfo> list = new List<APChequePaymentInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [APChequePayment]");
            list = _bpmServerSG.GetUpdateAPChequePayment(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [APChequePayment]", "Total rows = " + list.Count.ToString());
                bool result = _bpmBranchService.UpdateAPChequePayment(list, param);

                if (result)
                {
                    Scheduler sch = new Scheduler();
                    sch.EntityName = _entityName;
                    sch.LastUpdateDt = _serverDate;
                    sch.JobType = _jobType;
                    result = ACABatchScheduler.UpdateScheduler(sch);
                    return true;
                }
                else
                    return false;
            }
            else
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "APChequePayment");
                return true;
            }
        }


        #endregion

        #region IJob Members

        public void Execute(JobExecutionContext context)
        {
            context.Status = StatusCode.Executing;
            context.Commit();
            Guid batchKey = context.BatchExecutionContext.BatchExecutionContextData.Key;

            try
            {
                _depMgr = new ACADepLineMgr();
                _logger = ACABatchLogger.GetInstance(DL_FULL_NAME);

                _serverDate = _bpmServerSG.GetServerTime();

                ACADepLineInfo depLine = _depMgr.GetDepedencyLineStatus(DL_NAME_ID);

                if (depLine.Status == "0") // success
                {
                    _activeFileKey = SAPFile.FindSuitableKey();
                }
                else //last error 
                {
                    string fileKey = depLine.FileKey;

                    if (string.IsNullOrEmpty(fileKey))
                    {
                        _logger.Severity = Severity.Level7;
                        _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, "ระบบไม่พบ key ของการนำเข้าที่ผิดพลาดก่อนหน้านี้");
                        context.Status = StatusCode.Failed;
                        context.Commit();
                        return;
                    }

                    _activeFileKey = depLine.FileKey;
                }

                //*** start process data to tables
                List<string> tables = _depMgr.GetDependencyLineTable(DL_NAME_ID);
                Hashtable failHt = _depMgr.GetAllLastFailHt(DL_NAME_ID);

                foreach (string tb in tables)
                {

                    if (!ImportData(batchKey, tb, true)) //always overwrite
                    {
                        if (!failHt.ContainsKey(tb))
                        {
                            HandleError(batchKey, _activeFileKey, tb);
                        }
                        break;
                    }
                    else
                    {
                        if (!HandleSuccess(batchKey, tb))
                            throw new Exception("ERROR Stop!");
                    }

                }

                if (_depMgr.IsErrorRemain(DL_NAME_ID))
                {
                    List<ACADepLineInfo> depList = _depMgr.GetAllDLFailTb(DL_NAME_ID);
                    _logger.Severity = Severity.Level7;
                    foreach (ACADepLineInfo dep in depList)
                        _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, string.Format("ยังคงเหลือข้อมูลที่ยังไม่ถูกแก้ไขและนำเข้าโดยสมบูรณ์, Interface: {0}, Key: {1} ",dep.LastFailTb, dep.FileKey), "");

                    context.Status = StatusCode.Failed;
                    context.Commit();
                }
                else
                {
                    context.Status = StatusCode.Success;
                    context.Commit();
                }

            }
            catch (Exception e)
            {
                _logger.Severity = Severity.Level10;
                _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, e.Message, "เกิดปัญหาการเรียกฟังก์ชันในการประมวลผลแบ็ช");
                context.Status = StatusCode.Failed;
                context.Commit();
            }
        }

        #endregion
    }
}


/* ARPostJob
 using System;
using System.Collections.Generic;
using System.Text;
using Avanade.ACA.Batch;
using PEA.BPM.Integration.BPMIntegration.Interface.Services;
using PEA.BPM.Integration.BPMIntegration.SG;
using PEA.BPM.Integration.BPMIntegration.BS;
using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;

namespace PEA.BPM.Integration.BPMIntegration.BranchServerBatch.Transaction
{
    public class ARPostJob: IJob
    {
        private IBpmServerService _bpmServerSG;
        private IBpmBranchService _bpmBranchService;

        public ARPostJob()
        {
            _bpmServerSG = new BpmServerSG();
            _bpmBranchService = new BPMBranchService();
        }

        private void UploadAR(DateTime lastModifiedDate)
        {
            List<AR> ars = _bpmBranchService.GetToUploadAR(lastModifiedDate);
            _bpmServerSG.UploadAR(ars);
            _bpmBranchService.SetSyncAR(lastModifiedDate);
        }

        private void UploadARPayment(DateTime lastModifiedDate)
        {
            List<ARPayment> arPayments = _bpmBranchService.GetToUploadARPayment(lastModifiedDate);
            _bpmServerSG.UploadARPayment(arPayments);
            _bpmBranchService.SetSyncARPayment(lastModifiedDate);
        }

        private void UploadPayment(DateTime lastModifiedDate)
        {
            List<Payment> payments = _bpmBranchService.GetToUploadPayment(lastModifiedDate);
            _bpmServerSG.UploadPayment(payments);
            _bpmBranchService.SetSyncPayment(lastModifiedDate);
        }

        private void UploadARPaymentType(DateTime lastModifiedDate)
        {
            List<ARPaymentType> arPaymentTypes = _bpmBranchService.GetToUploadARPaymentType(lastModifiedDate);
            _bpmServerSG.UploadARPaymentType(arPaymentTypes);
            _bpmBranchService.SetSyncARPaymentType(lastModifiedDate);
        }

        private void UploadRTARPaymentTypeARPayment(DateTime lastModifiedDate)
        {
            List<RTARPaymentTypeARPayment> rts = _bpmBranchService.GetToUploadRTARPaymentTypeARPayment(lastModifiedDate);
            _bpmServerSG.UploadRTARPaymentTypeARPayment(rts);
            _bpmBranchService.SetSyncRTARPaymentTypeARPayment(lastModifiedDate);
        }

        private void UploadReceipt(DateTime lastModifiedDate)
        {
            List<Receipt> receipts = _bpmBranchService.GetToUploadReceipt(lastModifiedDate);
            _bpmServerSG.UploadReceipt(receipts);
            _bpmBranchService.SetSyncReceipt(lastModifiedDate);
        }

        private void UploadReceiptItem(DateTime lastModifiedDate)
        {
            List<ReceiptItem> receiptItems = _bpmBranchService.GetToUploadReceiptItem(lastModifiedDate);
            _bpmServerSG.UploadReceiptItem(receiptItems);
            _bpmBranchService.SetSyncReceiptItem(lastModifiedDate);
        }

        #region IJob Members

        public void Execute(JobExecutionContext context)
        {
            try
            {
                DateTime lastModifiedDate = DateTime.Now;

                UploadAR(lastModifiedDate);
                UploadARPayment(lastModifiedDate);
                UploadPayment(lastModifiedDate);
                UploadARPaymentType(lastModifiedDate);
                UploadRTARPaymentTypeARPayment(lastModifiedDate);
                UploadReceipt(lastModifiedDate);
                UploadReceiptItem(lastModifiedDate);

                context.Status = StatusCode.Success;
                context.Commit();
            }
            catch (Exception e)
            {
                //StreamWriter sw = new StreamWriter(IntegrationFileNames.JobLogPath);
                //sw.Write(e.ToString());
                //sw.Close();
            }
        }

        #endregion
    }
}

 
 
 */

/* ARReadJob
 using System;
using System.Collections.Generic;
using System.Text;
using Avanade.ACA.Batch;
using PEA.BPM.Integration.BPMIntegration.Interface.Services;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using PEA.BPM.Integration.BPMIntegration.BS;
using PEA.BPM.Integration.BPMIntegration.SG;
using PEA.BPM.Architecture.ArchitectureBS;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;

namespace PEA.BPM.Integration.BPMIntegration.BranchServerBatch.Transaction
{
    public class ARReadJob : IJob
    {
        private IBpmBranchService _bpmBranchService;
        private IBpmServerService _bpmServerSG;

        private ISchedulerService _service;
        private string _entityName;
        private DateTime _lastModifiedDt;
        private DateTime _serverDate;
        private string _modifiedBy;
        private string _branchId;

        public ARReadJob()
        {
            _bpmBranchService = new BPMBranchService();
            _bpmServerSG = new BpmServerSG();
            _service = new SchedulerBS();

            _branchId = Session.Branch.Id;
            _modifiedBy = Session.User.Id;
            _serverDate = ACABatchScheduler.GetDbServerTime();
        }

        private void DownloadAR()
        {
            _entityName = typeof(AR).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<AR> ars = _bpmServerSG.GetUpdateAR(_branchId, _lastModifiedDt, _serverDate);
            bool result = _bpmBranchService.UpdateAR(ars);
            
            if (result)
            {
                Scheduler sch = new Scheduler();
                sch.EntityName = _entityName;
                sch.LastUpdateDt = _serverDate;
                result = ACABatchScheduler.UpdateScheduler(sch);
            }           
        }

        private void DownloadARPayment()
        {
            _entityName = typeof(ARPayment).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<ARPayment> arPayments = _bpmServerSG.GetUpdateARPayment(_branchId, _lastModifiedDt, _serverDate);
            bool result = _bpmBranchService.UpdateARPayment(arPayments);

            if (result)
            {
                Scheduler sch = new Scheduler();
                sch.EntityName = _entityName;
                sch.LastUpdateDt = _serverDate;
                result = ACABatchScheduler.UpdateScheduler(sch);
            }   
        }

        private void DownloadPayment()
        {
            _entityName = typeof(Payment).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<Payment> payments = _bpmServerSG.GetUpdatePayment(_branchId, _lastModifiedDt, _serverDate);
            bool result = _bpmBranchService.UpdatePayment(payments);

            if (result)
            {
                Scheduler sch = new Scheduler();
                sch.EntityName = _entityName;
                sch.LastUpdateDt = _serverDate;
                result = ACABatchScheduler.UpdateScheduler(sch);
            } 
        }

        private void DownloadARPaymentType()
        {
            _entityName = typeof(ARPaymentType).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<ARPaymentType> arPaymentTypes = _bpmServerSG.GetUpdateARPaymentType(_branchId, _lastModifiedDt, _serverDate);
            bool result = _bpmBranchService.UpdateARPaymentType(arPaymentTypes);

            if (result)
            {
                Scheduler sch = new Scheduler();
                sch.EntityName = _entityName;
                sch.LastUpdateDt = _serverDate;
                result = ACABatchScheduler.UpdateScheduler(sch);
            } 
        }

        private void DownloadRTARPaymentTypeARPayment()
        {
            _entityName = typeof(RTARPaymentTypeARPayment).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<RTARPaymentTypeARPayment> rts = _bpmServerSG.GetUpdateRTARPaymentTypeARPayment(_branchId, _lastModifiedDt, _serverDate);
            bool result = _bpmBranchService.UpdateRTARPaymentTypeARPayment(rts);

            if (result)
            {
                Scheduler sch = new Scheduler();
                sch.EntityName = _entityName;
                sch.LastUpdateDt = _serverDate;
                result = ACABatchScheduler.UpdateScheduler(sch);
            } 
        }

        private void DownloadReceipt()
        {
            _entityName = typeof(Receipt).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<Receipt> receipts = _bpmServerSG.GetUpdateReceipt(_branchId, _lastModifiedDt, _serverDate);
            bool result = _bpmBranchService.UpdateReceipt(receipts);

            if (result)
            {
                Scheduler sch = new Scheduler();
                sch.EntityName = _entityName;
                sch.LastUpdateDt = _serverDate;
                result = ACABatchScheduler.UpdateScheduler(sch);
            } 
        }

        private void DownloadReceiptItem()
        {
            _entityName = typeof(ReceiptItem).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<ReceiptItem> receiptItems = _bpmServerSG.GetUpdateReceiptItem(_branchId, _lastModifiedDt, _serverDate);
            bool result = _bpmBranchService.UpdateReceiptItem(receiptItems);

            if (result)
            {
                Scheduler sch = new Scheduler();
                sch.EntityName = _entityName;
                sch.LastUpdateDt = _serverDate;
                result = ACABatchScheduler.UpdateScheduler(sch);
            } 
        }

        #region IJob Members

        public void Execute(JobExecutionContext context)
        {
            try
            {
                DownloadAR();
                DownloadARPayment();
                DownloadPayment();
                DownloadARPaymentType();
                DownloadRTARPaymentTypeARPayment();
                DownloadReceipt();
                DownloadReceiptItem();

                context.Status = StatusCode.Success;
                context.Commit();
            }
            catch (Exception ex)
            {
                //StreamWriter sw = new StreamWriter(IntegrationFileNames.JobLogPath);
                //sw.Write(ex.ToString());
                //sw.Close();
            }
        }

        #endregion

    }
}

 */
