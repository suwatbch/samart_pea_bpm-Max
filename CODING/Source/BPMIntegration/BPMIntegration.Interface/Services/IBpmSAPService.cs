using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;
using PEA.BPM.Integration.BPMIntegration.Interface.ExportEntities;
using PEA.BPM.Integration.BPMIntegration.Interface.Constants;

namespace PEA.BPM.Integration.BPMIntegration.Interface.Services
{
    public interface IBpmSAPService
    {

        #region *** IMPORT
        //DL01
        bool Import_NonWorkingDay(List<NonWorkingDayInfo> impList, ACABatchParam param);
        bool Import_AccountClass(List<AccountClassInfo> impList, ACABatchParam param);
        bool Import_ContractType(List<ContractTypeInfo> impList, ACABatchParam param);
        bool Import_MeterSizeType(List<MeterSizeInfo> impList, ACABatchParam param);
        bool Import_PaymentMethod(List<PaymentMethodInfo> impList, ACABatchParam param);
        bool Import_TaxCode(List<TaxCodeInfo> impList, ACABatchParam param);
        bool Import_UnitType(List<UnitTypeInfo> impList, ACABatchParam param);

        //DL02
        bool Import_BusinessPartner(List<BusinessPartnerInfo> impList, ACABatchParam param, ref bool skipError, string fileType);
        bool Import_ContractAccount(List<ContractAccountInfo> impList, ACABatchParam param, ref bool skipError, string fileType);
        bool Import_Branch(List<BranchInfo> impList, ACABatchParam param, ref bool skipError);
        bool Import_MRU(List<MRUInfo> impList, ACABatchParam param, ref bool skipError);
        bool Import_MRUPlan(List<MRUPlanInfo> impList, ACABatchParam param, ref bool skipError);
        bool Import_Employee(List<EmployeeInfo> impList, ACABatchParam param, ref bool skipError);
        bool Import_AgencyAsset(List<AgencyAssetInfo> impList, ACABatchParam param, ref bool skipError);
        bool Import_Bank(List<BankInfo> impList, ACABatchParam param, ref bool skipError);
        bool Import_BankAccount(List<BankAccountInfo> impList, ACABatchParam param, ref bool skipError);
        bool Import_MainSub(List<MainSubInfo> impList, ACABatchParam param, ref bool skipError);
        bool Import_Company(List<CompanyInfo> impList, ACABatchParam param, ref bool skipError);
        bool Import_MRUPlanByBulk(ACABatchParam param);
        bool Import_CABulk(ACABatchParam param);
        bool Import_ControllerBulk(ACABatchParam param);

        //DL03
        int Import_BillingData(ACABatchParam param);

        //DL04
        bool Import_AR(ACABatchParam param, string fileType);
        bool Import_ARElectric(ACABatchParam param);
        bool Import_DisconnectionDoc(List<DisconnectionDocInfo> impList, ACABatchParam param);
        bool Import_RTDisconnectionDocCaDoc(List<RTDisconnectionDocCaDocInfo> impList, ACABatchParam param);
        bool Import_DisconnectionStatus(List<DisconnectionStatus> impList, ACABatchParam param);
        bool Import_CancelBPMPayment(ACABatchParam param);
        bool Import_CancelBPMPayment_50A(ACABatchParam param);
        bool Import_ARSubGroupInvoice(ACABatchParam param);
        bool Import_ARDisconnect(ACABatchParam param);

        //DL5
        bool Import_PayFromSAP(ACABatchParam param, string fileType);

        //DL06 - DIRECT DEBIT
        bool Import_BillingDetailStatus(List<BillingDetailStatusInfo> impList, ACABatchParam param);
        bool Import_BillingReverse(List<BillingReverseInfo> impList, ACABatchParam param);
        bool Import_DirectDebit(ACABatchParam param);
        bool Import_Receiptx(ACABatchParam param);
        bool Import_PWBBillStatus(ACABatchParam param);

        #endregion

        #region *** EXPORT
        //21.2.2013
        //Support Auto export
        void MarkBranchPrefix();
        void ProcessExportDone();
        bool CheckAnotherRegionalToExport();

        //*** DONE flag
        void SetSync_AgencyCompensation(SetSyncParam param);
        void SetSync_ARNormal(SetSyncParam param);
        void SetSync_ARSpotBill(SetSyncParam param);
        void SetSync_ARGroupInvoice(SetSyncParam param);
        void SetSync_ARPaidByAgency(SetSyncParam param);
        void SetSync_ARPartialPayment(SetSyncParam param);

        void SetSync_ARNonInvNormal(SetSyncParam param);
        void SetSync_ARNonInvCashSale(SetSyncParam param);
        void SetSync_ARNonInvAdvancePayment(SetSyncParam param);
        void SetSync_APPayment(SetSyncParam param);
        void SetSync_ARDepositReceipt(SetSyncParam param);
        void SetSync_APVoucher(SetSyncParam param);
        void SetSync_APBankPayIn(SetSyncParam param);
        string GetExportFileName(string interfaceName);
        string GetExportFileNameWithoutExtension(string interfaceName);
        string GetExportFileNameTimestamp(string interfaceName, string billBookId);

        List<AG_Compensation> Export_AgencyCompensation(string branchId, ACABatchParam param);
        List<AG_BillBook> GetBillBookForExport();
        List<AG_BillBookInvoice> Export_BillBookInvoice(string billBookId, ACABatchParam param);
        List<AR_Normal> Export_ARNormal(string branchId);
        List<AR_SpotBill> Export_ARSpotBill(string branchId);
        List<AR_GrpInv> Export_ARGroupInvoice(string branchId);
        List<AR_Agency> Export_ARPaidByAgency(string branchId);
        List<AR_PartialPay> Export_ARPartialPayment(string branchId);

        List<ARn_Normal> Export_ARNonInvNormal(string branchId);
        List<ARn_CashSale> Export_ARNonInvCashSale(string branchId);
        List<ARn_AdvPayment> Export_ARNonInvAdvancePayment(string branchId);
        List<ARn_APPayment> Export_APPayment(string branchId);
        List<AR_DepositReceipt> Export_ARDepositReceipt(string branchId);
        List<AP_Payment> Export_APVoucher(string branchId);
        List<AP_BankPayIn> Export_APBankPayIn(string branchId);
        List<AR_Normal> Export_MPayARNormal(string branchId);
        List<ARn_AdvPayment> Export_MPayARNonInvAdvancePayment(string branchId);
        List<AR_DepositReceipt> Export_MPayARDepositReceipt(string branchId);
        List<ARn_APPayment> Export_APChequePayment(string branchId);
        List<AR_Reconnect> Export_ARReconnect(string branchId);

        List<BillInfo> GetReconnection(string BranchId, DateTime fromDate, DateTime toDate);

        #endregion

        #region *** Clear data for spot bill case

        void ClearSpotBillCase();

        #endregion

        #region *** Clear data for spot bill case

        void DecreaseInterfaceRunningNumber(string interfaceNumber);

        #endregion

        #region *** Manage branchId for exporting payment data to SAP

        List<String> GetListOfBranchIdForExportingData();   
        void ClearListOfBranchIdForExportingData(List<String> branchIdList);

        #endregion

        #region *** Other
        DateTime GetDBDateTime();
        String GetDBDateTime(string format);
        #endregion
    }
}
