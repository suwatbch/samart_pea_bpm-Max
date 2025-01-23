using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class BillDetailSearchParam: BaseParam
    {
        private string _caId;

        [DataMember(Order=1)]
        public string CaId
        {
            get { return _caId; }
            set { _caId = value; }
        }


        public BillDetailSearchParam(){}
        public BillDetailSearchParam(string caId)
        {
            _caId = caId;
        }
    }
}
