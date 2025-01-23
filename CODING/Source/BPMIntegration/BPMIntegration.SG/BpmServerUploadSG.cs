using System;
using System.Collections.Generic;
using System.Text;
//using Microsoft.Web.Services3.Security.Tokens;

using PEA.BPM.Integration.BPMIntegration.Interface;
using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;
using PEA.BPM.Integration.BPMIntegration.Interface.Services;
using PEA.BPM.Architecture.CommonUtilities;
using System.IO;
using System.Configuration;
using PEA.BPM.Integration.BPMIntegration.Interface.Constants;

namespace PEA.BPM.Integration.BPMIntegration.SG
{
    public class BpmServerUploadSG : IBpmServerService
    {
        MasterULWS.MasterIntegrationWebService _mtWS;
        BlanULWS.BLANIntegrationWebService _blWS;
        ARULWS.ARWebService _arWS;
        AgencyULWS.AgencyIntegrationWebService _agWS;
        UtilULWS.UtilitiesIntegrationWebService _suWS;
        CashULWS.CashManagementWebService _cmWS;
        EPayULWS.EPaymentWebService _epWS;
        ExportTabULWS.ExportTableIntegrationWebService _exWS;

        private const string WS_AR_ADDR_UL = "WS_AR_ADDR_UL";
        private const string WS_AGENCY_ADDR_UL = "WS_AGENCY_ADDR_UL";
        private const string WS_MASTER_ADDR_UL = "WS_MASTER_ADDR_UL";
        private const string WS_BLAN_ADDR_UL = "WS_BLAN_ADDR_UL";
        private const string WS_UTIL_ADDR_UL = "WS_UTIL_ADDR_UL";
        private const string WS_CASH_ADDR_UL = "WS_CASH_ADDR_UL";
        private const string WS_EPAY_ADDR_UL = "WS_EPAY_ADDR_UL";
        private const string WS_EXPORTTAB_ADDR_UL = "WS_EXPORTTAB_ADDR_UL";

        public BpmServerUploadSG()
        {
            try
            {
                _arWS = new ARULWS.ARWebService();
                ARULWS.AuthenInfo arWSInfo = new ARULWS.AuthenInfo();
                arWSInfo.AuthToken = WebServiceIntegrationToken.Value;
                _arWS.AuthenInfoValue = arWSInfo;
                _arWS.Timeout = 7200000; //1000*60*60*2
                _arWS.Url = ConfigurationManager.AppSettings[WS_AR_ADDR_UL];

                _agWS = new AgencyULWS.AgencyIntegrationWebService();
                AgencyULWS.AuthenInfo agencyWSInfo = new AgencyULWS.AuthenInfo();
                agencyWSInfo.AuthToken = WebServiceIntegrationToken.Value;
                _agWS.AuthenInfoValue = agencyWSInfo;
                _agWS.Timeout = 7200000; //1000*60*60*2
                _agWS.Url = ConfigurationManager.AppSettings[WS_AGENCY_ADDR_UL];

                _mtWS = new MasterULWS.MasterIntegrationWebService();
                MasterULWS.AuthenInfo masterWSInfo = new MasterULWS.AuthenInfo();
                masterWSInfo.AuthToken = WebServiceIntegrationToken.Value;
                _mtWS.AuthenInfoValue = masterWSInfo;
                _mtWS.Timeout = 7200000; //1000*60*60*2 
                _mtWS.Url = ConfigurationManager.AppSettings[WS_MASTER_ADDR_UL];

                _blWS = new BlanULWS.BLANIntegrationWebService();
                BlanULWS.AuthenInfo blanWSInfo = new BlanULWS.AuthenInfo();
                blanWSInfo.AuthToken = WebServiceIntegrationToken.Value;
                _blWS.AuthenInfoValue = blanWSInfo;
                _blWS.Timeout = 7200000; //1000*60*60*2
                _blWS.Url = ConfigurationManager.AppSettings[WS_BLAN_ADDR_UL];

                _suWS = new UtilULWS.UtilitiesIntegrationWebService();
                UtilULWS.AuthenInfo suWSInfo = new UtilULWS.AuthenInfo();
                suWSInfo.AuthToken = WebServiceIntegrationToken.Value;
                _suWS.AuthenInfoValue = suWSInfo;
                _suWS.Timeout = 7200000; //1000*60*60*2 
                _suWS.Url = ConfigurationManager.AppSettings[WS_UTIL_ADDR_UL];

                _cmWS = new CashULWS.CashManagementWebService();
                CashULWS.AuthenInfo cuWSInfo = new CashULWS.AuthenInfo();
                cuWSInfo.AuthToken = WebServiceIntegrationToken.Value;
                _cmWS.AuthenInfoValue = cuWSInfo;
                _cmWS.Timeout = 7200000; //1000*60*60*2 
                _cmWS.Url = ConfigurationManager.AppSettings[WS_CASH_ADDR_UL];

                _epWS = new EPayULWS.EPaymentWebService();
                EPayULWS.AuthenInfo epWSInfo = new EPayULWS.AuthenInfo();
                epWSInfo.AuthToken = WebServiceIntegrationToken.Value;
                _epWS.AuthenInfoValue = epWSInfo;
                _epWS.Timeout = 7200000; //1000*60*60*2 
                _epWS.Url = ConfigurationManager.AppSettings[WS_EPAY_ADDR_UL];

                _exWS = new ExportTabULWS.ExportTableIntegrationWebService();
                ExportTabULWS.AuthenInfo exWSInfo = new ExportTabULWS.AuthenInfo();
                exWSInfo.AuthToken = WebServiceIntegrationToken.Value;
                _exWS.AuthenInfoValue = exWSInfo;
                _exWS.Timeout = 7200000; //1000*60*60*2 
                _exWS.Url = ConfigurationManager.AppSettings[WS_EXPORTTAB_ADDR_UL];
            }
            catch (Exception ex)
            { }
        }

