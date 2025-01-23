using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.AgencyManagementModule.Interface.Constants;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CAB08_01AgencyInfo
    {
        #region "Local Variable"
        private string _agentId;
        private string _agentName;
        #endregion

        #region "Property"

        [DataMember(Order=1)]
        public string AgentId
        {
            get 
            {
                if (_agentId.Length == ModuleConfigurationNames.CustomerNoLength)
                    return _agentId.Substring(ModuleConfigurationNames.AgentIdLength, ModuleConfigurationNames.AgentIdLength);
                else                
                    return _agentId;             
            }
            set { this._agentId = value; }
        }

        [DataMember(Order=2)]
        public string AgentName
        {
            get { return _agentName; }
            set { this._agentName = value; }
        }
        #endregion
    }
}
