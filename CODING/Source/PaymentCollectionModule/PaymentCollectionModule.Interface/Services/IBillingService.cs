using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports;
using System.Data;
using System.Data.Common;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;

namespace PEA.BPM.PaymentCollectionModule.Interface.Services
{
    public interface IBillingService
    {
        List<Invoice> SearchInvoiceByCustomerId(CustomerSearchParam param);
        List<Invoice> SearchInvoiceFromSAP(SAPSearchParam param);
        Invoice SearchOriginalInvoiceByInstallmentItemCaDoc(OriginalInvoiceSearchParam param);
        List<InstallmentInvoice> SearchInstallmentInvoice(string caDoc);
        List<Invoice> SearchInvoiceByGroupInvoiceNo(GroupInvoiceSearchParam param);
        List<Invoice> SearchInvoiceItemByGroupInvoiceNo(InvoiceItemSearchParam param);
        List<BillSearchDetail> SearchBillByCustomerDetail(CustomerSearchParam param);
        List<Bill> SearchBillByBillBookId(string billBookId, string billBookStatus);
        List<BookSearchDetail> SearchBillByAgent(AgencySearchParam param);
        List<GroupInvoiceItem> GetGroupInvoiceItem(string billBookId);

        bool CheckExistingInvoiceNo(string caId, string period);
        
        CaIdAndBranchId GetBranchIdByCaId(String caId);

        Customer GetCustomerDetailOnPaymentHistory(HistoryViewParam param);
        List<PaidInvoice> GetPaymentHistory(HistoryViewParam param);
        ResultPayInvoice PayInvoice(DbTransaction trans, List<Invoice> paidInvoices, List<PaymentMethod> paymentMethods,
            List<PrintingReceipt> receipts, List<PrintingReceipt> groupDividualReceipts, ExternalReceipt extReceipt,
            DateTime paymentDate, string posId, string terminalCode, string branchId, string branchName, decimal? payingAmount, 
            string screenType, string cashierId, string cashierName, string workId, bool isOffline);
        List<BillBook> SearchBillBookByDetail(string billBookId, string agencyId, string agencyName);
        BillBook GetBillBookDetail(string billBookId, string statusLineStr);        
        List<AdvancePaymentHistory> SearchAdvancePaymentHistory(string billBookId);
        List<PaymentMethod> SearchAGPayment(string billBookId);
        DayClosingResult CloseDayPayment(string branchId);

        List<DisconnectionDoc> SearchDisconnectionStatusByDiscNo(string discNo);
        DateTime GetServerTime();
        LastDisconnect GetLastDisconnect(string caId);

        List<PaymentNonReceiptInfo> SearchPaymentNonReceipt(PaymentNonReceiptInfo receiptGen);
        void CreateReceiptService(List<PaymentNonReceiptInfo> paymentGenReList);

        List<CaAndBpInfo> GetCaAndBpOtherBranch(string caId);
        void SaveCaAndBpOtherBranch(List<CaAndBpInfo> list);

        bool GetPaidGAmount(string invoiceNo);
        bool GetInActiveBillBook(string invoiceNo);
        bool GetDuplicateExtReceipt(string receiptId,string branchId);
        void OfflineLog(OfflineLogInfo logInfo);
        ActivePayment GetActivePayment(SAPSearchParam param, bool renew);
        void SaveActivePayment(ActivePayment acp);
        string CheckWorkStatus(OpenWorkParam param);
        TrayMoneyInfo GetMoneyInTray(string workId);

        bool ValidatePaymentActive(string receiptId, bool isPayment);

        List<LastReceiptPayment> GetRepayLastReceiptData(string receiptId);

        List<Invoice> SearchInvoiceFromOneTouch(OneTouchSearchParam param);
        bool FlagOneTouchPayment(OneTouchLogInfo param);
        void SaveOneTouchLog(OneTouchLogInfo log);

        List<OfflinePayment> GetOfflinePayment(string branchId, string cashierId, string workId);
        void UpdateOfflinePayment(string branchId, string cashierId, string PosId, string workId);

        //// รวมใบเสร็จแผนผ่อน 2021-10-07 Check ใบเสร็จรวม Enable Status from ta.AppSetting
        string CheckSettingGroupReceiptEnableStatus();


        /// <summary>
        /// DCR QR Payment for 2023-08
        /// Check status QR Payment
        /// </summary>
        /// <returns> </returns>
        QRPaymentResponse CheckStatusQRPayment(QRPaymentInfo qrpaymentInfo);

        QRRefundResponse QRPaymentRefund(QRPaymentInfo qrpaymentInfo);

        QRRefundResponse QRPostOfflinePayment(QRPaymentInfo qrpaymentInfo);

        string QRPaymentMethodByBranchId(string branchId);

    }
}
