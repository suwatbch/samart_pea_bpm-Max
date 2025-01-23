using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class LastReceiptPayment
    {
        private string _receiptId;

        [DataMember(Order=1)]
        public string ReceiptId
        {
            get { return _receiptId; }
            set { _receiptId = value; }
        }

        private decimal? _cashAmount;

        [DataMember(Order=2)]
        public decimal? CashAmount
        {
            get { return _cashAmount; }
            set { _cashAmount = value; }
        }

        private decimal? _chqAmount;

        [DataMember(Order=3)]
        public decimal? ChqAmount
        {
            get { return _chqAmount; }
            set { _chqAmount = value; }
        }

        private decimal? _transAmount;

        [DataMember(Order=4)]
        public decimal? TransAmount
        {
            get { return _transAmount; }
            set { _transAmount = value; }
        }

        private decimal? _adjAmount;

        [DataMember(Order=5)]
        public decimal? AdjAmount
        {
            get { return _adjAmount; }
            set { _adjAmount = value; }
        }

        private decimal? _gAmount;

        [DataMember(Order=6)]
        public decimal? GAmount
        {
            get { return _gAmount; }
            set { _gAmount = value; }
        }
    }
}
