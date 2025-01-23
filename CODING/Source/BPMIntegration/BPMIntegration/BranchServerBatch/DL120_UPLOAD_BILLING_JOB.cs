using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Configuration;
using Avanade.ACA.Batch;
using PEA.BPM.Integration.ACABatchService;
using PEA.BPM.Integration.BPMIntegration.BS;
using PEA.BPM.Integration.BPMIntegration.SG;
using PEA.BPM.Integration.BPMIntegration.Interface.Services;
using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;
using PEA.BPM.Integration.BPMIntegration.Interface.Constants;

namespace PEA.BPM.Integration.BPMIntegration.BranchServerBatch
{
    public class DL120_UPLOAD_BILLING_JOB : IJob
    {
        private const string DL_NAME_ID = "DL120";
        private const string DL_FULL_NAME = "DL120_UPLOAD_BILLING";
        private const string BRANCH_ID = "BRANCH_ID";
        private string _activeFileKey;
        private ACADepLineMgr _depMgr;
        private ACABatchLogger _logger;

        private IBpmBranchService _bpmBranchService;
        private IBpmServerService _bpmServerSG;
        private string _entityName;
        private DateTime _serverDate;
        private DateTime _localDate;
        private string _branchId;

        public DL120_UPLOAD_BILLING_JOB()
        {
            _depMgr = new ACADepLineMgr();
            _logger = ACABatchLogger.GetInstance(DL_FULL_NAME);

            _bpmBranchService = new BPMBranchService();
            _bpmServerSG = new BpmServerUploadSG();
            _branchId = ConfigurationManager.AppSettings[BRANCH_ID];
            _localDate = DateTime.Now;//_service.GetDbServerTime();
            _logger.BranchId = _branchId;
        }

        #region Additional Members

        //wrapper method
        private bool ExportData(Guid batchKey, string tb, bool overwrite)
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
                            ret = UploadPrintPool(param);
                        }
                        break;
                    case "GrpPrintPool":
                        {
                            param.FileName = "GrpPrintPool";
                            ret = UploadGrpPrintPool(param);
                        }
                        break;
                    case "GreenReceiptDetail":
                        {
                            param.FileName = "GreenReceiptDetail";
                            ret = UploadGreenReceiptDetail(param);
                        }
                        break;
                    //case "BillPrintTracking":
                    case "PrintHistory":
                        {
                            param.FileName = "PrintHistory";
                            ret = UploadPrintHistory(param);
                        }
                        break;
                    case "DeliveryHistory":
                        {
                            param.FileName = "DeliveryHistory";
                            ret = UploadDeliveryHistory(param);
                        }
                        break;
                    case "MaxBillSeqNo":
                        {
                            param.FileName = "MaxBillSeqNo";
                            ret = UploadMaxBillSeqNo(param);
                        }
                        break;
                    case "Approver":
                        {
                            param.FileName = "Approver";
                            ret = UploadApprover(param);
                        }
                        break;
                    case "DeliveryPlace":
                        {
                            param.FileName = "DeliveryPlace";
                            ret = UploadDeliveryPlace(param);
                        }
                        break;
                    //case "GreenReceiptPrintHistory":
                    //    {
                    //        param.FileName = "GreenReceiptPrintHistory";
                    //        ret = UploadGreenReceiptPrintHistory(param);
                    //    }
                    //    break;
                    case "BarcodeMRU":
                        {
                            param.FileName = "BarcodeMRU";
                            ret = UploadBarcodeMRU(param);
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
                _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, e.ToString(), string.Format("Uploading data failed, {0}", tb));
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

        private bool UploadPrintHistory(ACABatchParam param)
        {
            //_entityName = typeof(BillPrintTrackingInfo).Name;
            //_entityName = "BillPrintTracking";
            _entityName = "PrintHistory";
            //_serverDate = _bpmServerSG.GetServerTime();
            //string _infName = "BillPrintTracking";
            string _infName = "PrintHistory";

            try
            {
                List<PrintHistoryInfo> list = new List<PrintHistoryInfo>();
                list = _bpmBranchService.GetToUploadPrintHistory(_serverDate);
                int listCount = list.Count;
                int rows = 0;

                if (listCount > 0)
                {
                    _logger.HandleLog(param.BatchKey, "0", _infName, _entityName, _localDate, _serverDate, listCount);

                    rows = _bpmServerSG.UploadPrintHistory(list, _branchId);

                    if (rows == list.Count)
                        return _logger.HandleLog(param.BatchKey, "1", _infName, _entityName, _localDate, _serverDate, rows);
                    else
                        return false;
                }
                else
                    return _logger.HandleLog(param.BatchKey, "2", _infName, _entityName, _localDate, _serverDate, rows);
            }
            catch (Exception ex)
            {
                int syncRows = _bpmBranchService.SetSyncPrintHistory(_serverDate);
                return _logger.HandleLog(param.BatchKey, "3", _infName, _entityName, _localDate, _serverDate, syncRows, ex.ToString());
            }
        }

