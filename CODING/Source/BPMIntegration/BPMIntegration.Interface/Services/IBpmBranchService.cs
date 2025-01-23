using System;

using System.Collections.Generic;
using System.Text;
using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;
using System.Data.Common;

namespace PEA.BPM.Integration.BPMIntegration.Interface.Services
{
    public interface IBpmBranchService
    {
        //isolate
        bool UpdateAccountClass(List<AccountClassInfo> list, ACABatchParam param);
        bool UpdateContractType(List<ContractTypeInfo> list, ACABatchParam param);
        bool UpdateMeterSize(List<MeterSizeInfo> list, ACABatchParam param);
        bool UpdatePaymentMethod(List<PaymentMethodInfo> list, ACABatchParam param);
        bool UpdateTaxCode(List<TaxCodeInfo> list, ACABatchParam param);
        bool UpdateUnitType(List<UnitTypeInfo> list, ACABatchParam param);
        bool UpdateBusinessPartnerType(List<BusinessPartnerTypeInfo> list, ACABatchParam param);        
        bool UpdatePaymentType(List<PaymentTypeInfo> list, ACABatchParam param);
        bool UpdateInterestKey(List<InterestKeyInfo> list, ACABatchParam param);
        bool UpdateAgencyBillCollectionStatus(List<AgencyBillCollectionStatusInfo> list, ACABatchParam param);
        bool UpdateAgencyBillOutStatus(List<AgencyBillOutStatusInfo> list, ACABatchParam param);
        bool UpdateAgencyCommissionRate(List<AgencyCommissionRateInfo> list, ACABatchParam param);
        bool UpdateBillBookStatus(List<BillBookStatusInfo> list, ACABatchParam param);
        bool UpdateAgencyCollectArea(List<AgencyCollectAreaInfo> list, ACABatchParam param);
        bool UpdateDisconnectionStatus(List<DisconnectionStatusInfo> list, ACABatchParam param);
        bool UpdateCashierMoneyFlowType(List<CashierMoneyFlowTypeInfo> list, ACABatchParam param);
        bool UpdateOwnBank(List<OwnBankInfo> list, ACABatchParam param);

        //master
        bool UpdateNonworkingDay(List<NonWorkingDayInfo> list, ACABatchParam param);
        bool UpdateCalendar(List<CalendarInfo> list, ACABatchParam param);
        bool UpdateBusinessPartner(List<BusinessPartnerInfo> list, ACABatchParam param);
        bool UpdateBranch(List<BranchInfo> list, ACABatchParam param);
        bool UpdateMRU(List<MRUInfo> list, ACABatchParam param);
        bool UpdateMRUPlan(List<MRUPlanInfo> list, ACABatchParam param);
        bool UpdateContractAccount(List<ContractAccountInfo> list, ACABatchParam param);
        bool UpdateEmployee(List<EmployeeInfo> list, ACABatchParam param);
        bool UpdateAgencyAsset(List<AgencyAssetInfo> list, ACABatchParam param);
        bool UpdateBank(List<BankInfo> list, ACABatchParam param);
        bool UpdateBankAccount(List<BankAccountInfo> list, ACABatchParam param);
        bool UpdateDebtType(List<MainSubInfo> list, ACABatchParam param);
        bool UpdatePaymentSequence(List<PaymentSequenceInfo> list, ACABatchParam param);
        bool UpdateOldCaMapping(List<OldCaMappingInfo> list, ACABatchParam param);

