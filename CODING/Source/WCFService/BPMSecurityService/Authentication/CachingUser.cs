using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using PEA.BPM.WebService.Security.BPMAuthenticationDSTableAdapters;
using System.Data.SqlClient;

namespace PEA.BPM.WebService.Security
{
    public class CachingUser
    {
        #region Private
        string _userid;
        string _password;
        string _fullname;
        string _department;
        string _token;
        DateTime? _lastrequest;
        DateTime? _lastlogin;
        DateTime? _pwdexpireddt;
        string _branchid;
        DateTime? _postdt;
        string _postbranchserverid;
        string _syncflag;
        string _modifiedby;
        DateTime? _modifieddt;
        string _active;
        string _curip;
        string _reqip;
        string _reqflag;
        bool _isuseriddirty = false;
        bool _ispassworddirty = false;
        bool _isfullnamedirty = false;
        bool _isdepartmentdirty = false;
        bool _istokendirty = false;
        bool _islastrequestdirty = false;
        bool _islastlogindirty = false;
        bool _ispwdexpireddtdirty = false;
        bool _isbranchiddirty = false;
        bool _ispostdtdirty = false;
        bool _ispostbranchserveriddirty = false;
        bool _issyncflagdirty = false;
        bool _ismodifiedbydirty = false;
        bool _ismodifieddtdirty = false;
        bool _isactivedirty = false;
        bool _iscuripdirty = false;
        bool _isreqipdirty = false;
        bool _isreqflagdirty = false;
        bool _isglobaldirty = false;
        #endregion

        public CachingUser(BPMAuthenticationDS.UserRow drow)
        {
            this.UserId = drow.UserId;
            this.Password = drow.IsPasswordNull() ? null : drow.Password;
            this.FullName = drow.IsFullNameNull() ? null : drow.FullName;
            this.Department = drow.IsDepartmentNull() ? null : drow.Department;
            this.Token = drow.IsTokenNull() ? null : drow.Token;
            this.LastRequest = drow.IsLastRequestNull() ? (DateTime?)null : drow.LastRequest;
            this.LastLogin = drow.IsLastLoginNull() ? (DateTime?)null : drow.LastLogin;
            this.PwdExpiredDt = drow.IsPwdExpiredDtNull() ? (DateTime?)null : drow.PwdExpiredDt;
            this.BranchId = drow.IsBranchIdNull() ? null : drow.BranchId;
            this.PostDt = drow.IsPostDtNull() ? (DateTime?)null : drow.PostDt;
            this.PostBranchServerId = drow.IsPostBranchServerIdNull() ? null : drow.PostBranchServerId;
            this.SyncFlag = drow.IsSyncFlagNull() ? null : drow.SyncFlag;
            this.ModifiedDt = drow.IsModifiedDtNull() ? (DateTime?)null : drow.ModifiedDt;
            this.ModifiedBy = drow.IsModifiedByNull() ? null : drow.ModifiedBy;
            this.Active = drow.IsActiveNull() ? null : drow.Active;
            this.CurIP = drow.IsCurIPNull() ? null : drow.CurIP;
            this.ReqIP = drow.IsReqIPNull() ? null : drow.ReqIP;
            this.ReqFlag = drow.IsReqFlagNull() ? null : drow.ReqFlag;

            this.ClearDirty();
        }

        private void ClearDirty()
        {
            _isuseriddirty = false;
            _ispassworddirty = false;
            _isfullnamedirty = false;
            _isdepartmentdirty = false;
            _istokendirty = false;
            _islastrequestdirty = false;
            _islastlogindirty = false;
            _ispwdexpireddtdirty = false;
            _isbranchiddirty = false;
            _ispostdtdirty = false;
            _ispostbranchserveriddirty = false;
            _issyncflagdirty = false;
            _ismodifiedbydirty = false;
            _ismodifieddtdirty = false;
            _isactivedirty = false;
            _iscuripdirty = false;
            _isreqipdirty = false;
            _isreqflagdirty = false;
            _isglobaldirty = false;
        }

