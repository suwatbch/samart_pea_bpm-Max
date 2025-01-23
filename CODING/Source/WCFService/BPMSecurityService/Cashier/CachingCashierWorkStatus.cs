using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

namespace PEA.BPM.WebService.Security.Cashier
{
    public class CachingCashierWorkStatus
    {
        #region Private
        string _workid;
        string _cashierid;
        string _posid;
        string _status;
        DateTime? _openworkdt;
        DateTime? _closeworkdt;
        string _closeworkby;
        string _branchid;
        int? _baseline;
        string _markedbl;
        DateTime? _postdt;
        string _syncflag;
        DateTime? _modifieddt;
        string _modifiedby;
        string _active;
        bool _isworkiddirty = false;
        bool _iscashieriddirty = false;
        bool _isposiddirty = false;
        bool _isstatusdirty = false;
        bool _isopenworkdtdirty = false;
        bool _iscloseworkdtdirty = false;
        bool _iscloseworkbydirty = false;
        bool _isbranchiddirty = false;
        bool _isbaselinedirty = false;
        bool _ismarkedbldirty = false;
        bool _ispostdtdirty = false;
        bool _issyncflagdirty = false;
        bool _ismodifieddtdirty = false;
        bool _ismodifiedbydirty = false;
        bool _isactivedirty = false;
        bool _isglobaldirty = false;
        #endregion

        public CachingCashierWorkStatus(BPMAuthenticationDS.CashierWorkStatusRow drow)
        {
            this.WorkId = drow.WorkId;
            this.CashierId = drow.IsCashierIdNull() ? null : drow.CashierId;
            this.PosId = drow.IsPosIdNull() ? null : drow.PosId;
            this.Status = drow.IsStatusNull() ? null : drow.Status;
            this.OpenWorkDt = drow.IsOpenWorkDtNull() ? (DateTime?)null : drow.OpenWorkDt;
            this.CloseWorkDt = drow.IsCloseWorkDtNull() ? (DateTime?)null : drow.CloseWorkDt;
            this.CloseWorkBy = drow.IsCloseWorkByNull() ? null : drow.CloseWorkBy;
            this.BranchId = drow.IsBranchIdNull() ? null : drow.BranchId;
            this.BaseLine = drow.IsBaseLineNull() ? (int?)null : drow.BaseLine;
            this.MarkedBL = drow.IsMarkedBLNull() ? null : drow.MarkedBL;
            this.PostDt = drow.IsPostDtNull() ? (DateTime?)null : drow.PostDt;
            this.SyncFlag = drow.IsSyncFlagNull() ? null : drow.SyncFlag;
            this.ModifiedDt = drow.IsModifiedDtNull() ? (DateTime?)null : drow.ModifiedDt;
            this.ModifiedBy = drow.IsModifiedByNull() ? null : drow.ModifiedBy;
            this.Active = drow.IsActiveNull() ? null : drow.Active;

            this.ClearDirty();
        }

        private void ClearDirty()
        {
            _isworkiddirty = false;
            _iscashieriddirty = false;
            _isposiddirty = false;
            _isstatusdirty = false;
            _isopenworkdtdirty = false;
            _iscloseworkdtdirty = false;
            _iscloseworkbydirty = false;
            _isbranchiddirty = false;
            _isbaselinedirty = false;
            _ismarkedbldirty = false;
            _ispostdtdirty = false;
            _issyncflagdirty = false;
            _ismodifieddtdirty = false;
            _ismodifiedbydirty = false;
            _isactivedirty = false;
            _isglobaldirty = false;
        }

        public string GetUpdateQuery()
        {
            if (!this.IsDirty) return "";

            string updatecolumn = "";
            //if (IsWorkIdDirty) updatecolumn += GetUpdateStringColumn("WorkId", this.WorkId);
            if (IsCashierIdDirty) updatecolumn += GetUpdateStringColumn("CashierId", this.CashierId);
            if (IsPosIdDirty) updatecolumn += GetUpdateStringColumn("PosId", this.PosId);
            if (IsStatusDirty) updatecolumn += GetUpdateStringColumn("Status", this.Status);
            if (IsOpenWorkDtDirty) updatecolumn += GetUpdateDateTimeColumn("OpenWorkDt", this.OpenWorkDt);
            if (IsCloseWorkDtDirty) updatecolumn += GetUpdateDateTimeColumn("CloseWorkDt", this.CloseWorkDt);
            if (IsCloseWorkByDirty) updatecolumn += GetUpdateStringColumn("CloseWorkBy", this.CloseWorkBy);
            if (IsBranchIdDirty) updatecolumn += GetUpdateStringColumn("BranchId", this.BranchId);
            if (IsBaseLineDirty) updatecolumn += GetUpdateIntColumn("BaseLine", this.BaseLine);
            if (IsMarkedBLDirty) updatecolumn += GetUpdateStringColumn("MarkedBL", this.MarkedBL);
            if (IsPostDtDirty) updatecolumn += GetUpdateDateTimeColumn("PostDt", this.PostDt);
            if (IsSyncFlagDirty) updatecolumn += GetUpdateStringColumn("SyncFlag", this.SyncFlag);
            if (IsModifiedDtDirty) updatecolumn += GetUpdateDateTimeColumn("ModifiedDt", this.ModifiedDt);
            if (IsModifiedByDirty) updatecolumn += GetUpdateStringColumn("ModifiedBy", this.ModifiedBy);
            if (IsActiveDirty) updatecolumn += GetUpdateStringColumn("Active", this.Active);
            updatecolumn = updatecolumn.Remove(0, 1); // remove , หน้าสุดออก

            string query = string.Format("UPDATE [ts].[CashierWorkStatus] SET {0} WHERE [WorkId] = '{1}'", updatecolumn, this.WorkId);
            return query;
        }

