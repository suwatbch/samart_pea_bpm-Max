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
using PEA.BPM.SmartPlus.Interface.BusinessEntities;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using System;


namespace PEA.BPM.SmartPlus.DA
{
    public class SmartPlusDA
    {
        public const int CMD_TIMEOUT = 36000;


        #region ..GET AR INFORMATION 2022-05-23 SEPERATE SERVICE FROM MAIN BPM (DA)

        public bool SmartplusLogAndFilter(string serviceName, string caId, int timeDiff)
        {
            bool Result = false;
            try
            {            
                ///Database db             = DatabaseFactory.CreateDatabase("POSDatabase");
                Database db             = DatabaseFactory.CreateDatabase("ADTDatabase");                
                DbCommand cmd           = db.GetStoredProcCommand("SmartPlus_Ins_LogAndFilter");
                cmd.CommandTimeout      = CMD_TIMEOUT;
                db.AddOutParameter(cmd, "Result",       DbType.String, 20);
                db.AddInParameter(cmd,  "ServiceName",  DbType.String, serviceName);
                db.AddInParameter(cmd,  "CaId",         DbType.String, caId);
                db.AddInParameter(cmd,  "TimeDiff",     DbType.Int32, timeDiff);      
                db.ExecuteNonQuery(cmd).ToString();

                string res              = (string)db.GetParameterValue(cmd, "Result");

                if (res.ToUpper() == "SUCCESS")
                {
                    Result = true;
                }
                else
                {
                    Result = false;
                }
            }
            catch (Exception ex)
            {
                Result = true;
            }            

            return Result;
        }

        public List<Invoice> SearchInvoiceByCustomerId(string caId, bool isSearchByBP)
        {
            Database db         = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd       = db.GetStoredProcCommand("pc_sel_ARInvoiceByCaId_SmartPlus");
            cmd.CommandTimeout  = CMD_TIMEOUT;
            db.AddInParameter(cmd, "CaId", DbType.String, caId);
            db.AddInParameter(cmd, "IsSearchByBP", DbType.Boolean, isSearchByBP);
            DataTable dt        = db.ExecuteDataSet(cmd).Tables[0];

            return ConvertToInvoiceList(dt, true);
        }

        public List<Bill> SearchBillByInvoiceNo(string invoiceNo, Boolean chkIsPaid)
        {
            Database db         = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd       = db.GetStoredProcCommand("pc_sel_ARByInvoiceNo_Smartplus");
            cmd.CommandTimeout  = CMD_TIMEOUT;
            db.AddInParameter(cmd, "InvoiceNo", DbType.String, invoiceNo);
            db.AddInParameter(cmd, "ChkIsPaid", DbType.Boolean, chkIsPaid);
            DataTable dt        = db.ExecuteDataSet(cmd).Tables[0];

            return ConvertToBillList(dt);
        }