        #region Download the server time

        public DateTime GetServerTime()
        {
            return _suWS.GetServerTime();
        }

        #endregion

        #region Upload DL51 cash data from branch to BPM Server

        public int UploadCashierWorkStatus(List<CashierWorkStatusInfo> list, string branchId)
        {
            return _cmWS.UploadCashierWorkStatus(ServiceHelper.CompressData<List<CashierWorkStatusInfo>>(list), branchId);
        }

        public int UploadCashierMoneyTransfer(List<CashierMoneyTransferInfo> list, string branchId)
        {
            return _cmWS.UploadCashierMoneyTransfer(ServiceHelper.CompressData<List<CashierMoneyTransferInfo>>(list), branchId);
        }

        public int UploadCashierMoneyFlow(List<CashierMoneyFlowInfo> list, string branchId)
        {
            return _cmWS.UploadCashierMoneyFlow(ServiceHelper.CompressData<List<CashierMoneyFlowInfo>>(list), branchId);
        }

        public int UploadCashierMoneyFlowItem(List<CashierMoneyFlowItemInfo> list, string branchId)
        {
            return _cmWS.UploadCashierMoneyFlowItem(ServiceHelper.CompressData<List<CashierMoneyFlowItemInfo>>(list), branchId);
        }

        #endregion

        #region Upload DL71 master data from branch to BPM Server
            
        public int UploadMRUPlan(List<MRUPlanInfo> list, string branchId)
        {
            return _mtWS.UploadMRUPlan(ServiceHelper.CompressData<List<MRUPlanInfo>>(list), branchId);
        }

        #endregion

        #region Upload DL80 AR data from branch to BPM Server

        public int UploadAP(List<APInfo> ars, string branchId)
        {
            return _arWS.UploadAP(ServiceHelper.CompressData<List<APInfo>>(ars), branchId);
        }

        public int UploadAPChequePayment(List<APChequePaymentInfo> ars, string branchId)
        {
            return _arWS.UploadAPChequePayment(ServiceHelper.CompressData<List<APChequePaymentInfo>>(ars), branchId);
        }

        #endregion

        #region Upload DL90 payment data from branch to BPM Server
        
        public int UploadAR(List<AR> ars, string branchId)
        {
            return _arWS.UploadAR(ServiceHelper.CompressData<List<AR>>(ars), branchId);
        }

        public int UploadARPayment(List<ARPayment> arPayments, string branchId)
        {
            return _arWS.UploadARPayment(ServiceHelper.CompressData<List<ARPayment>>(arPayments), branchId);
        }

        public int UploadPayment(List<Payment> payments, string branchId)
        {
            return _arWS.UploadPayment(ServiceHelper.CompressData<List<Payment>>(payments), branchId);
        }

        public int UploadARPaymentType(List<ARPaymentType> arPaymentTypes, string branchId)
        {
            return _arWS.UploadARPaymentType(ServiceHelper.CompressData<List<ARPaymentType>>(arPaymentTypes), branchId);
        }

        public int UploadRTARPaymentTypeARPayment(List<RTARPaymentTypeARPayment> rts, string branchId)
        {
            return _arWS.UploadRTARPaymentTypeARPayment(ServiceHelper.CompressData<List<RTARPaymentTypeARPayment>>(rts), branchId);
        }