        //billing
        bool UpdatePrintPool(List<PrintPoolInfo> list, ACABatchParam param);
        bool UpdateGrpPrintPool(List<GrpPrintPoolInfo> list, ACABatchParam param);
        bool UpdateGreenReceiptDetail(List<GreenReceiptDetailInfo> list, ACABatchParam param);
        //bool UpdateGreenReceiptPrintHistory(List<GreenReceiptPrintHistoryInfo> list, ACABatchParam param);
        bool UpdateBillingDetail(List<BillingDetailInfo> list, ACABatchParam param);
        bool UpdatePrintHistory(List<PrintHistoryInfo> list, ACABatchParam param);
        bool UpdateDeliveryHistory(List<DeliveryHistoryInfo> list, ACABatchParam param);
        bool UpdateMaxBillSeqNo(List<MaxBillSeqNoInfo> list, ACABatchParam param);
        bool UpdateApprover(List<ApproverInfo> list, ACABatchParam param);
        bool UpdateDeliveryPlace(List<DeliveryPlaceInfo> list, ACABatchParam param);
        bool UpdatePWBBillStatus(List<PWBBillStatusInfo> list, ACABatchParam param);
        bool UpdateBarcodeMRU(List<BarcodeMRUInfo> list, ACABatchParam param);
        bool UpdateBillStatus(List<BillStatusInfo> list, ACABatchParam param);

        //ar
        bool UpdateAR(List<AR> list, ACABatchParam param);
        bool UpdateDisconnectionDoc(List<DisconnectionDocInfo> list, ACABatchParam param);
        bool UpdateRTDisconnectionDocCaDoc(List<RTDisconnectionDocCaDocInfo> list, ACABatchParam param);
        bool UpdateAP(List<APInfo> list, ACABatchParam param);
        bool UpdateAPChequePayment(List<APChequePaymentInfo> list, ACABatchParam param);


        //payFromSap
        bool UpdateARPayment(List<ARPayment> list, ACABatchParam param);
        bool UpdatePayment(List<Payment> list, ACABatchParam param);
        bool UpdateARPaymentType(List<ARPaymentType> list, ACABatchParam param);
        bool UpdateRTARPaymentTypeARPayment(List<RTARPaymentTypeARPayment> list, ACABatchParam param);
        bool UpdateReceipt(List<Receipt> list, ACABatchParam param);
        bool UpdateReceiptItem(List<ReceiptItem> list, ACABatchParam param);
        
        //cash management
        bool UpdateCashierWorkStatus(List<CashierWorkStatusInfo> list, ACABatchParam param);
        bool UpdateCashierMoneyTransfer(List<CashierMoneyTransferInfo> list, ACABatchParam param);
        bool UpdateCashierMoneyFlow(List<CashierMoneyFlowInfo> list, ACABatchParam param);
        bool UpdateCashierMoneyFlowItem(List<CashierMoneyFlowItemInfo> list, ACABatchParam param);


        //agency
        bool UpdateBillBook(List<BillBookInfo> list, ACABatchParam param);
        bool UpdateBillStatusInfo(List<BillStatusInfoInfo> list, ACABatchParam param);
        bool UpdateBillBookDetail(List<BillBookDetailInfo> list, ACABatchParam param);
        bool UpdateBillBookInputItem(List<BillBookInputItemInfo> list, ACABatchParam param);
        bool UpdateBillBookInputSet(List<BillBookInputSetInfo> list, ACABatchParam param);
        bool UpdateAgencyCommission(List<AgencyCommissionInfo> list, ACABatchParam param);
        bool UpdateRTAgencyContractMru(List<RTAgencyContractMruInfo> list, ACABatchParam param);
        bool UpdateRTAgencyCommissionBillBook(List<RTAgencyCommissionBillBookInfo> list, ACABatchParam param);
        //bool UpdateAgencyModuleConfig(List<AgencyModuleConfigInfo> list, ACABatchParam param);

        //e-payment
        bool UpdateEPayClarify(List<EPayClarifyInfo> list, ACABatchParam param);
        bool UpdateEPayUpload(List<EPayUploadInfo> list, ACABatchParam param);
        bool UpdateEPayUploadItem(List<EPayUploadItemInfo> list, ACABatchParam param);
        bool UpdateRTEPayUploadPayment(List<RTEPayUploadPaymentInfo> list, ACABatchParam param);

