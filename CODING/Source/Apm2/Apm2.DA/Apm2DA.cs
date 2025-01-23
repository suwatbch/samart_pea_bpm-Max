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
using PEA.BPM.Apm2.Interface.BusinessEntities;
//using PEA.BPM.ApmModule.Interface.BusinessEntities.ReceiptPrinting;
using System;

namespace PEA.BPM.Apm2.DA
{
    public class Apm2DA
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
            DbCommand cmd = db.GetStoredProcCommand("apm_get_AuthenToken");
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
            DbCommand cmd = db.GetStoredProcCommand("apm_get_CheckToken");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "UserId", DbType.String, userId);
            db.AddInParameter(cmd, "Token", DbType.String, Token);

             return db.ExecuteScalar(cmd).ToString();
        }

        public List<SearchInvoiceInfo> SearchInvoiceByCaId(string caId, string TerminalId)
        {
            List<SearchInvoiceInfo> conInfoList = new List<SearchInvoiceInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("apmg2_sel_SearchInvoiceByCaId");
            cmd.CommandTimeout = CMD_TIMEOUT;

            db.AddInParameter(cmd, "pCaId", DbType.String, caId);
            db.AddInParameter(cmd, "pTerminalId", DbType.String, TerminalId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow r in dt.Rows)
            {
                SearchInvoiceInfo conInfo = new SearchInvoiceInfo();
                conInfo.ReturnType  = DaHelper.GetString(r, "ReturnType");
                conInfo.CaId        = DaHelper.GetString(r, "CaId");
                conInfo.CaName      = DaHelper.GetString(r, "CaName");
                conInfo.PmId        = DaHelper.GetString(r, "PmId");
                conInfo.RateTypeId  = DaHelper.GetString(r, "RateTypeId");
                conInfo.DtId        = DaHelper.GetString(r, "DtId");
                conInfo.DtName      = DaHelper.GetString(r, "DtName");
                conInfo.InvoiceNo   = DaHelper.GetString(r, "InvoiceNo");
                conInfo.Period      = DaHelper.GetBillPeriod(DaHelper.GetString(r, "Period"));
                conInfo.DueDate     = DateFormatter.ToDateThString(DaHelper.GetDateTime(r, "DueDate"));
                conInfo.Amount      = DaHelper.ToMoneyFormat(DaHelper.GetDecimal(r, "Amount").Value);
                conInfo.GAmount     = DaHelper.ToMoneyFormat(DaHelper.GetDecimal(r, "GAmount").Value);
                conInfo.Remark      = DaHelper.GetString(r, "Remark");
                //conInfo.PmId        = DaHelper.GetString(r, "PmId");
                //conInfo.RateTypeId  = DaHelper.GetString(r, "RateTypeId");

                conInfoList.Add(conInfo);
            }

            return conInfoList;
        }

        public List<PrintInvoiceInfo> UpdatePaymentStatus(string caId, string InvoiceNo, string TerminalId)
        {
            List<PrintInvoiceInfo> conInfoList = new List<PrintInvoiceInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("apmg2_upd_UpdatePaymentStatus");
            cmd.CommandTimeout = CMD_TIMEOUT;

            db.AddInParameter(cmd, "pCaId", DbType.String, caId);
            db.AddInParameter(cmd, "pInvoiceNo", DbType.String, InvoiceNo);
            db.AddInParameter(cmd, "pTerminalId", DbType.String, TerminalId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow r in dt.Rows)
            {
                PrintInvoiceInfo conInfo = new PrintInvoiceInfo();
                conInfo.BranchId = DaHelper.GetString(r, "BranchId");
                conInfo.BranchName = DaHelper.GetString(r, "BranchName");
                conInfo.BranchAddress = DaHelper.GetString(r, "BranchAddress");
                conInfo.CaName = DaHelper.GetString(r, "CaName");
                conInfo.CaAddress = DaHelper.GetString(r, "CaAddress");
                conInfo.MeterId = DaHelper.GetString(r, "MeterId");
                conInfo.RateTypeId = DaHelper.GetString(r, "RateTypeId");
                conInfo.CaBranchId = DaHelper.GetString(r, "CaBranchId");
                conInfo.CaBranchName = DaHelper.GetString(r, "CaBranchName");
                conInfo.CaId = DaHelper.GetString(r, "CaId");
                conInfo.Period = DaHelper.GetBillPeriod(DaHelper.GetString(r, "Period"));
                conInfo.MeterReadDt = DateFormatter.ToDateThString(DaHelper.GetDateTime(r, "MeterReadDt"));
                conInfo.LastUnit = DaHelper.GetDecimal(r, "LastUnit").Value;
                conInfo.ReadUnit = DaHelper.GetDecimal(r, "ReadUnit").Value;
                conInfo.Qty = DaHelper.GetDecimal(r, "Qty").Value;
                conInfo.BaseAmount = DaHelper.ToMoneyFormat(DaHelper.GetDecimal(r, "BaseAmount").Value);
                conInfo.FTUnitPrice = DaHelper.GetDecimal(r, "FTUnitPrice").Value;
                conInfo.FTAmount = DaHelper.ToMoneyFormat(DaHelper.GetDecimal(r, "FTAmount").Value);
                conInfo.Amount = DaHelper.ToMoneyFormat(DaHelper.GetDecimal(r, "Amount").Value);
                conInfo.TaxCode = DaHelper.GetDecimal(r, "TaxCode").Value;
                conInfo.VatAmount = DaHelper.ToMoneyFormat(DaHelper.GetDecimal(r, "VatAmount").Value);
                conInfo.GAmount = DaHelper.ToMoneyFormat(DaHelper.GetDecimal(r, "GAmount").Value);
                conInfo.PaymentDt = DateFormatter.ToDateTimeThString(DaHelper.GetDateTime(r, "PaymentDt"));
                //conInfo.PaymentDt = DateFormatter.ToDateTimeThString(DaHelper.GetDateTime(r, "PaymentDt"));
                conInfo.ControllerId = DaHelper.GetString(r, "ControllerId");
                conInfo.InvoiceNo = DaHelper.GetString(r, "InvoiceNo");
                conInfo.InvoiceDt = DateFormatter.ToDateThString(DaHelper.GetDateTime(r, "InvoiceDt"));
                conInfo.BusinessArea = DaHelper.GetString(r, "BusinessArea");
                conInfo.DtId = DaHelper.GetString(r, "DtId");
                conInfo.DtName = DaHelper.GetString(r, "DtName");

                conInfoList.Add(conInfo);
            }

            return conInfoList;
        }
    }
}