        public int UploadReceipt(List<Receipt> receipts, string branchId)
        {
            return _arWS.UploadReceipt(ServiceHelper.CompressData<List<Receipt>>(receipts), branchId);
        }

        public int UploadReceiptItem(List<ReceiptItem> receiptItems, string branchId)
        {
            return _arWS.UploadReceiptItem(ServiceHelper.CompressData<List<ReceiptItem>>(receiptItems), branchId);
        }

        public int UploadPaymentLog(List<PaymentLogInfo> list, string branchId)
        {
            return _arWS.UploadPaymentLog(ServiceHelper.CompressData<List<PaymentLogInfo>>(list), branchId);
        }

        #endregion        

        #region Upload DL100 agency data from branch to BPM Server

        public int UploadBillBook(List<BillBookInfo> list, string branchId)
        {
            return _agWS.UploadBillBook(ServiceHelper.CompressData<List<BillBookInfo>>(list), branchId);
        }

        public int UploadBillStatusInfo(List<BillStatusInfoInfo> list, string branchId)
        {
            return _agWS.UploadBillStatusInfo(ServiceHelper.CompressData<List<BillStatusInfoInfo>>(list), branchId);
        }

        public int UploadBillBookDetail(List<BillBookDetailInfo> list, string branchId)
        {
            return _agWS.UploadBillBookDetail(ServiceHelper.CompressData<List<BillBookDetailInfo>>(list), branchId);
        }

        public int UploadBillBookInputItem(List<BillBookInputItemInfo> list, string branchId)
        {
            return _agWS.UploadBillBookInputItem(ServiceHelper.CompressData<List<BillBookInputItemInfo>>(list), branchId);
        }

        public int UploadBillBookInputSet(List<BillBookInputSetInfo> list, string branchId)
        {
            return _agWS.UploadBillBookInputSet(ServiceHelper.CompressData<List<BillBookInputSetInfo>>(list), branchId);
        }

        public int UploadAgencyCommission(List<AgencyCommissionInfo> list, string branchId)
        {
            return _agWS.UploadAgencyCommission(ServiceHelper.CompressData<List<AgencyCommissionInfo>>(list), branchId);
        }

        public int UploadRTAgencyContractMru(List<RTAgencyContractMruInfo> list, string branchId)
        {
            return _agWS.UploadRTAgencyContractMru(ServiceHelper.CompressData<List<RTAgencyContractMruInfo>>(list), branchId);
        }

        public int UploadRTAgencyCommissionBillBook(List<RTAgencyCommissionBillBookInfo> list, string branchId)
        {
            return _agWS.UploadRTAgencyCommissionBillBook(ServiceHelper.CompressData<List<RTAgencyCommissionBillBookInfo>>(list), branchId);
        }

        #endregion

        #region Upload DL101 EPayment data from branch to BPM Server    

        public int UploadEPayClarify(List<EPayClarifyInfo> list, string branchId)
        {
            return _epWS.UploadEPayClarify(ServiceHelper.CompressData<List<EPayClarifyInfo>>(list), branchId);
        }    

        #endregion

        #region Upload DL110 technical data from branch to BPM Server

        //will be obsolete soon
        public int UploadRoleUser(List<RoleUserInfo> list, string branchId)
        {
            return _suWS.UploadRoleUser(ServiceHelper.CompressData<List<RoleUserInfo>>(list), branchId);
        }

        //will be obsolete soon
        public int UploadUser(List<UserInfo> list, string branchId)
        {
            return _suWS.UploadUser(ServiceHelper.CompressData<List<UserInfo>>(list), branchId);
        }

        public int UploadUnlockLog(List<UnlockLogInfo> list, string branchId)
        {
            return _suWS.UploadUnlockLog(ServiceHelper.CompressData<List<UnlockLogInfo>>(list), branchId);
        }

        #endregion  

        #region Upload DL120 billing data from branch to BPM Server

        public int UploadPrintPool(List<PrintPoolInfo> list, string branchId)
        {
            return _blWS.UploadPrintPool(ServiceHelper.CompressData<List<PrintPoolInfo>>(list), branchId);
        }

        public int UploadGrpPrintPool(List<GrpPrintPoolInfo> list, string branchId)
        {
            return _blWS.UploadGrpPrintPool(ServiceHelper.CompressData<List<GrpPrintPoolInfo>>(list), branchId);
        }

        public int UploadGreenReceiptDetail(List<GreenReceiptDetailInfo> list, string branchId)
        {
            return _blWS.UploadGreenReceiptDetail(ServiceHelper.CompressData<List<GreenReceiptDetailInfo>>(list), branchId);
        }

