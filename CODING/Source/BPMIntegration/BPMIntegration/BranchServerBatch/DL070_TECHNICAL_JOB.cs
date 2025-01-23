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
    public class DL070_TECHNICAL_JOB : IJob
    {
        private const string DL_NAME_ID = "DL070";
        private const string DL_FULL_NAME = "DL070_TECHNICAL";
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

        public DL070_TECHNICAL_JOB()
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
                    case "AppSetting":
                        {
                            param.FileName = "AppSetting";
                            ret = DownloadAppSetting(param);
                        }
                        break;
                    case "Terminal":
                        {
                            param.FileName = "Terminal";
                            ret = DownloadTerminal(param);
                        }
                        break;
                    case "UnlockLog":
                        {
                            param.FileName = "UnlockLog";
                            ret = DownloadUnlockLog(param);
                        }
                        break;
                    case "User":
                        {
                            param.FileName = "User";
                            ret = DownloadUser(param);
                        }
                        break;
                    case "Role":
                        {
                            param.FileName = "Role";
                            ret = DownloadRole(param);
                        }
                        break;
                    case "RTRoleUser":
                        {
                            param.FileName = "RTRoleUser";
                            ret = DownloadRTRoleUser(param);
                        }
                        break;
                    case "Function":
                        {
                            param.FileName = "Function";
                            ret = DownloadFunction(param);
                        }
                        break;
                    case "Service":
                        {
                            param.FileName = "Service";
                            ret = DownloadService(param);
                        }
                        break;
                    case "RTRoleFunction":
                        {
                            param.FileName = "RTRoleFunction";
                            ret = DownloadRTRoleFunction(param);
                        }
                        break;
                    case "RTFunctionService":
                        {
                            param.FileName = "RTFunctionService";
                            ret = DownloadRTFunctionService(param);
                        }
                        break;
                    case "UnlockRemark":
                        {
                            param.FileName = "UnlockRemark";
                            ret = DownloadUnlockRemark(param);
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

        private bool DownloadAppSetting(ACABatchParam param)
        {
            _entityName = typeof(AppSettingInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<AppSettingInfo> list = new List<AppSettingInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [AppSetting]");
            list = _bpmServerSG.DownloadAppSetting(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [AppSetting]", "Total rows = " + list.Count.ToString());
                bool result = _bpmBranchService.UpdateAppSetting(list, param);
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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "AppSetting");
                return true;
            }
        }

        private bool DownloadTerminal(ACABatchParam param)
        {
            _entityName = typeof(Terminal).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<Terminal> list = new List<Terminal>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [Terminal]");
            list = _bpmServerSG.DownloadTerminal(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [Terminal]", "Total rows = " + list.Count.ToString());
                bool result = _bpmBranchService.UpdateTerminal(list, param);
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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "Terminal");
                return true;
            }
        }

        private bool DownloadUnlockLog(ACABatchParam param)
        {
            _entityName = typeof(UnlockLogInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<UnlockLogInfo> list = new List<UnlockLogInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [UnlockLog]");
            list = _bpmServerSG.DownloadUnlockLog(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [UnlockLog]", "Total rows = " + list.Count.ToString());
                bool result = _bpmBranchService.UpdateUnlockLog(list, param);
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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "UnlockLog");
                return true;
            }
        }

        private bool DownloadUser(ACABatchParam param)
        {
            _entityName = typeof(UserInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<UserInfo> list = new List<UserInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [User]");
            list = _bpmServerSG.DownloadUser(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [User]", "Total rows = " + list.Count.ToString());
                bool result = _bpmBranchService.UpdateUser(list, param);
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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "User");
                return true;
            }
        }

        private bool DownloadRole(ACABatchParam param)
        {
            _entityName = typeof(RoleInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<RoleInfo> list = new List<RoleInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [Role]");
            list = _bpmServerSG.DownloadRole(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [Role]", "Total rows = " + list.Count.ToString());
                bool result = _bpmBranchService.UpdateRole(list, param);
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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "Role");
                return true;
            }
        }

        private bool DownloadFunction(ACABatchParam param)
        {
            _entityName = typeof(FunctionInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<FunctionInfo> list = new List<FunctionInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [Function]");
            list = _bpmServerSG.DownloadFunction(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [Function]", "Total rows = " + list.Count.ToString());
                bool result = _bpmBranchService.UpdateFunction(list, param);
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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "Function");
                return true;
            }
        }

        private bool DownloadService(ACABatchParam param)
        {
            _entityName = typeof(ServiceInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<ServiceInfo> list = new List<ServiceInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [Service]");
            list = _bpmServerSG.DownloadService(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [Service]", "Total rows = " + list.Count.ToString());
                bool result = _bpmBranchService.UpdateService(list, param);
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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "Service");
                return true;
            }
        }

        private bool DownloadRTFunctionService(ACABatchParam param)
        {
            _entityName = typeof(FunctionServiceInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<FunctionServiceInfo> list = new List<FunctionServiceInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [FunctionService]");
            list = _bpmServerSG.DownloadFunctionService(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [FunctionService]", "Total rows = " + list.Count.ToString());
                bool result = _bpmBranchService.UpdateFunctionService(list, param);
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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "FunctionService");
                return true;
            }
        }

        private bool DownloadRTRoleUser(ACABatchParam param)
        {
            _entityName = typeof(RoleUserInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<RoleUserInfo> list = new List<RoleUserInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [RoleUser]");
            list = _bpmServerSG.DownloadRoleUser(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [RoleUser]", "Total rows = " + list.Count.ToString());
                bool result = _bpmBranchService.UpdateRoleUser(list, param);
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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "RoleUser");
                return true;
            }
        }

        private bool DownloadRTRoleFunction(ACABatchParam param)
        {
            _entityName = typeof(RoleFunctionInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<RoleFunctionInfo> list = new List<RoleFunctionInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [RoleFunction]");
            list = _bpmServerSG.DownloadRoleFunction(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [RoleFunction]", "Total rows = " + list.Count.ToString());
                bool result = _bpmBranchService.UpdateRoleFunction(list, param);
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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "RoleFunction");
                return true;
            }
        }

        private bool DownloadUnlockRemark(ACABatchParam param)
        {
            _entityName = typeof(UnlockRemarkInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<UnlockRemarkInfo> list = new List<UnlockRemarkInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [UnlockRemark]");
            list = _bpmServerSG.DownloadUnlockRemark(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [UnlockRemark]", "Total rows = " + list.Count.ToString());
                bool result = _bpmBranchService.UpdateUnlockRemark(list, param);
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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "UnlockRemark");
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