        public void UpdateToDatabase(string query, SqlConnection conn)
        {
            if (query.Length == 0) return;
            
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
        private string GetUpdateIntColumn(string columnstr, int? columndata)
        {
            string query = string.Format(",[{0}] = ", columnstr);
            if (columndata == null) query += "NULL";
            else query += columndata.Value.ToString();
            return query + Environment.NewLine;
        }



        #region IsDirty
        public bool IsDirty
        {
            get { return _isglobaldirty; }
            set { _isglobaldirty = value; }
        }
        #endregion

        #region WorkId
        public string WorkId
        {
            get { return _workid; }
            set
            {
                if (_workid != value)
                {
                    _workid = value;
                    _isworkiddirty = true;
                    _isglobaldirty = true;
                }
            }
        }
        public bool IsWorkIdDirty
        {
            get { return _isworkiddirty; }
            set { _isworkiddirty = value; }
        }
        #endregion

        #region CashierId
        public string CashierId
        {
            get { return _cashierid; }
            set
            {
                if (_cashierid != value)
                {
                    _cashierid = value;
                    _iscashieriddirty = true;
                    _isglobaldirty = true;
                }
            }
        }
        public bool IsCashierIdDirty
        {
            get { return _iscashieriddirty; }
            set { _iscashieriddirty = value; }
        }
        #endregion

        #region PosId
        public string PosId
        {
            get { return _posid; }
            set
            {
                if (_posid != value)
                {
                    _posid = value;
                    _ispostdtdirty = true;
                    _isglobaldirty = true;
                }
            }
        }
        public bool IsPosIdDirty
        {
            get { return _ispostdtdirty; }
            set { _ispostdtdirty = value; }
        }
        #endregion

        #region Status
        public string Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    _isstatusdirty = true;
                    _isglobaldirty = true;
                }
            }
        }
        public bool IsStatusDirty
        {
            get { return _isstatusdirty; }
            set { _isstatusdirty = value; }
        }
        #endregion

        #region OpenWorkDt
        public DateTime? OpenWorkDt
        {
            get { return _openworkdt; }
            set
            {
                if (_openworkdt != value)
                {
                    _openworkdt = value;
                    _isopenworkdtdirty = true;
                    _isglobaldirty = true;
                }
            }
        }
        public bool IsOpenWorkDtDirty
        {
            get { return _isopenworkdtdirty; }
            set { _isopenworkdtdirty = value; }
        }
        #endregion

        #region CloseWorkDt
        public DateTime? CloseWorkDt
        {
            get { return _closeworkdt; }
            set
            {
                if (_closeworkdt != value)
                {
                    _closeworkdt = value;
                    _iscloseworkdtdirty = true;
                    _isglobaldirty = true;
                }
            }
        }
        public bool IsCloseWorkDtDirty
        {
            get { return _iscloseworkdtdirty; }
            set { _iscloseworkdtdirty = value; }
        }
        #endregion

        #region CloseWorkBy
        public string CloseWorkBy
        {
            get { return _closeworkby; }
            set
            {
                if (_closeworkby != value)
                {
                    _closeworkby = value;
                    _iscloseworkbydirty = true;
                    _isglobaldirty = true;
                }
            }
        }
        public bool IsCloseWorkByDirty
        {
            get { return _iscloseworkbydirty; }
            set { _iscloseworkbydirty = value; }
        }
        #endregion

        #region BranchId
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
        
        #region BaseLine
        public int? BaseLine
        {
            get { return _baseline; }
            set
            {
                if (_baseline != value)
                {
                    _baseline = value;
                    _isbaselinedirty = true;
                    _isglobaldirty = true;
                }
            }
        }
        public bool IsBaseLineDirty
        {
            get { return _isbaselinedirty; }
            set { _isbaselinedirty = value; }
        }
        #endregion

        #region MarkedBL
        public string MarkedBL
        {
            get { return _markedbl; }
            set
            {
                if (_markedbl != value)
                {
                    _markedbl = value;
                    _ismarkedbldirty = true;
                    _isglobaldirty = true;
                }
            }
        }
        public bool IsMarkedBLDirty
        {
            get { return _ismarkedbldirty; }
            set { _ismarkedbldirty = value; }
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
            set { _isactivedirty = value; }
        }
        #endregion




    }
}
