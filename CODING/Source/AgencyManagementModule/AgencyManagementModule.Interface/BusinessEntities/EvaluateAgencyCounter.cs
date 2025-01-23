using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class EvaluateAgencyCounter
    {
        #region "Variable Local"
        private string _branchId;
        private string _agentId;
        private decimal? _percentOfItem = 0;
        private decimal? _percentOfAmount = 0;
        #endregion      
        #region "Property"

        [DataMember(Order=1)]
        public string BranchId
        {
            get { return _branchId; }
            set { this._branchId = value; }
        }

        [DataMember(Order=2)]
        public string AgentId
        {
            get { return _agentId; }
            set { this._agentId = value; }
        }

        [DataMember(Order=3)]
        public decimal? PercentOfItem
        {
            get { return _percentOfItem; }
            set { this._percentOfItem = value; }
        }

        [DataMember(Order=4)]
        public decimal? PercentOfAmount
        {
            get { return _percentOfAmount; }
            set { this._percentOfAmount = value; }
        }
        #endregion
    }
}
