using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class BillBookInputSetInfo
    {
        private string _BIpSetId;
        private string _BillBookId;
        private string _BranchId;
        private string _MruId;
        private string _BillPeriodType;
        private string _BillOutType;
        private DateTime? _PostDt;
        private string _PostBranchServerId;
        private string _SyncFlag;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string BIpSetId
        {
            get { return _BIpSetId; }
            set { _BIpSetId = value; }
        }
        [DataMember(Order = 2)]
        public string BillBookId
        {
            get { return _BillBookId; }
            set { _BillBookId = value; }
        }
        [DataMember(Order = 3)]
        public string BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }
        [DataMember(Order = 4)]
        public string MruId
        {
            get { return _MruId; }
            set { _MruId = value; }
        }
        [DataMember(Order = 5)]
        public string BillPeriodType
        {
            get { return _BillPeriodType; }
            set { _BillPeriodType = value; }
        }
        [DataMember(Order = 6)]
        public string BillOutType
        {
            get { return _BillOutType; }
            set { _BillOutType = value; }
        }
        [DataMember(Order = 7)]
        public DateTime? PostDt
        {
            get { return _PostDt; }
            set { _PostDt = value; }
        }
        [DataMember(Order = 8)]
        public string PostBranchServerId
        {
            get { return _PostBranchServerId; }
            set { _PostBranchServerId = value; }
        }
        [DataMember(Order = 9)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 10)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 11)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 12)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 13)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

    }
}
