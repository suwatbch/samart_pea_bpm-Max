using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class RoleUserInfo
    {
        private string _RTId;
        private string _RoleId;
        private string _UserId;
        private DateTime? _PostDt;
        private string _PostBranchServerId;
        private string _SyncFlag;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string RTId
        {
            get { return _RTId; }
            set { _RTId = value; }
        }
        [DataMember(Order = 2)]
        public string RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
        }
        [DataMember(Order = 3)]
        public string UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        [DataMember(Order = 4)]
        public DateTime? PostDt
        {
            get { return _PostDt; }
            set { _PostDt = value; }
        }
        [DataMember(Order = 5)]
        public string PostBranchServerId
        {
            get { return _PostBranchServerId; }
            set { _PostBranchServerId = value; }
        }
        [DataMember(Order = 6)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 7)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 8)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 9)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 10)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }
    }
}
