using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.CashManagementModule.Interface.Constants
{
    public class SecurityNames : PEA.BPM.Infrastructure.Interface.Constants.SecurityNames
    {
        //Important! these functions will be automatically enabled by using POS permission.
        public const string PaymentCollection = "P100";
        public const string TransferManagement = "P400";
        public const string ForceTransferManagement = "P401";
        public const string CancelBankDelivery = "P402";
        public const string MoneyCheckIn = "P403";
        public const string CancelCashCheckingIn = "P404";
        public const string ForceCloseWork = "P405";
        public const string StartOpenBalance = "P406";
        //use POSObserver instead
        //public const string Observer = "P407";
        public const string AdjustOpenBalance = "P408";
        public const string DailyRemainReport = "PR40";
        public const string MoneyFlowReport = "PR41";
        public const string DailyPayInReport = "PR42";
        public const string SummaryCloseWork = "PR43";

    }
}
