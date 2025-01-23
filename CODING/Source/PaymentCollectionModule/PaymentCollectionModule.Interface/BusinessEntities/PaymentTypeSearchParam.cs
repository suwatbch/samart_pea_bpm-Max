using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class PaymentTypeSearchParam
    {
        private string _paymentId;

        [DataMember(Order = 1)]
        public string PaymentId
        {
            get { return _paymentId; }
            set { _paymentId = value; }
        }
    }
}