        private bool UploadDeliveryHistory(ACABatchParam param)
        {
            //_entityName = typeof(BillSentTrackingInfo).Name;
            _entityName = "DeliveryHistory";
            //_serverDate = _bpmServerSG.GetServerTime();
            string _infName = "DeliveryHistory";

            try
            {
                List<DeliveryHistoryInfo> list = new List<DeliveryHistoryInfo>();
                list = _bpmBranchService.GetToUploadDeliveryHistory(_serverDate);
                int listCount = list.Count;
                int rows = 0;

                if (listCount > 0)
                {
                    _logger.HandleLog(param.BatchKey, "0", _infName, _entityName, _localDate, _serverDate, listCount);

                    rows = _bpmServerSG.UploadDeliveryHistory(list, _branchId);

                    if (rows == list.Count)
                        return _logger.HandleLog(param.BatchKey, "1", _infName, _entityName, _localDate, _serverDate, rows);
                    else
                        return false;
                }
                else
                    return _logger.HandleLog(param.BatchKey, "2", _infName, _entityName, _localDate, _serverDate, rows);
            }
            catch (Exception ex)
            {
                int syncRows = _bpmBranchService.SetSyncDeliveryHistory(_serverDate);
                return _logger.HandleLog(param.BatchKey, "3", _infName, _entityName, _localDate, _serverDate, syncRows, ex.ToString());
            }
        }

        private bool UploadApprover(ACABatchParam param)
        {
            //_entityName = typeof(ApproverInfo).Name;
            _entityName = "Approver";
            //_serverDate = _bpmServerSG.GetServerTime();
            string _infName = "Approver";

            try
            {
                List<ApproverInfo> list = new List<ApproverInfo>();
                list = _bpmBranchService.GetToUploadApprover(_serverDate);
                int listCount = list.Count;
                int rows = 0;

                if (listCount > 0)
                {
                    _logger.HandleLog(param.BatchKey, "0", _infName, _entityName, _localDate, _serverDate, listCount);

                    rows = _bpmServerSG.UploadApprover(list, _branchId);

                    if (rows == list.Count)
                        return _logger.HandleLog(param.BatchKey, "1", _infName, _entityName, _localDate, _serverDate, rows);
                    else
                        return false;
                }
                else
                    return _logger.HandleLog(param.BatchKey, "2", _infName, _entityName, _localDate, _serverDate, rows);
            }
            catch (Exception ex)
            {
                int syncRows = _bpmBranchService.SetSyncApprover(_serverDate);
                return _logger.HandleLog(param.BatchKey, "3", _infName, _entityName, _localDate, _serverDate, syncRows, ex.ToString());
            }
        }

        private bool UploadDeliveryPlace(ACABatchParam param)
        {
            //_entityName = typeof(DeliveryPlaceInfo).Name;
            _entityName = "DeliveryPlace";
            //_serverDate = _bpmServerSG.GetServerTime();
            string _infName = "DeliveryPlace";

            try
            {
                List<DeliveryPlaceInfo> list = new List<DeliveryPlaceInfo>();
                list = _bpmBranchService.GetToUploadDeliveryPlace(_serverDate);
                int listCount = list.Count;
                int rows = 0;

                if (listCount > 0)
                {
                    _logger.HandleLog(param.BatchKey, "0", _infName, _entityName, _localDate, _serverDate, listCount);

                    rows = _bpmServerSG.UploadDeliveryPlace(list, _branchId);

                    if (rows == list.Count)
                        return _logger.HandleLog(param.BatchKey, "1", _infName, _entityName, _localDate, _serverDate, rows);
                    else
                        return false;
                }
                else
                    return _logger.HandleLog(param.BatchKey, "2", _infName, _entityName, _localDate, _serverDate, rows);
            }
            catch (Exception ex)
            {
                int syncRows = _bpmBranchService.SetSyncDeliveryPlace(_serverDate);
                return _logger.HandleLog(param.BatchKey, "3", _infName, _entityName, _localDate, _serverDate, syncRows, ex.ToString());
            }
        }

