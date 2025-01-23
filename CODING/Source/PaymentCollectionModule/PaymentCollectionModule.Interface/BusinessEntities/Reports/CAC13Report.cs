using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports
{
    [DataContract]
    public class CAC13Report
    {
        private string _branchId;
        private string _branchName;
        private string _paymentDt;
        private string _posId;
        private string _controllerId;
        private string _receiptId;
        private string _dtName;
        private int? _quantity;
        private decimal? _baseAmount;
        private decimal? _ftAmount;
        private decimal? _vatAmount;
        private decimal? _gAmount;

        private int? _quantity1;
        private decimal? _baseAmount1;
        private decimal? _ftAmount1;
        private decimal? _vatAmount1;
        private decimal? _gAmount1;

        private int? _quantity2;
        private decimal? _baseAmount2;
        private decimal? _ftAmount2;
        private decimal? _vatAmount2;
        private decimal? _gAmount2;


        [DataMember(Order=1)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }


        [DataMember(Order=2)]
        public string BranchName
        {
            get { return _branchName; }
            set { _branchName = value; }
        }


        [DataMember(Order=3)]
        public string PaymentDt
        {
            get { return (_paymentDt == null) ? "" : (_paymentDt.Contains("/") ? _paymentDt : _paymentDt.Substring(8, 2) + "/" + _paymentDt.Substring(5, 2) + "/" + (StringConvert.ToInt32(_paymentDt.Substring(0, 4)) + 543)); }
            set { _paymentDt = value; }
        }


        [DataMember(Order=4)]
        public string PosId
        {
            get { return _posId; }
            set { _posId = value; }
        }


        [DataMember(Order=5)]
        public string ControllerId
        {
            get { return _controllerId; }
            set { _controllerId = value; }
        }


        [DataMember(Order=6)]
        public string ReceiptId
        {
            get { return _receiptId; }
            set { _receiptId = value; }
        }


        [DataMember(Order=7)]
        public string DtName
        {
            get { return _dtName; }
            set { _dtName = value; }
        }


        [DataMember(Order=8)]
        public int? Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }


        [DataMember(Order=9)]
        public decimal? BaseAmount
        {
            get { return _baseAmount; }
            set { _baseAmount = value; }
        }


        [DataMember(Order=10)]
        public decimal? FtAmount
        {
            get { return _ftAmount; }
            set { _ftAmount = value; }
        }


        [DataMember(Order=11)]
        public decimal? VatAmount
        {
            get { return _vatAmount; }
            set { _vatAmount = value; }
        }


        [DataMember(Order=12)]
        public decimal? GAmount
        {
            get { return _gAmount; }
            set { _gAmount = value; }
        }



        [DataMember(Order=13)]
        public int? Quantity1
        {
            get { return _quantity1; }
            set { _quantity1 = value; }
        }


        [DataMember(Order=14)]
        public decimal? BaseAmount1
        {
            get { return _baseAmount1; }
            set { _baseAmount1 = value; }
        }


        [DataMember(Order=15)]
        public decimal? FtAmount1
        {
            get { return _ftAmount1; }
            set { _ftAmount1 = value; }
        }


        [DataMember(Order=16)]
        public decimal? VatAmount1
        {
            get { return _vatAmount1; }
            set { _vatAmount1 = value; }
        }


        [DataMember(Order=17)]
        public decimal? GAmount1
        {
            get { return _gAmount1; }
            set { _gAmount1 = value; }
        }


        [DataMember(Order=18)]
        public int? Quantity2
        {
            get { return _quantity2; }
            set { _quantity2 = value; }
        }


        [DataMember(Order=19)]
        public decimal? BaseAmount2
        {
            get { return _baseAmount2; }
            set { _baseAmount2 = value; }
        }


        [DataMember(Order=20)]
        public decimal? FtAmount2
        {
            get { return _ftAmount2; }
            set { _ftAmount2 = value; }
        }


        [DataMember(Order=21)]
        public decimal? VatAmount2
        {
            get { return _vatAmount2; }
            set { _vatAmount2 = value; }
        }


        [DataMember(Order=22)]
        public decimal? GAmount2
        {
            get { return _gAmount2; }
            set { _gAmount2 = value; }
        }
    }
}
