using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using System.Collections;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;
using System.ComponentModel;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;

namespace PEA.BPM.PaymentCollectionModule.DA
{
    public class BillingDA
    {
        #region Normal Function
        private int timeout = 300; //5*60      


        //DCR รวมใบเสร็จแผนผ่อน 2021AUG (BillingDA)
        public List<ReceiptGroupItems> GetGroupReceiptItems(DbTransaction trans,string ListReceiptId, string ListInvoiceNo, DateTime PaymentDt)
        {
            Database    db      = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand   cmd     = db.GetStoredProcCommand("pc_get_ConfirmGroupReceiptByListInvoiceNo");
            cmd.CommandTimeout  = timeout;
            db.AddInParameter(cmd, "ListReceiptId", DbType.String, ListReceiptId);
            db.AddInParameter(cmd, "ListInvoiceNo", DbType.String, ListInvoiceNo);
            db.AddInParameter(cmd, "PaymentDt", DbType.DateTime, PaymentDt);
            DataTable       dt  = db.ExecuteDataSet(cmd, trans).Tables[0];
            var Result          = new List<ReceiptGroupItems>();            
            foreach (DataRow r in dt.Rows)
            {
                var m                = new ReceiptGroupItems();
                m.MainGroupReceiptId = DaHelper.GetString(r, "MainGroupReceiptId");
                m.ReceiptId          = DaHelper.GetString(r, "ReceiptId");
                m.InvoiceNo          = DaHelper.GetString(r, "InvoiceNo");
                Result.Add(m);
            }

            return Result;            
        }

        // DCR รวมใบเสร็จแผนผ่อน 2021OCT (BillingDA)
        public List<string> GetGroupReceiptPaymentMethods(DbTransaction trans, string receiptId)
        {
            Database db         = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd       = db.GetStoredProcCommand("pc_get_GroupReceiptPaymentMethods");
            cmd.CommandTimeout  = timeout;
            db.AddInParameter(cmd, "ReceiptId", DbType.String, receiptId);

            List<string> PaymentMethodsText = new List<string>();

            #region Pre-Printed Payment Methods
            DataTable dt        = new DataTable();
            if (trans != null)
            {
                dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            }
            else
            {
                dt = db.ExecuteDataSet(cmd).Tables[0];
            }

            StringBuilder sbd = new StringBuilder();
            int AllRows       = dt.Rows.Count;
            int currentRows   = 0;
            foreach (DataRow r in dt.Rows)
            {
                currentRows   += 1;
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
            Database db         = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd       = db.GetStoredProcCommand("pc_get_GroupReceiptPaymentMethods");
            cmd.CommandTimeout  = timeout;
            db.AddInParameter(cmd, "ReceiptId", DbType.String, receiptId);
            DataTable dt = new DataTable();
            if (trans != null)
            {
                dt = db.ExecuteDataSet(cmd, trans).Tables[1];
            }
            else
            {
                dt = db.ExecuteDataSet(cmd).Tables[1];
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


        public string GetGroupInvoiceDescriptionText(DbTransaction trans, string groupInvoicNo)
        {
            Database db         = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd       = db.GetStoredProcCommand("pc_get_GroupInvoiceText");
            cmd.CommandTimeout  = timeout;
            db.AddInParameter(cmd, "GroupInvoiceNo", DbType.String, groupInvoicNo);
            DataTable dt        = new DataTable();
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

        public string GetReceiptPrintingSeqTextByReceiptId(DbTransaction trans, string ReceiptId)
        {
            Database db     = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd   = db.GetStoredProcCommand("pc_sel_GroupReceiptPrintingSeq");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "ReceiptId", DbType.String, ReceiptId);
            DataTable dt    = new DataTable();
            if (trans != null)
            {
                dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            }
            else
            {
                dt = db.ExecuteDataSet(cmd).Tables[0];
            }
            
            string PrintingSeqText  = null;

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

        public ReceiptGroupMainData GetGroupReceiptMainData(DbTransaction trans, string ReceiptId)
        {
            Database db         = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd       = db.GetStoredProcCommand("pc_get_GroupReceiptMainData");
            cmd.CommandTimeout  = timeout;
            db.AddInParameter(cmd, "ReceiptId", DbType.String, ReceiptId);            
            DataTable dt        = db.ExecuteDataSet(cmd, trans).Tables[0];
            var result          = new ReceiptGroupMainData();
            if (dt.Rows.Count == 0)
            {
                return result;
            }
            foreach (DataRow r in dt.Rows)
            {
                result.GroupReceiptId               = DaHelper.GetString(r, "Receiptid");
                result.GroupReceiptPeriodText       = DaHelper.GetString(r, "GroupReceiptPeriodText");
                result.GroupReceiptQty              = DaHelper.GetDecimal(r,"GroupReceiptQty");                
                result.GroupReceiptInstallmentText  = DaHelper.GetString(r, "GroupReceiptInstallmentText");
                result.GroupReceiptInvoiceNo        = DaHelper.GetString(r, "InvoiceNo");
                result.GroupReceiptAmount           = DaHelper.GetDecimal(r, "GroupReceiptAmount");
                result.GroupReceiptVatTotal         = DaHelper.GetDecimal(r, "GroupReceiptVatTotal");
                result.GroupReceiptTotal            = DaHelper.GetDecimal(r, "GroupReceiptTotal");
                result.GroupReceiptMeterIdText      = DaHelper.GetString(r, "GroupReceiptMeterIdText");
                result.GroupReceiptRateTypeText     = DaHelper.GetString(r, "GroupReceiptRateTypeText");
                result.GroupReceiptXReceiptId       = DaHelper.GetString(r, "XReceiptList");
            }

            return result;
        }

        public int InsertGroupReceiptMain(string MainReceiptId, DateTime PaymentDate)
        {
            Database db         = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd       = db.GetStoredProcCommand("pc_ins_InsertGroupReceiptMain");
            cmd.CommandTimeout  = timeout;
            db.AddInParameter(cmd, "MainReceiptId", DbType.String, MainReceiptId);
            db.AddInParameter(cmd, "PaymentDt", DbType.DateTime, PaymentDate);
            int RowCount        = 0;
            RowCount            = db.ExecuteNonQuery(cmd);            
            return RowCount;            
        }

        public int InsertGroupReceiptMain(DbTransaction trans,string MainReceiptId,DateTime PaymentDate)
        {
            Database db         = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd       = db.GetStoredProcCommand("pc_ins_InsertGroupReceiptMain");
            cmd.CommandTimeout  = timeout;
            db.AddInParameter(cmd, "MainReceiptId", DbType.String, MainReceiptId);
            db.AddInParameter(cmd, "PaymentDt", DbType.DateTime, PaymentDate);            
            int RowCount        = 0;
            RowCount            = db.ExecuteNonQuery(cmd, trans);
            return RowCount;            
        }

        public int InsertGroupReceiptItems(string MainReceiptId, string ReceiptIdItem, DateTime PaymentDate)
        {
            Database db         = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd       = db.GetStoredProcCommand("pc_ins_InsertGroupReceiptItems");
            cmd.CommandTimeout  = timeout;
            db.AddInParameter(cmd, "MainGroupReceiptId", DbType.String, MainReceiptId);
            db.AddInParameter(cmd, "ReceiptIdItem", DbType.String, ReceiptIdItem);
            db.AddInParameter(cmd, "PaymentDt", DbType.DateTime, PaymentDate);
            int InsertedRows    = 0;
            InsertedRows        = db.ExecuteNonQuery(cmd);
            return InsertedRows;
        }

        public int InsertGroupReceiptItems(DbTransaction trans, string MainReceiptId, string ReceiptIdItem, DateTime PaymentDate)
        {
            Database db        = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd      = db.GetStoredProcCommand("pc_ins_InsertGroupReceiptItems");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "MainGroupReceiptId",    DbType.String, MainReceiptId);
            db.AddInParameter(cmd, "ReceiptIdItem",         DbType.String, ReceiptIdItem);
            db.AddInParameter(cmd, "PaymentDt",             DbType.DateTime, PaymentDate);
            int InsertedRows   = 0;
            InsertedRows       = db.ExecuteNonQuery(cmd, trans);
            return InsertedRows;
        }

        // END


        // DCR QRPayment 2023AUG (BillingDA)
        public string QRPaymentMethodByBranchId(string branchId)
        {
            string _result = string.Empty; 

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_get_QRPaymentStatus");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            DataTable dt = new DataTable();

            dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count == 0) 
            { 

            }
            foreach (DataRow r in dt.Rows)
            {
                _result = DaHelper.GetString(r, "StatusQRPayment");
            }

            return _result;
        }

        public bool CheckExistingInvoiceNo(string caId, string period)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_get_InvoiceByCaIdAndInvoiceNo");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "CaId", DbType.String, caId);
            db.AddInParameter(cmd, "Period", DbType.String, period);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Invoice> SearchInvoiceByCustomerId(string caId, bool isSearchByBP)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_ARInvoiceByCaId");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "CaId", DbType.String, caId);
            db.AddInParameter(cmd, "IsSearchByBP", DbType.Boolean, isSearchByBP);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            return ConvertToInvoiceList(dt, true);
        }

        public Invoice SearchOriginalInvoiceByInstallmentItemCaDoc(string caDoc)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_AROriginalInvoiceByInstallmentCaDoc");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "InstallmentCaDoc", DbType.String, caDoc);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            return ConvertToInvoiceList(dt, false)[0];
        }

        public List<InstallmentInvoice> SearchInstallmentInvoice(string caDoc)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_ARInstallmentInvoiceByCaDoc");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "CaDoc", DbType.String, caDoc);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            return ConvertToInstallmentInvoiceList(dt);
        }

        public List<Invoice> SearchInvoiceByGroupInvoiceNo(string groupInvoiceNo, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_ARInvoiceByGroupInvoiceNo");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "GroupInvoiceNo", DbType.String, groupInvoiceNo);
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            return ConvertToInvoiceList(dt, true);
        }

        public List<Invoice> SearchInvoiceItemByGroupInvoiceNo(string billBookId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_ARInvoiceItemByGroupInvoiceNo");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "BillBookId", DbType.String, billBookId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];


            List<Invoice> invoices = new List<Invoice>();
            foreach (DataRow dr in dt.Rows)
            {
                Invoice inv = new Invoice();
                inv.PaymentId = DaHelper.GetString(dr, "PaymentId");
                inv.CaDoc = DaHelper.GetString(dr, "CaDoc");
                inv.InvoiceNo = DaHelper.GetString(dr, "InvoiceNo");
                inv.InvoiceDate = DaHelper.GetDate(dr, "InvoiceDt");
                inv.BranchId = DaHelper.GetString(dr, "BranchId");
                inv.TechBranchName = DaHelper.GetString(dr, "TechBranchName");
                inv.CommBranchId = DaHelper.GetString(dr, "CommBranchId");
                inv.CommBranchName = DaHelper.GetString(dr, "CommBranchName");
                inv.CaId = DaHelper.GetString(dr, "CaId");
                inv.Name = DaHelper.GetString(dr, "CaName");
                inv.Address = DaHelper.GetString(dr, "CaAddress");

                //Tax13
                inv.CaTaxId = DaHelper.GetString(dr, "CaTaxId");
                inv.CaTaxBranch = DaHelper.GetString(dr, "CaTaxBranch");

                inv.Qty = DaHelper.GetDecimal(dr, "Qty");
                inv.VatAmount = DaHelper.GetDecimal(dr, "VatAmount");
                inv.AmountExVat = DaHelper.GetDecimal(dr, "ExVatAmount");
                inv.GAmount = DaHelper.GetDecimal(dr, "GAmount");
                inv.ToPayQty = DaHelper.GetDecimal(dr, "ToPayQty");
                inv.ToPayGAmount = DaHelper.GetDecimal(dr, "ToPayGAmount");
                inv.ToPayVatAmount = DaHelper.GetDecimal(dr, "ToPayVatAmount");
                inv.PaidQty = DaHelper.GetDecimal(dr, "PaidQty");
                inv.PaidVatAmount = DaHelper.GetDecimal(dr, "PaidVatAmount");
                inv.PaidGAmount = DaHelper.GetDecimal(dr, "PaidGAmount");
                inv.ARPmId = DaHelper.GetString(dr, "ARPmId");
                inv.ControllerId = DaHelper.GetString(dr, "ControllerId");
                inv.ControllerName = DaHelper.GetString(dr, "ControllerName");
                inv.MruId = DaHelper.GetString(dr, "MruId");

                List<Bill> bills = new List<Bill>();
                Bill bill = new Bill();
                bill.ARPmId = inv.ARPmId;
                bill.InvoiceNo = inv.InvoiceNo;
                bill.CustomerId = inv.CaId;
                bill.BranchId = inv.BranchId;
                bill.Name = inv.Name;
                bill.Address = inv.Address;
                bill.ContractTypeId = DaHelper.GetString(dr, "CtId");
                bill.DebtId = DaHelper.GetString(dr, "DtId");
                bill.Period = DaHelper.GetString(dr, "Period");
                bill.MeterId = DaHelper.GetString(dr, "MeterId");
                bill.RateTypeId = DaHelper.GetString(dr, "RateTypeId");
                bill.MeterReadDate = DaHelper.GetDate(dr, "MeterReadDt");
                bill.PreviousUnit = DaHelper.GetDecimal(dr, "ReadUnit");
                bill.LastUnit = DaHelper.GetDecimal(dr, "LastUnit");
                bill.FullBaseAmount = DaHelper.GetDecimal(dr, "FullBaseAmount");
                bill.FullFtAmount = DaHelper.GetDecimal(dr, "FullFtAmount");
                bill.FullQty = DaHelper.GetDecimal(dr, "FullQty");
                bill.FullAmount = DaHelper.GetDecimal(dr, "FullAmount");
                bill.FullVatAmount = DaHelper.GetDecimal(dr, "FullVatAmount");
                bill.FullGAmount = DaHelper.GetDecimal(dr, "FullGAmount");
                bill.BaseAmount = DaHelper.GetDecimal(dr, "BaseAmount");
                bill.FtUnitPrice = DaHelper.GetDecimal(dr, "FtUnitPrice");
                bill.FtAmount = DaHelper.GetDecimal(dr, "FtAmount");
                bill.AmountExVat = DaHelper.GetDecimal(dr, "Amount");
                bill.Qty = DaHelper.GetDecimal(dr, "BillQty");
                bill.GAmount = DaHelper.GetDecimal(dr, "BillGAmount");
                bill.TaxCode = DaHelper.GetString(dr, "TaxCode");
                bill.TaxRate = DaHelper.GetDecimal(dr, "TaxRate");
                bill.VatAmount = DaHelper.GetDecimal(dr, "BillVatAmount");
                bill.ControllerId = DaHelper.GetString(dr, "ControllerId");
                bills.Add(bill);

                inv.Bills = bills;

                invoices.Add(inv);
            }

            return invoices;
        }

