using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class OfflineItems
    {
        private DateTime? _paymentDt;
        private string _posId;
        private string _terminalCode;
        private string _branchId;
        private string _branchName;
        private decimal? _payingAmount;
        private string _screenType;
        private string _cashierId;
        private string _cashierName;
        private string _workId;
        private List<Invoice> _invoices = new List<Invoice>();
        //private List<ToBePaidBill> _bills;
        private List<PaymentMethod> _paymentMethods = new List<PaymentMethod>();
        private List<PrintingReceipt> _receipts = new List<PrintingReceipt>();
        private ExternalReceipt _extReceipt = new ExternalReceipt();
        private List<PrintingReceipt> _groupDividualReceipts = new List<PrintingReceipt>();


        [DataMember(Order=1)]
        public List<PrintingReceipt> GroupDividualReceipts
        {
            get { return _groupDividualReceipts; }
            set { _groupDividualReceipts = value; }
        }


        [DataMember(Order=2)]
        public ExternalReceipt ExtReceipt
        {
            get { return _extReceipt; }
            set { _extReceipt = value; }
        }


        [DataMember(Order=3)]
        public List<PrintingReceipt> Receipts
        {
            get { return _receipts; }
            set { _receipts = value; }
        }


        [DataMember(Order=4)]
        public DateTime? PaymentDt
        {
            get { return _paymentDt; }
            set { _paymentDt = value; }
        }


        [DataMember(Order=5)]
        public string PosId
        {
            get { return _posId; }
            set { _posId = value; }
        }


        [DataMember(Order = 6)]
        public string TerminalCode
        {
            get { return _terminalCode; }
            set { _terminalCode = value; }
        }


        [DataMember(Order=7)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }


        [DataMember(Order = 8)]
        public string BranchName
        {
            get { return _branchName; }
            set { _branchName = value; }
        }


        [DataMember(Order=9)]
        public decimal? PayingAmount
        {
            get { return _payingAmount; }
            set { _payingAmount = value; }
        }


        [DataMember(Order=10)]
        public string ScreenType
        {
            get { return _screenType; }
            set { _screenType = value; }
        }


        [DataMember(Order=11)]
        public string CashierId
        {
            get { return _cashierId; }
            set { _cashierId = value; }
        }


        [DataMember(Order = 12)]
        public string CashierName
        {
            get { return _cashierName; }
            set { _cashierName = value; }
        }


        [DataMember(Order = 13)]
        public string WorkId
        {
            get { return _workId; }
            set { _workId = value; }
        }

        [DataMember(Order=14)]
        public List<Invoice> Invoices
        {
            get { return _invoices; }
            set { _invoices = value; }
        }

        //public List<ToBePaidBill> Bills
        //{
        //    get { return _bills; }
        //    set { _bills = value; }
        //}


        [DataMember(Order=15)]
        public List<PaymentMethod> PaymentMethods
        {
            get { return _paymentMethods; }
            set { _paymentMethods = value; }
        }


    }
}
