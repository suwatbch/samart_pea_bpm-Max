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
using PEA.BPM.QueueModule.Interface.BusinessEntities;
//using PEA.BPM.QueueModule.Interface.BusinessEntities.ReceiptPrinting;
using System;

namespace PEA.BPM.QueueModule.DA
{
    public class QueueDA
    { 
        public const int CMD_TIMEOUT = 36000; // 10 hr

        //public string InsertIntoPayment(DbTransaction trans, DateTime paymentDt, string branchId, string postServerId, string modifiedBy)
        //{
        //    Database db = DatabaseFactory.CreateDatabase("POSDatabase");
        //    DbCommand cmd = db.GetStoredProcCommand("ePay_ins_Payment");

        //    db.AddInParameter(cmd, "PaymentDt", DbType.DateTime, paymentDt);
        //    db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
        //    db.AddInParameter(cmd, "PostedServerId", DbType.String, postServerId);
        //    db.AddInParameter(cmd, "ModifiedBy", DbType.String, modifiedBy);

        //    return db.ExecuteScalar(cmd, trans).ToString();
        //}
        public Credential Login(string userId, string hashPassword, string localIP)
        {
            Credential conInfoList = new Credential();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("queue_get_AuthenToken");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "UserId", DbType.String, userId);
            db.AddInParameter(cmd, "HashPassword", DbType.String, hashPassword);
            db.AddInParameter(cmd, "TerminalIP", DbType.String, localIP);

            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow r in dt.Rows)
            {
                conInfoList.Status = DaHelper.GetString(r, "Status");
                conInfoList.Token = DaHelper.GetString(r, "Token");

            }

            return conInfoList;
        }

        public string CheckToken(string userId, string Token)
        {
            Credential conInfoList = new Credential();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("queue_get_CheckToken");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "UserId", DbType.String, userId);
            db.AddInParameter(cmd, "Token", DbType.String, Token);

             return db.ExecuteScalar(cmd).ToString();
        }

        public List<SearchInvoiceInfo> SearchInvoiceByCaId(string caId, string TerminalId)
        {
            List<SearchInvoiceInfo> conInfoList = new List<SearchInvoiceInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("queue_sel_SearchInvoiceByCaId");
            cmd.CommandTimeout = CMD_TIMEOUT;

            db.AddInParameter(cmd, "pCaId", DbType.String, caId);
            db.AddInParameter(cmd, "pTerminalId", DbType.String, TerminalId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow r in dt.Rows)
            {
                SearchInvoiceInfo conInfo = new SearchInvoiceInfo();
                conInfo.ReturnType = DaHelper.GetString(r, "ReturnType");
                conInfo.CaId = DaHelper.GetString(r, "CaId");
                conInfo.CaName = DaHelper.GetString(r, "CaName");
                conInfo.CaAddress = DaHelper.GetString(r, "CaAddress");
                conInfo.InvoiceNo = DaHelper.GetString(r, "InvoiceNo");
                conInfo.Period = DaHelper.GetBillPeriod(DaHelper.GetString(r, "Period"));
                conInfo.DueDate = DateFormatter.ToDateThString(DaHelper.GetDateTime(r, "DueDate"));
                conInfo.DebtType = DaHelper.GetString(r, "DebtType");
                conInfo.Amount = DaHelper.ToMoneyFormat(DaHelper.GetDecimal(r, "Amount").Value);
                conInfo.Remark = DaHelper.GetString(r, "Remark");

                conInfoList.Add(conInfo);
            }

            return conInfoList;
        }
    }
}
