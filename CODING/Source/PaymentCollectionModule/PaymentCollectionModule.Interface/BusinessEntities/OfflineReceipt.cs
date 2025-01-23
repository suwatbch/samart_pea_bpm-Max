using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class OfflineReceipt
    {
        List<PrintingReceipt> _printingReceipts = new List<PrintingReceipt>();

        [DataMember(Order=1)]
        public List<PrintingReceipt> PrintingReceipts
        {
            get { return _printingReceipts; }
            set { _printingReceipts = value; }
        }

        ExternalReceipt _externalReceipt;

        [DataMember(Order=2)]
        public ExternalReceipt ExternalReceipt
        {
            get { return _externalReceipt; }
            set { _externalReceipt = value; }
        }

        DateTime _paymentDate;

        [DataMember(Order=3)]
        public DateTime PaymentDate
        {
            get { return _paymentDate; }
            set { _paymentDate = value; }
        }

        public OfflineReceipt() { }

        public OfflineReceipt(List<PrintingReceipt> printingReceipts, ExternalReceipt externalReceipt, DateTime paymentDate)
        {
            _printingReceipts = printingReceipts;
            _externalReceipt = externalReceipt;
            _paymentDate = paymentDate;
        }
    }
}
