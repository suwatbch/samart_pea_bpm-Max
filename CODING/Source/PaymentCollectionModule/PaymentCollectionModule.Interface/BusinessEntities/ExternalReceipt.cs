using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class ExternalReceipt
    {
        private string _receiptId;

        [DataMember(Order=1)]
        public string ReceiptId
        {
            get { return _receiptId; }
            set { _receiptId = value; }
        }

        private DateTime? _receiptDate;

        [DataMember(Order=2)]
        public DateTime? ReceiptDate
        {
            get { return _receiptDate; }
            set { _receiptDate = value; }
        }

        public ExternalReceipt() { }

        public ExternalReceipt(string receiptId, DateTime receiptDate)
        {
            _receiptId = receiptId;
            _receiptDate = receiptDate;
        }
    }
}
