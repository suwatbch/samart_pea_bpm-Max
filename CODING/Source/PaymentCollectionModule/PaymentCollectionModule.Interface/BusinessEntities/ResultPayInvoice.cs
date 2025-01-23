using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class ResultPayInvoice
    {
         private List<Invoice> paidInvoices = new List<Invoice>();
         private List<PrintingReceipt> receipts = new List<PrintingReceipt>();
         private bool _oneTouch;

        [DataMember(Order=1)]
         public List<Invoice> PaidInvoices
        {
            get { return paidInvoices; }
            set { paidInvoices = value; }
        }


        [DataMember(Order=2)]
        public List<PrintingReceipt> Receipts
        {
            get { return receipts; }
            set { receipts = value; }
        }

        [DataMember(Order = 3)]
        public bool OneTouchFlag
        {
            get { return _oneTouch; }
            set { _oneTouch = value; }
        }

    }
}
