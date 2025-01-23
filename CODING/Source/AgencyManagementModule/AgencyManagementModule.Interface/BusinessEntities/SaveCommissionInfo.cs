using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class SaveCommissionInfo
    {
        private string _period;
        private string _agentId;
        



        [DataMember(Order=1)]
        public string Period
        {
            set { _period = value; }
            get { return _period; }
        }


        [DataMember(Order=2)]
        public string AgentId
        {
            set { _agentId = value; }
            get { return _agentId; }
        }



    }
}