        //technical
        bool UpdateService(List<ServiceInfo> list, ACABatchParam param);
        bool UpdateFunction(List<FunctionInfo> list, ACABatchParam param);
        bool UpdateRole(List<RoleInfo> list, ACABatchParam param);
        bool UpdateUser(List<UserInfo> list, ACABatchParam param);
        bool UpdateFunctionService(List<FunctionServiceInfo> list, ACABatchParam param);
        bool UpdateRoleFunction(List<RoleFunctionInfo> list, ACABatchParam param);
        bool UpdateRoleUser(List<RoleUserInfo> list, ACABatchParam param);
        bool UpdateUnlockLog(List<UnlockLogInfo> list, ACABatchParam param);
        bool UpdateTerminal(List<Terminal> list, ACABatchParam param);
        bool UpdateAppSetting(List<AppSettingInfo> list, ACABatchParam param);
        bool UpdateUnlockRemark(List<UnlockRemarkInfo> list, ACABatchParam param);

        List<MRUPlanInfo> GetToUploadMRUPlan(DateTime lastModifiedDate);

        List<PrintPoolInfo> GetToUploadPrintPool(DateTime lastModifiedDate);
        List<GrpPrintPoolInfo> GetToUploadGrpPrintPool(DateTime lastModifiedDate);
        List<GreenReceiptDetailInfo> GetToUploadGreenReceiptDetail(DateTime lastModifiedDate);
        //List<GreenReceiptPrintHistoryInfo> GetToUploadGreenReceiptPrintHistory(DateTime lastModifiedDate);
        List<PrintHistoryInfo> GetToUploadPrintHistory(DateTime lastModifiedDate);
        List<DeliveryHistoryInfo> GetToUploadDeliveryHistory(DateTime lastModifiedDate);
        List<MaxBillSeqNoInfo> GetToUploadMaxBillSeqNo(DateTime lastModifiedDate);
        List<ApproverInfo> GetToUploadApprover(DateTime lastModifiedDate);
        List<DeliveryPlaceInfo> GetToUploadDeliveryPlace(DateTime lastModifiedDate);
        List<BarcodeMRUInfo> GetToUploadBarcodeMRU(DateTime lastModifiedDate);
        List<BillStatusInfo> GetToUploadBillStatus(DateTime lastModifiedDate);

        List<AR> GetToUploadAR(DateTime lastModifiedDate);
        List<APInfo> GetToUploadAP(DateTime lastModifiedDate);
        List<APChequePaymentInfo> GetToUploadAPChequePayment(DateTime lastModifiedDate);
        List<ARPayment> GetToUploadARPayment(DateTime lastModifiedDate);
        List<Payment> GetToUploadPayment(DateTime lastModifiedDate);
        List<ARPaymentType> GetToUploadARPaymentType(DateTime lastModifiedDate);
        List<RTARPaymentTypeARPayment> GetToUploadRTARPaymentTypeARPayment(DateTime lastModifiedDate);
        List<Receipt> GetToUploadReceipt(DateTime lastModifiedDate);
        List<ReceiptItem> GetToUploadReceiptItem(DateTime lastModifiedDate);
        List<PaymentLogInfo> GetToUploadPaymentLog(DateTime lastModifiedDate);

        List<BillBookInfo> GetToUploadBillBook(DateTime lastModifiedDate);
        List<BillStatusInfoInfo> GetToUploadBillStatusInfo(DateTime lastModifiedDate);
        List<BillBookDetailInfo> GetToUploadBillBookDetail(DateTime lastModifiedDate);
        List<BillBookInputItemInfo> GetToUploadBillBookInputItem(DateTime lastModifiedDate);
        List<BillBookInputSetInfo> GetToUploadBillBookInputSet(DateTime lastModifiedDate);
        List<AgencyCommissionInfo> GetToUploadAgencyCommission(DateTime lastModifiedDate);
        List<RTAgencyContractMruInfo> GetToUploadRTAgencyContractMru(DateTime lastModifiedDate);
        List<RTAgencyCommissionBillBookInfo> GetToUploadRTAgencyCommissionBillBook(DateTime lastModifiedDate);

        List<EPayClarifyInfo> GetToUploadEPayClarify(DateTime lastModifiedDate);
        //List<EPayUploadInfo> GetToUploadEPayUpload(DateTime lastModifiedDate);
        //List<EPayUploadItemInfo> GetToUploadEPayUploadItem(DateTime lastModifiedDate);
        //List<RTEPayUploadPaymentInfo> GetToUploadRTEPayUploadPayment(DateTime lastModifiedDate);

