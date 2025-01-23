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
using PEA.BPM.EcsModule.Interface.BusinessEntities;
using System;


namespace PEA.BPM.EcsModule.DA
{
    public class EcsDA
    {
        public const int CMD_TIMEOUT = 36000; 
     
        public Credential Login(string userId, string hashPassword, string localIP)
        {
            Credential conInfoList = new Credential();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ecs_get_AuthenToken");
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
            DbCommand cmd = db.GetStoredProcCommand("ecs_get_CheckToken");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "UserId", DbType.String, userId);
            db.AddInParameter(cmd, "Token", DbType.String, Token);

            return db.ExecuteScalar(cmd).ToString();
        }
    
        public string UpdatePaymentStatus(string caId, string InvoiceNo, string TerminalId,string User,string Password)
        {
            List<PrintInvoiceInfo> conInfoList = new List<PrintInvoiceInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ecs_upd_UpdatePaymentStatus");
            cmd.CommandTimeout = CMD_TIMEOUT;

            db.AddOutParameter(cmd, "Result", DbType.String, 20);
            db.AddInParameter(cmd, "pCaId", DbType.String, caId);
            db.AddInParameter(cmd, "pInvoiceNo", DbType.String, InvoiceNo);
            db.AddInParameter(cmd, "pTerminalId", DbType.String, TerminalId);
            db.AddInParameter(cmd, "@pUserName", DbType.String, User);
            db.AddInParameter(cmd, "@pPassword", DbType.String, Password);
            db.ExecuteNonQuery(cmd).ToString();
            string result = (string)db.GetParameterValue(cmd, "Result");
            return result;

            #region OLD
            /*
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

                conInfoList.Add(conInfo);
            }

            return conInfoList;
             */
            #endregion
        }

        public List<PrintInvoiceInfo> CancelPaymentStatus(string caId, string InvoiceNo, string TerminalId, string User, string Password)
        {
            List<PrintInvoiceInfo> conInfoList = new List<PrintInvoiceInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ecs_upd_CancelPaymentStatus");
            cmd.CommandTimeout = CMD_TIMEOUT;

            db.AddInParameter(cmd, "pCaId", DbType.String, caId);
            db.AddInParameter(cmd, "pInvoiceNo", DbType.String, InvoiceNo);
            db.AddInParameter(cmd, "pTerminalId", DbType.String, TerminalId);
            db.AddInParameter(cmd, "@pUserName", DbType.String, User);
            db.AddInParameter(cmd, "@pPassword", DbType.String, Password);
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

                conInfoList.Add(conInfo);
            }

            return conInfoList;
        }

        public List<int> GetEcsSetting()
        {
            var lstEcsSetting = new List<int>();

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_get_EcsSetting");
            cmd.CommandTimeout = CMD_TIMEOUT;           
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow r in dt.Rows)
            {
                var ecsQueueLimitSetting = Convert.ToInt32(r["EcsQueueLimitSetting"].ToString());
                var ecsTimeDelaySetting = Convert.ToInt32(r["EcsTimeDelaySetting"].ToString());

                lstEcsSetting.Add(ecsQueueLimitSetting);
                lstEcsSetting.Add(ecsTimeDelaySetting);
            }

            return lstEcsSetting;
        }

        public void SaveEcsCallLog(string workType)
        {           
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_ins_EcsCallLog");
            db.AddInParameter(cmd, "WorkType", DbType.String, workType.Trim());
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.ExecuteNonQuery(cmd);           
        }
    }
}
