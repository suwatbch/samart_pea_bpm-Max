using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace AdminToolService.OutOfShift
{
    public class OutOfShiftDA
    {
        private string conn= ConnectionString.Instance.GetConnectionString(AdminToolService.Properties.Settings.Default.OutOfShiftDB);
    
        #region Method : SelectOutOfShiftByDateTime

        public DataTable SelectOutOfShift(DateTime startDt, DateTime endDt,string caseCode)
        {
            DateTime sDt = new DateTime(startDt.Year, startDt.Month, startDt.Day, 0, 0, 0);
            DateTime eDt = new DateTime(endDt.Year, endDt.Month, endDt.Day, 23, 59, 59);
            DataTable dt = new DataTable("OutOfShiftList");
            SqlDataAdapter da;
            SqlConnection sqlConn = new SqlConnection(conn);
            SqlCommand comm;

            if (string.IsNullOrEmpty(caseCode))
            {
                comm = new SqlCommand("SELECT * FROM ts.paymentLog WHERE (CorrectedDt BETWEEN @startDt AND @endDt)", sqlConn);
                comm.Parameters.AddWithValue("@startDt", sDt);
                comm.Parameters.AddWithValue("@endDt", eDt);                
            }
            else
            {
                caseCode = caseCode.Trim().Replace(" ", "");
                comm = new SqlCommand("SELECT * FROM ts.paymentLog WHERE (CorrectedDt BETWEEN @startDt AND @endDt) AND correctedCaseCode = @caseCode", sqlConn);
                comm.Parameters.AddWithValue("@startDt", sDt);
                comm.Parameters.AddWithValue("@endDt", eDt);
                comm.Parameters.AddWithValue("@caseCode", caseCode);
            }

            da = new SqlDataAdapter(comm);
            da.Fill(dt);
            return dt;
        }

        public DateTime GetDBDateTime()
        {
            string query;
            DataTable dt = new DataTable("OfflineErrorLog");

            query = string.Format("SELECT GETDATE()");

            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            da.Fill(dt);
            return (DateTime)dt.Rows[0][0];
        }

        public DataTable SelectCountOutOfShift(DateTime stDt, string caseCode)
        {
            DataTable dt = new DataTable("CountOutOfShiftList");
            SqlDataAdapter da;
            SqlConnection sqlConn = new SqlConnection(conn);
            SqlCommand comm;

            if (string.IsNullOrEmpty(caseCode))
            {
                comm = new SqlCommand("SELECT Count=COUNT(*), Now=getdate() FROM ts.paymentLog WHERE CorrectedDt >= @endDt", sqlConn);
                comm.Parameters.AddWithValue("@endDt", stDt);
            }
            else
            {
                caseCode = caseCode.Trim().Replace(" ", "");
                comm = new SqlCommand("SELECT Count=COUNT(*), Now=getdate() FROM ts.paymentLog WHERE CorrectedDt >= @endDt AND correctedCaseCode = @caseCode", sqlConn);
                comm.Parameters.AddWithValue("@endDt", stDt);
                comm.Parameters.AddWithValue("@caseCode", caseCode);
            }

            da = new SqlDataAdapter(comm);
            da.Fill(dt);
            return dt;
        }

        #endregion
    }
}
