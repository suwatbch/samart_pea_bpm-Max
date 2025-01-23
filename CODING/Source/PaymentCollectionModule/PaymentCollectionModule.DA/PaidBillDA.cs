using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using PEA.BPM.Architecture.CommonUtilities;
using System.Data;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.SqlServer.Server;

namespace PEA.BPM.PaymentCollectionModule.DA
{
    public class PaidBillDA
    {
        private int timeout = 2000;//5*60

        public List<Receipt> SearchPaidBill(PaidBillSearchParam param)
        {
            List<Receipt> receipts = new List<Receipt>();

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_ReceiptByDetail");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "ReceiptId", DbType.String, param.ReceiptId);
            db.AddInParameter(cmd, "CashierName", DbType.String, param.CashierName);
            db.AddInParameter(cmd, "CaId", DbType.String, param.CustomerId);
            db.AddInParameter(cmd, "CaName", DbType.String, param.CustomerName);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                Receipt r = new Receipt();
                r.ReceiptId =  DaHelper.GetString(dr, "ReceiptId");
                r.ReceiptDate = DaHelper.GetDate(dr, "ReceiptDt");
                r.CustomerId =  DaHelper.GetString(dr, "CaId");
                r.CustomerName =  DaHelper.GetString(dr, "CaName");
                r.CustomerAddress = DaHelper.GetString(dr, "CaAddress");
                r.PosId = DaHelper.GetString(dr, "PosId");
                r.CashierName = DaHelper.GetString(dr, "CashierName");
                r.GAmount = DaHelper.GetDecimal(dr, "Amount");
                
                receipts.Add(r);
            }

            return receipts;
        }


        public List<ReceiptGroupMainData> SelectGroupReceiptMainData(string ReceiptId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_GroupReceiptMainData");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "ReceiptId", DbType.String, ReceiptId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            var results = new List<ReceiptGroupMainData>();
            
            if (dt.Rows.Count == 0)
            {
                return results;
            }
            foreach (DataRow r in dt.Rows)
            {
                var m                       = new ReceiptGroupMainData();
                m.GroupReceiptId            = DaHelper.GetString(r, "Receiptid");
                m.GroupReceiptPeriodText    = DaHelper.GetString(r, "GroupReceiptPeriodText");
                m.GroupReceiptQty           = DaHelper.GetDecimal(r, "GroupReceiptQty");
                m.GroupReceiptInstallmentText = DaHelper.GetString(r, "GroupReceiptInstallmentText");
                m.GroupReceiptInvoiceNo     = DaHelper.GetString(r, "InvoiceNo");
                m.GroupReceiptAmount        = DaHelper.GetDecimal(r, "GroupReceiptAmount");
                m.GroupReceiptVatTotal      = DaHelper.GetDecimal(r, "GroupReceiptVatTotal");
                m.GroupReceiptTotal         = DaHelper.GetDecimal(r, "GroupReceiptTotal");
                results.Add(m);
            }

            return results;
        }


        // DCR รวมใบเสร็จแผนผ่อน 2021OCT (PaidDA)
        //public string GetGroupReceiptPaymentMethods(DbTransaction trans, string receiptId)
        //{
        //    Database db = DatabaseFactory.CreateDatabase("POSDatabase");
        //    DbCommand cmd = db.GetStoredProcCommand("pc_get_GroupReceiptPaymentMethods");
        //    cmd.CommandTimeout = timeout;
        //    db.AddInParameter(cmd, "ReceiptId", DbType.String, receiptId);
        //    DataTable dt = new DataTable();
        //    if (trans != null)
        //    {
        //        dt = db.ExecuteDataSet(cmd, trans).Tables[0];
        //    }
        //    else
        //    {
        //        dt = db.ExecuteDataSet(cmd).Tables[0];
        //    }

        //    StringBuilder sbd = new StringBuilder();
        //    int AllRows = dt.Rows.Count;
        //    int currentRows = 0;
        //    foreach (DataRow r in dt.Rows)
        //    {
        //        currentRows += 1;
        //        string methods = DaHelper.GetString(r, "PaymentText");
        //        if (methods != null)
        //        {
        //            sbd.Append(methods);
        //            if (currentRows < AllRows)
        //            {
        //                sbd.Append("|");
        //            }
        //        }
        //    }

        //    string result = null;
        //    result = sbd.ToString();
        //    return result;
        //}


        //// PaidBillDA
        public List<string> GetGroupReceiptPaymentMethods(DbTransaction trans, string receiptId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_get_GroupReceiptPaymentMethods");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "ReceiptId", DbType.String, receiptId);

            List<string> PaymentMethodsText = new List<string>();

            #region Pre-Printed Payment Methods
            DataTable dt = new DataTable();
            if (trans != null)
            {
                dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            }
            else
            {
                dt = db.ExecuteDataSet(cmd).Tables[0];
            }

            StringBuilder sbd = new StringBuilder();
            int AllRows = dt.Rows.Count;
            int currentRows = 0;
            foreach (DataRow r in dt.Rows)
            {
                currentRows += 1;
                string methods = DaHelper.GetString(r, "PaymentText");
                if (methods != null)
                {
                    sbd.Append(methods);
                    if (currentRows < AllRows)
                    {
                        sbd.Append("|");
                    }
                }
            }

            PaymentMethodsText.Add(sbd.ToString());
            #endregion

            #region POS-Slip Payment Methods
            DataTable dt2 = new DataTable();
            if (trans != null)
            {
                dt2 = db.ExecuteDataSet(cmd, trans).Tables[1];
            }
            else
            {
                dt2 = db.ExecuteDataSet(cmd).Tables[1];
            }

            StringBuilder sbd2 = new StringBuilder();
            int AllRows2 = dt2.Rows.Count;
            int currentRows2 = 0;
            foreach (DataRow r in dt2.Rows)
            {
                currentRows2 += 1;
                string methods = DaHelper.GetString(r, "PaymentText");
                if (methods != null)
                {
                    sbd2.Append(methods);
                    if (currentRows2 < AllRows2)
                    {
                        sbd2.Append("@");
                    }
                }
            }

            PaymentMethodsText.Add(sbd2.ToString());
            #endregion

            return PaymentMethodsText;
        }

        public string GetGroupReceiptPaymentMethodsPOSSlip(DbTransaction trans, string receiptId)
        {
            Database db     = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd   = db.GetStoredProcCommand("pc_get_GroupReceiptPaymentMethodsPosSlip");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "ReceiptId", DbType.String, receiptId);
            DataTable dt = new DataTable();
            if (trans != null)
            {
                dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            }
            else
            {
                dt = db.ExecuteDataSet(cmd).Tables[0];
            }

            StringBuilder sbd = new StringBuilder();
            int AllRows = dt.Rows.Count;
            int currentRows = 0;
            foreach (DataRow r in dt.Rows)
            {
                currentRows += 1;
                string methods = DaHelper.GetString(r, "PaymentText");
                if (methods != null)
                {
                    sbd.Append(methods);
                    if (currentRows < AllRows)
                    {
                        sbd.Append("@");
                    }
                }
            }