        //public int UploadGreenReceiptPrintHistory(List<GreenReceiptPrintHistoryInfo> list, string branchId)
        //{
        //    return _blWS.UploadGreenReceiptPrintHistory(ServiceHelper.CompressData<List<GreenReceiptPrintHistoryInfo>>(list), branchId);
        //}

        public int UploadMaxBillSeqNo(List<MaxBillSeqNoInfo> list, string branchId)
        {
            return _blWS.UploadMaxBillSeqNo(ServiceHelper.CompressData<List<MaxBillSeqNoInfo>>(list), branchId);
        }

        public int UploadPrintHistory(List<PrintHistoryInfo> list, string branchId)
        {
            //return _blWS.UploadBillPrintTracking(ServiceHelper.CompressData<List<BillPrintTrackingInfo>>(list), branchId);
            return _blWS.UploadPrintHistory(ServiceHelper.CompressData<List<PrintHistoryInfo>>(list), branchId);
        }

        public int UploadDeliveryHistory(List<DeliveryHistoryInfo> list, string branchId)
        {
            return _blWS.UploadDeliveryHistory(ServiceHelper.CompressData<List<DeliveryHistoryInfo>>(list), branchId);
        }

        public int UploadApprover(List<ApproverInfo> list, string branchId)
        {
            return _blWS.UploadApprover(ServiceHelper.CompressData<List<ApproverInfo>>(list), branchId);
        }

        public int UploadDeliveryPlace(List<DeliveryPlaceInfo> list, string branchId)
        {
            return _blWS.UploadDeliveryPlace(ServiceHelper.CompressData<List<DeliveryPlaceInfo>>(list), branchId);
        }

        public int UploadBarcodeMRU(List<BarcodeMRUInfo> list, string branchId)
        {
            return _blWS.UploadBarcodeMRU(ServiceHelper.CompressData<List<BarcodeMRUInfo>>(list), branchId);            
        }

        public int UploadBillStatus(List<BillStatusInfo> list, string branchId)
        {
            return _blWS.UploadBillStatus(ServiceHelper.CompressData<List<BillStatusInfo>>(list), branchId);   
        }
        #endregion

        #region Upload DL200 Export Table data from branch to BPM Server

        public int UploadExportTransactionLog(List<ExportTransactionLogInfo> list, string branchId)
        {
            return _exWS.UploadExportTransactionLog(ServiceHelper.CompressData<List<ExportTransactionLogInfo>>(list), branchId);
        }

        #endregion

        #region Trigger Export

        public void SignalExport(string batchName, string branchId, string modifiedBy)
        {
            //_cmServerWS.TriggerExport(branchId, modifiedBy);
            _suWS.SignalExport(batchName, branchId, modifiedBy);
        }
        
        #endregion

        #region IBpmServerService Members

