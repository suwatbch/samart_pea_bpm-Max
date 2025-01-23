using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class AdvancePaymentHistory
    {
        private string _itemId;

        [DataMember(Order=1)]
        public string ItemId
        {
            get { return _itemId; }
            set { _itemId = value; }
        }

        private Decimal? _paidGAmount;

        [DataMember(Order=2)]
        public Decimal? PaidGAmount
        {
            get { return _paidGAmount; }
            set { _paidGAmount = value; }
        }

        private DateTime? _paymentDt;

        [DataMember(Order=3)]
        public DateTime? PaymentDt
        {
            get { return _paymentDt; }
            set { _paymentDt = value; }
        }

        private string _receiptId;

        [DataMember(Order=4)]
        public string ReceiptId
        {
            get { return _receiptId; }
            set { _receiptId = value; }
        }
    }
}
