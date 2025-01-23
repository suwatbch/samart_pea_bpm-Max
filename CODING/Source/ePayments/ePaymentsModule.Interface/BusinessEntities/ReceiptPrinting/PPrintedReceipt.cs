using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities.ReceiptPrinting
{
    [DataContract]
    public class PPrintedReceipt
    {
        private string _receiptId;
        private string _receiptName;
        private string _receiptBranch;
        private string _receiptAddr;
        private string _receiptDt;
        private string _receiptMonth;
        private string _receiptYear;
        private string _compName;
        private string _compAddr;
        private string _receiveDtDetail;
        private int? _qty;
        private decimal? _amount;
        private string _tranfDetail;
        private string _strAmount;
        private DateTime? _payDt;
        private DateTime? _paymentDt;
        private int? _totalBillCount;
        private decimal? _totalBillAmount;
        private string _bankName;
        private string _tranfAccNo;
        private DateTime? _tranfDt;
        private List<DateTime> _payDtList = new List<DateTime>();

        public PPrintedReceipt()
        {
            _payDtList = new List<DateTime>();
        }


        [DataMember(Order=1)]
        public string ReceiptId
        {
            get { return this._receiptId; }
            set { this._receiptId = value; }
        }


        [DataMember(Order=2)]
        public string ReceiptName
        {
            get { return this._receiptName; }
            set { this._receiptName = value; }
        }


        [DataMember(Order=3)]
        public string ReceiptBranch
        {
            get { return this._receiptBranch; }
            set { this._receiptBranch = value; }
        }


        [DataMember(Order=4)]
        public string ReceiptAddr
        {
            get { return this._receiptAddr; }
            set { this._receiptAddr = value; }
        }


        [DataMember(Order=5)]
        public string ReceiptDt
        {
            get { return this._receiptDt; }
            set { this._receiptDt = value; }
        }


        [DataMember(Order=6)]
        public string ReceiptMonth
        {
            get { return this._receiptMonth; }
            set { this._receiptMonth = value; }
        }


        [DataMember(Order=7)]
        public string ReceiptYear
        {
            get { return this._receiptYear; }
            set { this._receiptYear = value; }
        }


        [DataMember(Order=8)]
        public string CompName
        {
            get { return this._compName; }
            set { this._compName = value; }
        }


        [DataMember(Order=9)]
        public string CompAddr
        {
            get { return this._compAddr; }
            set { this._compAddr = value; }
        }


        [DataMember(Order=10)]
        public string ReceiveDtDetail
        {
            get { return this._receiveDtDetail; }
            set { this._receiveDtDetail = value; }
        }


        [DataMember(Order=11)]
        public int? Qty
        {
            get { return this._qty; }
            set { this._qty = value; }
        }


        [DataMember(Order=12)]
        public decimal? Amount
        {
            get { return this._amount; }
            set { this._amount = value; }
        }


        [DataMember(Order=13)]
        public string TranfDetail
        {
            get { return this._tranfDetail; }
            set { this._tranfDetail = value; }
        }


        [DataMember(Order=14)]
        public string StrAmount
        {
            get { return this._strAmount; }
            set { this._strAmount = value; }
        }


        [DataMember(Order=15)]
        public DateTime? PayDt
        {
            get { return this._payDt; }
            set { this._payDt = value; }
        }


        [DataMember(Order=16)]
        public DateTime? PaymentDt
        {
            get { return this._paymentDt; }
            set { this._paymentDt = value; }
        }


        [DataMember(Order=17)]
        public int? TotalBillCount
        {
            get { return this._totalBillCount; }
            set { this._totalBillCount = value; }
        }


        [DataMember(Order=18)]
        public decimal? TotalBillAmount
        {
            get { return this._totalBillAmount; }
            set { this._totalBillAmount = value; }
        }


        [DataMember(Order=19)]
        public string BankName
        {
            get { return this._bankName; }
            set { this._bankName = value; }
        }


        [DataMember(Order=20)]
        public string TranfAccNo
        {
            get { return this._tranfAccNo; }
            set { this._tranfAccNo = value; }
        }


        [DataMember(Order=21)]
        public DateTime? TranfDt
        {
            get { return this._tranfDt; }
            set { this._tranfDt = value; }
        }


        [DataMember(Order=22)]
        public List<DateTime> PayDtList
        {
            get { return this._payDtList; }
            set { this._payDtList = value; }
        }


    }
}