        public List<AccountClassInfo> GetUpdateAccountClass(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<ContractTypeInfo> GetUpdateContractType(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<MeterSizeInfo> GetUpdateMeterSize(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<PaymentMethodInfo> GetUpdatePaymentMethod(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<TaxCodeInfo> GetUpdateTaxCode(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<UnitTypeInfo> GetUpdateUnitType(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<BusinessPartnerTypeInfo> GetUpdateBusinessPartnerType(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<PaymentTypeInfo> GetUpdatePaymentType(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<InterestKeyInfo> GetUpdateInterestKey(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<AgencyBillCollectionStatusInfo> GetUpdateAgencyBillCollectionStatus(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<AgencyBillOutStatusInfo> GetUpdateAgencyBillOutStatus(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<AgencyCommissionRateInfo> GetUpdateAgencyCommissionRate(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<BillBookStatusInfo> GetUpdateBillBookStatus(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<AgencyCollectAreaInfo> GetUpdateAgencyCollectArea(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<DisconnectionStatusInfo> GetUpdateDisconnectionStatus(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<CashierMoneyFlowTypeInfo> GetUpdateCashierMoneyFlowType(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<NonWorkingDayInfo> GetUpdateNonWorkingDay(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<CalendarInfo> GetUpdateCalendar(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<BusinessPartnerInfo> GetUpdateBusinessPartner(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<BranchInfo> GetUpdateBranch(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<MRUInfo> GetUpdateMRU(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<MRUPlanInfo> GetUpdateMRUPlan(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<ContractAccountInfo> GetUpdateContractAccount(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<EmployeeInfo> GetUpdateEmployee(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<AgencyAssetInfo> GetUpdateAgencyAsset(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<BankInfo> GetUpdateBank(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<BankAccountInfo> GetUpdateBankAccount(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<MainSubInfo> GetUpdateDebtType(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<BillingDetailInfo> GetUpdateBillingDetail(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<PrintHistoryInfo> GetUpdatePrintHistory(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<DeliveryHistoryInfo> GetUpdateDeliveryHistory(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<ApproverInfo> GetUpdateApprover(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<DeliveryPlaceInfo> GetUpdateDeliveryPlace(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<PWBBillStatusInfo> GetUpdatePWBBillStatus(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<AR> GetUpdateAR(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<APInfo> GetUpdateAP(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<APChequePaymentInfo> GetUpdateAPChequePayment(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<DisconnectionDocInfo> GetUpdateDisconnectionDoc(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<RTDisconnectionDocCaDocInfo> GetUpdateRTDisconnectionDocCaDoc(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<ARPayment> GetUpdateARPayment(string _branchId, DateTime _lastModifiedDt, DateTime _serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<Payment> GetUpdatePayment(string _branchId, DateTime _lastModifiedDt, DateTime _serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<ARPaymentType> GetUpdateARPaymentType(string _branchId, DateTime _lastModifiedDt, DateTime _serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<RTARPaymentTypeARPayment> GetUpdateRTARPaymentTypeARPayment(string _branchId, DateTime _lastModifiedDt, DateTime _serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<Receipt> GetUpdateReceipt(string _branchId, DateTime _lastModifiedDt, DateTime _serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<ReceiptItem> GetUpdateReceiptItem(string _branchId, DateTime _lastModifiedDt, DateTime _serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<CashierWorkStatusInfo> GetUpdateCashierWorkStatus(string _branchId, DateTime _lastModifiedDt, DateTime _serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<CashierMoneyTransferInfo> GetUpdateCashierMoneyTransfer(string _branchId, DateTime _lastModifiedDt, DateTime _serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<CashierMoneyFlowInfo> GetUpdateCashierMoneyFlow(string _branchId, DateTime _lastModifiedDt, DateTime _serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<CashierMoneyFlowItemInfo> GetUpdateCashierMoneyFlowItem(string _branchId, DateTime _lastModifiedDt, DateTime _serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<BillBookInfo> GetUpdateBillBook(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<BillStatusInfoInfo> GetUpdateBillStatusInfo(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<BillBookDetailInfo> GetUpdateBillBookDetail(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<BillBookInputItemInfo> GetUpdateBillBookInputItem(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<BillBookInputSetInfo> GetUpdateBillBookInputSet(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<AgencyCommissionInfo> GetUpdateAgencyCommission(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<RTAgencyContractMruInfo> GetUpdateRTAgencyContractMru(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<RTAgencyCommissionBillBookInfo> GetUpdateRTAgencyCommissionBillBook(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        //public List<AgencyModuleConfigInfo> GetUpdateAgencyModuleConfig(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        public List<EPayClarifyInfo> GetUpdateEPayClarify(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<EPayUploadInfo> GetUpdateEPayUpload(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<EPayUploadItemInfo> GetUpdateEPayUploadItem(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<RTEPayUploadPaymentInfo> GetUpdateRTEPayUploadPayment(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<AppSettingInfo> DownloadAppSetting(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<FunctionInfo> DownloadFunction(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<RoleInfo> DownloadRole(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<FunctionServiceInfo> DownloadFunctionService(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<RoleFunctionInfo> DownloadRoleFunction(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<RoleUserInfo> DownloadRoleUser(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<ServiceInfo> DownloadService(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<Terminal> DownloadTerminal(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<UnlockLogInfo> DownloadUnlockLog(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<UserInfo> DownloadUser(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<UnlockRemarkInfo> DownloadUnlockRemark(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }
     
        public List<BillUpdateInfo> GetUpdateBillUpdate(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<PaymentSequenceInfo> GetUpdatePaymentSequence(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }
       
        public List<PrintPoolInfo> GetUpdatePrintPool(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<GrpPrintPoolInfo> GetUpdateGrpPrintPool(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<GreenReceiptDetailInfo> GetUpdateGreenReceiptDetail(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        //public List<GreenReceiptPrintHistoryInfo> GetUpdateGreenReceiptPrintHistory(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        public List<MaxBillSeqNoInfo> GetUpdateMaxBillSeqNo(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }     

        public List<OwnBankInfo> GetUpdateOwnBank(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<BarcodeMRUInfo> GetUpdateBarcodeMRU(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<BillStatusInfo> GetUpdateBillStatus(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<OldCaMappingInfo> GetUpdateOldCaMapping(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