        private List<Invoice> ConvertToInvoiceList(DataTable dt, Boolean chkIsPaid)
        {
            List<Invoice> invoices = new List<Invoice>();
            foreach (DataRow dr in dt.Rows)
            {
                Invoice inv         = new Invoice();
                inv.InvoiceNo       = DaHelper.GetString(dr, "InvoiceNo");
                inv.InvoiceDate     = DaHelper.GetDate(dr, "InvoiceDt");
                inv.BranchId        = DaHelper.GetString(dr, "BranchId");
                inv.TechBranchName  = DaHelper.GetString(dr, "TechBranchName");
                inv.CommBranchId    = DaHelper.GetString(dr, "CommBranchId");
                inv.CommBranchName  = DaHelper.GetString(dr, "CommBranchName");
                inv.CaId            = DaHelper.GetString(dr, "CaId");
                inv.BpId            = DaHelper.GetString(dr, "BpId");
                inv.Name            = DaHelper.GetString(dr, "CaName");
                inv.PayByName       = DaHelper.GetString(dr, "ReceiptPrintName");
                inv.PmId            = DaHelper.GetString(dr, "PmId");
                inv.Address         = DaHelper.GetString(dr, "CaAddress");
                inv.GroupInvoiceReceiptType = DaHelper.GetString(dr, "GroupIvReceiptType");
                inv.DueDate         = DaHelper.GetDate(dr, "DueDt");

                inv.Qty             = DaHelper.GetDecimal(dr, "Qty");
                inv.AmountExVat     = DaHelper.GetDecimal(dr, "ExVatAmount");
                inv.VatAmount       = DaHelper.GetDecimal(dr, "VatAmount");
                inv.GAmount         = DaHelper.GetDecimal(dr, "GAmount");

                inv.PaidQty         = DaHelper.GetDecimal(dr, "PaidQty");
                inv.PaidVatAmount   = DaHelper.GetDecimal(dr, "PaidVatAmount");
                inv.PaidGAmount     = DaHelper.GetDecimal(dr, "PaidGAmount");

                inv.ToPayQty        = inv.ToBePaidQty;
                inv.ToPayGAmount    = inv.ToBePaidGAmount;
                inv.ToPayVatAmount  = inv.ToBePaidVatAmount;

                inv.OriginalInvoiceNo       = DaHelper.GetString(dr, "OriginalInvoiceNo");
                inv.InstallmentPeriod       = DaHelper.GetInt(dr, "InstallmentPeriod");
                inv.InstallmentTotalPeriod  = DaHelper.GetInt(dr, "InstallmentTotalPeriod");

                inv.CaDoc           = DaHelper.GetString(dr, "CaDoc");
                inv.ControllerId    = DaHelper.GetString(dr, "ControllerId");
                inv.ControllerName  = DaHelper.GetString(dr, "ControllerName");
                inv.MruId           = DaHelper.GetString(dr, "MruId");
                if (inv.InvoiceNo == null || inv.InvoiceNo == "NULL")
                {
                    invoices = null;
                    break;
                }
                else
                {
                    inv.Bills = SearchBillByInvoiceNo(inv.InvoiceNo, chkIsPaid);
                }
                inv.CaTaxId         = DaHelper.GetString(dr, "CaTaxId");
                inv.CaTaxBranch     = DaHelper.GetString(dr, "CaTaxBranch");
                inv.EnergyAmount    = DaHelper.GetDecimal(dr, "EnergyAmount").Value;

                invoices.Add(inv);
            }
            return invoices;
        }

