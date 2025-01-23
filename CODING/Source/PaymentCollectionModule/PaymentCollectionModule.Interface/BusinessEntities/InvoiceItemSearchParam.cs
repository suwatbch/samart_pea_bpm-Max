using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class InvoiceItemSearchParam: BaseParam
    {
        private string _groupInvoiceNo;

        [DataMember(Order=1)]
        public string GroupInvoiceNo
        {
            get { return _groupInvoiceNo; }
            set { _groupInvoiceNo = value; }
        }

        public InvoiceItemSearchParam(){}
        public InvoiceItemSearchParam(string groupInvoiceNo)
        {
            _groupInvoiceNo = groupInvoiceNo;
        }
    }
}
