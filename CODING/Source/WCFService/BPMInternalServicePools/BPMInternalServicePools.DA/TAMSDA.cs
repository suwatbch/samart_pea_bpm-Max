using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;

namespace BPMInternalServicePools.DA
{
    public class TAMSDA
    {
        private const int CMD_TIMEOUT = 36000; 

        // List<SearchContractorServiceInfo> conInfoList = new List<SearchContractorServiceInfo>();
        // Database db = DatabaseFactory.CreateDatabase("POSDatabase");
        // DbCommand cmd = db.GetStoredProcCommand("tpnb_get_GetBillInformation");
        // cmd.CommandTimeout = CMD_TIMEOUT;

        // db.AddInParameter(cmd, "CaId", DbType.String, caId);
        // db.AddInParameter(cmd, "BillFlag", DbType.String, billFlag);
        // DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
        //public void InsertTransactionToBPMOnetouchLog()
        //{                
        //    Database db = DatabaseFactory.CreateDatabase("POSDatabase");
        //    DbCommand cmd = db.GetStoredProcCommand("tpnb_ins_InsNonBankTransactionLog");
        //    db.AddInParameter(cmd, "ServiceName", DbType.String, serviceName.Trim());
        //    db.AddInParameter(cmd, "AuthUserId", DbType.String, userId.Trim());
        //    db.AddInParameter(cmd, "AuthToken", DbType.String, token.Trim());
        //    db.AddInParameter(cmd, "CaId", DbType.String, caId.Trim());
        //    db.AddInParameter(cmd, "InvoiceNo", DbType.String, invoiceNo.Trim());
        //    db.AddInParameter(cmd, "AgencyId", DbType.String, agencyId.Trim());
        //    db.AddInParameter(cmd, "ServiceResultText", DbType.String, serviceResultText.Trim());
        //    cmd.CommandTimeout = CMD_TIMEOUT;
        //    db.ExecuteNonQuery(cmd); 
        //}

        //public void InsertTransactionToOnetouchLog()
        //{
        //    Database db = DatabaseFactory.CreateDatabase("LOGDatabase");
        //    DbCommand cmd = db.GetStoredProcCommand("tpnb_ins_InsNonBankTransactionLog");
        //    db.AddInParameter(cmd, "ServiceName", DbType.String, serviceName.Trim());
        //    db.AddInParameter(cmd, "AuthUserId", DbType.String, userId.Trim());
        //    db.AddInParameter(cmd, "AuthToken", DbType.String, token.Trim());
        //    db.AddInParameter(cmd, "CaId", DbType.String, caId.Trim());
        //    db.AddInParameter(cmd, "InvoiceNo", DbType.String, invoiceNo.Trim());
        //    db.AddInParameter(cmd, "AgencyId", DbType.String, agencyId.Trim());
        //    db.AddInParameter(cmd, "ServiceResultText", DbType.String, serviceResultText.Trim());
        //    cmd.CommandTimeout = CMD_TIMEOUT;
        //    db.ExecuteNonQuery(cmd);
        //}
        
    }
}
