using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using System.Configuration;

using Avanade.ACA.Batch;

using PEA.BPM.Integration.BPMIntegration.Interface.Services;
using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;
using PEA.BPM.Integration.BPMIntegration.BS;
using PEA.BPM.Integration.ACABatchService;
using PEA.BPM.Integration.BPMIntegration.SG;
using PEA.BPM.Integration.BPMIntegration.Interface.Constants;

namespace PEA.BPM.Integration.BPMIntegration.BranchServerBatch
{
    public class DL060_AGENCY_JOB : IJob
    {
        private const string DL_NAME_ID = "DL060";
        private const string DL_FULL_NAME = "DL060_AGENCY";
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

        public DL060_AGENCY_JOB()
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
                    case "BillStatusInfo":
                        {
                            param.FileName = "BillStatusInfo";
                            ret = DownloadBillStatus(param);
                        }
                        break;
                    case "BillBook":
                        {
                            param.FileName = "BillBook";
                            ret = DownloadBillBook(param);
                        }
                        break;
                    case "BillBookDetail":
                        {
                            param.FileName = "BillBookDetail";
                            ret = DownloadBillBookDetail(param);
                        }
                        break;
                    case "BillBookInputItem":
                        {
                            param.FileName = "BillBookInputItem";
                            ret = DownloadBillBookInputItem(param);
                        }
                        break;
                    case "BillBookInputSet":
                        {
                            param.FileName = "BillBookInputSet";
                            ret = DownloadBillBookInputSet(param);
                        }
                        break;
                    case "AgencyCommission":
                        {
                            param.FileName = "AgencyCommission";
                            ret = DownloadAgencyCommission(param);
                        }
                        break;
                    case "RTAgencyContractMru":
                        {
                            param.FileName = "RTAgencyContractMru";
                            ret = DownloadRTAgencyContractMru(param);
                        }
                        break;
                    case "RTAgencyCommissionBillBook":
                        {
                            param.FileName = "RTAgencyCommissionBillBook";
                            ret = DownloadRTAgencyCommissionBillBook(param);
                        }
                        break;
                    //case "AgencyModuleConfig":
                    //    {
                    //        param.FileName = "AgencyModuleConfig";
                    //        ret = DownloadAgencyModuleConfig(param);
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

        private bool DownloadBillBook(ACABatchParam param)
        {
            _entityName = typeof(BillBookInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<BillBookInfo> list = new List<BillBookInfo>();
           _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [BillBook]");
            list = _bpmServerSG.GetUpdateBillBook(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [BillBook]", "Total rows = " + list.Count.ToString());
                bool result = _bpmBranchService.UpdateBillBook(list, param);
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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "BillBook");
                return true;
            }
        }

        private bool DownloadBillStatus(ACABatchParam param)
        {
            _entityName = typeof(BillStatusInfoInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<BillStatusInfoInfo> list = new List<BillStatusInfoInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [BillStatusInfo]");
            list = _bpmServerSG.GetUpdateBillStatusInfo(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [BillStatusInfo]", "Total rows = " + list.Count.ToString());
                bool result = _bpmBranchService.UpdateBillStatusInfo(list, param);
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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "BillStatusInfo");
                return true;
            }
        }

        private bool DownloadBillBookDetail(ACABatchParam param)
        {
            _entityName = typeof(BillBookDetailInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<BillBookDetailInfo> list = new List<BillBookDetailInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [BillBookDetail]");
            list = _bpmServerSG.GetUpdateBillBookDetail(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [BillBookDetail]", "Total rows = " + list.Count.ToString());
                bool result = _bpmBranchService.UpdateBillBookDetail(list, param);
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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "BillBookDetail");
                return true;
            }
        }

        private bool DownloadBillBookInputItem(ACABatchParam param)
        {
            _entityName = typeof(BillBookInputItemInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<BillBookInputItemInfo> list = new List<BillBookInputItemInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [BillBookInputItem]");
            list = _bpmServerSG.GetUpdateBillBookInputItem(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [BillBookInputItem]", "Total rows = " + list.Count.ToString());
                bool result = _bpmBranchService.UpdateBillBookInputItem(list, param);
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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "BillBookInputItem");
                return true;
            }
        }

        private bool DownloadBillBookInputSet(ACABatchParam param)
        {
            _entityName = typeof(BillBookInputSetInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<BillBookInputSetInfo> list = new List<BillBookInputSetInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [BillBookInputSet]");
            list = _bpmServerSG.GetUpdateBillBookInputSet(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [BillBookInputSet]", "Total rows = " + list.Count.ToString());
                bool result = _bpmBranchService.UpdateBillBookInputSet(list, param);
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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "BillBookInputSet");
                return true;
            }
        }

        private bool DownloadAgencyCommission(ACABatchParam param)
        {
            _entityName = typeof(AgencyCommissionInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<AgencyCommissionInfo> list = new List<AgencyCommissionInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [AgencyCommission]");
            list = _bpmServerSG.GetUpdateAgencyCommission(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [AgencyCommission]", "Total rows = " + list.Count.ToString());
                bool result = _bpmBranchService.UpdateAgencyCommission(list, param);
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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "AgencyCommission");
                return true;
            }
        }

        private bool DownloadRTAgencyContractMru(ACABatchParam param)
        {
            _entityName = typeof(RTAgencyContractMruInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<RTAgencyContractMruInfo> list = new List<RTAgencyContractMruInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [RTAgencyContractMru]");
            list = _bpmServerSG.GetUpdateRTAgencyContractMru(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [RTAgencyContractMru]", "Total rows = " + list.Count.ToString());
                bool result = _bpmBranchService.UpdateRTAgencyContractMru(list, param);
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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "RTAgencyContractMru");
                return true;
            }
        }

        private bool DownloadRTAgencyCommissionBillBook(ACABatchParam param)
        {
            _entityName = typeof(RTAgencyCommissionBillBookInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<RTAgencyCommissionBillBookInfo> list = new List<RTAgencyCommissionBillBookInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [RTAgencyCommissionBillBook]");
            list = _bpmServerSG.GetUpdateRTAgencyCommissionBillBook(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [RTAgencyCommissionBillBook]", "Total rows = " + list.Count.ToString());
                bool result = _bpmBranchService.UpdateRTAgencyCommissionBillBook(list, param);
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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "RTAgencyCommissionBillBook");
                return true;
            }
        }

        //private bool DownloadAgencyModuleConfig(ACABatchParam param)
        //{
        //    _entityName = typeof(AgencyModuleConfigInfo).Name;
        //    _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
        //    List<AgencyModuleConfigInfo> list = new List<AgencyModuleConfigInfo>();
        //    _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [AgencyModuleConfig]");
        //    list = _bpmServerSG.GetUpdateAgencyModuleConfig(_branchId, _lastModifiedDt, _serverDate);

        //    if (list.Count > 0)
        //    {
        //        _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [AgencyModuleConfig]", "Total rows = " + list.Count.ToString());
        //        bool result = _bpmBranchService.UpdateAgencyModuleConfig(list, param);
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
        //        _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "AgencyModuleConfig");
        //        return true;
        //    }
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
