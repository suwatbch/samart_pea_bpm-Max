//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by the "Add Business Module" or "Add Foundational Module" recipe.
//
// For more information see: 
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.scsf.2006jun/SCSF/html/03-270-Creating%20a%20Module.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

namespace PEA.BPM.PaymentCollectionModule.Interface.Constants
{
    /// <summary>
    /// Constants for event topic names.
    /// </summary>
    public class EventTopicNames : PEA.BPM.Infrastructure.Interface.Constants.EventTopicNames
    {
        public const string ActionSuccess = "ActionSuccess";

        public const string PayItem = "PayItem";
        public const string InvoiceSearchedByCustomerId = "InvoiceSearchedByCustomerId";
        public const string InvoiceSearchedByGroupInvoiceNo = "InvoiceSearchedByGroupInvoiceNo";
        public const string InvoiceSearchedFromSAP = "InvoiceSearchedFromSAP";
        public const string InvoiceSearchedFromOneTouch = "InvoiceSearchedFromOneTouch";
        public const string BillSearchedByCustomerDetail = "BillSearchedByCustomerDetail";
        public const string BillSearchedByBillBookId = "BillSearchedByBillBookId";
        public const string BillSearchedByAgent = "BillSearchedByAgent";

        public const string InvoiceItemAdd = "InvoiceItemAdd";
        public const string InvoiceItemAddByMultiSearch = "InvoiceItemAddByMultiSearch"; // DCR 67-020 

        public const string PaymentItemAdd = "PaymentItemAdd";
        public const string ElectricPaymentItemAdd = "ElectricPaymentItemAdd";
        public const string ReMeterItemAdd = "ReMeterItemAdd";
        public const string NewPaymentItemAdd = "NewPaymentItemAdd";
        public const string OneTouchPaymentItemAdd = "OneTouchPaymentItemAdd";
        public const string InvoicePaymentMethodClick = "InvoicePaymentMethodClick";
        public const string PaymentItemSave = "PaymentItemSave";
        public const string PaymentMethodSave = "PaymentMethodSave";
        public const string ViewHistoryClick = "ViewHistoryClick";
        public const string ViewBillDetailClick = "ViewBillDetailClick";
        public const string ViewInvoiceDetailClick = "ViewInvoiceDetailClick";
        public const string PaidBillSearch = "PaidBillSearch";
        public const string ReceiptSelect = "ReceiptSelect";
        public const string ReceiptItemAdd = "ReceiptItemAdd";
        public const string CancellationClick = "CancellationClick";
        public const string ShowingChangeAmount = "ShowingChangeAmount";
        public const string FocusOnSearchById = "FocusOnSearchById";

        public const string ReceiptSearch = "ReceiptSearch";


        public const string PrintingOutputModify = "PrintingOutputModify";
        public const string DefaultOutputPrint = "DefaultOutputPrint";
        public const string PrintingTypeSet = "PrintingTypeSet";

        public const string AgAdvPaymentAdd = "AgAdvPaymentAdd";

        public const string CancelTransaction = "CancelTransaction";

        public const string SlipHeaderUpdate = "SlipHeaderUpdate";        

        public const string ShowReportClick = "ShowReportClick";
        public const string ShowReportCAC19Click = "ShowReportCAC19Click";

        public const string ShowStatusBar = "ShowStatusBar";
        public const string ShowGroupInvoicingReport = "ShowGroupInvoicingReport";
        public const string AccountPayablePayment = "AccountPayablePayment";

        public const string RepaymentSearch = "RepaymentSearch";
        public const string RepaymentItemAdd = "RepaymentItemAdd";

        public const string ClosePaymentView = "ClosePaymentView";
        
        /* Generate Receipt for Payment non receipt */
        public const string SearchPaymentNonReceipt = "SearchPaymentNonReceipt";

        public const string QRPaymentMethodClick = "QRPaymentMethodClick";

        // DCR 67-020 20241017 Serch invoice by multi CustomerId 
        public const string InvoiceSearchedByMultiCustomerId = "InvoiceSearchedByMultiCustomerId";
        public const string SearchResultByCaIdOnInvoice = "SearchResultByCaIdOnInvoice";
        

        //public const string BillSearchedByCustomerBillNo = "BillSearchedByCustomerBillNo";

        //public const string BillPaymentMethod = "BillPaymentMethod";
        //public const string BillPaymentPaidClicked = "BillPaymentPaidClicked";
        //public const string BillPaymentDetailClicked = "BillPaymentDetailClicked";
        //public const string BillPaymentListShow = "BillPaymentListShow";
        //public const string BillMultiplePaymentSearchClick = "BillMultiplePaymentSearchClick";
        //public const string BillPaymentPaidByCash = "BillPaymentPaidByCash";
        //public const string BillPaymentPaidByCQ = "BillPaymentPaidByCQ";
        //public const string BillPaymentPaidByDeposit = "BillPaymentPaidByDeposit";
        //public const string BillPaymentPaidByCreditCard = "BillPaymentPaidByCreditCard";

        //public const string BillAgencyPaidClicked = "BillAgencyPaidClicked";
        //public const string BillAgencyPaidByCash = "BillAgencyPaidByCash";
        //public const string BillAgencyPaidByCQ = "BillAgencyPaidByCQ";

        //public const string BillRetrunDetailClicked = "BillRetrunDetailClicked";

        
        //public const string GoToPayingListClicked = "GoToPayingListClicked";

        //public const string PayingListOkClicked = "PayingListOkClicked";

        //public const string PaymentMethodOkClicked = "PaymentMethodOkClicked";
    }
}