        private static List<Bill> ConvertToBillList(DataTable dt)
        {
            List<Bill> bills = new List<Bill>();
            foreach (DataRow dr in dt.Rows)
            {
                Bill b              = new Bill();
                b.ItemId            = DaHelper.GetString(dr, "ItemId");
                b.InvoiceNo         = DaHelper.GetString(dr, "InvoiceNo");
                b.CustomerId        = DaHelper.GetString(dr, "CaId");
                b.BranchId          = DaHelper.GetString(dr, "BranchId");
                b.TechBranchName    = DaHelper.GetString(dr, "TechBranchName");
                b.CommBranchId      = DaHelper.GetString(dr, "CommBranchId");
                b.CommBranchName    = DaHelper.GetString(dr, "CommBranchName");
                b.Name              = DaHelper.GetString(dr, "CaName");
                b.Address           = DaHelper.GetString(dr, "CaAddress");
                b.ContractTypeId    = DaHelper.GetString(dr, "CtId");
                b.DebtId            = DaHelper.GetString(dr, "DtId");
                b.DebtType          = DaHelper.GetString(dr, "DtName");
                b.Description       = DaHelper.GetString(dr, "Description");
                b.BillBookId        = DaHelper.GetString(dr, "BillBookId");
                b.GroupInvoiceId    = DaHelper.GetString(dr, "GroupInvoiceNo");
                b.Period            = DaHelper.GetString(dr, "Period");
                b.PaymentMethodId   = DaHelper.GetString(dr, "PmId");
                b.PaymentOrderFlag  = DaHelper.GetString(dr, "PaymentOrderFlag");
                b.PaymentOrderDt    = DaHelper.GetDate(dr, "PaymentOrderDt");
                b.SecurityDeposit   = DaHelper.GetDecimal(dr, "SecurityDeposit");
                b.DisConnectDate    = DaHelper.GetDate(dr, "DisconnectDt");
                b.DisconnectDocNo   = DaHelper.GetString(dr, "DisconnectDoc");
                b.CancelFlag        = DaHelper.GetString(dr, "CancelFlag");
                b.ModifiedFlag      = DaHelper.GetString(dr, "ModifiedFlag");

                //Tax 13 
                b.CaTaxId           = DaHelper.GetString(dr, "CaTaxId");
                b.CaTaxBranch       = DaHelper.GetString(dr, "CaTaxBranch");

                // for electric item
                b.MeterId           = DaHelper.GetString(dr, "MeterId");
                b.RateTypeId        = DaHelper.GetString(dr, "RateTypeId");
                b.MeterReadDate     = DaHelper.GetDate(dr, "MeterReadDt");
                b.PreviousUnit      = DaHelper.GetDecimal(dr, "ReadUnit");
                b.LastUnit          = DaHelper.GetDecimal(dr, "LastUnit");
                b.FullBaseAmount    = DaHelper.GetDecimal(dr, "FullBaseAmount");
                b.FullFtAmount      = DaHelper.GetDecimal(dr, "FullFtAmount");
                b.FullQty           = DaHelper.GetDecimal(dr, "FullQty");
                b.FullAmount        = DaHelper.GetDecimal(dr, "FullAmount");
                b.FullVatAmount     = DaHelper.GetDecimal(dr, "FullVatAmount");
                b.FullGAmount       = DaHelper.GetDecimal(dr, "FullGAmount");

                b.BaseAmount        = DaHelper.GetDecimal(dr, "BaseAmount");
                b.FtUnitPrice       = DaHelper.GetDecimal(dr, "FtUnitPrice");
                b.FtAmount          = DaHelper.GetDecimal(dr, "FtAmount");
                b.AmountExVat       = DaHelper.GetDecimal(dr, "Amount");

                b.UnitPrice         = DaHelper.GetDecimal(dr, "UnitPrice");
                b.Qty               = DaHelper.GetDecimal(dr, "Qty");
                b.ToPayQty          = DaHelper.GetDecimal(dr, "Qty");
                b.UnitTypeId        = DaHelper.GetString(dr, "UnitTypeId");
                b.UnitTypeName      = DaHelper.GetString(dr, "UnitTypeName");
                b.GAmount           = DaHelper.GetDecimal(dr, "GAmount");
                b.ToPayGAmount      = DaHelper.GetDecimal(dr, "GAmount") - DaHelper.GetDecimal(dr, "PaidGAmount");
                b.LeftInstallmentAmount = DaHelper.GetDecimal(dr, "LeftInstallmentAmount");

                b.BookCreateDt      = DaHelper.GetDate(dr, "BookCreateDt");
                b.BookTotalVatAmount = DaHelper.GetDecimal(dr, "BookTotalVatAmount");
                b.BookTotalGAmount  = DaHelper.GetDecimal(dr, "BookTotalAmount");
                b.BookAdvanceAmount = DaHelper.GetDecimal(dr, "BookAdvanceAmount");
                b.BookPaidGAmount   = DaHelper.GetDecimal(dr, "BookPaidAmount");
                b.BookTotalBillCollected = DaHelper.GetInt(dr, "BookTotalBillCollected");
                b.BookTotalBill     = DaHelper.GetInt(dr, "BookTotalBill");

                b.TaxCode           = DaHelper.GetString(dr, "TaxCode");
                b.TaxRate           = DaHelper.GetDecimal(dr, "TaxRate");
                b.VatAmount         = DaHelper.GetDecimal(dr, "VatAmount");
                b.ToPayVatAmount    = DaHelper.GetDecimal(dr, "VatAmount");
                b.ControllerId      = DaHelper.GetString(dr, "ControllerId");
                b.AccountClass      = DaHelper.GetString(dr, "AccountClassId");
                b.DueDate           = DaHelper.GetDate(dr, "DueDt");
                b.DeferralDate      = DaHelper.GetDate(dr, "DeferralDt");
                b.OriginalDueDate   = DaHelper.GetDate(dr, "OriginalDueDt");

                b.InterestLockFlag  = DaHelper.GetString(dr, "InterestLockFlag");
                b.InterestKey       = DaHelper.GetString(dr, "InterestKey");
                b.InterestRate      = DaHelper.GetDecimal(dr, "InterestRate");
                //TODO: INSTALLMENT CASE
                //b.OriginalDtId = DaHelper.GetString(dr, "OriginalDtId");
                //b.MainCaDoc = DaHelper.GetString(dr, "MainCaDoc");
                //b.OriginalCaDoc = DaHelper.GetString(dr, "OriginalCaDoc");

                b.SubGroupInvoiceNo = DaHelper.GetString(dr, "SubGroupInvoiceNo");
                //b.EnergyAmount = DaHelper.GetDecimal(dr, "EnergyAmount").Value;

                b.DiscStatusId              = DaHelper.GetString(dr, "DiscStatusId");
                b.IsServiceEndOfTheYear     = DaHelper.GetString(dr, "IsServiceEndOfTheYear");
                b.IsExpenseDuringTheYear    = DaHelper.GetString(dr, "IsExpenseDuringTheYear");
                b.BaseGroupTotalAmount      = DaHelper.GetDecimal(dr, "BaseGroupTotalAmount");


                bills.Add(b);
            }
            return bills;
        }

