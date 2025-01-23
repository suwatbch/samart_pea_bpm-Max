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
using System.Threading;
using PEA.BPM.Integration.BPMIntegration.Interface.Constants;

namespace PEA.BPM.Integration.BPMIntegration.BranchServerBatch
{
    public class DL021_MASTER2_JOB : IJob
    {
        private const string DL_NAME_ID = "DL021";
        private const string DL_FULL_NAME = "DL021_MASTER";
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

        public DL021_MASTER2_JOB()
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
                    case "BusinessPartner":
                        {
                            param.FileName = "BusinessPartner";
                            ret = DownloadBusinessPartner(param);
                        }
                        break;
                    case "ContractAccount":
                        {
                            param.FileName = "ContractAccount";
                            ret = DownloadContractAccount(param);
                        }
                        break;
                    case "OldCaMapping":
                        {
                            param.FileName = "OldCaMapping";
                            ret = DownloadOldCaMapping(param);
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

        private bool DownloadBusinessPartner(ACABatchParam param)
        {
            _entityName = typeof(BusinessPartnerInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<BusinessPartnerInfo> businessPartners = new List<BusinessPartnerInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [BusinessPartner]");
            businessPartners = _bpmServerSG.GetUpdateBusinessPartner(_branchId, _lastModifiedDt, _serverDate);

            if (businessPartners.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [BusinessPartner]", "Total rows = " + businessPartners.Count.ToString());
                bool result = _bpmBranchService.UpdateBusinessPartner(businessPartners, param);
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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "BusinessPartner");
                return true;
            }
            return true;
        }

        private bool DownloadContractAccount(ACABatchParam param)
        {
            _entityName = typeof(ContractAccountInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<ContractAccountInfo> contractAccs = new List<ContractAccountInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [ContractAccount]");
            contractAccs = _bpmServerSG.GetUpdateContractAccount(_branchId, _lastModifiedDt, _serverDate);

            if (contractAccs.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [ContractAccount]", "Total rows = " + contractAccs.Count.ToString());
                bool result = _bpmBranchService.UpdateContractAccount(contractAccs, param);

                if (result)
                {
                    Scheduler sch = new Scheduler();
                    sch.EntityName = _entityName;
                    sch.LastUpdateDt = _serverDate;

                    //sch.LastUpdateDt = contractAccs[contractAccs.Count - 1].ModifiedDt.Value;

                    sch.JobType = _jobType;
                    result = ACABatchScheduler.UpdateScheduler(sch);
                    return true;
                }
                else
                    return false;
            }
            else
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "ContractAccount");
                return true;
            }
        }


        private bool DownloadOldCaMapping(ACABatchParam param)
        {
            _entityName = typeof(OldCaMappingInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<OldCaMappingInfo> oldCaMappingInfo = new List<OldCaMappingInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [OldCaMapping]");
            oldCaMappingInfo = _bpmServerSG.GetUpdateOldCaMapping(_branchId, _lastModifiedDt, _serverDate);

            if (oldCaMappingInfo.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [OldCaMapping]", "Total rows = " + oldCaMappingInfo.Count.ToString());
                bool result = _bpmBranchService.UpdateOldCaMapping(oldCaMappingInfo, param);
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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "OldCaMapping");
                return true;
            }
            return true;
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
