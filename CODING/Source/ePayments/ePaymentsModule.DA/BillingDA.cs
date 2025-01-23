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
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities.ReceiptPrinting;
using System;

namespace PEA.BPM.ePaymentsModule.DA
{
    public class BillingDA
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

        public string InsertIntoARPaymentType(DbTransaction trans, string paymentId, decimal paidAmount, string paymentMethod, string postedServerId, string modifiedBy)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ePay_ins_ARPaymentType");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "PaymentId", DbType.String, paymentId);
            db.AddInParameter(cmd, "Amount", DbType.Decimal, paidAmount);
            db.AddInParameter(cmd, "ChangeAmount", DbType.Decimal, 0);
            db.AddInParameter(cmd, "PtId", DbType.String, paymentMethod);
            db.AddInParameter(cmd, "BankKey", DbType.String, null);
            db.AddInParameter(cmd, "ChqNo", DbType.String, null);
            db.AddInParameter(cmd, "ChqAccNo", DbType.String, null);
            db.AddInParameter(cmd, "ChqDt", DbType.DateTime, null);
            db.AddInParameter(cmd, "DraftFlag", DbType.String, null);
            db.AddInParameter(cmd, "CashierChequeFlag", DbType.String, null);
            db.AddInParameter(cmd, "TranfAccNo", DbType.String, null);
            db.AddInParameter(cmd, "TranfDt", DbType.DateTime, null);
            db.AddInParameter(cmd, "CancelARPtId", DbType.String, null);
            db.AddInParameter(cmd, "PostedServerId", DbType.String, postedServerId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, modifiedBy);

