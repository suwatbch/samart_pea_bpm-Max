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
    /// <summary>
    /// NonWorkingDay,AccountClass,ContractType,MeterSizeType,PaymentMethod,TaxCode,UnitType (sequencially)
    /// </summary>
    public class DL010_ISOLATE_JOB : IJob
    {
        private const string DL_NAME_ID = "DL010";
        private const string DL_FULL_NAME = "DL010_ISOLATE";
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

        public DL010_ISOLATE_JOB()
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
                    case "AccountClass":
                        {
                            param.FileName = "AccountClass";
                            ret = DownloadAccountClass(param);
                        }
                        break;
                    case "ContractType":
                        {
                            param.FileName = "ContractType";
                            ret = DownloadContractType(param);
                        }
                        break;
                    case "MeterSizeType":
                        {
                            param.FileName = "MeterSize";
                            ret = DownloadMeterSizeType(param);
                        }
                        break;
                    case "PaymentMethod":
                        {
                            param.FileName = "PaymentMethod";
                            ret = DownloadPaymentMethod(param);
                        }
                        break;
                    case "TaxCode":
                        {
                            param.FileName = "TaxCode";
                            ret = DownloadTaxCode(param);
                        }
                        break;
                    case "UnitType":
                        {
                            param.FileName = "UnitType";
                            ret = DownloadUnitType(param);
                        }
                        break;
                    case "BusinessPartnerType":
                        {
                            param.FileName = "BusinessPartnerType";
                            ret = DownloadBusinessPartnerType(param);
                        }
                        break;                  
                    case "PaymentType":
                        {
                            param.FileName = "PaymentType";
                            ret = DownloadPaymentType(param);
                        }
                        break;                      
                    case "InterestKey":
                        {
                            param.FileName = "InterestKey";
                            ret = DownloadInterestKey(param);
                        }
                        break;
                    case "AgencyBillCollectionStatus":
                        {
                            param.FileName = "AgencyBillCollectionStatus";
                            ret = DownloadAgencyBillCollectionStatus(param);
                            //ret = true;
                        }
                        break;
                    case "AgencyBillOutStatus":
                        {
                            param.FileName = "AgencyBillOutStatus";
                            ret = DownloadAgencyBillOutStatus(param);
                            //ret = true;
                        }
                        break;
                    case "AgencyCommissionRate":
                        {
                            param.FileName = "AgencyCommissionRate";
                            ret = DownloadAgencyCommissionRate(param);
                            //ret = true;
                        }
                        break;
                    case "AgencyCollectArea":
                        {
                            param.FileName = "AgencyCollectArea";
                            ret = DownloadAgencyCollectArea(param);
                        }
                        break;
                    case "DisconnectStatus":
                        {
                            param.FileName = "DisconnectionStatus";
                            ret = DownloadDisconnectionStatus(param);
                        }
                        break;
                    case "BillBookStatus":
                        {
                            param.FileName = "BillBookStatus";
                            ret = DownloadBillBookStatus(param);
                        }
                        break;
                    case "CashierMoneyFlowType":
                        {
                            param.FileName = "CashierMoneyFlowType";
                            ret = DownloadCashierMoneyFlowType(param);
                        }
                        break;
                    case "OwnBank":
                        {
                            param.FileName = "OwnBank";
                            ret = DownloadOwnBank(param);
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

        private bool DownloadAccountClass(ACABatchParam param)
        {
            _entityName = typeof(AccountClassInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<AccountClassInfo> accoutClass = new List<AccountClassInfo>();

            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [AccountClass]");
            
            accoutClass = _bpmServerSG.GetUpdateAccountClass(_branchId, _lastModifiedDt, _serverDate);
            if (accoutClass.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [AccountClass]", "Total rows = " + accoutClass.Count.ToString());
                bool result = _bpmBranchService.UpdateAccountClass(accoutClass, param);

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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "AccountClass");
                return true;
            }
        }

        private bool DownloadContractType(ACABatchParam param)
        {
            _entityName = typeof(ContractTypeInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<ContractTypeInfo> contractType = new List<ContractTypeInfo>();

            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [ContractType]");

            contractType = _bpmServerSG.GetUpdateContractType(_branchId, _lastModifiedDt, _serverDate);
            if (contractType.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [ContractType]", "Total rows = " + contractType.Count.ToString());
                bool result = _bpmBranchService.UpdateContractType(contractType, param);

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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "ContractType");
                return true;
            }
        }

        private bool DownloadMeterSizeType(ACABatchParam param)
        {
            _entityName = typeof(MeterSizeInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<MeterSizeInfo> meterSize = new List<MeterSizeInfo>();

            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [MeterSize]");

            meterSize = _bpmServerSG.GetUpdateMeterSize(_branchId, _lastModifiedDt, _serverDate);
            if (meterSize.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [MeterSize]", "Total rows = " + meterSize.Count.ToString());
                bool result = _bpmBranchService.UpdateMeterSize(meterSize, param);

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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "MeterSize");
                return true;
            }
        }

        private bool DownloadPaymentMethod(ACABatchParam param)
        {
            _entityName = typeof(PaymentMethodInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<PaymentMethodInfo> paymentMethod = new List<PaymentMethodInfo>();

            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [PaymentMethod]");

            paymentMethod = _bpmServerSG.GetUpdatePaymentMethod(_branchId, _lastModifiedDt, _serverDate);
            if (paymentMethod.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [PaymentMethod]", "Total rows = " + paymentMethod.Count.ToString());
                bool result = _bpmBranchService.UpdatePaymentMethod(paymentMethod, param);

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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "PaymentMethod");
                return true;
            }
        }

        private bool DownloadTaxCode(ACABatchParam param)
        {
            _entityName = typeof(TaxCodeInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<TaxCodeInfo> taxCode = new List<TaxCodeInfo>();

            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [TaxCode]");

            taxCode = _bpmServerSG.GetUpdateTaxCode(_branchId, _lastModifiedDt, _serverDate);
            if (taxCode.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [TaxCode]", "Total rows = " + taxCode.Count.ToString());
                bool result = _bpmBranchService.UpdateTaxCode(taxCode, param);

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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "TaxCode");
                return true;
            }
        }

        private bool DownloadUnitType(ACABatchParam param)
        {
            _entityName = typeof(UnitTypeInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<UnitTypeInfo> unitType = new List<UnitTypeInfo>();

            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [UnitType]");

            unitType = _bpmServerSG.GetUpdateUnitType(_branchId, _lastModifiedDt, _serverDate);
            if (unitType.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [UnitType]", "Total rows = " + unitType.Count.ToString());
                bool result = _bpmBranchService.UpdateUnitType(unitType, param);

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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "AccountClass");
                return true;
            }
        }

        private bool DownloadBusinessPartnerType(ACABatchParam param)
        {
            _entityName = typeof(BusinessPartnerTypeInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<BusinessPartnerTypeInfo> list = new List<BusinessPartnerTypeInfo>();

            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [BusinessPartnerType]");

            list = _bpmServerSG.GetUpdateBusinessPartnerType(_branchId, _lastModifiedDt, _serverDate);
            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [BusinessPartnerType]", "Total rows = " + list.Count.ToString());
                bool result = _bpmBranchService.UpdateBusinessPartnerType(list, param);

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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "BusinessPartnerType");
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

            if(list.Count>0)
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

        private bool DownloadPaymentType(ACABatchParam param)
        {
            _entityName = typeof(PaymentTypeInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<PaymentTypeInfo> list = new List<PaymentTypeInfo>();

            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [PaymentType]");

            list = _bpmServerSG.GetUpdatePaymentType(_branchId, _lastModifiedDt, _serverDate);
            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [PaymentType]", "Total rows = " + list.Count.ToString());
                bool result = _bpmBranchService.UpdatePaymentType(list, param);

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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "PaymentType");
                return true;
            }
        }

        private bool DownloadInterestKey(ACABatchParam param)
        {
            _entityName = typeof(InterestKeyInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<InterestKeyInfo> list = new List<InterestKeyInfo>();

            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [InterestKey]");

            list = _bpmServerSG.GetUpdateInterestKey(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [InterestKey]", "Total rows = " + list.Count.ToString());
                bool result = _bpmBranchService.UpdateInterestKey(list, param);

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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "InterestKey");
                return true;
            }
        }

        private bool DownloadAgencyBillCollectionStatus(ACABatchParam param)
        {
            _entityName = typeof(AgencyBillCollectionStatusInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<AgencyBillCollectionStatusInfo> list = new List<AgencyBillCollectionStatusInfo>();

            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [AgencyBillCollectionStatus]");

            list = _bpmServerSG.GetUpdateAgencyBillCollectionStatus(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [AgencyBillCollectionStatus]", "Total rows = " + list.Count.ToString());
                bool result = _bpmBranchService.UpdateAgencyBillCollectionStatus(list, param);

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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "AgencyBillCollectionStatus");
                return true;
            }
        }

        private bool DownloadAgencyBillOutStatus(ACABatchParam param)
        {
            _entityName = typeof(AgencyBillOutStatusInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<AgencyBillOutStatusInfo> list = new List<AgencyBillOutStatusInfo>();

            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [AgencyBillOutStatus]");

            list = _bpmServerSG.GetUpdateAgencyBillOutStatus(_branchId, _lastModifiedDt, _serverDate);
            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [AgencyBillOutStatus]", "Total rows = " + list.Count.ToString());
                bool result = _bpmBranchService.UpdateAgencyBillOutStatus(list, param);

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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "AgencyBillOutStatus");
                return true;
            }
        }

        private bool DownloadAgencyCommissionRate(ACABatchParam param)
        {
            _entityName = typeof(AgencyCommissionRateInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<AgencyCommissionRateInfo> list = new List<AgencyCommissionRateInfo>();

            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [AgencyCommissionRate]");

            list = _bpmServerSG.GetUpdateAgencyCommissionRate(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [AgencyCommissionRate]", "Total rows = " + list.Count.ToString());
                bool result = _bpmBranchService.UpdateAgencyCommissionRate(list, param);

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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "AgencyCommissionRate");
                return true;
            }
        }

        private bool DownloadBillBookStatus(ACABatchParam param)
        {
            _entityName = typeof(BillBookStatusInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<BillBookStatusInfo> list = new List<BillBookStatusInfo>();

            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [BillBookStatus]");

            list = _bpmServerSG.GetUpdateBillBookStatus(_branchId, _lastModifiedDt, _serverDate);

            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [BillBookStatus]", "Total rows = " + list.Count.ToString());
                bool result = _bpmBranchService.UpdateBillBookStatus(list, param);

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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "BillBookStatus");
                return true;
            }
        }

        private bool DownloadAgencyCollectArea(ACABatchParam param)
        {
            _entityName = typeof(AgencyCollectAreaInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<AgencyCollectAreaInfo> list = new List<AgencyCollectAreaInfo>();

            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [AgencyCollectArea]");

            list = _bpmServerSG.GetUpdateAgencyCollectArea(_branchId, _lastModifiedDt, _serverDate);
            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [AgencyCollectArea]", "Total rows = " + list.Count.ToString());
                bool result = _bpmBranchService.UpdateAgencyCollectArea(list, param);

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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "AgencyCollectArea");
                return true;
            }
        }

        private bool DownloadDisconnectionStatus(ACABatchParam param)
        {
            _entityName = typeof(DisconnectionStatusInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<DisconnectionStatusInfo> list = new List<DisconnectionStatusInfo>();

            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [DisconnectionStatus]");

            list = _bpmServerSG.GetUpdateDisconnectionStatus(_branchId, _lastModifiedDt, _serverDate);
            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [DisconnectionStatus]", "Total rows = " + list.Count.ToString());
                bool result = _bpmBranchService.UpdateDisconnectionStatus(list, param);

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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "DisconnectionStatus");
                return true;
            }
        }

        private bool DownloadCashierMoneyFlowType(ACABatchParam param)
        {
            _entityName = typeof(CashierMoneyFlowTypeInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<CashierMoneyFlowTypeInfo> list = new List<CashierMoneyFlowTypeInfo>();

            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [CashierMoneyFlowType]");

            list = _bpmServerSG.GetUpdateCashierMoneyFlowType(_branchId, _lastModifiedDt, _serverDate);
            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [CashierMoneyFlowType]", "Total rows = " + list.Count.ToString());
                bool result = _bpmBranchService.UpdateCashierMoneyFlowType(list, param);

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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "CashierMoneyFlowType");
                return true;
            }
        }

        private bool DownloadOwnBank(ACABatchParam param)
        {
            _entityName = typeof(OwnBankInfo).Name;
            _lastModifiedDt = ACABatchScheduler.GetLastModifiedDate(_entityName);
            List<OwnBankInfo> list = new List<OwnBankInfo>();

            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Started [OwnBank]");

            list = _bpmServerSG.GetUpdateOwnBank(_branchId, _lastModifiedDt, _serverDate);
            if (list.Count > 0)
            {
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Download Completed [OwnBank]", "Total rows = " + list.Count.ToString());
                bool result = _bpmBranchService.UpdateOwnBank(list, param);

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
                _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already up-to-date.", "OwnBank");
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
                    //need manual select, should be auto selected?
                    //31/01/08 : Tong --> suggest auto selected krub pom!
                    //if (failHt.ContainsKey(tb))
                    //{
                    //    _logger.WriteLog(batchKey, ACABatchLogger.ErrorType.FileValidation, "ไม่พบข้อมูลที่ต้องการนำเข้า เพื่อแก้ไขข้อผิดพลาดก่อนหน้านี้ กรุณาตรวจสอบที่: ", tb);
                    //    continue;
                    //}

                    if (!ImportData(batchKey, tb, true)) //always overwrite
                    {
                        if (!failHt.ContainsKey(tb))
                            HandleError(batchKey, _activeFileKey, tb);
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
