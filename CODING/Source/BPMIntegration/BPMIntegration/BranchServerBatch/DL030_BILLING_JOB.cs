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
    public class DL030_BILLING_JOB : IJob
    {
        private const string DL_NAME_ID = "DL030";
        private const string DL_FULL_NAME = "DL030_BILLING";
        private const string BRANCH_ID = "BRANCH_ID";
        private const string DL_LOT = "DL_LOT";

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
        private int _dlLot;

        public DL030_BILLING_JOB()
        {
            _depMgr = new ACADepLineMgr();
            _logger = ACABatchLogger.GetInstance(DL_FULL_NAME);

            _bpmBranchService = new BPMBranchService();
            _bpmServerSG = new BpmServerDownloadSG();            
            _branchId = ConfigurationManager.AppSettings[BRANCH_ID];
            _dlLot = Convert.ToInt32(ConfigurationManager.AppSettings[DL_LOT]);
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
                    case "PrintPool":
                        {
                            param.FileName = "PrintPool";
                            ret = DownloadPrintPool(param);
                        }
                        break;
                    case "GrpPrintPool":
                        {
                            param.FileName = "GrpPrintPool";
                            ret = DownloadGrpPrintPool(param);
                        }
                        break;         
                    case "BillingDetail":
                        {
                            param.FileName = "BillingDetail";
                            ret = DownloadBillingDetail(param);
                        }
                        break;
                    case "GreenReceiptDetail":
                        {
                            param.FileName = "GreenReceiptDetail";
                            ret = DownloadGreenReceiptDetail(param);
                        }
                        break;
                    //Removed
                    //case "GreenReceiptPrintHistory":
                    //    {
                    //        param.FileName = "GreenReceiptPrintHistory";
                    //        ret = DownloadGreenReceiptPrintHistory(param);
                    //    }
                    //    break;

                    //Renamed to PrintHistory
                    //case "BillPrintTracking":
                    case "PrintHistory":
                        {
                            param.FileName = "PrintHistory";
                            ret = DownloadPrintHistory(param);
                        }
                        break;

                    //Renamed to DeliveryHistory
                    //case "BillSentTracking":
                    case "DeliveryHistory":
                        {
                            param.FileName = "DeliveryHistory";
                            ret = DownloadDeliveryHistory(param);
                        }
                        break;
                    case "MaxBillSeqNo":
                        {
                            param.FileName = "MaxBillSeqNo";
                            ret = DownloadMaxBillSeqNo(param);
                        }
                        break;
                    case "Approver":
                        {
                            param.FileName = "Approver";
                            ret = DownloadApprover(param);
                        }
                        break;
                    case "DeliveryPlace":
                        {
                            param.FileName = "DeliveryPlace";
                            ret = DownloadDeliveryPlace(param);
                        }
                        break;
                    //Renamed to PWBBillStatus
                    //case "BillMasterProcess":
                    case "PWBBillStatus":
                        {
                            param.FileName = "PWBBillStatus";
                            ret = DownloadPWBBillStatus(param);
                        }
                        break;
                    case "BarcodeMRU":
                        {
                            param.FileName = "BarcodeMRU";
                            ret = DownloadBarcodeMRU(param);
                        }
                        break;
                    //add BillStatus
                    case "BillStatus":
                        {
                            param.FileName = "BillStatus";
                            ret = DownloadBillStatus(param);
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

        private bool DownloadPrintPool(ACABatchParam param)
        {
            _entityName = typeof(PrintPoolInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<PrintPoolInfo> list = new List<PrintPoolInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [PrintPool]");
            list = _bpmServerSG.GetUpdatePrintPool(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [PrintPool]", "Total rows = " + list.Count.ToString());

                bool result = _bpmBranchService.UpdatePrintPool(list, param);
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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "PrintPool");
                return true;
            }
        }

        private bool DownloadGrpPrintPool(ACABatchParam param)
        {
            _entityName = typeof(GrpPrintPoolInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<GrpPrintPoolInfo> list = new List<GrpPrintPoolInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [GrpPrintPool]");
            list = _bpmServerSG.GetUpdateGrpPrintPool(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [GrpPrintPool]", "Total rows = " + list.Count.ToString());

                bool result = _bpmBranchService.UpdateGrpPrintPool(list, param);
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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "GrpPrintPool");
                return true;
            }
        }

        private bool DownloadGreenReceiptDetail(ACABatchParam param)
        {
            _entityName = typeof(GreenReceiptDetailInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<GreenReceiptDetailInfo> list = new List<GreenReceiptDetailInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [GreenReceiptDetail]");
            list = _bpmServerSG.GetUpdateGreenReceiptDetail(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [GreenReceiptDetail]", "Total rows = " + list.Count.ToString());

                bool result = _bpmBranchService.UpdateGreenReceiptDetail(list, param);
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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "GreenReceiptDetail");
                return true;
            }
        }

        //private bool DownloadGreenReceiptPrintHistory(ACABatchParam param)
        //{
        //    _entityName = typeof(GreenReceiptPrintHistoryInfo).Name;
        //    _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
        //    List<GreenReceiptPrintHistoryInfo> list = new List<GreenReceiptPrintHistoryInfo>();
        //    _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [GreenReceiptPrintHistory]");
        //    list = _bpmServerSG.GetUpdateGreenReceiptPrintHistory(_branchId, _lastModifiedDt, _serverDate);

        //    if (list.Count > 0)
        //    {
        //        _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [GreenReceiptPrintHistory]", "Total rows = " + list.Count.ToString());

        //        bool result = _bpmBranchService.UpdateGreenReceiptPrintHistory(list, param);
        //        if (result)
        //        {
        //            Scheduler sch = new Scheduler();
        //            sch.EntityName = _entityName;
        //            sch.LastUpdateDt = _serverDate;
        //            sch.JobType = _jobType;
        //            result = ACABatchScheduler.UpdateScheduler(sch);
        //            return true;
        //        }
        //        else
        //            return false;
        //    }
        //    else
        //    {
        //        _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "GreenReceiptPrintHistory");
        //        return true;
        //    }
        //}

        private bool DownloadBillingDetail(ACABatchParam param)
        {
            bool _isRecRemained = false;

            _entityName = typeof(BillingDetailInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<BillingDetailInfo> billingDetails = new List<BillingDetailInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [BillingDetail]");
            billingDetails = _bpmServerSG.GetUpdateBillingDetail(_branchId, _lastModifiedDt, _serverDate);

            if (billingDetails.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [BillingDetail]", "Total rows = " + billingDetails.Count.ToString());

                bool result = _bpmBranchService.UpdateBillingDetail(billingDetails, param);

                if (billingDetails.Count >= (_dlLot>0?_dlLot:int.MaxValue))
                {
                    billingDetails.Sort(new CompareDateTime());
                    _serverDate = billingDetails[billingDetails.Count - 1].ModifiedDt.Value;
                    _isRecRemained = true;
                }
                else 
                {
                    _isRecRemained = false;
                }
                
                if (result)
                {
                    Scheduler sch = new Scheduler();
                    sch.EntityName = _entityName;
                    sch.LastUpdateDt = _serverDate;
                    sch.JobType = _jobType;
                    result = ACABatchScheduler.UpdateScheduler(sch);

                    if (_isRecRemained)
                    {
                        DownloadBillingDetail(param);
                    }
                    else
                        return true;
                }
                else
                    return false;
            }
            else
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "BillingDetail");
                return true;
            }

            return true;
        }

        private bool DownloadPrintHistory(ACABatchParam param)
        {
            _entityName = typeof(PrintHistoryInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<PrintHistoryInfo> PrintHistories = new List<PrintHistoryInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [PrintHistory]");
            PrintHistories = _bpmServerSG.GetUpdatePrintHistory(_branchId, _lastModifiedDt, _serverDate);

            if (PrintHistories.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [PrintHistory]", "Total rows = " + PrintHistories.Count.ToString());

                bool result = _bpmBranchService.UpdatePrintHistory(PrintHistories, param);
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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "PrintHistory");
                return true;
            }

        }

        private bool DownloadDeliveryHistory(ACABatchParam param)
        {
            _entityName = typeof(DeliveryHistoryInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<DeliveryHistoryInfo> DeliveryHistories = new List<DeliveryHistoryInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [DeliveryHistory]");
            DeliveryHistories = _bpmServerSG.GetUpdateDeliveryHistory(_branchId, _lastModifiedDt, _serverDate);

            if (DeliveryHistories.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [DeliveryHistory]", "Total rows = " + DeliveryHistories.Count.ToString());

                bool result = _bpmBranchService.UpdateDeliveryHistory(DeliveryHistories, param);
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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "DeliveryHistory");
                return true;
            }

        }

        private bool DownloadMaxBillSeqNo(ACABatchParam param)
        {
            _entityName = typeof(MaxBillSeqNoInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<MaxBillSeqNoInfo> list = new List<MaxBillSeqNoInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [MaxBillSeqNo]");
            list = _bpmServerSG.GetUpdateMaxBillSeqNo(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [MaxBillSeqNo]", "Total rows = " + list.Count.ToString());

                bool result = _bpmBranchService.UpdateMaxBillSeqNo(list, param);
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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "MaxBillSeqNo");
                return true;
            }
        }
        
        private bool DownloadApprover(ACABatchParam param)
        {
            _entityName = typeof(ApproverInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<ApproverInfo> list = new List<ApproverInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [Approver]");
            list = _bpmServerSG.GetUpdateApprover(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [Approver]", "Total rows = " + list.Count.ToString());

                bool result = _bpmBranchService.UpdateApprover(list, param);
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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "Approver");
                return true;
            }
        }

        private bool DownloadDeliveryPlace(ACABatchParam param)
        {
            _entityName = typeof(DeliveryPlaceInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<DeliveryPlaceInfo> list = new List<DeliveryPlaceInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [DeliveryPlace]");
            list = _bpmServerSG.GetUpdateDeliveryPlace(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [DeliveryPlace]", "Total rows = " + list.Count.ToString());

                bool result = _bpmBranchService.UpdateDeliveryPlace(list, param);
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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "DeliveryPlace");
                return true;
            }
        }

        private bool DownloadPWBBillStatus(ACABatchParam param)
        {
            _entityName = typeof(PWBBillStatusInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<PWBBillStatusInfo> list = new List<PWBBillStatusInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [PWBBillStatus]");
            list = _bpmServerSG.GetUpdatePWBBillStatus(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [PWBBillStatus]", "Total rows = " + list.Count.ToString());

                bool result = _bpmBranchService.UpdatePWBBillStatus(list, param);
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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "PWBBillStatus");
                return true;
            }
        }

        private bool DownloadBarcodeMRU(ACABatchParam param)
        {
            _entityName = typeof(BarcodeMRUInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<BarcodeMRUInfo> list = new List<BarcodeMRUInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [BarcodeMRu]");
            list = _bpmServerSG.GetUpdateBarcodeMRU(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [BarcodeMRu]", "Total rows = " + list.Count.ToString());

                bool result = _bpmBranchService.UpdateBarcodeMRU(list, param);
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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "BarcodeMRU");
                return true;
            }
        }

        private bool DownloadBillStatus(ACABatchParam param)
        {
            _entityName = typeof(BillStatusInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<BillStatusInfo> list = new List<BillStatusInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [BillStatus]");
            list = _bpmServerSG.GetUpdateBillStatus(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [BillStatus]", "Total rows = " + list.Count.ToString());

                bool result = _bpmBranchService.UpdateBillStatus(list, param);
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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "BillStatus");
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

    class CompareDateTime : IComparer<BillingDetailInfo>
    {
        public int Compare(BillingDetailInfo x, BillingDetailInfo y)
        {
            return DateTime.Compare(x.ModifiedDt.Value, y.ModifiedDt.Value);
        }
    }
}
