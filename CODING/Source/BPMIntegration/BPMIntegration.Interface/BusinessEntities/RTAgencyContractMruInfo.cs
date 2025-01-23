using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class RTAgencyContractMruInfo
    {
        private string _AgentMruId;
        private string _CaId;
        private string _CaName;
        private string _CaBranchId;
        private string _BranchId;
        private string _BranchName;
        private string _MruId;
        private decimal? _SecurityDeposit;
        private DateTime? _PostDt;
        private string _PostBranchServerId;
        private string _SyncFlag;
        private string _ModifiedBy;
        private DateTime? _ModifiedDt;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string AgentMruId
        {
            get { return _AgentMruId; }
            set { _AgentMruId = value; }
        }
        [DataMember(Order = 2)]
        public string CaId
        {
            get { return _CaId; }
            set { _CaId = value; }
        }
        [DataMember(Order = 3)]
        public string CaName
        {
            get { return _CaName; }
            set { _CaName = value; }
        }
        [DataMember(Order = 4)]
        public string CaBranchId
        {
            get { return _CaBranchId; }
            set { _CaBranchId = value; }
        }
        [DataMember(Order = 5)]
        public string BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }
        [DataMember(Order = 6)]
        public string BranchName
        {
            get { return _BranchName; }
            set { _BranchName = value; }
        }
        [DataMember(Order = 7)]
        public string MruId
        {
            get { return _MruId; }
            set { _MruId = value; }
        }
        [DataMember(Order = 8)]
        public decimal? SecurityDeposit
        {
            get { return _SecurityDeposit; }
            set { _SecurityDeposit = value; }
        }
        [DataMember(Order = 9)]
        public DateTime? PostDt
        {
            get { return _PostDt; }
            set { _PostDt = value; }
        }
        [DataMember(Order = 10)]
        public string PostBranchServerId
        {
            get { return _PostBranchServerId; }
            set { _PostBranchServerId = value; }
        }
        [DataMember(Order = 11)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 12)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 13)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 14)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 15)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

    }
}
