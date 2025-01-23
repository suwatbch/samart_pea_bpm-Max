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
using PEA.BPM.NonBankModule.Interface.BusinessEntities;
using System;


namespace PEA.BPM.NonBankModule.DA
{
    public class NonBankDA
    {
        public const int CMD_TIMEOUT = 36000;     

        public Credential Login(string userId, string hashPassword)
        {
            Credential conInfoList = new Credential();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("NonBank_get_AuthenToken");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "UserId", DbType.String, userId);
            db.AddInParameter(cmd, "HashPassword", DbType.String, hashPassword);           

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
            DbCommand cmd = db.GetStoredProcCommand("NonBank_get_CheckToken");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "UserId", DbType.String, userId);
            db.AddInParameter(cmd, "Token", DbType.String, Token);

            return db.ExecuteScalar(cmd).ToString();
        }    
       
        public void SaveNonBankCallLog(string workType)
        {           
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_ins_NonBankCallLog");
            db.AddInParameter(cmd, "WorkType", DbType.String, workType.Trim());
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.ExecuteNonQuery(cmd);           
        }

        public void InsertNonBankTransactionLog(string serviceName, string userId, string token, string caId, string invoiceNo, string agencyId, string serviceResultText)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("tpnb_ins_InsNonBankTransactionLog");
                db.AddInParameter(cmd, "ServiceName", DbType.String, serviceName.Trim());
                db.AddInParameter(cmd, "AuthUserId", DbType.String, userId.Trim());
                db.AddInParameter(cmd, "AuthToken", DbType.String, token.Trim());
                db.AddInParameter(cmd, "CaId", DbType.String, caId.Trim());
                db.AddInParameter(cmd, "InvoiceNo", DbType.String, invoiceNo.Trim());
                db.AddInParameter(cmd, "AgencyId", DbType.String, agencyId.Trim());
                db.AddInParameter(cmd, "ServiceResultText", DbType.String, serviceResultText.Trim());
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {

            }

        }

        public List<SearchContractorServiceInfo> SearchBillInformation(string caId, string billFlag)
        {
            List<SearchContractorServiceInfo> conInfoList = new List<SearchContractorServiceInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("tpnb_get_GetBillInformation");
            cmd.CommandTimeout = CMD_TIMEOUT;

            db.AddInParameter(cmd, "CaId", DbType.String, caId);
            db.AddInParameter(cmd, "BillFlag", DbType.String, billFlag);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    SearchContractorServiceInfo conInfo = new SearchContractorServiceInfo();
                    conInfo.BranchId            = DaHelper.GetString(r, "BranchId")         == null ? "" : DaHelper.GetString(r, "BranchId");
                    conInfo.BranchName          = DaHelper.GetString(r, "BranchName")       == null ? "" : DaHelper.GetString(r, "BranchName");
                    conInfo.CaId                = DaHelper.GetString(r, "CaId")             == null ? "" : DaHelper.GetString(r, "CaId");
                    conInfo.CAPmId              = DaHelper.GetString(r, "CAPmId")           == null ? "" : DaHelper.GetString(r, "CAPmId");
                    conInfo.AccountClassId      = DaHelper.GetString(r, "AccountClassId")   == null ? "" : DaHelper.GetString(r, "AccountClassId");
                    conInfo.AccountClassName    = DaHelper.GetString(r, "AccountClassName") == null ? "" : DaHelper.GetString(r, "AccountClassName");
                    conInfo.MainSub             = DaHelper.GetString(r, "MainSub")          == null ? "" : DaHelper.GetString(r, "MainSub");
                    conInfo.DtName              = DaHelper.GetString(r, "DtName")           == null ? "" : DaHelper.GetString(r, "DtName");
                    conInfo.InvoiceNo           = DaHelper.GetString(r, "InvoiceNo")        == null ? "" : DaHelper.GetString(r, "InvoiceNo");
                    conInfo.CaDoc               = DaHelper.GetString(r, "CaDoc")            == null ? "" : DaHelper.GetString(r, "CaDoc");
                    conInfo.MeterReadDt         = DateFormatter.ToDateThString(DaHelper.GetDateTime(r, "MeterReadDt"))  == null ? "" : DateFormatter.ToDateThString(DaHelper.GetDateTime(r, "MeterReadDt"));
                    conInfo.BillingDt           = DateFormatter.ToDateThString(DaHelper.GetDateTime(r, "BillingDt"))    == null ? "" : DateFormatter.ToDateThString(DaHelper.GetDateTime(r, "BillingDt"));
                    conInfo.DueDt               = DateFormatter.ToDateThString(DaHelper.GetDateTime(r, "DueDt"))        == null ? "" : DateFormatter.ToDateThString(DaHelper.GetDateTime(r, "DueDt"));
                    conInfo.DueDt2              = DateFormatter.ToDateThString(DaHelper.GetDateTime(r, "DueDt2"))       == null ? "" : DateFormatter.ToDateThString(DaHelper.GetDateTime(r, "DueDt2"));
                    conInfo.DunningDt           = DateFormatter.ToDateThString(DaHelper.GetDateTime(r, "DunningDt"))    == null ? "" : DateFormatter.ToDateThString(DaHelper.GetDateTime(r, "DunningDt"));
                    conInfo.Period              = DaHelper.GetString(r, "Period")               == null ? "" : DaHelper.GetString(r, "Period");
                    conInfo.Qty                 = DaHelper.GetDecimal(r, "Qty")                 == null ? 0 : DaHelper.GetDecimal(r, "Qty").Value;
                    conInfo.FTAmount            = DaHelper.GetDecimal(r, "FTAmount")            == null ? 0 : DaHelper.GetDecimal(r, "FTAmount").Value;
                    conInfo.VatRate             = DaHelper.GetDecimal(r, "VatRate")             == null ? 0 : DaHelper.GetDecimal(r, "VatRate").Value;
                    conInfo.VatAmount           = DaHelper.GetDecimal(r, "VatAmount")           == null ? 0 : DaHelper.GetDecimal(r, "VatAmount").Value;
                    conInfo.GAmount             = DaHelper.GetDecimal(r, "GAmount")             == null ? 0 : DaHelper.GetDecimal(r, "GAmount").Value;
                    conInfo.InvestigateFlag     = DaHelper.GetString(r, "InvestigateFlag")      == null ? "" : DaHelper.GetString(r, "InvestigateFlag");
                    conInfo.DisconnectionFlag   = DaHelper.GetString(r, "DisconnectionFlag")    == null ? "" : DaHelper.GetString(r, "DisconnectionFlag");
                    conInfo.GroupInvoiceFlag    = DaHelper.GetString(r, "GroupInvoiceFlag")     == null ? "" : DaHelper.GetString(r, "GroupInvoiceFlag");
                    conInfo.InstallmentFlag     = DaHelper.GetString(r, "InstallmentFlag")      == null ? "" : DaHelper.GetString(r, "InstallmentFlag");
                    conInfo.PaymentOrderFlag    = DaHelper.GetString(r, "PaymentOrderFlag")     == null ? "" : DaHelper.GetString(r, "PaymentOrderFlag");
                    conInfo.ReceiveStatus       = DaHelper.GetString(r, "ReceiveStatus")        == null ? "" : DaHelper.GetString(r, "ReceiveStatus");
                    conInfo.ReceiptId           = DaHelper.GetString(r, "ReceiptId")            == null ? "" : DaHelper.GetString(r, "ReceiptId");
                    conInfo.ReceiptDt           = DateFormatter.ToDateThString(DaHelper.GetDateTime(r, "ReceiptDt")) == null ? "" : DateFormatter.ToDateThString(DaHelper.GetDateTime(r, "ReceiptDt"));
                    conInfo.ReceiptCh           = DaHelper.GetString(r, "ReceiptCh")            == null ? "" : DaHelper.GetString(r, "ReceiptCh");
                    conInfo.ReceiptBranchId     = DaHelper.GetString(r, "ReceiptBranchId")      == null ? "" : DaHelper.GetString(r, "ReceiptBranchId");
                    conInfo.ReceiptBranchName   = DaHelper.GetString(r, "ReceiptBranchName")    == null ? "" : DaHelper.GetString(r, "ReceiptBranchName");
                    conInfo.TaxId               = DaHelper.GetString(r, "TaxId")                == null ? "" : DaHelper.GetString(r, "TaxId");
                    conInfo.TaxBranch           = DaHelper.GetString(r, "TaxBranch")            == null ? "" : DaHelper.GetString(r, "TaxId"); 
                    conInfo.Remark              = "";

                    conInfoList.Add(conInfo);
                }
            }
            else
            {
                SearchContractorServiceInfo conInfo = new SearchContractorServiceInfo();
                conInfo.BranchId            = "";
                conInfo.BranchName          = "";
                conInfo.CaId                = "";
                conInfo.CAPmId              = "";
                conInfo.AccountClassId      = "";
                conInfo.AccountClassName    = "";
                conInfo.MainSub             = "";
                conInfo.DtName              = "";
                conInfo.InvoiceNo           = "";
                conInfo.CaDoc               = "";
                conInfo.MeterReadDt         = "";
                conInfo.BillingDt           = "";
                conInfo.DueDt               = "";
                conInfo.DueDt2              = "";
                conInfo.DunningDt           = "";
                conInfo.Period              = "";
                conInfo.Qty                 = Convert.ToDecimal(0.0);
                conInfo.FTAmount            = Convert.ToDecimal(0.0);
                conInfo.VatRate             = Convert.ToDecimal(0.0);
                conInfo.VatAmount           = Convert.ToDecimal(0.0);
                conInfo.GAmount             = Convert.ToDecimal(0.0);
                conInfo.InvestigateFlag     = "";                               //Uthen 14-07-2017
                conInfo.DisconnectionFlag   = "";
                conInfo.GroupInvoiceFlag    = "";
                conInfo.InstallmentFlag     = "";
                conInfo.PaymentOrderFlag    = "";
                conInfo.ReceiveStatus       = "";
                conInfo.ReceiptId           = "";
                conInfo.ReceiptDt           = "";
                conInfo.ReceiptCh           = "";
                conInfo.ReceiptBranchId     = "";
                conInfo.ReceiptBranchName   = "";
                conInfo.TaxId               = "";
                conInfo.TaxBranch           = "";
                conInfo.Remark              = "";

                conInfoList.Add(conInfo);
            }            

            return conInfoList;
        }        

        public List<SearchConAccountServiceInfo> SearchContractorInformation(string caId)
        {
            List<SearchConAccountServiceInfo> conAccountInfoList = new List<SearchConAccountServiceInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("tpnb_get_GetContractorInformation");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "CaId", DbType.String, caId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    SearchConAccountServiceInfo conAcountInfo = new SearchConAccountServiceInfo();
                    conAcountInfo.BranchId          = DaHelper.GetString(r, "BranchId")     == null ? "" : DaHelper.GetString(r, "BranchId");
                    conAcountInfo.BranchName        = DaHelper.GetString(r, "BranchName")   == null ? "" : DaHelper.GetString(r, "BranchName");
                    conAcountInfo.CaName            = DaHelper.GetString(r, "CaName")       == null ? "" : DaHelper.GetString(r, "CaName");
                    conAcountInfo.CaAddress         = DaHelper.GetString(r, "CaAddress")    == null ? "" : DaHelper.GetString(r, "CaAddress");
                    conAcountInfo.AccountClassId    = DaHelper.GetString(r, "AccountClassId")   == null ? "" : DaHelper.GetString(r, "AccountClassId");
                    conAcountInfo.AccountClassName  = DaHelper.GetString(r, "AccountClassName") == null ? "" : DaHelper.GetString(r, "AccountClassName");
                    conAcountInfo.MeterSizeName     = DaHelper.GetString(r, "MeterSizeName")    == null ? "" : DaHelper.GetString(r, "MeterSizeName");
                    conAcountInfo.MeterInstallDt    = DateFormatter.ToDateThString(DaHelper.GetDateTime(r, "MeterInstallDt")) == null ? "" : DateFormatter.ToDateThString(DaHelper.GetDateTime(r, "MeterInstallDt"));
                    conAcountInfo.Remark            = "";
                    conAccountInfoList.Add(conAcountInfo);
                }
            }
            else
            {
                SearchConAccountServiceInfo conAcountInfo = new SearchConAccountServiceInfo();
                conAcountInfo.BranchId                  = "";
                conAcountInfo.BranchName                = "";
                conAcountInfo.CaName                    = "";
                conAcountInfo.CaAddress                 = "";
                conAcountInfo.AccountClassId            = "";
                conAcountInfo.AccountClassName          = "";
                conAcountInfo.MeterSizeName             = "";
                conAcountInfo.MeterInstallDt            = "";
                conAcountInfo.Remark                    = "";
                conAccountInfoList.Add(conAcountInfo);
            }
            return conAccountInfoList;
        }

        public string UpdateBillMarkFlagData(string caId, string invoiceNo, string agencyId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("tpnb_upd_UpdateARPayment");
            cmd.CommandTimeout = CMD_TIMEOUT;

            db.AddOutParameter(cmd, "oResult", DbType.String, 20);
            db.AddInParameter(cmd, "pCaId ", DbType.String, caId);
            db.AddInParameter(cmd, "pInvoiceNo", DbType.String, invoiceNo);
            db.AddInParameter(cmd, "pAgencyId", DbType.String, agencyId);
            db.ExecuteNonQuery(cmd).ToString();

            string result = (string)db.GetParameterValue(cmd, "oResult");
            return result;
        }

        //----------------Option---------------

        public List<int> GetNonBankSetting()
        {
            //var lstNonBankSetting = new List<int>();

            //Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            //DbCommand cmd = db.GetStoredProcCommand("ta_get_NonBankSetting");
            //cmd.CommandTimeout = CMD_TIMEOUT;           
            //DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            //foreach (DataRow r in dt.Rows)
            //{
            //    var NonBankQueueLimitSetting = Convert.ToInt32(r["NonBankQueueLimitSetting"].ToString());
            //    var NonBankTimeDelaySetting = Convert.ToInt32(r["NonBankTimeDelaySetting"].ToString());

            //    lstNonBankSetting.Add(NonBankQueueLimitSetting);
            //    lstNonBankSetting.Add(NonBankTimeDelaySetting);
            //}

            //return lstNonBankSetting;
            List<int> li = new List<int>();
            li.Add(100);
            li.Add(200);
            return li;
        }
        
        public List<PrintInvoiceInfo> UpdatePaymentStatus(string caId, string InvoiceNo, string TerminalId, string User, string Password)
        {
            List<PrintInvoiceInfo> conInfoList = new List<PrintInvoiceInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("NonBank_upd_UpdatePaymentStatus");
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

        public List<PrintInvoiceInfo> CancelPaymentStatus(string caId, string InvoiceNo, string TerminalId, string User, string Password)
        {
            List<PrintInvoiceInfo> conInfoList = new List<PrintInvoiceInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("NonBank_upd_CancelPaymentStatus");
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

        

    }
}