        public void UpdateToDatabase(SqlConnection conn)
        {
            if (!this.IsDirty) return;

            string updatecolumn = "";
            //if (IsUserIdDirty) updatecolumn += GetUpdateStringColumn("UserId", this.UserId);
            if (IsPasswordDirty) updatecolumn += GetUpdateStringColumn("Password", this.Password);
            if (IsFullNameDirty) updatecolumn += GetUpdateStringColumn("FullName", this.FullName);
            if (IsDepartmentDirty) updatecolumn += GetUpdateStringColumn("Department", this.Department);
            if (IsTokenDirty) updatecolumn += GetUpdateStringColumn("Token", this.Token);
            if (IsLastRequestDirty) updatecolumn += GetUpdateDateTimeColumn("LastRequest", this.LastRequest);
            if (IsLastLoginDirty) updatecolumn += GetUpdateDateTimeColumn("LastLogin", this.LastLogin);
            if (IsPwdExpiredDtDirty) updatecolumn += GetUpdateDateTimeColumn("PwdExpiredDt", this.PwdExpiredDt);
            if (IsBranchIdDirty) updatecolumn += GetUpdateStringColumn("BranchId", this.BranchId);
            if (IsPostDtDirty) updatecolumn += GetUpdateDateTimeColumn("PostDt", this.PostDt);
            if (IsPostBranchServerIdDirty) updatecolumn += GetUpdateStringColumn("PostBranchServerId", this.PostBranchServerId);
            if (IsSyncFlagDirty) updatecolumn += GetUpdateStringColumn("SyncFlag", this.SyncFlag);
            if (IsModifiedDtDirty) updatecolumn += GetUpdateDateTimeColumn("ModifiedDt", this.ModifiedDt);
            if (IsModifiedByDirty) updatecolumn += GetUpdateStringColumn("ModifiedBy", this.ModifiedBy);
            if (IsActiveDirty) updatecolumn += GetUpdateStringColumn("Active", this.Active);
            if (IsCurIPDirty) updatecolumn += GetUpdateStringColumn("CurIP", this.CurIP);
            if (IsReqIPDirty) updatecolumn += GetUpdateStringColumn("ReqIP", this.ReqIP);
            if (IsReqFlagDirty) updatecolumn += GetUpdateStringColumn("ReqFlag", this.ReqFlag);
            updatecolumn = updatecolumn.Remove(0, 1); // remove , หน้าสุดออก

            string query = string.Format("UPDATE [ta].[User] SET {0} WHERE [UserId] = '{1}'", updatecolumn, this.UserId);
            SqlCommand sqlcmdb = new SqlCommand(query, conn);
            sqlcmdb.ExecuteNonQuery();

            this.ClearDirty();
        }

        private string GetUpdateStringColumn(string columnstr, string columndata)
        {
            string query = string.Format(",[{0}] = ", columnstr);
            if (columndata == null) query += "NULL";
            else query += "'" + columndata + "'";
            return query + Environment.NewLine;
        }
        private string GetUpdateDateTimeColumn(string columnstr, DateTime? columndata)
        {
            string query = string.Format(",[{0}] = ", columnstr);
            if (columndata == null) query += "NULL";
            else query += "'" + columndata.Value.ToString("s") + "'";
            return query + Environment.NewLine;
        }

        #region IsDirty
        public bool IsDirty
        {
            get { return _isglobaldirty; }
            set { _isglobaldirty = value; }
        }
        #endregion

        #region UserId
        public string UserId
        {
            get { return _userid; }
            set 
            {
                if (_userid != value)
                {
                    _userid = value;
                    _isuseriddirty = true;
                    _isglobaldirty = true;
                }
            }
        }
        public bool IsUserIdDirty
        {
            get { return _isuseriddirty; }
            set { _isuseriddirty = value; }
        }
        #endregion

