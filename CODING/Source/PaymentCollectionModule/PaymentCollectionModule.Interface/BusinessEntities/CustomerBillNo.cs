using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class CustomerBillNox
    {
        private string _billno;


        [DataMember(Order=1)]
        public string Billno
        {
            get { return _billno; }
            set { _billno = value; }
        }

    }
}
