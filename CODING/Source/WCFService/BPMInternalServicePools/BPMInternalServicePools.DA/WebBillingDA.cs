using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BPMInternalServicePools.Model;  
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;

namespace BPMInternalServicePools.DA
{
    public class WebBillingDA
    {
        private const int CMD_TIMEOUT = 36000; 
        
        public List<WebBillingModel> GetBillingByCaid(string caid)
        {
            Database db         = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd       = db.GetStoredProcCommand("ta_sel_ARonWeb");
            db.AddInParameter(cmd, "CaId", DbType.String, caid.Trim());            
            cmd.CommandTimeout  = CMD_TIMEOUT;    
            DataTable result    = db.ExecuteDataSet(cmd).Tables[0];

            var BillingResult = new List<WebBillingModel>();   

            foreach(DataRow r in result.Rows)
            {
                var m = new WebBillingModel();
                m.Caid      = r["CaId"].ToString();
                m.InvoiceNo = r["InvoiceNo"].ToString();
                m.CaName    = r["CaName"].ToString();
                m.CaAddress = r["CaAddress"].ToString();
                m.Period    = r["Period"].ToString();
                m.GAmount   = (float)Convert.ToDouble(r["GAmount"].ToString());
                m.Active    = "1";

                BillingResult.Add(m);
            }

            return BillingResult;           
        }
    }
}
