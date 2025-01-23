using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class ReceiptSearchParam
    {
        private string _receiptId;

        [DataMember(Order=1)]
        public string ReceiptId
        {
            get { return _receiptId; }
            set { _receiptId = value; }
        }

        private string _paymentId;

        [DataMember(Order=2)]
        public string PaymentId
        {
            get { return _paymentId; }
            set { _paymentId = value; }
        }

        private string _cashierName;

        [DataMember(Order=3)]
        public string CashierName
        {
            get { return _cashierName; }
            set { _cashierName = value; }
        }

        private string _customerId;

        [DataMember(Order=4)]
        public string CustomerId
        {
            get { return _customerId; }
            set { _customerId = value; }
        }

        private string _customerName;

        [DataMember(Order=5)]
        public string CustomerName
        {
            get { return _customerName; }
            set { _customerName = value; }
        }

        private string _branchId;

        [DataMember(Order=6)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }

        private bool _isCancel;

        [DataMember(Order=7)]
        public bool IsCancel
        {
            get { return _isCancel; }
            set { _isCancel = value; }
        }
    }
}