        #endregion

        #region ..GET AR INFORMATION

        public int InsertInterestPending(InterestPendingModel model)
        {
            var Result = 0;
            
            try
            {
                if(model.InvoiceNo == null)
                {
                   model.InvoiceNo = "";
                }
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("SmartPlus_Ins_PendingInterest");
                db.AddInParameter(cmd, "pCaID", DbType.String, model.CaId.Trim());
                db.AddInParameter(cmd, "pDescription", DbType.String, model.Description.Trim());
                db.AddInParameter(cmd, "pQty", DbType.Decimal, model.Qty);
                db.AddInParameter(cmd, "pUnitTypeId", DbType.String, model.UnitTypeId.Trim());
                db.AddInParameter(cmd, "pAmount", DbType.Double, model.Amount);
                db.AddInParameter(cmd, "pTaxCode", DbType.String, model.TaxCode.Trim());
                db.AddInParameter(cmd, "pVatAmount", DbType.Double, model.VatAmount);
                db.AddInParameter(cmd, "pGAmount", DbType.Double, model.GAmount);
                db.AddInParameter(cmd, "pPOSDebtFlag", DbType.String, model.POSDebtFlag.Trim());
                db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, model.PostBranchServerId.Trim());
                db.AddInParameter(cmd, "pModifiedBy", DbType.String, model.ModifiedBy.Trim());
                db.AddInParameter(cmd, "pInvoiceNo", DbType.String, model.InvoiceNo);
                db.AddInParameter(cmd, "pOriginalInvoiceNo", DbType.String, model.OriginalInvoiceNo.Trim());
                db.AddInParameter(cmd, "pRefInvoiceNo", DbType.String, model.RefInvoiceNo.Trim());
                db.AddInParameter(cmd, "CallTransactionId ", DbType.String, model.WebServiceCallGroupId.Trim());
                db.AddInParameter(cmd, "Period",    DbType.String,model.Period.Trim());
                db.AddInParameter(cmd, "FTAmount",  DbType.Decimal,model.FTAmount);
                db.AddInParameter(cmd, "InstallmentPeriodText", DbType.String, model.InstallmentPeriodText);
                cmd.CommandTimeout  = CMD_TIMEOUT;
                Result = db.ExecuteNonQuery(cmd);                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public string GetInterestPendingInvoiceNo(string CaId, string OriginalInvoiceNo, string RefInvoiceNo, string TransactionId)
        {
            string resInvoiceNo = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("SmartPlus_Get_PendingInterest");
                db.AddInParameter(cmd, "CaId", DbType.String, CaId.Trim());  
                db.AddInParameter(cmd, "OriginalInvoiceNo", DbType.String, OriginalInvoiceNo.Trim());
                db.AddInParameter(cmd, "RefInvoiceNo", DbType.String,RefInvoiceNo.Trim());
                db.AddInParameter(cmd, "CallTransactionId ", DbType.String, TransactionId.Trim());
                cmd.CommandTimeout  = CMD_TIMEOUT;
                DataTable dt        = db.ExecuteDataSet(cmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        resInvoiceNo = r["InvoiceNo"].ToString();
                        resInvoiceNo = DaHelper.GetString(r, "InvoiceNo");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resInvoiceNo;
        }

        public void InsertSmartPlusTransactionLog(string serviceName, string userId, string token, string caId, string invoiceNo, string agencyId, string serviceResultText)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("SmartPlus_INS_InsNonBankTransactionLog");
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
                throw ex;
            }

        }