        List<CashierWorkStatusInfo> GetToUploadCashierWorkStatus(DateTime lastModifiedDate);
        List<CashierMoneyTransferInfo> GetToUploadCashierMoneyTransfer(DateTime lastModifiedDate);
        List<CashierMoneyFlowInfo> GetToUploadCashierMoneyFlow(DateTime lastModifiedDate);
        List<CashierMoneyFlowItemInfo> GetToUploadCashierMoneyFlowItem(DateTime lastModifiedDate);

        List<UserInfo> GetToUploadUser(DateTime lastModifiedDate);
        List<RoleUserInfo> GetToUploadRoleUser(DateTime lastModifiedDate);
        List<UnlockLogInfo> GetToUploadUnlockLog(DateTime lastModifiedDate);

        List<ExportTransactionLogInfo> GetToUploadExportTransactionLog(DateTime lastModifiedDate);

        int SetSyncBillingDetail(DateTime lastModifiedDate);
        int SetSyncPrintHistory(DateTime lastModifiedDate);
        int SetSyncDeliveryHistory(DateTime lastModifiedDate);
        int SetSyncApprover(DateTime lastModifiedDate);
        int SetSyncDeliveryPlace(DateTime lastModifiedDate);
        int SetSyncPrintPool(DateTime lastModifiedDate);
        int SetSyncGrpPrintPool(DateTime lastModifiedDate);
        int SetSyncMaxBillSeqNo(DateTime lastModifiedDate);
        int SetSyncGreenReceiptDetail(DateTime lastModifiedDate);
        //int SetSyncGreenReceiptPrintHistory(DateTime lastModifiedDate);
        int SetSyncBarcodeMRU(DateTime lastModifiedDate);
        int SetSyncBillStatus(DateTime lastModifiedDate);

        int SetSyncAR(DateTime lastModifiedDate);
        int SetSyncAP(DateTime lastModifiedDate);
        int SetSyncAPChequePayment(DateTime lastModifiedDate);
        int SetSyncARPayment(DateTime lastModifiedDate);
        int SetSyncPayment(DateTime lastModifiedDate);
        int SetSyncARPaymentType(DateTime lastModifiedDate);
        int SetSyncRTARPaymentTypeARPayment(DateTime lastModifiedDate);
        int SetSyncReceipt(DateTime lastModifiedDate);
        int SetSyncReceiptItem(DateTime lastModifiedDate);
        int SetSyncPaymentLog(DateTime lastModifiedDate);

        int SetSyncCashierWorkStatus(DateTime lastModifiedDate);
        int SetSyncCashierMoneyTransfer(DateTime lastModifiedDate);
        int SetSyncCashierMoneyFlow(DateTime lastModifiedDate);
        int SetSyncCashierMoneyFlowItem(DateTime lastModifiedDate);

        int SetSyncBillBook(DateTime lastModifiedDate);
        int SetSyncBillStatusInfo(DateTime lastModifiedDate);
        int SetSyncBillBookDetail(DateTime lastModifiedDate);
        int SetSyncBillBookInputItem(DateTime lastModifiedDate);
        int SetSyncBillBookInputSet(DateTime lastModifiedDate);
        int SetSyncAgencyCommission(DateTime lastModifiedDate);
        int SetSyncRTAgencyContractMru(DateTime lastModifiedDate);
        int SetSyncRTAgencyCommissionBillBook(DateTime lastModifiedDate);

        int SetSyncRoleUser(DateTime lastModifiedDate);
        int SetSyncUser(DateTime lastModifiedDate);
        int SetSyncUnlockLog(DateTime lastModifiedDate);

        int SetSyncEPayClarify(DateTime lastModifiedDate);
        //int SetSyncEPayUpload(DateTime lastModifiedDate);
        //int SetSyncEPayUploadItem(DateTime lastModifiedDate);
        //int SetSyncRTEPayUploadPayment(DateTime lastModifiedDate);

        int SetSyncExportTransactionLog(DateTime lastModifiedDate);

        int SetSyncMRUPlan(DateTime lastModifiedDate);

    }
}
