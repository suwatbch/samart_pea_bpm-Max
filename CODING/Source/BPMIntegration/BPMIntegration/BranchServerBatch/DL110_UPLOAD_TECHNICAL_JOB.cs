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
    public class DL110_UPLOAD_TECHNICAL_JOB : IJob
    {
        private const string DL_NAME_ID = "DL110";
        private const string DL_FULL_NAME = "DL110_UPLOAD_TECHNICAL";
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

        public DL110_UPLOAD_TECHNICAL_JOB()
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
                    case "UnlockLog":
                        {
                            param.FileName = "UnlockLog";
                            ret = UploadUnlockLog(param);
                        }
                        break;
                    case "User":
                        {
                            param.FileName = "User";
                            ret = UploadUser(param);
                        }
                        break;
                    case "RTRoleUser":
                        {
                            param.FileName = "RTRoleUser";
                            ret = UploadRoleUser(param);
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

        private bool UploadUnlockLog(ACABatchParam param)
        {
            _entityName = typeof(UnlockLogInfo).Name;
            //_serverDate = _bpmServerSG.GetServerTime();
            string _infName = "UnlockLog";

            try
            {
                List<UnlockLogInfo> list = new List<UnlockLogInfo>();
                list = _bpmBranchService.GetToUploadUnlockLog(_serverDate);
                int listCount = list.Count;
                int rows = 0;

                if (listCount > 0)
                {
                    _logger.HandleLog(param.BatchKey, "0", _infName, _entityName, _localDate, _serverDate, listCount);

                    rows = _bpmServerSG.UploadUnlockLog(list, _branchId);

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
                int syncRows = _bpmBranchService.SetSyncUnlockLog(_serverDate);
                return _logger.HandleLog(param.BatchKey, "3", _infName, _entityName, _localDate, _serverDate, syncRows, ex.ToString());
            }
        }

        private bool UploadUser(ACABatchParam param)
        {
            _entityName = typeof(UserInfo).Name;
            //_serverDate = _bpmServerSG.GetServerTime();
            string _infName = "User";

            try
            {
                List<UserInfo> list = new List<UserInfo>();
                list = _bpmBranchService.GetToUploadUser(_serverDate);
                int listCount = list.Count;
                int rows = 0;

                if (listCount > 0)
                {
                    _logger.HandleLog(param.BatchKey, "0", _infName, _entityName, _localDate, _serverDate, listCount);

                    rows = _bpmServerSG.UploadUser(list, _branchId);

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
                int syncRows = _bpmBranchService.SetSyncUser(_serverDate);
                return _logger.HandleLog(param.BatchKey, "3", _infName, _entityName, _localDate, _serverDate, syncRows, ex.ToString());
            }
        }

        private bool UploadRoleUser(ACABatchParam param)
        {
            _entityName = typeof(RoleUserInfo).Name;
            //_serverDate = _bpmServerSG.GetServerTime();
            string _infName = "RoleUser";

            try
            {
                List<RoleUserInfo> list = new List<RoleUserInfo>();
                list = _bpmBranchService.GetToUploadRoleUser(_serverDate);
                int listCount = list.Count;
                int rows = 0;

                if (listCount > 0)
                {
                    _logger.HandleLog(param.BatchKey, "0", _infName, _entityName, _localDate, _serverDate, listCount);

                    rows = _bpmServerSG.UploadRoleUser(list, _branchId);

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
                int syncRows = _bpmBranchService.SetSyncRoleUser(_serverDate);
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
