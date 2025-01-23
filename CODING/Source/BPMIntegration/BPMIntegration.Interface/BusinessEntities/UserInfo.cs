using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class UserInfo
    {
        private string _UserId;
        private string _Password;
        private string _FullName;
        private string _Department;
        private string _Token;
        private DateTime? _LastRequest;
        private DateTime? _LastLogin;
        private DateTime? _PwdExpiredDt;
        private string _BranchId;
        private string _Permission;
        private string _CurIP;
        private string _ReqIP;
        private string _ReqFlag;
        private DateTime? _PostDt;
        private string _PostBranchServerId;
        private string _SyncFlag;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        [DataMember(Order = 2)]
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        [DataMember(Order = 3)]
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }
        [DataMember(Order = 4)]
        public string Department
        {
            get { return _Department; }
            set { _Department = value; }
        }
        [DataMember(Order = 5)]
        public string Token
        {
            get { return _Token; }
            set { _Token = value; }
        }
        [DataMember(Order = 6)]
        public DateTime? LastRequest
        {
            get { return _LastRequest; }
            set { _LastRequest = value; }
        }
        [DataMember(Order = 7)]
        public DateTime? LastLogin
        {
            get { return _LastLogin; }
            set { _LastLogin = value; }
        }
        [DataMember(Order = 8)]
        public DateTime? PwdExpiredDt
        {
            get { return _PwdExpiredDt; }
            set { _PwdExpiredDt = value; }
        }
        [DataMember(Order = 9)]
        public string BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }
        [DataMember(Order = 10)]
        public string Permission
        {
            get { return _Permission; }
            set { _Permission = value; }
        }
        [DataMember(Order = 11)]
        public string CurIP
        {
            get { return _CurIP; }
            set { _CurIP = value; }
        }
        [DataMember(Order = 12)]
        public string ReqIP
        {
            get { return _ReqIP; }
            set { _ReqIP = value; }
        }
        [DataMember(Order = 13)]
        public string ReqFlag
        {
            get { return _ReqFlag; }
            set { _ReqFlag = value; }
        }
        [DataMember(Order = 14)]
        public DateTime? PostDt
        {
            get { return _PostDt; }
            set { _PostDt = value; }
        }
        [DataMember(Order = 15)]
        public string PostBranchServerId
        {
            get { return _PostBranchServerId; }
            set { _PostBranchServerId = value; }
        }
        [DataMember(Order = 16)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 17)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 18)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 19)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 20)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }
    }
}