        public List<Bill> SearchBillByInvoiceNo(string invoiceNo, Boolean chkIsPaid)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_ARByInvoiceNo");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "InvoiceNo", DbType.String, invoiceNo);
            db.AddInParameter(cmd, "ChkIsPaid", DbType.Boolean, chkIsPaid);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            return ConvertToBillList(dt);
        }

        public List<BillSearchDetail> SearchBillByCustomerDetail(CustomerSearchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_ARSummaryByCaDetail");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "CaName", DbType.String, param.Name);
            db.AddInParameter(cmd, "CaAddress", DbType.String, param.Address);
            db.AddInParameter(cmd, "CaRegisterId", DbType.String, param.RegId);
            db.AddInParameter(cmd, "BpCaId", DbType.String, param.BpCaId);
            db.AddInParameter(cmd, "Branch", DbType.String, param.Branch);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            return ConvertToBillSearchDetailList(dt);
        }


        public List<Bill> SearchBillByBillBookId(string billBookId, string billBookStatus)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_ARByBillBookId");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "BillBookId", DbType.String, billBookId);
            db.AddInParameter(cmd, "BsId", DbType.String, billBookStatus);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            return ConvertToBillList(dt);
        }

        public List<BookSearchDetail> SearchBillByAgent(AgencySearchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_ARSummaryByAgent");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "AgencyId", DbType.String, param.AgencyId);
            db.AddInParameter(cmd, "AgencyName", DbType.String, param.AgencyName);
            db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            return ConvertToBookSearchDetailList(dt);
        }

        public List<PaymentMethod> SearchAGPayment(string billBookId)
        {
            List<PaymentMethod> paymentMethods = new List<PaymentMethod>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_AGPayment");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "BillBookId", DbType.String, billBookId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                PaymentMethod p = new PaymentMethod();
                p.ARPtId = DaHelper.GetString(dr, "ARPtId");
                p.ToPayAmount = DaHelper.GetDecimal(dr, "Amount");
                p.ChangeAmount = 0;
                p.Bank = new Bank(DaHelper.GetString(dr, "BankKey"), DaHelper.GetString(dr, "BankName"));
                p.PtId = DaHelper.GetString(dr, "PtId");
                p.ChqNo = DaHelper.GetString(dr, "ChqNo");
                p.ChqAccNo = DaHelper.GetString(dr, "ChqAccNo");
                p.ChqDt = DaHelper.GetDate(dr, "ChqDt");

                paymentMethods.Add(p);
            }

            return paymentMethods;
        }

        public List<string> SearchARPmIdByBillBookId(string billBookId, DateTime paymentDate)
        {
            List<string> invoiceList = new List<string>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_ARPmIdByBillBookId");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "BillBookId", DbType.String, billBookId);
            db.AddInParameter(cmd, "PaymentDt", DbType.DateTime, paymentDate);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                string invoice = "";
                invoice = DaHelper.GetString(dr, "ARPmId") + ":" + DaHelper.GetString(dr, "InvoiceNo");

                invoiceList.Add(invoice);
            }

            return invoiceList;
        }

        public List<GroupInvoiceItem> GetGroupInvoiceItem(string billBookId)
        {
            List<GroupInvoiceItem> giis = new List<GroupInvoiceItem>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_GroupInvoiceItem");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "BillBookId", DbType.String, billBookId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                GroupInvoiceItem gii = new GroupInvoiceItem();
                gii.CaId = DaHelper.GetString(dr, "CaId");
                gii.Name = DaHelper.GetString(dr, "CaName");
                gii.Address = DaHelper.GetString(dr, "CaAddress");
                gii.BranchId = DaHelper.GetString(dr, "TechBranchId");
                gii.Period = DaHelper.GetString(dr, "Period");
                gii.ToBePaidExVatAmount = DaHelper.GetDecimal(dr, "ToBePaidExVatAmount");

                giis.Add(gii);
            }

            return giis;
        }

        public CaIdAndBranchId GetBranchIdByCaId(string caId)
        {
            CaIdAndBranchId caIdAndBranchId = null;

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_get_BranchByCaId");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "CaId", DbType.String, caId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                caIdAndBranchId = new CaIdAndBranchId();
                caIdAndBranchId.CaId = DaHelper.GetString(dr, "CaId");
                caIdAndBranchId.TechBranchId = DaHelper.GetString(dr, "TechBranchId");
                caIdAndBranchId.CommBranchId = DaHelper.GetString(dr, "CommBranchId");
            }

            return caIdAndBranchId;
        }

        public Customer GetCustomerDetail(string CustomerId)
        {
            Customer customerDetail = null;

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_get_CaDetail");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "CaId", DbType.String, CustomerId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                customerDetail = new Customer();
                customerDetail.CustomerId = DaHelper.GetString(dr, "CaId");
                customerDetail.Name = DaHelper.GetString(dr, "CaName");
                customerDetail.CaTaxId = DaHelper.GetString(dr, "CaTaxId");
                customerDetail.CaTaxBranch = DaHelper.GetString(dr, "CaTaxBranch");
                customerDetail.Address = DaHelper.GetString(dr, "CaAddress");
                customerDetail.BranchId = DaHelper.GetString(dr, "BranchId");
                customerDetail.TechBranchName = DaHelper.GetString(dr, "TechBranchName");
                customerDetail.CommBranchId = DaHelper.GetString(dr, "CommBranchId");
                customerDetail.CommBranchName = DaHelper.GetString(dr, "CommBranchName");
                customerDetail.CtId = DaHelper.GetString(dr, "CtId");
                customerDetail.AccountClassId = DaHelper.GetString(dr, "AccountClassId");
                customerDetail.SecurityDeposit = DaHelper.GetDecimal(dr, "SecurityDeposit");
                customerDetail.MeterSizeId = DaHelper.GetString(dr, "MeterSizeId");
                customerDetail.MeterSizeName = DaHelper.GetString(dr, "MeterSizeName");
                customerDetail.ContractTypeId = DaHelper.GetString(dr, "CtId");
                customerDetail.ControllerId = DaHelper.GetString(dr, "ControllerId");
                customerDetail.ControllerName = DaHelper.GetString(dr, "ControllerName");
                customerDetail.MruId = DaHelper.GetString(dr, "MruId");
            }

            return customerDetail;
        }


        public List<PaidInvoice> GetPaymentHistory(string CustomerId)
        {
            List<PaidInvoice> paidInvoices = new List<PaidInvoice>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_ARPaymentHistory");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "CaId", DbType.String, CustomerId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                string realInvoiceNo = DaHelper.GetString(dr, "RealInvoiceNo");
                string invoiceNo = DaHelper.GetString(dr, "InvoiceNo");
                string receiptId = DaHelper.GetString(dr, "ReceiptId");
                string realReceiptId = DaHelper.GetString(dr, "RealReceiptId");

                PaidInvoice p = paidInvoices.Find(delegate(PaidInvoice pi)
                    {
                        return pi.RealInvoiceNo == realInvoiceNo
                            && pi.InvoiceNo == invoiceNo
                            && pi.ReceiptId == receiptId
                            && pi.RealReceiptId == realReceiptId;
                    }
                );

                if (p == null)
                {
                    p = new PaidInvoice();
                    p.RealInvoiceNo = realInvoiceNo;
                    p.InvoiceNo = invoiceNo;
                    p.DebtTypes.Add(
                        new PaidInvoice.Debt(
                            DaHelper.GetString(dr, "DtId"),
                            DaHelper.GetString(dr, "DtName"),
                            DaHelper.GetDecimal(dr, "GAmount")
                            )
                        );

                    p.ReceiptId = receiptId;
                    p.RealReceiptId = realReceiptId;
                    p.Period = DaHelper.GetString(dr, "Period");
                    p.InstallmentPeriod = DaHelper.GetInt(dr, "InstallmentPeriod");
                    p.InstallmentTotalPeriod = DaHelper.GetInt(dr, "InstallmentTotalPeriod");

                    p.RateTypeId = DaHelper.GetString(dr, "RateTypeId");
                    p.CancelDate = DaHelper.GetDate(dr, "CancelDt");
                    p.PaidGAmount = DaHelper.GetDecimal(dr, "PaidGAmount");
                    p.PaidDate = DaHelper.GetDate(dr, "PaymentDt");
                    p.PosId = DaHelper.GetString(dr, "PosId");
                    p.CashierId = DaHelper.GetString(dr, "CashierId");
                    p.PmName = DaHelper.GetString(dr, "PmName");
                    paidInvoices.Add(p);
                }
                else
                {
                    p.DebtTypes.Add(
                         new PaidInvoice.Debt(
                            DaHelper.GetString(dr, "DtId"),
                             DaHelper.GetString(dr, "DtName"),
                             DaHelper.GetDecimal(dr, "GAmount")
                             )
                         );
                }
            }

            return paidInvoices;
        }

        public List<BillBook> SearchBillBookByDetail(string billBookId, string agencyId, string agencyName)
        {
            List<BillBook> billbooks = new List<BillBook>();

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_BillBookByDetail");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "BillBookId", DbType.String, billBookId);
            db.AddInParameter(cmd, "AgencyId", DbType.String, agencyId);
            db.AddInParameter(cmd, "AgencyName", DbType.String, agencyName);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                BillBook b = new BillBook();
                b.BillBookId = DaHelper.GetString(dr, "BillBookId");
                b.AgentId = DaHelper.GetString(dr, "CaId");
                b.AgentName = DaHelper.GetString(dr, "CaName");
                b.Period = DaHelper.GetString(dr, "BookPeriod");
                b.BookTotalGAmount = DaHelper.GetDecimal(dr, "BookTotalGAmount");
                b.AdvancePayment = DaHelper.GetDecimal(dr, "AdvPayAmount");

                billbooks.Add(b);
            }

            return billbooks;
        }

        public BillBook GetBillBookDetail(string billBookId, string statusLineStr)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_get_BillBookDetail");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "BillBookId", DbType.String, billBookId);
            db.AddInParameter(cmd, "StatusLineStr", DbType.String, statusLineStr);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            if (dt.Rows.Count == 1)
            {
                DataRow dr = dt.Rows[0];

                BillBook b = new BillBook();
                b.BillBookId = DaHelper.GetString(dr, "BillBookId");
                b.AgentId = DaHelper.GetString(dr, "CaId");
                b.ContractTypeId = DaHelper.GetString(dr, "CtId");
                b.AgentName = DaHelper.GetString(dr, "CaName");
                b.AgentAddress = DaHelper.GetString(dr, "CaAddress");
                b.AgentBranchId = DaHelper.GetString(dr, "BranchId");
                b.TechBranchName = DaHelper.GetString(dr, "TechBranchName");
                b.CommBranchId = DaHelper.GetString(dr, "CommBranchId");
                b.CommBranchName = DaHelper.GetString(dr, "CommBranchName");
                b.Period = DaHelper.GetString(dr, "BookPeriod");
                b.ControllerId = DaHelper.GetString(dr, "ControllerId");
                b.AdvancePayment = DaHelper.GetDecimal(dr, "AdvPayAmount");
                b.DueDate = DaHelper.GetDate(dr, "AdvPayDueDate");
                b.PaidGAmount = DaHelper.GetDecimal(dr, "PaidGAmount");
                b.PaidCountNumber = DaHelper.GetInt(dr, "PaidCountNumber");
                b.TotalBill = DaHelper.GetInt(dr, "TotalBill");
                b.BookCreateDt = DaHelper.GetDate(dr, "BookCreateDt");
                b.BookTotalGAmount = DaHelper.GetDecimal(dr, "BookTotalGAmount");
                b.BookTotalVatAmount = DaHelper.GetDecimal(dr, "BookTotalVatAmount");

                return b;
            }
            else
            {
                return null;
            }
        }

        public List<AdvancePaymentHistory> SearchAdvancePaymentHistory(string billBookId)
        {
            List<AdvancePaymentHistory> histories = new List<AdvancePaymentHistory>();

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_ARAdvancePaymentHistory");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "BillBookId", DbType.String, billBookId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                AdvancePaymentHistory h = new AdvancePaymentHistory();
                h.ItemId = DaHelper.GetString(dr, "ItemId");
                h.PaidGAmount = DaHelper.GetDecimal(dr, "PaidGAmount");
                h.PaymentDt = DaHelper.GetDate(dr, "PaymentDt");
                h.ReceiptId = DaHelper.GetString(dr, "ReceiptId");

                histories.Add(h);
            }

            return histories;
        }

        public string InsertIntoAR(DbTransaction trans, string branchId, string posId, Invoice invoice, string postedServerId, bool isOffline)
        {
            Bill bill = invoice.Bills[0];
            string invoiceNo = invoice.InvoiceNo;

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_ins_AR");
            cmd.CommandTimeout = timeout;

            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "PosId", DbType.String, posId);
            db.AddInParameter(cmd, "CaId", DbType.String, bill.CustomerId);
            db.AddInParameter(cmd, "DtId", DbType.String, bill.DebtId);
            db.AddInParameter(cmd, "Description", DbType.String, bill.Description);
            db.AddInParameter(cmd, "InvoiceNo", DbType.String, invoiceNo);
            db.AddInParameter(cmd, "GroupInvoiceNo", DbType.String, bill.GroupInvoiceId);
            db.AddInParameter(cmd, "BillBookId", DbType.String, bill.BillBookId);
            db.AddInParameter(cmd, "Period", DbType.String, bill.Period);
            db.AddInParameter(cmd, "MeterReadDt", DbType.DateTime, bill.MeterReadDate);
            db.AddInParameter(cmd, "ReadUnit", DbType.Decimal, bill.PreviousUnit);
            db.AddInParameter(cmd, "LastUnit", DbType.Decimal, bill.LastUnit);
            db.AddInParameter(cmd, "BaseAmount", DbType.Decimal, bill.BaseAmount);
            db.AddInParameter(cmd, "FTUnitPrice", DbType.Decimal, bill.FtUnitPrice);
            db.AddInParameter(cmd, "FTAmount", DbType.Decimal, bill.FtAmount);
            db.AddInParameter(cmd, "Amount", DbType.Decimal, bill.AmountExVat);
            db.AddInParameter(cmd, "UnitPrice", DbType.Decimal, bill.UnitPrice);
            db.AddInParameter(cmd, "Qty", DbType.Decimal, bill.Qty);
            db.AddInParameter(cmd, "UnitTypeId", DbType.String, bill.UnitTypeId);
            db.AddInParameter(cmd, "TaxCode", DbType.String, bill.TaxCode);
            db.AddInParameter(cmd, "VatAmount", DbType.Decimal, bill.VatAmount);
            db.AddInParameter(cmd, "GAmount", DbType.Decimal, bill.GAmount);
            db.AddInParameter(cmd, "DueDt", DbType.DateTime, bill.DueDate);
            db.AddInParameter(cmd, "DisconnectDt", DbType.DateTime, bill.DisConnectDate);
            db.AddInParameter(cmd, "DisconnectDoc", DbType.String, bill.DisconnectDocNo);
            db.AddInParameter(cmd, "OriginalInvoiceNo", DbType.String, invoice.OriginalInvoiceNo);
            db.AddInParameter(cmd, "InstallmentFlag", DbType.String, bill.InstallmentFlag);
            db.AddInParameter(cmd, "CancelFlag", DbType.String, bill.CancelFlag);
            db.AddInParameter(cmd, "ModifiedFlag", DbType.String, bill.ModifiedFlag);
            db.AddInParameter(cmd, "PostedServerId", DbType.String, postedServerId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, ((isOffline == true) ? "OFFLINE" : Session.User.Id));

            db.AddInParameter(cmd, "NotificationNo", DbType.String, bill.NotificationNo);   //OneTouch

            return db.ExecuteScalar(cmd, trans).ToString();
        }

        public string InsertIntoPayment(DbTransaction trans, DateTime PaymentDt, string PosId, string terminalCode,
             string branchId, string branchName, string postedServerId, string cashierId, string cashierName, string workId, bool isOffline)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_ins_Payment");
            cmd.CommandTimeout = timeout;

            db.AddInParameter(cmd, "PaymentDt", DbType.DateTime, PaymentDt);
            db.AddInParameter(cmd, "PosId", DbType.String, PosId);
            db.AddInParameter(cmd, "Terminalcode", DbType.String, terminalCode);
            db.AddInParameter(cmd, "CashierId", DbType.String, cashierId);
            db.AddInParameter(cmd, "CashierName", DbType.String, cashierName);
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "BranchName", DbType.String, branchName);
            db.AddInParameter(cmd, "WorkId", DbType.String, workId);
            db.AddInParameter(cmd, "PostedServerId", DbType.String, postedServerId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, ((isOffline == true) ? "OFFLINE" : Session.User.Id));

            return db.ExecuteScalar(cmd, trans).ToString();
        }

        public string InsertIntoARPaymentType(DbTransaction trans, string PaymentId, PaymentMethod pm, string branchId, string posId, string postedServerId, bool isOffline)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_ins_ARPaymentType");
            cmd.CommandTimeout = timeout;

            db.AddInParameter(cmd, "PaymentId", DbType.String, PaymentId);
            db.AddInParameter(cmd, "Amount", DbType.Decimal, pm.ToPayAmount);
            db.AddInParameter(cmd, "ChangeAmount", DbType.Decimal, pm.ChangeAmount);
            db.AddInParameter(cmd, "FeeAmount", DbType.Decimal, pm.FeeAmount);
            db.AddInParameter(cmd, "PtId", DbType.String, pm.PtId);
            db.AddInParameter(cmd, "BankKey", DbType.String, pm.BankId);
            db.AddInParameter(cmd, "BankName", DbType.String, pm.BankName);
            db.AddInParameter(cmd, "GroupBankName", DbType.String, pm.GroupBankName);
            db.AddInParameter(cmd, "ChqNo", DbType.String, pm.ChqNo);
            db.AddInParameter(cmd, "ChqAccNo", DbType.String, pm.ChqAccNo);
            db.AddInParameter(cmd, "ChqDt", DbType.DateTime, pm.ChqDt);
            db.AddInParameter(cmd, "DraftFlag", DbType.String, pm.DraftFlag);
            db.AddInParameter(cmd, "CashierChequeFlag", DbType.String, pm.CashierChequeFlag);
            db.AddInParameter(cmd, "TranfAccNo", DbType.String, pm.DepositAccNo);
            db.AddInParameter(cmd, "TranfDt", DbType.DateTime, pm.DepositDt);
            db.AddInParameter(cmd, "CancelARPtId", DbType.String, null);
            db.AddInParameter(cmd, "ClearingAccNo", DbType.String, pm.ClaringAccNo);
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "PosId", DbType.String, posId);
            db.AddInParameter(cmd, "PostedServerId", DbType.String, postedServerId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, ((isOffline == true) ? "OFFLINE" : Session.User.Id));

            return db.ExecuteScalar(cmd, trans).ToString();
        }

        public string InsertIntoARPayment(DbTransaction trans, Invoice iv, string branchId, string PmId, DateTime paymentDate, string posId, string postedServerId, bool isOffline)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_ins_ARPayment");
            cmd.CommandTimeout = timeout;

            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "InvoiceNo", DbType.String, iv.InvoiceNo);
            db.AddInParameter(cmd, "PmId", DbType.String, PmId);
            db.AddInParameter(cmd, "PaymentDt", DbType.DateTime, paymentDate);
            db.AddInParameter(cmd, "Qty", DbType.Decimal, iv.ToPayQty);
            db.AddInParameter(cmd, "VatAmount", DbType.Decimal, iv.ToPayVatAmount);
            db.AddInParameter(cmd, "GAmount", DbType.Decimal, iv.ToPayGAmount);
            db.AddInParameter(cmd, "AdjAmount", DbType.Decimal, iv.ToPayAdjAmount);
            db.AddInParameter(cmd, "CancelARPmId", DbType.String, null);
            db.AddInParameter(cmd, "PosId", DbType.String, posId);
            db.AddInParameter(cmd, "PostedServerId", DbType.String, postedServerId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, ((isOffline == true) ? "OFFLINE" : Session.User.Id));

            return db.ExecuteScalar(cmd, trans).ToString();
        }


        public void InsertIntoRTARPaymentTypeARPayment(DbTransaction trans, string ARPtId, string ARPmId, decimal? Amount, string postedServerId, bool isOffline)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_ins_RTARPaymentTypeARPayment");
            cmd.CommandTimeout = timeout;

            db.AddInParameter(cmd, "ARPtId", DbType.String, ARPtId);
            db.AddInParameter(cmd, "ARPmId", DbType.String, ARPmId);
            db.AddInParameter(cmd, "Amount", DbType.Decimal, Amount);
            db.AddInParameter(cmd, "PostedServerId", DbType.String, postedServerId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, ((isOffline == true) ? "OFFLINE" : Session.User.Id));
            db.ExecuteScalar(cmd, trans);
        }

        //Offline Mode Only
        public string InsertIntoAROffline(DbTransaction trans, string branchId, Invoice invoice, string postedServerId)
        {
            Bill bill = invoice.Bills[0];
            string invoiceNo = invoice.InvoiceNo;

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_ins_OfflineAR");
            cmd.CommandTimeout = timeout;

            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "CaId", DbType.String, bill.CustomerId);
            db.AddInParameter(cmd, "DtId", DbType.String, bill.DebtId);
            db.AddInParameter(cmd, "Description", DbType.String, bill.Description);
            db.AddInParameter(cmd, "InvoiceNo", DbType.String, invoiceNo);
            db.AddInParameter(cmd, "GroupInvoiceNo", DbType.String, bill.GroupInvoiceId);
            db.AddInParameter(cmd, "BillBookId", DbType.String, bill.BillBookId);
            db.AddInParameter(cmd, "Period", DbType.String, bill.Period);
            db.AddInParameter(cmd, "MeterReadDt", DbType.DateTime, bill.DueDate);
            db.AddInParameter(cmd, "ReadUnit", DbType.Decimal, bill.PreviousUnit);
            db.AddInParameter(cmd, "LastUnit", DbType.Decimal, bill.LastUnit);
            db.AddInParameter(cmd, "BaseAmount", DbType.Decimal, bill.BaseAmount);
            db.AddInParameter(cmd, "FTUnitPrice", DbType.Decimal, bill.FtUnitPrice);
            db.AddInParameter(cmd, "FTAmount", DbType.Decimal, bill.FtAmount);
            db.AddInParameter(cmd, "Amount", DbType.Decimal, bill.AmountExVat);
            db.AddInParameter(cmd, "UnitPrice", DbType.Decimal, bill.UnitPrice);
            //------------------------------------------------------------------
            decimal? QtyDecimal;
            if (bill.Qty.ToString().Length > 18)
            {
                QtyDecimal = null;
            }
            else
            {
                QtyDecimal = bill.Qty;
            }
            db.AddInParameter(cmd, "Qty", DbType.Decimal, QtyDecimal);
            //db.AddInParameter(cmd, "Qty", DbType.Decimal, bill.Qty);
            //------------------------------------------------------------------
            db.AddInParameter(cmd, "UnitTypeId", DbType.String, bill.UnitTypeId);
            db.AddInParameter(cmd, "TaxCode", DbType.String, bill.TaxCode);
            //------------------------------------------------------------------
            decimal? VatAmountDecimal;
            if (bill.VatAmount.ToString().Length > 18)
            {
                VatAmountDecimal = null;
            }
            else
            {
                VatAmountDecimal = bill.VatAmount;
            }
            db.AddInParameter(cmd, "VatAmount", DbType.Decimal, VatAmountDecimal);
            //db.AddInParameter(cmd, "VatAmount", DbType.Decimal, bill.VatAmount);
            //------------------------------------------------------------------
            decimal? GAmountDecimal;
            if (bill.GAmount.ToString().Length > 18)
            {
                GAmountDecimal = null;
            }
            else
            {
                GAmountDecimal = bill.GAmount;
            }
            db.AddInParameter(cmd, "GAmount", DbType.Decimal, GAmountDecimal);
            //db.AddInParameter(cmd, "GAmount", DbType.Decimal, bill.GAmount);
            //------------------------------------------------------------------
            db.AddInParameter(cmd, "DueDt", DbType.DateTime, bill.DueDate);
            db.AddInParameter(cmd, "DisconnectDt", DbType.DateTime, bill.DisConnectDate);
            db.AddInParameter(cmd, "DisconnectDoc", DbType.String, bill.DisconnectDocNo);
            db.AddInParameter(cmd, "OriginalInvoiceNo", DbType.String, invoice.OriginalInvoiceNo);
            db.AddInParameter(cmd, "InstallmentFlag", DbType.String, bill.InstallmentFlag);
            //db.AddInParameter(cmd, "LastInstallmentFlag", DbType.String, bill.LastInstallmentFlag);
            //db.AddInParameter(cmd, "SpotBillFlag", DbType.String, bill.SpotBillFlag);

            //db.AddInParameter(cmd, "InstallmentFlag", DbType.String, bill.InstallmentFlag);
            //db.AddInParameter(cmd, "LastInstallmentFlag", DbType.String, bill.LastInstallmentFlag);

            db.AddInParameter(cmd, "CancelFlag", DbType.String, bill.CancelFlag);
            db.AddInParameter(cmd, "ModifiedFlag", DbType.String, bill.ModifiedFlag);
            db.AddInParameter(cmd, "PostedServerId", DbType.String, postedServerId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, Session.User.Id);

            return db.ExecuteScalar(cmd, trans).ToString();
        }

        public string InsertIntoPaymentOffline(DbTransaction trans, DateTime PaymentDt, string PosId,
            string branchId, string postedServerId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_ins_OfflinePayment");
            cmd.CommandTimeout = timeout;

            db.AddInParameter(cmd, "PaymentDt", DbType.DateTime, PaymentDt);
            db.AddInParameter(cmd, "PosId", DbType.String, PosId);
            db.AddInParameter(cmd, "CashierId", DbType.String, Session.User.Id);
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "PostedServerId", DbType.String, postedServerId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, Session.User.Id);

            return db.ExecuteScalar(cmd, trans).ToString();
        }

        public string InsertIntoARPaymentTypeOffline(DbTransaction trans, string PaymentId, PaymentMethod pm, string postedServerId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_ins_OfflineARPaymentType");
            cmd.CommandTimeout = timeout;

            db.AddInParameter(cmd, "PaymentId", DbType.String, PaymentId);
            db.AddInParameter(cmd, "Amount", DbType.Decimal, pm.ToPayAmount);
            db.AddInParameter(cmd, "ChangeAmount", DbType.Decimal, pm.ChangeAmount);
            db.AddInParameter(cmd, "FeeAmount", DbType.Decimal, pm.FeeAmount);
            db.AddInParameter(cmd, "PtId", DbType.String, pm.PtId);
            db.AddInParameter(cmd, "BankKey", DbType.String, pm.BankId);
            db.AddInParameter(cmd, "ChqNo", DbType.String, pm.ChqNo);
            db.AddInParameter(cmd, "ChqAccNo", DbType.String, pm.ChqAccNo);
            db.AddInParameter(cmd, "ChqDt", DbType.DateTime, pm.ChqDt);
            db.AddInParameter(cmd, "DraftFlag", DbType.String, pm.DraftFlag);
            db.AddInParameter(cmd, "CashierChequeFlag", DbType.String, pm.CashierChequeFlag);
            db.AddInParameter(cmd, "TranfAccNo", DbType.String, pm.DepositAccNo);
            db.AddInParameter(cmd, "TranfDt", DbType.DateTime, pm.DepositDt);
            db.AddInParameter(cmd, "CancelARPtId", DbType.String, null);
            db.AddInParameter(cmd, "PostedServerId", DbType.String, postedServerId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, Session.User.Id);

            return db.ExecuteScalar(cmd, trans).ToString();
        }

        public string InsertIntoARPaymentOffline(DbTransaction trans, Invoice iv, string branchId, string PmId, DateTime paymentDate, string postedServerId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_ins_OfflineARPayment");
            cmd.CommandTimeout = timeout;

            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "InvoiceNo", DbType.String, iv.InvoiceNo);
            db.AddInParameter(cmd, "PmId", DbType.String, PmId);
            db.AddInParameter(cmd, "PaymentDt", DbType.DateTime, paymentDate);
            db.AddInParameter(cmd, "Qty", DbType.Decimal, iv.ToPayQty);
            db.AddInParameter(cmd, "VatAmount", DbType.Decimal, iv.ToPayVatAmount);
            db.AddInParameter(cmd, "GAmount", DbType.Decimal, iv.ToPayGAmount);
            db.AddInParameter(cmd, "AdjAmount", DbType.Decimal, iv.ToPayAdjAmount);
            db.AddInParameter(cmd, "CancelARPmId", DbType.String, null);
            db.AddInParameter(cmd, "PostedServerId", DbType.String, postedServerId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, Session.User.Id);

            return db.ExecuteScalar(cmd, trans).ToString();
        }


        public void InsertIntoRTARPaymentTypeARPaymentOffline(DbTransaction trans, string ARPtId, string ARPmId, decimal? Amount, string postedServerId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_ins_OfflineRTARPaymentTypeARPayment");
            cmd.CommandTimeout = timeout;

            db.AddInParameter(cmd, "ARPtId", DbType.String, ARPtId);
            db.AddInParameter(cmd, "ARPmId", DbType.String, ARPmId);
            db.AddInParameter(cmd, "Amount", DbType.Decimal, Amount);
            db.AddInParameter(cmd, "PostedServerId", DbType.String, postedServerId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, Session.User.Id);
            db.ExecuteScalar(cmd, trans);
        }


        public void UpdateElectricPaymentByBillBook(DbTransaction trans, string billBookId, string bookPaymentId,
            DateTime? paymentDate, string posId, string terminalCode, string branchId, string branchName,
            string cashierId, string cashierName, string workId, string postedServerId, bool isOffline)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_upd_ElectUserPaymentByBillBook");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "BillBookId", DbType.String, billBookId);
            db.AddInParameter(cmd, "BPaymentId", DbType.String, bookPaymentId);
            db.AddInParameter(cmd, "PaymentDt", DbType.DateTime, paymentDate);
            db.AddInParameter(cmd, "PosId", DbType.String, posId);
            db.AddInParameter(cmd, "TerminalCode", DbType.String, terminalCode);
            db.AddInParameter(cmd, "CashierId", DbType.String, cashierId);
            db.AddInParameter(cmd, "CashierName", DbType.String, cashierName);
            db.AddInParameter(cmd, "WorkId", DbType.String, workId);
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "BranchName", DbType.String, branchName);
            db.AddInParameter(cmd, "PostedServerId", DbType.String, postedServerId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, ((isOffline == true) ? "OFFLINE" : Session.User.Id));
            db.ExecuteScalar(cmd, trans);
        }

        public void UpdateElectricPaymentByGroupInvoice(DbTransaction trans, string billBookId, string groupPaymentId,
            DateTime? paymentDate, string posId, string terminalCode, string branchId, string branchName,
            string cashierId, string cashierName, string workId, string postedServerId, bool isOffline)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_upd_ElectUserPaymentByGroupInvoice");
            cmd.CommandTimeout = timeout;

            db.AddInParameter(cmd, "BillBookId", DbType.String, billBookId);
            db.AddInParameter(cmd, "GPaymentId", DbType.String, groupPaymentId);
            db.AddInParameter(cmd, "PaymentDt", DbType.DateTime, paymentDate);
            db.AddInParameter(cmd, "PosId", DbType.String, posId);
            db.AddInParameter(cmd, "TerminalCode", DbType.String, terminalCode);
            db.AddInParameter(cmd, "CashierId", DbType.String, cashierId);
            db.AddInParameter(cmd, "CashierName", DbType.String, cashierName);
            db.AddInParameter(cmd, "WorkId", DbType.String, workId);
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "BranchName", DbType.String, branchName);
            db.AddInParameter(cmd, "PostedServerId", DbType.String, postedServerId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, ((isOffline == true) ? "OFFLINE" : Session.User.Id));
            db.ExecuteScalar(cmd, trans);
        }

        public void SaveReceipt(DbTransaction trans, PrintingReceipt receipt, ExternalReceipt extReceipt, string postedServerId, bool isOffline)
        {
            string receiptId = receipt.ReceiptId.Replace("-", "");

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_ins_Receipt");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "ReceiptId", DbType.String, receiptId);
            db.AddInParameter(cmd, "PaymentId", DbType.String, receipt.PrintingInvoices[0].PaymentId);
            db.AddInParameter(cmd, "PrintingSequence", DbType.Int16, receipt.PrintingSequence);
            db.AddInParameter(cmd, "TotalReceipt", DbType.Int16, receipt.TotalReceipt);
            db.AddInParameter(cmd, "CaId", DbType.String, receipt.PrintingInvoices[0].CaId);
            db.AddInParameter(cmd, "Name", DbType.String, receipt.CustomerName);

            //Tax 13
            db.AddInParameter(cmd, "CaTaxId", DbType.String, receipt.CaTaxId);
            db.AddInParameter(cmd, "CaTaxBranch", DbType.String, receipt.CaTaxBranch);

            db.AddInParameter(cmd, "Address", DbType.String, receipt.CustomerAddress);
            db.AddInParameter(cmd, "IsNameAddrModified", DbType.String, receipt.IsNameAddressModified ? "1" : "0");
            db.AddInParameter(cmd, "NoOfPrinting", DbType.Int32, 1);
            if (extReceipt.ReceiptId == null)
            {
                db.AddInParameter(cmd, "ReceiptType", DbType.String, receipt.ReceiptType[0].ToString());
            }
            else
            {
                db.AddInParameter(cmd, "ReceiptType", DbType.String, receipt.ReceiptType[2].ToString());
            }

            if (receipt.GroupReceiptIdInstallment != null)
                db.AddInParameter(cmd, "ExtReceiptId", DbType.String, receipt.GroupReceiptIdInstallment);   // Group receipt installment. 
            else 
                db.AddInParameter(cmd, "ExtReceiptId", DbType.String, extReceipt.ReceiptId);
            
            db.AddInParameter(cmd, "ExtReceiptDt", DbType.DateTime, extReceipt.ReceiptDate);

            db.AddInParameter(cmd, "CaDoc", DbType.String, receipt.PrintingInvoices[0].CaDoc);
            db.AddInParameter(cmd, "Description", DbType.String, receipt.PrintingInvoices[0].Bills[0].Description);
            db.AddInParameter(cmd, "InvoiceNo", DbType.String, receipt.PrintingInvoices[0].InvoiceNo);
            db.AddInParameter(cmd, "InvoiceDt", DbType.DateTime, receipt.PrintingInvoices[0].InvoiceDate);

            //Case Installment
            if (receipt.PrintingInvoices[0].OriginalInvoice != null && receipt.PrintingInvoices[0].InstallmentTotalPeriod != null && receipt.PrintingInvoices[0].InstallmentTotalPeriod > 0)
            {
                db.AddInParameter(cmd, "OriginalInvoiceNo", DbType.String, receipt.PrintingInvoices[0].OriginalInvoiceNo);
                db.AddInParameter(cmd, "OriginalInvoiceDt", DbType.DateTime, receipt.PrintingInvoices[0].OriginalInvoiceDt);
                db.AddInParameter(cmd, "QtyInstallment", DbType.Decimal, receipt.PrintingInvoices[0].OriginalInvoice.Qty);
                db.AddInParameter(cmd, "AmountInstallment", DbType.Decimal, receipt.PrintingInvoices[0].OriginalInvoice.AmountExVat);
                db.AddInParameter(cmd, "VatAmountInstallment", DbType.Decimal, receipt.PrintingInvoices[0].OriginalInvoice.VatAmount);
                db.AddInParameter(cmd, "GAmountInstallment", DbType.Decimal, receipt.PrintingInvoices[0].OriginalInvoice.GAmount);
            }
            else
            {
                if (receipt.PrintingInvoices[0].Bills[0].DebtId == CodeNames.DebtType.Interest.Id
                    || receipt.PrintingInvoices[0].Bills[0].DebtId == "M00300010")
                {
                    db.AddInParameter(cmd, "OriginalInvoiceNo", DbType.String, receipt.PrintingInvoices[0].OriginalInvoiceNo);
                }
                else
                {
                    db.AddInParameter(cmd, "OriginalInvoiceNo", DbType.String, null);
                }
                db.AddInParameter(cmd, "OriginalInvoiceDt", DbType.DateTime, null);
                db.AddInParameter(cmd, "QtyInstallment", DbType.Decimal, null);
                db.AddInParameter(cmd, "AmountInstallment", DbType.Decimal, null);
                db.AddInParameter(cmd, "VatAmountInstallment", DbType.Decimal, null);
                db.AddInParameter(cmd, "GAmountInstallment", DbType.Decimal, null);
            }

            db.AddInParameter(cmd, "InstallmentPeriod", DbType.String, receipt.PrintingInvoices[0].InstallmentPeriod);
            db.AddInParameter(cmd, "InstallmentTotalPeriod", DbType.String, receipt.PrintingInvoices[0].InstallmentTotalPeriod);
            db.AddInParameter(cmd, "GroupInvoiceNo", DbType.String, receipt.PrintingInvoices[0].Bills[0].GroupInvoiceId);
            db.AddInParameter(cmd, "BillBookId", DbType.String, receipt.PrintingInvoices[0].BillBookId);
            db.AddInParameter(cmd, "SpotBillInvoiceNo", DbType.String, receipt.PrintingInvoices[0].SpotBillInvoiceNo);
            db.AddInParameter(cmd, "MeterId", DbType.String, receipt.PrintingInvoices[0].Bills[0].MeterId);
            db.AddInParameter(cmd, "RateTypeId", DbType.String, receipt.PrintingInvoices[0].Bills[0].RateTypeId);
            db.AddInParameter(cmd, "BranchId", DbType.String, receipt.PrintingInvoices[0].BranchId);
            db.AddInParameter(cmd, "TechBranchName", DbType.String, receipt.PrintingInvoices[0].TechBranchName);
            db.AddInParameter(cmd, "CommBranchId", DbType.String, receipt.PrintingInvoices[0].CommBranchId);
            db.AddInParameter(cmd, "CommBranchName", DbType.String, receipt.PrintingInvoices[0].CommBranchName);
            db.AddInParameter(cmd, "MruId", DbType.String, receipt.PrintingInvoices[0].MruId);
            db.AddInParameter(cmd, "Period", DbType.String, receipt.PrintingInvoices[0].Period);
            db.AddInParameter(cmd, "MeterReadDt", DbType.DateTime, receipt.PrintingInvoices[0].Bills[0].MeterReadDate);
            db.AddInParameter(cmd, "ReadUnit", DbType.Decimal, receipt.PrintingInvoices[0].Bills[0].PreviousUnit);
            db.AddInParameter(cmd, "LastUnit", DbType.Decimal, receipt.PrintingInvoices[0].Bills[0].LastUnit);
            db.AddInParameter(cmd, "FullBaseAmount", DbType.Decimal, GetAmount(receipt.PrintingInvoices[0].Bills, "FullBaseAmount"));
            db.AddInParameter(cmd, "FullFTAmount", DbType.Decimal, GetAmount(receipt.PrintingInvoices[0].Bills, "FullFtAmount"));
            db.AddInParameter(cmd, "FullQty", DbType.Decimal, GetAmount(receipt.PrintingInvoices[0].Bills, "FullQty"));

            if (receipt.PrintingInvoices[0].Bills[0].DebtId == "P00010002")
            {
                db.AddInParameter(cmd, "FullAmount", DbType.Decimal, GetBillBookAmount(receipt.PrintingInvoices, "FullAmount"));
                db.AddInParameter(cmd, "FullGAmount", DbType.Decimal, GetBillBookAmount(receipt.PrintingInvoices, "FullGAmount"));
                db.AddInParameter(cmd, "Amount", DbType.Decimal, GetBillBookAmount(receipt.PrintingInvoices, "Amount"));
                db.AddInParameter(cmd, "GAmount", DbType.Decimal, GetBillBookAmount(receipt.PrintingInvoices, "GAmount"));
            }
            else
            {
                db.AddInParameter(cmd, "FullAmount", DbType.Decimal, GetAmount(receipt.PrintingInvoices[0].Bills, "FullAmount"));
                db.AddInParameter(cmd, "FullGAmount", DbType.Decimal, GetAmount(receipt.PrintingInvoices[0].Bills, "FullGAmount"));
                db.AddInParameter(cmd, "Amount", DbType.Decimal, receipt.PrintingInvoices[0].ToPayExVatAmount);
                db.AddInParameter(cmd, "GAmount", DbType.Decimal, receipt.PrintingInvoices[0].ToPayGAmount);
            }

            db.AddInParameter(cmd, "FullVatAmount", DbType.Decimal, GetAmount(receipt.PrintingInvoices[0].Bills, "FullVatAmount"));
            db.AddInParameter(cmd, "BaseAmount", DbType.Decimal, receipt.PrintingInvoices[0].Bills[0].BaseAmount);
            db.AddInParameter(cmd, "FtUnitPrice", DbType.Decimal, receipt.PrintingInvoices[0].Bills[0].FtUnitPrice);
            db.AddInParameter(cmd, "FtAmount", DbType.Decimal, receipt.PrintingInvoices[0].Bills[0].FtAmount);
            db.AddInParameter(cmd, "UnitPrice", DbType.Decimal, receipt.PrintingInvoices[0].Bills[0].UnitPrice);
            db.AddInParameter(cmd, "Qty", DbType.Decimal, receipt.PrintingInvoices[0].ToPayQty);
            db.AddInParameter(cmd, "VatAmount", DbType.Decimal, receipt.PrintingInvoices[0].ToPayVatAmount);
            db.AddInParameter(cmd, "DtId", DbType.String, receipt.PrintingInvoices[0].Bills[0].DebtId);
            db.AddInParameter(cmd, "DtName", DbType.String, receipt.PrintingInvoices[0].Bills[0].DebtType);
            //db.AddInParameter(cmd, "ControllerId", DbType.String, receipt.PrintingInvoices[0].Bills[0].ControllerId);
            db.AddInParameter(cmd, "ControllerId", DbType.String, receipt.PrintingInvoices[0].ControllerId);
            db.AddInParameter(cmd, "ControllerName", DbType.String, receipt.PrintingInvoices[0].ControllerName);
            db.AddInParameter(cmd, "TaxCode", DbType.String, receipt.PrintingInvoices[0].Bills[0].TaxCode);
            db.AddInParameter(cmd, "TaxRate", DbType.Decimal, receipt.PrintingInvoices[0].Bills[0].TaxRate);
            db.AddInParameter(cmd, "PartialPayment", DbType.Int16, GetPartialPayment(receipt.PrintingInvoices[0]));
            db.AddInParameter(cmd, "GroupIvReceiptType", DbType.String, receipt.PrintingInvoices[0].GroupInvoiceReceiptType);


            db.AddInParameter(cmd, "PostedServerId", DbType.String, postedServerId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, ((isOffline == true) ? "OFFLINE" : Session.User.Id));
            db.ExecuteNonQuery(cmd, trans);

            foreach (PrintingInvoice b in receipt.PrintingInvoices)
            {
                cmd = db.GetStoredProcCommand("pc_ins_ReceiptItem");
                db.AddInParameter(cmd, "ReceiptId", DbType.String, receiptId);
                db.AddInParameter(cmd, "ARPmId", DbType.String, b.ARPmId);
                db.AddInParameter(cmd, "PostedServerId", DbType.String, postedServerId);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, ((isOffline == true) ? "OFFLINE" : Session.User.Id));
                db.ExecuteNonQuery(cmd, trans);
            }

            //cmd = db.GetStoredProcCommand("pc_get_ReceiptForPrint");
            //db.AddInParameter(cmd, "ReceiptId", DbType.String, receiptId);
            //DataSet ds = (null == trans) ? db.ExecuteDataSet(cmd) : db.ExecuteDataSet(cmd, trans);
            //DataTable dt = ds.Tables[2];
            //PrintingInvoice iv = new PrintingInvoice();
            //iv.Bills = new List<Bill>();
            //foreach (DataRow drx in dt.Rows)
            //{
            //    Bill b = new Bill();
            //    b.invoice=DaHelper.GetString(drx, "Invoiceno");
            //    b.IsServiceEndOfTheYear = DaHelper.GetString(drx, "IsServiceEndOfTheYear");
            //    b.IsExpenseDuringTheYear = DaHelper.GetString(drx, "IsExpenseDuringTheYear");
            //    iv.Bills.Add(b);
            //}
        }

        //TODO: INSTALLMENT CASE
        //public void SaveInterestLog(DbTransaction trans, List<InterestLog> interestLog)
        //{
        //    Database db = DatabaseFactory.CreateDatabase("POSDatabase");
        //    DbCommand cmd = db.GetStoredProcCommand("pc_ins_InterestLog");
        //    cmd.CommandTimeout = timeout;

        //    foreach (InterestLog i in interestLog)
        //    {
        //        db.AddInParameter(cmd, "CaDoc", DbType.String, i.CaDoc);
        //        db.AddInParameter(cmd, "SubCaDoc", DbType.String, i.SubCaDoc);
        //        db.AddInParameter(cmd, "InvoiceNo", DbType.String, i.InvoiceNo);
        //        db.AddInParameter(cmd, "PaymentBranchId", DbType.String, i.PaymentBranchId);
        //        db.AddInParameter(cmd, "TechBranchId", DbType.String, i.TechBranchId);
        //        db.AddInParameter(cmd, "PaymentDt", DbType.DateTime, i.PaymentDt);
        //        db.AddInParameter(cmd, "CashierId", DbType.String, i.CashierId);
        //        db.AddInParameter(cmd, "CashierName", DbType.String, i.CashierName);
        //        db.ExecuteNonQuery(cmd, trans);
        //    }
        //}

        private decimal? GetAmount(List<Bill> bills, string columnName)
        {
            decimal? amount = 0;

            foreach (Bill b in bills)
            {
                switch (columnName)
                {
                    case "FullBaseAmount":
                        amount += b.FullBaseAmount;
                        break;
                    case "FullFtAmount":
                        amount += b.FullFtAmount;
                        break;
                    case "FullQty":
                        amount += b.FullQty;
                        break;
                    case "FullAmount":
                        amount += b.FullAmount;
                        break;
                    case "FullVatAmount":
                        amount += b.FullVatAmount;
                        break;
                    case "FullGAmount":
                        amount += b.FullGAmount;
                        break;
                    default:
                        amount = null;
                        break;
                }
            }

            return amount;
        }

        private decimal? GetBillBookAmount(List<PrintingInvoice> inv, string columnName)
        {
            decimal? amount = 0;

            foreach (PrintingInvoice i in inv)
            {
                switch (columnName)
                {
                    case "FullAmount":
                        amount += i.ToPayGAmount;
                        break;
                    case "FullGAmount":
                        amount += i.ToPayGAmount;
                        break;
                    case "Amount":
                        amount += i.ToPayGAmount;
                        break;
                    case "GAmount":
                        amount += i.ToPayGAmount;
                        break;
                    default:
                        amount = null;
                        break;
                }
            }

            return amount;
        }

        private Int16 GetPartialPayment(PrintingInvoice pi)
        {
            if (pi.InstallmentTotalPeriod != null && pi.InstallmentTotalPeriod > 0)
            {
                if (pi.InstallmentPeriod == pi.InstallmentTotalPeriod)
                {
                    if ((pi.TotalPaidAmount - pi.ToPayGAmount) == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                if ((pi.TotalPaidAmount - pi.ToPayGAmount) == 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }



        public void SaveReceiptOffline(DbTransaction trans, PrintingReceipt receipt, ExternalReceipt extReceipt, string postedServerId)
        {
            string receiptId = receipt.ReceiptId.Replace("-", "");

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_ins_OfflineReceipt");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "ReceiptId", DbType.String, receiptId);
            db.AddInParameter(cmd, "PaymentId", DbType.String, receipt.PrintingInvoices[0].PaymentId);
            db.AddInParameter(cmd, "PrintingSequence", DbType.Int16, receipt.PrintingSequence);
            db.AddInParameter(cmd, "TotalReceipt", DbType.Int16, receipt.TotalReceipt);
            db.AddInParameter(cmd, "CaId", DbType.String, receipt.PrintingInvoices[0].CaId);
            db.AddInParameter(cmd, "Name", DbType.String, receipt.CustomerName);
            db.AddInParameter(cmd, "Address", DbType.String, receipt.CustomerAddress);
            db.AddInParameter(cmd, "IsNameAddrModified", DbType.String, receipt.IsNameAddressModified ? "1" : "0");
            db.AddInParameter(cmd, "NoOfPrinting", DbType.Int32, 1);
            if (extReceipt.ReceiptId == null)
            {
                db.AddInParameter(cmd, "ReceiptType", DbType.String, receipt.ReceiptType[0].ToString());
            }
            else
            {
                db.AddInParameter(cmd, "ReceiptType", DbType.String, receipt.ReceiptType[2].ToString());
            }
            db.AddInParameter(cmd, "ExtReceiptId", DbType.String, extReceipt.ReceiptId);
            db.AddInParameter(cmd, "ExtReceiptDt", DbType.DateTime, extReceipt.ReceiptDate);
            db.AddInParameter(cmd, "PostedServerId", DbType.String, postedServerId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, Session.User.Id);
            db.ExecuteNonQuery(cmd, trans);

            foreach (PrintingInvoice b in receipt.PrintingInvoices)
            {
                cmd = db.GetStoredProcCommand("pc_ins_OfflineReceiptItem");
                db.AddInParameter(cmd, "ReceiptId", DbType.String, receiptId);
                db.AddInParameter(cmd, "ARPmId", DbType.String, b.ARPmId);
                db.AddInParameter(cmd, "PostedServerId", DbType.String, postedServerId);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, Session.User.Id);
                db.ExecuteNonQuery(cmd, trans);
            }
        }


        public List<Branch> GetBranchDetail(string branchId)
        {
            List<Branch> branch = new List<Branch>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_get_BranchDetail");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                branch.Add(
                    new Branch(
                        DaHelper.GetString(dr, "BranchId"),
                        DaHelper.GetString(dr, "BranchName")
                    )
                    );
            }

            return branch;
        }

        //===
        public List<DisconnectionDoc> SearchDisconnectionStatusByDiscNo(string discNo)
        {
            List<DisconnectionDoc> disconnectDoc = new List<DisconnectionDoc>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            DbCommand cmd = db.GetStoredProcCommand("pc_sel_Disconnection");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "DiscNo", DbType.String, discNo);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                DisconnectionDoc obj = new DisconnectionDoc();
                obj.DiscNo = DaHelper.GetString(dr, "DiscNo");
                obj.CreatedDt = DaHelper.GetDate(dr, "CreatedDt");
                obj.DiscStatusId = DaHelper.GetString(dr, "DiscStatusId");
                obj.DiscStatus = DaHelper.GetString(dr, "DiscStatus");
                obj.ReleaseDt = DaHelper.GetDate(dr, "ReleaseDt");
                obj.WorkOrderNo = DaHelper.GetString(dr, "WorkOrderNo");
                obj.DiscPlanStart = DaHelper.GetDate(dr, "DiscPlanStart");
                obj.DiscPlanEnd = DaHelper.GetDate(dr, "DiscPlanEnd");
                obj.WorkCenter = DaHelper.GetString(dr, "WorkCenter");
                obj.PostpConfirm = DaHelper.GetDate(dr, "PostpConfirm");
                obj.FuseRemConfirm = DaHelper.GetDate(dr, "FuseRemConfirm");
                obj.MeterRemConfirm = DaHelper.GetDate(dr, "MeterRemConfirm");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.Active = DaHelper.GetString(dr, "Active");

                disconnectDoc.Add(obj);
            }

            return disconnectDoc;
        }

        //Use app server time
        //public DateTime GetServerTime()
        //{
        //    Database db = DatabaseFactory.CreateDatabase("POSDatabase");
        //    DbCommand cmd = db.GetStoredProcCommand("ta_get_ServerTime");
        //    cmd.CommandTimeout = timeout;

        //    return (DateTime)db.ExecuteScalar(cmd);
        //}

        public LastDisconnect GetLastDisconnect(string caId)
        {
            LastDisconnect aLastDisconnect = null;

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_get_LastDisconnectByCaId");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "CaId", DbType.String, caId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];


            aLastDisconnect = new LastDisconnect();
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                aLastDisconnect.CaId = DaHelper.GetString(dr, "caId");
                aLastDisconnect.PaidDisconnectFlag = DaHelper.GetInt(dr, "PaidDisconnectFlag") == 1 ? true : false;
                aLastDisconnect.LastDisconnectDate = DaHelper.GetDate(dr, "lastDisconnectDt");
                aLastDisconnect.LastDisconnectDoc = DaHelper.GetString(dr, "lastDisconnectNo");
            }
            else
            {
                aLastDisconnect.CaId = caId;
                aLastDisconnect.PaidDisconnectFlag = true;
                aLastDisconnect.LastDisconnectDate = null;
                aLastDisconnect.LastDisconnectDoc = null;
            }

            return aLastDisconnect;
        }

        public void ProcessOfflinePaymentQueue(DbTransaction trans)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_ins_OfflineClarifyPayment");
            cmd.CommandTimeout = timeout;
            db.ExecuteNonQuery(cmd, trans);
        }

        public CaAndBpInfo.BusinessPartnerInfo GetBpOtherBranch(string caId)
        {
            CaAndBpInfo.BusinessPartnerInfo b = new CaAndBpInfo.BusinessPartnerInfo();

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_get_BusinessPartnerOtherBranch");
            cmd.CommandTimeout = 10000;
            db.AddInParameter(cmd, "pCaId", DbType.String, caId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count == 0) return b;
            DataRow dr = dt.Rows[0];

            b.BpId = DaHelper.GetString(dr, "BpId");
            b.BpTypeId = DaHelper.GetString(dr, "BpTypeId");
            b.TaxCode = DaHelper.GetString(dr, "TaxCode");
            b.CitizenId = DaHelper.GetString(dr, "CitizenId");
            b.PassportNo = DaHelper.GetString(dr, "PassportNo");
            b.RegisterId = DaHelper.GetString(dr, "RegisterId");
            b.VatCode = DaHelper.GetString(dr, "VatCode");
            b.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
            b.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");
            b.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
            b.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;

            return b;
        }

        public CaAndBpInfo.ContractAccountInfo GetCaOtherBranch(string caId)
        {
            CaAndBpInfo.ContractAccountInfo b = new CaAndBpInfo.ContractAccountInfo();

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_get_ContractAccountOtherBranch");
            cmd.CommandTimeout = 10000;
            db.AddInParameter(cmd, "pCaId", DbType.String, caId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count == 0) return b;
            DataRow dr = dt.Rows[0];

            b.CaId = DaHelper.GetString(dr, "CaId");
            b.TechBranchId = DaHelper.GetString(dr, "TechBranchId");
            b.CommBranchId = DaHelper.GetString(dr, "CommBranchId");
            b.MruId = DaHelper.GetString(dr, "MruId");
            b.BpId = DaHelper.GetString(dr, "BpId");
            b.CaName = DaHelper.GetString(dr, "CaName");
            b.ReceiptType = DaHelper.GetString(dr, "GroupIvReceiptType");
            b.ReceiptPrintName = DaHelper.GetString(dr, "ReceiptPrintName");
            b.CaAddress = DaHelper.GetString(dr, "CaAddress");
            b.CtId = DaHelper.GetString(dr, "CtId");
            b.PmId = DaHelper.GetString(dr, "PmId");
            b.AccountClassId = DaHelper.GetString(dr, "AccountClassId");
            b.SecurityDeposit = DaHelper.GetDecimal(dr, "SecurityDeposit");
            b.InterestKey = DaHelper.GetString(dr, "InterestKey");
            b.PaidBy = DaHelper.GetString(dr, "PaidBy");
            b.ContractValidFrom = DaHelper.GetDate(dr, "ContractValidFrom");
            b.ContractValidTo = DaHelper.GetDate(dr, "ContractValidTo");
            b.MeterInstallDt = DaHelper.GetDate(dr, "MeterInstallDt");
            b.MeterSizeId = DaHelper.GetString(dr, "MeterSizeId");
            b.ControllerId = DaHelper.GetString(dr, "ControllerId");
            b.TransportHelp = DaHelper.GetDecimal(dr, "TransportHelp");
            b.PenaltyWaiveFlag = DaHelper.GetString(dr, "PenaltyWaiveFlag");
            b.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
            b.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");
            b.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
            b.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;

            return b;
        }

        public void SaveBpAndCaOtherBranch(List<CaAndBpInfo> list, DbTransaction trans)
        {
            CaAndBpInfo.BusinessPartnerInfo bp = list[0].BpObj;
            CaAndBpInfo.ContractAccountInfo ca = list[0].CaObj;

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            DbCommand cmd = db.GetStoredProcCommand("ta_ins_BusinessPartnerOtherBranch");
            db.AddInParameter(cmd, "pBpId", DbType.String, bp.BpId);
            db.AddInParameter(cmd, "pBpTypeId", DbType.String, bp.BpTypeId);
            db.AddInParameter(cmd, "pTaxCode", DbType.String, bp.TaxCode);
            db.AddInParameter(cmd, "pCitizenId", DbType.String, bp.CitizenId);
            db.AddInParameter(cmd, "pPassportNo", DbType.String, bp.PassportNo);
            db.AddInParameter(cmd, "pRegisterId", DbType.String, bp.RegisterId);
            db.AddInParameter(cmd, "pVatCode", DbType.String, bp.VatCode);
            db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
            db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, bp.ModifiedDt);
            db.AddInParameter(cmd, "pModifiedBy", DbType.String, bp.ModifiedBy);
            db.AddInParameter(cmd, "pActive", DbType.String, bp.Active == true ? "1" : "0");
            db.ExecuteNonQuery(cmd, trans);

            cmd = db.GetStoredProcCommand("ta_ins_ContractAccountOtherBranch");
            db.AddInParameter(cmd, "pCaId", DbType.String, ca.CaId);
            db.AddInParameter(cmd, "pTechBranchId", DbType.String, ca.TechBranchId);
            db.AddInParameter(cmd, "pCommBranchId", DbType.String, ca.CommBranchId);
            db.AddInParameter(cmd, "pMruId", DbType.String, ca.MruId);
            db.AddInParameter(cmd, "pBpId", DbType.String, ca.BpId);
            db.AddInParameter(cmd, "pCaName", DbType.String, ca.CaName);
            db.AddInParameter(cmd, "pReceiptPrintName", DbType.String, ca.ReceiptPrintName);
            db.AddInParameter(cmd, "pCaAddress", DbType.String, ca.CaAddress);
            db.AddInParameter(cmd, "pCtId", DbType.String, ca.CtId);
            db.AddInParameter(cmd, "pPmId", DbType.String, ca.PmId);
            db.AddInParameter(cmd, "pAccountClassId", DbType.String, ca.AccountClassId);
            db.AddInParameter(cmd, "pSecurityDeposit", DbType.Decimal, ca.SecurityDeposit);
            db.AddInParameter(cmd, "pInterestKey", DbType.String, ca.InterestKey);
            db.AddInParameter(cmd, "pPaidBy", DbType.String, ca.PaidBy);
            db.AddInParameter(cmd, "pContractValidFrom", DbType.DateTime, ca.ContractValidFrom);
            db.AddInParameter(cmd, "pContractValidTo", DbType.DateTime, ca.ContractValidTo);
            db.AddInParameter(cmd, "pMeterInstallDt", DbType.DateTime, ca.MeterInstallDt);
            db.AddInParameter(cmd, "pMeterSizeId", DbType.String, ca.MeterSizeId);
            db.AddInParameter(cmd, "pControllerId", DbType.String, ca.ControllerId);
            db.AddInParameter(cmd, "pGroupIvReceiptType", DbType.String, ca.ReceiptType);
            db.AddInParameter(cmd, "pTransportHelp", DbType.Decimal, ca.TransportHelp);
            db.AddInParameter(cmd, "pPenaltyWaiveFlag", DbType.String, ca.PenaltyWaiveFlag);
            db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
            db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, ca.ModifiedDt);
            db.AddInParameter(cmd, "pModifiedBy", DbType.String, ca.ModifiedBy);
            db.AddInParameter(cmd, "pActive", DbType.String, ca.Active == true ? "1" : "0");
            db.ExecuteNonQuery(cmd, trans);
        }

        #endregion

        #region Data Mapping Functions

        /// <summary>
        /// Converts to invoice list.
        /// </summary>
        /// <param name="dt">The DataTable of Invoice.</param>
        /// <param name="chkIsPaid">if set to <c>true</c> [Check: GAmount - PaidAmount > 0].</param>
        /// <returns>List of Invoice</returns>
        private List<Invoice> ConvertToInvoiceList(DataTable dt, Boolean chkIsPaid)
        {
            List<Invoice> invoices = new List<Invoice>();
            foreach (DataRow dr in dt.Rows)
            {
                Invoice inv = new Invoice();
                inv.InvoiceNo = DaHelper.GetString(dr, "InvoiceNo");
                inv.InvoiceDate = DaHelper.GetDate(dr, "InvoiceDt");
                inv.BranchId = DaHelper.GetString(dr, "BranchId");
                inv.TechBranchName = DaHelper.GetString(dr, "TechBranchName");
                inv.CommBranchId = DaHelper.GetString(dr, "CommBranchId");
                inv.CommBranchName = DaHelper.GetString(dr, "CommBranchName");
                inv.CaId = DaHelper.GetString(dr, "CaId");
                inv.BpId = DaHelper.GetString(dr, "BpId");
                inv.Name = DaHelper.GetString(dr, "CaName");
                inv.PayByName = DaHelper.GetString(dr, "ReceiptPrintName");
                inv.PmId = DaHelper.GetString(dr, "PmId");
                inv.Address = DaHelper.GetString(dr, "CaAddress");
                inv.GroupInvoiceReceiptType = DaHelper.GetString(dr, "GroupIvReceiptType");
                inv.DueDate = DaHelper.GetDate(dr, "DueDt");

                inv.Qty = DaHelper.GetDecimal(dr, "Qty");
                inv.AmountExVat = DaHelper.GetDecimal(dr, "ExVatAmount");
                inv.VatAmount = DaHelper.GetDecimal(dr, "VatAmount");
                inv.GAmount = DaHelper.GetDecimal(dr, "GAmount");

                inv.PaidQty = DaHelper.GetDecimal(dr, "PaidQty");
                inv.PaidVatAmount = DaHelper.GetDecimal(dr, "PaidVatAmount");
                inv.PaidGAmount = DaHelper.GetDecimal(dr, "PaidGAmount");

                inv.ToPayQty = inv.ToBePaidQty;
                inv.ToPayGAmount = inv.ToBePaidGAmount;
                inv.ToPayVatAmount = inv.ToBePaidVatAmount;

                inv.OriginalInvoiceNo = DaHelper.GetString(dr, "OriginalInvoiceNo");
                inv.InstallmentPeriod = DaHelper.GetInt(dr, "InstallmentPeriod");
                inv.InstallmentTotalPeriod = DaHelper.GetInt(dr, "InstallmentTotalPeriod");

                inv.CaDoc = DaHelper.GetString(dr, "CaDoc");
                inv.ControllerId = DaHelper.GetString(dr, "ControllerId");
                inv.ControllerName = DaHelper.GetString(dr, "ControllerName");
                inv.MruId = DaHelper.GetString(dr, "MruId");

                // 20180125 Kanokwan.L แก้ไข Invoiceno = null แล้วระบบแสดง Alert เตือนหน้าบ้านไม่สื่อความหมาย 
                if (inv.InvoiceNo == null || inv.InvoiceNo == "NULL")
                {
                    invoices = null;
                    break;
                }
                else
                {
                    inv.Bills = SearchBillByInvoiceNo(inv.InvoiceNo, chkIsPaid);
                }


                //Tax 13 
                inv.CaTaxId = DaHelper.GetString(dr, "CaTaxId");
                inv.CaTaxBranch = DaHelper.GetString(dr, "CaTaxBranch");

                //Energy Amount
                inv.EnergyAmount = DaHelper.GetDecimal(dr, "EnergyAmount").Value;

                invoices.Add(inv);
            }
            return invoices;
        }

        private List<InstallmentInvoice> ConvertToInstallmentInvoiceList(DataTable dt)
        {
            List<InstallmentInvoice> invoices = new List<InstallmentInvoice>();
            foreach (DataRow dr in dt.Rows)
            {
                InstallmentInvoice inv = new InstallmentInvoice();
                inv.InvoiceNo = DaHelper.GetString(dr, "InvoiceNo");
                inv.CaId = DaHelper.GetString(dr, "CaId");
                inv.GAmount = DaHelper.GetDecimal(dr, "GAmount");
                inv.InstallmentPeriod = DaHelper.GetInt(dr, "InstallmentPeriod");
                inv.InstallmentTotalPeriod = DaHelper.GetInt(dr, "InstallmentTotalPeriod");


                invoices.Add(inv);
            }
            return invoices;
        }

        private static List<Bill> ConvertToBillList(DataTable dt)
        {
            List<Bill> bills = new List<Bill>();
            foreach (DataRow dr in dt.Rows)
            {
                Bill b = new Bill();
                b.ItemId = DaHelper.GetString(dr, "ItemId");
                b.InvoiceNo = DaHelper.GetString(dr, "InvoiceNo");
                b.CustomerId = DaHelper.GetString(dr, "CaId");
                b.BranchId = DaHelper.GetString(dr, "BranchId");
                b.TechBranchName = DaHelper.GetString(dr, "TechBranchName");
                b.CommBranchId = DaHelper.GetString(dr, "CommBranchId");
                b.CommBranchName = DaHelper.GetString(dr, "CommBranchName");
                b.Name = DaHelper.GetString(dr, "CaName");
                b.Address = DaHelper.GetString(dr, "CaAddress");
                b.ContractTypeId = DaHelper.GetString(dr, "CtId");
                b.DebtId = DaHelper.GetString(dr, "DtId");
                b.DebtType = DaHelper.GetString(dr, "DtName");
                b.Description = DaHelper.GetString(dr, "Description");
                b.BillBookId = DaHelper.GetString(dr, "BillBookId");
                b.GroupInvoiceId = DaHelper.GetString(dr, "GroupInvoiceNo");
                b.Period = DaHelper.GetString(dr, "Period");
                b.PaymentMethodId = DaHelper.GetString(dr, "PmId");
                b.PaymentOrderFlag = DaHelper.GetString(dr, "PaymentOrderFlag");
                b.PaymentOrderDt = DaHelper.GetDate(dr, "PaymentOrderDt");
                b.SecurityDeposit = DaHelper.GetDecimal(dr, "SecurityDeposit");
                b.DisConnectDate = DaHelper.GetDate(dr, "DisconnectDt");
                b.DisconnectDocNo = DaHelper.GetString(dr, "DisconnectDoc");
                b.CancelFlag = DaHelper.GetString(dr, "CancelFlag");
                b.ModifiedFlag = DaHelper.GetString(dr, "ModifiedFlag");

                //Tax 13 
                b.CaTaxId = DaHelper.GetString(dr, "CaTaxId");
                b.CaTaxBranch = DaHelper.GetString(dr, "CaTaxBranch");

                // for electric item
                b.MeterId = DaHelper.GetString(dr, "MeterId");
                b.RateTypeId = DaHelper.GetString(dr, "RateTypeId");
                b.MeterReadDate = DaHelper.GetDate(dr, "MeterReadDt");
                b.PreviousUnit = DaHelper.GetDecimal(dr, "ReadUnit");
                b.LastUnit = DaHelper.GetDecimal(dr, "LastUnit");
                b.FullBaseAmount = DaHelper.GetDecimal(dr, "FullBaseAmount");
                b.FullFtAmount = DaHelper.GetDecimal(dr, "FullFtAmount");
                b.FullQty = DaHelper.GetDecimal(dr, "FullQty");
                b.FullAmount = DaHelper.GetDecimal(dr, "FullAmount");
                b.FullVatAmount = DaHelper.GetDecimal(dr, "FullVatAmount");
                b.FullGAmount = DaHelper.GetDecimal(dr, "FullGAmount");

                b.BaseAmount = DaHelper.GetDecimal(dr, "BaseAmount");
                b.FtUnitPrice = DaHelper.GetDecimal(dr, "FtUnitPrice");
                b.FtAmount = DaHelper.GetDecimal(dr, "FtAmount");
                b.AmountExVat = DaHelper.GetDecimal(dr, "Amount");

                b.UnitPrice = DaHelper.GetDecimal(dr, "UnitPrice");
                b.Qty = DaHelper.GetDecimal(dr, "Qty");
                b.ToPayQty = DaHelper.GetDecimal(dr, "Qty");
                b.UnitTypeId = DaHelper.GetString(dr, "UnitTypeId");
                b.UnitTypeName = DaHelper.GetString(dr, "UnitTypeName");
                b.GAmount = DaHelper.GetDecimal(dr, "GAmount");
                b.ToPayGAmount = DaHelper.GetDecimal(dr, "GAmount") - DaHelper.GetDecimal(dr, "PaidGAmount");
                b.LeftInstallmentAmount = DaHelper.GetDecimal(dr, "LeftInstallmentAmount");

                b.BookCreateDt = DaHelper.GetDate(dr, "BookCreateDt");
                b.BookTotalVatAmount = DaHelper.GetDecimal(dr, "BookTotalVatAmount");
                b.BookTotalGAmount = DaHelper.GetDecimal(dr, "BookTotalAmount");
                b.BookAdvanceAmount = DaHelper.GetDecimal(dr, "BookAdvanceAmount");
                b.BookPaidGAmount = DaHelper.GetDecimal(dr, "BookPaidAmount");
                b.BookTotalBillCollected = DaHelper.GetInt(dr, "BookTotalBillCollected");
                b.BookTotalBill = DaHelper.GetInt(dr, "BookTotalBill");

                b.TaxCode = DaHelper.GetString(dr, "TaxCode");
                b.TaxRate = DaHelper.GetDecimal(dr, "TaxRate");
                b.VatAmount = DaHelper.GetDecimal(dr, "VatAmount");
                b.ToPayVatAmount = DaHelper.GetDecimal(dr, "VatAmount");
                b.ControllerId = DaHelper.GetString(dr, "ControllerId");
                b.AccountClass = DaHelper.GetString(dr, "AccountClassId");
                b.DueDate = DaHelper.GetDate(dr, "DueDt");
                b.DeferralDate = DaHelper.GetDate(dr, "DeferralDt");
                b.OriginalDueDate = DaHelper.GetDate(dr, "OriginalDueDt");

                b.InterestLockFlag = DaHelper.GetString(dr, "InterestLockFlag");
                b.InterestKey = DaHelper.GetString(dr, "InterestKey");
                b.InterestRate = DaHelper.GetDecimal(dr, "InterestRate");
                //TODO: INSTALLMENT CASE
                //b.OriginalDtId = DaHelper.GetString(dr, "OriginalDtId");
                //b.MainCaDoc = DaHelper.GetString(dr, "MainCaDoc");
                //b.OriginalCaDoc = DaHelper.GetString(dr, "OriginalCaDoc");

                b.SubGroupInvoiceNo = DaHelper.GetString(dr, "SubGroupInvoiceNo");
                //b.EnergyAmount = DaHelper.GetDecimal(dr, "EnergyAmount").Value;

                b.DiscStatusId = DaHelper.GetString(dr, "DiscStatusId");
                b.IsServiceEndOfTheYear = DaHelper.GetString(dr, "IsServiceEndOfTheYear");
                b.IsExpenseDuringTheYear = DaHelper.GetString(dr, "IsExpenseDuringTheYear");
                b.BaseGroupTotalAmount = DaHelper.GetDecimal(dr, "BaseGroupTotalAmount");


                bills.Add(b);
            }
            return bills;
        }

        private List<BillSearchDetail> ConvertToBillSearchDetailList(DataTable dt)
        {
            List<BillSearchDetail> bills = new List<BillSearchDetail>();
            foreach (DataRow dr in dt.Rows)
            {
                BillSearchDetail b = new BillSearchDetail();
                b.CustomerId = DaHelper.GetString(dr, "CaId");
                b.Name = DaHelper.GetString(dr, "CaName");
                b.Address = DaHelper.GetString(dr, "CaAddress");
                b.ToPayAmount = DaHelper.GetDecimal(dr, "ToPayAmount");
                bills.Add(b);
            }

            return bills;
        }

        private List<BookSearchDetail> ConvertToBookSearchDetailList(DataTable dt)
        {
            List<BookSearchDetail> bills = new List<BookSearchDetail>();
            foreach (DataRow dr in dt.Rows)
            {
                BookSearchDetail b = new BookSearchDetail();
                b.BillBookId = DaHelper.GetString(dr, "BillBookId");
                b.TotalBill = DaHelper.GetInt(dr, "TotalBillCollected");
                b.CustomerId = DaHelper.GetString(dr, "CaId");
                b.CustomerName = DaHelper.GetString(dr, "CaName");
                b.ToPayAmount = DaHelper.GetDecimal(dr, "ToPayAmount");
                bills.Add(b);
            }

            return bills;
        }

        #endregion

        #region Insert data from SAP's RFC

        public void InsertIntoBusinessPartner(List<BusinessPartnerInfo> list, DbTransaction trans)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            foreach (BusinessPartnerInfo obj in list)
            {
                DbCommand cmd = db.GetStoredProcCommand("ta_rfc_BusinessPartner");
                cmd.CommandTimeout = timeout;
                db.AddInParameter(cmd, "BpId", DbType.String, obj.BpId);
                db.AddInParameter(cmd, "BpTypeId", DbType.String, obj.BpTypeId);
                db.AddInParameter(cmd, "TaxCode", DbType.String, obj.TaxCode);
                db.AddInParameter(cmd, "CitizenId", DbType.String, obj.CitizenId);
                db.AddInParameter(cmd, "PassportNo", DbType.String, obj.PassportNo);
                db.AddInParameter(cmd, "RegisterId", DbType.String, obj.RegisterId);
                db.AddInParameter(cmd, "VatCode", DbType.String, obj.VatCode);
                //db.AddInParameter(cmd, "SyncFlag", DbType.String, "0");
                //db.AddInParameter(cmd, "ModifiedDt", DbType.DateTime, obj.ModifiedDt);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, obj.ModifiedBy);
                db.AddInParameter(cmd, "Action", DbType.String, obj.Action);
                db.AddInParameter(cmd, "FileType", DbType.String, obj.FileType);
                db.AddInParameter(cmd, "ExitOnDoubleInsert", DbType.String, "0");
                db.ExecuteNonQuery(cmd, trans);
            }

        }

        public void InsertIntoContractAccount(List<ContractAccountInfo> list, DbTransaction trans)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            foreach (ContractAccountInfo obj in list)
            {
                DbCommand cmd = db.GetStoredProcCommand("ta_rfc_ContractAccount");
                cmd.CommandTimeout = timeout;
                db.AddInParameter(cmd, "CaId", DbType.String, obj.CaId);
                db.AddInParameter(cmd, "BpId", DbType.String, obj.BpId);
                db.AddInParameter(cmd, "TechBranchId", DbType.String, obj.TechBranchId);
                db.AddInParameter(cmd, "CommBranchId", DbType.String, obj.CommBranchId);
                db.AddInParameter(cmd, "MruId", DbType.String, obj.MruId);
                db.AddInParameter(cmd, "CaName", DbType.String, obj.CaName);
                db.AddInParameter(cmd, "CaAddress", DbType.String, obj.CaAddress);
                db.AddInParameter(cmd, "CtId", DbType.String, obj.CtId);
                db.AddInParameter(cmd, "PmId", DbType.String, obj.PmId);
                db.AddInParameter(cmd, "AccountClassId", DbType.String, obj.AccountClassId);
                db.AddInParameter(cmd, "SecurityDeposit", DbType.Decimal, obj.SecurityDeposit);
                db.AddInParameter(cmd, "MeterInstallDt", DbType.DateTime, obj.MeterInstallDt);
                db.AddInParameter(cmd, "MeterSizeId", DbType.String, obj.MeterSizeId);
                db.AddInParameter(cmd, "CollectArea", DbType.String, obj.CollectArea);
                db.AddInParameter(cmd, "PaidBy", DbType.String, obj.PaidBy);
                db.AddInParameter(cmd, "TransportHelp", DbType.Decimal, obj.TransportHelp);
                db.AddInParameter(cmd, "PenaltyWaiveFlag", DbType.String, obj.PenaltyWaiveFlag);
                //db.AddInParameter(cmd, "FarLandHelp", DbType.Decimal, obj.FarLandHelp);
                //db.AddInParameter(cmd, "ExtraMoneyHelp", DbType.Decimal, obj.ExtraMoneyHelp);
                //db.AddInParameter(cmd, "SignContractDt", DbType.DateTime, obj.SignContractDt);
                db.AddInParameter(cmd, "ContractValidFrom", DbType.DateTime, obj.ContractValidFrom);
                db.AddInParameter(cmd, "ContractValidTo", DbType.DateTime, obj.ContractValidTo);
                db.AddInParameter(cmd, "GroupIvReceiptType", DbType.String, obj.ReceiptType);
                db.AddInParameter(cmd, "ReceiptPrintName", DbType.String, obj.ReceiptPrintName);
                db.AddInParameter(cmd, "InterestKey", DbType.String, obj.InterestKey);
                db.AddInParameter(cmd, "ControllerId", DbType.String, obj.ControllerId);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, obj.ModifiedBy);
                db.AddInParameter(cmd, "Action", DbType.String, obj.Action);
                db.AddInParameter(cmd, "ExitOnDoubleInsert", DbType.String, "0");
                db.AddInParameter(cmd, "FileType", DbType.String, obj.FileType);

                //Tax13
                db.AddInParameter(cmd, "CaTaxId", DbType.String, obj.CaTaxId);
                db.AddInParameter(cmd, "CaTaxBranch", DbType.String, obj.CaTaxBranch);

                db.ExecuteNonQuery(cmd, trans);
            }

        }

        public void InsertIntoAR(List<ARInfo> list, DbTransaction trans, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            foreach (ARInfo obj in list)
            {
                DbCommand cmd = db.GetStoredProcCommand("ta_rfc_AR");
                cmd.CommandTimeout = timeout;
                db.AddInParameter(cmd, "ItemId", DbType.String, obj.ItemId);
                db.AddInParameter(cmd, "CaId", DbType.String, obj.CaId);
                db.AddInParameter(cmd, "DtId", DbType.String, obj.DtId);
                db.AddInParameter(cmd, "Description", DbType.String, obj.Description);
                db.AddInParameter(cmd, "InvoiceNo", DbType.String, obj.InvoiceNo);
                db.AddInParameter(cmd, "InvoiceDt", DbType.DateTime, obj.InvoiceDt);
                db.AddInParameter(cmd, "GroupInvoiceNo", DbType.String, obj.GroupInvoiceNo);
                db.AddInParameter(cmd, "Period", DbType.String, obj.Period);
                db.AddInParameter(cmd, "MeterId", DbType.String, obj.MeterId);
                db.AddInParameter(cmd, "RateTypeId", DbType.String, obj.RateTypeId);
                db.AddInParameter(cmd, "MeterReadDt", DbType.DateTime, obj.MeterReadDt);
                db.AddInParameter(cmd, "ReadUnit", DbType.Decimal, obj.ReadUnit);
                db.AddInParameter(cmd, "LastUnit", DbType.Decimal, obj.LastUnit);
                db.AddInParameter(cmd, "BaseAmount", DbType.Decimal, obj.BaseAmount);
                db.AddInParameter(cmd, "FTUnitPrice", DbType.Decimal, obj.FTUnitPrice);
                db.AddInParameter(cmd, "FTAmount", DbType.Decimal, obj.FTAmount);
                db.AddInParameter(cmd, "UnitPrice", DbType.Decimal, obj.UnitPrice);
                db.AddInParameter(cmd, "Qty", DbType.Decimal, obj.Qty);
                db.AddInParameter(cmd, "Amount", DbType.Decimal, obj.Amount);
                db.AddInParameter(cmd, "UnitTypeId", DbType.String, obj.UnitTypeId);
                db.AddInParameter(cmd, "TaxCode", DbType.String, obj.TaxCode);
                db.AddInParameter(cmd, "VatAmount", DbType.Decimal, obj.VatAmount);
                db.AddInParameter(cmd, "GAmount", DbType.Decimal, obj.GAmount);
                db.AddInParameter(cmd, "DueDt", DbType.DateTime, obj.DueDt);
                db.AddInParameter(cmd, "DueDt2", DbType.DateTime, obj.DueDt2);
                db.AddInParameter(cmd, "InterestLockFlag", DbType.String, obj.InterestLockFlag);
                db.AddInParameter(cmd, "InterestKey", DbType.String, obj.InterestKey);
                db.AddInParameter(cmd, "DisconnectDt", DbType.DateTime, obj.DisconnectDt);//CutOfDt
                db.AddInParameter(cmd, "DisconnectDoc", DbType.String, obj.DisconnectDoc);
                db.AddInParameter(cmd, "SubstDocNo", DbType.String, obj.SubstDocNo);

                if (obj.FileType == "E" || obj.FileType == "F")
                    db.AddInParameter(cmd, "InstallmentFlag", DbType.String, "1");
                else
                    db.AddInParameter(cmd, "InstallmentFlag", DbType.String, "0");

                db.AddInParameter(cmd, "InstallmentPeriod", DbType.Int32, obj.InstallmentPeriod);
                db.AddInParameter(cmd, "InstallmentTotalPeriod", DbType.Int32, obj.InstallmentTotalPeriod);
                db.AddInParameter(cmd, "SpotBillInvoiceNo", DbType.String, obj.SpotBillInvoiceNo);
                db.AddInParameter(cmd, "CancelFlag", DbType.String, obj.CancelFlag);
                db.AddInParameter(cmd, "PaymentOrderFlag", DbType.String, obj.PaymentOrderFlag);
                db.AddInParameter(cmd, "PaymentOrderDt", DbType.DateTime, obj.PaymentOrderDt);
                db.AddInParameter(cmd, "PostBranchServerId", DbType.String, branchId);
                db.AddInParameter(cmd, "ModifiedFlag", DbType.String, obj.ModifiedFlag);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, obj.ModifiedBy);
                db.AddInParameter(cmd, "Action", DbType.String, obj.Action);
                db.AddInParameter(cmd, "FileType", DbType.String, obj.FileType);
                db.ExecuteNonQuery(cmd, trans);
            }
        }

        public void InsertIntoPayFromSAP(List<PayFromSAPInfo> list, DbTransaction trans, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            foreach (PayFromSAPInfo obj in list)
            {
                DbCommand cmd = db.GetStoredProcCommand("ta_rfc_PayFromSAP");
                cmd.CommandTimeout = timeout;
                db.AddInParameter(cmd, "PaymentDocNo", DbType.String, obj.PaymentDocNo);
                db.AddInParameter(cmd, "ReceiptNo", DbType.String, obj.ReceiptNo);
                db.AddInParameter(cmd, "InvoiceNo", DbType.String, obj.InvoiceNo);
                db.AddInParameter(cmd, "CaDoc", DbType.String, obj.CaDoc);
                db.AddInParameter(cmd, "PaymentDt", DbType.DateTime, obj.PaymentDt);
                db.AddInParameter(cmd, "PmId", DbType.String, obj.PmId);
                db.AddInParameter(cmd, "DocType", DbType.String, obj.DocType);
                db.AddInParameter(cmd, "VatAmount", DbType.Decimal, obj.VatAmount);
                db.AddInParameter(cmd, "Amount", DbType.Decimal, obj.Amount);
                db.AddInParameter(cmd, "PostBranchServerId", DbType.String, branchId);
                db.AddInParameter(cmd, "CancelFlag", DbType.String, obj.CancelFlag);
                db.AddInParameter(cmd, "Action", DbType.String, obj.Action);
                db.AddInParameter(cmd, "ExitOnDoubleInsert", DbType.String, "0");
                db.AddInParameter(cmd, "ARPmId_RunningNo", DbType.String, obj.ARPmId);
                db.AddInParameter(cmd, "ARPtId_RunningNo", DbType.String, obj.ARPtId);
                db.AddInParameter(cmd, "ReceiptId_RunningNo", DbType.String, obj.ReceiptId);
                db.AddInParameter(cmd, "TotalAmount", DbType.Decimal, obj.TotalAmount);
                db.AddInParameter(cmd, "DueDt", DbType.DateTime, obj.DueDt);
                db.ExecuteNonQuery(cmd, trans);
            }

        }

        public void InsertIntoDisconnectionDoc(List<DisconnectionDoc> list, DbTransaction trans)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            foreach (DisconnectionDoc obj in list)
            {
                DbCommand cmd = db.GetStoredProcCommand("ta_rfc_DisconnectionDoc");
                cmd.CommandTimeout = timeout;
                db.AddInParameter(cmd, "DiscNo", DbType.String, obj.DiscNo);
                db.AddInParameter(cmd, "CreatedDt", DbType.DateTime, obj.CreatedDt);
                db.AddInParameter(cmd, "DiscStatusId", DbType.String, obj.DiscStatusId);
                db.AddInParameter(cmd, "ReleaseDt", DbType.DateTime, obj.ReleaseDt);
                db.AddInParameter(cmd, "WorkOrderNo", DbType.String, obj.WorkOrderNo);
                db.AddInParameter(cmd, "DiscPlanStart", DbType.DateTime, obj.DiscPlanStart);
                db.AddInParameter(cmd, "DiscPlanEnd", DbType.DateTime, obj.DiscPlanEnd);
                db.AddInParameter(cmd, "WorkCenter", DbType.String, obj.WorkCenter);
                db.AddInParameter(cmd, "PostpConfirm", DbType.DateTime, obj.PostpConfirm);
                db.AddInParameter(cmd, "FuseRemConfirm", DbType.DateTime, obj.FuseRemConfirm);
                db.AddInParameter(cmd, "MeterRemConfirm", DbType.DateTime, obj.MeterRemConfirm);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, obj.ModifiedBy);
                db.AddInParameter(cmd, "Action", DbType.String, obj.Action);
                db.AddInParameter(cmd, "ExitOnDoubleInsert", DbType.String, "0");
                db.ExecuteNonQuery(cmd, trans);
            }

        }

        public void InsertIntoRTDisconnectionDocCaDoc(List<RTDisconnectionDocCaDoc> list, DbTransaction trans)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            foreach (RTDisconnectionDocCaDoc obj in list)
            {
                DbCommand cmd = db.GetStoredProcCommand("ta_rfc_RTDisconnectionDocCaDoc");
                cmd.CommandTimeout = timeout;
                db.AddInParameter(cmd, "DiscNo", DbType.String, obj.DiscNo);
                db.AddInParameter(cmd, "CaDocNo", DbType.String, obj.CaDocNo);
                db.AddInParameter(cmd, "CancelFlag", DbType.String, obj.CancelFlag);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, obj.ModifiedBy);
                db.AddInParameter(cmd, "Action", DbType.String, obj.Action);
                db.AddInParameter(cmd, "ExitOnDoubleInsert", DbType.String, "0");
                db.ExecuteNonQuery(cmd, trans);
            }

        }

        #endregion

        #region Insert data for POS Real Time

        public void InsertIntoBusinessPartnerForRealTime(List<BusinessPartnerInfo> list, DbTransaction trans)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            foreach (BusinessPartnerInfo obj in list)
            {
                DbCommand cmd = db.GetStoredProcCommand("ta_RealTime_ins_BusinessPartner");
                cmd.CommandTimeout = timeout;
                db.AddInParameter(cmd, "BpId", DbType.String, obj.BpId);
                db.AddInParameter(cmd, "BpTypeId", DbType.String, obj.BpTypeId);
                db.AddInParameter(cmd, "TaxCode", DbType.String, obj.TaxCode);
                db.AddInParameter(cmd, "CitizenId", DbType.String, obj.CitizenId);
                db.AddInParameter(cmd, "PassportNo", DbType.String, obj.PassportNo);
                db.AddInParameter(cmd, "RegisterId", DbType.String, obj.RegisterId);
                db.AddInParameter(cmd, "VatCode", DbType.String, obj.VatCode);
                db.AddInParameter(cmd, "SyncFlag", DbType.String, obj.SyncFlag);
                db.AddInParameter(cmd, "ModifiedDt", DbType.DateTime, obj.ModifiedDt);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, obj.ModifiedBy);
                db.AddInParameter(cmd, "Active", DbType.String, obj.Active == false ? "0" : "1");
                db.ExecuteNonQuery(cmd, trans);
            }
        }

        public void InsertIntoContractAccountForRealTime(List<ContractAccountInfo> list, DbTransaction trans)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            foreach (ContractAccountInfo obj in list)
            {
                DbCommand cmd = db.GetStoredProcCommand("ta_RealTime_ins_ContractAccount");
                cmd.CommandTimeout = timeout;
                db.AddInParameter(cmd, "CaId", DbType.String, obj.CaId);
                db.AddInParameter(cmd, "TechBranchId", DbType.String, obj.TechBranchId);
                db.AddInParameter(cmd, "CommBranchId", DbType.String, obj.CommBranchId);
                db.AddInParameter(cmd, "MruId", DbType.String, obj.MruId);
                db.AddInParameter(cmd, "BpId", DbType.String, obj.BpId);
                db.AddInParameter(cmd, "CaName", DbType.String, obj.CaName);
                db.AddInParameter(cmd, "ReceiptPrintName", DbType.String, obj.ReceiptPrintName);
                db.AddInParameter(cmd, "CaAddress", DbType.String, obj.CaAddress);
                db.AddInParameter(cmd, "CtId", DbType.String, obj.CtId);
                db.AddInParameter(cmd, "PmId", DbType.String, obj.PmId);
                db.AddInParameter(cmd, "AccountClassId", DbType.String, obj.AccountClassId);
                db.AddInParameter(cmd, "SecurityDeposit", DbType.Decimal, obj.SecurityDeposit);
                db.AddInParameter(cmd, "InterestKey", DbType.String, obj.InterestKey);
                db.AddInParameter(cmd, "PaidBy", DbType.String, obj.PaidBy);
                db.AddInParameter(cmd, "ContractValidFrom", DbType.DateTime, obj.ContractValidFrom);
                db.AddInParameter(cmd, "ContractValidTo", DbType.DateTime, obj.ContractValidTo);
                db.AddInParameter(cmd, "MeterSizeId", DbType.String, obj.MeterSizeId);
                db.AddInParameter(cmd, "MeterInstallDt", DbType.DateTime, obj.MeterInstallDt);
                db.AddInParameter(cmd, "ControllerId", DbType.String, obj.ControllerId);
                db.AddInParameter(cmd, "GroupIvReceiptType", DbType.String, obj.ReceiptType);
                db.AddInParameter(cmd, "TransportHelp", DbType.Decimal, obj.TransportHelp);
                db.AddInParameter(cmd, "PenaltyWaiveFlag", DbType.String, obj.PenaltyWaiveFlag);
                db.AddInParameter(cmd, "SyncFlag", DbType.String, obj.SyncFlag);
                db.AddInParameter(cmd, "ModifiedDt", DbType.DateTime, obj.ModifiedDt);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, obj.ModifiedBy);
                db.AddInParameter(cmd, "Active", DbType.String, obj.Active == false ? "0" : "1");
                db.ExecuteNonQuery(cmd, trans);
            }

        }

        public void InsertIntoARForRealTime(List<ARInfo> list, DbTransaction trans)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            foreach (ARInfo obj in list)
            {
                DbCommand cmd = db.GetStoredProcCommand("ta_RealTime_ins_AR");
                cmd.CommandTimeout = timeout;
                db.AddInParameter(cmd, "ItemId", DbType.String, obj.ItemId);
                db.AddInParameter(cmd, "CaDoc", DbType.String, obj.CaDoc);
                db.AddInParameter(cmd, "CaId", DbType.String, obj.CaId);
                db.AddInParameter(cmd, "DtId", DbType.String, obj.DtId);
                db.AddInParameter(cmd, "Description", DbType.String, obj.Description);
                db.AddInParameter(cmd, "InvoiceNo", DbType.String, obj.InvoiceNo);
                db.AddInParameter(cmd, "InvoiceDt", DbType.DateTime, obj.InvoiceDt);
                db.AddInParameter(cmd, "GroupInvoiceNo", DbType.String, obj.GroupInvoiceNo);
                db.AddInParameter(cmd, "BillBookId", DbType.String, obj.BillBookId);
                db.AddInParameter(cmd, "Period", DbType.String, obj.Period);
                db.AddInParameter(cmd, "MeterId", DbType.String, obj.MeterId);
                db.AddInParameter(cmd, "RateTypeId", DbType.String, obj.RateTypeId);
                db.AddInParameter(cmd, "MeterReadDt", DbType.DateTime, obj.MeterReadDt);
                db.AddInParameter(cmd, "ReadUnit", DbType.Decimal, obj.ReadUnit);
                db.AddInParameter(cmd, "LastUnit", DbType.Decimal, obj.LastUnit);
                db.AddInParameter(cmd, "BaseAmount", DbType.Decimal, obj.BaseAmount);
                db.AddInParameter(cmd, "FTUnitPrice", DbType.Decimal, obj.FTUnitPrice);
                db.AddInParameter(cmd, "FTAmount", DbType.Decimal, obj.FTAmount);
                db.AddInParameter(cmd, "UnitPrice", DbType.Decimal, obj.UnitPrice);
                db.AddInParameter(cmd, "Qty", DbType.Decimal, obj.Qty);
                db.AddInParameter(cmd, "UnitTypeId", DbType.String, obj.UnitTypeId);
                db.AddInParameter(cmd, "Amount", DbType.Decimal, obj.Amount);
                db.AddInParameter(cmd, "TaxCode", DbType.String, obj.TaxCode);
                db.AddInParameter(cmd, "VatAmount", DbType.Decimal, obj.VatAmount);
                db.AddInParameter(cmd, "GAmount", DbType.Decimal, obj.GAmount);
                db.AddInParameter(cmd, "DueDt", DbType.DateTime, obj.DueDt);
                db.AddInParameter(cmd, "DueDt2", DbType.DateTime, obj.DueDt2);
                db.AddInParameter(cmd, "PaidQty", DbType.Decimal, obj.PaidQty);
                db.AddInParameter(cmd, "PaidVatAmount", DbType.Decimal, obj.PaidVatAmount);
                db.AddInParameter(cmd, "PaidGAmount", DbType.Decimal, obj.PaidGAmount);
                db.AddInParameter(cmd, "DisconnectDt", DbType.DateTime, obj.DisconnectDt);//CutOfDt
                db.AddInParameter(cmd, "DisconnectDoc", DbType.String, obj.DisconnectDoc);
                db.AddInParameter(cmd, "SubstDocNo", DbType.String, obj.SubstDocNo);
                db.AddInParameter(cmd, "OriginalInvoiceNo", DbType.String, obj.OriginalInvoiceNo);
                db.AddInParameter(cmd, "SpotBillInvoiceNo", DbType.String, obj.SpotBillInvoiceNo);
                db.AddInParameter(cmd, "InterestLockFlag", DbType.String, obj.InterestLockFlag);
                db.AddInParameter(cmd, "InterestKey", DbType.String, obj.InterestKey);
                db.AddInParameter(cmd, "InstallmentFlag", DbType.String, obj.InstallmentFlag);
                db.AddInParameter(cmd, "InstallmentPeriod", DbType.Int32, obj.InstallmentPeriod);
                db.AddInParameter(cmd, "InstallmentTotalPeriod", DbType.Int32, obj.InstallmentTotalPeriod);
                db.AddInParameter(cmd, "PaymentOrderFlag", DbType.String, obj.PaymentOrderFlag);
                db.AddInParameter(cmd, "PaymentOrderDt", DbType.DateTime, obj.PaymentOrderDt);
                //--POS2--START
                db.AddInParameter(cmd, "CheckInRefNo", DbType.String, obj.CheckInRefNo);
                //--POS2--END
                db.AddInParameter(cmd, "CancelFlag", DbType.String, obj.CancelFlag);
                db.AddInParameter(cmd, "ModifiedFlag", DbType.String, obj.ModifiedFlag);
                db.AddInParameter(cmd, "POSDebtFlag", DbType.String, obj.POSDebtFlag);
                db.AddInParameter(cmd, "SyncFlag", DbType.String, obj.SyncFlag);
                db.AddInParameter(cmd, "PostBranchServerId", DbType.String, obj.PostBranchServerId);
                db.AddInParameter(cmd, "PostDt", DbType.DateTime, obj.PostDt);
                db.AddInParameter(cmd, "ModifiedDt", DbType.DateTime, obj.ModifiedDt);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, obj.ModifiedBy);
                db.AddInParameter(cmd, "Active", DbType.String, obj.Active == false ? "0" : "1");
                db.AddInParameter(cmd, "CreatedDt", DbType.DateTime, obj.CreatedDt);
                db.AddInParameter(cmd, "FileName", DbType.String, obj.FileName);
                db.ExecuteNonQuery(cmd, trans);
            }
        }

        public void InsertIntoARPaymentForRealTime(List<ARPaymentInfo> list, DbTransaction trans)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            foreach (ARPaymentInfo obj in list)
            {
                DbCommand cmd = db.GetStoredProcCommand("ta_RealTime_ins_ARPayment");
                cmd.CommandTimeout = timeout;
                db.AddInParameter(cmd, "ARPmId", DbType.String, obj.ArpmId);
                db.AddInParameter(cmd, "InvoiceNo", DbType.String, obj.InvoiceNo);
                db.AddInParameter(cmd, "PmId", DbType.String, obj.PmId);
                db.AddInParameter(cmd, "DocType", DbType.String, obj.DocType);
                db.AddInParameter(cmd, "Qty", DbType.Decimal, obj.Qty);
                db.AddInParameter(cmd, "VatAmount", DbType.Decimal, obj.VatAmount);
                db.AddInParameter(cmd, "GAmount", DbType.Decimal, obj.GAmount);
                db.AddInParameter(cmd, "AdjAmount", DbType.Decimal, obj.AdjAmount);
                db.AddInParameter(cmd, "CancelARPmId", DbType.String, obj.CancelArpmId);
                db.AddInParameter(cmd, "PaymentDt", DbType.DateTime, obj.PaymentDt);
                db.AddInParameter(cmd, "Pending", DbType.String, obj.Pending);
                //--POS2--START
                db.AddInParameter(cmd, "PaidChannel", DbType.Byte, obj.PaidChannel);
                //--POS2--END
                db.AddInParameter(cmd, "SyncFlag", DbType.String, obj.SyncFlag);
                db.AddInParameter(cmd, "PostDt", DbType.DateTime, obj.PostDt);
                db.AddInParameter(cmd, "PostBranchServerId", DbType.String, obj.PostBranchServerId);
                db.AddInParameter(cmd, "ModifiedDt", DbType.DateTime, obj.ModifiedDt);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, obj.ModifiedBy);
                db.AddInParameter(cmd, "Active", DbType.String, obj.Active == false ? "0" : "1");
                db.ExecuteNonQuery(cmd, trans);
            }
        }

        public void InsertIntoRTARPaymentForRealTime(List<RTARPaymentInfo> list, DbTransaction trans)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            foreach (RTARPaymentInfo obj in list)
            {
                DbCommand cmd = db.GetStoredProcCommand("ta_RealTime_ins_RTARPaymenttypeArpayment");
                cmd.CommandTimeout = timeout;
                db.AddInParameter(cmd, "ARPtId", DbType.String, obj.ARPtId);
                db.AddInParameter(cmd, "ARPmId", DbType.String, obj.ARPmId);
                db.AddInParameter(cmd, "Amount", DbType.Decimal, obj.Amount);
                db.AddInParameter(cmd, "SyncFlag", DbType.String, obj.SyncFlag);
                db.AddInParameter(cmd, "PostDt", DbType.DateTime, obj.PostDt);
                db.AddInParameter(cmd, "PostBranchServerId", DbType.String, obj.PostBranchServerId);
                db.AddInParameter(cmd, "ModifiedDt", DbType.DateTime, obj.ModifiedDt);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, obj.ModifiedBy);
                db.AddInParameter(cmd, "Active", DbType.String, obj.Active == false ? "0" : "1");
                db.ExecuteNonQuery(cmd, trans);
            }
        }


        public void InsertIntoARPaymenttypeForRealTime(List<ARPaymentTypeInfo> list, DbTransaction trans)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            foreach (ARPaymentTypeInfo obj in list)
            {
                DbCommand cmd = db.GetStoredProcCommand("ta_RealTime_ins_ARPaymenttype");
                cmd.CommandTimeout = timeout;
                db.AddInParameter(cmd, "ARPtId", DbType.String, obj.ARPtId);
                db.AddInParameter(cmd, "PaymentId", DbType.String, obj.PaymentId);
                db.AddInParameter(cmd, "Amount", DbType.Decimal, obj.Amount);
                db.AddInParameter(cmd, "PtId", DbType.String, obj.PtId);
                db.AddInParameter(cmd, "ChangeAmount", DbType.Decimal, obj.ChangeAmount);
                db.AddInParameter(cmd, "FeeAmount", DbType.Decimal, obj.FeeAmount);
                db.AddInParameter(cmd, "BankKey", DbType.String, obj.BankId);
                db.AddInParameter(cmd, "ChqNo", DbType.String, obj.ChqNo);
                db.AddInParameter(cmd, "ChqAccNo", DbType.String, obj.ChqAccNo);
                db.AddInParameter(cmd, "ChqDt", DbType.DateTime, obj.ChqDt);
                db.AddInParameter(cmd, "DraftFlag", DbType.String, obj.DraftFlag);
                db.AddInParameter(cmd, "CashierChequeFlag", DbType.String, obj.CashierChequeFlag);
                db.AddInParameter(cmd, "TranfAccNo", DbType.String, obj.TranfAccNo);
                db.AddInParameter(cmd, "TranfDt", DbType.DateTime, obj.TranfDt);
                db.AddInParameter(cmd, "CancelARPtId", DbType.String, obj.CancelARPtId);
                db.AddInParameter(cmd, "SyncFlag", DbType.String, obj.SyncFlag);
                db.AddInParameter(cmd, "PostDt", DbType.DateTime, obj.PostDt);
                db.AddInParameter(cmd, "PostBranchServerId", DbType.String, obj.PostBranchServerId);
                db.AddInParameter(cmd, "ModifiedDt", DbType.DateTime, obj.ModifiedDt);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, obj.ModifiedBy);
                db.AddInParameter(cmd, "Active", DbType.String, obj.Active == false ? "0" : "1");
                db.ExecuteNonQuery(cmd, trans);
            }
        }

        public void InsertIntoPaymentForRealTime(List<PaymentInfo> list, DbTransaction trans)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            foreach (PaymentInfo obj in list)
            {
                DbCommand cmd = db.GetStoredProcCommand("ta_RealTime_ins_Payment");
                cmd.CommandTimeout = timeout;
                db.AddInParameter(cmd, "PaymentId", DbType.String, obj.PaymentId);
                db.AddInParameter(cmd, "PaymentDt", DbType.DateTime, obj.PaymentDt);
                db.AddInParameter(cmd, "PosId", DbType.String, obj.PosId);
                db.AddInParameter(cmd, "CashierId", DbType.String, obj.CashierId);
                db.AddInParameter(cmd, "BranchId", DbType.String, obj.BranchId);
                db.AddInParameter(cmd, "OriginalPaymentId", DbType.String, obj.OriginalPaymentId);
                //--POS2--START
                db.AddInParameter(cmd, "PaidChannel", DbType.Byte, obj.PaidChannel);
                db.AddInParameter(cmd, "CmScope", DbType.Byte, obj.CmScope);
                //--POS2--END
                db.AddInParameter(cmd, "WorkId", DbType.String, obj.WorkId);
                db.AddInParameter(cmd, "WorkFlag", DbType.Byte, obj.WorkFlag);
                db.AddInParameter(cmd, "SyncFlag", DbType.String, obj.SyncFlag);
                db.AddInParameter(cmd, "PostDt", DbType.DateTime, obj.PostDt);
                db.AddInParameter(cmd, "PostBranchServerId", DbType.String, obj.PostBranchServerId);
                db.AddInParameter(cmd, "ModifiedDt", DbType.DateTime, obj.ModifiedDt);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, obj.ModifiedBy);
                db.AddInParameter(cmd, "Active", DbType.String, obj.Active == false ? "0" : "1");
                db.ExecuteNonQuery(cmd, trans);
            }
        }

        public void InsertIntoReceiptItemForRealTime(List<ReceiptItemInfo> list, DbTransaction trans)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            foreach (ReceiptItemInfo obj in list)
            {
                DbCommand cmd = db.GetStoredProcCommand("ta_RealTime_ins_ReceiptItem");
                cmd.CommandTimeout = timeout;
                db.AddInParameter(cmd, "ReceiptId", DbType.String, obj.ReceiptId);
                db.AddInParameter(cmd, "ARPmId", DbType.String, obj.ARPmId);
                db.AddInParameter(cmd, "SyncFlag", DbType.String, obj.SyncFlag);
                db.AddInParameter(cmd, "PostDt", DbType.DateTime, obj.PostDt);
                db.AddInParameter(cmd, "PostBranchServerId", DbType.String, obj.PostBranchServerId);
                db.AddInParameter(cmd, "ModifiedDt", DbType.DateTime, obj.ModifiedDt);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, obj.ModifiedBy);
                db.AddInParameter(cmd, "Active", DbType.String, obj.Active == false ? "0" : "1");
                db.ExecuteNonQuery(cmd, trans);
            }
        }

        public void InsertIntoReceiptForRealTime(List<ReceiptInfo> list, DbTransaction trans)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            foreach (ReceiptInfo obj in list)
            {
                DbCommand cmd = db.GetStoredProcCommand("ta_RealTime_ins_Receipt");
                cmd.CommandTimeout = timeout;
                db.AddInParameter(cmd, "ReceiptId", DbType.String, obj.ReceiptId);
                db.AddInParameter(cmd, "PaymentId", DbType.String, obj.PaymentId);
                db.AddInParameter(cmd, "PrintingSequence", DbType.Int32, obj.PrintingSequence);
                db.AddInParameter(cmd, "TotalReceipt", DbType.Int32, obj.TotalReceipt);
                db.AddInParameter(cmd, "CaId", DbType.String, obj.CaId);
                db.AddInParameter(cmd, "Name", DbType.String, obj.Name);
                db.AddInParameter(cmd, "Address", DbType.String, obj.Address);
                db.AddInParameter(cmd, "IsNameAddrModified", DbType.String, obj.IsNameAddrModified);
                db.AddInParameter(cmd, "NoOfPrinting", DbType.Int32, obj.NoOfPrinting);
                db.AddInParameter(cmd, "CancelDt", DbType.DateTime, obj.CancelDt);
                db.AddInParameter(cmd, "CancelReason", DbType.String, obj.CancelReason);
                db.AddInParameter(cmd, "ReceiptType", DbType.String, obj.ReceiptType);
                db.AddInParameter(cmd, "ExtReceiptId", DbType.String, obj.ExtReceiptId);
                db.AddInParameter(cmd, "ExtReceiptDt", DbType.DateTime, obj.ExtReceiptDt);
                //--POS2--START
                db.AddInParameter(cmd, "CaDoc", DbType.String, obj.CaDoc);
                db.AddInParameter(cmd, "Description", DbType.String, obj.Description);
                db.AddInParameter(cmd, "InvoiceNo", DbType.String, obj.InvoiceNo);
                db.AddInParameter(cmd, "InvoiceDt", DbType.DateTime, obj.InvoiceDt);
                db.AddInParameter(cmd, "OriginalInvoiceNo", DbType.String, obj.OriginalInvoiceNo);
                db.AddInParameter(cmd, "OriginalInvoiceDt", DbType.DateTime, obj.OriginalInvoiceDt);
                db.AddInParameter(cmd, "InstallmentPeriod", DbType.Int32, obj.InstallmentPeriod);
                db.AddInParameter(cmd, "InstallmentTotalPeriod", DbType.Int32, obj.InstallmentTotalPeriod);
                db.AddInParameter(cmd, "MeterId", DbType.String, obj.MeterId);
                db.AddInParameter(cmd, "RateTypeId", DbType.String, obj.RateTypeId);
                db.AddInParameter(cmd, "BranchId", DbType.String, obj.BranchId);
                db.AddInParameter(cmd, "Period", DbType.String, obj.Period);
                db.AddInParameter(cmd, "GroupInvoiceNo", DbType.String, obj.GroupInvoiceNo);
                db.AddInParameter(cmd, "BillBookId", DbType.String, obj.BillBookId);
                db.AddInParameter(cmd, "SpotBillInvoiceNo", DbType.String, obj.SpotBillInvoiceNo);
                db.AddInParameter(cmd, "MeterReadDt", DbType.DateTime, obj.MeterReadDt);
                db.AddInParameter(cmd, "ReadUnit", DbType.Decimal, obj.ReadUnit);
                db.AddInParameter(cmd, "LastUnit", DbType.Decimal, obj.LastUnit);
                db.AddInParameter(cmd, "FullBaseAmount", DbType.Decimal, obj.FullBaseAmount);
                db.AddInParameter(cmd, "FullFTAmount", DbType.Decimal, obj.FullFTAmount);
                db.AddInParameter(cmd, "FullQty", DbType.Decimal, obj.FullQty);
                db.AddInParameter(cmd, "FullAmount", DbType.Decimal, obj.FullAmount);
                db.AddInParameter(cmd, "FullVatAmount", DbType.Decimal, obj.FullVatAmount);
                db.AddInParameter(cmd, "FullGAmount", DbType.Decimal, obj.FullGAmount);
                db.AddInParameter(cmd, "BaseAmount", DbType.Decimal, obj.BaseAmount);
                db.AddInParameter(cmd, "FTUnitPrice", DbType.Decimal, obj.FTUnitPrice);
                db.AddInParameter(cmd, "FTAmount", DbType.Decimal, obj.FTAmount);
                db.AddInParameter(cmd, "UnitPrice", DbType.Decimal, obj.UnitPrice);
                db.AddInParameter(cmd, "Qty", DbType.Decimal, obj.Qty);
                db.AddInParameter(cmd, "Amount", DbType.Decimal, obj.Amount);
                db.AddInParameter(cmd, "VatAmount", DbType.Decimal, obj.VatAmount);
                db.AddInParameter(cmd, "GAmount", DbType.Decimal, obj.GAmount);
                db.AddInParameter(cmd, "QtyInstallment", DbType.Decimal, obj.QtyInstallment);
                db.AddInParameter(cmd, "AmountInstallment", DbType.Decimal, obj.AmountInstallment);
                db.AddInParameter(cmd, "VatAmountInstallment", DbType.Decimal, obj.VatAmountInstallment);
                db.AddInParameter(cmd, "GAmountInstallment", DbType.Decimal, obj.GAmountInstallment);
                db.AddInParameter(cmd, "DtId", DbType.String, obj.DtId);
                db.AddInParameter(cmd, "DtName", DbType.String, obj.DtName);
                db.AddInParameter(cmd, "ControllerId", DbType.String, obj.ControllerId);
                db.AddInParameter(cmd, "TaxCode", DbType.String, obj.TaxCode);
                db.AddInParameter(cmd, "TaxRate", DbType.Decimal, obj.TaxRate);
                db.AddInParameter(cmd, "PartialPayment", DbType.Byte, obj.PartialPayment);
                db.AddInParameter(cmd, "GroupIvReceiptType", DbType.String, obj.GroupIvReceiptType);
                //--POS2--END
                db.AddInParameter(cmd, "SyncFlag", DbType.String, obj.SyncFlag);
                db.AddInParameter(cmd, "PostDt", DbType.DateTime, obj.PostDt);
                db.AddInParameter(cmd, "PostBranchServerId", DbType.String, obj.PostBranchServerId);
                db.AddInParameter(cmd, "ModifiedDt", DbType.DateTime, obj.ModifiedDt);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, obj.ModifiedBy);
                db.AddInParameter(cmd, "Active", DbType.String, obj.Active == false ? "0" : "1");
                db.ExecuteNonQuery(cmd, trans);
            }
        }

        public void InsertIntoDisconnectionDocForRealTime(List<DisconnectionDoc> list, DbTransaction trans)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            foreach (DisconnectionDoc obj in list)
            {
                DbCommand cmd = db.GetStoredProcCommand("ta_RealTime_ins_DisconnectionDoc");
                cmd.CommandTimeout = timeout;
                db.AddInParameter(cmd, "DiscNo", DbType.String, obj.DiscNo);
                db.AddInParameter(cmd, "CreatedDt", DbType.DateTime, obj.CreatedDt);
                db.AddInParameter(cmd, "DiscStatusId", DbType.String, obj.DiscStatusId);
                db.AddInParameter(cmd, "ReleaseDt", DbType.DateTime, obj.ReleaseDt);
                db.AddInParameter(cmd, "WorkOrderNo", DbType.String, obj.WorkOrderNo);
                db.AddInParameter(cmd, "DiscPlanStart", DbType.DateTime, obj.DiscPlanStart);
                db.AddInParameter(cmd, "DiscPlanEnd", DbType.DateTime, obj.DiscPlanEnd);
                db.AddInParameter(cmd, "WorkCenter", DbType.String, obj.WorkCenter);
                db.AddInParameter(cmd, "PostpConfirm", DbType.DateTime, obj.PostpConfirm);
                db.AddInParameter(cmd, "FuseRemConfirm", DbType.DateTime, obj.FuseRemConfirm);
                db.AddInParameter(cmd, "MeterRemConfirm", DbType.DateTime, obj.MeterRemConfirm);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, obj.ModifiedBy);
                db.AddInParameter(cmd, "ModifiedDt", DbType.DateTime, obj.ModifiedDt);
                db.AddInParameter(cmd, "Active", DbType.String, obj.Active);
                db.ExecuteNonQuery(cmd, trans);
            }
        }

        public void InsertIntoRTDisconnectionDocCaDocForRealTime(List<RTDisconnectionDocCaDoc> list, DbTransaction trans)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            foreach (RTDisconnectionDocCaDoc obj in list)
            {
                DbCommand cmd = db.GetStoredProcCommand("ta_RealTime_ins_RTDisconnectionDocCaDoc");
                cmd.CommandTimeout = timeout;
                db.AddInParameter(cmd, "DiscNo", DbType.String, obj.DiscNo);
                db.AddInParameter(cmd, "CaDocNo", DbType.String, obj.CaDocNo);
                db.AddInParameter(cmd, "CancelFlag", DbType.String, obj.CancelFlag);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, obj.ModifiedBy);
                db.AddInParameter(cmd, "ModifiedDt", DbType.DateTime, obj.ModifiedDt);
                db.AddInParameter(cmd, "Active", DbType.String, obj.Active);
                db.ExecuteNonQuery(cmd, trans);
            }
        }
        #endregion

        #region Receipt Generating Functions

        public List<PaymentNonReceiptInfo> SearchPaymentNonReceipt(PaymentNonReceiptInfo receiptGen)
        {
            List<PaymentNonReceiptInfo> paymentList = new List<PaymentNonReceiptInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_paymentNonReceipt");
            db.AddInParameter(cmd, "CaId", DbType.String, receiptGen.CaId);
            db.AddInParameter(cmd, "BranchId", DbType.String, receiptGen.BranchId);
            db.AddInParameter(cmd, "PaymentDt", DbType.DateTime, receiptGen.PaymentDt);
            db.AddInParameter(cmd, "CashierId", DbType.String, receiptGen.Cashier);
            db.AddInParameter(cmd, "GroupInvoiceNo", DbType.String, receiptGen.GroupInvoiceNo);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow r in dt.Rows)
            {
                PaymentNonReceiptInfo payment = new PaymentNonReceiptInfo();
                payment.CaId = DaHelper.GetString(r, "CaId");
                payment.CaName = DaHelper.GetString(r, "CaName");
                payment.InvoiceNo = DaHelper.GetString(r, "InvoiceNo");
                payment.PmName = DaHelper.GetString(r, "PmName");
                payment.PmId = DaHelper.GetString(r, "PmId");
                payment.ArpmId = DaHelper.GetString(r, "ArpmId");
                payment.PaymentDt = DaHelper.GetDateTime(r, "PaymentDt");
                payment.GAmount = DaHelper.GetDecimal(r, "GAmount").Value;
                payment.Cashier = DaHelper.GetString(r, "Cashier");
                payment.DtId = DaHelper.GetString(r, "DtId").Trim();
                payment.PaperSize = DaHelper.GetString(r, "DefaultPaperSize");
                payment.TaxCode = DaHelper.GetString(r, "TaxCode");
                payment.TaxRate = DaHelper.GetDecimal(r, "TaxRate");
                payment.GroupInvoiceNo = DaHelper.GetString(r, "GroupInvoiceNo");
                paymentList.Add(payment);

            }

            return paymentList;
        }

        public void CreateReceiptData(DbTransaction trans, PaymentNonReceiptInfo paymentGenReceipt)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_ins_MissingNormalReceipt");
            db.AddInParameter(cmd, "PReceiptId", DbType.String, paymentGenReceipt.ReceiptId);
            db.AddInParameter(cmd, "PBranchId", DbType.String, paymentGenReceipt.BranchId);
            db.AddInParameter(cmd, "PCaId", DbType.String, paymentGenReceipt.CaId);
            db.AddInParameter(cmd, "PPaymentDt", DbType.DateTime, paymentGenReceipt.PaymentDt);
            db.AddInParameter(cmd, "PPostBranchId", DbType.String, paymentGenReceipt.PostBranchId);
            db.AddInParameter(cmd, "PModifiedBy", DbType.String, paymentGenReceipt.Cashier);
            db.AddInParameter(cmd, "PReceiptType", DbType.String, paymentGenReceipt.ReceiptType);
            db.AddInParameter(cmd, "PArpmId", DbType.String, paymentGenReceipt.ArpmId);
            db.ExecuteNonQuery(cmd, trans);
        }

        public void CreateGroupInvoiceReceiptData(DbTransaction trans, PaymentNonReceiptInfo paymentGenReceipt)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_ins_MissingGroupInvoiceReceipt");
            db.AddInParameter(cmd, "PReceiptId", DbType.String, paymentGenReceipt.ReceiptId);
            db.AddInParameter(cmd, "PBranchId", DbType.String, paymentGenReceipt.BranchId);
            db.AddInParameter(cmd, "PCaId", DbType.String, paymentGenReceipt.CaId);
            db.AddInParameter(cmd, "PPaymentDt", DbType.DateTime, paymentGenReceipt.PaymentDt);
            db.AddInParameter(cmd, "PPostBranchId", DbType.String, paymentGenReceipt.PostBranchId);
            db.AddInParameter(cmd, "PModifiedBy", DbType.String, paymentGenReceipt.Cashier);
            db.AddInParameter(cmd, "PReceiptType", DbType.String, paymentGenReceipt.ReceiptType);
            db.AddInParameter(cmd, "PArpmId", DbType.String, paymentGenReceipt.ArpmId);
            db.ExecuteNonQuery(cmd, trans);
        }

        public bool GetPaidGAmount(string invoiceNo)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_get_PaidGAmountByInvoiceNo");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "InvoiceNo", DbType.String, invoiceNo);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool GetInActiveBillBook(string invoiceNo)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_get_InActiveBillBook");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "InvoiceNo", DbType.String, invoiceNo);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool GetDuplicateExtReceipt(string receiptId, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_get_DuplicateExtReceipt");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "ReceiptId", DbType.String, receiptId);
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidatePaymentActive(string receiptId, bool isPayment)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_get_ValidatePaymentActive");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "ReceiptId", DbType.String, receiptId);
            db.AddInParameter(cmd, "IsPayment", DbType.Boolean, isPayment);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //rfc 
        public List<ARPaymentInfo> GetARPaymentInfo(List<ARInfo> arList)
        {
            List<ARPaymentInfo> arPmList = new List<ARPaymentInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            foreach (ARInfo ar in arList)
            {
                DbCommand cmd = db.GetStoredProcCommand("pc_sel_CopyARPayment");
                db.AddInParameter(cmd, "InvoiceNo", DbType.String, ar.InvoiceNo);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    ARPaymentInfo data = new ARPaymentInfo();
                    data.ArpmId = DaHelper.GetString(dr, "ARPmId");
                    data.InvoiceNo = DaHelper.GetString(dr, "InvoiceNo");
                    data.PmId = DaHelper.GetString(dr, "PmId");
                    data.DocType = DaHelper.GetString(dr, "DocType");
                    data.Qty = DaHelper.GetDecimal(dr, "Qty");
                    data.VatAmount = DaHelper.GetDecimal(dr, "VatAmount");
                    data.GAmount = DaHelper.GetDecimal(dr, "GAmount");
                    data.AdjAmount = DaHelper.GetDecimal(dr, "AdjAmount");
                    data.CancelArpmId = DaHelper.GetString(dr, "CancelARPmId");
                    data.PaymentDt = DaHelper.GetDate(dr, "PaymentDt");
                    data.Pending = DaHelper.GetString(dr, "Pending");
                    //--POS--START
                    data.PaidChannel = DaHelper.GetByte(dr, "PaidChannel");
                    //--POS--END
                    data.PostDt = DaHelper.GetDate(dr, "PostDt");
                    data.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                    data.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                    data.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                    data.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");
                    data.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                    arPmList.Add(data);
                }
            }

            return arPmList;
        }

        public List<RTARPaymentInfo> GetRTARPaymentInfo(List<ARPaymentInfo> arPmList)
        {
            List<RTARPaymentInfo> rtARPmList = new List<RTARPaymentInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            foreach (ARPaymentInfo arPm in arPmList)
            {
                DbCommand cmd = db.GetStoredProcCommand("pc_sel_CopyRTARPayment");
                db.AddInParameter(cmd, "ARPmId", DbType.String, arPm.ArpmId);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    RTARPaymentInfo data = new RTARPaymentInfo();
                    data.ARPtId = DaHelper.GetString(dr, "ARPtId");
                    data.ARPmId = DaHelper.GetString(dr, "ARPmId");
                    data.Amount = DaHelper.GetDecimal(dr, "Amount");
                    data.PostDt = DaHelper.GetDate(dr, "PostDt");
                    data.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                    data.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                    data.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                    data.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");
                    data.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                    rtARPmList.Add(data);
                }
            }

            return rtARPmList;
        }

        public List<ARPaymentTypeInfo> GetPaymentTypeInfo(List<RTARPaymentInfo> rtARPmList)
        {
            List<ARPaymentTypeInfo> arPaymentTypeList = new List<ARPaymentTypeInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            foreach (RTARPaymentInfo rtPm in rtARPmList)
            {
                DbCommand cmd = db.GetStoredProcCommand("pc_sel_CopyPaymentType");
                db.AddInParameter(cmd, "ARPtId", DbType.String, rtPm.ARPtId);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    ARPaymentTypeInfo data = new ARPaymentTypeInfo();
                    data.ARPtId = DaHelper.GetString(dr, "ARPtId");
                    data.PaymentId = DaHelper.GetString(dr, "PaymentId");
                    data.Amount = DaHelper.GetDecimal(dr, "Amount");
                    data.PtId = DaHelper.GetString(dr, "PtId");
                    data.ChangeAmount = DaHelper.GetDecimal(dr, "ChangeAmount");
                    data.FeeAmount = DaHelper.GetDecimal(dr, "FeeAmount");
                    data.BankId = DaHelper.GetString(dr, "BankKey");
                    data.ChqNo = DaHelper.GetString(dr, "ChqNo");
                    data.ChqAccNo = DaHelper.GetString(dr, "ChqAccNo");
                    data.ChqDt = DaHelper.GetDate(dr, "ChqDt");
                    data.DraftFlag = DaHelper.GetString(dr, "DraftFlag");
                    data.CashierChequeFlag = DaHelper.GetString(dr, "CashierChequeFlag");
                    data.TranfAccNo = DaHelper.GetString(dr, "TranfAccNo");
                    data.TranfDt = DaHelper.GetDate(dr, "TranfDt");
                    data.CancelARPtId = DaHelper.GetString(dr, "CancelARPtId");
                    data.PostDt = DaHelper.GetDate(dr, "PostDt");
                    data.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                    data.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                    data.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                    data.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");
                    data.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                    arPaymentTypeList.Add(data);
                }
            }

            return arPaymentTypeList;
        }

        public List<PaymentInfo> GetPaymentInfo(List<ARPaymentTypeInfo> arPaymentTypeList)
        {
            List<string> ptList = new List<string>();
            List<PaymentInfo> paymentList = new List<PaymentInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            foreach (ARPaymentTypeInfo paymentType in arPaymentTypeList)
            {
                if (ptList.Contains(paymentType.PaymentId)) continue;

                DbCommand cmd = db.GetStoredProcCommand("pc_sel_CopyPayment");
                db.AddInParameter(cmd, "PaymentId", DbType.String, paymentType.PaymentId);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    PaymentInfo data = new PaymentInfo();
                    data.PaymentId = DaHelper.GetString(dr, "PaymentId");
                    data.PaymentDt = DaHelper.GetDate(dr, "PaymentDt");
                    data.PosId = DaHelper.GetString(dr, "PosId");
                    data.CashierId = DaHelper.GetString(dr, "CashierId");
                    data.BranchId = DaHelper.GetString(dr, "BranchId");
                    data.OriginalPaymentId = DaHelper.GetString(dr, "OriginalPaymentId");
                    //--POS2--START
                    data.PaidChannel = DaHelper.GetByte(dr, "PaidChannel");
                    data.CmScope = DaHelper.GetByte(dr, "CmScope");
                    //--POS2--END
                    data.PostDt = DaHelper.GetDate(dr, "PostDt");
                    data.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                    data.WorkId = DaHelper.GetString(dr, "WorkId");
                    data.WorkFlag = DaHelper.GetByte(dr, "WorkFlag");
                    data.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                    data.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                    data.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");
                    data.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                    paymentList.Add(data);
                }

                ptList.Add(paymentType.PaymentId);
            }

            return paymentList;
        }

        public List<ReceiptInfo> GetReceiptInfo(List<PaymentInfo> paymentList)
        {
            List<ReceiptInfo> receiptList = new List<ReceiptInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            foreach (PaymentInfo payment in paymentList)
            {
                DbCommand cmd = db.GetStoredProcCommand("pc_sel_CopyReceipt");
                db.AddInParameter(cmd, "PaymentId", DbType.String, payment.PaymentId);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    ReceiptInfo data = new ReceiptInfo();
                    data.ReceiptId = DaHelper.GetString(dr, "ReceiptId");
                    data.PaymentId = DaHelper.GetString(dr, "PaymentId");
                    data.PrintingSequence = DaHelper.GetShort(dr, "PrintingSequence");
                    data.TotalReceipt = DaHelper.GetShort(dr, "TotalReceipt");
                    data.CaId = DaHelper.GetString(dr, "CaId");
                    data.Name = DaHelper.GetString(dr, "Name");
                    data.Address = DaHelper.GetString(dr, "Address");
                    data.IsNameAddrModified = DaHelper.GetString(dr, "IsNameAddrModified");
                    data.NoOfPrinting = DaHelper.GetInt(dr, "NoOfPrinting");
                    data.CancelDt = DaHelper.GetDate(dr, "CancelDt");
                    data.CancelReason = DaHelper.GetString(dr, "CancelReason");
                    data.ReceiptType = DaHelper.GetString(dr, "ReceiptType");
                    data.ExtReceiptId = DaHelper.GetString(dr, "ExtReceiptId");
                    data.ExtReceiptDt = DaHelper.GetDate(dr, "ExtReceiptDt");

                    //--START--POS2------------------------------
                    data.CaDoc = DaHelper.GetString(dr, "CaDoc");
                    data.Description = DaHelper.GetString(dr, "Description");
                    data.InvoiceNo = DaHelper.GetString(dr, "InvoiceNO");
                    data.InvoiceDt = DaHelper.GetDate(dr, "InvoiceDt");
                    data.OriginalInvoiceNo = DaHelper.GetString(dr, "OriginalInvoiceNo");
                    data.OriginalInvoiceDt = DaHelper.GetDate(dr, "OriginalInvoiceDt");
                    data.InstallmentPeriod = DaHelper.GetInt(dr, "InstallmentPeriod");
                    data.InstallmentTotalPeriod = DaHelper.GetInt(dr, "InstallmentTotalPeriod");
                    data.MeterId = DaHelper.GetString(dr, "MeterId");
                    data.RateTypeId = DaHelper.GetString(dr, "RateTypeId");
                    data.BranchId = DaHelper.GetString(dr, "BranchId");
                    data.Period = DaHelper.GetString(dr, "Period");
                    data.GroupInvoiceNo = DaHelper.GetString(dr, "GroupInvoiceNo");
                    data.BillBookId = DaHelper.GetString(dr, "BillBookId");
                    data.SpotBillInvoiceNo = DaHelper.GetString(dr, "SpotBillInvoiceNo");
                    data.MeterReadDt = DaHelper.GetDate(dr, "MeterReadDt");
                    data.ReadUnit = DaHelper.GetDecimal(dr, "ReadUnit");
                    data.LastUnit = DaHelper.GetDecimal(dr, "LastUnit");
                    data.FullBaseAmount = DaHelper.GetDecimal(dr, "FullBaseAmount");
                    data.FullFTAmount = DaHelper.GetDecimal(dr, "FullFTAAmount");
                    data.FullQty = DaHelper.GetDecimal(dr, "FullQty");
                    data.FullAmount = DaHelper.GetDecimal(dr, "FullAmount");
                    data.FullVatAmount = DaHelper.GetDecimal(dr, "FullVatAmount");
                    data.FullGAmount = DaHelper.GetDecimal(dr, "FullGAmount");
                    data.BaseAmount = DaHelper.GetDecimal(dr, "BaseAmount");
                    data.FTUnitPrice = DaHelper.GetDecimal(dr, "FTUnitPrice");
                    data.FTAmount = DaHelper.GetDecimal(dr, "FTAmount");
                    data.UnitPrice = DaHelper.GetDecimal(dr, "UnitPrice");
                    data.Qty = DaHelper.GetDecimal(dr, "Qty");
                    data.Amount = DaHelper.GetDecimal(dr, "Amount");
                    data.VatAmount = DaHelper.GetDecimal(dr, "VatAmount");
                    data.GAmount = DaHelper.GetDecimal(dr, "GAmount");
                    data.QtyInstallment = DaHelper.GetDecimal(dr, "QtyInstallment");
                    data.AmountInstallment = DaHelper.GetDecimal(dr, "AmountInstallment");
                    data.VatAmountInstallment = DaHelper.GetDecimal(dr, "VatInstallment");
                    data.GAmountInstallment = DaHelper.GetDecimal(dr, "GAmountInstallment");
                    data.DtId = DaHelper.GetString(dr, "DtId");
                    data.DtName = DaHelper.GetString(dr, "DtName");
                    data.ControllerId = DaHelper.GetString(dr, "ControllerId");
                    data.TaxCode = DaHelper.GetString(dr, "TaxCode");
                    data.TaxRate = DaHelper.GetDecimal(dr, "TaxRate");
                    data.PartialPayment = DaHelper.GetByte(dr, "PartialPayment");
                    data.GroupIvReceiptType = DaHelper.GetString(dr, "GroupInvoiceReceiptType");
                    //--END--POS2--------------------------------

                    data.PostDt = DaHelper.GetDate(dr, "PostDt");
                    data.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                    data.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                    data.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                    data.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");
                    data.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                    receiptList.Add(data);
                }
            }

            return receiptList;
        }

        public List<ReceiptItemInfo> GetReceiptItemInfo(List<ReceiptInfo> receiptList)
        {
            List<ReceiptItemInfo> receiptItemList = new List<ReceiptItemInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            foreach (ReceiptInfo receipt in receiptList)
            {
                DbCommand cmd = db.GetStoredProcCommand("pc_sel_CopyReceiptItem");
                db.AddInParameter(cmd, "ReceiptId", DbType.String, receipt.ReceiptId);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    ReceiptItemInfo data = new ReceiptItemInfo();
                    data.ReceiptId = DaHelper.GetString(dr, "ReceiptId");
                    data.ARPmId = DaHelper.GetString(dr, "ARPmId");
                    data.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                    data.PostDt = DaHelper.GetDate(dr, "PostDt");
                    data.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                    data.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                    data.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");
                    data.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                    receiptItemList.Add(data);
                }
            }

            return receiptItemList;
        }

        //allowed only search by caId
        public ArrayList GetDataFromDb(SAPSearchParam param)
        {
            ArrayList debtList = new ArrayList();
            List<BusinessPartnerInfo> bp = new List<BusinessPartnerInfo>();
            List<ContractAccountInfo> ca = new List<ContractAccountInfo>();
            List<ARInfo> ar = new List<ARInfo>();
            List<DisconnectionDoc> disDoc = new List<DisconnectionDoc>();
            List<RTDisconnectionDocCaDoc> rtDisDoc = new List<RTDisconnectionDocCaDoc>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            //Copy AR 
            DbCommand cmd1 = db.GetStoredProcCommand("pc_sel_CopyCA");
            db.AddInParameter(cmd1, "CaId", DbType.String, param.CaId);
            DataTable dt1 = db.ExecuteDataSet(cmd1).Tables[0];
            foreach (DataRow dr in dt1.Rows)
            {
                ContractAccountInfo b = new ContractAccountInfo();
                b.CaId = DaHelper.GetString(dr, "CaId");
                b.TechBranchId = DaHelper.GetString(dr, "TechBranchId");
                b.CommBranchId = DaHelper.GetString(dr, "CommBranchId");
                b.MruId = DaHelper.GetString(dr, "MruId");
                b.BpId = DaHelper.GetString(dr, "BpId");
                b.CaName = DaHelper.GetString(dr, "CaName");
                b.ReceiptType = DaHelper.GetString(dr, "GroupIvReceiptType");
                b.ReceiptPrintName = DaHelper.GetString(dr, "ReceiptPrintName");
                b.CaAddress = DaHelper.GetString(dr, "CaAddress");
                b.CtId = DaHelper.GetString(dr, "CtId");
                b.PmId = DaHelper.GetString(dr, "PmId");
                b.AccountClassId = DaHelper.GetString(dr, "AccountClassId");
                b.SecurityDeposit = DaHelper.GetDecimal(dr, "SecurityDeposit");
                b.InterestKey = DaHelper.GetString(dr, "InterestKey");
                b.PaidBy = DaHelper.GetString(dr, "PaidBy");
                b.ContractValidFrom = DaHelper.GetDate(dr, "ContractValidFrom");
                b.ContractValidTo = DaHelper.GetDate(dr, "ContractValidTo");
                b.MeterInstallDt = DaHelper.GetDate(dr, "MeterInstallDt");
                b.MeterSizeId = DaHelper.GetString(dr, "MeterSizeId");
                b.ControllerId = DaHelper.GetString(dr, "ControllerId");
                b.TransportHelp = DaHelper.GetDecimal(dr, "TransportHelp");
                b.PenaltyWaiveFlag = DaHelper.GetString(dr, "PenaltyWaiveFlag");
                b.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                b.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");
                b.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                b.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;

                ////Tax13
                //b.CaTaxId = DaHelper.GetString(dr, "CaTaxId");
                //b.CaTaxBranch = DaHelper.GetString(dr, "CaTaxBranch");
                ca.Add(b);
            }

            //Copy BP
            foreach (ContractAccountInfo c in ca)
            {
                DbCommand cmd2 = db.GetStoredProcCommand("pc_sel_CopyBP");
                db.AddInParameter(cmd2, "BpId", DbType.String, c.BpId);
                DataTable dt2 = db.ExecuteDataSet(cmd2).Tables[0];
                foreach (DataRow dr in dt2.Rows)
                {
                    BusinessPartnerInfo b = new BusinessPartnerInfo();
                    b.BpId = DaHelper.GetString(dr, "BpId");
                    b.BpTypeId = DaHelper.GetString(dr, "BpTypeId");
                    b.TaxCode = DaHelper.GetString(dr, "TaxCode");
                    b.CitizenId = DaHelper.GetString(dr, "CitizenId");
                    b.PassportNo = DaHelper.GetString(dr, "PassportNo");
                    b.RegisterId = DaHelper.GetString(dr, "RegisterId");
                    b.VatCode = DaHelper.GetString(dr, "VatCode");
                    b.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                    b.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");
                    b.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                    b.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                    bp.Add(b);
                }
            }

            //Copy AR
            DbCommand cmd3 = db.GetStoredProcCommand("pc_sel_CopyAR");
            db.AddInParameter(cmd3, "CaId", DbType.String, param.CaId);
            DataTable dt3 = db.ExecuteDataSet(cmd3).Tables[0];
            foreach (DataRow dr in dt3.Rows)
            {
                ARInfo obj = new ARInfo();
                obj.ItemId = DaHelper.GetString(dr, "ItemId");
                obj.CaId = DaHelper.GetString(dr, "CaId");
                obj.DtId = DaHelper.GetString(dr, "DtId");
                obj.CaDoc = DaHelper.GetString(dr, "CaDoc");
                obj.Description = DaHelper.GetString(dr, "Description");
                obj.InvoiceNo = DaHelper.GetString(dr, "InvoiceNo");
                obj.InvoiceDt = DaHelper.GetDate(dr, "InvoiceDt");
                obj.GroupInvoiceNo = DaHelper.GetString(dr, "GroupInvoiceNo");
                obj.BillBookId = DaHelper.GetString(dr, "BillBookId");
                obj.Period = DaHelper.GetString(dr, "Period");
                obj.MeterId = DaHelper.GetString(dr, "MeterId");
                obj.RateTypeId = DaHelper.GetString(dr, "RateTypeId");
                obj.MeterReadDt = DaHelper.GetDate(dr, "MeterReadDt");
                obj.ReadUnit = DaHelper.GetDecimal(dr, "ReadUnit");
                obj.LastUnit = DaHelper.GetDecimal(dr, "LastUnit");
                obj.BaseAmount = DaHelper.GetDecimal(dr, "BaseAmount");
                obj.FTUnitPrice = DaHelper.GetDecimal(dr, "FTUnitPrice");
                obj.FTAmount = DaHelper.GetDecimal(dr, "FTAmount");
                obj.UnitPrice = DaHelper.GetDecimal(dr, "UnitPrice");
                obj.Qty = DaHelper.GetDecimal(dr, "Qty");
                obj.UnitTypeId = DaHelper.GetString(dr, "UnitTypeId");
                obj.Amount = DaHelper.GetDecimal(dr, "Amount");
                obj.TaxCode = DaHelper.GetString(dr, "TaxCode");
                obj.VatAmount = DaHelper.GetDecimal(dr, "VatAmount");
                obj.GAmount = DaHelper.GetDecimal(dr, "GAmount");
                obj.DueDt = DaHelper.GetDate(dr, "DueDt");
                obj.DueDt2 = DaHelper.GetDate(dr, "DueDt2");
                obj.PaidQty = DaHelper.GetDecimal(dr, "PaidQty");
                obj.PaidVatAmount = DaHelper.GetDecimal(dr, "PaidVatAmount");
                obj.PaidGAmount = DaHelper.GetDecimal(dr, "PaidGAmount");
                obj.DisconnectDt = DaHelper.GetDate(dr, "DisconnectDt");
                obj.DisconnectDoc = DaHelper.GetString(dr, "DisconnectDoc");
                obj.SubstDocNo = DaHelper.GetString(dr, "SubstDocNo");
                obj.OriginalInvoiceNo = DaHelper.GetString(dr, "OriginalInvoiceNo");
                obj.SpotBillInvoiceNo = DaHelper.GetString(dr, "SpotBillInvoiceNo");
                obj.InterestLockFlag = DaHelper.GetString(dr, "InterestLockFlag");
                obj.InterestKey = DaHelper.GetString(dr, "InterestKey");
                obj.InstallmentFlag = DaHelper.GetString(dr, "InstallmentFlag");
                obj.InstallmentPeriod = DaHelper.GetInt(dr, "InstallmentPeriod");
                obj.InstallmentTotalPeriod = DaHelper.GetInt(dr, "InstallmentTotalPeriod");
                obj.PaymentOrderFlag = DaHelper.GetString(dr, "PaymentOrderFlag");
                obj.PaymentOrderDt = DaHelper.GetDate(dr, "PaymentOrderDt");
                //--POS2--START
                obj.CheckInRefNo = DaHelper.GetString(dr, "CheckInRefNo");
                //--POS--END
                obj.CancelFlag = DaHelper.GetString(dr, "CancelFlag");
                obj.ModifiedFlag = DaHelper.GetString(dr, "ModifiedFlag");
                obj.POSDebtFlag = DaHelper.GetString(dr, "POSDebtFlag");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");
                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                obj.CreatedDt = DaHelper.GetDate(dr, "CreatedDt");
                obj.FileName = DaHelper.GetString(dr, "FileName");
                ar.Add(obj);
            }

            //Copy RTDisDoc
            List<string> caDocList = new List<string>();
            foreach (ARInfo r in ar)
            {
                if (caDocList.Contains(r.CaDoc)) continue;

                DbCommand cmd4 = db.GetStoredProcCommand("pc_sel_CopyRTDisDoc");
                db.AddInParameter(cmd4, "CaDoc", DbType.String, r.CaDoc);  //len 12
                DataTable dt4 = db.ExecuteDataSet(cmd4).Tables[0];
                foreach (DataRow dr in dt4.Rows)
                {
                    RTDisconnectionDocCaDoc obj = new RTDisconnectionDocCaDoc();
                    obj.DiscNo = DaHelper.GetString(dr, "DiscNo");
                    obj.CaDocNo = DaHelper.GetString(dr, "CaDocNo");
                    obj.CancelFlag = DaHelper.GetString(dr, "CancelFlag");
                    obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                    obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");
                    obj.Active = DaHelper.GetString(dr, "Active");
                    rtDisDoc.Add(obj);
                }

                caDocList.Add(r.CaDoc);
            }

            List<string> disDocument = new List<string>();
            foreach (RTDisconnectionDocCaDoc rt in rtDisDoc)
            {
                if (disDocument.Contains(rt.DiscNo)) continue;

                DbCommand cmd5 = db.GetStoredProcCommand("pc_sel_CopyDiscDoc");
                db.AddInParameter(cmd5, "DiscDoc", DbType.String, rt.DiscNo);  //len 12
                DataTable dt = db.ExecuteDataSet(cmd5).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    DisconnectionDoc obj = new DisconnectionDoc();
                    obj.DiscNo = DaHelper.GetString(dr, "DiscNo");
                    obj.CreatedDt = DaHelper.GetDate(dr, "CreatedDt");
                    obj.DiscStatusId = DaHelper.GetString(dr, "DiscStatusId");
                    obj.ReleaseDt = DaHelper.GetDate(dr, "ReleaseDt");
                    obj.WorkOrderNo = DaHelper.GetString(dr, "WorkOrderNo");
                    obj.DiscPlanStart = DaHelper.GetDate(dr, "DiscPlanStart");
                    obj.DiscPlanEnd = DaHelper.GetDate(dr, "DiscPlanEnd");
                    obj.WorkCenter = DaHelper.GetString(dr, "WorkCenter");
                    obj.PostpConfirm = DaHelper.GetDate(dr, "PostpConfirm");
                    obj.FuseRemConfirm = DaHelper.GetDate(dr, "FuseRemConfirm");
                    obj.MeterRemConfirm = DaHelper.GetDate(dr, "MeterRemConrim");
                    obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                    obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");
                    obj.Active = DaHelper.GetString(dr, "Active");
                    disDoc.Add(obj);
                }

                disDocument.Add(rt.DiscNo);
            }

            debtList.Add(bp);
            debtList.Add(ca);
            debtList.Add(ar);
            debtList.Add(disDoc);
            debtList.Add(rtDisDoc);
            return debtList;
        }

        public List<LastReceiptPayment> GetRepayLastReceiptData(string receiptId)
        {
            List<LastReceiptPayment> lastReceiptPayments = new List<LastReceiptPayment>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            DbCommand cmd = db.GetStoredProcCommand("pc_get_RepayLastReceiptData");
            db.AddInParameter(cmd, "ReceiptId", DbType.String, receiptId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                LastReceiptPayment data = new LastReceiptPayment();
                data.ReceiptId = DaHelper.GetString(dr, "ReceiptId");
                data.CashAmount = DaHelper.GetDecimal(dr, "CashAmount");
                data.ChqAmount = DaHelper.GetDecimal(dr, "ChqAmount");
                data.TransAmount = DaHelper.GetDecimal(dr, "TransAmount");
                data.AdjAmount = DaHelper.GetDecimal(dr, "AdjAmount");
                data.GAmount = DaHelper.GetDecimal(dr, "GAmount");
                lastReceiptPayments.Add(data);
            }

            return lastReceiptPayments;
        }

        #endregion

        #region Offline Logging

        public void OfflineLog(OfflineLogInfo log)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_ins_OfflineLog");
            db.AddInParameter(cmd, "LogFileName", DbType.String, log.FileName);
            db.AddInParameter(cmd, "ErrorMsg", DbType.String, log.ErrorMsg);
            db.AddInParameter(cmd, "PosId", DbType.String, log.PosId);
            db.AddInParameter(cmd, "ClientIP", DbType.String, log.ClientIP);
            db.AddInParameter(cmd, "Solved", DbType.String, log.Solved);
            db.AddInParameter(cmd, "CashierId", DbType.String, log.RunCashier);
            db.AddInParameter(cmd, "BranchId", DbType.String, log.BranchId);
            db.AddInParameter(cmd, "BranchName", DbType.String, log.BranchName);
            db.ExecuteNonQuery(cmd);
        }


        public string CheckWorkStatus(OpenWorkParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_ins_OpenWorkSilence");
            db.AddInParameter(cmd, "CashierId", DbType.String, param.CashierId);
            db.AddInParameter(cmd, "PosId", DbType.String, param.PosId);
            db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
            db.AddInParameter(cmd, "PaymentDt", DbType.DateTime, param.PaymentDt);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, param.ModifiedBy);
            db.AddInParameter(cmd, "PostedBranchId", DbType.String, param.PostedBranchId);
            return (string)db.ExecuteScalar(cmd);
        }

        //// รวมใบเสร็จแผนผ่อน 2021-10-07 Check ใบเสร็จรวม Enable Status from ta.AppSetting
        public string CheckSettingGroupReceiptEnableStatus()
        {
            Database db     = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd   = db.GetStoredProcCommand("ta_get_GroupReceiptSettingStatus");           
            return (string)db.ExecuteScalar(cmd);
        }

       

        #endregion

        #region Check money in tray

        public TrayMoneyInfo GetMoneyInTray(DbTransaction trans, string workId)
        {
            TrayMoneyInfo trayMoneyInfo = new TrayMoneyInfo();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_sel_cashierWorkCash");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "WorkId", DbType.String, workId);
            DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            DataRow cashRow = dt.Rows[0];
            trayMoneyInfo.CashAmount = DaHelper.GetDecimal(cashRow, "TotalAmount");
            trayMoneyInfo.CashPendingAmount = DaHelper.GetDecimal(cashRow, "PendingAmount");


            BindingList<ChequeInfo> chequeList = new BindingList<ChequeInfo>();
            DbCommand cmd1 = db.GetStoredProcCommand("cm_sel_cashierWorkCheque");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd1, "WorkId", DbType.String, workId);
            DataTable dt1 = db.ExecuteDataSet(cmd1, trans).Tables[0];
            foreach (DataRow dr in dt1.Rows)
            {
                ChequeInfo cq = new ChequeInfo();
                cq.BankKey = DaHelper.GetString(dr, "BankKey");
                cq.BankName = DaHelper.GetString(dr, "BankName");
                cq.ChqNo = DaHelper.GetString(dr, "ChqNo");
                cq.ChqAccNo = DaHelper.GetString(dr, "ChqAccNo");
                cq.ChqDt = DaHelper.GetDate(dr, "ChqDt");
                cq.Amount = DaHelper.GetDecimal(dr, "Amount");
                cq.TransStatus = DaHelper.GetString(dr, "TransStatus");
                chequeList.Add(cq);
            }

            trayMoneyInfo.ChequeList = chequeList;

            return trayMoneyInfo;
        }

        #endregion


        #region Insert OneTouch Log
        public void SaveOneTouchLog(OneTouchLogInfo log)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_ins_OneTouchLog");

            db.AddInParameter(cmd, "NotificationNo", DbType.String, log.NotificationNo);
            db.AddInParameter(cmd, "InvoiceNo", DbType.String, log.InvoiceNo);
            db.AddInParameter(cmd, "DtId", DbType.String, log.DebtId);
            db.AddInParameter(cmd, "ReceiptId", DbType.String, log.ReceiptId);
            db.AddInParameter(cmd, "GAmount", DbType.Decimal, log.GAmount);
            db.AddInParameter(cmd, "VatAmount", DbType.Decimal, log.VatAmount);
            db.AddInParameter(cmd, "SyncFlag", DbType.String, log.SyncFlag);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, log.ModifiedBy);
            db.AddInParameter(cmd, "Action", DbType.String, log.Action);

            db.ExecuteNonQuery(cmd);
        }
        #endregion

        #region Offline Payment
        public List<OfflinePayment> GetOfflinePayment(string branchId, string cashierId, string workId)
        {

            List<OfflinePayment> OfflinePayments = new List<OfflinePayment>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_Offline_Payment");

            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "CashierId", DbType.String, cashierId);
            db.AddInParameter(cmd, "WorkId", DbType.String, workId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                OfflinePayment op = new OfflinePayment();
                op.CashierId = DaHelper.GetString(dr, "CashierId");
                op.CashierName = DaHelper.GetString(dr, "CashierName");
                op.GAmount = DaHelper.GetDecimal(dr, "GAmount");

                OfflinePayments.Add(op);
            }

            return OfflinePayments;
        }

        public void UpdateOfflinePayment(string branchId, string cashierId, string posId, string workId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_upd_Offline_Payment");

            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "CashierId", DbType.String, cashierId);
            db.AddInParameter(cmd, "WorkId", DbType.String, workId);
            db.AddInParameter(cmd, "PosId", DbType.String, posId);
            db.ExecuteNonQuery(cmd);
        }

        #endregion


    }
}