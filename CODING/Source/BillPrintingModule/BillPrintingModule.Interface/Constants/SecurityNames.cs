using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.BillPrintingModule.Interface.Constants
{
    public class SecurityNames : PEA.BPM.Infrastructure.Interface.Constants.SecurityNames
    {
        public const string DualBillPrinting = "B100";
        public const string DualBillReprinting = "B101";
        public const string A4BillPrinting = "B102";
        public const string GroupInvoicePrinting = "B200";
        public const string GroupInvoiceReprinting = "B201";
        public const string DirectDebitByBankPrinting = "B300";
        public const string DirectDebitByBankRePrinting = "B301";
        public const string GreenReceipt = "B600";
        public const string GreenReceiptReprint = "B601";

        //repoorting 
        public const string StreetRouteReport = "B400";
        public const string StreetRouteUnreceiveReport = "B401";
        public const string StreetRouteReceiveReport = "B402";
        public const string DailyPrintReport = "B403";
        public const string DailyUnprintReport = "B404";
        public const string BillDeliveryReport = "B405";
        public const string F16Report = "B406";
        public const string GrpInvSummaryReport = "B407";
        public const string PrintBankGreenBillReport = "B408";
        public const string GroupingCrossCheckReport = "B409";
        public const string BillingStatusReport = "B410";

        //configure 
        public const string DeliveryPlace = "B500";
        public const string AuthorizedPerson = "B501";
        public const string PrintTargetLocation = "B502";
        public const string ChangePrintType = "B504";
        
        //new added by tong_kung 01/07/2551
        public const string ManageBarcode = "B503";

    }
}
