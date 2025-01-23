using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [Serializable]
    public class ReceiptGroupItems
    {
        private string _mainGroupReceiptId;
        public string MainGroupReceiptId
        {
            get { return _mainGroupReceiptId; }
            set { _mainGroupReceiptId = value; }
        }

        private string _receiptId;
        public string ReceiptId
        {
            get { return _receiptId; }
            set { _receiptId = value; }
        }

        private string _invoiceNo;
        public string InvoiceNo
        {
            get { return _invoiceNo; }
            set { _invoiceNo = value; }
        }
    }

    [DataContract, Serializable]
    public class ReceiptGroupPaymentMethods
    {
        private string _paymentDetail;
        public string PaymentDetail
        {
            get { return _paymentDetail;  }
            set { _paymentDetail = value; }
        }        
    }

}
