using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CAB02_DetailReportInfo
    {
        private int _rowId;
        private int _seqAegncy;
        private string _branchId;
        private string _agentId;
        private string _caName;
        private string _bookperiod;
        private string _advPayDueDate;
        private int? _receiveCount;
        private int? _bookLot;
        private int? _totalBill;
        private decimal? _totalelective = 0;
        private decimal? _advpayamount = 0;
        private string _chqPaymentDate;
        private string _transPaymentDate;
        private string _cashPaymentDate;
        private string _tranfAccNo;
        private decimal? _tranfAmount = 0;
        private string _chqNo;
        private decimal? _chqAmount = 0;
        private decimal? _cashamount = 0;
        private decimal? _balance = 0;
        private decimal? _sumBalance = 0;
        private string _receiptId;
        private decimal? _paymentBL = 0;
        private string _billbookId;
        private int? _paymentCount = 0;


        [DataMember(Order=1)]
        public int RowId
        {
            get { return this._rowId; }
            set { this._rowId = value; }
        }


        [DataMember(Order=2)]
        public int? PaymentCount
        {
            get { return this._paymentCount; }
            set { this._paymentCount = value; }
        }


        [DataMember(Order=3)]
        public int SeqAgency
        {

            get { return this._seqAegncy; }
            set { this._seqAegncy = value; }

        }


        [DataMember(Order=4)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }


        [DataMember(Order=5)]
        public string AgentId
        {
            get { return this._agentId; }
            set { this._agentId = value; }
        }

        [DataMember(Order=6)]
        public string CaName
        {
            get { return this._caName; }
            set { this._caName = value; }
        }

        [DataMember(Order=7)]
        public string Bookperiod
        {
            get { return this._bookperiod; }
            set { this._bookperiod = value; }
        }

        [DataMember(Order=8)]
        public string AdvPayDueDate
        {
            get { return this._advPayDueDate; }
            set { this._advPayDueDate = value; }
        }

        [DataMember(Order=9)]
        public int? ReceiveCount
        {
            get { return this._receiveCount; }
            set { this._receiveCount = value; }
        }

        [DataMember(Order=10)]
        public int? BookLot
        {
            get { return this._bookLot; }
            set { this._bookLot = value; }
        }

        [DataMember(Order=11)]
        public int? TotalBill
        {
            get { return this._totalBill; }
            set { this._totalBill = value; }
        }

        [DataMember(Order=12)]
        public decimal? TotalElective
        {
            get { return this._totalelective; }
            set { this._totalelective = value; }
        }

        [DataMember(Order=13)]
        public decimal? AdvpayAmount
        {
            get { return this._advpayamount; }
            set { this._advpayamount = value; }
        }

        [DataMember(Order=14)]
        public string ChqPaymentDate
        {
            get { return this._chqPaymentDate; }
            set { this._chqPaymentDate = value; }
        }

        [DataMember(Order=15)]
        public string TransPaymentDate
        {
            get { return this._transPaymentDate; }
            set { this._transPaymentDate = value; }
        }

        [DataMember(Order=16)]
        public string CashPaymentDate
        {
            get { return this._cashPaymentDate; }
            set { this._cashPaymentDate = value; }
        }

        [DataMember(Order=17)]
        public string TranfAccNo
        {
            get { return this._tranfAccNo; }
            set { this._tranfAccNo = value; }
        }

        [DataMember(Order=18)]
        public decimal? TranfAmount
        {
            get { return this._tranfAmount; }
            set { this._tranfAmount = value; }
        }

        [DataMember(Order=19)]
        public string ChqNo
        {
            get { return this._chqNo; }
            set { this._chqNo = value; }
        }

        [DataMember(Order=20)]
        public decimal? ChqAmount
        {
            get { return this._chqAmount; }
            set { this._chqAmount = value; }
        }

        [DataMember(Order=21)]
        public decimal? CashAmount
        {
            get { return this._cashamount; }
            set { this._cashamount = value; }
        }

        //Checked TongKung
        //[DataMember(Order=22)]
        public decimal? TotalAmount
        {
            get { return this._chqAmount + CashAmount + TranfAmount; }
        }

        [DataMember(Order=23)]
        public decimal? Balance
        {
            get { return this._balance; }
            set { this._balance = value; }
        }


        [DataMember(Order=24)]
        public decimal? SumBalance
        {
            get { return this._sumBalance; }
            set { this._sumBalance = value; }
        }


        [DataMember(Order=25)]
        public string ReceiptId
        {
            get { return this._receiptId; }
            set { this._receiptId = value; }
        }


        [DataMember(Order=26)]
        public decimal? PaymentBL
        {
            get { return this._paymentBL; }
            set { this._paymentBL = value; }
        }


        [DataMember(Order=27)]
        public string BillbookId
        {
            get { return this._billbookId; }
            set { this._billbookId = value; }
        }
    }
}
