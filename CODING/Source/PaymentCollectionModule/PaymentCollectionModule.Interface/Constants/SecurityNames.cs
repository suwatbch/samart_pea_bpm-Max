using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.Constants
{
    public class SecurityNames : PEA.BPM.Infrastructure.Interface.Constants.SecurityNames
    {
        public const string PaymentCollection = "P100";
        public const string UnlockPayment = "P101";
        public const string UnlockNotQuarterCash = "P102";
        public const string UnlockInterestPayment = "P103";
        public const string AgencyGroupInvoicingPaymentCollection = "P200";
        public const string ReceiptCancellation = "P300";
        public const string ReceiptCancellationNow = "P301";
        public const string ReceiptReprinting = "P500";
        public const string ReceiptReprintingNow = "P501";
        public const string InterestInquiry = "P600";
        public const string POSObserver = "P601";
        public const string AccountPayablePayment = "P700";
        public const string AccountPayableCancellation = "P800";
        public const string GenerateReceipt = "P900";

        public const string CAC01 = "PR01";
        public const string CAC03 = "PR03";
        public const string CAC04 = "PR04";
        public const string CAC05 = "PR05";
        public const string CAC06 = "PR06";
        public const string CAC07 = "PR07";
        public const string CAC09 = "PR09";
        public const string CAC10 = "PR10";
        public const string CAC11 = "PR11";
        public const string CAC12 = "PR12";
        public const string CAC13 = "PR13";
        public const string CAC14 = "PR14";
        public const string CAC15 = "PR15";
        public const string CAC20 = "PR20";
    }
}
