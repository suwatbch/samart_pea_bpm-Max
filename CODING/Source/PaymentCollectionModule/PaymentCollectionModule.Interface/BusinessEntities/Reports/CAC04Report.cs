using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports
{
    [DataContract]
    public class CAC04Report
    {
        private string _branchId;
        private string _groupBankName;
        private string _bankKey;
        private string _bankName;
        private string _tranfAccNo;
        private string _clearingAccNo;
        private DateTime? _tranfDt;
        private DateTime? _paymentDt;
        private decimal? _amount;
        private decimal? _actualAmount;
        private decimal? _feeAmount;
        private string _receiptId;
        private string _caId;
        private string _caName;
        private string _dtName;
        private string _controllerName;
        private string _caDoc;
        private int? _quantity;
        private bool _isDuplicate;
        private string _paymentId;


        [DataMember(Order=1)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }


        [DataMember(Order=2)]
        public string GroupBankName
        {
            get { return _groupBankName; }
            set { _groupBankName = value; }
        }


        [DataMember(Order=3)]
        public string BankKey
        {
            get { return _bankKey; }
            set { _bankKey = value; }
        }


        [DataMember(Order=4)]
        public string BankName
        {
            get { return _bankName; }
            set { _bankName = value; }
        }


        [DataMember(Order=5)]
        public string TranfAccNo
        {
            get { return _tranfAccNo; }
            set { _tranfAccNo = value; }
        }


        [DataMember(Order=6)]
        public string ClearingAccNo
        {
            get { return _clearingAccNo; }
            set { _clearingAccNo = value; }
        }


        [DataMember(Order=7)]
        public DateTime? TranfDt
        {
            get { return _tranfDt; }
            set { _tranfDt = value; }
        }


        [DataMember(Order=8)]
        public DateTime? PaymentDt
        {
            get { return _paymentDt; }
            set { _paymentDt = value; }
        }

        //Checked TongKung
        //[DataMember(Order=9)]
        public string TranfDate
        {
            get { return _tranfDt.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")); }
        }

        //Checked TongKung
        //[DataMember(Order=10)]
        public string TranfTime
        {
            get { return _paymentDt.Value.ToString("dd/MM/yyyy  HH:mm", new CultureInfo("th-TH")); }
        }


        [DataMember(Order=11)]
        public decimal? Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }


        [DataMember(Order=12)]
        public decimal? ActualAmount
        {
            get { return _actualAmount; }
            set { _actualAmount = value; }
        }


        [DataMember(Order=13)]
        public decimal? FeeAmount
        {
            get { return _feeAmount; }
            set { _feeAmount = value; }
        }


        [DataMember(Order=14)]
        public string ReceiptId
        {
            get { return _receiptId; }
            set { _receiptId = value; }
        }


        [DataMember(Order=15)]
        public string CaId
        {
            get { return _caId; }
            set { _caId = value; }
        }


        [DataMember(Order=16)]
        public string CaName
        {
            get { return _caName; }
            set { _caName = value; }
        }


        [DataMember(Order=17)]
        public string DtName
        {
            get { return _dtName; }
            set { _dtName = value; }
        }


        [DataMember(Order=18)]
        public string ControllerName
        {
            get { return _controllerName; }
            set { _controllerName = value; }
        }


        [DataMember(Order=19)]
        public string CaDoc
        {
            get { return _caDoc; }
            set { _caDoc = value; }
        }


        [DataMember(Order=20)]
        public int? Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }


        [DataMember(Order=21)]
        public bool IsDuplicate
        {
            get { return _isDuplicate; }
            set { _isDuplicate = value; }
        }


        [DataMember(Order=22)]
        public string PaymentId
        {
            get { return _paymentId; }
            set { _paymentId = value; }
        }
    }
}
