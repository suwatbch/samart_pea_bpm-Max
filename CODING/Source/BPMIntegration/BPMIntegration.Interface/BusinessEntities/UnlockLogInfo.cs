using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class UnlockLogInfo
    {
        private string _UnlockId;
        private string _FncId;
        private DateTime? _UnlockDt;
        private string _CurrentUserId;
        private string _UnlockUserId;
        private string _Description;
        private string _Remark;
        private string _BranchId;
        private string _BranchName;
        private DateTime? _PostDt;
        private string _PostBranchServerId;
        private string _SyncFlag;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string UnlockId
        {
            get { return _UnlockId; }
            set { _UnlockId = value; }
        }
        [DataMember(Order = 2)]
        public string FncId
        {
            get { return _FncId; }
            set { _FncId = value; }
        }
        [DataMember(Order = 3)]
        public DateTime? UnlockDt
        {
            get { return _UnlockDt; }
            set { _UnlockDt = value; }
        }
        [DataMember(Order = 4)]
        public string CurrentUserId
        {
            get { return _CurrentUserId; }
            set { _CurrentUserId = value; }
        }
        [DataMember(Order = 5)]
        public string UnlockUserId
        {
            get { return _UnlockUserId; }
            set { _UnlockUserId = value; }
        }
        [DataMember(Order = 6)]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        [DataMember(Order = 7)]
        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }
        [DataMember(Order = 8)]
        public string BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }
        [DataMember(Order = 9)]
        public string BranchName
        {
            get { return _BranchName; }
            set { _BranchName = value; }
        }
        [DataMember(Order = 10)]
        public DateTime? PostDt
        {
            get { return _PostDt; }
            set { _PostDt = value; }
        }
        [DataMember(Order = 11)]
        public string PostBranchServerId
        {
            get { return _PostBranchServerId; }
            set { _PostBranchServerId = value; }
        }
        [DataMember(Order = 12)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 13)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 14)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 15)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 16)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

    }
}