        public List<ARInformationInfo> SearchSmartPlusBillInformation(string caId, string billFlag,string wsTransactionId)
        {
            List<ARInformationInfo> conInfoList = new List<ARInformationInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("SmartPlus_Get_BillInformation");
            cmd.CommandTimeout = CMD_TIMEOUT;

            db.AddInParameter(cmd, "CaId", DbType.String, caId);
            db.AddInParameter(cmd, "BillFlag", DbType.String, billFlag);           
            db.AddInParameter(cmd, "WSTransactionId", DbType.String, wsTransactionId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    ARInformationInfo conInfo   = new ARInformationInfo();
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
                    conInfo.FullGAmount         = DaHelper.GetDecimal(r, "FullGAmount")         == null ? 0 : DaHelper.GetDecimal(r, "FullGAmount").Value;
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
                    conInfo.TaxBranch           = DaHelper.GetString(r, "TaxBranch")            == null ? "" : DaHelper.GetString(r, "TaxBranch");
                    conInfo.Remark              = DaHelper.GetString(r, "Remark")               == null ? "" : DaHelper.GetString(r, "Remark");
                    conInfo.RefInvoiceNo        = DaHelper.GetString(r, "RefInvoiceNo")         == null ? "" : DaHelper.GetString(r, "RefInvoiceNo");
                    conInfo.RefWSKey            = DaHelper.GetString(r, "RefWSKey")             == null ? "" : DaHelper.GetString(r, "RefWSKey");
                    conInfo.InstallmentPeriodText = DaHelper.GetString(r, "InstallmentPeriodText") == null ? "" : DaHelper.GetString(r, "InstallmentPeriodText");
                    conInfoList.Add(conInfo);
                }
            }
            else
            {
                ARInformationInfo conInfo = new ARInformationInfo();
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
                conInfo.FullGAmount         = Convert.ToDecimal(0.0);
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
                conInfo.RefInvoiceNo        = "";
                conInfo.RefWSKey            = "";
                conInfoList.Add(conInfo);
            }            

            return conInfoList;
        }

