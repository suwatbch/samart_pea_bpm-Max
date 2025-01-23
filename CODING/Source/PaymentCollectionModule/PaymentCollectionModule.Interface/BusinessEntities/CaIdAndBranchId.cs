using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class CaIdAndBranchId
    {
        private string _caId;
        private string _techBranchId;
        private string _commBranchId;


        [DataMember(Order=1)]
        public string CaId
        {
            get { return _caId; }
            set { _caId = value; }
        }


        [DataMember(Order=2)]
        public string TechBranchId
        {
            get { return _techBranchId; }
            set { _techBranchId = value; }
        }


        [DataMember(Order=3)]
        public string CommBranchId
        {
            get { return _commBranchId; }
            set { _commBranchId = value; }
        }

    }
}
