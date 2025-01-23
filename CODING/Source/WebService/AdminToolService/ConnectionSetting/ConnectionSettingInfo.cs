using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace AdminToolService.ConnectionSetting
{
    public class ConnectionSettingInfo
    {
        #region Variables
        private string _branchId;
        private string _online;
        private string _branch;
        private string _heartbeat;
        private DateTime? _modifiedDt;
        private string _modifiedBy;
        private string _active;
        #endregion

        #region Properties
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }

        public string Online
        {
            get { return _online; }
            set { _online = value; }
        }

        public string Branch
        {
            get { return _branch; }
            set { _branch = value; }
        }

        public string Heartbeat
        {
            get { return _heartbeat; }
            set { _heartbeat = value; }
        }

        public DateTime? ModifiedDt
        {
            get { return _modifiedDt; }
            set { _modifiedDt = value; }
        }

        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        public string Active
        {
            get { return _active; }
            set { _active = value; }
        }
        #endregion

        #region Constructor
        public ConnectionSettingInfo()
        {
        }

        public ConnectionSettingInfo(string branchId, string online, string branch, string heartbeat, DateTime? modifiedDt, string modifiedBy, string active)
        {
            _branchId = branchId;
            _online = online;
            _branch = branch;
            _heartbeat = heartbeat;
            _modifiedDt = modifiedDt;
            _modifiedBy = modifiedBy;
            _active = active;
        }
        #endregion
    }
}