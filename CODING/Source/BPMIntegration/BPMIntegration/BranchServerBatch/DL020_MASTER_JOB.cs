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
    public class DL020_MASTER_JOB : IJob
    {
        private const string DL_NAME_ID = "DL020";
        private const string DL_FULL_NAME = "DL020_MASTER";
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

        public DL020_MASTER_JOB()
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
                    case "Calendar":
                        {
                            param.FileName = "Calendar";
                            ret = DownloadCalendar(param);
                        }
                        break;
                    case "NonWorkingDay":
                        {
                            param.FileName = "NonWorkingDay";
                            ret = DownloadNonWorkingDay(param);
                        }
                        break;                  
                    case "Branch":
                        {
                            param.FileName = "Branch";
                            ret = DownloadBranch(param);
                        }
                        break;
                    case "MRU":
                        {
                            param.FileName = "MRU";
                            ret = DownloadMRU(param);
                        }
                        break;
                    case "MRUPlan":
                        {
                            param.FileName = "MRUPlan";
                            ret = DownloadMRUPlan(param);
                        }
                        break;                   
                    case "Employee":
                        {
                            param.FileName = "Employee";
                            ret = DownloadEmployee(param);
                        }
                        break;
                    case "AgencyAsset":
                        {
                            param.FileName = "AgencyAsset";
                            ret = DownloadAgencyAsset(param);
                        }
                        break;
                    case "Bank":
                        {
                            param.FileName = "Bank";
                            ret = DownloadBank(param);
                        }
                        break;
                    case "BankAccount":
                        {
                            param.FileName = "BankAccount";
                            ret = DownloadBankAccount(param);
                        }
                        break;
                    case "MainSub":
                        {
                            param.FileName = "MainSub";
                            ret = DownloadMainSub(param);
                        }
                        break;
                    case "PaymentSequence":
                        {
                            param.FileName = "PaymentSequence";
                            ret = DownloadPaymentSequence(param);
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

        private bool DownloadBranch(ACABatchParam param)
        {
            _entityName = typeof(BranchInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<BranchInfo> branches = new List<BranchInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [Branch]");
            branches = _bpmServerSG.GetUpdateBranch(_branchId, _lastModifiedDt, _serverDate);

            if (branches.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [Branch]", "Total rows = " + branches.Count.ToString());
                bool result = _bpmBranchService.UpdateBranch(branches, param);

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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "Branch");
                return true;
            }
        }

        private bool DownloadMRU(ACABatchParam param)
        {
            _entityName = typeof(MRUInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<MRUInfo> mrus = new List<MRUInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [MRU]");
            mrus = _bpmServerSG.GetUpdateMRU(_branchId, _lastModifiedDt, _serverDate);

            if (mrus.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [MRU]", "Total rows = " + mrus.Count.ToString());
                bool result = _bpmBranchService.UpdateMRU(mrus, param);

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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "MRU");
                return true;
            }
        }

        private bool DownloadMRUPlan(ACABatchParam param)
        {
            _entityName = typeof(MRUPlanInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<MRUPlanInfo> mruPlans = new List<MRUPlanInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [MRUPlan]");
            mruPlans = _bpmServerSG.GetUpdateMRUPlan(_branchId, _lastModifiedDt, _serverDate);

            if (mruPlans.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [MRUPlan]", "Total rows = " + mruPlans.Count.ToString());
                bool result = _bpmBranchService.UpdateMRUPlan(mruPlans, param);

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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "MRUPlan");
                return true;
            }
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

        private bool DownloadEmployee(ACABatchParam param)
        {
            _entityName = typeof(EmployeeInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<EmployeeInfo> emps = new List<EmployeeInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [Employee]");
            emps = _bpmServerSG.GetUpdateEmployee(_branchId, _lastModifiedDt, _serverDate);

            if (emps.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [Employee]", "Total rows = " + emps.Count.ToString());
                bool result = _bpmBranchService.UpdateEmployee(emps, param);

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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "Employee");
                return true;
            }
        }

        private bool DownloadAgencyAsset(ACABatchParam param)
        {
            _entityName = typeof(AgencyAssetInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<AgencyAssetInfo> agencyAssets = new List<AgencyAssetInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [AgencyAsset]");
            agencyAssets = _bpmServerSG.GetUpdateAgencyAsset(_branchId, _lastModifiedDt, _serverDate);

            if (agencyAssets.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [AgencyAsset]", "Total rows = " + agencyAssets.Count.ToString());
                bool result = _bpmBranchService.UpdateAgencyAsset(agencyAssets, param);

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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "AgencyAsset");
                return true;
            }
        }

        private bool DownloadBank(ACABatchParam param)
        {
            _entityName = typeof(BankInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<BankInfo> banks = new List<BankInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [Bank]");
            banks = _bpmServerSG.GetUpdateBank(_branchId, _lastModifiedDt, _serverDate);

            if (banks.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [Bank]", "Total rows = " + banks.Count.ToString());
                bool result = _bpmBranchService.UpdateBank(banks, param);

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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "Bank");
                return true;
            }
        }

        private bool DownloadBankAccount(ACABatchParam param)
        {
            _entityName = typeof(BankAccountInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<BankAccountInfo> bankAccs = new List<BankAccountInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [BankAccount]");
            bankAccs = _bpmServerSG.GetUpdateBankAccount(_branchId, _lastModifiedDt, _serverDate);

            if (bankAccs.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [BankAccount]", "Total rows = " + bankAccs.Count.ToString());
                bool result = _bpmBranchService.UpdateBankAccount(bankAccs, param);

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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "BankAccount");
                return true;
            }
        }

        private bool DownloadMainSub(ACABatchParam param)
        {
            _entityName = typeof(MainSubInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<MainSubInfo> mainSubs = new List<MainSubInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [MainSub]");
            mainSubs = _bpmServerSG.GetUpdateDebtType(_branchId, _lastModifiedDt, _serverDate);

            if (mainSubs.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [MainSub]", "Total rows = " + mainSubs.Count.ToString());
                bool result = _bpmBranchService.UpdateDebtType(mainSubs, param);

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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "MainSub");
                return true;
            }
        }

        private bool DownloadCalendar(ACABatchParam param)
        {
            _entityName = typeof(CalendarInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<CalendarInfo> list = new List<CalendarInfo>();

            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [Calendar]");

            list = _bpmServerSG.GetUpdateCalendar(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [Calendar]", "Total rows = " + list.Count.ToString());
                bool result = _bpmBranchService.UpdateCalendar(list, param);

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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "Calendar");
                return true;
            }
        }

        private bool DownloadNonWorkingDay(ACABatchParam param)
        {
            _entityName = typeof(NonWorkingDayInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);

            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [NonWorkingDay]");

            List<NonWorkingDayInfo> list = _bpmServerSG.GetUpdateNonWorkingDay(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [NonWorkingDay]", "Total rows = " + list.Count.ToString());

                bool result = _bpmBranchService.UpdateNonworkingDay(list, param);
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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "NonWorkingDay");
                return true;
            }

        }

        private bool DownloadPaymentSequence(ACABatchParam param)
        {
            _entityName = typeof(PaymentSequenceInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<PaymentSequenceInfo> list = new List<PaymentSequenceInfo>();
            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [PaymentSequence]");
            list = _bpmServerSG.GetUpdatePaymentSequence(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [PaymentSequence]", "Total rows = " + list.Count.ToString());
                bool result = _bpmBranchService.UpdatePaymentSequence(list, param);

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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "PaymentSequence");
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
