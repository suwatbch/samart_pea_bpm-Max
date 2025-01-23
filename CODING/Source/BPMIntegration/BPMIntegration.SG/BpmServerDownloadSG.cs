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
    public class BpmServerDownloadSG : IBpmServerService
    {
        MasterDLWS.MasterIntegrationWebService _mtWS;
        BlanDLWS.BLANIntegrationWebService _blWS;
        ARDLWS.ARWebService _arWS;
        AgencyDLWS.AgencyIntegrationWebService _agWS;
        UtilDLWS.UtilitiesIntegrationWebService _suWS;
        CashDLWS.CashManagementWebService _cmWS;        
        EPayDLWS.EPaymentWebService _epWS;

        private const string WS_AR_ADDR_DL = "WS_AR_ADDR_DL";
        private const string WS_AGENCY_ADDR_DL = "WS_AGENCY_ADDR_DL";
        private const string WS_MASTER_ADDR_DL = "WS_MASTER_ADDR_DL";
        private const string WS_BLAN_ADDR_DL = "WS_BLAN_ADDR_DL";
        private const string WS_UTIL_ADDR_DL = "WS_UTIL_ADDR_DL";
        private const string WS_CASH_ADDR_DL = "WS_CASH_ADDR_DL";
        private const string WS_EPAY_ADDR_DL = "WS_EPAY_ADDR_DL";
        
        public BpmServerDownloadSG()
        {
            _arWS = new ARDLWS.ARWebService();
            ARDLWS.AuthenInfo arWSInfo = new ARDLWS.AuthenInfo();
            arWSInfo.AuthToken = WebServiceIntegrationToken.Value;
            _arWS.AuthenInfoValue = arWSInfo;
            _arWS.Timeout = 7200000; //1000*60*60*2
            _arWS.Url = ConfigurationManager.AppSettings[WS_AR_ADDR_DL];

            _agWS = new AgencyDLWS.AgencyIntegrationWebService();
            AgencyDLWS.AuthenInfo agencyWSInfo = new AgencyDLWS.AuthenInfo();
            agencyWSInfo.AuthToken = WebServiceIntegrationToken.Value;
            _agWS.AuthenInfoValue = agencyWSInfo;
            _agWS.Timeout = 7200000; //1000*60*60*2
            _agWS.Url = ConfigurationManager.AppSettings[WS_AGENCY_ADDR_DL];

            _mtWS = new MasterDLWS.MasterIntegrationWebService();
            MasterDLWS.AuthenInfo masterWSInfo = new MasterDLWS.AuthenInfo();
            masterWSInfo.AuthToken = WebServiceIntegrationToken.Value;
            _mtWS.AuthenInfoValue = masterWSInfo;
            _mtWS.Timeout = 7200000; //1000*60*60*2 
            _mtWS.Url = ConfigurationManager.AppSettings[WS_MASTER_ADDR_DL];

            _blWS = new BlanDLWS.BLANIntegrationWebService();
            BlanDLWS.AuthenInfo blanWSInfo = new BlanDLWS.AuthenInfo();
            blanWSInfo.AuthToken = WebServiceIntegrationToken.Value;
            _blWS.AuthenInfoValue = blanWSInfo;
            _blWS.Timeout = 7200000; //1000*60*60*2
            _blWS.Url = ConfigurationManager.AppSettings[WS_BLAN_ADDR_DL];

            _suWS = new UtilDLWS.UtilitiesIntegrationWebService();
            UtilDLWS.AuthenInfo suWSInfo = new UtilDLWS.AuthenInfo();
            suWSInfo.AuthToken = WebServiceIntegrationToken.Value;
            _suWS.AuthenInfoValue = suWSInfo;
            _suWS.Timeout = 7200000; //1000*60*60*2 
            _suWS.Url = ConfigurationManager.AppSettings[WS_UTIL_ADDR_DL];

            _cmWS = new CashDLWS.CashManagementWebService();
            CashDLWS.AuthenInfo cuWSInfo = new CashDLWS.AuthenInfo();
            cuWSInfo.AuthToken = WebServiceIntegrationToken.Value;
            _cmWS.AuthenInfoValue = cuWSInfo;
            _cmWS.Timeout = 7200000; //1000*60*60*2 
            _cmWS.Url = ConfigurationManager.AppSettings[WS_CASH_ADDR_DL];      


            _epWS = new EPayDLWS.EPaymentWebService();
            EPayDLWS.AuthenInfo epWSInfo = new EPayDLWS.AuthenInfo();
            epWSInfo.AuthToken = WebServiceIntegrationToken.Value;
            _epWS.AuthenInfoValue = epWSInfo;
            _epWS.Timeout = 7200000; //1000*60*60*2 
            _epWS.Url = ConfigurationManager.AppSettings[WS_EPAY_ADDR_DL];


            //_ws = new BillPrintingWCFServiceClient();
            //string absoluteUri = _ws.Endpoint.Address.Uri.AbsoluteUri;

            //if (onlineConnection)
            //{
            //    string[] url = absoluteUri.Split('/');
            //    string registerProxyAddress = "http://" + url[2] + "/" + url[3] + "/";
            //    absoluteUri = absoluteUri.Replace(registerProxyAddress, Session.Server.Address.Center);
            //    _ws = new BillPrintingWCFServiceClient("BasicHttpBinding_IBillPrintingWCFService", new EndpointAddress(absoluteUri));
            //    if (Session.User.Token.Center == null) Authorization.LoadServerToken(true);
            //    _ws.AuthInfoValue = new AuthInfo();
            //    _ws.AuthInfoValue.UserId = Session.User.Id;
            //    _ws.AuthInfoValue.AuthToken = Session.User.Token.Center;
            //}
            //else
            //{
            //    absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Branch);
            //    _ws = new BillPrintingWCFServiceClient("BasicHttpBinding_IBillPrintingWCFService", new EndpointAddress(absoluteUri));
            //    if (Session.User.Token.Branch == null) Authorization.LoadServerToken(false);
            //    _ws.AuthInfoValue = new AuthInfo();
            //    _ws.AuthInfoValue.UserId = Session.User.Id;
            //    _ws.AuthInfoValue.AuthToken = Session.User.Token.Branch;
            //}
            //_ws.InnerChannel.OperationTimeout = new TimeSpan(0, 5, 0);
        }

        #region Download the server time

        public DateTime GetServerTime()
        {
            return _suWS.GetServerTime();
        }

        #endregion
        
        #region Download DL010 Data from BPM Server

        public List<NonWorkingDayInfo> GetUpdateNonWorkingDay(string  branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<NonWorkingDayInfo>>(_mtWS.GetUpdateNonWorkingDay(branchId, lastModifiedDate, serverDate));
        }

        public List<AccountClassInfo> GetUpdateAccountClass(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<AccountClassInfo>>(_mtWS.GetUpdateAccountClass(branchId, lastModifiedDate, serverDate));
        }

        public List<ContractTypeInfo> GetUpdateContractType(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<ContractTypeInfo>>(_mtWS.GetUpdateContractType(branchId, lastModifiedDate, serverDate));
        }

        public List<MeterSizeInfo> GetUpdateMeterSize(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {            
            return ServiceHelper.DecompressData<List<MeterSizeInfo>>(_mtWS.GetUpdateMeterSize(branchId, lastModifiedDate, serverDate));
        }

        public List<PaymentMethodInfo> GetUpdatePaymentMethod(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {            
            return ServiceHelper.DecompressData<List<PaymentMethodInfo>>(_mtWS.GetUpdatePaymentMethod(branchId, lastModifiedDate, serverDate));
        }
        
        public List<TaxCodeInfo> GetUpdateTaxCode(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {            
            return ServiceHelper.DecompressData<List<TaxCodeInfo>>(_mtWS.GetUpdateTaxCode(branchId, lastModifiedDate, serverDate));
        }

        public List<UnitTypeInfo> GetUpdateUnitType(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {            
            return ServiceHelper.DecompressData<List<UnitTypeInfo>>(_mtWS.GetUpdateUnitType(branchId, lastModifiedDate, serverDate));
        }
        
        public List<BusinessPartnerTypeInfo> GetUpdateBusinessPartnerType(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<BusinessPartnerTypeInfo>>(_mtWS.GetUpdateBusinessPartnerType(branchId, lastModifiedDate, serverDate));
        }

        public List<CalendarInfo> GetUpdateCalendar(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<CalendarInfo>>(_mtWS.GetUpdateCalendar(branchId, lastModifiedDate, serverDate));
        }

        public List<PaymentTypeInfo> GetUpdatePaymentType(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<PaymentTypeInfo>>(_mtWS.GetUpdatePaymentType(branchId, lastModifiedDate, serverDate));
        }

        public List<InterestKeyInfo> GetUpdateInterestKey(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<InterestKeyInfo>>(_mtWS.GetUpdateInterestKey(branchId, lastModifiedDate, serverDate));
        }

        public List<AgencyBillCollectionStatusInfo> GetUpdateAgencyBillCollectionStatus(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<AgencyBillCollectionStatusInfo>>(_mtWS.GetUpdateAgencyBillCollectionStatus(branchId, lastModifiedDate, serverDate));
        }

        public List<AgencyBillOutStatusInfo> GetUpdateAgencyBillOutStatus(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<AgencyBillOutStatusInfo>>(_mtWS.GetUpdateAgencyBillOutStatus(branchId, lastModifiedDate, serverDate));
        }

        public List<AgencyCommissionRateInfo> GetUpdateAgencyCommissionRate(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<AgencyCommissionRateInfo>>(_mtWS.GetUpdateAgencyCommissionRate(branchId, lastModifiedDate, serverDate));
        }

        public List<BillBookStatusInfo> GetUpdateBillBookStatus(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<BillBookStatusInfo>>(_mtWS.GetUpdateBillBookStatus(branchId, lastModifiedDate, serverDate));
        }

        public List<AgencyCollectAreaInfo> GetUpdateAgencyCollectArea(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<AgencyCollectAreaInfo>>(_mtWS.GetUpdateAgencyCollectArea(branchId, lastModifiedDate, serverDate));
        }

        public List<DisconnectionStatusInfo> GetUpdateDisconnectionStatus(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<DisconnectionStatusInfo>>(_mtWS.GetUpdateDisconnectionStatus(branchId, lastModifiedDate, serverDate));
        }

        public List<CashierMoneyFlowTypeInfo> GetUpdateCashierMoneyFlowType(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<CashierMoneyFlowTypeInfo>>(_mtWS.GetUpdateCashierMoneyFlowType(branchId, lastModifiedDate, serverDate));
        }

        public List<OwnBankInfo> GetUpdateOwnBank(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<OwnBankInfo>>(_mtWS.GetUpdateOwnBank(branchId, lastModifiedDate, serverDate));
        }

        #endregion

        #region Download DL020 Data from BPM Server

        public List<BusinessPartnerInfo> GetUpdateBusinessPartner(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            CompressData cd = _mtWS.GetUpdateBusinessPartner(branchId, lastModifiedDate, serverDate);            
            StreamWriter sw = new StreamWriter("C:\\CompressDataLog.txt", true);            
            sw.WriteLine(DateTime.Now + " compress length (BP) = " + cd.OriginalSize.ToString() + "|" + cd.CompressSize.ToString());
            sw.Close();
            return ServiceHelper.DecompressData<List<BusinessPartnerInfo>>(cd);
            
            //return ServiceHelper.DecompressData<List<BusinessPartnerInfo>>(_mtWS.GetUpdateBusinessPartner(branchId, lastModifiedDate, serverDate));
        }

        public List<BranchInfo> GetUpdateBranch(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {           
            return ServiceHelper.DecompressData<List<BranchInfo>>(_mtWS.GetUpdateBranch(branchId, lastModifiedDate, serverDate));
        }

        public List<MRUInfo> GetUpdateMRU(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<MRUInfo>>(_mtWS.GetUpdateMRU(branchId, lastModifiedDate, serverDate));
        }

        public List<MRUPlanInfo> GetUpdateMRUPlan(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<MRUPlanInfo>>(_mtWS.GetUpdateMRUPlan(branchId, lastModifiedDate, serverDate));
        }

        public List<ContractAccountInfo> GetUpdateContractAccount(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            CompressData cd = _mtWS.GetUpdateContractAccount(branchId, lastModifiedDate, serverDate);
            StreamWriter sw = new StreamWriter("C:\\CompressDataLog.txt",true);
            sw.WriteLine(DateTime.Now + " compress length (CA) = " + cd.OriginalSize.ToString() + "|" + cd.CompressSize.ToString());
            sw.Close();
            return ServiceHelper.DecompressData<List<ContractAccountInfo>>(cd);

            //return ServiceHelper.DecompressData<List<ContractAccountInfo>>(_mtWS.GetUpdateContractAccount(branchId, lastModifiedDate, serverDate));
        }

        public List<EmployeeInfo> GetUpdateEmployee(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<EmployeeInfo>>(_mtWS.GetUpdateEmployee(branchId, lastModifiedDate, serverDate));
        }

        public List<AgencyAssetInfo> GetUpdateAgencyAsset(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<AgencyAssetInfo>>(_mtWS.GetUpdateAgencyAsset(branchId, lastModifiedDate, serverDate));
        }
        
        public List<BankInfo> GetUpdateBank(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<BankInfo>>(_mtWS.GetUpdateBank(branchId, lastModifiedDate, serverDate));
        }

        public List<BankAccountInfo> GetUpdateBankAccount(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<BankAccountInfo>>(_mtWS.GetUpdateBankAccount(branchId, lastModifiedDate, serverDate));
        }

        public List<MainSubInfo> GetUpdateDebtType(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<MainSubInfo>>(_mtWS.GetUpdateDebtType(branchId, lastModifiedDate, serverDate));
        }

        public List<PaymentSequenceInfo> GetUpdatePaymentSequence(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<PaymentSequenceInfo>>(_mtWS.GetUpdatePaymentSequence(branchId, lastModifiedDate, serverDate));
        }

        public List<OldCaMappingInfo> GetUpdateOldCaMapping(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<OldCaMappingInfo>>(_mtWS.GetUpdateOldCaMapping(branchId, lastModifiedDate, serverDate));
        }

        #endregion        
         
        #region Download DL030 Data from BPM Server

        public List<PrintPoolInfo> GetUpdatePrintPool(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<PrintPoolInfo>>(_blWS.GetUpdatePrintPool(branchId, lastModifiedDate, serverDate));
        }

        public List<GrpPrintPoolInfo> GetUpdateGrpPrintPool(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<GrpPrintPoolInfo>>(_blWS.GetUpdateGrpPrintPool(branchId, lastModifiedDate, serverDate));
        }

        public List<GreenReceiptDetailInfo> GetUpdateGreenReceiptDetail(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<GreenReceiptDetailInfo>>(_blWS.GetUpdateGreenReceiptDetail(branchId, lastModifiedDate, serverDate));
        }

        //public List<GreenReceiptPrintHistoryInfo> GetUpdateGreenReceiptPrintHistory(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        //{
        //    return ServiceHelper.DecompressData<List<GreenReceiptPrintHistoryInfo>>(_blWS.GetUpdateGreenReceiptPrintHistory(branchId, lastModifiedDate, serverDate));
        //}

        public List<MaxBillSeqNoInfo> GetUpdateMaxBillSeqNo(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<MaxBillSeqNoInfo>>(_blWS.GetUpdateMaxBillSeqNo(branchId, lastModifiedDate, serverDate));
        }

        public List<BillingDetailInfo> GetUpdateBillingDetail(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            CompressData cd = _blWS.GetUpdateBillingDetail(branchId, lastModifiedDate, serverDate);
            StreamWriter sw = new StreamWriter("C:\\CompressDataLog.txt", true);
            sw.WriteLine(DateTime.Now + " compress length (BillDetail) = " + cd.OriginalSize.ToString() + "|" + cd.CompressSize.ToString());
            sw.Close();
            return ServiceHelper.DecompressData<List<BillingDetailInfo>>(cd);

            //return ServiceHelper.DecompressData<List<BillingDetailInfo>>(_blWS.GetUpdateBillingDetail(branchId, lastModifiedDate, serverDate));
        }

        public List<PrintHistoryInfo> GetUpdatePrintHistory(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<PrintHistoryInfo>>(_blWS.GetUpdatePrintHistory(branchId, lastModifiedDate, serverDate));
            //return new List<PrintHistoryInfo>();
        }

        public List<DeliveryHistoryInfo> GetUpdateDeliveryHistory(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<DeliveryHistoryInfo>>(_blWS.GetUpdateDeliveryHistory(branchId, lastModifiedDate, serverDate));
        }

        public List<ApproverInfo> GetUpdateApprover(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<ApproverInfo>>(_blWS.GetUpdateApprover(branchId, lastModifiedDate, serverDate));
        }

        public List<DeliveryPlaceInfo> GetUpdateDeliveryPlace(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<DeliveryPlaceInfo>>(_blWS.GetUpdateDeliveryPlace(branchId, lastModifiedDate, serverDate));
        }

        public List<PWBBillStatusInfo> GetUpdatePWBBillStatus(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<PWBBillStatusInfo>>(_blWS.GetUpdatePWBBillStatus(branchId, lastModifiedDate, serverDate));
        }

        public List<BillUpdateInfo> GetUpdateBillUpdate(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<BillUpdateInfo>>(_blWS.GetUpdateBillUpdate(branchId, lastModifiedDate, serverDate));
        }

        public List<BarcodeMRUInfo> GetUpdateBarcodeMRU(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<BarcodeMRUInfo>>(_blWS.GetUpdateBarcodeMRU(branchId, lastModifiedDate, serverDate));            
        }

        public List<BillStatusInfo> GetUpdateBillStatus(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<BillStatusInfo>>(_blWS.GetUpdateBillStatus(branchId, lastModifiedDate, serverDate));
            //return new List<BillStatusInfo>();
        }


        #endregion

        #region Download DL040 Data from BPM Server

        public List<AR> GetUpdateAR(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            CompressData cd = _arWS.DownloadAR(branchId, lastModifiedDt, serverDate);
            StreamWriter sw = new StreamWriter("C:\\CompressDataLog.txt", true);
            sw.WriteLine(DateTime.Now + " compress length (AR) = " + cd.OriginalSize.ToString() + "|" + cd.CompressSize.ToString());
            sw.Close();
            return ServiceHelper.DecompressData<List<AR>>(cd);
            
            //return ServiceHelper.DecompressData<List<AR>>(_arWS.DownloadAR(branchId, lastModifiedDt, serverDate));
        }

        public List<DisconnectionDocInfo> GetUpdateDisconnectionDoc(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            CompressData cd = _arWS.DownloadDisconnectionDoc(branchId, lastModifiedDt, serverDate);            
            return ServiceHelper.DecompressData<List<DisconnectionDocInfo>>(cd);
        }

        public List<RTDisconnectionDocCaDocInfo> GetUpdateRTDisconnectionDocCaDoc(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            CompressData cd = _arWS.DownloadRTDisconnectionDocCaDoc(branchId, lastModifiedDt, serverDate);
            return ServiceHelper.DecompressData<List<RTDisconnectionDocCaDocInfo>>(cd);
        }

        public List<APInfo> GetUpdateAP(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            CompressData cd = _arWS.DownloadAP(branchId, lastModifiedDt, serverDate);
            return ServiceHelper.DecompressData<List<APInfo>>(cd);
        }

        public List<APChequePaymentInfo> GetUpdateAPChequePayment(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            CompressData cd = _arWS.DownloadAPChequePayment(branchId, lastModifiedDt, serverDate);
            return ServiceHelper.DecompressData<List<APChequePaymentInfo>>(cd);
        }

        #endregion        

        #region Download DL050 Data from BPM Server

        public List<ARPayment> GetUpdateARPayment(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {           
            return ServiceHelper.DecompressData<List<ARPayment>>(_arWS.DownloadARPayment(branchId, lastModifiedDate, serverDate));
        }

        public List<Payment> GetUpdatePayment(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {            
            return ServiceHelper.DecompressData<List<Payment>>(_arWS.DownloadPayment(branchId, lastModifiedDate, serverDate));
        }

        public List<ARPaymentType> GetUpdateARPaymentType(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<ARPaymentType>>(_arWS.DownloadARPaymentType(branchId, lastModifiedDate, serverDate));
        }

        public List<RTARPaymentTypeARPayment> GetUpdateRTARPaymentTypeARPayment(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<RTARPaymentTypeARPayment>>(_arWS.DownloadRTARPaymentTypeARPayment(branchId, lastModifiedDate, serverDate));
        }

        public List<Receipt> GetUpdateReceipt(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<Receipt>>(_arWS.DownloadReceipt(branchId, lastModifiedDate, serverDate));            
        }

        public List<ReceiptItem> GetUpdateReceiptItem(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<ReceiptItem>>(_arWS.DownloadReceiptItem(branchId, lastModifiedDate, serverDate));
        }

        #endregion

        #region Download DL051 Data from BPMServer

        public List<CashierWorkStatusInfo> GetUpdateCashierWorkStatus(string _branchId, DateTime _lastModifiedDate, DateTime _serverDate)
        {
            return ServiceHelper.DecompressData<List<CashierWorkStatusInfo>>(_cmWS.GetUpdateCashierWorkStatus(_branchId, _lastModifiedDate, _serverDate));
        }

        public List<CashierMoneyTransferInfo> GetUpdateCashierMoneyTransfer(string _branchId, DateTime _lastModifiedDate, DateTime _serverDate)
        {
            return ServiceHelper.DecompressData<List<CashierMoneyTransferInfo>>(_cmWS.GetUpdateCashierMoneyTransfer(_branchId, _lastModifiedDate, _serverDate));
        }

        public List<CashierMoneyFlowInfo> GetUpdateCashierMoneyFlow(string _branchId, DateTime _lastModifiedDate, DateTime _serverDate)
        {
            return ServiceHelper.DecompressData<List<CashierMoneyFlowInfo>>(_cmWS.GetUpdateCashierMoneyFlow(_branchId, _lastModifiedDate, _serverDate));
        }

        public List<CashierMoneyFlowItemInfo> GetUpdateCashierMoneyFlowItem(string _branchId, DateTime _lastModifiedDate, DateTime _serverDate)
        {
            return ServiceHelper.DecompressData<List<CashierMoneyFlowItemInfo>>(_cmWS.GetUpdateCashierMoneyFlowItem(_branchId, _lastModifiedDate, _serverDate));
        }

        #endregion

        #region Download DL060 Data from BPM Server

        public List<BillBookInfo> GetUpdateBillBook(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<BillBookInfo>>(_agWS.DownloadBillBook(branchId, lastModifiedDate, serverDate));
        }

        public List<BillStatusInfoInfo> GetUpdateBillStatusInfo(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<BillStatusInfoInfo>>(_agWS.DownloadBillStatusInfo(branchId, lastModifiedDate, serverDate));
        }

        public List<BillBookDetailInfo> GetUpdateBillBookDetail(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<BillBookDetailInfo>>(_agWS.DownloadBillBookDetail(branchId, lastModifiedDate, serverDate));
        }

        public List<BillBookInputItemInfo> GetUpdateBillBookInputItem(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<BillBookInputItemInfo>>(_agWS.DownloadBillBookInputItem(branchId, lastModifiedDate, serverDate));
        }

        public List<BillBookInputSetInfo> GetUpdateBillBookInputSet(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<BillBookInputSetInfo>>(_agWS.DownloadBillBookInputSet(branchId, lastModifiedDate, serverDate));
        }

        public List<AgencyCommissionInfo> GetUpdateAgencyCommission(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<AgencyCommissionInfo>>(_agWS.DownloadAgencyCommission(branchId, lastModifiedDate, serverDate));
        }

        public List<RTAgencyContractMruInfo> GetUpdateRTAgencyContractMru(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<RTAgencyContractMruInfo>>(_agWS.DownloadRTAgencyContractMru(branchId, lastModifiedDate, serverDate));
        }

        public List<RTAgencyCommissionBillBookInfo> GetUpdateRTAgencyCommissionBillBook(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<RTAgencyCommissionBillBookInfo>>(_agWS.DownloadRTAgencyCommissionBillBook(branchId, lastModifiedDate, serverDate));
        }

        //public List<AgencyModuleConfigInfo> GetUpdateAgencyModuleConfig(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        //{
        //    return ServiceHelper.DecompressData<List<AgencyModuleConfigInfo>>(_agWS.DownloadAgencyModuleConfig(branchId, lastModifiedDate, serverDate));
        //}

        #endregion    

        #region Download DL061 Data from BPM Server


        public List<EPayClarifyInfo> GetUpdateEPayClarify(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<EPayClarifyInfo>>(_epWS.GetUpdateEPayClarify(branchId, lastModifiedDate, serverDate));
        }

        public List<EPayUploadInfo> GetUpdateEPayUpload(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<EPayUploadInfo>>(_epWS.GetUpdateEPayUpload(branchId, lastModifiedDate, serverDate));
        }

        public List<EPayUploadItemInfo> GetUpdateEPayUploadItem(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<EPayUploadItemInfo>>(_epWS.GetUpdateEPayUploadItem(branchId, lastModifiedDate, serverDate));
        }

        public List<RTEPayUploadPaymentInfo> GetUpdateRTEPayUploadPayment(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<RTEPayUploadPaymentInfo>>(_epWS.GetUpdateRTEPayUploadPayment(branchId, lastModifiedDate, serverDate));
        }

        #endregion

        #region Download DL070 Data from BPM Server

        public List<AppSettingInfo> DownloadAppSetting(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<AppSettingInfo>>(_suWS.DownloadAppSetting(branchId, lastModifiedDate, serverDate));
        }

        public List<Terminal> DownloadTerminal(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<Terminal>>(_suWS.DownloadTerminal(branchId, lastModifiedDate, serverDate));
        }

        public List<ServiceInfo> DownloadService(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<ServiceInfo>>(_suWS.DownloadService(branchId, lastModifiedDate, serverDate));
        }

        public List<FunctionInfo> DownloadFunction(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<FunctionInfo>>(_suWS.DownloadFunction(branchId, lastModifiedDate, serverDate));
        }

        public List<RoleInfo> DownloadRole(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<RoleInfo>>(_suWS.DownloadRole(branchId, lastModifiedDate, serverDate));
        }

        public List<UserInfo> DownloadUser(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<UserInfo>>(_suWS.DownloadUser(branchId, lastModifiedDate, serverDate));
        }

        public List<FunctionServiceInfo> DownloadFunctionService(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<FunctionServiceInfo>>(_suWS.DownloadFunctionService(branchId, lastModifiedDate, serverDate));
        }

        public List<RoleFunctionInfo> DownloadRoleFunction(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<RoleFunctionInfo>>(_suWS.DownloadRoleFunction(branchId, lastModifiedDate, serverDate));
        }

        public List<RoleUserInfo> DownloadRoleUser(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<RoleUserInfo>>(_suWS.DownloadRoleUser(branchId, lastModifiedDate, serverDate));
        }

        public List<UnlockLogInfo> DownloadUnlockLog(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<UnlockLogInfo>>(_suWS.DownloadUnlockLog(branchId, lastModifiedDate, serverDate));
        }

        public List<UnlockRemarkInfo> DownloadUnlockRemark(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            return ServiceHelper.DecompressData<List<UnlockRemarkInfo>>(_suWS.DownloadUnlockRemark(branchId, lastModifiedDate, serverDate));
        }

        #endregion
        
        #region IBpmServerService Members

        public int UploadMRUPlan(List<MRUPlanInfo> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UploadBillingDetail(List<BillingDetailSyncInfo> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UploadPrintHistory(List<PrintHistoryInfo> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UploadDeliveryHistory(List<DeliveryHistoryInfo> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UploadApprover(List<ApproverInfo> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UploadDeliveryPlace(List<DeliveryPlaceInfo> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UploadCashierWorkStatus(List<CashierWorkStatusInfo> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UploadCashierMoneyTransfer(List<CashierMoneyTransferInfo> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UploadCashierMoneyFlow(List<CashierMoneyFlowInfo> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UploadCashierMoneyFlowItem(List<CashierMoneyFlowItemInfo> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UploadAR(List<AR> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UploadAP(List<APInfo> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UploadAPChequePayment(List<APChequePaymentInfo> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UploadARPayment(List<ARPayment> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UploadPayment(List<Payment> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UploadARPaymentType(List<ARPaymentType> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UploadRTARPaymentTypeARPayment(List<RTARPaymentTypeARPayment> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UploadReceipt(List<Receipt> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UploadReceiptItem(List<ReceiptItem> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UploadBillBook(List<BillBookInfo> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UploadBillStatusInfo(List<BillStatusInfoInfo> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UploadBillBookDetail(List<BillBookDetailInfo> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UploadBillBookInputItem(List<BillBookInputItemInfo> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UploadBillBookInputSet(List<BillBookInputSetInfo> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UploadAgencyCommission(List<AgencyCommissionInfo> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UploadRTAgencyContractMru(List<RTAgencyContractMruInfo> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UploadRTAgencyCommissionBillBook(List<RTAgencyCommissionBillBookInfo> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UploadEPayClarify(List<EPayClarifyInfo> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UploadUnlockLog(List<UnlockLogInfo> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UploadRoleUser(List<RoleUserInfo> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UploadUser(List<UserInfo> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void SignalExport(string batchName, string branchId, string modifiedBy)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UploadPrintPool(List<PrintPoolInfo> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UploadGrpPrintPool(List<GrpPrintPoolInfo> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UploadGreenReceiptDetail(List<GreenReceiptDetailInfo> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        //public int UploadGreenReceiptPrintHistory(List<GreenReceiptPrintHistoryInfo> list, string branchId)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        public int UploadMaxBillSeqNo(List<MaxBillSeqNoInfo> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UploadBarcodeMRU(List<BarcodeMRUInfo> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UploadBillStatus(List<BillStatusInfo> list, string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int UploadPaymentLog(List<PaymentLogInfo> list, string branchId)
        {
            //throw new NotImplementedException();
            throw new Exception("The method or operation is not implemented.");
        }
        
        public int UploadExportTransactionLog(List<ExportTransactionLogInfo> list, string branchId)
        {
            //throw new NotImplementedException();
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
