using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading;
using System.Data.Common;
using System.Globalization;
using System.ComponentModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.HelpDeskUnlockMarkFlagModule.Interface.BusinessEntities;
using System;


namespace PEA.BPM.HelpDeskUnlockMarkFlagModule.DA
{
    public class HelpDeskUnlockMarkFlagDA
    {
        public const int CMD_TIMEOUT = 36000; 
     
        public Credential   Login(string userId, string hashPassword)
        {
            Credential conInfoList = new Credential();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("Helpdesk_get_AuthenToken");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "UserId", DbType.String, userId);
            db.AddInParameter(cmd, "HashPassword", DbType.String, hashPassword);          

            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow r in dt.Rows)
            {
                conInfoList.Status  = DaHelper.GetString(r, "Status");
                conInfoList.Token   = DaHelper.GetString(r, "Token");
            }

            return conInfoList;
        }

        public string       CheckToken(string userId, string Token)
        {
            Credential conInfoList = new Credential();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("Helpdesk_get_CheckToken");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "UserId", DbType.String, userId);
            db.AddInParameter(cmd, "Token", DbType.String, Token);

            return db.ExecuteScalar(cmd).ToString();
        }    
      
        public string       UnlockMarkFlagDA(string invoiceNo)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("Helpdesk_get_UnlockMarkflag");
            cmd.CommandTimeout = CMD_TIMEOUT;

            db.AddInParameter(cmd, "InvoiceNo", DbType.String, invoiceNo);           
            
            return db.ExecuteScalar(cmd).ToString();          
        }        

        public SearchARInfo SearchARInfoAR(string caId,string period,double amount)
        {
            SearchARInfo ARInfo = new SearchARInfo();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("Helpdesk_get_ARInfo");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "CaId",          DbType.String, caId);
            db.AddInParameter(cmd, "Period",        DbType.String, period);
            db.AddInParameter(cmd, "GAmount",       DbType.Double, amount);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow r in dt.Rows)
            {
                ARInfo.CaId             = DaHelper.GetString(r, "CaId");
                ARInfo.InvoiceNo        = DaHelper.GetString(r, "InvoiceNo");
                ARInfo.Period           = DaHelper.GetString(r, "Period");
                ARInfo.Amount           = DaHelper.ToMoneyFormat(DaHelper.GetDecimal(r, "GAmount").Value);
                ARInfo.MarkflagStatus   = DaHelper.GetString(r, "Active");  
            }

            return ARInfo;
        }

    }
}
