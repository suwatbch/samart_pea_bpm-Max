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

namespace AdminToolService.CloseAccountStatus
{
    public class CloseAccountStatusDA
    {
        private string conn = ConnectionString.Instance.GetConnectionString(AdminToolService.Properties.Settings.Default.CloseAccountStatusDB);

        #region Method : SelectCloseAccountStatusByDateTimeStatus()
        public DataTable SelectCloseAccountStatusByDateTimeStatus(DateTime datetime, string status, string region)
        {
            using (SqlConnection sqlconn = new SqlConnection(conn))
            {
                SqlCommand sqlcmd = new SqlCommand("cm_sel_BLStatus", sqlconn);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandTimeout = AdminToolService.Properties.Settings.Default.CommandTimeout;
                sqlconn.Open();

                SqlCommandBuilder.DeriveParameters(sqlcmd);
                sqlcmd.Parameters["@pDate"].Value = datetime;
                sqlcmd.Parameters["@pStatus"].Value = status;

                SqlDataReader dr = sqlcmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dt.TableName = "CloseAccountStatus";
                return dt;
            }
        }
        #endregion
    }
}