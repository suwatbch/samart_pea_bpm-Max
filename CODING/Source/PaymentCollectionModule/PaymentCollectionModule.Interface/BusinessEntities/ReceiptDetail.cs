using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class ReceiptDetail
    {
        private List<PaidItem> _paidItems = new List<PaidItem>();
        private List<PaidMethod> _paidMethods = new List<PaidMethod>();


        [DataMember(Order=1)]
        public List<PaidItem> PaidItems
        {
            get { return _paidItems; }
            set { _paidItems = value; }
        }


        [DataMember(Order=2)]
        public List<PaidMethod> PaidMethods
        {
            get { return _paidMethods; }
            set { _paidMethods = value; }
        }
    }
}
