using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CMruBatchInfo
    {
        private string _caId;
        private string _branchId;
        private string _mruId;


        [DataMember(Order=1)]
        public string CaId
        {
            set { _caId = value; }
            get { return _caId; }
        }


        [DataMember(Order=2)]
        public string BranchId
        {
            set { _branchId = value; }
            get { return _branchId; }
        }


        [DataMember(Order=3)]
        public string MruId
        {
            set { _mruId = value; }
            get { return _mruId; }
        }

    }
}