        #region Password
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    _ispassworddirty = true;
                    _isglobaldirty = true;
                }
            }
        }
        public bool IsPasswordDirty
        {
            get { return _ispassworddirty; }
            set { _ispassworddirty = value; }
        }
        #endregion

        #region FullName
        public string FullName
        {
            get { return _fullname; }
            set
            {
                if (_fullname != value)
                {
                    _fullname = value;
                    _isfullnamedirty = true;
                    _isglobaldirty = true;
                }
            }
        }
        public bool IsFullNameDirty
        {
            get { return _isfullnamedirty; }
            set { _isfullnamedirty = value; }
        }
        #endregion

        #region Department
        public string Department
        {
            get { return _department; }
            set
            {
                if (_department != value)
                {
                    _department = value;
                    _isdepartmentdirty = true;
                    _isglobaldirty = true;
                }
            }
        }
        public bool IsDepartmentDirty
        {
            get { return _isdepartmentdirty; }
            set { _isdepartmentdirty = value; }
        }
        #endregion

        #region Token
        public string Token
        {
            get { return _token; }
            set
            {
                if (_token != value)
                {
                    _token = value;
                    _istokendirty = true;
                    _isglobaldirty = true;
                }
            }
        }
        public bool IsTokenDirty
        {
            get { return _istokendirty; }
            set { _istokendirty = value; }
        }
        #endregion

        #region LastRequest
        public DateTime? LastRequest
        {
            get { return _lastrequest; }
            set
            {
                if (_lastrequest != value)
                {
                    _lastrequest = value;
                    _islastrequestdirty = true;
                    _isglobaldirty = true;
                }
            }
        }
        public bool IsLastRequestDirty
        {
            get { return _islastrequestdirty; }
            set { _islastrequestdirty = value; }
        }
        #endregion

        #region LastLogin
        public DateTime? LastLogin
        {
            get { return _lastlogin; }
            set
            {
                if (_lastlogin != value)
                {
                    _lastlogin = value;
                    _islastlogindirty = true;
                    _isglobaldirty = true;
                }
            }
        }
        public bool IsLastLoginDirty
        {
            get { return _islastlogindirty; }
            set { _islastlogindirty = value; }
        }
        #endregion

        #region PwdExpiredDt
        public DateTime? PwdExpiredDt
        {
            get { return _pwdexpireddt; }
            set
            {
                if (_pwdexpireddt != value)
                {
                    _pwdexpireddt = value;
                    _ispwdexpireddtdirty = true;
                    _isglobaldirty = true;
                }
            }
        }
        public bool IsPwdExpiredDtDirty
        {
            get { return _ispwdexpireddtdirty; }
            set { _ispwdexpireddtdirty = value; }
        }
        #endregion

        #region BranchID
        public string BranchId
        {
            get { return _branchid; }
            set
            {
                if (_branchid != value)
                {
                    _branchid = value;
                    _isbranchiddirty = true;
                    _isglobaldirty = true;
                }
            }
        }
        public bool IsBranchIdDirty
        {
            get { return _isbranchiddirty; }
            set { _isbranchiddirty = value; }
        }
        #endregion

        #region PostDt
        public DateTime? PostDt
        {
            get { return _postdt; }
            set
            {
                if (_postdt != value)
                {
                    _postdt = value;
                    _ispostdtdirty = true;
                    _isglobaldirty = true;
                }
            }
        }
        public bool IsPostDtDirty
        {
            get { return _ispostdtdirty; }
            set { _ispostdtdirty = value; }
        }
        #endregion

        #region PostBranchServerId
        public string PostBranchServerId
        {
            get { return _postbranchserverid; }
            set
            {
                if (_postbranchserverid != value)
                {
                    _postbranchserverid = value;
                    _ispostbranchserveriddirty = true;
                    _isglobaldirty = true;
                }
            }
        }
        public bool IsPostBranchServerIdDirty
        {
            get { return _ispostbranchserveriddirty; }
            set { _ispostbranchserveriddirty = value; }
        }
        #endregion
       
        #region SyncFlag
        public string SyncFlag
        {
            get { return _syncflag; }
            set
            {
                if (_syncflag != value)
                {
                    _syncflag = value;
                    _issyncflagdirty = true;
                    _isglobaldirty = true;
                }
            }
        }
        public bool IsSyncFlagDirty
        {
            get { return _issyncflagdirty; }
            set { _issyncflagdirty = value; }
        }
        #endregion

        #region ModifiedDt
        public DateTime? ModifiedDt
        {
            get { return _modifieddt; }
            set
            {
                if (_modifieddt != value)
                {
                    _modifieddt = value;
                    _ismodifieddtdirty = true;
                    _isglobaldirty = true;
                }
            }
        }
        public bool IsModifiedDtDirty
        {
            get { return _ismodifieddtdirty; }
            set { _ismodifieddtdirty = value; }
        }
        #endregion
        
        #region ModifiedBy
        public string ModifiedBy
        {
            get { return _modifiedby; }
            set
            {
                if (_modifiedby != value)
                {
                    _modifiedby = value;
                    _ismodifiedbydirty = true;
                    _isglobaldirty = true;
                }
            }
        }
        public bool IsModifiedByDirty
        {
            get { return _ismodifiedbydirty; }
            set { _ismodifiedbydirty = value; }
        }
        #endregion
      
        #region Active
        public string Active
        {
            get { return _active; }
            set
            {
                if (_active != value)
                {
                    _active = value;
                    _isactivedirty = true;
                    _isglobaldirty = true;
                }
            }
        }
        public bool IsActiveDirty
        {
            get { return _isactivedirty; }
            set { _isactivedirty = value;  }
        }
        #endregion
        
        #region CurIP
        public string CurIP
        {
            get { return _curip; }
            set
            {
                if (_curip != value)
                {
                    _curip = value;
                    _iscuripdirty = true;
                    _isglobaldirty = true;
                }
            }
        }
        public bool IsCurIPDirty
        {
            get { return _iscuripdirty; }
            set { _iscuripdirty = value; }
        }
        #endregion

        #region ReqIP
        public string ReqIP
        {
            get { return _reqip; }
            set
            {
                if (_reqip != value)
                {
                    _reqip = value;
                    _isreqipdirty = true;
                    _isglobaldirty = true;
                }
            }
        }
        public bool IsReqIPDirty
        {
            get { return _isreqipdirty; }
            set { _isreqipdirty = value; }
        }
        #endregion
                 
        #region ReqFlag
        public string ReqFlag
        {
            get { return _reqflag; }
            set
            {
                if (_reqflag != value)
                {
                    _reqflag = value;
                    _isreqflagdirty = true;
                    _isglobaldirty = true;
                }
            }
        }
        public bool IsReqFlagDirty
        {
            get { return _isreqflagdirty; }
            set { _isreqflagdirty = value; }
        }
        #endregion


    }
}
