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
    public class DL100_UPLOAD_AGENCY_JOB : IJob
    {
        private const string DL_NAME_ID = "DL100";
        private const string DL_FULL_NAME = "DL100_UPLOAD_AGENCY";
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

        public DL100_UPLOAD_AGENCY_JOB()
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
                    case "BillBook":
                        {
                            param.FileName = "BillBook";
                            ret = UploadBillBook(param);
                        }
                        break;
                    case "BillStatusInfo":
                        {
                            param.FileName = "BillStatusInfo";
                            ret = UploadBillStatus(param);
                        }
                        break;
                    case "BillBookDetail":
                        {
                            param.FileName = "BillBookDetail";
                            ret = UploadBillBookDetail(param);
                        }
                        break;
                    case "BillBookInputItem":
                        {
                            param.FileName = "BillBookInputItem";
                            ret = UploadBillBookInputItem(param);
                        }
                        break;
                    case "BillBookInputSet":
                        {
                            param.FileName = "BillBookInputSet";
                            ret = UploadBillBookInputSet(param);
                        }
                        break;
                    case "AgencyCommission":
                        {
                            param.FileName = "AgencyCommission";
                            ret = UploadAgencyCommission(param);
                        }
                        break;
                    case "RTAgencyContractMru":
                        {
                            param.FileName = "RTAgencyContractMru";
                            ret = UploadRTAgencyContractMru(param);
                        }
                        break;
                    case "RTAgencyCommissionBillBook":
                        {
                            param.FileName = "RTAgencyCommissionBillBook";
                            ret = UploadRTAgencyCommissionBillBook(param);
                        }
                        break;
                    //case "AgencyModuleConfig":
                    //    {
                    //        param.FileName = "AgencyModuleConfig";
                    //        ret = UploadAgencyModuleConfig(param);
                    //    }
                    //    break;
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

        private bool UploadBillBook(ACABatchParam param)
        {
            _entityName = typeof(BillBookInfo).Name;
            //_serverDate = _bpmServerSG.GetServerTime();
            string _infName = "BillBook";

            try
            {
                List<BillBookInfo> list = new List<BillBookInfo>();
                list = _bpmBranchService.GetToUploadBillBook(_serverDate);
                int listCount = list.Count;
                int rows = 0;

                if (listCount > 0)
                {
                    _logger.HandleLog(param.BatchKey, "0", _infName, _entityName, _localDate, _serverDate, listCount);

                    rows = _bpmServerSG.UploadBillBook(list, _branchId);

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
                int syncRows = _bpmBranchService.SetSyncBillBook(_serverDate);
                return _logger.HandleLog(param.BatchKey, "3", _infName, _entityName, _localDate, _serverDate, syncRows, ex.ToString());
            }
        }

        private bool UploadBillStatus(ACABatchParam param)
        {
            _entityName = typeof(BillStatusInfoInfo).Name;
            //_serverDate = _bpmServerSG.GetServerTime();
            string _infName = "BillStatus";

            try
            {
                List<BillStatusInfoInfo> list = new List<BillStatusInfoInfo>();
                list = _bpmBranchService.GetToUploadBillStatusInfo(_serverDate);
                int listCount = list.Count;
                int rows = 0;

                if (listCount > 0)
                {
                    _logger.HandleLog(param.BatchKey, "0", _infName, _entityName, _localDate, _serverDate, listCount);

                    rows = _bpmServerSG.UploadBillStatusInfo(list, _branchId);

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
                int syncRows = _bpmBranchService.SetSyncBillStatusInfo(_serverDate);
                return _logger.HandleLog(param.BatchKey, "3", _infName, _entityName, _localDate, _serverDate, syncRows, ex.ToString());
            }
        }

        private bool UploadBillBookDetail(ACABatchParam param)
        {
            _entityName = typeof(BillBookDetailInfo).Name;
            //_serverDate = _bpmServerSG.GetServerTime();
            string _infName = "BillBookDetail";

            try
            {
                List<BillBookDetailInfo> list = new List<BillBookDetailInfo>();
                list = _bpmBranchService.GetToUploadBillBookDetail(_serverDate);
                int listCount = list.Count;
                int rows = 0;

                if (listCount > 0)
                {
                    _logger.HandleLog(param.BatchKey, "0", _infName, _entityName, _localDate, _serverDate, listCount);

                    rows = _bpmServerSG.UploadBillBookDetail(list, _branchId);

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
                int syncRows = _bpmBranchService.SetSyncBillBookDetail(_serverDate);
                return _logger.HandleLog(param.BatchKey, "3", _infName, _entityName, _localDate, _serverDate, syncRows, ex.ToString());
            }
        }

        private bool UploadBillBookInputItem(ACABatchParam param)
        {
            _entityName = typeof(BillBookInputItemInfo).Name;
            //_serverDate = _bpmServerSG.GetServerTime();
            string _infName = "BillBookInputItem";

            try
            {
                List<BillBookInputItemInfo> list = new List<BillBookInputItemInfo>();
                list = _bpmBranchService.GetToUploadBillBookInputItem(_serverDate);
                int listCount = list.Count;
                int rows = 0;

                if (listCount > 0)
                {
                    _logger.HandleLog(param.BatchKey, "0", _infName, _entityName, _localDate, _serverDate, listCount);

                    rows = _bpmServerSG.UploadBillBookInputItem(list, _branchId);

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
                int syncRows = _bpmBranchService.SetSyncBillBookInputItem(_serverDate);
                return _logger.HandleLog(param.BatchKey, "3", _infName, _entityName, _localDate, _serverDate, syncRows, ex.ToString());
            }
        }

        private bool UploadBillBookInputSet(ACABatchParam param)
        {
            _entityName = typeof(BillBookInputSetInfo).Name;
            //_serverDate = _bpmServerSG.GetServerTime();
            string _infName = "BillBookInputSet";

            try
            {
                List<BillBookInputSetInfo> list = new List<BillBookInputSetInfo>();
                list = _bpmBranchService.GetToUploadBillBookInputSet(_serverDate);
                int listCount = list.Count;
                int rows = 0;

                if (listCount > 0)
                {
                    _logger.HandleLog(param.BatchKey, "0", _infName, _entityName, _localDate, _serverDate, listCount);

                    rows = _bpmServerSG.UploadBillBookInputSet(list, _branchId);

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
                int syncRows = _bpmBranchService.SetSyncBillBookInputSet(_serverDate);
                return _logger.HandleLog(param.BatchKey, "3", _infName, _entityName, _localDate, _serverDate, syncRows, ex.ToString());
            }
        }

        private bool UploadAgencyCommission(ACABatchParam param)
        {
            _entityName = typeof(AgencyCommissionInfo).Name;
            //_serverDate = _bpmServerSG.GetServerTime();
            string _infName = "AgencyCommission";

            try
            {
                List<AgencyCommissionInfo> list = new List<AgencyCommissionInfo>();
                list = _bpmBranchService.GetToUploadAgencyCommission(_serverDate);
                int listCount = list.Count;
                int rows = 0;

                if (listCount > 0)
                {
                    _logger.HandleLog(param.BatchKey, "0", _infName, _entityName, _localDate, _serverDate, listCount);

                    rows = _bpmServerSG.UploadAgencyCommission(list, _branchId);

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
                int syncRows = _bpmBranchService.SetSyncAgencyCommission(_serverDate);
                return _logger.HandleLog(param.BatchKey, "3", _infName, _entityName, _localDate, _serverDate, syncRows, ex.ToString());
            }
        }

        private bool UploadRTAgencyContractMru(ACABatchParam param)
        {
            _entityName = typeof(RTAgencyContractMruInfo).Name;
            //_serverDate = _bpmServerSG.GetServerTime();
            string _infName = "RTAgencyContractMru";

            try
            {
                List<RTAgencyContractMruInfo> list = new List<RTAgencyContractMruInfo>();
                list = _bpmBranchService.GetToUploadRTAgencyContractMru(_serverDate);
                int listCount = list.Count;
                int rows = 0;

                if (listCount > 0)
                {
                    _logger.HandleLog(param.BatchKey, "0", _infName, _entityName, _localDate, _serverDate, listCount);

                    rows = _bpmServerSG.UploadRTAgencyContractMru(list, _branchId);

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
                int syncRows = _bpmBranchService.SetSyncRTAgencyContractMru(_serverDate);
                return _logger.HandleLog(param.BatchKey, "3", _infName, _entityName, _localDate, _serverDate, syncRows, ex.ToString());
            }
        }

        private bool UploadRTAgencyCommissionBillBook(ACABatchParam param)
        {
            _entityName = typeof(RTAgencyCommissionBillBookInfo).Name;
            //_serverDate = _bpmServerSG.GetServerTime();
            string _infName = "RTAgencyCommissionBillBook";

            try
            {
                List<RTAgencyCommissionBillBookInfo> list = new List<RTAgencyCommissionBillBookInfo>();
                list = _bpmBranchService.GetToUploadRTAgencyCommissionBillBook(_serverDate);
                int listCount = list.Count;
                int rows = 0;

                if (listCount > 0)
                {
                    _logger.HandleLog(param.BatchKey, "0", _infName, _entityName, _localDate, _serverDate, listCount);

                    rows = _bpmServerSG.UploadRTAgencyCommissionBillBook(list, _branchId);

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
                int syncRows = _bpmBranchService.SetSyncRTAgencyCommissionBillBook(_serverDate);
                return _logger.HandleLog(param.BatchKey, "3", _infName, _entityName, _localDate, _serverDate, syncRows, ex.ToString());
            }
        }

        //private bool UploadAgencyModuleConfig(ACABatchParam param)
        //{
        //    _entityName = typeof(AgencyModuleConfigInfo).Name;
        //    _lastModifiedDt = _service.GetLastUploadedDate(_entityName);
        //    List<AgencyModuleConfigInfo> list = new List<AgencyModuleConfigInfo>();
        //    list = _bpmBranchService.GetToUploadAgencyModuleConfig(_lastModifiedDt);

        //    if (list.Count > 0)
        //    {
        //        _logger.WriteLog(param.BatchKey, ACABatchLogger.ErrorType.Success, "Getting Data Completed [AgencyModuleConfig]", "Total rows = " + list.Count.ToString());
        //        int rows = _bpmServerSG.UploadAgencyModuleConfig(list, _branchId);
        //        _bpmBranchService.SetSyncAgencyModuleConfig(_lastModifiedDt);

        //        if (rows == list.Count)
        //        {
        //            _logger.WriteLog(param.BatchKey, ACABatchLogger.ErrorType.Success, "Updating Data Completed [AgencyModuleConfig]", "Total rows = " + rows, rows);

        //            Scheduler sch = new Scheduler();
        //            sch.EntityName = _entityName;
        //            sch.LastUpdateDt = _serverDate;
        //            sch.JobType = "UL";
        //            bool result = _service.UpdateScheduler(sch);

        //            return result;
        //        }
        //        else
        //            return false;
        //    }
        //    else
        //    {
        //        _logger.WriteLog(param.BatchKey, ACABatchLogger.ErrorType.Success, "Data were already uploaded at " + _lastModifiedDt, "AgencyModuleConfig");
        //        return true;
        //    }
        //    return true;

        //}

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
                _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, e.ToString(), "เกิดปัญหาการเรียกฟังก์ชันในการประมวลผลแบ็ช");
                context.Status = StatusCode.Failed;
                context.Commit();
            }
        }

        #endregion
    
    }
}