            string result = null;
            result = sbd.ToString();
            return result;
        }

        public string GetReceiptPrintingSeqTextByReceiptId(DbTransaction trans, string ReceiptId)
        {
            Database db         = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd       = db.GetStoredProcCommand("pc_sel_GroupReceiptPrintingSeq");
            cmd.CommandTimeout  = timeout;
            db.AddInParameter(cmd, "ReceiptId", DbType.String, ReceiptId);
            DataTable dt        = new DataTable();
            if (trans != null)
            {
                dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            }
            else
            {
                dt = db.ExecuteDataSet(cmd).Tables[0];
            }

            string PrintingSeqText = null;

            if (dt.Rows.Count == 0)
            {
                return PrintingSeqText;
            }
            foreach (DataRow r in dt.Rows)
            {
                PrintingSeqText = DaHelper.GetString(r, "PrintingSeqText");
            }

            return PrintingSeqText;
        }

        public ReceiptGroupMainData GetGroupReceiptMainData(DbTransaction trans,string ReceiptId)
        {
            Database db         = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd       = db.GetStoredProcCommand("pc_get_GroupReceiptMainData");
            cmd.CommandTimeout  = timeout;
            db.AddInParameter(cmd, "ReceiptId", DbType.String, ReceiptId);

            DataTable dt    = new DataTable();
            if (trans == null)
            {
                dt = db.ExecuteDataSet(cmd).Tables[0];
            }
            else
            {
                dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            }
            
            var result          = new ReceiptGroupMainData();
            if (dt.Rows.Count == 0)
            {
                return result;
            }

            foreach (DataRow r in dt.Rows)
            {
                result.GroupReceiptId           = DaHelper.GetString(r, "Receiptid");
                result.GroupReceiptPeriodText   = DaHelper.GetString(r, "GroupReceiptPeriodText");
                result.GroupReceiptQty          = DaHelper.GetDecimal(r, "GroupReceiptQty");
                result.GroupReceiptInstallmentText = DaHelper.GetString(r, "GroupReceiptInstallmentText");
                result.GroupReceiptInvoiceNo    = DaHelper.GetString(r, "InvoiceNo");
                result.GroupReceiptAmount       = DaHelper.GetDecimal(r, "GroupReceiptAmount");
                result.GroupReceiptVatTotal     = DaHelper.GetDecimal(r, "GroupReceiptVatTotal");
                result.GroupReceiptTotal        = DaHelper.GetDecimal(r, "GroupReceiptTotal");
                result.GroupReceiptRateTypeText = DaHelper.GetString(r, "GroupReceiptRateTypeText");
                result.GroupReceiptMeterIdText  = DaHelper.GetString(r, "GroupReceiptMeterIdText");
                result.GroupReceiptXReceiptId   = DaHelper.GetString(r, "XReceiptList");
            }

            return result;
        }

        public ReceiptGroupMainData GetGroupReceiptMainData(string ReceiptId)
        {
            Database db         = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd       = db.GetStoredProcCommand("pc_get_GroupReceiptMainData");
            cmd.CommandTimeout  = timeout;
            db.AddInParameter(cmd, "ReceiptId", DbType.String, ReceiptId);
            DataTable dt        = db.ExecuteDataSet(cmd).Tables[0];
            var result          = new ReceiptGroupMainData();
            if (dt.Rows.Count == 0)
            {
                return result;
            }
            foreach (DataRow r in dt.Rows)
            {
                result.GroupReceiptId           = DaHelper.GetString(r, "Receiptid");
                result.GroupReceiptPeriodText   = DaHelper.GetString(r, "GroupReceiptPeriodText");
                result.GroupReceiptQty          = DaHelper.GetDecimal(r, "GroupReceiptQty");
                result.GroupReceiptInstallmentText = DaHelper.GetString(r, "GroupReceiptInstallmentText");
                result.GroupReceiptInvoiceNo    = DaHelper.GetString(r, "InvoiceNo");
                result.GroupReceiptAmount       = DaHelper.GetDecimal(r, "GroupReceiptAmount");
                result.GroupReceiptVatTotal     = DaHelper.GetDecimal(r, "GroupReceiptVatTotal");
                result.GroupReceiptTotal        = DaHelper.GetDecimal(r, "GroupReceiptTotal");
                result.GroupReceiptRateTypeText = DaHelper.GetString(r, "GroupReceiptRateTypeText");
                result.GroupReceiptMeterIdText  = DaHelper.GetString(r, "GroupReceiptMeterIdText");
                result.GroupReceiptXReceiptId   = DaHelper.GetString(r, "XReceiptList");
            }

            return result;
        }

        public ReceiptGroupItems GetGroupReceiptItemsData(string ReceiptId)
        {
            Database db         = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd       = db.GetStoredProcCommand("pc_get_GroupReceiptItemsData");
            cmd.CommandTimeout  = timeout;
            db.AddInParameter(cmd, "ReceiptId", DbType.String, ReceiptId);
            DataTable dt        = db.ExecuteDataSet(cmd).Tables[0];
            var result = new ReceiptGroupItems();
            if (dt.Rows.Count == 0)
            {
                return result;
            }
            foreach (DataRow r in dt.Rows)
            {
                result.MainGroupReceiptId   = DaHelper.GetString(r, "MainGroupReceiptId");
                result.ReceiptId            = DaHelper.GetString(r, "Receiptid");
                result.InvoiceNo            = DaHelper.GetString(r, "InvoiceNo");               
            }

            return result;
        }

        public List<ReceiptGroupItems> GetExtGroupReceiptItemsData(string ReceiptId, DbTransaction trans)
        {
            Database db         = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd       = db.GetStoredProcCommand("pc_sel_GroupReceiptMainData");
            cmd.CommandTimeout  = timeout;
            db.AddInParameter(cmd, "ReceiptId", DbType.String, ReceiptId);
            DataSet ds          = (null == trans) ? db.ExecuteDataSet(cmd) : db.ExecuteDataSet(cmd, trans);
            //DataTable dt        = db.ExecuteDataSet(cmd).Tables[0];
            DataTable dt        = ds.Tables[0];
            var result          = new List<ReceiptGroupItems>();
            if (dt.Rows.Count == 0)
            {
                return result;
            }

            foreach (DataRow r in dt.Rows)
            {
                ReceiptGroupItems tmp = new ReceiptGroupItems();
                tmp.MainGroupReceiptId  = DaHelper.GetString(r, "MainGroupReceiptId");
                tmp.ReceiptId           = DaHelper.GetString(r, "Receiptid");
                result.Add(tmp);                                
            }

            return result;
        }



        


        public List<Receipt> SearchReceipt(ReceiptSearchParam param)
        {
            List<Receipt> receipts = new List<Receipt>();

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_ReceiptByDetail");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "PaymentId", DbType.String, param.PaymentId);
            db.AddInParameter(cmd, "ReceiptId", DbType.String, param.ReceiptId);
            db.AddInParameter(cmd, "CashierName", DbType.String, param.CashierName);
            db.AddInParameter(cmd, "CaId", DbType.String, param.CustomerId);
            db.AddInParameter(cmd, "CaName", DbType.String, param.CustomerName);
            db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
            db.AddInParameter(cmd, "IsCancel", DbType.String, (param.IsCancel == true ? "1" : "0"));

            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Receipt r = new Receipt();
                    r.ReceiptId = DaHelper.GetString(dr, "ReceiptId");
                    r.DisplayReceiptId = DaHelper.GetString(dr, "DisplayReceiptId");
                    r.PrintingSequence = DaHelper.GetShort(dr, "PrintingSequence");
                    r.ReceiptDate = DaHelper.GetDate(dr, "ReceiptDt");
                    r.CustomerId = DaHelper.GetString(dr, "CaId");
                    r.CustomerName = DaHelper.GetString(dr, "CaName");
                    r.CustomerAddress = DaHelper.GetString(dr, "CaAddress");
                    r.PaymentId = DaHelper.GetString(dr, "PaymentId");
                    r.PosId = DaHelper.GetString(dr, "PosId");
                    r.CashierName = DaHelper.GetString(dr, "CashierName");
                    r.GAmount = DaHelper.GetDecimal(dr, "GAmount");

                    //if (param.IsCancel == true) // กรณียกเลิกใบเสร็จ
                    {
                        string tmp_receipt = r.ReceiptId;
                        if (tmp_receipt != null)
                        {
                            var GR_Main = new ReceiptGroupMainData();                            
                            GR_Main     = GetGroupReceiptMainData(tmp_receipt);
                                                       

                            if (GR_Main.GroupReceiptId != null)
                            {
                                if (GR_Main.GroupReceiptId == r.ReceiptId)
                                {
                                    r.GroupReceiptId                = GR_Main.GroupReceiptId;
                                    r.GroupReceiptPeriodText        = GR_Main.GroupReceiptPeriodText;
                                    r.GroupReceiptQty               = GR_Main.GroupReceiptQty;
                                    r.GroupReceiptInstallmentText   = GR_Main.GroupReceiptInstallmentText;
                                    r.GroupReceiptInvoiceNo         = GR_Main.GroupReceiptInvoiceNo;
                                    r.GroupReceiptAmount            = GR_Main.GroupReceiptAmount;
                                    r.GroupReceiptVatTotal          = GR_Main.GroupReceiptVatTotal;
                                    r.GroupReceiptTotal             = GR_Main.GroupReceiptTotal;
                                    r.GroupRefReceiptId             = GR_Main.GroupReceiptId;
                                    r.GroupReceiptMeterIdText       = GR_Main.GroupReceiptMeterIdText;
                                    r.GroupReceiptRateTypeText      = GR_Main.GroupReceiptRateTypeText;
                                    r.GroupReceiptIsMain            = true;                                    
                                }
                            }
                            else
                            {
                                var GR_Items    = new ReceiptGroupItems();
                                GR_Items        = GetGroupReceiptItemsData(r.ReceiptId);
                                if (GR_Items.ReceiptId != null)
                                {
                                    r.GroupRefReceiptId     = GR_Items.MainGroupReceiptId;
                                    r.GroupReceiptId        = GR_Items.ReceiptId;
                                    r.GroupReceiptIsItems   = true;
                                }
                            }
                        }
                    }

                    receipts.Add(r);

                    DataTable dtPmInfo = ds.Tables[1];
                    string filter = string.Format(" ReceiptId='{0}' ", r.ReceiptId.Trim());
                    DataRow[] drs = dtPmInfo.Select(filter);
                    r.PmInfo = new List<PaymentTypeInfo>();

                    for (int i = 0; i < drs.Length; i++)
                    {
                        PaymentTypeInfo pmInfo = new PaymentTypeInfo();
                        pmInfo.ReceiptId = DaHelper.GetString(drs[i], "receiptId");
                        pmInfo.PtId = DaHelper.GetString(drs[i], "ptId");
                        pmInfo.BankKey = DaHelper.GetString(drs[i], "bankKey");
                        pmInfo.ChqNo = DaHelper.GetString(drs[i], "chqNo");
                        pmInfo.ChqAccNo = DaHelper.GetString(drs[i], "chqAccNo");
                        pmInfo.ChqDt = DaHelper.GetDateTime(drs[i], "chqDt");
                        pmInfo.Amount = DaHelper.GetDecimal(drs[i], "amount");
                        r.PmInfo.Add(pmInfo);
                    }

                }


                


                // Add receipt relation
                if (param.PaymentId != null)
                {
                    dt = ds.Tables[2];

                    Dictionary<string, Receipt> rr = new Dictionary<string, Receipt>();
                    foreach (Receipt r in receipts)
                    {
                        rr.Add(r.ReceiptId, r);
                    }

                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        DataRow dr = dt.Rows[k];
                        Receipt receipt = rr[dr["ReceiptId"].ToString()];
                        //if (receipt.RelatedReceipts.Count == 0)
                        {

                            string filter = string.Format("(ARPtId='{0}' And PtId<>'1')" // วิธีการจ่ายเงินเดียวกัน ที่ไม่ใช่ Cash
                                + " OR (OriginalInvoiceNo='{1}' And DtId<>'{3}')" // ตัวต้นของดอกเบี้ยปกติ                            
                                + " OR (InvoiceNo='{2}' And DtId<>'{3}')" // ตัวดอกเบี้ยปกติ
                                + " OR (BillBookId='{4}')" // ดอกเบี้ย GroupInvoice
                                + " OR (BillBookId IS NOT NULL AND PaymentId='{5}')" // รับหลาย Group Invoice ในการรับเงินหนึ่งครั้ง
                                + " OR (IsCancelAll='1')", // ต้องยกเลิกทั้งหมด
                                dr["ARPtId"].ToString().Trim(),
                                dr["InvoiceNo"].ToString().Trim(),
                                dr["OriginalInvoiceNo"].ToString().Trim(),
                                dr["DtId"].ToString().Trim(),
                                dr["BillBookId"].ToString().Trim(),
                                dr["PaymentId"].ToString().Trim());
                            DataRow[] drs = dt.Select(filter);

                            for (int i = 0; i < drs.Length; i++)
                            {
                                string rid = drs[i]["ReceiptId"].ToString();
                                Receipt rreceipt = rr[rid];


                                if (receipt.ReceiptId != rid && !receipt.RelatedReceipts.Contains(rreceipt))
                                {
                                    receipt.RelatedReceipts.Add(rreceipt);
                                }
                            }
                        }
                    }
                }
            }

            return receipts;
        }

        public ReceiptDetail GetReceiptDetail(string receiptId)
        {
            ReceiptDetail rd = new ReceiptDetail();

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_get_ReceiptDetail");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "ReceiptId", DbType.String, receiptId);
            DataSet ds = db.ExecuteDataSet(cmd);

            DataTable dt;

            List<PaidItem> paidItems = new List<PaidItem>();
            dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                PaidItem item = new PaidItem();
                item.DebtId = DaHelper.GetString(dr, "DtId");
                item.DebtType = DaHelper.GetString(dr, "DtName");
                item.Period = DaHelper.GetString(dr, "Period");
                item.RateTypeId = DaHelper.GetString(dr, "RateTypeId");
                item.Description = DaHelper.GetString(dr, "Description");
                item.CutOffDate = DaHelper.GetDate(dr, "CutOfDate");
                item.Amount = DaHelper.GetDecimal(dr, "GAmount");
                paidItems.Add(item);
            }
            rd.PaidItems = paidItems;

            List<PaidMethod> paidMethods = new List<PaidMethod>();
            dt = ds.Tables[1];
            foreach (DataRow dr in dt.Rows)
            {
                PaidMethod method = new PaidMethod();
                //method.Method = DaHelper.GetString(dr, "PtName");
                //method.Description = DaHelper.GetString(dr, "Description");
                //method.ActualAmount = DaHelper.GetDecimal(dr, "ActualAmount");
                //method.PaidAmount = DaHelper.GetDecimal(dr, "Amount");
                paidMethods.Add(method);
            }
            rd.PaidMethods = paidMethods;

            return rd;
        }

        public string CancelReceipt(DbTransaction trans, List<string> receiptIds, string reason, string posId, string terminalCode, string branchId,
                                        string branchName, string cashierId, string cashierName, string workId, string postedServerId, string repayReceiptId)
        {
            SqlDatabase db     = (SqlDatabase)DatabaseFactory.CreateDatabase("POSDatabase");
            //Database db = DatabaseFactory.CreateDatabase("POSDatabase");            
            DbCommand cmd      = db.GetStoredProcCommand("pc_upd_CancelReceipt");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "ReceiptIds", SqlDbType.Structured, GetRecieptList(receiptIds));
            db.AddInParameter(cmd, "Reason", DbType.String, reason);
            db.AddInParameter(cmd, "PosId", DbType.String, posId);
            db.AddInParameter(cmd, "TerminalCode", DbType.String, terminalCode);
            db.AddInParameter(cmd, "CashierId", DbType.String, cashierId);
            db.AddInParameter(cmd, "CashierName", DbType.String, cashierName);
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "BranchName", DbType.String, branchName);
            db.AddInParameter(cmd, "WorkId", DbType.String, workId);
            db.AddInParameter(cmd, "PostedServerId", DbType.String, postedServerId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, cashierId);
            db.AddInParameter(cmd, "RepayReceiptId", DbType.String, repayReceiptId);

            return db.ExecuteScalar(cmd, trans).ToString();
        }

        private ReceiptCollection GetRecieptList(List<string> receiptIds)
        {
            ReceiptCollection recCollect = new ReceiptCollection();
            foreach (string r in receiptIds)
            {
                recCollect.Add(r);
            }

            return recCollect;
        }

        private PaymentTypeCollection GetPaymentIdList(List<string> paymentIds)
        {
            PaymentTypeCollection recCollect = new PaymentTypeCollection();
            foreach (string r in paymentIds)
            {
                recCollect.Add(r);
            }

            return recCollect;
        }

        public void RepayLastReceipt(DbTransaction trans, string reprintReceiptId, string cancelPaymentId, string branchId, string postedServerId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_upd_RepayLastReceipt");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "RepayReceiptId", DbType.String, reprintReceiptId);
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "CancelPaymentId", DbType.String, cancelPaymentId);
            db.AddInParameter(cmd, "PostedServerId", DbType.String, postedServerId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, Session.User.Id);
            db.ExecuteNonQuery(cmd, trans);
        }
        
        public List<PaidMethod> GetReturnPayment(DbTransaction trans, string cancelPaymentId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_get_ReturnPayment");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "CancelPaymentId", DbType.String, cancelPaymentId);
            DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];

            List<PaidMethod> pms = new List<PaidMethod>();
            foreach (DataRow dr in dt.Rows)
            {
                PaidMethod pm       = new PaidMethod();
                pm.MethodId         = DaHelper.GetString(dr, "PtId");
                pm.BankName         = DaHelper.GetString(dr, "BankName");
                pm.ChqNo            = DaHelper.GetString(dr, "ChqNo");
                pm.ChqAccNo         = DaHelper.GetString(dr, "ChqAccNo");
                pm.ChqDate          = DaHelper.GetDate(dr, "ChqDt");
                pm.TransferAccNo    = DaHelper.GetString(dr, "TranfAccNo");
                pm.TransferDate     = DaHelper.GetDate(dr, "TranfDt");
                pm.Amount           = DaHelper.GetDecimal(dr, "Amount");

                pms.Add(pm);
            }

            return pms;
        }

        public void SaveReprintReceipt(DbTransaction trans, string reprintReceiptId, string newReceiptId, string postedServerId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_ins_RepayLastReceipt");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "RepayReceiptId", DbType.String, reprintReceiptId);
            db.AddInParameter(cmd, "NewReceiptId", DbType.String, newReceiptId);
            db.AddInParameter(cmd, "PostedServerId", DbType.String, postedServerId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, Session.User.Id);
            db.ExecuteNonQuery(cmd, trans);

            //DCR รวมใบเสร็จแผนผ่อน 2021-09-30 Re-Check this 'newReceptId' is 'groupReceipt' or not
            if (newReceiptId.Contains("X") == true)
            {
                ConfirmIsGroupReceiptLastRepay(trans, newReceiptId);
            }
        }

        //DCR รวมใบเสร็จแผนผ่อน 2021-09-30 (PaidBillDA)
        public void ConfirmIsGroupReceiptLastRepay(DbTransaction trans, string newReceiptId)
        {
            Database db         = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd       = db.GetStoredProcCommand("pc_get_ConfirmGroupReceiptByListInvoiceNo");
            cmd.CommandTimeout  = timeout;
            db.AddInParameter(cmd, "ListReceiptId", DbType.String, newReceiptId);
            db.AddInParameter(cmd, "ListInvoiceNo", DbType.String, "");
            db.AddInParameter(cmd, "PaymentDt", DbType.DateTime, DateTime.Now);
            db.ExecuteNonQuery(cmd, trans);            
        }




        public PrintingInfo GetReceiptforPrint(DbTransaction trans, string receiptId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_get_ReceiptForPrint");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "ReceiptId", DbType.String, receiptId);
            DataSet ds = (null == trans) ? db.ExecuteDataSet(cmd) : db.ExecuteDataSet(cmd, trans);
            
            List<PaymentMethod> pms = new List<PaymentMethod>();
            Random random = new Random();
            DataTable dt = ds.Tables[0];
            foreach (DataRow drx in dt.Rows)
            {
                PaymentMethod pm = new PaymentMethod();                
                pm.UiRefId = random.Next();
                pm.ARPtId = DaHelper.GetString(drx, "ARPtId");
                pm.PtId = DaHelper.GetString(drx, "PtId");
                pm.PtName = DaHelper.GetString(drx, "PtName");
                pm.ToPayAmount = DaHelper.GetDecimal(drx, "Amount");
                pm.ChangeAmount = DaHelper.GetDecimal(drx, "ChangeAmount");
                pm.DepositAccNo = DaHelper.GetString(drx, "TranfAccNo");
                pm.ChqNo = DaHelper.GetString(drx, "ChqNo");
                pm.ChqDt = DaHelper.GetDate(drx, "ChqDt");

                Bank bank = new Bank();
                bank.BankKey = DaHelper.GetString(drx, "BankKey");
                bank.BankName = DaHelper.GetString(drx, "BankName");
                pm.Bank = bank;

                pm.ToPayInvoices = new List<InvoicePaymentMethod>();
                
                pms.Add(pm);
            }

            dt = ds.Tables[1];
            DataRow dr = dt.Rows[0];
            PrintingReceipt receipt = new PrintingReceipt();
            PrintingInvoice iv      = new PrintingInvoice();

            

            if (receiptId.Substring(0, 1) == "H")
            {
                receipt.ReceiptId = receiptId;
                receipt.PrintingSequence = DaHelper.GetShort(dr, "PrintingSequence").Value;
                receipt.TotalReceipt = DaHelper.GetShort(dr, "TotalReceipt").Value;
                receipt.CustomerId = DaHelper.GetString(dr, "CaId");
                receipt.CustomerName = DaHelper.GetString(dr, "RcName");
                receipt.CustomerAddress = DaHelper.GetString(dr, "RcAddress");

                //Tax13
                receipt.CaTaxId = DaHelper.GetString(dr, "RcCaTaxId");
                receipt.CaTaxBranch = DaHelper.GetString(dr, "RcCaTaxBranch");

                receipt.BranchName = DaHelper.GetString(dr, "BranchName");
                receipt.BranchAddress = DaHelper.GetString(dr, "BranchAddress");
                receipt.BranchNumber = DaHelper.GetString(dr, "BranchNumber");
                receipt.TerminalCode = DaHelper.GetString(dr, "TerminalCode");

                receipt.ContractType = DaHelper.GetString(dr, "CtId");
                receipt.TotalAmount = DaHelper.GetDecimal(dr, "TotalAmount").Value;
                receipt.ChangeAmount = DaHelper.GetDecimal(dr, "TotalChangeAmount").Value;
                receipt.AdjChangeAmount = DaHelper.GetDecimal(dr, "AdjAmount").Value;
                receipt.PaidAmount = DaHelper.GetDecimal(dr, "PaidAmount").Value;

                receipt.PaymentDate = DaHelper.GetDate(dr, "PaymentDt").Value;
                receipt.CashierId = DaHelper.GetString(dr, "CashierId");
                receipt.CashierName = DaHelper.GetString(dr, "CashierName");

                receipt.EnergyAmount = DaHelper.GetDecimal(dr, "EnergyAmount").Value;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr = dt.Rows[i];
                    iv = new PrintingInvoice();

                    iv.PaymentId = DaHelper.GetString(dr, "PaymentId");
                    iv.CaId = receipt.CustomerId;
                    iv.BranchId = DaHelper.GetString(dr, "TechBranchId");
                    iv.GroupInvoiceReceiptType = DaHelper.GetString(dr, "GroupIvReceiptType");
                    Random iv_random = new Random();
                    iv.UiRefId = iv_random.Next();

                    iv.Bills = new List<Bill>();
                    DataTable dt2 = ds.Tables[2];

                    foreach (DataRow drx in dt2.Rows)
                    {
                        if (DaHelper.GetString(dr, "InvoiceNo") == DaHelper.GetString(drx, "InvoiceNo"))
                        {
                            Bill b = new Bill();
                            b.CustomerId = iv.CaId;
                            b.InvoiceNo = DaHelper.GetString(drx, "InvoiceNo");
                            b.GroupInvoiceId = DaHelper.GetString(drx, "GroupInvoiceNo");
                            b.DebtId = DaHelper.GetString(drx, "DtId");
                            b.DebtType = DaHelper.GetString(drx, "DtName");
                            b.Description = DaHelper.GetString(drx, "Description");

                            b.Period = DaHelper.GetString(drx, "Period");
                            b.PreviousUnit = DaHelper.GetDecimal(drx, "ReadUnit");
                            b.LastUnit = DaHelper.GetDecimal(drx, "LastUnit");
                            b.MeterReadDate = DaHelper.GetDate(drx, "MeterReadDt");
                            b.UnitPrice = DaHelper.GetDecimal(drx, "UnitPrice");
                            b.Qty = DaHelper.GetDecimal(drx, "Qty");
                            b.BaseAmount = DaHelper.GetDecimal(drx, "BaseAmount");
                            b.FtUnitPrice = DaHelper.GetDecimal(drx, "FTUnitPrice");
                            b.FtAmount = DaHelper.GetDecimal(drx, "FTAmount");
                            b.AmountExVat = DaHelper.GetDecimal(drx, "Amount");
                            b.TaxCode = DaHelper.GetString(drx, "TaxCode");
                            b.TaxRate = DaHelper.GetDecimal(drx, "TaxRate");
                            b.VatAmount = DaHelper.GetDecimal(drx, "VatAmount");
                            b.GAmount = DaHelper.GetDecimal(drx, "GAmount");
                            b.ToPayVatAmount = DaHelper.GetDecimal(drx, "ToPayVatAmount");
                            b.ToPayGAmount = DaHelper.GetDecimal(drx, "ToPayGAmount");
                            b.MeterId = DaHelper.GetString(dr, "MeterId");
                            b.RateTypeId = DaHelper.GetString(dr, "RateTypeId");


                            b.BookCreateDt = DaHelper.GetDate(drx, "BookCreateDt");
                            b.BookTotalBill = DaHelper.GetInt(drx, "BookTotalBill");
                            b.BookTotalGAmount = DaHelper.GetDecimal(drx, "BookTotalGAmount");
                            b.BookAdvanceAmount = DaHelper.GetDecimal(drx, "BookAdvanceAmount");
                            b.BookTotalBillCollected = DaHelper.GetInt(drx, "BookTotalBillCollected");
                            b.BillBookId = DaHelper.GetString(drx, "BillBookId");
                            b.ControllerId = DaHelper.GetString(drx, "BillControllerId");


                            iv.CaDoc = DaHelper.GetString(drx, "CaDoc");
                            iv.OriginalInvoiceNo = DaHelper.GetString(drx, "OriginalInvoiceNo");
                            iv.SpotBillInvoiceNo = DaHelper.GetString(drx, "SpotBillInvoiceNo");
                            iv.InstallmentPeriod = DaHelper.GetInt(drx, "InstallmentPeriod");
                            iv.InstallmentTotalPeriod = DaHelper.GetInt(drx, "InstallmentTotalPeriod");
                            iv.Qty = DaHelper.GetDecimal(drx, "Qty");

                            iv.ToPayQty = DaHelper.GetDecimal(drx, "ToPayQty");
                            iv.PaidGAmount = DaHelper.GetDecimal(drx, "ToBePaidGAmount");
                            iv.ToPayGAmount = DaHelper.GetDecimal(drx, "ToPayGAmount");
                            iv.ToPayVatAmount = DaHelper.GetDecimal(drx, "ToPayVatAmount");
                            iv.InvoiceNo = b.InvoiceNo;
                            iv.InvoiceDate = DaHelper.GetDate(drx, "InvoiceDt");
                            iv.AmountExVat = b.AmountExVat;
                            iv.VatAmount = b.VatAmount;
                            iv.GAmount = b.GAmount;
                            iv.PartialPayment = DaHelper.GetByte(drx, "PartialPayment");

                            ////Tax13
                            //iv.CaTaxId = receipt.CaTaxId;
                            //iv.CaTaxBranch = receipt.CaTaxBranch;

                            iv.Bills.Add(b);
                        }
                    }

                    #region DCR รวมใบเสร็จแผนผ่อน 2021AUG พิมพ์ซ่อมใบเสร็จรับเงิน (PaidBillDA)
                    ReceiptGroupMainData GR_DATA = GetGroupReceiptMainData(trans,receipt.ReceiptId);
                    if (GR_DATA.GroupReceiptId == receipt.ReceiptId)
                    {                        
                        iv.GroupReceiptId           = GR_DATA.GroupReceiptId;
                        iv.GroupReceiptPeriodText   = GR_DATA.GroupReceiptPeriodText;
                        iv.GroupReceiptQty          = GR_DATA.GroupReceiptQty;                        
                        iv.GroupReceiptAmount       = GR_DATA.GroupReceiptAmount;
                        iv.GroupReceiptVatTotal     = GR_DATA.GroupReceiptVatTotal;
                        iv.GroupReceiptTotal        = GR_DATA.GroupReceiptTotal;
                        iv.GroupReceiptInstallmentText = GR_DATA.GroupReceiptInstallmentText;
                        iv.GroupReceiptMeterIdText  = GR_DATA.GroupReceiptMeterIdText;
                        iv.GroupReceiptRateTypeText = GR_DATA.GroupReceiptRateTypeText;
                        iv.GroupXReceiptId          = GR_DATA.GroupReceiptXReceiptId;
                        
                        
                    }                      
                    #endregion


                    receipt.PrintingInvoices.Add(iv);
                }
            }
            else
            {
                receipt.PrintingInvoices.Add(iv);
               
                receipt.ReceiptId = receiptId;
                receipt.PrintingSequence = DaHelper.GetShort(dr, "PrintingSequence").Value;
                receipt.TotalReceipt = DaHelper.GetShort(dr, "TotalReceipt").Value;
                receipt.CustomerId = DaHelper.GetString(dr, "CaId");
                receipt.CustomerName = DaHelper.GetString(dr, "RcName");
                receipt.CustomerAddress = DaHelper.GetString(dr, "RcAddress");

                //Tax13
                receipt.CaTaxId = DaHelper.GetString(dr, "RcCaTaxId");
                receipt.CaTaxBranch = DaHelper.GetString(dr, "RcCaTaxBranch");

                receipt.BranchName = DaHelper.GetString(dr, "BranchName");
                receipt.BranchAddress = DaHelper.GetString(dr, "BranchAddress");
                receipt.BranchNumber = DaHelper.GetString(dr, "BranchNumber");
                receipt.TerminalCode = DaHelper.GetString(dr, "TerminalCode");

                receipt.ContractType = DaHelper.GetString(dr, "CtId");
                receipt.TotalAmount = DaHelper.GetDecimal(dr, "TotalAmount").Value;
                receipt.ChangeAmount = DaHelper.GetDecimal(dr, "TotalChangeAmount").Value;
                receipt.AdjChangeAmount = DaHelper.GetDecimal(dr, "AdjAmount").Value;
                receipt.PaidAmount = DaHelper.GetDecimal(dr, "PaidAmount").Value;

                receipt.PaymentDate = DaHelper.GetDate(dr, "PaymentDt").Value;
                receipt.CashierId = DaHelper.GetString(dr, "CashierId");
                receipt.CashierName = DaHelper.GetString(dr, "CashierName");

                receipt.EnergyAmount = DaHelper.GetDecimal(dr, "EnergyAmount").Value;
                iv.EnergyAmount = receipt.EnergyAmount;

                iv.PaymentId = DaHelper.GetString(dr, "PaymentId");
                iv.CaId = receipt.CustomerId;
                iv.BranchId = DaHelper.GetString(dr, "TechBranchId");
                iv.GroupInvoiceReceiptType = DaHelper.GetString(dr, "GroupIvReceiptType");
                Random iv_random = new Random();
                iv.UiRefId = iv_random.Next();

                

                iv.Bills = new List<Bill>();
                dt = ds.Tables[2];
                decimal? totalAmountExVat = 0;
                decimal? totalVatAmount = 0;
                decimal? totalGAmount = 0;
                foreach (DataRow drx in dt.Rows)
                {
                    Bill b = new Bill();
                    b.CustomerId = iv.CaId;
                    b.InvoiceNo = DaHelper.GetString(drx, "InvoiceNo");
                    b.GroupInvoiceId = DaHelper.GetString(drx, "GroupInvoiceNo");
                    b.DebtId = DaHelper.GetString(drx, "DtId");
                    b.DebtType = DaHelper.GetString(drx, "DtName");
                    b.Description = DaHelper.GetString(drx, "Description");

                    b.Period = DaHelper.GetString(drx, "Period");
                    b.PreviousUnit = DaHelper.GetDecimal(drx, "ReadUnit");
                    b.LastUnit = DaHelper.GetDecimal(drx, "LastUnit");
                    b.MeterReadDate = DaHelper.GetDate(drx, "MeterReadDt");
                    b.UnitPrice = DaHelper.GetDecimal(drx, "UnitPrice");
                    b.Qty = DaHelper.GetDecimal(drx, "Qty");
                    b.BaseAmount = DaHelper.GetDecimal(drx, "BaseAmount");
                    b.FtUnitPrice = DaHelper.GetDecimal(drx, "FTUnitPrice");
                    b.FtAmount = DaHelper.GetDecimal(drx, "FTAmount");
                    b.AmountExVat = DaHelper.GetDecimal(drx, "Amount");
                    b.TaxCode = DaHelper.GetString(drx, "TaxCode");
                    b.TaxRate = DaHelper.GetDecimal(drx, "TaxRate");
                    b.VatAmount = DaHelper.GetDecimal(drx, "VatAmount");
                    b.GAmount = DaHelper.GetDecimal(drx, "GAmount");
                    b.ToPayVatAmount = DaHelper.GetDecimal(drx, "ToPayVatAmount");
                    b.ToPayGAmount = DaHelper.GetDecimal(drx, "ToPayGAmount");
                    b.MeterId = DaHelper.GetString(dr, "MeterId");
                    b.RateTypeId = DaHelper.GetString(dr, "RateTypeId");


                    b.BookCreateDt = DaHelper.GetDate(drx, "BookCreateDt");
                    b.BookTotalBill = DaHelper.GetInt(drx, "BookTotalBill");
                    b.BookTotalGAmount = DaHelper.GetDecimal(drx, "BookTotalGAmount");
                    b.BookAdvanceAmount = DaHelper.GetDecimal(drx, "BookAdvanceAmount");
                    b.BookTotalBillCollected = DaHelper.GetInt(drx, "BookTotalBillCollected");
                    b.BillBookId = DaHelper.GetString(drx, "BillBookId");
                    b.ControllerId = DaHelper.GetString(drx, "BillControllerId");
                    b.IsServiceEndOfTheYear = DaHelper.GetString(drx, "IsServiceEndOfTheYear");
                    b.IsExpenseDuringTheYear = DaHelper.GetString(drx, "IsExpenseDuringTheYear");


                    iv.CaDoc = DaHelper.GetString(drx, "CaDoc");
                    iv.OriginalInvoiceNo = DaHelper.GetString(drx, "OriginalInvoiceNo");
                    iv.SpotBillInvoiceNo = DaHelper.GetString(drx, "SpotBillInvoiceNo");
                    iv.InstallmentPeriod = DaHelper.GetInt(drx, "InstallmentPeriod");
                    iv.InstallmentTotalPeriod = DaHelper.GetInt(drx, "InstallmentTotalPeriod");
                    iv.Qty = DaHelper.GetDecimal(drx, "Qty");

                    iv.ToPayQty = DaHelper.GetDecimal(drx, "ToPayQty");
                    iv.PaidGAmount = DaHelper.GetDecimal(drx, "ToBePaidGAmount");
                    iv.ToPayGAmount = DaHelper.GetDecimal(drx, "ToPayGAmount");
                    iv.ToPayVatAmount = DaHelper.GetDecimal(drx, "ToPayVatAmount");
                    iv.InvoiceNo = b.InvoiceNo;
                    iv.InvoiceDate = DaHelper.GetDate(drx, "InvoiceDt");

                    totalAmountExVat += b.AmountExVat;
                    totalVatAmount += b.VatAmount;
                    totalGAmount += b.GAmount;

                    iv.PartialPayment = DaHelper.GetByte(drx, "PartialPayment");

                    ////Tax13
                    //iv.CaTaxId = receipt.CaTaxId;
                    //iv.CaTaxBranch = receipt.CaTaxBranch;
                    b.EnergyAmount = iv.EnergyAmount;

                    iv.Bills.Add(b);
                }

                iv.AmountExVat = totalAmountExVat;
                iv.VatAmount = totalVatAmount;
                iv.GAmount = totalGAmount;

                #region DCR รวมใบเสร็จแผนผ่อน 2021AUG พิมพ์ซ่อมใบเสร็จรับเงิน (PaidBillDA)
                ReceiptGroupMainData GR_DATA    = GetGroupReceiptMainData(trans, receipt.ReceiptId);
                string PrintingSeqText          = GetReceiptPrintingSeqTextByReceiptId(trans, receipt.ReceiptId);
                //string PaymentMethodsWithPipe   = GetGroupReceiptPaymentMethods(trans, receipt.ReceiptId);
                //string PaymentMethodsPosSlip    = GetGroupReceiptPaymentMethodsPOSSlip(trans, receipt.ReceiptId);

                List<string> PaymentMethodsText = new List<string>();
                PaymentMethodsText              = GetGroupReceiptPaymentMethods(trans, receiptId);
                if (PaymentMethodsText != null && PaymentMethodsText.Count > 0)
                {
                    if (PaymentMethodsText[0].ToString() != null && PaymentMethodsText[0].ToString().Length > 1)
                    {
                        receipt.GroupReceiptPaymentMethodsWithPipe = PaymentMethodsText[0].ToString();
                    }

                    if (PaymentMethodsText[1].ToString() != null && PaymentMethodsText[1].ToString().Length > 1)
                    {
                        receipt.GroupReceiptPaymentMethodsWithPipePOSSlip = PaymentMethodsText[1].ToString();
                    }
                } 

                //if (PaymentMethodsPosSlip != null)
                //{
                //    receipt.GroupReceiptPaymentMethodsWithPipePOSSlip = PaymentMethodsPosSlip;
                //}

                //if (PaymentMethodsWithPipe != null)
                //{
                //    receipt.GroupReceiptPaymentMethodsWithPipe = PaymentMethodsWithPipe.Trim().ToString();
                //}

                if (PrintingSeqText != null)
                {
                    receipt.GroupReceiptPrintingSeqTextWithPipe = PrintingSeqText.Trim().ToString();
                }

                if (GR_DATA.GroupReceiptId == receipt.ReceiptId)
                {
                    //string paymentMethodText        = GetGroupReceiptPaymentMethods(trans, receipt.ReceiptId);
                    iv.GroupReceiptId               = GR_DATA.GroupReceiptId;
                    iv.GroupReceiptPeriodText       = GR_DATA.GroupReceiptPeriodText;
                    iv.GroupReceiptQty              = GR_DATA.GroupReceiptQty;
                    iv.GroupReceiptAmount           = GR_DATA.GroupReceiptAmount;
                    iv.GroupReceiptVatTotal         = GR_DATA.GroupReceiptVatTotal;
                    iv.GroupReceiptTotal            = GR_DATA.GroupReceiptTotal;
                    iv.GroupReceiptInstallmentText  = GR_DATA.GroupReceiptInstallmentText;
                    iv.GroupReceiptRateTypeText     = GR_DATA.GroupReceiptRateTypeText;
                    iv.GroupReceiptMeterIdText      = GR_DATA.GroupReceiptMeterIdText;
                    iv.GroupXReceiptId              = GR_DATA.GroupReceiptXReceiptId;

                    receipt.GroupReceiptAmount          = GR_DATA.GroupReceiptAmount;
                    receipt.GroupReceiptInstallmentText = GR_DATA.GroupReceiptInstallmentText;
                    receipt.GroupReceiptOrNot           = "Y";
                    receipt.GroupReceiptPeriodText      = GR_DATA.GroupReceiptPeriodText;
                    receipt.GroupReceiptQty             = GR_DATA.GroupReceiptQty; ;
                    receipt.GroupReceiptTotal           = GR_DATA.GroupReceiptTotal; ;
                    receipt.GroupReceiptVatTotal        = GR_DATA.GroupReceiptVatTotal;
                    receipt.GroupReceiptRateTypeText    = GR_DATA.GroupReceiptRateTypeText;
                    receipt.GroupReceiptMeterIdText     = GR_DATA.GroupReceiptMeterIdText;
                    receipt.GroupXReceiptId             = GR_DATA.GroupReceiptXReceiptId;
                    //receipt.GroupReceiptPaymentMethodsWithPipe = paymentMethodText;                   
                    
                }
                #endregion

                
            }



            dt = ds.Tables[3];
            foreach (DataRow drx in dt.Rows)
            {
                string arptId = DaHelper.GetString(drx, "CancelARPtId");
                if (arptId == null)
                {
                    arptId = DaHelper.GetString(drx, "ARPtId");
                }                

                PaymentMethod pm = pms.Find(delegate(PaymentMethod pmx)
                    {
                        return pmx.ARPtId == arptId;
                    }
                );

                InvoicePaymentMethod.Create(iv, pm, DaHelper.GetDecimal(drx, "Amount").Value);
            }

            dt = ds.Tables[4];
            bool haveCancelReceipt = false;
            List<ReceiptStatus> rss = new List<ReceiptStatus>();
            foreach (DataRow drx in dt.Rows)
            {
                ReceiptStatus rs = new ReceiptStatus();
                rs.Id = DaHelper.GetString(drx, "ReceiptId");
                rs.IsCancelled = DaHelper.GetDate(drx, "CancelDt") != null;
                rs.PrintingSequence = DaHelper.GetShort(drx, "PrintingSequence").Value;
                rs.TotalReceipt = DaHelper.GetShort(drx, "TotalReceipt").Value;

                rss.Add(rs);
                haveCancelReceipt = haveCancelReceipt || rs.IsCancelled;
            }

            //**For Receipt used to cancel
            if (haveCancelReceipt)
            {
                receipt.PaidAmount = receipt.TotalAmount + receipt.AdjChangeAmount;
                receipt.ChangeAmount = 0;
            }


            #region DCR GroupInvoiceText (PaidBillDA) 2021-OCT-25 Uthen.P
            foreach (var r in receipt.PrintingInvoices)
            {
                string DisplayCaPayer           = null;
                string DisplayGroupInvoiceNo    = null;
                DisplayCaPayer                  = r.DisplayCaId;
                DisplayGroupInvoiceNo           = r.DisplayInvoiceNo;
                if (DisplayCaPayer.StartsWith("4") == true && DisplayGroupInvoiceNo != null)
                {
                    string tmpResult = GetGroupInvoiceDescriptionText(trans, DisplayGroupInvoiceNo);
                    if(tmpResult != null)
                    {
                        receipt.GroupInvoiceDescriptionText = tmpResult;
                    }
                    
                }
            }
            #endregion


            PrintingInfo pinfo = new PrintingInfo();
            pinfo.PrintingReceipt = receipt;
            pinfo.PaymentMethods = pms;
            pinfo.ReceiptStatus = rss;

            //CreateReceipt(pinfo);
            
            return pinfo;
        }

        public string GetGroupInvoiceDescriptionText(DbTransaction trans, string groupInvoicNo)
        {
            Database db         = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd       = db.GetStoredProcCommand("pc_get_GroupInvoiceText");
            cmd.CommandTimeout  = timeout;
            db.AddInParameter(cmd, "GroupInvoiceNo", DbType.String, groupInvoicNo);    
            DataTable dt = new DataTable();
            if (trans != null)
            {
                dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            }
            else
            {
                dt = db.ExecuteDataSet(cmd).Tables[0];
            }

            string GroupInvoiceDescText = null;
            foreach (DataRow dr in dt.Rows)
            {
                GroupInvoiceDescText = DaHelper.GetString(dr, "GroupText");                
            }

            return GroupInvoiceDescText;

        }


        public void IncreaseNoOfReprint(DbTransaction trans, string receiptId, string postedServerId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_upd_IncreaseNoOfReprint");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "ReceiptId", DbType.String, receiptId);
            db.AddInParameter(cmd, "PostedServerId", DbType.String, postedServerId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, Session.User.Id);
            db.ExecuteNonQuery(cmd, trans);
        }

        public List<OneTouchLogInfo> SearchOneTouchPayment(List<string> receiptIds)
        {
            List<OneTouchLogInfo> OneTouchInfos = new List<OneTouchLogInfo>();
            
            SqlDatabase db = (SqlDatabase)DatabaseFactory.CreateDatabase("POSDatabase");
            //Database db = DatabaseFactory.CreateDatabase("POSDatabase");            
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_OneTouchPayment");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "ReceiptIds", SqlDbType.Structured, GetRecieptList(receiptIds));
        
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                OneTouchLogInfo OneTouchInfo = new OneTouchLogInfo();
                OneTouchInfo.NotificationNo = DaHelper.GetString(dr, "NotificationNo");
                OneTouchInfo.InvoiceNo = DaHelper.GetString(dr, "InvoiceNo");
                OneTouchInfo.ReceiptId = DaHelper.GetString(dr, "ReceiptId");
                OneTouchInfo.DebtId = DaHelper.GetString(dr, "DtId");
                OneTouchInfo.GAmount = DaHelper.GetDecimal(dr, "GAmount");
                OneTouchInfo.VatAmount = DaHelper.GetDecimal(dr, "VatAmount");


                OneTouchInfos.Add(OneTouchInfo);
            }

            return OneTouchInfos;
        }

        public List<string> SearchPaymentTypeQR(List<string> paymentIds) {
            List<string> result = new List<string>();

            SqlDatabase db = (SqlDatabase)DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_QRPaymentType");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "@PaymentIds", SqlDbType.Structured, GetPaymentIdList(paymentIds));
            
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                string qrRefNo;
                qrRefNo = DaHelper.GetString(dr, "QRRefNo");
                result.Add(qrRefNo);
            }
            return result; 
        }


        
    }


    public class ReceiptCollection : List<string>, IEnumerable<SqlDataRecord>
    {
        IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
        {
            SqlDataRecord ret = new SqlDataRecord(
                new SqlMetaData("ReceiptId", SqlDbType.Char, 16)
                );

            foreach (string receiptId in this)
            {
                ret.SetString(0, receiptId);
                yield return ret;
            }
        }
    }

    public class PaymentTypeCollection : List<string>, IEnumerable<SqlDataRecord>
    {
        IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
        {
            SqlDataRecord ret = new SqlDataRecord(
                new SqlMetaData("PaymentId", SqlDbType.Char, 22)
                );

            foreach (string paymentId in this)
            {
                ret.SetString(0, paymentId);
                yield return ret;
            }
        }
    }


}
