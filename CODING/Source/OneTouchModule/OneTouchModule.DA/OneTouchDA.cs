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
using PEA.BPM.OneTouchModule.Interface.BusinessEntities;
//using PEA.BPM.OneTouchModule.Interface.BusinessEntities.ReceiptPrinting;
using System;

namespace PEA.BPM.OneTouchModule.DA
{
    public class OneTouchDA
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
            DbCommand cmd = db.GetStoredProcCommand("OneTouch_get_AuthenToken");
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
            DbCommand cmd = db.GetStoredProcCommand("OneTouch_get_CheckToken");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "UserId", DbType.String, userId);
            db.AddInParameter(cmd, "Token", DbType.String, Token);

             return db.ExecuteScalar(cmd).ToString();
        }

        public List<OneTouchInfo> SearchPayment(string NotificationNo)
        {
            List<OneTouchInfo> conInfoList = new List<OneTouchInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("onetouch_sel_Payment");
            cmd.CommandTimeout = CMD_TIMEOUT;

            db.AddInParameter(cmd, "NotificationNo", DbType.String, NotificationNo);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow r in dt.Rows)
            {
                OneTouchInfo conInfo = new OneTouchInfo();
                conInfo.NotificationNo = DaHelper.GetString(r, "NotificationNo");
                conInfo.InvoiceNo = DaHelper.GetString(r, "InvoiceNo");
                conInfo.ReceiptId = DaHelper.GetString(r, "ReceiptId");
                conInfo.DebtId = DaHelper.GetString(r, "DtId");
                conInfo.GAmount = DaHelper.GetDecimal(r, "Amount");

                conInfoList.Add(conInfo);
            }

            return conInfoList;
        }

        public string UpdatePayment(string NotificationNo, string InvoiceNo, string ReceiptId, string DebtId)
        {
   
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("onetouch_upd_payment");
            cmd.CommandTimeout = CMD_TIMEOUT;

            db.AddInParameter(cmd, "NotificationNo", DbType.String, NotificationNo);
            db.AddInParameter(cmd, "InvoiceNo", DbType.String, InvoiceNo);
            db.AddInParameter(cmd, "ReceiptId", DbType.String, ReceiptId);
            db.AddInParameter(cmd, "DebtId", DbType.String, DebtId);

            return db.ExecuteScalar(cmd).ToString();
        }
    }
}