        private bool UploadPrintPool(ACABatchParam param)
        {
            _entityName = typeof(PrintPoolInfo).Name;
            string _infName = "PrintPool";

            try
            {
                List<PrintPoolInfo> list = new List<PrintPoolInfo>();
                list = _bpmBranchService.GetToUploadPrintPool(_serverDate);
                int listCount = list.Count;
                int rows = 0;

                if (listCount > 0)
                {
                    _logger.HandleLog(param.BatchKey, "0", _infName, _entityName, _localDate, _serverDate, listCount);

                    rows = _bpmServerSG.UploadPrintPool(list, _branchId);

                    if (rows == list.Count)
                        return _logger.HandleLog(param.BatchKey, "1", _infName, _entityName, _localDate, _serverDate, rows);
                    else
                        return false;
                }
                else
                    return _logger.HandleLog(param.BatchKey, "2", _infName, _entityName, _localDate, _serverDate, rows);
            }
            catch (Exception ex)
            {
                int syncRows = _bpmBranchService.SetSyncPrintPool(_serverDate);
                return _logger.HandleLog(param.BatchKey, "3", _infName, _entityName, _localDate, _serverDate, syncRows, ex.ToString());
            }
        }

        private bool UploadGrpPrintPool(ACABatchParam param)
        {
            _entityName = typeof(GrpPrintPoolInfo).Name;
            //_serverDate = _bpmServerSG.GetServerTime();
            string _infName = "GrpPrintPool";

            try
            {
                List<GrpPrintPoolInfo> list = new List<GrpPrintPoolInfo>();
                list = _bpmBranchService.GetToUploadGrpPrintPool(_serverDate);
                int listCount = list.Count;
                int rows = 0;

                if (listCount > 0)
                {
                    _logger.HandleLog(param.BatchKey, "0", _infName, _entityName, _localDate, _serverDate, listCount);

                    rows = _bpmServerSG.UploadGrpPrintPool(list, _branchId);

                    if (rows == list.Count)
                        return _logger.HandleLog(param.BatchKey, "1", _infName, _entityName, _localDate, _serverDate, rows);
                    else
                        return false;
                }
                else
                    return _logger.HandleLog(param.BatchKey, "2", _infName, _entityName, _localDate, _serverDate, rows);
            }
            catch (Exception ex)
            {
                int syncRows = _bpmBranchService.SetSyncGrpPrintPool(_serverDate);
                return _logger.HandleLog(param.BatchKey, "3", _infName, _entityName, _localDate, _serverDate, syncRows, ex.ToString());
            }            
        }

        private bool UploadMaxBillSeqNo(ACABatchParam param)
        {
            _entityName = typeof(MaxBillSeqNoInfo).Name;
            string _infName = "PrintPool";

            try
            {
                List<MaxBillSeqNoInfo> list = new List<MaxBillSeqNoInfo>();
                list = _bpmBranchService.GetToUploadMaxBillSeqNo(_serverDate);
                int listCount = list.Count;
                int rows = 0;

                if (listCount > 0)
                {
                    _logger.HandleLog(param.BatchKey, "0", _infName, _entityName, _localDate, _serverDate, listCount);

                    rows = _bpmServerSG.UploadMaxBillSeqNo(list, _branchId);

                    if (rows == list.Count)
                        return _logger.HandleLog(param.BatchKey, "1", _infName, _entityName, _localDate, _serverDate, rows);
                    else
                        return false;
                }
                else
                    return _logger.HandleLog(param.BatchKey, "2", _infName, _entityName, _localDate, _serverDate, rows);
            }
            catch (Exception ex)
            {
                int syncRows = _bpmBranchService.SetSyncMaxBillSeqNo(_serverDate);
                return _logger.HandleLog(param.BatchKey, "3", _infName, _entityName, _localDate, _serverDate, syncRows, ex.ToString());
            }            
        }

