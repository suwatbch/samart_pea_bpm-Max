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
using System.Transactions;

namespace AdminToolService.ConnectionSetting
{
    public class ConnectionSettingDA
    {
        private string conn = ConnectionString.Instance.GetConnectionString(AdminToolService.Properties.Settings.Default.ConnectionSettingDB);

        #region Method : SelectConnectionSettingByBranchIdActive()
        public DataTable SelectConnectionSettingByBranchIdActive(string branchId, string active)
        {
            DataTable dt = new DataTable("ConnectionSetting");
            string query = "SELECT * FROM ta.ConnectionSetting WHERE ";
            query += "((BranchId LIKE @branchid) OR (@branchid IS NULL)) AND ";
            query += "((Active = @active) OR (@active IS NULL))";

            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            da.SelectCommand.Parameters.Add("@branchid", SqlDbType.Char);
            da.SelectCommand.Parameters.Add("@active", SqlDbType.Char);
            da.SelectCommand.Parameters["@branchid"].IsNullable = true;
            da.SelectCommand.Parameters["@active"].IsNullable = true;

            if (string.IsNullOrEmpty(branchId))
            {
                da.SelectCommand.Parameters["@branchid"].Value = DBNull.Value;
            }
            else
            {
                da.SelectCommand.Parameters["@branchid"].Value = branchId;
            }

            if (string.IsNullOrEmpty(active))
            {
                da.SelectCommand.Parameters["@active"].Value = DBNull.Value;
            }
            else
            {
                da.SelectCommand.Parameters["@active"].Value = active;
            }

            da.Fill(dt);
            return dt;
        }
        #endregion

        #region Method : GetConnectionSettingInfo()
        public ConnectionSettingInfo GetConnectionSettingInfo(string branchId)
        {
            DataTable dt = new DataTable("ConnectionSettingInfo");
            string query = string.Format("SELECT * FROM ta.ConnectionSetting WHERE BranchId = '{0}'", branchId.Trim());
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                string online;
                string branch;
                string heartbeat;
                DateTime? modifieddt;
                string modifiedby;
                string active;

                online = dr["Online"].ToString();
                branch = dr["Branch"].ToString();
                heartbeat = dr["Heartbeat"].ToString();
                if (dr["ModifiedDt"] == DBNull.Value) modifieddt = null; else modifieddt = (DateTime)dr["ModifiedDt"];
                modifiedby = dr["ModifiedBy"].ToString();
                active = dr["Active"].ToString();

                return new ConnectionSettingInfo(branchId, online, branch, heartbeat, modifieddt, modifiedby, active);
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Method : AddConnectionSetting()
        public int AddConnectionSetting(ConnectionSettingInfo connectionSettingInfo)
        {
            DataTable dt = new DataTable("ConnectionSetting");
            string query = string.Format("SELECT TOP(0) * FROM ta.ConnectionSetting");
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            da.Fill(dt);

            DataRow newrow = dt.NewRow();
            newrow["BranchId"] = connectionSettingInfo.BranchId;
            newrow["Online"] = connectionSettingInfo.Online;
            newrow["Branch"] = connectionSettingInfo.Branch;
            newrow["Heartbeat"] = connectionSettingInfo.Heartbeat;
            newrow["ModifiedBy"] = connectionSettingInfo.ModifiedBy;
            newrow["ModifiedDt"] = connectionSettingInfo.ModifiedDt;
            newrow["Active"] = connectionSettingInfo.Active;
            dt.Rows.Add(newrow);

            SqlCommandBuilder sqlcmb = new SqlCommandBuilder(da);
            da.InsertCommand = sqlcmb.GetInsertCommand();

            int ret = -1;

            using (TransactionScope ts = new TransactionScope())
            {
                ret = da.Update(dt);
                ts.Complete();
            }

            return ret;
        }
        #endregion

        #region Method : UpdateConnectionSetting()
        public int UpdateConnectionSetting(string targetBranchId, ConnectionSettingInfo connectionSettingInfo)
        {
            DataTable dt = new DataTable("ConnectionSetting");
            string query = string.Format("SELECT * FROM ta.ConnectionSetting WHERE BranchId = '{0}'", targetBranchId);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                DataRow editrow = dt.Rows[0];
                editrow["BranchId"] = connectionSettingInfo.BranchId;
                editrow["Online"] = connectionSettingInfo.Online;
                editrow["Branch"] = connectionSettingInfo.Branch;
                editrow["Heartbeat"] = connectionSettingInfo.Heartbeat;
                editrow["ModifiedBy"] = connectionSettingInfo.ModifiedBy;
                editrow["ModifiedDt"] = connectionSettingInfo.ModifiedDt;
                editrow["Active"] = connectionSettingInfo.Active;
            }
            else
            {
                throw new Exception("ConnectionSetting not found.");
            }

            SqlCommandBuilder sqlcmb = new SqlCommandBuilder(da);
            da.UpdateCommand = sqlcmb.GetUpdateCommand();

            int ret = -1;

            using (TransactionScope ts = new TransactionScope())
            {
                ret = da.Update(dt);
                ts.Complete();
            }

            return ret;
        }
        #endregion

        #region Method : DeleteConnectionSettingByBranchIdList()
        public int DeleteConnectionSettingByBranchIdList(string branchIdList)
        {
            int ret = -1;
            using (SqlConnection connection = new SqlConnection(conn))
            {
                SqlCommand sqlcmd = new SqlCommand(string.Format("DELETE FROM ta.ConnectionSetting WHERE BranchId IN ({0})", branchIdList), connection);
                sqlcmd.Connection.Open();

                using (TransactionScope ts = new TransactionScope())
                {
                    ret = sqlcmd.ExecuteNonQuery();
                    ts.Complete();
                }
            }
            return ret;
        }
        #endregion
    }
}