            return db.ExecuteScalar(cmd, trans).ToString();
        }

        public int InsertIntoRTARPaymentTypeARPayment(DbTransaction trans, string ARPtId, string invoiceNo, decimal? Amount, string postedServerId, string modifiedBy)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ePay_ins_RTARPaymentTypeARPayment");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "ARPtId", DbType.String, ARPtId);
            db.AddInParameter(cmd, "InvoiceNo", DbType.String, invoiceNo);
            db.AddInParameter(cmd, "Amount", DbType.Decimal, Amount);
            db.AddInParameter(cmd, "PostedServerId", DbType.String, postedServerId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, modifiedBy);
            return (int)db.ExecuteScalar(cmd, trans);
        }

        public int UpdateARPaidAmount(DbTransaction trans, string invoiceNo, string modifiedBy, string postServerId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ePay_upd_ARPaidAmount");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "InvoiceNo", DbType.String, invoiceNo);
            db.AddInParameter(cmd, "PostServerId", DbType.String, postServerId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, modifiedBy);
            return (int)db.ExecuteScalar(cmd, trans);
        }

        public int UpdateARPayment(DbTransaction trans, string caId, string invoiceNo, string modifiedBy, string postedServerId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ePay_upd_UpdateARPayment");
            db.AddOutParameter(cmd, "pResult", DbType.String, 1);
            db.AddInParameter(cmd, "pCaId", DbType.String, caId);
            db.AddInParameter(cmd, "pInvoiceNo", DbType.String, invoiceNo);
            db.AddInParameter(cmd, "pPostedServerId", DbType.String, postedServerId);
            db.AddInParameter(cmd, "pModifiedBy", DbType.String, modifiedBy);
            cmd.CommandTimeout = CMD_TIMEOUT;
            return (int)db.ExecuteScalar(cmd, trans);
        }

        public void SaveReceipt(DbTransaction trans, string receiptId, string paymentId,
                                  PaymentInfo payment)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ePay_ins_Receipt");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "ReceiptId", DbType.String, receiptId);
            db.AddInParameter(cmd, "PaymentId", DbType.String, paymentId);
            db.AddInParameter(cmd, "PrintingSequence", DbType.Int16, 1);
            db.AddInParameter(cmd, "TotalReceipt", DbType.Int16, 1);
            db.AddInParameter(cmd, "CaId", DbType.String, payment.CaId);
            db.AddInParameter(cmd, "NoOfPrinting", DbType.Int32, 1);
            db.AddInParameter(cmd, "ReceiptType", DbType.String, "5");
            db.AddInParameter(cmd, "ExtReceiptId", DbType.String, payment.ReceiptNo);
            db.AddInParameter(cmd, "ExtReceiptDt", DbType.DateTime, payment.PaymentDt);
            db.AddInParameter(cmd, "PostedServerId", DbType.String, payment.PostServerId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, payment.ModifiedBy);
            db.ExecuteNonQuery(cmd, trans);

            //recept item
            cmd = db.GetStoredProcCommand("pc_ins_ReceiptItem");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "InvoiceNo", DbType.String, payment.InvoiceNo);
            db.AddInParameter(cmd, "ReceiptId", DbType.String, receiptId);
            db.AddInParameter(cmd, "PostedServerId", DbType.String, payment.PostServerId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, payment.ModifiedBy);
            db.ExecuteNonQuery(cmd, trans);
        }

        public void DelBillMarkFlagService()
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ePay_del_BillMarkFlag");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.ExecuteNonQuery(cmd);
        }

        //public int InsertARPayment(DbTransaction trans, string invoiceNo, string period, string modifierBy,
        //              string branchId, string postServerId, decimal gAmount)
        //{
        //    Database db = DatabaseFactory.CreateDatabase("POSDatabase");
        //    DbCommand cmd = db.GetStoredProcCommand("ePay_ins_ARPayment");
        //    // Add data ot ARPyment & RTAPaymentTypeARPayment            
        //    db.AddInParameter(cmd, "pInvoiceNo", DbType.String, invoiceNo);
        //    db.AddInParameter(cmd, "pPeriod", DbType.String, period);
        //    db.AddInParameter(cmd, "pGAmount", DbType.Decimal, gAmount);
        //    db.AddInParameter(cmd, "pPostedServerId", DbType.String, postServerId);
        //    db.AddInParameter(cmd, "pModifiedBy", DbType.String, modifierBy);
        //    db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
        //    return db.ExecuteNonQuery(cmd, trans);
        //}

        public List<EpayUploadItem> GetDebtComparableData(string caInvoceNo)
        {
            List<EpayUploadItem> conInfoList = new List<EpayUploadItem>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ePay_sel_DebtInfo");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "CaInvoice", DbType.String, caInvoceNo);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            EpayUploadItem tmpInfo = new EpayUploadItem();

            foreach (DataRow r in dt.Rows)
            {
                EpayUploadItem conInfo = new EpayUploadItem();
                conInfo.SysCaId = DaHelper.GetString(r, "CaId").Trim();
                conInfo.CaName = DaHelper.GetString(r, "CaName").Trim();
                conInfo.CaAddress = DaHelper.GetString(r, "CaAddress").Trim();
                conInfo.SysBranchId = DaHelper.GetString(r, "BranchId").Trim();
                conInfo.SysDueDate = DaHelper.GetDate(r, "DueDt");
                conInfo.SysDueDate = conInfo.SysDueDate != null ? conInfo.SysDueDate.Value.Date : conInfo.SysDueDate;
                conInfo.SysGAmount = DaHelper.GetDecimal(r, "GAmount");
                conInfo.SysInvoiceNo = DaHelper.GetString(r, "InvoiceNo").Trim();
                conInfo.SysPeriod = DaHelper.GetString(r, "Period").Trim();
                conInfo.SysVatAmount = DaHelper.GetDecimal(r, "VatAmount");
                conInfo.SysPending = DaHelper.GetString(r, "Pending").Trim();
                conInfo.SysPmId = DaHelper.GetString(r, "PmId").Trim();

                if (conInfo.SysPmId == "C")
                {
                    if (conInfo.SysCaId == tmpInfo.SysCaId && conInfo.SysInvoiceNo == tmpInfo.SysInvoiceNo && conInfo.Period == tmpInfo.Period)
                    {
                        conInfoList.Remove(tmpInfo);
                    }
                }
                conInfoList.Add(conInfo);
                tmpInfo = conInfo;
            }

            return conInfoList;
        }

        public List<Company> SearchCompany(Company compParm)
        {
            List<Company> compList = new List<Company>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("[ePay_sel_Company]");
            cmd.CommandTimeout = CMD_TIMEOUT;

            db.AddInParameter(cmd, "companyId", DbType.String, compParm.CompanyId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow r in dt.Rows)
            {
                Company comp = new Company();
                comp.CompanyId = DaHelper.GetString(r, "CompanyId").Trim();
                comp.CompanyName = DaHelper.GetString(r, "CompanyName").Trim();

                compList.Add(comp);
            }

            return compList;
        }

        public string InsertEPayUploadData(DbTransaction trans, EPayUpload ePayUpload, string PosId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ePay_ins_EPayUpload");
                cmd.CommandTimeout = CMD_TIMEOUT;

                db.AddInParameter(cmd, "FileName", DbType.String, ePayUpload.FileName);
                db.AddInParameter(cmd, "UploadDt", DbType.DateTime, ePayUpload.UploadDt);
                db.AddInParameter(cmd, "CompanyId", DbType.String, ePayUpload.CompanyId);
                db.AddInParameter(cmd, "BillCount", DbType.Int32, ePayUpload.BillCount);
                db.AddInParameter(cmd, "BillAmount", DbType.Decimal, ePayUpload.BillAmount);
                db.AddInParameter(cmd, "TotalBillCount", DbType.Int32, ePayUpload.TotalBillCount);
                db.AddInParameter(cmd, "TotalBillAmount", DbType.Decimal, ePayUpload.TotalBillAmount);
                db.AddInParameter(cmd, "PosId", DbType.String, PosId);
                db.AddInParameter(cmd, "SyncFlag", DbType.String, ePayUpload.SyncFlag);
                db.AddInParameter(cmd, "PostDt", DbType.DateTime, ePayUpload.PostDt);
                db.AddInParameter(cmd, "PostBranchServerId", DbType.String, ePayUpload.PostBranchId);
                db.AddInParameter(cmd, "ModifiedDt", DbType.DateTime, ePayUpload.ModifiedDt);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, ePayUpload.ModifiedBy);
                db.AddInParameter(cmd, "Active", DbType.String, ePayUpload.Active);
                return db.ExecuteScalar(cmd, trans).ToString();
            }
            catch
            {
                throw;
            }
        }

        public string InsertEPayUploadItemData(DbTransaction trans, EpayUploadItem ePayUploadItem, string PosId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ePay_ins_EPayUploadItem");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "FileId", DbType.String, ePayUploadItem.EpayUploads.FileId);
                db.AddInParameter(cmd, "BranchId", DbType.String, ePayUploadItem.BranchId);
                db.AddInParameter(cmd, "CaId", DbType.String, ePayUploadItem.CaId);
                db.AddInParameter(cmd, "PayDt", DbType.DateTime, ePayUploadItem.PayDt);
                db.AddInParameter(cmd, "DueDt", DbType.DateTime, ePayUploadItem.DueDt);
                db.AddInParameter(cmd, "OutSourceAmount", DbType.Decimal, ePayUploadItem.OutSourceAmount);
                db.AddInParameter(cmd, "VatAmount", DbType.Decimal, ePayUploadItem.VatAmount);
                db.AddInParameter(cmd, "RecNo", DbType.String, ePayUploadItem.RecNo);
                db.AddInParameter(cmd, "Period", DbType.String, ePayUploadItem.Period);
                db.AddInParameter(cmd, "CompanyId", DbType.String, ePayUploadItem.CompanyId);
                db.AddInParameter(cmd, "ActCode", DbType.String, ePayUploadItem.ActCode);
                db.AddInParameter(cmd, "PosNo", DbType.String, ePayUploadItem.PosNo);
                db.AddInParameter(cmd, "InvoiceNo", DbType.String, ePayUploadItem.InvoiceNo);
                db.AddInParameter(cmd, "UploadStatus", DbType.String, ePayUploadItem.UploadStatus);
                db.AddInParameter(cmd, "PosId", DbType.String, PosId);
                db.AddInParameter(cmd, "SyncFlag", DbType.String, ePayUploadItem.SyncFlag);
                db.AddInParameter(cmd, "PostDt", DbType.DateTime, ePayUploadItem.PostDt);
                db.AddInParameter(cmd, "PostBranchServerId", DbType.String, ePayUploadItem.PostBranchId);
                db.AddInParameter(cmd, "ModifiedDt", DbType.DateTime, ePayUploadItem.ModifiedDt);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, ePayUploadItem.ModifiedBy);
                db.AddInParameter(cmd, "Active", DbType.String, ePayUploadItem.Active);
                return db.ExecuteScalar(cmd, trans).ToString();
            }
            catch
            {
                throw;
            }
        }

        public void InsertEPayClearifyData(DbTransaction trans, EPayClearify ePayClearify, string PosId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ePay_ins_EPayClearify");
                cmd.CommandTimeout = CMD_TIMEOUT;

                db.AddInParameter(cmd, "EPayItemId", DbType.String, ePayClearify.EpayUploadItems.EpayItemId);
                db.AddInParameter(cmd, "InvoiceNo", DbType.String, ePayClearify.EpayUploadItems.InvoiceNo);
                db.AddInParameter(cmd, "NewInvoiceNo", DbType.String, ePayClearify.NewInvoiceNo);
                db.AddInParameter(cmd, "FixedType", DbType.String, ePayClearify.FixedType);
                db.AddInParameter(cmd, "FixedDt", DbType.DateTime, ePayClearify.FixedDt);
                db.AddInParameter(cmd, "FixedBy", DbType.String, ePayClearify.FixedBy);
                db.AddInParameter(cmd, "RefDocNo", DbType.String, ePayClearify.RefDocNo);
                db.AddInParameter(cmd, "PosId", DbType.String, PosId);
                db.AddInParameter(cmd, "SyncFlag", DbType.String, ePayClearify.SyncFlag);
                db.AddInParameter(cmd, "PostDt", DbType.DateTime, ePayClearify.PostDt);
                db.AddInParameter(cmd, "PostBranchServerId", DbType.String, ePayClearify.PostBranchId);
                db.AddInParameter(cmd, "ModifiedDt", DbType.DateTime, ePayClearify.ModifiedDt);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, ePayClearify.ModifiedBy);
                db.AddInParameter(cmd, "Active", DbType.String, ePayClearify.Active);
                db.ExecuteNonQuery(cmd, trans);
            }
            catch
            {
                throw;
            }
        }

        public void InsertPaymentTransData(DbTransaction trans, PaymentTransParam payTrans)
        {
            try
            {
                List<Receipt> receiptList = new List<Receipt>();
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ePay_ins_Payment_Trans");
                cmd.CommandTimeout = CMD_TIMEOUT;

                db.AddInParameter(cmd, "FileId", DbType.String, payTrans.FileId.Trim());
                db.AddInParameter(cmd, "Prefix", DbType.String, payTrans.Prefix.Trim());
                db.AddInParameter(cmd, "PosId", DbType.String, payTrans.PosId.Trim());
                db.AddInParameter(cmd, "PtId", DbType.String, payTrans.PtId.Trim());
                db.AddInParameter(cmd, "ReceiptType", DbType.String, payTrans.ReceiptType.Trim());
                db.AddInParameter(cmd, "ModifiedDt", DbType.DateTime, payTrans.ModifiedDt);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, payTrans.ModifiedBy);

                db.ExecuteNonQuery(cmd, trans);

            }
            catch
            {
                throw;
            }
        }

        public List<AgentPayment> GetAgentPaymentData(AgentPayment agentPayment)
        {
            try
            {
                List<AgentPayment> agentPayList = new List<AgentPayment>();
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ePay_get_AgentPayment");
                cmd.CommandTimeout = CMD_TIMEOUT;

                db.AddInParameter(cmd, "CompId", DbType.String, agentPayment.AgentId);
                db.AddInParameter(cmd, "CompName", DbType.String, agentPayment.AgentName);
                db.AddInParameter(cmd, "UploadDt", DbType.DateTime, agentPayment.PostDt);

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

                foreach (DataRow r in dt.Rows)
                {
                    AgentPayment agentPay = new AgentPayment();
                    agentPay.PostDt  = DaHelper.GetDate(r, "PostDt");
                    agentPay.AgentId = DaHelper.GetString(r, "CaId").Trim();
                    agentPay.AgentName = DaHelper.GetString(r, "CaName");
                    agentPay.AgentAddr = DaHelper.GetString(r, "CaAddress");
                    agentPay.TotalBillAmount = DaHelper.GetDecimal(r, "TotalBillAmount");
                    agentPay.DepositDt = DaHelper.GetDate(r, "DepositDate");
                    agentPay.TranfDt = DaHelper.GetDate(r, "TranfDt");
                    agentPay.GAmount = DaHelper.GetDecimal(r, "Amount");
                    agentPay.TranfAccNo = DaHelper.GetString(r, "TranfAccNo");
                    agentPay.BankName = DaHelper.GetString(r, "BankName");
                    agentPay.IsSysData = true;

                    agentPayList.Add(agentPay);
                }

                return agentPayList;
            }
            catch
            {
                throw;
            }
        }

        public void InsertReceiptData(DbTransaction trans, Receipt receipt)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ePay_ins_Receipt");
                cmd.CommandTimeout = CMD_TIMEOUT;

                db.AddInParameter(cmd, "ReceiptId", DbType.String, receipt.ReceiptId);
                db.AddInParameter(cmd, "PaymentId", DbType.String, receipt.PaymentId);
                db.AddInParameter(cmd, "ARPmId", DbType.String, receipt.ARPmId);
                db.AddInParameter(cmd, "PrintingSequence", DbType.Int32, receipt.PrintingSequence);
                db.AddInParameter(cmd, "TotalReceipt", DbType.Int32, receipt.TotalReceipt);
                db.AddInParameter(cmd, "CaId", DbType.String, receipt.CaId);
                db.AddInParameter(cmd, "Name", DbType.String, receipt.Name);
                db.AddInParameter(cmd, "Address", DbType.String, receipt.Address);
                db.AddInParameter(cmd, "IsNameAddrModified", DbType.String, receipt.IsNameAddrModified);
                db.AddInParameter(cmd, "NoOfPrinting", DbType.Int32, receipt.NoOfPrinting);
                db.AddInParameter(cmd, "ReceiptType", DbType.String, receipt.ReceiptType);
                db.AddInParameter(cmd, "ExtReceiptId", DbType.String, receipt.ExtReceiptId);
                db.AddInParameter(cmd, "ExtReceiptDt", DbType.DateTime, receipt.ExtReceiptDt);
                db.AddInParameter(cmd, "SyncFlag", DbType.String, receipt.SyncFlag);
                db.AddInParameter(cmd, "PostDt", DbType.DateTime, receipt.PostDt);
                db.AddInParameter(cmd, "PostBranchServerId", DbType.String, receipt.PostBranchServerId);
                db.AddInParameter(cmd, "ModifiedDt", DbType.DateTime, receipt.ModifiedDt);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, receipt.ModifiedBy);
                db.AddInParameter(cmd, "Active", DbType.String, receipt.Active);
                db.ExecuteNonQuery(cmd, trans);
            }
            catch
            {
                throw;
            }
        }

        public void InsertAgentPaymentMaster(DbTransaction trans, AgentPayment agentPayment)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ePay_ins_Agent_Payment_Master");
                cmd.CommandTimeout = CMD_TIMEOUT;

                db.AddInParameter(cmd, "CompanyId", DbType.String, agentPayment.AgentId.Trim());
                db.AddInParameter(cmd, "CompanyName", DbType.String, agentPayment.AgentName.Trim());
                db.AddInParameter(cmd, "Address", DbType.String, agentPayment.AgentAddr.Trim());
                db.AddInParameter(cmd, "GAmount", DbType.Decimal, agentPayment.TotalBillAmount);
                db.AddInParameter(cmd, "Prefix", DbType.String, agentPayment.Prefix.Trim());
                db.AddInParameter(cmd, "DtId", DbType.String, agentPayment.DtId.Trim());
                db.AddInParameter(cmd, "PmId", DbType.String, agentPayment.PmId.Trim());
                db.AddInParameter(cmd, "PosId", DbType.String, agentPayment.PosId.Trim());
                db.AddInParameter(cmd, "BranchId", DbType.String, agentPayment.BranchId.Trim());
                db.AddInParameter(cmd, "BranchServerId", DbType.String, agentPayment.PostBranchServerId.Trim());
                db.AddInParameter(cmd, "ReceiptType", DbType.String, agentPayment.ReceiptType.Trim());
                db.AddInParameter(cmd, "UploadDt", DbType.DateTime, agentPayment.PostDt);
                db.AddInParameter(cmd, "ModifiedDt", DbType.DateTime, agentPayment.ModifiedDt);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, agentPayment.ModifiedBy.Trim());

                db.ExecuteNonQuery(cmd, trans);
            }
            catch
            {
                throw;
            }
        }

        public void InsertAgentPaymentDetail(DbTransaction trans, AgentPayment agentPayment)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ePay_ins_Agent_Payment_Detail");
                cmd.CommandTimeout = CMD_TIMEOUT;

                db.AddInParameter(cmd, "BranchServerId", DbType.String, agentPayment.PostBranchServerId.Trim());
                db.AddInParameter(cmd, "PtId", DbType.String, agentPayment.PtId.Trim());
                db.AddInParameter(cmd, "BankKey", DbType.String, agentPayment.BankKey.Trim());
                db.AddInParameter(cmd, "TranfAccNo", DbType.String, agentPayment.TranfAccNo.Trim());
                db.AddInParameter(cmd, "TranfDt", DbType.DateTime, agentPayment.TranfDt);
                db.AddInParameter(cmd, "Amount", DbType.Decimal, agentPayment.GAmount);
                db.AddInParameter(cmd, "PosId", DbType.String, agentPayment.PosId.Trim());
                db.AddInParameter(cmd, "ModifiedDt", DbType.DateTime, agentPayment.ModifiedDt);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, agentPayment.ModifiedBy.Trim());

                db.ExecuteNonQuery(cmd, trans);
            }
            catch
            {
                throw;
            }
        }

        public int GetUploadFileName(string fileName)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ePay_sel_UploadFileName");
                cmd.CommandTimeout = CMD_TIMEOUT;

                db.AddInParameter(cmd, "fileName", DbType.String, fileName);
                return (int)db.ExecuteScalar(cmd);
            }
            catch
            {
                throw;
            }
        }

        #region Cancel Agent Payment

        public List<CancelPayment> SearchAgentPaymentData(CancelPayment payment)
        {
            try
            {
                List<CancelPayment> agentPayList = new List<CancelPayment>();
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ePay_Search_AgentPayment");
                cmd.CommandTimeout = CMD_TIMEOUT;

                db.AddInParameter(cmd, "ReceiptId", DbType.String, payment.ReceiptId);
                db.AddInParameter(cmd, "ReceiverName", DbType.String, payment.ReceiverName);
                db.AddInParameter(cmd, "CompanyId", DbType.String, payment.AgentId);
                db.AddInParameter(cmd, "CompnayName", DbType.String , payment.AgentName);

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

                foreach (DataRow r in dt.Rows)
                {
                    CancelPayment agentPay = new CancelPayment();
                    agentPay.ReceiptId = DaHelper.GetString(r, "ReceiptId");
                    agentPay.PaymentDate = DaHelper.GetDate(r, "PaymentDt");
                    agentPay.AgentName = DaHelper.GetString(r, "CompanyName").Trim();
                    agentPay.GAmount = DaHelper.GetDecimal(r, "GAmount");
                    agentPay.ReceiverId = DaHelper.GetString(r, "ReceiverId");
                    agentPay.ReceiverName = DaHelper.GetString(r, "ReceiverName");

                    agentPayList.Add(agentPay);
                }

                return agentPayList;
            }
            catch
            {
                throw;
            }
        }

        public void InsertCancelPaymentData(DbTransaction trans, CancelPayment cancelPayment)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ePay_ins_Cancel_Payment");
                cmd.CommandTimeout = CMD_TIMEOUT;

                db.AddInParameter(cmd, "ReceiptId", DbType.String, cancelPayment.ReceiptId.Trim());
                db.AddInParameter(cmd, "Reason", DbType.String, cancelPayment.Reason.Trim());
                db.AddInParameter(cmd, "PosId", DbType.String, cancelPayment.PosId.Trim());
                db.AddInParameter(cmd, "PostedBranchId", DbType.String, cancelPayment.PostBranchId.Trim());
                db.AddInParameter(cmd, "PaymentDt", DbType.DateTime, cancelPayment.PaymentDate);
                db.AddInParameter(cmd, "ModifiedDt", DbType.DateTime, cancelPayment.ModifiedDt);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, cancelPayment.ModifiedBy);
               
                db.ExecuteNonQuery(cmd, trans);
            }
            catch
            {
                throw;
            }
        }


        #endregion


        #region Clearify
        public string InsertClearifyPayment(DbTransaction trans, DateTime PaymentDt, string PosId,
                                            string branchId, string postedServerId, string modifiedBy)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ePay_ins_ClearifyPayment");
            cmd.CommandTimeout = CMD_TIMEOUT;

            db.AddInParameter(cmd, "PaymentDt", DbType.DateTime, PaymentDt);
            db.AddInParameter(cmd, "PosId", DbType.String, PosId);
            db.AddInParameter(cmd, "CashierId", DbType.String, modifiedBy);
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "PostedServerId", DbType.String, postedServerId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, modifiedBy);

            return db.ExecuteScalar(cmd, trans).ToString();
        }

        public string InsertClearifyARPaymentType(DbTransaction trans, string PaymentId, decimal debtAmount, string PosId,
                                            string postedServerId, string modifiedBy)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ePay_ins_ClearifyARPaymentType");
            cmd.CommandTimeout = CMD_TIMEOUT;

            db.AddInParameter(cmd, "PaymentId", DbType.String, PaymentId);
            db.AddInParameter(cmd, "Amount", DbType.Decimal, debtAmount);
            db.AddInParameter(cmd, "PosId", DbType.String, PosId);
            //db.AddInParameter(cmd, "ChangeAmount", DbType.Decimal, pm.ChangeAmount);
            //db.AddInParameter(cmd, "FeeAmount", DbType.Decimal, pm.FeeAmount);
            //db.AddInParameter(cmd, "PtId", DbType.String, pm.PtId);
            //db.AddInParameter(cmd, "BankKey", DbType.String, pm.BankId);
            //db.AddInParameter(cmd, "ChqNo", DbType.String, pm.ChqNo);
            //db.AddInParameter(cmd, "ChqAccNo", DbType.String, pm.ChqAccNo);
            //db.AddInParameter(cmd, "ChqDt", DbType.DateTime, pm.ChqDt);
            //db.AddInParameter(cmd, "DraftFlag", DbType.String, pm.DraftFlag);
            //db.AddInParameter(cmd, "CashierChequeFlag", DbType.String, pm.CashierChequeFlag);
            //db.AddInParameter(cmd, "TranfAccNo", DbType.String, pm.DepositAccNo);
            //db.AddInParameter(cmd, "TranfDt", DbType.DateTime, pm.DepositDt);
            //db.AddInParameter(cmd, "CancelARPtId", DbType.String, null);
            db.AddInParameter(cmd, "PostedServerId", DbType.String, postedServerId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, modifiedBy);

            return db.ExecuteScalar(cmd, trans).ToString();
        }

        public string InsertClearifyAR(DbTransaction trans, string branchId, string caId, string description,
                                            decimal gamount, string posId, string postedServerId, string modifiedBy, string InvoiceNo)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ePay_ins_ClearifyAR");
            cmd.CommandTimeout = CMD_TIMEOUT;

            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "CaId", DbType.String, caId);
            //db.AddInParameter(cmd, "DtId", DbType.String, bill.DebtId);
            db.AddInParameter(cmd, "Description", DbType.String, description);
            //db.AddInParameter(cmd, "InvoiceNo", DbType.String, invoiceNo);
            //db.AddInParameter(cmd, "GroupInvoiceNo", DbType.String, bill.GroupInvoiceId);
            //db.AddInParameter(cmd, "BillBookId", DbType.String, bill.BillBookId);
            //db.AddInParameter(cmd, "Period", DbType.String, bill.Period);
            //db.AddInParameter(cmd, "MeterReadDt", DbType.DateTime, bill.DueDate);
            //db.AddInParameter(cmd, "ReadUnit", DbType.Decimal, bill.PreviousUnit);
            //db.AddInParameter(cmd, "LastUnit", DbType.Decimal, bill.LastUnit);
            //db.AddInParameter(cmd, "BaseAmount", DbType.Decimal, bill.BaseAmount);
            //db.AddInParameter(cmd, "FTUnitPrice", DbType.Decimal, bill.FtUnitPrice);
            //db.AddInParameter(cmd, "FTAmount", DbType.Decimal, bill.FtAmount);
            db.AddInParameter(cmd, "Amount", DbType.Decimal, gamount);
            //db.AddInParameter(cmd, "UnitPrice", DbType.Decimal, bill.UnitPrice);
            //db.AddInParameter(cmd, "Qty", DbType.Decimal, bill.Qty);
            //db.AddInParameter(cmd, "UnitTypeId", DbType.String, bill.UnitTypeId);
            //db.AddInParameter(cmd, "TaxCode", DbType.String, bill.TaxCode);
            //db.AddInParameter(cmd, "VatAmount", DbType.Decimal, bill.VatAmount);
            db.AddInParameter(cmd, "GAmount", DbType.Decimal, gamount);
            //db.AddInParameter(cmd, "DueDt", DbType.DateTime, bill.DueDate);
            //db.AddInParameter(cmd, "DisconnectDt", DbType.DateTime, bill.DisConnectDate);
            //db.AddInParameter(cmd, "DisconnectDoc", DbType.String, bill.DisconnectDocNo);
            //db.AddInParameter(cmd, "OriginalInvoiceNo", DbType.String, invoice.OriginalInvoiceNo);
            //db.AddInParameter(cmd, "InstallmentFlag", DbType.String, bill.InstallmentFlag);
            //db.AddInParameter(cmd, "LastInstallmentFlag", DbType.String, bill.LastInstallmentFlag);
            //db.AddInParameter(cmd, "SpotBillFlag", DbType.String, bill.SpotBillFlag);
            //db.AddInParameter(cmd, "InstallmentFlag", DbType.String, bill.InstallmentFlag);
            //db.AddInParameter(cmd, "LastInstallmentFlag", DbType.String, bill.LastInstallmentFlag);
            //db.AddInParameter(cmd, "CancelFlag", DbType.String, bill.CancelFlag);
            //db.AddInParameter(cmd, "ModifiedFlag", DbType.String, bill.ModifiedFlag);
            db.AddInParameter(cmd, "PosId", DbType.String, posId);
            db.AddInParameter(cmd, "PostedServerId", DbType.String, postedServerId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, modifiedBy);
            db.AddInParameter(cmd, "InvoiceNo", DbType.String, InvoiceNo);

            return db.ExecuteScalar(cmd, trans).ToString();
        }

        public string InsertClearifyARPayment(DbTransaction trans, string arPtId, string invoiceNo, string branchId, decimal gamount,
                                            DateTime paymentDt, string posId, string postedServerId, string modifiedBy)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ePay_ins_ClearifyARPayment");
            cmd.CommandTimeout = CMD_TIMEOUT;

            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "ARPtId", DbType.String, arPtId);
            db.AddInParameter(cmd, "InvoiceNo", DbType.String, invoiceNo);
            //db.AddInParameter(cmd, "PmId", DbType.String, PmId);
            db.AddInParameter(cmd, "PaymentDt", DbType.DateTime, paymentDt);
            //db.AddInParameter(cmd, "Qty", DbType.Decimal, iv.ToPayQty);
            //db.AddInParameter(cmd, "VatAmount", DbType.Decimal, iv.ToPayVatAmount);
            db.AddInParameter(cmd, "GAmount", DbType.Decimal, gamount);
            //db.AddInParameter(cmd, "AdjAmount", DbType.Decimal, iv.ToPayAdjAmount);
            //db.AddInParameter(cmd, "CancelARPmId", DbType.String, null);
            db.AddInParameter(cmd, "PosId", DbType.String, posId);
            db.AddInParameter(cmd, "PostedServerId", DbType.String, postedServerId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, modifiedBy);

            return db.ExecuteScalar(cmd, trans).ToString();
        }

        public int InsertClearifyRTARPaymentTypeARPayment(DbTransaction trans, string ARPtId, string ARPmId, decimal? Amount, string postedServerId, string modifiedBy)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ePay_ins_ClearifyRTARPaymentTypeARPayment");
            cmd.CommandTimeout = CMD_TIMEOUT;

            db.AddInParameter(cmd, "ARPtId", DbType.String, ARPtId);
            db.AddInParameter(cmd, "ARPmId", DbType.String, ARPmId);
            db.AddInParameter(cmd, "Amount", DbType.Decimal, Amount);
            db.AddInParameter(cmd, "PostedServerId", DbType.String, postedServerId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, modifiedBy);
            return   db.ExecuteNonQuery(cmd, trans);            
        }

        public int InsertClearifyReceipt(DbTransaction trans, Receipt receipt, string postId, string prefix)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ePay_ins_ClearifyReceipt");
            cmd.CommandTimeout = CMD_TIMEOUT;

            db.AddInParameter(cmd, "Prefix", DbType.String, prefix);
            db.AddInParameter(cmd, "PostId", DbType.String, postId);
            db.AddInParameter(cmd, "PaymentId", DbType.String, receipt.PaymentId);
            db.AddInParameter(cmd, "CaId", DbType.String, receipt.CaId);
            db.AddInParameter(cmd, "CaName", DbType.String, receipt.Name);
            db.AddInParameter(cmd, "CaAddress", DbType.String, receipt.Address);            
            db.AddInParameter(cmd, "ExtReceiptId", DbType.String, receipt.ExtReceiptId);
            db.AddInParameter(cmd, "ArPmId", DbType.String, receipt.ARPmId);
            db.AddInParameter(cmd, "PostBranchServerId", DbType.String, receipt.PostBranchServerId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, receipt.ModifiedBy);
            return db.ExecuteNonQuery(cmd, trans);
        }

        public int UpldateClearify(DbTransaction trans, SaveClearifyInfo saveClearifyItem, string receiptInvoiceNo)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ePay_upd_Clearify");
            cmd.CommandTimeout = CMD_TIMEOUT;

            db.AddInParameter(cmd, "IssueId", DbType.String, saveClearifyItem.IssueId);
            db.AddInParameter(cmd, "NewInvoiceNo", DbType.String, saveClearifyItem.NewInvoiceNo);
            db.AddInParameter(cmd, "ReceiptInvoiceNo", DbType.String, receiptInvoiceNo);
            db.AddInParameter(cmd, "FixedType", DbType.String, ((int)saveClearifyItem.ClearifyType).ToString());
            db.AddInParameter(cmd, "FixedBy", DbType.String, saveClearifyItem.ModifeidBy);
            db.AddInParameter(cmd, "PostedServerId", DbType.String, saveClearifyItem.PostBranchServerId);           
            return db.ExecuteNonQuery(cmd, trans);            
        }

        public int ClearCaPayment(DbTransaction trans, string posId, string orgInvoiceNo
                                    , string invoiceNo, string postBranchId, string modifiedBy
                                    , string branchId, string caId, string caName, string caAddress
                                    , decimal debtAmount)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ePay_cus_CaPayment");
            cmd.CommandTimeout = CMD_TIMEOUT;

            db.AddInParameter(cmd, "PosId", DbType.String, posId);
            db.AddInParameter(cmd, "OrgInvoiceNo", DbType.String, orgInvoiceNo);           	
	        db.AddInParameter(cmd, "InvoiceNo", DbType.String, invoiceNo);	
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, modifiedBy);
	        db.AddInParameter(cmd, "PostBranchId", DbType.String,postBranchId);
	        db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
	        db.AddInParameter(cmd, "CaId", DbType.String, caId);
	        db.AddInParameter(cmd, "CaName", DbType.String, caName);
	        db.AddInParameter(cmd, "CaAddress", DbType.String, caAddress);
            db.AddInParameter(cmd, "DebtAmount", DbType.Decimal, debtAmount);
            return db.ExecuteNonQuery(cmd, trans);
        }

        #endregion

    }
}