        private bool UploadGreenReceiptDetail(ACABatchParam param)
        {
            _entityName = typeof(GreenReceiptDetailInfo).Name;
            string _infName = "GreenReceiptDetail";

            try
            {
                List<GreenReceiptDetailInfo> list = new List<GreenReceiptDetailInfo>();
                list = _bpmBranchService.GetToUploadGreenReceiptDetail(_serverDate);
                int listCount = list.Count;
                int rows = 0;

                if (listCount > 0)
                {
                    _logger.HandleLog(param.BatchKey, "0", _infName, _entityName, _localDate, _serverDate, listCount);

                    rows = _bpmServerSG.UploadGreenReceiptDetail(list, _branchId);

                    if (rows == list.Count)
                        return _logger.HandleLog(param.BatchKey, "1", _infName, _entityName, _localDate, _serverDate, rows);
                    else
                        return false;
                }
                else
                    return _logger.HandleLog(param.BatchKey, "2", _infName, _entityName, _localDate, _serverDate, rows);
            }
            catch (Exception ex)
            {
                int syncRows = _bpmBranchService.SetSyncGreenReceiptDetail(_serverDate);
                return _logger.HandleLog(param.BatchKey, "3", _infName, _entityName, _localDate, _serverDate, syncRows, ex.ToString());
            }            
        }

        //private bool UploadGreenReceiptPrintHistory(ACABatchParam param)
        //{
        //    _entityName = typeof(GreenReceiptPrintHistoryInfo).Name;
        //    string _infName = "GreenReceiptPrintHistory";

        //    try
        //    {
        //        List<GreenReceiptPrintHistoryInfo> list = new List<GreenReceiptPrintHistoryInfo>();
        //        list = _bpmBranchService.GetToUploadGreenReceiptPrintHistory(_serverDate);
        //        int listCount = list.Count;
        //        int rows = 0;

        //        if (listCount > 0)
        //        {
        //            _logger.HandleLog(param.BatchKey, "0", _infName, _entityName, _localDate, _serverDate, listCount);

        //            rows = _bpmServerSG.UploadGreenReceiptPrintHistory(list, _branchId);

        //            if (rows == list.Count)
        //                return _logger.HandleLog(param.BatchKey, "1", _infName, _entityName, _localDate, _serverDate, rows);
        //            else
        //                return false;
        //        }
        //        else
        //            return _logger.HandleLog(param.BatchKey, "2", _infName, _entityName, _localDate, _serverDate, rows);
        //    }
        //    catch (Exception ex)
        //    {
        //        int syncRows = _bpmBranchService.SetSyncGreenReceiptPrintHistory(_serverDate);
        //        return _logger.HandleLog(param.BatchKey, "3", _infName, _entityName, _localDate, _serverDate, syncRows, ex.ToString());
        //    }            
        //}

        private bool UploadBarcodeMRU(ACABatchParam param)
        {
            _entityName = typeof(BarcodeMRUInfo).Name;
            string _infName = "BarcodeMRU";

            try
            {
                List<BarcodeMRUInfo> list = new List<BarcodeMRUInfo>();
                list = _bpmBranchService.GetToUploadBarcodeMRU(_serverDate);
                int listCount = list.Count;
                int rows = 0;

                if (listCount > 0)
                {
                    _logger.HandleLog(param.BatchKey, "0", _infName, _entityName, _localDate, _serverDate, listCount);

                    rows = _bpmServerSG.UploadBarcodeMRU(list, _branchId);

                    if (rows == list.Count)
                        return _logger.HandleLog(param.BatchKey, "1", _infName, _entityName, _localDate, _serverDate, rows);
                    else
                        return false;
                }
                else
                    return _logger.HandleLog(param.BatchKey, "2", _infName, _entityName, _localDate, _serverDate, rows);
            }
            catch (Exception ex)
            {
                int syncRows = _bpmBranchService.SetSyncBarcodeMRU(_serverDate);
                return _logger.HandleLog(param.BatchKey, "3", _infName, _entityName, _localDate, _serverDate, syncRows, ex.ToString());
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

                    if (!ExportData(batchKey, tb, true))
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
                    _logger.Severity = Severity.Level6;
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
                _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, e.ToString(), "เกิดปัญหาการเรียกฟังก์ชันในการประมวลผลแบ็ช");
                context.Status = StatusCode.Failed;
                context.Commit();
            }
        }

        #endregion
    }
}
