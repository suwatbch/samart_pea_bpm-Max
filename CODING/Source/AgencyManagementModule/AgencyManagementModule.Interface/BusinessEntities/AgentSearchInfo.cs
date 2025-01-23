using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;


namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public enum SerachType
    {
        [EnumMember]
        All = 0,
        [EnumMember]
        AgentId = 1,
        [EnumMember]
        Deposit = 2,
        [EnumMember]
        AgentName = 3
    }

    [DataContract]
    public class AgentSearchInfo
    {
        private string _keyword;
        private SerachType _type = SerachType.All;
        private string _agentId;
        private string _branchId;
        
       
        [DataMember(Order=2)]
        public string Keyword
        {
            get { return _keyword; }
            set { _keyword = value; }
        }


        [DataMember(Order=3)]
        public SerachType Type
        {
            get { return _type; }
            set { _type = value; }
        }


        [DataMember(Order=4)]
        public string AgentId
        {
            get { return _agentId; }
            set { _agentId = value; }
        }


        [DataMember(Order=5)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }

    }
}
