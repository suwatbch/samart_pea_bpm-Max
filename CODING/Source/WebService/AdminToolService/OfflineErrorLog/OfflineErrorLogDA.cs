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

namespace AdminToolService.OfflineErrorLog
{
    public class OfflineErrorLogDA
    {
        private string conn = ConnectionString.Instance.GetConnectionString(AdminToolService.Properties.Settings.Default.OfflineErrorLogDB);

        #region Method : SelectOfflineErrorLogByDateTimeActive()
        public DataTable SelectOfflineErrorLogByDateTimeActive(DateTime datetime, string active)
        {
            string query;
            DateTime startday = new DateTime(datetime.Year, datetime.Month, datetime.Day, 0, 0, 0);
            DateTime endday = new DateTime(datetime.Year, datetime.Month, datetime.Day, 23, 59, 59);
            DataTable dt = new DataTable("OfflineErrorLog");

            if (string.IsNullOrEmpty(active))
            {
                query = string.Format("SELECT * FROM ta.GlobalOfflineLog WHERE (ModifiedDt BETWEEN '{0}' AND '{1}')", startday.ToString("s"), endday.ToString("s"));
            }
            else
            {
                query = string.Format("SELECT * FROM ta.GlobalOfflineLog WHERE (ModifiedDt BETWEEN '{0}' AND '{1}') AND (Active = '{2}')", startday.ToString("s"), endday.ToString("s"), active.Trim());
            }

            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            da.Fill(dt);
            return dt;
        }

        public void UpdateLogStatus(string fileName, string status)
        {
            DataTable dt = new DataTable("OfflineErrorLog");
            string query = string.Format("SELECT Active FROM ta.GlobalOfflineLog WHERE LogFileName = '{0}'", fileName);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                DataRow editrow = dt.Rows[0];                
                editrow["Active"] = status;
            }
            else
            {
                throw new Exception("ConnectionSetting not found.");
            }

            SqlCommandBuilder sqlcmb = new SqlCommandBuilder(da);
            da.UpdateCommand = sqlcmb.GetUpdateCommand();

            using (TransactionScope ts = new TransactionScope())
            {
                da.Update(dt);
                ts.Complete();
            }
        }

        #endregion
    }
}