using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class AgentId
    {
        private int? _branchId = 0;
        private int? _lineId = 0;
        private int? _agencyNumber = 0;


        [DataMember(Order=1)]
        public int? BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }


        [DataMember(Order=2)]
        public int? LineId
        {
            get { return _lineId; }
            set { _lineId = value; }
        }


        [DataMember(Order=3)]
        public int? CustomerNumber
        {
            get { return _agencyNumber; }
            set { _agencyNumber = value; }
        }
    }
}
