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

namespace AdminToolService.Consolidate
{
    public class ConsolidateDA
    {
        private string conn = ConnectionString.Instance.GetConnectionString(AdminToolService.Properties.Settings.Default.ConsolidateDB);

        #region Method : SelectConsolidateByDateTime()
        public DataTable SelectConsolidateByDateTime(DateTime datetime)
        {
            using (SqlConnection sqlconn = new SqlConnection(conn))
            {
                string date = datetime.Year.ToString("0000") + datetime.Month.ToString("00") + datetime.Day.ToString("00");

                SqlCommand sqlcmd = new SqlCommand("ta_sel_consolidate", sqlconn);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandTimeout = AdminToolService.Properties.Settings.Default.CommandTimeout;
                sqlconn.Open();

                SqlCommandBuilder.DeriveParameters(sqlcmd);
                sqlcmd.Parameters["@pDate"].Value = date;

                SqlDataReader dr = sqlcmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dt.TableName = "Consolidate";
                return dt;
            }
        }
        #endregion
    }
}