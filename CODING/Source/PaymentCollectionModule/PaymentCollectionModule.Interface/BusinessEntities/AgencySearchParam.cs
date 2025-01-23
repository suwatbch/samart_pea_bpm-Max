using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class AgencySearchParam
    {
        private string _agencyId;

        [DataMember(Order=1)]
        public string AgencyId
        {
          get { return _agencyId; }
          set { _agencyId = value; }
        }

        private string _agencyName;

        [DataMember(Order=2)]
        public string AgencyName
        {
            get { return _agencyName; }
            set { _agencyName = value; }
        }

        private string _branchId;

        [DataMember(Order = 3)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }

        public AgencySearchParam() { }

    }
}
