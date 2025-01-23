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
    public class ConnectionSettingBS
    {
        #region Method : GetConnectionSettingDisplay()
        public DataTable GetConnectionSettingDisplay(string branchId, string active)
        {
            try
            {
                ConnectionSettingDA da = new ConnectionSettingDA();
                DataTable dt = da.SelectConnectionSettingByBranchIdActive(branchId, active);
                return dt;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        #endregion

        #region Method : GetConnectionSettingInfo()
        public ConnectionSettingInfo GetConnectionSettingInfo(string branchId)
        {
            try
            {
                ConnectionSettingDA da = new ConnectionSettingDA();
                ConnectionSettingInfo csi = da.GetConnectionSettingInfo(branchId);
                return csi;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        #endregion

        #region Method : AddConnectionSetting()
        public int AddConnectionSetting(ConnectionSettingInfo connectionSettingInfo)
        {
            try
            {
                ConnectionSettingDA da = new ConnectionSettingDA();
                int ret = da.AddConnectionSetting(connectionSettingInfo);
                return ret;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        #endregion

        #region Method : UpdateConnectionSetting()
        public int UpdateConnectionSetting(string targetBranchId, ConnectionSettingInfo connectionSettingInfo)
        {
            try
            {
                ConnectionSettingDA da = new ConnectionSettingDA();
                int ret = da.UpdateConnectionSetting(targetBranchId, connectionSettingInfo);
                return ret;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        #endregion

        #region Method : DeleteConnectionSettingByBranchIdList()
        public int DeleteConnectionSettingByBranchIdList(string branchIdList)
        {
            try
            {
                ConnectionSettingDA da = new ConnectionSettingDA();
                int ret = da.DeleteConnectionSettingByBranchIdList(branchIdList);
                return ret;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        #endregion
    }
}