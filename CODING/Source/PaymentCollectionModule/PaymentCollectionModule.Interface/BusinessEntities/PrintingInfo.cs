using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract, Serializable]
    public class PrintingInfo
    {
        private PrintingReceipt _printingReceipt;

        [DataMember(Order=1)]
        public PrintingReceipt PrintingReceipt
        {
            get { return _printingReceipt; }
            set { _printingReceipt = value; }
        }

        private List<PaymentMethod> _paymentMethods = new List<PaymentMethod>();

        [DataMember(Order=2)]
        public List<PaymentMethod> PaymentMethods
        {
            get { return _paymentMethods; }
            set { _paymentMethods = value; }
        }

        private List<ReceiptStatus> _receiptStatus = new List<ReceiptStatus>();

        [DataMember(Order=3)]
        public List<ReceiptStatus> ReceiptStatus
        {
            get { return _receiptStatus; }
            set { _receiptStatus = value; }
        }

        public PrintingInfo()
        {
        }        

    }
}
