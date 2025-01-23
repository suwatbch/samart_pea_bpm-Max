using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class HistoryViewParam : BaseParam
    {
        private string _caId;

        [DataMember(Order=1)]
        public string CaId
        {
            get { return _caId; }
            set { _caId = value; }
        }
        
        public HistoryViewParam(){}
        public HistoryViewParam(string customerId, bool isOtherBranch): base(isOtherBranch)
        {
            _caId = customerId;
        }
    }
}