        public List<ARInformationInfo> SearchSmartPlusCorpBillInformation(string caId, string billFlag, string wsTransactionId)
        {
            List<ARInformationInfo> conInfoList = new List<ARInformationInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("SmartPlusCorp_Get_BillInformation");
            cmd.CommandTimeout = CMD_TIMEOUT;

            db.AddInParameter(cmd, "CaId", DbType.String, caId);
            db.AddInParameter(cmd, "BillFlag", DbType.String, billFlag);
            db.AddInParameter(cmd, "WSTransactionId", DbType.String, wsTransactionId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    ARInformationInfo conInfo = new ARInformationInfo();
                    conInfo.BranchId = DaHelper.GetString(r, "BranchId") == null ? "" : DaHelper.GetString(r, "BranchId");
                    conInfo.BranchName = DaHelper.GetString(r, "BranchName") == null ? "" : DaHelper.GetString(r, "BranchName");
                    conInfo.CaId = DaHelper.GetString(r, "CaId") == null ? "" : DaHelper.GetString(r, "CaId");
                    conInfo.CAPmId = DaHelper.GetString(r, "CAPmId") == null ? "" : DaHelper.GetString(r, "CAPmId");
                    conInfo.AccountClassId = DaHelper.GetString(r, "AccountClassId") == null ? "" : DaHelper.GetString(r, "AccountClassId");
                    conInfo.AccountClassName = DaHelper.GetString(r, "AccountClassName") == null ? "" : DaHelper.GetString(r, "AccountClassName");
                    conInfo.MainSub = DaHelper.GetString(r, "MainSub") == null ? "" : DaHelper.GetString(r, "MainSub");
                    conInfo.DtName = DaHelper.GetString(r, "DtName") == null ? "" : DaHelper.GetString(r, "DtName");
                    conInfo.InvoiceNo = DaHelper.GetString(r, "InvoiceNo") == null ? "" : DaHelper.GetString(r, "InvoiceNo");
                    conInfo.CaDoc = DaHelper.GetString(r, "CaDoc") == null ? "" : DaHelper.GetString(r, "CaDoc");
                    conInfo.MeterReadDt = DateFormatter.ToDateThString(DaHelper.GetDateTime(r, "MeterReadDt")) == null ? "" : DateFormatter.ToDateThString(DaHelper.GetDateTime(r, "MeterReadDt"));
                    conInfo.BillingDt = DateFormatter.ToDateThString(DaHelper.GetDateTime(r, "BillingDt")) == null ? "" : DateFormatter.ToDateThString(DaHelper.GetDateTime(r, "BillingDt"));
                    conInfo.DueDt = DateFormatter.ToDateThString(DaHelper.GetDateTime(r, "DueDt")) == null ? "" : DateFormatter.ToDateThString(DaHelper.GetDateTime(r, "DueDt"));
                    conInfo.DueDt2 = DateFormatter.ToDateThString(DaHelper.GetDateTime(r, "DueDt2")) == null ? "" : DateFormatter.ToDateThString(DaHelper.GetDateTime(r, "DueDt2"));
                    conInfo.DunningDt = DateFormatter.ToDateThString(DaHelper.GetDateTime(r, "DunningDt")) == null ? "" : DateFormatter.ToDateThString(DaHelper.GetDateTime(r, "DunningDt"));
                    conInfo.Period = DaHelper.GetString(r, "Period") == null ? "" : DaHelper.GetString(r, "Period");

                    conInfo.Qty = DaHelper.GetDecimal(r, "Qty") == null ? 0 : DaHelper.GetDecimal(r, "Qty").Value;
                    conInfo.FTAmount = DaHelper.GetDecimal(r, "FTAmount") == null ? 0 : DaHelper.GetDecimal(r, "FTAmount").Value;
                    conInfo.VatRate = DaHelper.GetDecimal(r, "VatRate") == null ? 0 : DaHelper.GetDecimal(r, "VatRate").Value;
                    conInfo.VatAmount = DaHelper.GetDecimal(r, "VatAmount") == null ? 0 : DaHelper.GetDecimal(r, "VatAmount").Value;
                    conInfo.GAmount = DaHelper.GetDecimal(r, "GAmount") == null ? 0 : DaHelper.GetDecimal(r, "GAmount").Value;
                    conInfo.FullGAmount = DaHelper.GetDecimal(r, "FullGAmount") == null ? 0 : DaHelper.GetDecimal(r, "FullGAmount").Value;
                    conInfo.InvestigateFlag = DaHelper.GetString(r, "InvestigateFlag") == null ? "" : DaHelper.GetString(r, "InvestigateFlag");
                    conInfo.DisconnectionFlag = DaHelper.GetString(r, "DisconnectionFlag") == null ? "" : DaHelper.GetString(r, "DisconnectionFlag");
                    conInfo.GroupInvoiceFlag = DaHelper.GetString(r, "GroupInvoiceFlag") == null ? "" : DaHelper.GetString(r, "GroupInvoiceFlag");
                    conInfo.InstallmentFlag = DaHelper.GetString(r, "InstallmentFlag") == null ? "" : DaHelper.GetString(r, "InstallmentFlag");
                    conInfo.PaymentOrderFlag = DaHelper.GetString(r, "PaymentOrderFlag") == null ? "" : DaHelper.GetString(r, "PaymentOrderFlag");
                    conInfo.ReceiveStatus = DaHelper.GetString(r, "ReceiveStatus") == null ? "" : DaHelper.GetString(r, "ReceiveStatus");
                    conInfo.ReceiptId = DaHelper.GetString(r, "ReceiptId") == null ? "" : DaHelper.GetString(r, "ReceiptId");
                    conInfo.ReceiptDt = DateFormatter.ToDateThString(DaHelper.GetDateTime(r, "ReceiptDt")) == null ? "" : DateFormatter.ToDateThString(DaHelper.GetDateTime(r, "ReceiptDt"));
                    conInfo.ReceiptCh = DaHelper.GetString(r, "ReceiptCh") == null ? "" : DaHelper.GetString(r, "ReceiptCh");
                    conInfo.ReceiptBranchId = DaHelper.GetString(r, "ReceiptBranchId") == null ? "" : DaHelper.GetString(r, "ReceiptBranchId");
                    conInfo.ReceiptBranchName = DaHelper.GetString(r, "ReceiptBranchName") == null ? "" : DaHelper.GetString(r, "ReceiptBranchName");
                    conInfo.TaxId = DaHelper.GetString(r, "TaxId") == null ? "" : DaHelper.GetString(r, "TaxId");
                    conInfo.TaxBranch = DaHelper.GetString(r, "TaxBranch") == null ? "" : DaHelper.GetString(r, "TaxBranch");
                    conInfo.Remark = DaHelper.GetString(r, "Remark") == null ? "" : DaHelper.GetString(r, "Remark");
                    conInfo.RefInvoiceNo = DaHelper.GetString(r, "RefInvoiceNo") == null ? "" : DaHelper.GetString(r, "RefInvoiceNo");
                    conInfo.RefWSKey = DaHelper.GetString(r, "RefWSKey") == null ? "" : DaHelper.GetString(r, "RefWSKey");
                    conInfo.InstallmentPeriodText = DaHelper.GetString(r, "InstallmentPeriodText") == null ? "" : DaHelper.GetString(r, "InstallmentPeriodText");
                    conInfoList.Add(conInfo);
                }
            }
            else
            {
                ARInformationInfo conInfo = new ARInformationInfo();
                conInfo.BranchId = "";
                conInfo.BranchName = "";
                conInfo.CaId = "";
                conInfo.CAPmId = "";
                conInfo.AccountClassId = "";
                conInfo.AccountClassName = "";
                conInfo.MainSub = "";
                conInfo.DtName = "";
                conInfo.InvoiceNo = "";
                conInfo.CaDoc = "";
                conInfo.MeterReadDt = "";
                conInfo.BillingDt = "";
                conInfo.DueDt = "";
                conInfo.DueDt2 = "";
                conInfo.DunningDt = "";
                conInfo.Period = "";
                conInfo.Qty = Convert.ToDecimal(0.0);
                conInfo.FTAmount = Convert.ToDecimal(0.0);
                conInfo.VatRate = Convert.ToDecimal(0.0);
                conInfo.VatAmount = Convert.ToDecimal(0.0);
                conInfo.GAmount = Convert.ToDecimal(0.0);
                conInfo.FullGAmount = Convert.ToDecimal(0.0);
                conInfo.InvestigateFlag = "";                               //Uthen 14-07-2017
                conInfo.DisconnectionFlag = "";
                conInfo.GroupInvoiceFlag = "";
                conInfo.InstallmentFlag = "";
                conInfo.PaymentOrderFlag = "";
                conInfo.ReceiveStatus = "";
                conInfo.ReceiptId = "";
                conInfo.ReceiptDt = "";
                conInfo.ReceiptCh = "";
                conInfo.ReceiptBranchId = "";
                conInfo.ReceiptBranchName = "";
                conInfo.TaxId = "";
                conInfo.TaxBranch = "";
                conInfo.Remark = "";
                conInfo.RefInvoiceNo = "";
                conInfo.RefWSKey = "";
                conInfoList.Add(conInfo);
            }

