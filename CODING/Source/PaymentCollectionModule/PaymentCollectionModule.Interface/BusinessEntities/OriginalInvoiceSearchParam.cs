using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class OriginalInvoiceSearchParam: BaseParam
    {
        private string _caDocNo;

        [DataMember(Order=1)]
        public string CaDocNo
        {
            get { return _caDocNo; }
            set { _caDocNo = value; }
        }

        public OriginalInvoiceSearchParam(){}
        public OriginalInvoiceSearchParam(string caDocNo)
        {
            _caDocNo = caDocNo;
        }
    }
}
