using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports
{
    [DataContract, Serializable]
    public class CAC19QRPaymentReport
    {
        public CAC19QRPaymentReport()
        {
        }
        

        public CAC19QRPaymentReport(string bankTransaction)
        {
            // Ref1. 
            this.QRRef1Bank = bankTransaction.Substring(84, 20);

            // Ref2.
            this.QRRef2Bank = bankTransaction.Substring(104, 20);

            // GAmount.
            decimal gAmount = 0;
            string amountString = bankTransaction.Substring(163, 13);
            if (decimal.TryParse(amountString, out gAmount))
                this.GAmountBank = (gAmount / 100);

            // BranchId 
            this.BranchID = this.QRRef1Bank.Substring(0, 6);

            // Transaction Type 
            this.TranSactionType = bankTransaction.Substring(152, 1);
            if (TranSactionType == "D")
                this.GAmountBank = this.GAmountBank * -1;
        }

        // Bank session. 
        private string _qrRef1Bank;
        private string _qrRef2Bank;
        private decimal? _gAmountBank;
        private string _branchId;
        private string _tranSactionType; 


        [DataMember(Order = 1)]
        public string QRRef1Bank
        {
            get { return _qrRef1Bank; }
            set { _qrRef1Bank = value; }
        }

        [DataMember(Order = 2)]
        public string QRRef2Bank
        {
            get { return _qrRef2Bank; }
            set { _qrRef2Bank = value; }
        }

        [DataMember(Order = 3)]
        public decimal? GAmountBank
        {
            get { return _gAmountBank; }
            set { _gAmountBank = value; }
        }

        [DataMember(Order = 4)]
        public string BranchID
        {
            get { return _branchId; }
            set { _branchId = value; }
        }

        [DataMember(Order = 5)]
        public string TranSactionType
        {
            get { return _tranSactionType; }
            set { _tranSactionType = value; }
        }



    }
}