            return conInfoList;
        }
        
        public DataTable SearchInvoiceByCustomerIdSameWithBPM(string caId, bool isSearchByBP)
        {
            Database db     = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd   = db.GetStoredProcCommand("pc_sel_ARInvoiceByCaId");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "CaId", DbType.String, caId);
            db.AddInParameter(cmd, "IsSearchByBP", DbType.Boolean, isSearchByBP);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            return dt;
        }

        

        
        public SmartplusSettingsModel GetSmartPlusSetting()
        {
            var settings = new SmartplusSettingsModel();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("SmartPlus_Get_Settings");
            cmd.CommandTimeout = CMD_TIMEOUT;
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            var conAcountInfo = new ContractorAccountDetailModel();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    string UserId   = DaHelper.GetString(r, "UserId");
                    string Token    = DaHelper.GetString(r, "Token");
                    string CurIP    = DaHelper.GetString(r, "CurIP");
                    string EndPoint = DaHelper.GetString(r, "ServiceEndPoint");
                    string HashPwd  = DaHelper.GetString(r, "HashPassword");

                    settings.UserId             = UserId;
                    settings.Token              = Token;
                    settings.CurIP              = CurIP;
                    settings.ServiceEndPoint    = EndPoint;
                    settings.HashPassword       = HashPwd;
                }
            }

            return settings;
        }           

        #endregion

        #region ..GET CA DETAIL

        public ContractorAccountDetailModel SearchContractorInformation(string caId)
        {            
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("SmartPlus_Get_ContractorInformation");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "CaId", DbType.String, caId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            var conAcountInfo = new ContractorAccountDetailModel();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    conAcountInfo.BranchId = DaHelper.GetString(r, "BranchId") == null ? "" : DaHelper.GetString(r, "BranchId");
                    conAcountInfo.BranchName = DaHelper.GetString(r, "BranchName") == null ? "" : DaHelper.GetString(r, "BranchName");
                    conAcountInfo.CaName = DaHelper.GetString(r, "CaName") == null ? "" : DaHelper.GetString(r, "CaName");
                    conAcountInfo.CaAddress = DaHelper.GetString(r, "CaAddress") == null ? "" : DaHelper.GetString(r, "CaAddress");
                    conAcountInfo.AccountClassId = DaHelper.GetString(r, "AccountClassId") == null ? "" : DaHelper.GetString(r, "AccountClassId");
                    conAcountInfo.AccountClassName = DaHelper.GetString(r, "AccountClassName") == null ? "" : DaHelper.GetString(r, "AccountClassName");
                    conAcountInfo.MeterSizeName = DaHelper.GetString(r, "MeterSizeName") == null ? "" : DaHelper.GetString(r, "MeterSizeName");
                    conAcountInfo.MeterInstallDt = DateFormatter.ToDateThString(DaHelper.GetDateTime(r, "MeterInstallDt")) == null ? "" : DateFormatter.ToDateThString(DaHelper.GetDateTime(r, "MeterInstallDt"));
                    conAcountInfo.Remark = "";
                }
            }
            else
            {                
                conAcountInfo.BranchId                  = "";
                conAcountInfo.BranchName                = "";
                conAcountInfo.CaName                    = "";
                conAcountInfo.CaAddress                 = "";
                conAcountInfo.AccountClassId            = "";
                conAcountInfo.AccountClassName          = "";
                conAcountInfo.MeterSizeName             = "";
                conAcountInfo.MeterInstallDt            = "";
                conAcountInfo.Remark                    = "";                
            }
            return conAcountInfo;
        }
        #endregion

        #region ..UPDATE MARK FLAG

        public string UpdateBillMarkFlagData(string caId, string invoiceNo,string wsKey)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("SmartPlus_Upd_ARPayment");
            cmd.CommandTimeout = CMD_TIMEOUT;

            db.AddOutParameter(cmd, "oResult", DbType.String, 20);
            db.AddInParameter(cmd, "pCaId ", DbType.String, caId);
            db.AddInParameter(cmd, "pInvoiceNo", DbType.String, invoiceNo);
            db.AddInParameter(cmd, "pWSKey", DbType.String, wsKey);            
            db.ExecuteNonQuery(cmd).ToString();

            string result = (string)db.GetParameterValue(cmd, "oResult");
            return result;
        }

        //public string UpdateBillMarkFlagData(string caId, string invoiceNo, string wsKey)
        //{
        //    Database db         = DatabaseFactory.CreateDatabase("POSDatabase");
        //    DbCommand cmd       = db.GetStoredProcCommand("SmartPlus_Upd_ARPayment");
        //    cmd.CommandTimeout  = CMD_TIMEOUT;

        //    db.AddOutParameter(cmd, "oResult",      DbType.String, 20);
        //    db.AddInParameter(cmd, "pCaId ",        DbType.String, caId);
        //    db.AddInParameter(cmd, "pInvoiceNo",    DbType.String, invoiceNo);
        //    db.AddInParameter(cmd, "pWSKey", DbType.String, wsKey);
        //    db.ExecuteNonQuery(cmd).ToString();

        //    string result = (string)db.GetParameterValue(cmd, "oResult");
        //    return result;
        //}

        #endregion

        #region ..CANCEL MARK FLAG

        public string CancelPayment(string caId, string invoiceNo, string wsKey)
        {
            Database db     = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd   = db.GetStoredProcCommand("SmartPlus_Upd_CancelPayment");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "CaId", DbType.String, caId);
            db.AddInParameter(cmd, "InvoiceNo", DbType.String, invoiceNo);
            db.AddInParameter(cmd, "RefKey", DbType.String, wsKey);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            string CancelStatus = null;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    CancelStatus = DaHelper.GetString(r, "CancelingResult");
                }
            }

            return CancelStatus;
        }  

        //public string CancelPayment(string caId,string invoiceNo)
        //{ 
        //    Database    db      = DatabaseFactory.CreateDatabase("POSDatabase");
        //    DbCommand   cmd     = db.GetStoredProcCommand("SmartPlus_Upd_CancelPayment");
        //    cmd.CommandTimeout  = CMD_TIMEOUT;
        //    db.AddInParameter(cmd, "CaId",      DbType.String, caId);
        //    db.AddInParameter(cmd, "InvoiceNo", DbType.String, invoiceNo);            
        //    DataTable   dt      = db.ExecuteDataSet(cmd).Tables[0];
        //    string CancelStatus = null;            
        //    if (dt.Rows.Count > 0)
        //    {
        //        foreach (DataRow r in dt.Rows)
        //        {
        //            CancelStatus = DaHelper.GetString(r, "CancelingResult");                                       
        //        }
        //    }

        //    return CancelStatus;
        //}           

        #endregion
    }
}
