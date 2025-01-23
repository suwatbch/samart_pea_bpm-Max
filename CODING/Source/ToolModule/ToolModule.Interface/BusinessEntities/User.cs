using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ToolModule.Interface.BusinessEntities
{
    [DataContract]
    public class User
    {
        private string _rtId;
        private string _userId;
        private string _roleId;
        private string _newRoleId;
        private string _password;
        private string _fullName;
        private string _department;
        private DateTime? _modifiedDt;
        private string _newPassword;
        private bool _gcheck = false;
        private int _pwdState;
        private string _userFlag;
        private string _modifiedBy;
        private string _branchId;

        private string _changerPwd;
        private string _changerId;
        private string _scopeId;
        private string _scopeText;
        private string _branchName;

        [DataMember(Order=1)]
        public string ChangerPwd
        {
            get { return _changerPwd; }
            set { _changerPwd = value; }
        }


        [DataMember(Order=2)]
        public string ChangerId
        {
            get { return _changerId; }
            set { _changerId = value; }
        }


        [DataMember(Order=3)]
        public string RTId
        {
            get { return _rtId; }
            set { _rtId = value; }
        }


        [DataMember(Order=4)]
        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }


        [DataMember(Order=5)]
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }


        [DataMember(Order=6)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }


        [DataMember(Order=7)]
        public string RoleId
        {
            get { return _roleId; }
            set { _roleId = value; }
        }


        [DataMember(Order=8)]
        public string NewRoleId
        {
            get { return _newRoleId; }
            set { _newRoleId = value; }
        }
        

        [DataMember(Order=9)]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }


        [DataMember(Order=10)]
        public string NewPassword
        {
            get { return _newPassword; }
            set { _newPassword = value; }
        }


        [DataMember(Order=11)]
        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }


        [DataMember(Order=12)]
        public string Department
        {
            get { return _department; }
            set { _department = value; }
        }


        [DataMember(Order=13)]
        public DateTime? ModifiedDt
        {
            get { return _modifiedDt; }
            set { _modifiedDt = value; }
        }


        [DataMember(Order=14)]
        public bool GCheck
        {
            get { return _gcheck; }
            set { _gcheck = value; }
        }


        [DataMember(Order=15)]
        public int PwdState
        {
            get { return _pwdState; }
            set { _pwdState = value; }
        }


        [DataMember(Order=16)]
        public string UserFlag
        {
            get { return _userFlag; }
            set { _userFlag = value; }
        }

        [DataMember(Order = 17)]
        public string ScopeId
        {
            get { return _scopeId; }
            set { _scopeId = value; }
        }

        [DataMember(Order = 18)]
        public string ScopeText
        {
            get { return _scopeText; }
            set { _scopeText = value; }
        }

        [DataMember(Order = 19)]
        public string BranchName
        {
            get { return _branchName; }
            set { _branchName = value; }
        }
    }
}
