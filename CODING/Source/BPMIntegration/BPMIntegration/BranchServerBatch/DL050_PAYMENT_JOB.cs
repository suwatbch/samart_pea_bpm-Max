using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Configuration;
using Avanade.ACA.Batch;
using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;
using PEA.BPM.Integration.ACABatchService;
using PEA.BPM.Integration.BPMIntegration.BS;
using PEA.BPM.Integration.BPMIntegration.SG;
using PEA.BPM.Integration.BPMIntegration.Interface.Services;
using PEA.BPM.Integration.BPMIntegration.Interface.Constants;

namespace PEA.BPM.Integration.BPMIntegration.BranchServerBatch
{
    public class DL050_PAYMENT_JOB : IJob
    {
        private const string DL_NAME_ID = "DL050";
        private const string DL_FULL_NAME = "DL050_PAYMENT";
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

        public DL050_PAYMENT_JOB()
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
                    case "Payment":
                        {
                            param.FileName = "Payment";
                            ret = DownloadPayment(param);
                        }
                        break;
                    case "ARPaymentType":
                        {
                            param.FileName = "ARPaymentType";
                            ret = DownloadARPaymentType(param);
                        }
                        break;
                    case "ARPayment":
                        {
                            param.FileName = "ARPayment";
                            ret = DownloadARPayment(param);
                        }
                        break;
                    case "RTARPaymentTypeARPayment":
                        {
                            param.FileName = "RTARPaymentTypeARPayment";
                            ret = DownloadRTARPaymentTypeARPayment(param);
                        }
                        break;
                    case "Receipt":
                        {
                            param.FileName = "Receipt";
                            ret = DownloadReceipt(param);
                        }
                        break;
                    case "ReceiptItem":
                        {
                            param.FileName = "ReceiptItem";
                            ret = DownloadReceiptItem(param);
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

        private bool DownloadPayment(ACABatchParam param)
        {
            _entityName = typeof(Payment).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<Payment> payments = new List<Payment>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [Payment]");
            payments = _bpmServerSG.GetUpdatePayment(_branchId, _lastModifiedDt, _serverDate);

            if (payments.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [Payment]", "Total rows = " + payments.Count.ToString());

                bool result = _bpmBranchService.UpdatePayment(payments, param);
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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "Payment");
                return true;
            }

        }

        private bool DownloadARPaymentType(ACABatchParam param)
        {
            _entityName = typeof(ARPaymentType).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<ARPaymentType> arPaymentTypes = new List<ARPaymentType>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [ARPaymentType]");
            arPaymentTypes = _bpmServerSG.GetUpdateARPaymentType(_branchId, _lastModifiedDt, _serverDate);

            if (arPaymentTypes.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [ARPaymentType]", "Total rows = " + arPaymentTypes.Count.ToString());
                bool result = _bpmBranchService.UpdateARPaymentType(arPaymentTypes, param);

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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "ARPaymentType");
                return true;
            }
        }

        private bool DownloadARPayment(ACABatchParam param)
        {
            _entityName = typeof(ARPayment).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<ARPayment> arPayments = new List<ARPayment>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [ARPayment]");
            arPayments = _bpmServerSG.GetUpdateARPayment(_branchId, _lastModifiedDt, _serverDate);

            if (arPayments.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [ARPayment]", "Total rows = " + arPayments.Count.ToString());
                bool result = _bpmBranchService.UpdateARPayment(arPayments, param);

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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "ARPayment");
                return true;
            }
        }

        private bool DownloadRTARPaymentTypeARPayment(ACABatchParam param)
        {
            _entityName = typeof(RTARPaymentTypeARPayment).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<RTARPaymentTypeARPayment> rtARs = new List<RTARPaymentTypeARPayment>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [RTARPaymentTypeARPayment]");
            rtARs = _bpmServerSG.GetUpdateRTARPaymentTypeARPayment(_branchId, _lastModifiedDt, _serverDate);

            if (rtARs.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [RTARPaymentTypeARPayment]", "Total rows = " + rtARs.Count.ToString());
                bool result = _bpmBranchService.UpdateRTARPaymentTypeARPayment(rtARs, param);

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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "RTARPaymentTypeARPayment");
                return true;
            }
        }

        private bool DownloadReceipt(ACABatchParam param)
        {
            _entityName = typeof(Receipt).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<Receipt> receipts = new List<Receipt>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [Receipt]");
            receipts = _bpmServerSG.GetUpdateReceipt(_branchId, _lastModifiedDt, _serverDate);

            if (receipts.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [Receipt]", "Total rows = " + receipts.Count.ToString());
                bool result = _bpmBranchService.UpdateReceipt(receipts, param);

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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "Receipt");
                return true;
            }
        }

        private bool DownloadReceiptItem(ACABatchParam param)
        {
            _entityName = typeof(ReceiptItem).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<ReceiptItem> receiptItems = new List<ReceiptItem>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [ReceiptItem]");
            receiptItems = _bpmServerSG.GetUpdateReceiptItem(_branchId, _lastModifiedDt, _serverDate);

            if (receiptItems.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [ReceiptItem]", "Total rows = " + receiptItems.Count.ToString());
                bool result = _bpmBranchService.UpdateReceiptItem(receiptItems, param);

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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "ReceiptItem");
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
                        _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, string.Format("ยังคงเหลือข้อมูลที่ยังไม่ถูกแก้ไขและนำเข้าโดยสมบูรณ์, Interface: {0}, Key: {1} ",
                                                    dep.LastFailTb, dep.FileKey), "");

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
