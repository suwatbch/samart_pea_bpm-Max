using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using ProtoBuf;


namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class RelatedReceipts 
    {
        public RelatedReceipts(Receipt r)
        {
            this.ReceiptId = r.ReceiptId;
            this.DisplayReceiptId = r.DisplayReceiptId;
            this.PrintingSequence = r.PrintingSequence;
            this.ReceiptDate = r.ReceiptDate;
            this.CustomerId = r.CustomerId;
            this.CustomerName = r.CustomerName;
            this.CustomerAddress = r.CustomerAddress;
            this.PaymentId = r.PaymentId;
            this.PaymentType = r.PaymentType;
            this.PosId = r.PosId;
            this.CashierName = r.CashierName;
            this.GAmount = r.GAmount;
            this.PmInfo = r.PmInfo;
        }

        public RelatedReceipts()
        {
        }

        private string _receiptId;

        [DataMember(Order=1)]
        public string ReceiptId
        {
            get { return _receiptId; }
            set { _receiptId = value; }
        }

        private string _displayReceiptId;

        [DataMember(Order=2)]
        public string DisplayReceiptId
        {
            get { return _displayReceiptId; }
            set { _displayReceiptId = value; }
        }

        private short? _printingSequence;

        [DataMember(Order=3)]
        public short? PrintingSequence
        {
            get { return _printingSequence; }
            set { _printingSequence = value; }
        }

        private DateTime? _receiptDate;

        [DataMember(Order=4)]
        public DateTime? ReceiptDate
        {
            get { return _receiptDate; }
            set { _receiptDate = value; }
        }

        private string _customerId;

        [DataMember(Order=5)]
        public string CustomerId
        {
            get { return _customerId; }
            set { _customerId = value; }
        }

        private string _customerName;

        [DataMember(Order=6)]
        public string CustomerName
        {
            get { return _customerName; }
            set { _customerName = value; }
        }

        private string _customerAddress;

        [DataMember(Order=7)]
        public string CustomerAddress
        {
            get { return _customerAddress; }
            set { _customerAddress = value; }
        }

        private string _paymentId;

        [DataMember(Order=8)]
        public string PaymentId
        {
            get { return _paymentId; }
            set { _paymentId = value; }
        }

        private string _paymentType;

        [DataMember(Order=9)]
        public string PaymentType
        {
            get { return _paymentType; }
            set { _paymentType = value; }
        }

        private string _posId;

        [DataMember(Order=10)]
        public string PosId
        {
            get { return _posId; }
            set { _posId = value; }
        }

        private string _cashierName;

        [DataMember(Order=11)]
        public string CashierName
        {
            get { return _cashierName; }
            set { _cashierName = value; }
        }

        private decimal? _gAmount;

        [DataMember(Order=12)]
        public decimal? GAmount
        {
            get { return _gAmount; }
            set { _gAmount = value; }
        }

        private List<PaymentTypeInfo> _paymentTypeInfo = new List<PaymentTypeInfo>();

        [DataMember(Order=13)]
        public List<PaymentTypeInfo> PmInfo
        {
            get { return _paymentTypeInfo; }
            set { _paymentTypeInfo = value; }
        }

        private bool _isChecked = false;

        [DataMember(Order = 14)]
        public bool IsChecked
        {
            get { return _isChecked; }
            set { _isChecked = value; }
        }
    }

}
