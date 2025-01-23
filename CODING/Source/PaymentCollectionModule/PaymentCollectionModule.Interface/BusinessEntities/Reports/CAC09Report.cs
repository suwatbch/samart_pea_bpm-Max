using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports
{
    [DataContract]
    public class CAC09Report
    {
        private string _posId;
        private string _caId;
        private string _receiptId;
        private string _extReceiptId;
        private int? _quantity;
        private decimal? _electricBaseAmount;
        private decimal? _otherBaseAmount;
        private decimal? _ftAmount;
        private decimal? _totalVatAmount;
        private decimal? _totalGAmount;
        private decimal? _rpGAmount;
        private decimal? _vatAmount;
        private decimal? _adjAmount;
        private decimal? _gAmount;
        private string _type;


        [DataMember(Order=1)]
        public string PosId
        {
            get { return _posId; }
            set { _posId = value; }
        }


        [DataMember(Order=2)]
        public string CaId
        {
            get { return _caId; }
            set { _caId = value; }
        }


        [DataMember(Order=3)]
        public string ReceiptId
        {
            get { return _receiptId; }
            set { _receiptId = value; }
        }


        [DataMember(Order=4)]
        public string ExtReceiptId
        {
            get { return _extReceiptId; }
            set { _extReceiptId = value; }
        }


        [DataMember(Order=5)]
        public int? Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }


        [DataMember(Order=6)]
        public decimal? ElectricBaseAmount
        {
            get { return _electricBaseAmount; }
            set { _electricBaseAmount = value; }
        }


        [DataMember(Order=7)]
        public decimal? OtherBaseAmount
        {
            get { return _otherBaseAmount; }
            set { _otherBaseAmount = value; }
        }


        [DataMember(Order=8)]
        public decimal? FtAmount
        {
            get { return _ftAmount; }
            set { _ftAmount = value; }
        }


        [DataMember(Order=9)]
        public decimal? TotalVatAmount
        {
            get { return _totalVatAmount; }
            set { _totalVatAmount = value; }
        }


        [DataMember(Order=10)]
        public decimal? TotalGAmount
        {
            get { return _totalGAmount; }
            set { _totalGAmount = value; }
        }


        [DataMember(Order=11)]
        public decimal? RPGAmount
        {
            get { return _rpGAmount; }
            set { _rpGAmount = value; }
        }


        [DataMember(Order=12)]
        public decimal? VatAmount
        {
            get { return _vatAmount; }
            set { _vatAmount = value; }
        }


        [DataMember(Order=13)]
        public decimal? AdjAmount
        {
            get { return _adjAmount; }
            set { _adjAmount = value; }
        }


        [DataMember(Order=14)]
        public decimal? GAmount
        {
            get { return _gAmount; }
            set { _gAmount = value; }
        }


        [DataMember(Order=15)]
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

    }
}
