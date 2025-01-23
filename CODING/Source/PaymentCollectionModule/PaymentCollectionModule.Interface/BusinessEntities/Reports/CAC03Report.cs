using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports
{
    [DataContract]
    public class CAC03Report
    {
        private string _branchId;
        private string _groupBankName;
        private string _bankKey;
        private string _bankName;
        private string _chqType;
        private string _chqAccNo;
        private string _chqNo;
        private string _clearingAccNo;
        private DateTime? _chqDt;
        private DateTime? _paymentDt;
        private decimal? _chequeAmount;
        private decimal? _changeAmount;
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
        public string ChqType
        {
            get { return _chqType; }
            set { _chqType = value; }
        }


        [DataMember(Order=6)]
        public string ChqAccNo
        {
            get { return _chqAccNo; }
            set { _chqAccNo = value; }
        }


        [DataMember(Order=7)]
        public string ChqNo
        {
            get { return _chqNo; }
            set { _chqNo = value; }
        }


        [DataMember(Order=8)]
        public string ClearingAccNo
        {
            get { return _clearingAccNo; }
            set { _clearingAccNo = value; }
        }


        [DataMember(Order=9)]
        public DateTime? ChqDt
        {
            get { return _chqDt; }
            set { _chqDt = value; }
        }


        [DataMember(Order=10)]
        public DateTime? PaymentDt
        {
            get { return _paymentDt; }
            set { _paymentDt = value; }
        }

        //Checked TongKung
        //[DataMember(Order=11)]
        public string ChqDate
        {
            get { return _chqDt.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")); }
        }

        //Checked TongKung
        //[DataMember(Order=12)]
        public string ChqTime
        {
            get { return _paymentDt.Value.ToString("dd/MM/yyyy  HH:mm", new CultureInfo("th-TH")); }
        }


        [DataMember(Order=13)]
        public decimal? ChequeAmount
        {
            get { return _chequeAmount; }
            set { _chequeAmount = value; }
        }


        [DataMember(Order=14)]
        public decimal? ChangeAmount
        {
            get { return _changeAmount; }
            set { _changeAmount = value; }
        }


        [DataMember(Order=15)]
        public decimal? ActualAmount
        {
            get { return _actualAmount; }
            set { _actualAmount = value; }
        }


        [DataMember(Order=16)]
        public decimal? FeeAmount
        {
            get { return _feeAmount; }
            set { _feeAmount = value; }
        }


        [DataMember(Order=17)]
        public string ReceiptId
        {
            get { return _receiptId; }
            set { _receiptId = value; }
        }


        [DataMember(Order=18)]
        public string CaId
        {
            get { return _caId; }
            set { _caId = value; }
        }


        [DataMember(Order=19)]
        public string CaName
        {
            get { return _caName; }
            set { _caName = value; }
        }


        [DataMember(Order=20)]
        public string DtName
        {
            get { return _dtName; }
            set { _dtName = value; }
        }


        [DataMember(Order=21)]
        public string ControllerName
        {
            get { return _controllerName; }
            set { _controllerName = value; }
        }


        [DataMember(Order=22)]
        public string CaDoc
        {
            get { return _caDoc; }
            set { _caDoc = value; }
        }


        [DataMember(Order=23)]
        public int? Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }


        [DataMember(Order=24)]
        public bool IsDuplicate
        {
            get { return _isDuplicate; }
            set { _isDuplicate = value; }
        }


        [DataMember(Order=25)]
        public string PaymentId
        {
            get { return _paymentId; }
            set { _paymentId = value; }
        }
    }
}
