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

namespace AdminToolService.UnclarifyPayment
{
    public class UnclarifyPaymentDA
    {
        private string conn = ConnectionString.Instance.GetConnectionString(AdminToolService.Properties.Settings.Default.UnclarifyPaymentDB);

        public DataTable SelectUnclarifyPayment(DateTime importDate, string errorType)
        {
            using (SqlConnection sqlconn = new SqlConnection(conn))
            {
                SqlCommand sqlcmd = new SqlCommand("ta_sel_unclarifyPayment", sqlconn);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandTimeout = AdminToolService.Properties.Settings.Default.CommandTimeout;
                sqlconn.Open();

                SqlCommandBuilder.DeriveParameters(sqlcmd);
                sqlcmd.Parameters["@pImportDate"].Value = importDate;
                sqlcmd.Parameters["@pErrorType"].Value = errorType;

                SqlDataReader dr = sqlcmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dt.TableName = "UnclarifyPayment";
                return dt;
            }
        }
    }
}
