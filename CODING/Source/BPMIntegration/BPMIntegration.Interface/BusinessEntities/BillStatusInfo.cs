using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class BillStatusInfo
    {
        private string _BranchId;
        private string _MruId;
        private string _BillPred;
        private string _BranchName;
        private string _PortionId;
        private DateTime? _PostDt;
        private string _PostBranchServerId;
        private string _ModifiedBy;
        private DateTime? _ModifiedDt;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }
        [DataMember(Order = 2)]
        public string MruId
        {
            get { return _MruId; }
            set { _MruId = value; }
        }
        [DataMember(Order = 3)]
        public string BillPred
        {
            get { return _BillPred; }
            set { _BillPred = value; }
        }
        [DataMember(Order = 4)]
        public string BranchName
        {
            get { return _BranchName; }
            set { _BranchName = value; }
        }
        [DataMember(Order = 5)]
        public string PortionId
        {
            get { return _PortionId; }
            set { _PortionId = value; }
        }
        [DataMember(Order = 6)]
        public DateTime? PostDt
        {
            get { return _PostDt; }
            set { _PostDt = value; }
        }
        [DataMember(Order = 7)]
        public string PostBranchServerId
        {
            get { return _PostBranchServerId; }
            set { _PostBranchServerId = value; }
        }
        [DataMember(Order = 8)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 9)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 10)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 11)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

    }
}
