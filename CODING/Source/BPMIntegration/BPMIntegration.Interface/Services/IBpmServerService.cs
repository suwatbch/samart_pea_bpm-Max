using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;
using System.Data.Common;

namespace PEA.BPM.Integration.BPMIntegration.Interface.Services
{
    public interface IBpmServerService
    {
        DateTime GetServerTime();        

        //DL ISOLATE
        List<AccountClassInfo> GetUpdateAccountClass(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<ContractTypeInfo> GetUpdateContractType(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<MeterSizeInfo> GetUpdateMeterSize(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<PaymentMethodInfo> GetUpdatePaymentMethod(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<TaxCodeInfo> GetUpdateTaxCode(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<UnitTypeInfo> GetUpdateUnitType(string branchId, DateTime lastModifiedDate, DateTime serverDate);        
        List<BusinessPartnerTypeInfo> GetUpdateBusinessPartnerType(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<PaymentTypeInfo> GetUpdatePaymentType(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<InterestKeyInfo> GetUpdateInterestKey(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<AgencyBillCollectionStatusInfo> GetUpdateAgencyBillCollectionStatus(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<AgencyBillOutStatusInfo> GetUpdateAgencyBillOutStatus(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<AgencyCommissionRateInfo> GetUpdateAgencyCommissionRate(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<BillBookStatusInfo> GetUpdateBillBookStatus(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<AgencyCollectAreaInfo> GetUpdateAgencyCollectArea(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<DisconnectionStatusInfo> GetUpdateDisconnectionStatus(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<CashierMoneyFlowTypeInfo> GetUpdateCashierMoneyFlowType(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<OwnBankInfo> GetUpdateOwnBank(string branchId, DateTime lastModifiedDate, DateTime serverDate);

        //DL MASTER
        List<NonWorkingDayInfo> GetUpdateNonWorkingDay(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<CalendarInfo> GetUpdateCalendar(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<BusinessPartnerInfo> GetUpdateBusinessPartner(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<BranchInfo> GetUpdateBranch(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<MRUInfo> GetUpdateMRU(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<MRUPlanInfo> GetUpdateMRUPlan(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<ContractAccountInfo> GetUpdateContractAccount(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<EmployeeInfo> GetUpdateEmployee(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<AgencyAssetInfo> GetUpdateAgencyAsset(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<BankInfo> GetUpdateBank(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<BankAccountInfo> GetUpdateBankAccount(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<MainSubInfo> GetUpdateDebtType(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<PaymentSequenceInfo> GetUpdatePaymentSequence(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<OldCaMappingInfo> GetUpdateOldCaMapping(string branchId, DateTime lastModifiedDate, DateTime serverDate);

        //DL BILLING
        List<PrintPoolInfo> GetUpdatePrintPool(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<GrpPrintPoolInfo> GetUpdateGrpPrintPool(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<GreenReceiptDetailInfo> GetUpdateGreenReceiptDetail(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        //List<GreenReceiptPrintHistoryInfo> GetUpdateGreenReceiptPrintHistory(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<BillingDetailInfo> GetUpdateBillingDetail(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<PrintHistoryInfo> GetUpdatePrintHistory(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<DeliveryHistoryInfo> GetUpdateDeliveryHistory(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<MaxBillSeqNoInfo> GetUpdateMaxBillSeqNo(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<ApproverInfo> GetUpdateApprover(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<DeliveryPlaceInfo> GetUpdateDeliveryPlace(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<PWBBillStatusInfo> GetUpdatePWBBillStatus(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<BarcodeMRUInfo> GetUpdateBarcodeMRU(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<BillStatusInfo> GetUpdateBillStatus(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        
        //DL TRANSACTION AR
        List<AR> GetUpdateAR(string branchId, DateTime lastModifiedDt, DateTime serverDate);
        List<APInfo> GetUpdateAP(string branchId, DateTime lastModifiedDt, DateTime serverDate);
        List<APChequePaymentInfo> GetUpdateAPChequePayment(string branchId, DateTime lastModifiedDt, DateTime serverDate);
        List<DisconnectionDocInfo> GetUpdateDisconnectionDoc(string branchId, DateTime lastModifiedDt, DateTime serverDate);
        List<RTDisconnectionDocCaDocInfo> GetUpdateRTDisconnectionDocCaDoc(string branchId, DateTime lastModifiedDt, DateTime serverDate);
        
        //DL PAYFROMSAP
        List<ARPayment> GetUpdateARPayment(string _branchId, DateTime _lastModifiedDt, DateTime _serverDate);
        List<Payment> GetUpdatePayment(string _branchId, DateTime _lastModifiedDt, DateTime _serverDate);
        List<ARPaymentType> GetUpdateARPaymentType(string _branchId, DateTime _lastModifiedDt, DateTime _serverDate);
        List<RTARPaymentTypeARPayment> GetUpdateRTARPaymentTypeARPayment(string _branchId, DateTime _lastModifiedDt, DateTime _serverDate);
        List<Receipt> GetUpdateReceipt(string _branchId, DateTime _lastModifiedDt, DateTime _serverDate);
        List<ReceiptItem> GetUpdateReceiptItem(string _branchId, DateTime _lastModifiedDt, DateTime _serverDate);

        //DL CASH MANAGEMENT
        List<CashierWorkStatusInfo> GetUpdateCashierWorkStatus(string _branchId, DateTime _lastModifiedDt, DateTime _serverDate);
        List<CashierMoneyTransferInfo> GetUpdateCashierMoneyTransfer(string _branchId, DateTime _lastModifiedDt, DateTime _serverDate);
        List<CashierMoneyFlowInfo> GetUpdateCashierMoneyFlow(string _branchId, DateTime _lastModifiedDt, DateTime _serverDate);
        List<CashierMoneyFlowItemInfo> GetUpdateCashierMoneyFlowItem(string _branchId, DateTime _lastModifiedDt, DateTime _serverDate);

        //DL AGENCY      
        List<BillBookInfo> GetUpdateBillBook(string branchId, DateTime lastModifiedDt, DateTime serverDate);
        List<BillStatusInfoInfo> GetUpdateBillStatusInfo(string branchId, DateTime lastModifiedDt, DateTime serverDate);
        List<BillBookDetailInfo> GetUpdateBillBookDetail(string branchId, DateTime lastModifiedDt, DateTime serverDate);
        List<BillBookInputItemInfo> GetUpdateBillBookInputItem(string branchId, DateTime lastModifiedDt, DateTime serverDate);
        List<BillBookInputSetInfo> GetUpdateBillBookInputSet(string branchId, DateTime lastModifiedDt, DateTime serverDate);
        List<AgencyCommissionInfo> GetUpdateAgencyCommission(string branchId, DateTime lastModifiedDt, DateTime serverDate);
        List<RTAgencyContractMruInfo> GetUpdateRTAgencyContractMru(string branchId, DateTime lastModifiedDt, DateTime serverDate);
        List<RTAgencyCommissionBillBookInfo> GetUpdateRTAgencyCommissionBillBook(string branchId, DateTime lastModifiedDt, DateTime serverDate);
        //List<AgencyModuleConfigInfo> GetUpdateAgencyModuleConfig(string branchId, DateTime lastModifiedDt, DateTime serverDate);

        //DL EPAYMENT
        List<EPayClarifyInfo> GetUpdateEPayClarify(string branchId, DateTime lastModifiedDt, DateTime serverDate);
        List<EPayUploadInfo> GetUpdateEPayUpload(string branchId, DateTime lastModifiedDt, DateTime serverDate);
        List<EPayUploadItemInfo> GetUpdateEPayUploadItem(string branchId, DateTime lastModifiedDt, DateTime serverDate);
        List<RTEPayUploadPaymentInfo> GetUpdateRTEPayUploadPayment(string branchId, DateTime lastModifiedDt, DateTime serverDate);

        //DL TECHNICAL
        List<AppSettingInfo> DownloadAppSetting(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<FunctionInfo> DownloadFunction(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<RoleInfo> DownloadRole(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<FunctionServiceInfo> DownloadFunctionService(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<RoleFunctionInfo> DownloadRoleFunction(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<RoleUserInfo> DownloadRoleUser(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<ServiceInfo> DownloadService(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<Terminal> DownloadTerminal(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<UnlockLogInfo> DownloadUnlockLog(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<UserInfo> DownloadUser(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        List<UnlockRemarkInfo> DownloadUnlockRemark(string branchId, DateTime lastModifiedDate, DateTime serverDate);
        
        //UL MASTER
        int UploadMRUPlan(List<MRUPlanInfo> list, string branchId);

        //UL BILLING
        int UploadPrintPool(List<PrintPoolInfo> list, string branchId);
        int UploadGrpPrintPool(List<GrpPrintPoolInfo> list, string branchId);
        int UploadGreenReceiptDetail(List<GreenReceiptDetailInfo> list, string branchId);
        int UploadPrintHistory(List<PrintHistoryInfo> list, string branchId);
        int UploadDeliveryHistory(List<DeliveryHistoryInfo> list, string branchId);
        int UploadMaxBillSeqNo(List<MaxBillSeqNoInfo> list, string branchId);
        int UploadApprover(List<ApproverInfo> list, string branchId);
        int UploadDeliveryPlace(List<DeliveryPlaceInfo> list, string branchId);
        int UploadBarcodeMRU(List<BarcodeMRUInfo> list, string branchId);
        int UploadBillStatus(List<BillStatusInfo> list, string branchId);
        //int UploadGreenReceiptPrintHistory(List<GreenReceiptPrintHistoryInfo> list, string branchId);

        //UL CASH MANAGEMENT
        int UploadCashierWorkStatus(List<CashierWorkStatusInfo> list, string branchId);
        int UploadCashierMoneyTransfer(List<CashierMoneyTransferInfo> list, string branchId);
        int UploadCashierMoneyFlow(List<CashierMoneyFlowInfo> list, string branchId);
        int UploadCashierMoneyFlowItem(List<CashierMoneyFlowItemInfo> list, string branchId);

        //UL AR,PayFromSAP
        int UploadAR(List<AR> list, string branchId);
        int UploadAP(List<APInfo> list, string branchId);
        int UploadAPChequePayment(List<APChequePaymentInfo> list, string branchId);
        int UploadARPayment(List<ARPayment> list, string branchId);
        int UploadPayment(List<Payment> list, string branchId);
        int UploadARPaymentType(List<ARPaymentType> list, string branchId);
        int UploadRTARPaymentTypeARPayment(List<RTARPaymentTypeARPayment> list, string branchId);
        int UploadReceipt(List<Receipt> list, string branchId);
        int UploadReceiptItem(List<ReceiptItem> list, string branchId);
        int UploadPaymentLog(List<PaymentLogInfo> list, string branchId);

        //UL AGENCY
        int UploadBillBook(List<BillBookInfo> list, string branchId);
        int UploadBillStatusInfo(List<BillStatusInfoInfo> list, string branchId);
        int UploadBillBookDetail(List<BillBookDetailInfo> list, string branchId);
        int UploadBillBookInputItem(List<BillBookInputItemInfo> list, string branchId);
        int UploadBillBookInputSet(List<BillBookInputSetInfo> list, string branchId);
        int UploadAgencyCommission(List<AgencyCommissionInfo> list, string branchId);
        int UploadRTAgencyContractMru(List<RTAgencyContractMruInfo> list, string branchId);
        int UploadRTAgencyCommissionBillBook(List<RTAgencyCommissionBillBookInfo> list, string branchId);

        //UL EPAY
        int UploadEPayClarify(List<EPayClarifyInfo> list, string branchId);
        //int UploadEPayUpload(List<EPayUploadInfo> list, string branchId);
        //int UploadEPayUploadItem(List<EPayUploadItemInfo> list, string branchId);
        //int UploadRTEPayUploadPayment(List<RTEPayUploadPaymentInfo> list, string branchId);

        //UL TECHNICAL
        int UploadUnlockLog(List<UnlockLogInfo> list, string branchId);

        //UP EXPORT TABLE
        int UploadExportTransactionLog(List<ExportTransactionLogInfo> list, string branchId);

        //not used right now
        int UploadRoleUser(List<RoleUserInfo> list, string branchId);
        int UploadUser(List<UserInfo> list, string branchId);

        /// <summary>
        /// signal to export file to SAP
        /// </summary>
        /// <returns></returns>
        void SignalExport(string batchName, string branchId, string modifiedBy);

    }
}
