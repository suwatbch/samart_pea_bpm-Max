using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace PEA.BPM.PaymentCollectionModule.DA
{
    public class CalendarDA
    {
        private int timeout = 120;

        public DataTable GetCalendar(string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_Calendar");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            DataSet ds = db.ExecuteDataSet(cmd);
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else 
            {
                return new DataTable();
            }
        }
    }
}
