using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Text;
using System.Threading;

using Microsoft.Practices.EnterpriseLibrary.Data;
using PEA.BPM.AgencyManagementModule.Interface.Services;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.AgencyManagementModule.Interface.Constants;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.SqlServer.Server;

using Avanade.ACA.Batch;
using Avanade.ACA.Batch.Configuration;

namespace PEA.BPM.AgencyManagementModule.DA
{
    public class BillBookDataAccess : IBillBookRepository
    {

        #region "Select command"

        public List<BillBookPrePaidInfo> GetBillBookPrePaid(string billBookId)
        {
            List<BillBookPrePaidInfo> retVals = new List<BillBookPrePaidInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_BillBookPrePaid");
            db.AddInParameter(cmd, "pBillBookId", DbType.String, billBookId);
            DataSet data = db.ExecuteDataSet(cmd);
            if (data.Tables.Count > 0)
            {
                DataTable dt = data.Tables[0];
                foreach (DataRow r in dt.Rows)
                {
                    BillBookPrePaidInfo prePaid = new BillBookPrePaidInfo();
                    prePaid.BillBookId = DaHelper.GetString(r, "BillBookId");
                    prePaid.PaymentDt = DaHelper.GetDate(r, "PaymentDt") == null ? String.Empty : DaHelper.GetDate(r, "PaymentDt").Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                    prePaid.ReceiptId = DaHelper.GetString(r, "ReceiptId");
                    prePaid.Amount = DaHelper.GetDecimal(r, "Amount");
                    retVals.Add(prePaid);
                }
            }
            else
            {
                return new List<BillBookPrePaidInfo>();
            }
            return retVals;
        }

        //GetAdvanceBookPayment
        public decimal GetAdvancePaymentByMRU(DbTransaction trans, string bookId)
        {
            decimal ret = 0;
            object obj = new object();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_AdvancePaymentByMRU");
            db.AddInParameter(cmd, "BillBookId", DbType.String, bookId);
            if (trans != null)
            {
                obj = db.ExecuteScalar(cmd, trans);
            }
            else
            {
                obj = db.ExecuteScalar(cmd);
            }

            if (obj != System.DBNull.Value)
                ret = (decimal)obj;

            return ret;
        }

        public decimal GetPrePaidAccountReceive(DbTransaction trans, string bookId)
        {
            decimal? ret = 0;
            object obj = new object();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_AdvBookPayment");
            cmd.CommandTimeout = 600;
            db.AddInParameter(cmd, "BillBookId", DbType.String, bookId);
            DataSet data = db.ExecuteDataSet(cmd);
            if ((data.Tables.Count > 0) && (data.Tables[0].Rows.Count == 1))
            {
                ret = DaHelper.GetDecimal(data.Tables[0].Rows[0], "AdvPayAmount");
            }

            if (ret == null)
                return 0;
            else
                return ret.Value;
        }

        public BillBookCheckInInfo GetBillCheckInCancel(string billBookId)
        {
            BillBookCheckInInfo bookCheckIn = new BillBookCheckInInfo();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_BillBookCheckInCancel");
            db.AddInParameter(cmd, "pBillBookId", DbType.String, billBookId);
            DataSet data = db.ExecuteDataSet(cmd);
            if ((data.Tables.Count > 0) && (data.Tables[0].Rows.Count == 1))
            {
                DataRow row = data.Tables[0].Rows[0];
                bookCheckIn.BookId = DaHelper.GetString(row, "BookId"); ;
                bookCheckIn.BookOutType = DaHelper.GetString(row, "AboName");
                bookCheckIn.ContractType = Convert.ToInt32(DaHelper.GetString(row, "CtId"));
                bookCheckIn.BillAgentId = DaHelper.GetString(row, "AgentId");
                bookCheckIn.BillAgentName = DaHelper.GetString(row, "CaName");
                bookCheckIn.BillPaymentDate = DaHelper.GetDate(row, "CreateDate");
                bookCheckIn.ReceiveCount = DaHelper.GetByte(row, "ReceiveCount");
                bookCheckIn.BookType = DaHelper.GetInt(row, "BookType").Value;
                //TO DO: Find Paid Date
                bookCheckIn.PaidDate = Session.BpmDateTime.Now;
                bookCheckIn.ReturnDueDate = DaHelper.GetDate(row, "ReturnDueDate");
                bookCheckIn.BookPeriod = DaHelper.GetBillPeriod(DaHelper.GetString(row, "BookPeriod"));
                bookCheckIn.ModifiedBy = DaHelper.GetString(row, "ModifiedBy");
                bookCheckIn.ModifiedDt = DaHelper.GetDate(row, "ModifiedDt");

                bookCheckIn.BillBookCheckInDetail = this.GetBillBookCheckInDetail(billBookId, String.Empty);
                return bookCheckIn;
            }
            else
            {
                return new BillBookCheckInInfo();
            }
        }

        public BillBookCheckInInfo GetBillCheckInInfomation(string bookId, bool isHistory)
        {
            BillBookCheckInInfo bookCheckIn = new BillBookCheckInInfo();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_BillBookCheckinHeader");
            db.AddInParameter(cmd, "pBillBookId", DbType.String, bookId);

            DataSet data = db.ExecuteDataSet(cmd);


            if ((data.Tables.Count > 0) && (data.Tables[0].Rows.Count == 1))
            {
                DataRow row = data.Tables[0].Rows[0];
                bookCheckIn.BookId = DaHelper.GetString(row, "BookId");
                bookCheckIn.BookType = Convert.ToInt32(DaHelper.GetString(row, "BookType"));
                bookCheckIn.AccountClassId = DaHelper.GetString(row, "AccountClassId");
                bookCheckIn.BookOutType = DaHelper.GetString(row, "AboName");
                bookCheckIn.ContractType = Convert.ToInt32(DaHelper.GetString(row, "CtId"));
                bookCheckIn.BillAgentId = DaHelper.GetString(row, "AgentId");
                bookCheckIn.BillAgentName = DaHelper.GetString(row, "CaName");
                bookCheckIn.BillPaymentDate = DaHelper.GetDate(row, "CreateDate");
                bookCheckIn.ReceiveCount = DaHelper.GetByte(row, "ReceiveCount");
                bookCheckIn.PaidDate = DaHelper.GetDate(row, "CheckInDate");
                bookCheckIn.ReturnDueDate = DaHelper.GetDate(row, "ReturnDueDate");
                bookCheckIn.BookPeriod = DaHelper.GetBillPeriod(DaHelper.GetString(row, "BookPeriod"));
                bookCheckIn.Note = DaHelper.GetString(row, "Note");
                bookCheckIn.ModifiedDt = DaHelper.GetDate(row, "ModifiedDt");
                bookCheckIn.ModifiedBy = DaHelper.GetString(row, "ModifiedBy");
                bookCheckIn.BsId = DaHelper.GetString(row, "BsId");
                if (isHistory)
                {
                    bookCheckIn.BillBookCheckInDetail = this.GetBillBookCheckInHistoryDetail(bookId, bookCheckIn.BookPeriod);
                }
                else
                {
                    bookCheckIn.BillBookCheckInDetail = this.GetBillBookCheckInDetail(bookId, bookCheckIn.BookPeriod);
                }
                return bookCheckIn;
            }
            else
            {
                return new BillBookCheckInInfo();
            }

        }

        public BillBookCheckInInfo GetGroupInvoiceCheckInInfomation(string groupIvId, string branchId, bool isHistory)
        {
            BillBookCheckInInfo bookCheckIn = new BillBookCheckInInfo();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_GroupInvoiceCheckinHeader");
            db.AddInParameter(cmd, "pGroupInvoiceNo", DbType.String, groupIvId);
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);

            DataSet data = db.ExecuteDataSet(cmd);

            if ((data.Tables.Count > 0) && (data.Tables[0].Rows.Count > 0))
            {
                DataRow row = data.Tables[0].Rows[0];
                bookCheckIn.BookId = DaHelper.GetString(row, "BookId");
                bookCheckIn.BookType = Convert.ToInt32(DaHelper.GetString(row, "BookType"));
                bookCheckIn.AccountClassId = DaHelper.GetString(row, "AccountClassId");
                bookCheckIn.BookOutType = DaHelper.GetString(row, "AboName");
                bookCheckIn.ContractType = Convert.ToInt32(DaHelper.GetString(row, "CtId"));
                bookCheckIn.BillAgentId = DaHelper.GetString(row, "AgentId");
                bookCheckIn.BillAgentName = DaHelper.GetString(row, "CaName");
                bookCheckIn.BillPaymentDate = DaHelper.GetDate(row, "CreateDate");
                bookCheckIn.ReceiveCount = DaHelper.GetInt(row, "ReceiveCount");
                bookCheckIn.PaidDate = null;
                bookCheckIn.ReturnDueDate = DaHelper.GetDate(row, "ReturnDueDate");
                bookCheckIn.BookPeriod = data.Tables[0].Rows.Count == 1 ? DaHelper.GetBillPeriod(DaHelper.GetString(row, "BookPeriod")) : String.Empty;
                bookCheckIn.Note = DaHelper.GetString(row, "Note");
                bookCheckIn.ModifiedDt = DaHelper.GetDate(row, "ModifiedDt");
                bookCheckIn.ModifiedBy = DaHelper.GetString(row, "ModifiedBy");
                bookCheckIn.BsId = DaHelper.GetString(row, "BsId");

                if (isHistory)
                {
                    bookCheckIn.BillBookCheckInDetail = this.GetGroupInvoiceCheckInHistoryDetail(groupIvId, branchId);
                }
                else
                {
                    bookCheckIn.BillBookCheckInDetail = this.GetGroupInvoiceCheckInDetail(groupIvId, branchId);
                }
                return bookCheckIn;
            }
            else
            {
                return new BillBookCheckInInfo();
            }
        }

        public List<BillBookCheckinDetailInfo> GetBillBookCheckInDetail(string bookId, string period)
        {
            BillBookCheckInInfo bookCheckIn = new BillBookCheckInInfo();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_BillBookCheckInDetail");
            db.AddInParameter(cmd, "pBillBookId", DbType.String, bookId);
            db.AddInParameter(cmd, "pPeriod", DbType.String, DaHelper.SetBillPeriod(period));

            List<BillBookCheckinDetailInfo> retVals = new List<BillBookCheckinDetailInfo>();

            DataSet data = db.ExecuteDataSet(cmd);
            if ((data.Tables.Count > 0) && (data.Tables[0].Rows.Count > 0))
            {
                foreach (DataRow row in data.Tables[0].Rows)
                {
                    BillBookCheckinDetailInfo billbookDetail = new BillBookCheckinDetailInfo();
                    billbookDetail.BookId = DaHelper.GetString(row, "BookId");
                    billbookDetail.InvoiceNo = DaHelper.GetString(row, "InvoiceNo");
                    billbookDetail.Period = DaHelper.GetBillPeriod(DaHelper.GetString(row, "Period"));
                    billbookDetail.BranchId = DaHelper.GetString(row, "BranchId");
                    billbookDetail.MruId = DaHelper.GetString(row, "MruId");
                    billbookDetail.CaId = DaHelper.GetString(row, "CaId");
                    billbookDetail.CaName = DaHelper.GetString(row, "CaName");
                    billbookDetail.AbsId = DaHelper.GetString(row, "AbsId");
                    billbookDetail.AboId = DaHelper.GetString(row, "AboId");
                    billbookDetail.PmId = DaHelper.GetString(row, "PmId") == null ? PmIdEnum.NONE : DaHelper.GetString(row, "PmId");
                    billbookDetail.InBook = DaHelper.GetString(row, "InBook");
                    billbookDetail.PaidAmount = DaHelper.GetDecimal(row, "PaidAmount");
                    billbookDetail.TotalAmount = DaHelper.GetDecimal(row, "TotalAmount");
                    billbookDetail.GAmount = DaHelper.GetDecimal(row, "GAmount");
                    billbookDetail.Vat = DaHelper.GetDecimal(row, "Vat");
                    billbookDetail.BranchId = DaHelper.GetString(row, "BranchId");
                    billbookDetail.LastPaidDt = DaHelper.GetDate(row, "LastPaidDt");
                    billbookDetail.ModifiedBy = DaHelper.GetString(row, "ModifiedBy");
                    billbookDetail.ModifiedDt = DaHelper.GetDate(row, "ModifiedDt");
                    billbookDetail.PaidType = DaHelper.GetString(row, "PaidType") == null ? (int)PaidTypeEnum.UNDEFINE : Convert.ToInt32(DaHelper.GetString(row, "PaidType"));
                    billbookDetail.ARActive = DaHelper.GetString(row, "ARActive") == "0" ? false : true;
                    billbookDetail.InvSel = DaHelper.GetString(row, "InvSelected") == "Y" ? true : false;
                    billbookDetail.ItemPaid = DaHelper.GetDecimal(row, "ItemPaid");

                    if (billbookDetail.PaidType == (int)PaidTypeEnum.CHEQUE)
                    {
                        billbookDetail.ChequeList = this.GetChequeInfo(bookId, billbookDetail.InvoiceNo);
                    }

                    retVals.Add(billbookDetail);
                }
                return retVals;
            }
            else
            {
                return new List<BillBookCheckinDetailInfo>();
            }
        }

        public List<BillBookCheckinDetailInfo> GetGroupInvoiceCheckInDetail(string groupIvId, string branchId)
        {
            List<BillBookCheckinDetailInfo> retVals = new List<BillBookCheckinDetailInfo>();
            BillBookCheckInInfo bookCheckIn = new BillBookCheckInInfo();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_GroupInvoiceCheckinDetail");
            cmd.CommandTimeout = 360;
            db.AddInParameter(cmd, "pGroupInvoiceNo", DbType.String, groupIvId);
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            DataSet data = db.ExecuteDataSet(cmd);

            if ((data.Tables.Count > 0) && (data.Tables[0].Rows.Count > 0))
            {
                foreach (DataRow row in data.Tables[0].Rows)
                {
                    BillBookCheckinDetailInfo billbookDetail = new BillBookCheckinDetailInfo();
                    billbookDetail.BookId = DaHelper.GetString(row, "BookId").Trim();
                    billbookDetail.InvoiceNo = DaHelper.GetString(row, "InvoiceNo");
                    billbookDetail.Period = DaHelper.GetBillPeriod(DaHelper.GetString(row, "Period"));
                    billbookDetail.BranchId = DaHelper.GetString(row, "BranchId");
                    billbookDetail.MruId = DaHelper.GetString(row, "MruId");
                    billbookDetail.CaId = DaHelper.GetString(row, "CaId");
                    billbookDetail.CaName = DaHelper.GetString(row, "CaName");
                    billbookDetail.AbsId = DaHelper.GetString(row, "AbsId");
                    billbookDetail.AboId = DaHelper.GetString(row, "AboId");
                    billbookDetail.PmId = DaHelper.GetString(row, "PmId") == null ? PmIdEnum.NONE : DaHelper.GetString(row, "PmId");
                    billbookDetail.InBook = DaHelper.GetString(row, "InBook");
                    billbookDetail.PaidAmount = DaHelper.GetDecimal(row, "PaidGAmount");
                    billbookDetail.TotalAmount = DaHelper.GetDecimal(row, "TotalAmount");
                    billbookDetail.GAmount = DaHelper.GetDecimal(row, "GAmount");
                    billbookDetail.Vat = DaHelper.GetDecimal(row, "Vat");
                    billbookDetail.LastPaidDt = DaHelper.GetDate(row, "LastPaidDt");
                    billbookDetail.ModifiedBy = DaHelper.GetString(row, "ModifiedBy");
                    billbookDetail.ModifiedDt = DaHelper.GetDate(row, "ModifiedDt");
                    billbookDetail.PaidType = DaHelper.GetString(row, "PaidType") == null ? (int)PaidTypeEnum.UNDEFINE : Convert.ToInt32(DaHelper.GetString(row, "PaidType"));
                    billbookDetail.IsCheckIn = (DaHelper.GetString(row, "BillBookID") != String.Empty) && (billbookDetail.PaidAmount != 0);
                    billbookDetail.TotalDebtAmount = billbookDetail.TotalAmount - (billbookDetail.PaidAmount == null ? 0 : billbookDetail.PaidAmount);
                    billbookDetail.ItemPaid = DaHelper.GetDecimal(row, "ItemPaid");

                    billbookDetail.DtName = DaHelper.GetString(row, "DtName").Trim();
                    billbookDetail.SubGroupInvoiceNo = DaHelper.GetString(row, "SubGroupInvoiceNo").Trim();

                    retVals.Add(billbookDetail);
                }
                return retVals;
            }
            else
            {
                return new List<BillBookCheckinDetailInfo>();
            }
        }

        public List<BillBookCheckinDetailInfo> GetBillBookCheckInHistoryDetail(string bookId, string period)
        {
            List<BillBookCheckinDetailInfo> retVals = new List<BillBookCheckinDetailInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            string sql = @" SELECT BookId,   invoiceNo, Period, BranchId, MruId, CaId, AbsId, AboId, PmId, InBook, PaidGAmount, 
                                    TotalAmount, BranchId, LastPaidDt, ModifiedDt, ModifiedBy, CaName, PaidType, Vat
                            FROM ag_bill_checkin_history_detail_view 
                            WHERE BookId = '{0}' ";

            DbCommand cmd = db.GetSqlStringCommand(String.Format(sql, bookId));
            DataSet data = db.ExecuteDataSet(cmd);
            if ((data.Tables.Count > 0) && (data.Tables[0].Rows.Count > 0))
            {
                foreach (DataRow row in data.Tables[0].Rows)
                {
                    BillBookCheckinDetailInfo billbookDetail = new BillBookCheckinDetailInfo();
                    billbookDetail.BookId = DaHelper.GetString(row, "BookId");
                    billbookDetail.InvoiceNo = DaHelper.GetString(row, "InvoiceNo");
                    billbookDetail.Period = DaHelper.GetBillPeriod(DaHelper.GetString(row, "Period"));
                    billbookDetail.BranchId = DaHelper.GetString(row, "BranchId");
                    billbookDetail.MruId = DaHelper.GetString(row, "MruId");
                    billbookDetail.CaId = DaHelper.GetString(row, "CaId");
                    billbookDetail.CaName = DaHelper.GetString(row, "CaName");
                    billbookDetail.AbsId = DaHelper.GetString(row, "AbsId");
                    billbookDetail.AboId = DaHelper.GetString(row, "AboId");
                    billbookDetail.PmId = DaHelper.GetString(row, "PmId") == null ? PmIdEnum.NONE : DaHelper.GetString(row, "PmId");
                    billbookDetail.InBook = DaHelper.GetString(row, "InBook");
                    billbookDetail.PaidAmount = DaHelper.GetDecimal(row, "PaidAmount");
                    billbookDetail.TotalAmount = DaHelper.GetDecimal(row, "TotalAmount");
                    billbookDetail.GAmount = DaHelper.GetDecimal(row, "GAmount");
                    billbookDetail.Vat = DaHelper.GetDecimal(row, "Vat");
                    billbookDetail.BranchId = DaHelper.GetString(row, "BranchId");
                    billbookDetail.LastPaidDt = DaHelper.GetDate(row, "LastPaidDt");
                    billbookDetail.ModifiedBy = DaHelper.GetString(row, "ModifiedBy");
                    billbookDetail.ModifiedDt = DaHelper.GetDate(row, "ModifiedDt");
                    billbookDetail.PaidType = DaHelper.GetString(row, "PaidType") == null ? (int)PaidTypeEnum.UNDEFINE : Convert.ToInt32(DaHelper.GetString(row, "PaidType"));
                    if (billbookDetail.PaidType == (int)PaidTypeEnum.CHEQUE)
                    {
                        billbookDetail.ChequeList = this.GetChequeInfo(bookId, billbookDetail.InvoiceNo);
                    }

                    retVals.Add(billbookDetail);
                }
                return retVals;
            }
            else
            {
                return new List<BillBookCheckinDetailInfo>();
            }
        }

        public List<BillBookCheckinDetailInfo> GetGroupInvoiceCheckInHistoryDetail(string groupIvId, string branchId)
        {
            List<BillBookCheckinDetailInfo> retVals = new List<BillBookCheckinDetailInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_GroupInvoiceCheckinHistoryDetail");
            db.AddInParameter(cmd, "pGroupInvoiceNo", DbType.String, groupIvId);
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);

            DataSet data = db.ExecuteDataSet(cmd);

            if ((data.Tables.Count > 0) && (data.Tables[0].Rows.Count > 0))
            {
                foreach (DataRow row in data.Tables[0].Rows)
                {
                    BillBookCheckinDetailInfo billbookDetail = new BillBookCheckinDetailInfo();
                    billbookDetail.BookId = DaHelper.GetString(row, "GroupInvoiceNo");
                    billbookDetail.InvoiceNo = DaHelper.GetString(row, "InvoiceNo");
                    billbookDetail.Period = DaHelper.GetBillPeriod(DaHelper.GetString(row, "Period"));
                    billbookDetail.BranchId = DaHelper.GetString(row, "BranchId");
                    billbookDetail.MruId = DaHelper.GetString(row, "MruId");
                    billbookDetail.CaId = DaHelper.GetString(row, "CaId");
                    billbookDetail.CaName = DaHelper.GetString(row, "CaName");
                    billbookDetail.AbsId = DaHelper.GetString(row, "AbsId");
                    billbookDetail.AboId = DaHelper.GetString(row, "AboId");
                    billbookDetail.PmId = DaHelper.GetString(row, "PmId") == null ? PmIdEnum.NONE : DaHelper.GetString(row, "PmId");
                    billbookDetail.InBook = DaHelper.GetString(row, "InBook");
                    billbookDetail.PaidAmount = DaHelper.GetDecimal(row, "PaidGAmount");
                    billbookDetail.TotalAmount = DaHelper.GetDecimal(row, "TotalAmount");
                    billbookDetail.GAmount = DaHelper.GetDecimal(row, "GAmount");
                    billbookDetail.BranchId = DaHelper.GetString(row, "CommBranchId");
                    billbookDetail.LastPaidDt = DaHelper.GetDate(row, "LastPaidDt");
                    billbookDetail.ModifiedBy = DaHelper.GetString(row, "ModifiedBy");
                    billbookDetail.ModifiedDt = DaHelper.GetDate(row, "ModifiedDt");
                    billbookDetail.PaidType = DaHelper.GetString(row, "PaidType") == null ? (int)PaidTypeEnum.UNDEFINE : Convert.ToInt32(DaHelper.GetString(row, "PaidType"));
                    billbookDetail.IsCheckIn = DaHelper.GetString(row, "BillBookID").Trim() != String.Empty;
                    billbookDetail.TotalDebtAmount = billbookDetail.TotalAmount - billbookDetail.PaidAmount;
                    retVals.Add(billbookDetail);
                }
                return retVals;
            }
            else
            {
                return new List<BillBookCheckinDetailInfo>();
            }
        }

        public HashInfoCollection GetAbsInformation(string absId)
        {
            HashInfoCollection _ht = new HashInfoCollection();

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            string sql = String.Empty;

            sql = "SELECT AbsId, AbsName, Description " +
                    " FROM mt.AgencyBillCollectionStatus ";
            if (absId != String.Empty)
            {
                sql = String.Format(" {0} WHERE AbsId = {1}", sql, absId);
            }
            DbCommand cmd = db.GetSqlStringCommand(sql);
            DataSet data = db.ExecuteDataSet(cmd);
            if ((data.Tables.Count > 0) && (data.Tables[0].Rows.Count > 0))
            {
                foreach (DataRow row in data.Tables[0].Rows)
                {
                    HashInfo h = new HashInfo();
                    h.Id = DaHelper.GetString(row, "absId");
                    h.Value = DaHelper.GetString(row, "AbsName");
                    _ht.Add(h);
                }
            }
            return _ht;
        }

        public HashInfoCollection GetBillStatusInformation(string bsId)
        {
            HashInfoCollection _ht = new HashInfoCollection();

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            string sql = String.Empty;

            sql = @"SELECT BsId, BsName 
                     FROM mt.BillBookStatus ";

            if (bsId != String.Empty)
            {
                sql = String.Format(" {0} WHERE BsId = {1}", sql, bsId);
            }
            DbCommand cmd = db.GetSqlStringCommand(sql);
            DataSet data = db.ExecuteDataSet(cmd);
            if ((data.Tables.Count > 0) && (data.Tables[0].Rows.Count > 0))
            {
                foreach (DataRow row in data.Tables[0].Rows)
                {
                    HashInfo h = new HashInfo();
                    h.Id = DaHelper.GetString(row, "BsId");
                    h.Value = DaHelper.GetString(row, "BsName");
                    _ht.Add(h);
                }
            }
            return _ht;
        }

        public string GetContractTypeInformation(string ctId)
        {
            string retVal = String.Empty;

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            string sql = String.Empty;

            sql = @"SELECT * 
                    FROM
                        (	SELECT     mt.AccountClass.AccountClassId, mt.AccountClass.AccountClassName
	                        FROM    mt.AccountClass
	                        UNION ALL
	                        SELECT mt.ContractType.CtId, mt.ContractType.CtName
	                        FROM    mt.ContractType
                        ) AS AccountClassTable 
                    WHERE AccountClassId = '{0}' ";

            sql = String.Format(sql, ctId);

            DbCommand cmd = db.GetSqlStringCommand(sql);
            DataSet data = db.ExecuteDataSet(cmd);
            if ((data.Tables.Count > 0) && (data.Tables[0].Rows.Count == 1))
            {
                retVal = data.Tables[0].Rows[0]["AccountClassName"].ToString();
            }
            return retVal;
        }

        public HashInfoCollection GetPmInformation(string pmId)
        {
            HashInfoCollection _ht = new HashInfoCollection();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            string sql = String.Empty;

            sql = @"SELECT PmId, PmName FROM mt.Paymentmethod ";
            if (pmId != String.Empty)
            {
                sql = String.Format(" {0} WHERE PmId = {1}", sql, pmId);
            }
            DbCommand cmd = db.GetSqlStringCommand(sql);
            DataSet data = db.ExecuteDataSet(cmd);
            if ((data.Tables.Count > 0) && (data.Tables[0].Rows.Count > 0))
            {
                foreach (DataRow row in data.Tables[0].Rows)
                {
                    HashInfo h = new HashInfo();
                    h.Id = DaHelper.GetString(row, "PmId");
                    h.Value = DaHelper.GetString(row, "PmName");
                    _ht.Add(h);
                }
            }
            HashInfo h1 = new HashInfo();
            h1.Id = PmIdEnum.NONE;
            h1.Value = "¤éÒ§ªÓÃÐ";
            _ht.Add(h1);
            return _ht;
        }

        /// <summary>
        /// Use in rptBillBookCover.rdlc
        /// </summary>
        /// <param name="billBookId"></param>
        /// <returns></returns>
        public List<BillBookDetailReportListInfo> GetBillBookDetailReportList(string billBookId)
        {
            List<BillBookDetailReportListInfo> retVals = new List<BillBookDetailReportListInfo>();
            BillBookDetailReportListInfo item = new BillBookDetailReportListInfo();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_BillBookDetailReport");
            db.AddInParameter(cmd, "pBillBookId", DbType.String, billBookId);
            DataSet data = db.ExecuteDataSet(cmd);
            if (data.Tables.Count > 0)
            {
                foreach (DataRow row in data.Tables[0].Rows)
                {
                    item = new BillBookDetailReportListInfo();
                    string _period = DaHelper.GetBillPeriod(DaHelper.GetString(row, "Period"));
                    item.BillType = IsCurrentPeriod(_period) == true ? 0 : 1;
                    item.BranchId = DaHelper.GetString(row, "BranchId");
                    item.PayRepeat = DaHelper.GetString(row, "AboId");
                    item.AbsId = DaHelper.GetString(row, "AbsId");
                    item.ElectricPrice = DaHelper.GetDecimal(row, "TotalAmount");
                    item.FtPrice = DaHelper.GetDecimal(row, "TotalFt");
                    item.MRUId = DaHelper.GetString(row, "MruId");
                    item.Vat = DaHelper.GetDecimal(row, "TotalVat");
                    item.BillCount = DaHelper.GetInt(row, "TotalBillCount");
                    item.BaseAmount = DaHelper.GetDecimal(row, "BaseAmount");
                    item.IsAdvPaid = DaHelper.GetString(row, "AdvPay") == "0" ? false : true;
                    item.TotalNet = item.ElectricPrice + item.Vat + item.FtPrice;
                    retVals.Add(item);
                }
            }
            return retVals;
        }

        /// <summary>
        /// For report Rev_301_2
        /// </summary>
        /// <param name="billBookId"></param>
        /// <returns></returns>
        public List<BillBookInfoDetailReport> GetBillBookInfoDetailReportList(string billBookId)
        {
            List<BillBookInfoDetailReport> _retVals = new List<BillBookInfoDetailReport>();
            BillBookInfoDetailReport _item = new BillBookInfoDetailReport();
            string sql = @" SELECT 
                                    ts.BillBookDetail.BillBookId, ts.BillBookDetail.BranchId, ts.BillStatusInfo.MruId, ts.BillStatusInfo.CaId, ts.BillStatusInfo.Period, ts.BillStatusInfo.TotalAmount
                            FROM ts.BillBookDetail INNER JOIN
                                    ts.BillStatusInfo ON ts.BillBookDetail.invoiceNo = ts.BillStatusInfo.invoiceNo
                            WHERE BillBookId = '{0}' 
                            ORDER BY ts.BillBookDetail.BranchId, ts.BillStatusInfo.MruId, ts.BillStatusInfo.CaId ";

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            sql = String.Format(sql, billBookId);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            DataSet data = db.ExecuteDataSet(cmd);
            if (data.Tables.Count > 0)
            {
                foreach (DataRow row in data.Tables[0].Rows)
                {
                    _item = new BillBookInfoDetailReport();
                    _item.BookBillId = DaHelper.GetString(row, "BillBookId").Substring(ModuleConfigurationNames.BranchCodeLength, ModuleConfigurationNames.BillBookIdLength - ModuleConfigurationNames.BranchCodeLength);
                    _item.BranchId = DaHelper.GetString(row, "BranchId");
                    _item.CaId = DaHelper.GetString(row, "CaId");
                    _item.MRUId = DaHelper.GetString(row, "MRUId");
                    _item.Period = DaHelper.GetBillPeriod(DaHelper.GetString(row, "Period"));
                    _item.TotalAmount = DaHelper.GetDecimal(row, "TotalAmount");
                    _retVals.Add(_item);
                }
            }
            return _retVals;
        }

        public List<CAB02_DetailReportInfo> GetAgencyMoneyReturnReportList(string agencyIdFrom, string agencyIdTo,
                                                                                    string periodFrom, string periodTo, string branchId)
        {
            List<CAB02_DetailReportInfo> _retVals = new List<CAB02_DetailReportInfo>();
            Database db = DatabaseFactory.CreateDatabase("PosDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_agencyMoneyReturn30");
            db.AddInParameter(cmd, "pAgencyIdFrom", DbType.String, agencyIdFrom);
            db.AddInParameter(cmd, "pAgencyIdTo", DbType.String, agencyIdTo);
            db.AddInParameter(cmd, "pPeriodFrom", DbType.String, DaHelper.SetBillPeriod(periodFrom));
            db.AddInParameter(cmd, "pPeriodTo", DbType.String, DaHelper.SetBillPeriod(periodTo));
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);

            DataSet data = db.ExecuteDataSet(cmd);
            if (data.Tables.Count > 0)
            {
                string period = String.Empty;
                string receiveDate = String.Empty;
                //int? bookLot = 0;
                int rowId = 0;
                int seqAgency = 0;
                int count = 0;
                int? receive = 0;
                int? totalBill = 0;
                int? paymentCount = 0;
                decimal? balance = 0;
                decimal? sumBalance = 0;
                decimal? preBalance = 0;
                decimal? preBookLot = 0;
                string preReceiptId = String.Empty;
                string previousAgncyId = String.Empty;
                string preBillbookId = String.Empty;
                foreach (DataRow row in data.Tables[0].Rows)
                {
                    count += 1;
                    CAB02_DetailReportInfo _item = new CAB02_DetailReportInfo();
                    _item.AgentId = DaHelper.GetString(row, "AgentId").Substring(4);
                    if (_item.AgentId != previousAgncyId)
                    {
                        rowId = rowId + 1;
                    }

                    balance = DaHelper.GetDecimal(row, "TotalElective") == null ? 0 : DaHelper.GetDecimal(row, "TotalElective");
                    _item.RowId = rowId;
                    _item.BranchId = branchId;
                    _item.CaName = DaHelper.GetString(row, "CaName");
                    _item.Bookperiod = DaHelper.GetBillPeriod(DaHelper.GetString(row, "BookPeriod"));
                    _item.TotalBill = DaHelper.GetInt(row, "TotalBill");
                    _item.AdvpayAmount = DaHelper.GetDecimal(row, "AdvPayAmount") == null ? 0 : DaHelper.GetDecimal(row, "AdvPayAmount");
                    _item.AdvPayDueDate = DaHelper.GetDate(row, "AdvPayDueDate") == null ? String.Empty : DaHelper.GetDate(row, "AdvPayDueDate").Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                    _item.ReceiveCount = DaHelper.GetByte(row, "MaxReceiveCount");
                    _item.BookLot = DaHelper.GetInt(row, "BookLot");
                    _item.TotalElective = DaHelper.GetDecimal(row, "TotalElective") == null ? 0 : DaHelper.GetDecimal(row, "TotalElective");
                    _item.TranfAccNo = DaHelper.GetString(row, "TranfAccNo");
                    _item.TranfAmount = DaHelper.GetDecimal(row, "TranfAmount") == null ? 0 : DaHelper.GetDecimal(row, "TranfAmount");
                    _item.TransPaymentDate = DaHelper.GetDate(row, "transPaymentDt") == null ? String.Empty : DaHelper.GetDate(row, "transPaymentDt").Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                    _item.ChqNo = DaHelper.GetString(row, "ChqNo");
                    _item.ChqAmount = DaHelper.GetDecimal(row, "ChqAmount") == null ? 0 : DaHelper.GetDecimal(row, "ChqAmount");
                    _item.ChqPaymentDate = DaHelper.GetDate(row, "chqPaymentDt") == null ? String.Empty : DaHelper.GetDate(row, "chqPaymentDt").Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                    _item.CashAmount = DaHelper.GetDecimal(row, "CashAmount") == null ? 0 : DaHelper.GetDecimal(row, "CashAmount");
                    _item.CashPaymentDate = DaHelper.GetDate(row, "cashPaymentDt") == null ? String.Empty : DaHelper.GetDate(row, "cashPaymentDt").Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                    if (DaHelper.GetString(row, "cashReceiptId") != null)
                        _item.ReceiptId = DaHelper.GetString(row, "cashReceiptId");
                    else if (DaHelper.GetString(row, "chqReceiptId") != null)
                        _item.ReceiptId = DaHelper.GetString(row, "chqReceiptId");
                    else if (DaHelper.GetString(row, "tranfReceiptId") != null)
                        _item.ReceiptId = DaHelper.GetString(row, "tranfReceiptId");
                    else
                        _item.ReceiptId = String.Empty;
                    _item.PaymentBL = DaHelper.GetDecimal(row, "Balance") == null ? 0 : DaHelper.GetDecimal(row, "Balance");
                    _item.BillbookId = DaHelper.GetString(row, "BillBookId");

                    balance = balance - _item.TotalAmount;
                    _item.Balance = balance;


                    if (_item.AgentId != previousAgncyId || _item.Bookperiod != period || _item.BookLot != preBookLot || _item.ReceiveCount != receive || _item.TotalBill != totalBill)
                    {
                        sumBalance += preBalance;
                        _item.SumBalance = sumBalance;
                        seqAgency = 1;
                    }
                    else
                    {
                        _item.SumBalance = sumBalance;
                        seqAgency += 1;
                    }

                    _item.SeqAgency = seqAgency;

                    previousAgncyId = _item.AgentId;
                    period = _item.Bookperiod;
                    receive = _item.ReceiveCount;
                    totalBill = _item.TotalBill;
                    preBalance = balance;
                    preBookLot = _item.BookLot;

                    if (count == data.Tables[0].Rows.Count)
                    {
                        _item.SumBalance += balance;
                    }

                    if (preBillbookId != _item.BillbookId && preReceiptId != _item.ReceiptId)
                    {
                        paymentCount = 1;
                    }
                    else if (preBillbookId == _item.BillbookId && preReceiptId != _item.ReceiptId)
                    {
                        paymentCount += 1;
                    }
   
                    _item.PaymentCount = paymentCount;

                    preBillbookId = _item.BillbookId;
                    preReceiptId = _item.ReceiptId;

                    _retVals.Add(_item);
                }
            }
            return _retVals;
        }
        public List<ChequeInfo> GetChequeInfo(string billBookId, string invoiceNo)
        {
            List<ChequeInfo> retVals = new List<ChequeInfo>();

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_Cheque");
            db.AddInParameter(cmd, "pBillBookId", DbType.String, billBookId);
            db.AddInParameter(cmd, "pInvoiceNo", DbType.String, invoiceNo);

            DataSet data = db.ExecuteDataSet(cmd);
            if (data.Tables.Count > 0)
            {
                foreach (DataRow row in data.Tables[0].Rows)
                {
                    ChequeInfo cheque = new ChequeInfo();
                    cheque.BookId = billBookId;
                    cheque.CaId = DaHelper.GetString(row, "CaId");
                    cheque.ChequeNo = DaHelper.GetString(row, "ChqNo");
                    cheque.ChequeAmount = DaHelper.GetDecimal(row, "ChequeAmount");
                    cheque.ActualAmount = DaHelper.GetDecimal(row, "GAmount");
                    cheque.ChequeAccountNo = DaHelper.GetString(row, "ChqAccNo");
                    cheque.Period = DaHelper.GetBillPeriod(DaHelper.GetString(row, "Period"));
                    cheque.ChequeDt = DaHelper.GetDate(row, "ChqDt");
                    cheque.BankKey = DaHelper.GetString(row, "BankKey");
                    cheque.BankName = DaHelper.GetString(row, "BankName");
                    retVals.Add(cheque);
                }
            }
            return retVals;
        }

        public string GetCaPaidBy(DbTransaction trans, List<BillBookCheckinDetailInfo> billBookDetails)
        {
            string retVal = String.Empty;
            foreach (BillBookCheckinDetailInfo b in billBookDetails)
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ag_get_CaPaidBy");
                cmd.CommandTimeout = 600;
                db.AddInParameter(cmd, "pCaId", DbType.String, b.CaId);
                DataSet data = db.ExecuteDataSet(cmd, trans);
                if (data.Tables.Count > 0)
                {
                    if (data.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = data.Tables[0].Rows[0];
                        retVal = DaHelper.GetString(row, "PaidBy");
                        if (retVal != null && retVal != String.Empty)
                            break;
                    }
                }
            }
            return retVal;
        }

        public decimal? GetTaxRate(string vatCode)
        {

            decimal? retVal = 0;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            string sql = String.Empty;
            sql = @"SELECT ISNULL(TaxRate, 0) as  TaxRate FROM mt.TaxCode where TaxCode  = '{0}' ";
            sql = String.Format(sql, vatCode);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            DataSet data = db.ExecuteDataSet(cmd);
            if ((data.Tables.Count > 0) && (data.Tables[0].Rows.Count == 1))
            {
                retVal = DaHelper.GetDecimal(data.Tables[0].Rows[0], "TaxRate");
            }
            return retVal == null ? 0 : retVal / 100;
        }

        public DateTime? GetGroupInvoiceDueDate(DbTransaction trans, List<BillBookCheckinDetailInfo> billBookDetails)
        {
            DateTime? retVal = null;
            foreach (BillBookCheckinDetailInfo b in billBookDetails)
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ag_get_GroupInvoiceDueDate");
                cmd.CommandTimeout = 600;
                db.AddInParameter(cmd, "pInvoiceNo", DbType.String, b.InvoiceNo);
                DataSet data = db.ExecuteDataSet(cmd, trans);
                if (data.Tables.Count > 0)
                {
                    if (data.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = data.Tables[0].Rows[0];
                        retVal = DaHelper.GetDate(row, "DueDt2");
                        if (retVal != null)
                            break;
                    }
                }
            }
            return retVal;
        }

        public string GetTaxCode(DbTransaction trans, List<BillBookCheckinDetailInfo> billBookDetails)
        {
            string retVal = String.Empty;
            foreach (BillBookCheckinDetailInfo b in billBookDetails)
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ag_get_TaxCode");
                cmd.CommandTimeout = 600;
                db.AddInParameter(cmd, "pInvoiceNo", DbType.String, b.InvoiceNo);
                DataSet data = db.ExecuteDataSet(cmd, trans);
                if (data.Tables.Count > 0)
                {
                    DataRow row = data.Tables[0].Rows[0];
                    retVal = DaHelper.GetString(row, "TaxCode").Trim();
                    if (retVal != String.Empty)
                        break;
                }
            }
            return retVal;
        }

        public string GetCaByInvoiceNo(DbTransaction trans, string invoiceNo)
        {
            string retVal = String.Empty;

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_CAByInvoiceNo");
            db.AddInParameter(cmd, "pInvoiceNo", DbType.String, invoiceNo);
            DataSet data = db.ExecuteDataSet(cmd, trans);
            if (data.Tables.Count > 0)
            {
                DataRow row = data.Tables[0].Rows[0];
                retVal = DaHelper.GetString(row, "CaId");
            }

            return retVal;
        }

        public string GetBillKeeperNameByBillBook(string billBookId)
        {
            string retVal = String.Empty;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_BillKeeperNameByBillBook");
            db.AddInParameter(cmd, "pBillBookId", DbType.String, billBookId);
            DataSet data = db.ExecuteDataSet(cmd);
            if (data.Tables.Count > 0)
            {
                DataRow row = data.Tables[0].Rows[0];
                retVal = DaHelper.GetString(row, "BillKeeperName");
            }

            return retVal;
        }

        public bool CheckIsFullyPaid(BillBookCheckInInfo billBookCheckIn)
        {
            bool result = false;
            SqlDatabase db = (SqlDatabase)DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_chk_InvoiceIsFullyPaid");
            cmd.CommandTimeout = 600;
            db.AddInParameter(cmd, "pGroupInvoiceNo", DbType.String, billBookCheckIn.BookId);
            db.AddInParameter(cmd, "CheckInInvoice", SqlDbType.Structured, GetInvoiceCheckInList(billBookCheckIn.BillBookCheckInDetail));
            DataSet data = db.ExecuteDataSet(cmd);
            if ((data.Tables.Count > 0) && (data.Tables[0].Rows.Count > 0))
            {
                DataRow row = data.Tables[0].Rows[0];
                if (DaHelper.GetString(row, "result") == "TRUE")
                    result = true;
                else
                    result = false;

            }

            return result;

        }

        public bool CheckIsSubmitGroupSameDay(BillBookCheckInInfo billBookCheckIn)
        {
            bool result = false;
            SqlDatabase db = (SqlDatabase)DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_chk_SubmitGroupInvoiceSameDay");
            cmd.CommandTimeout = 600;
            db.AddInParameter(cmd, "pGroupInvoiceNo", DbType.String, billBookCheckIn.BookId);
            db.AddInParameter(cmd, "CheckInInvoice", SqlDbType.Structured, GetInvoiceCheckInList(billBookCheckIn.BillBookCheckInDetail));
            DataSet data = db.ExecuteDataSet(cmd);
            if ((data.Tables.Count > 0) && (data.Tables[0].Rows.Count > 0))
            {
                DataRow row = data.Tables[0].Rows[0];
                if (DaHelper.GetString(row, "result") == "TRUE")
                    result = true;
                else
                    result = false;

            }

            return result;

        }
        #endregion

        #region "Insert command"

        private InvoiceCollection GetInvoiceCheckInList(List<BillBookCheckinDetailInfo> detailList)
        {
            InvoiceCollection invCollect = new InvoiceCollection();
            foreach (BillBookCheckinDetailInfo item in detailList)
            {
                Invoice inv = new Invoice();
                inv.InvoiceNo	 = 	item.InvoiceNo;
                inv.Period	 = 	DaHelper.SetBillPeriod(item.Period);
                inv.MruId	 = 	item.MruId;
                inv.CaId	 = 	item.CaId;
                inv.CaName	 = 	item.CaName;
                inv.AbsId	 = 	item.AbsId;
                inv.AboId	 = 	item.AboId;
                inv.PmId	 = 	item.PmId;
                inv.InBook	 = 	item.InBook;
                inv.PaidAmount	 = 	item.PaidAmount == null? 0: item.PaidAmount.Value;
                inv.TotalAmount	 = 	item.TotalAmount== null? 0: item.TotalAmount.Value;
                inv.GAmount	 = 	item.GAmount==null? 0:item.GAmount.Value;
                inv.Vat	 = 	item.Vat==null? 0:item.Vat.Value;
                inv.BranchId	 = 	item.BranchId;
                inv.PaidType	 = 	item.PaidType;
                inv.ModifiedBy	 = 	item.ModifiedBy;
                inv.IsCheckIn	 = 	item.IsCheckIn;
                inv.TotalDebtAmount	 = 	item.TotalDebtAmount==null? 0 : item.TotalDebtAmount.Value;
                inv.ArActive	 = 	item.ARActive;
                inv.InvSel = item.InvSel;
                invCollect.Add(inv);
            }

            return invCollect;
        }

        private ChequeCollection GetChequeList(List<BillBookCheckinDetailInfo> detailList)
        {
            ChequeCollection chqCollect = new ChequeCollection();
            foreach (BillBookCheckinDetailInfo item in detailList)
            {
                foreach (ChequeInfo chq in item.ChequeList)
                {
                    Cheque q = new Cheque();
                    q.InvoiceNo = item.InvoiceNo;
                    q.Period = DaHelper.SetBillPeriod(chq.Period);
                    q.CaId = chq.CaId;
                    q.BankKey = chq.BankKey;
                    q.BankName = chq.BankName;
                    q.ChequeNo = chq.ChequeNo;
                    q.ChequeAmount = chq.ChequeAmount.Value;
                    q.ActualAmount = chq.ActualAmount.Value;
                    q.ChequeAccountNo = chq.ChequeAccountNo;
                    q.ChequeDt = chq.ChequeDt.Value;
                    chqCollect.Add(q);
                }
            }

            return chqCollect;
        }

        public void CheckInBillBook(BillBookCheckInInfo billBookCheckIn, string branchId, string terminalId, DbTransaction trans)
        {
            ChequeCollection chqCollection = GetChequeList(billBookCheckIn.BillBookCheckInDetail);
            SqlDatabase db = (SqlDatabase)DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_ins_CheckInBillBookMass");
            cmd.CommandTimeout = 600;
            db.AddInParameter(cmd, "pBillBookId", DbType.String, billBookCheckIn.BookId);
            db.AddInParameter(cmd, "pPassportId", DbType.Int32, Convert.ToInt32(DateTime.Now.ToString("MMddHHmmss", new CultureInfo("th-TH")))); 
            db.AddInParameter(cmd, "pBillAgentId", DbType.String, billBookCheckIn.BillAgentId);
            db.AddInParameter(cmd, "pBillCollectAmount", DbType.Decimal, billBookCheckIn.BillCollectAmount);
            db.AddInParameter(cmd, "pBillCollectCount", DbType.Int32, billBookCheckIn.BillCollectCount);
            db.AddInParameter(cmd, "pBookPeriod", DbType.String, DaHelper.SetBillPeriod(billBookCheckIn.BookPeriod));
            db.AddInParameter(cmd, "pTerminalId", DbType.String, terminalId);
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pModifiedBy", DbType.String, Session.User.Id);
            db.AddInParameter(cmd, "CheckInInvoice", SqlDbType.Structured, GetInvoiceCheckInList(billBookCheckIn.BillBookCheckInDetail));
            //this is an optional
            if(chqCollection.Count > 0)
                db.AddInParameter(cmd, "ChequeList", SqlDbType.Structured, chqCollection);

            db.ExecuteNonQuery(cmd, trans);
        }

        public void CheckInGroupInvoice(BillBookCheckInInfo groupInvCheckIn, string branchId, string terminalId, DbTransaction trans)
        {
            SqlDatabase db = (SqlDatabase)DatabaseFactory.CreateDatabase("POSDatabase");
            //Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_ins_CheckInGrpInvMass");
            cmd.CommandTimeout = 1200; //20 minutes
            db.AddInParameter(cmd, "pGroupInvoiceNo", DbType.String, groupInvCheckIn.BookId);
            db.AddInParameter(cmd, "pGroupPeriod", DbType.String, DaHelper.SetBillPeriod(groupInvCheckIn.BookPeriod));
            db.AddInParameter(cmd, "pBillCollectCount", DbType.Decimal, groupInvCheckIn.BillCollectCount);
            db.AddInParameter(cmd, "pBillCollectAmount", DbType.Decimal, groupInvCheckIn.BillCollectAmount);
            db.AddInParameter(cmd, "pTotalvat", DbType.Decimal, groupInvCheckIn.TotalVat);
            db.AddInParameter(cmd, "pTotalAmount", DbType.Decimal, groupInvCheckIn.TotalAmount);
            db.AddInParameter(cmd, "pTerminalId", DbType.String, terminalId);
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pPostedServerId", DbType.String, Session.Branch.PostedServerId);
            db.AddInParameter(cmd, "pModifiedBy", DbType.String, Session.User.Id);
            db.AddInParameter(cmd, "checkininvoice", SqlDbType.Structured, GetInvoiceCheckInList(groupInvCheckIn.BillBookCheckInDetail));
            db.ExecuteNonQuery(cmd, trans);
        }

        public string InsertAccountReceive(DbTransaction trans, AccountReceiveInfo ar, string postedServerId, string posId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_ins_AccountReceive");
            cmd.CommandTimeout = 600;
            //update advance payment         
            db.AddOutParameter(cmd, "pOutputNo", DbType.String, 35);
            db.AddInParameter(cmd, "pBookType", DbType.Int32, (int)ar.BookType);
            db.AddInParameter(cmd, "pCaId", DbType.String, ar.CaId);
            db.AddInParameter(cmd, "pBranchId", DbType.String, ar.BranchId);
            db.AddInParameter(cmd, "pBillBookId", DbType.String, ar.BillBookId);
            db.AddInParameter(cmd, "pGroupInvoiceNo", DbType.String, ar.GroupInvoiceNo);
            db.AddInParameter(cmd, "pDtId", DbType.String, ar.DtId);
            db.AddInParameter(cmd, "pDescription", DbType.String, ar.Description);
            db.AddInParameter(cmd, "pPeriod", DbType.String, ar.Period);
            db.AddInParameter(cmd, "pTaxCode", DbType.String, ar.TaxCode);
            db.AddInParameter(cmd, "pVatAmount", DbType.Decimal, ar.VatAmount);
            db.AddInParameter(cmd, "pAmount", DbType.Decimal, ar.Amount);
            db.AddInParameter(cmd, "pUnitPrice", DbType.Decimal, ar.UnitPrice);
            db.AddInParameter(cmd, "pGAmount", DbType.Currency, ar.GAmount);
            db.AddInParameter(cmd, "pDueDt", DbType.DateTime, ar.DueDt);
            db.AddInParameter(cmd, "pControllerId", DbType.String, ar.ControllerId);
            db.AddInParameter(cmd, "pPaidAmount", DbType.String, ar.PaidAmount);
            db.AddInParameter(cmd, "pCutOffDt", DbType.DateTime, ar.CutOffDt);
            db.AddInParameter(cmd, "pPostedServerId", DbType.String, postedServerId);
            db.AddInParameter(cmd, "pModifiedBy", DbType.String, ar.ModifiedBy);
            db.AddInParameter(cmd, "pPosId", DbType.String, posId.Trim());
            db.ExecuteNonQuery(cmd, trans);

            string outputNo = (string)db.GetParameterValue(cmd, "pOutputNo");
            return outputNo;
        }

        public string InsertPayment(DbTransaction trans, PaymentInfo payment, string postServer)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_ins_Payment");
            //update advance payment
            cmd.CommandTimeout = 600;
            db.AddOutParameter(cmd, "pPaymentId", DbType.String, 22);
            db.AddInParameter(cmd, "pPaymentDt", DbType.DateTime, payment.PaymentDt);
            db.AddInParameter(cmd, "pPosId", DbType.String, payment.PosId);
            db.AddInParameter(cmd, "pCashierId", DbType.String, payment.CashierId);
            db.AddInParameter(cmd, "pBranchId", DbType.String, payment.BranchId);
            db.AddInParameter(cmd, "pPostServerId", DbType.String, postServer);
            db.AddInParameter(cmd, "pModifiedBy", DbType.String, payment.ModifiedBy);
            db.ExecuteNonQuery(cmd, trans);
            string paymentId = (string)db.GetParameterValue(cmd, "pPaymentId");

            return paymentId;

        }

        public string InsertARPaymentType(DbTransaction trans, ARPaymentTypeInfo arPayment, string invoiceNo, string period, string branchId, string postServerId, string posId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_ins_ARPaymentType");
            cmd.CommandTimeout = 600;
            //update advance payment
            db.AddOutParameter(cmd, "pARPtId", DbType.String, 22);
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pPaymentId", DbType.String, arPayment.PaymentId);
            db.AddInParameter(cmd, "pActualAmount", DbType.Decimal, arPayment.ActualAmount);
            db.AddInParameter(cmd, "pAmount", DbType.Decimal, arPayment.Amount);
            db.AddInParameter(cmd, "pPtId", DbType.String, arPayment.PtId);
            db.AddInParameter(cmd, "pBankKey", DbType.String, arPayment.BankKey);
            db.AddInParameter(cmd, "pChqNo", DbType.String, arPayment.ChqNo);
            db.AddInParameter(cmd, "pChqAccNo", DbType.String, arPayment.ChqAccNo);
            db.AddInParameter(cmd, "pChqDt", DbType.DateTime, arPayment.ChqDt);
            db.AddInParameter(cmd, "pTranfAccNo", DbType.String, arPayment.TranfAccNo);
            db.AddInParameter(cmd, "pTranfDt", DbType.DateTime, arPayment.TranfDt);
            db.AddInParameter(cmd, "pModifiedBy", DbType.String, arPayment.ModifiedBy);
            db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, arPayment.ModifiedDt);
            db.AddInParameter(cmd, "pInvoiceNo", DbType.String, invoiceNo);
            db.AddInParameter(cmd, "pPeriod", DbType.String, DaHelper.SetBillPeriod(period));
            db.AddInParameter(cmd, "pPostServerId", DbType.String, postServerId);
            db.AddInParameter(cmd, "pPosId", DbType.String, posId.Trim());
            db.ExecuteNonQuery(cmd, trans);
            string _aRPtId = (string)db.GetParameterValue(cmd, "pARPtId");
            return _aRPtId;

        }

        public int InsertARPayment(DbTransaction trans, string billBookId, string invoiceNo, string period, string modifierBy,
                        string pmId, string branchId, int action, string postServerId, decimal? totalAmount, string posId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_upd_UpdateARPayment");
            cmd.CommandTimeout = 600;
            // Add data ot ARPyment & RTAPaymentTypeARPayment
            db.AddInParameter(cmd, "pBillBookId", DbType.String, billBookId);
            db.AddInParameter(cmd, "pTotalAmount", DbType.Decimal, totalAmount);
            db.AddInParameter(cmd, "pInvoiceNo", DbType.String, invoiceNo);
            db.AddInParameter(cmd, "pPeriod", DbType.String, period);
            db.AddInParameter(cmd, "pPmId", DbType.String, pmId);
            db.AddInParameter(cmd, "pPosId", DbType.String, posId.Trim());
            db.AddInParameter(cmd, "pPostedServerId", DbType.String, postServerId);
            db.AddInParameter(cmd, "pModifiedBy", DbType.String, modifierBy);
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pAction", DbType.Int32, action);
            return db.ExecuteNonQuery(cmd, trans);
        }

        public int InsertGroupInvoiceARPayment(DbTransaction trans, decimal? paidAmount, string billBookId, string invoiceNo, string period, string modifierBy,
                        string pmId, string branchId, int action, string postServerId, int isLastrec, string posId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_upd_UpdateGroupInvoiceARPayment");
            cmd.CommandTimeout = 600;
            // Add data ot ARPyment 
            db.AddInParameter(cmd, "pBillBookId", DbType.String, billBookId);
            db.AddInParameter(cmd, "pPaidAmount", DbType.Decimal, paidAmount);
            db.AddInParameter(cmd, "pInvoiceNo", DbType.String, invoiceNo);
            db.AddInParameter(cmd, "pPeriod", DbType.String, period);
            db.AddInParameter(cmd, "pPmId", DbType.String, pmId);
            db.AddInParameter(cmd, "pPostedServerId", DbType.String, postServerId);
            db.AddInParameter(cmd, "pModifiedBy", DbType.String, modifierBy);
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pAction", DbType.Int32, action);
            db.AddInParameter(cmd, "pIsLastRec", DbType.Int32, isLastrec);
            db.AddInParameter(cmd, "pPosId", DbType.String, posId.Trim());
            return db.ExecuteNonQuery(cmd, trans);
        }

        public BillBookHeaderInfo GetBillCollectedCountByAgecyCreateDate(string agencyId, string period, DateTime createDate)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_BillCountByAgecyCreateDate");
            BillBookHeaderInfo bookHeader = new BillBookHeaderInfo();

            db.AddInParameter(cmd, "pAgencyId", DbType.String, agencyId);
            db.AddInParameter(cmd, "pPeriod", DbType.String, period);
            db.AddInParameter(cmd, "pCreateDate", DbType.DateTime, createDate);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt != null)
            {
                DataRow row = dt.Rows[0];
                bookHeader.TotalBillCount = DaHelper.GetInt(row, "TotalBill");
                bookHeader.TotalBillCollected = DaHelper.GetInt(row, "TotalBillCollected");
            }
            return bookHeader;
        }

        public void BillBookSaveState(DbTransaction trans, string billbookId, string invoiceNo, string flag, string modifiedBy)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_upd_BillBookSaveState");
            cmd.CommandTimeout = 360;
            db.AddInParameter(cmd, "BillbookId", DbType.String, billbookId);
            db.AddInParameter(cmd, "InvoiceNo", DbType.String, invoiceNo);
            db.AddInParameter(cmd, "Flag", DbType.String, flag);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, modifiedBy);
            db.ExecuteNonQuery(cmd, trans);
        }

        #endregion

        #region "Update command"
        public int UpdateARStatus(DbTransaction trans, string invoiceNo, string period, string modifierBy, string billBookId, string postServerId)
        {
            BillBookCheckInInfo _bookCheckIn = new BillBookCheckInInfo();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_upd_ARStatus");
            cmd.CommandTimeout = 600;
            db.AddInParameter(cmd, "pInvoiceNo", DbType.String, invoiceNo);
            db.AddInParameter(cmd, "pModifiedBy", DbType.String, modifierBy);
            db.AddInParameter(cmd, "pPeriod", DbType.String, period);
            db.AddInParameter(cmd, "pBillBookId", DbType.String, billBookId);
            db.AddInParameter(cmd, "pPostServerId", DbType.String, postServerId);
            return db.ExecuteNonQuery(cmd, trans);
        }

        public int UpdateAgencyBillBookStatus(DbTransaction trans, string modifiedBy,
                        string billBookId, string aboId, string allowRepeated,
                        string absId, string invoiceNo, string inBook,
                        decimal? paidAmount, DateTime? lastPaidDt, DateTime? modifiedDt,
                        int paidType, string postServerId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_upd_AgencyBillBookStatus");
            cmd.CommandTimeout = 600;
            db.AddInParameter(cmd, "pBillBookId", DbType.String, billBookId);
            db.AddInParameter(cmd, "pInvoiceNo", DbType.String, invoiceNo);
            db.AddInParameter(cmd, "pAboId", DbType.String, aboId);
            db.AddInParameter(cmd, "pAllowRepeated", DbType.String, allowRepeated);
            db.AddInParameter(cmd, "pAbsId", DbType.String, absId);
            db.AddInParameter(cmd, "pInBook", DbType.String, inBook);
            db.AddInParameter(cmd, "pModifiedBy", DbType.String, modifiedBy);
            db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, modifiedDt);
            db.AddInParameter(cmd, "pPaidType", DbType.Int32, paidType);
            db.AddInParameter(cmd, "pPostServerId", DbType.String, postServerId);
            return db.ExecuteNonQuery(cmd, trans);
        }

        public int UpdateTotalCollected(DbTransaction trans, string billBookId, string modifiedBy)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_upd_SumBillBookCheckIn");
            cmd.CommandTimeout = 600;
            db.AddInParameter(cmd, "pBillBookId", DbType.String, billBookId);
            db.AddInParameter(cmd, "pModifiedBy", DbType.String, modifiedBy);
            return db.ExecuteNonQuery(cmd, trans);
        }

        #endregion

        #region "Delete command"

        public bool DeleteBillBookCheckIn(DbTransaction trans, BillBookCheckInInfo billBookCheckIn, string modifiedBy)
        {
            SqlDatabase db = (SqlDatabase)DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_ins_CancelBillBookCheckIn");
            cmd.CommandTimeout = 1200; //20 minutes
            db.AddInParameter(cmd, "pBillBookId", DbType.String, billBookCheckIn.BookId);
            db.AddInParameter(cmd, "pModifiedBy", DbType.String, modifiedBy);
            db.AddInParameter(cmd, "pNote", DbType.String, billBookCheckIn.Note);
            db.AddInParameter(cmd, "checkininvoice", SqlDbType.Structured, GetInvoiceCheckInList(billBookCheckIn.BillBookCheckInDetail));
            db.ExecuteNonQuery(cmd, trans);
            return true;
        }

        #region IBillBookRepository Members


        public bool DeleteARInformation(DbTransaction trans, BillBookCheckInInfo billBookInfo, string modifiedBy)
        {
            throw new NotImplementedException();
        }

        public bool DeleteBillBookCheckInDetail(DbTransaction trans, string billBookId, string invoiceNo, string modifiedBy, DateTime? modifiedDt)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion

        #region "Helper"
        private bool IsCurrentPeriod(string period)
        {
            bool retVal = false;
            // mm/yyyy          
            string _month = String.Empty;
            string _year = String.Empty;
            string _currentDate = this.ConvertToShortThaiDateTime(Session.BpmDateTime.Now);
            string _currMonth = _currentDate.Substring(3, 2);
            string _currYear = _currentDate.Substring(6, 4);
            if (period.Length == 7)
            {
                _month = period.Substring(0, 2);
                _year = period.Substring(3, 4);
                if ((_month == _currMonth) && (_year == _currYear))
                {
                    retVal = true;
                }
            }
            return retVal;
        }

        public string ConvertToShortThaiDateTime(DateTime? dateValue)
        {
            string retVal = String.Empty;
            try
            {
                if (dateValue != null)
                {
                    DateTime dt = (DateTime)dateValue;
                    DateTimeFormatInfo _th_dt;
                    CultureInfo th_culture = new CultureInfo("th-TH");
                    _th_dt = th_culture.DateTimeFormat;
                    retVal = dt.ToString("dd/MM/yyyy", _th_dt);
                }
            }
            catch { }
            return retVal;
        }

        #endregion       

        public void EnqueueBatchJobBillBook(string batchName, string[] param)
        {
            string destination = System.Configuration.ConfigurationManager.AppSettings["Destination"];

            BatchExecutionRequest request = new BatchExecutionRequest(batchName);
            request.BatchClientName = "";
            request.Destination = destination;
            request.StartPollingForResultAfter = TimeSpan.FromMinutes(5);

            ParameterData parem = new ParameterData();
            parem.Name = "BillBookId";
            parem.Value = param[0];
            request.Parameters.Add(parem);



            BatchExecutionRequestQueue queue = new BatchExecutionRequestQueue("ACABatch_SQL");
            queue.Enqueue(request);
        }
    }

    public class Cheque
    {
        public string InvoiceNo { get; set; }
        public string Period { get; set; }
        public string CaId { get; set; }
        public string BankKey { get; set; }
        public string BankName { get; set; }
        public string ChequeNo { get; set; }
        public decimal ChequeAmount { get; set; }
        public decimal ActualAmount { get; set; }
        public string ChequeAccountNo { get; set; }
        public DateTime ChequeDt { get; set; }

        public Cheque() { }

        public Cheque(string bookId, string invoiceNo, string period, string caId, string bankKey, string bankName, string chequeNo, 
                        decimal chequeAmount, decimal actualAmount, string chequeAccountNo, DateTime chequeDt)
        {
            InvoiceNo = invoiceNo;
            Period = period;
            CaId = caId;
            BankKey = bankKey;
            BankName = bankName;
            ChequeNo = chequeNo;
            ChequeAmount = chequeAmount;
            ActualAmount = actualAmount;
            ChequeAccountNo = chequeAccountNo;
            ChequeDt = chequeDt;
        }
    }

    public class ChequeCollection : List<Cheque>, IEnumerable<SqlDataRecord>
    {
        IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
        {
            SqlDataRecord ret = new SqlDataRecord(
                    new SqlMetaData("InvoiceNo", SqlDbType.Char, 22),
                    new SqlMetaData("Period", SqlDbType.Char, 6),
                    new SqlMetaData("CaId", SqlDbType.Char, 12),
                    new SqlMetaData("BankKey", SqlDbType.Char, 7),
                    new SqlMetaData("BankName", SqlDbType.VarChar, 60),
                    new SqlMetaData("ChequeNo", SqlDbType.Char, 20),
                    new SqlMetaData("ChequeAmount", SqlDbType.Money),
                    new SqlMetaData("ActualAmount", SqlDbType.Money),
                    new SqlMetaData("ChequeAccountNo", SqlDbType.Char, 20),
                    new SqlMetaData("ChequeDt", SqlDbType.DateTime)
                );

            foreach (Cheque chq in this)
            {
                ret.SetString(0, chq.InvoiceNo);
                ret.SetString(1, chq.Period);
                ret.SetString(2, chq.CaId);
                ret.SetString(3, chq.BankKey);
                ret.SetString(4, chq.BankName);
                ret.SetString(5, chq.ChequeNo);
                ret.SetDecimal(6, chq.ChequeAmount);
                ret.SetDecimal(7, chq.ActualAmount);
                ret.SetString(8, chq.ChequeAccountNo);
                ret.SetDateTime(9, chq.ChequeDt);
                yield return ret;
            }
        }
    }

    public class Invoice
    {
        public string InvoiceNo { get; set; }
        public string Period { get; set; }
        public string MruId { get; set; }
        public string CaId { get; set; }
        public string CaName { get; set; }
        public string AbsId { get; set; }
        public string AboId { get; set; }
        public string PmId { get; set; }
        public string InBook { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal GAmount { get; set; }
        public decimal Vat { get; set; }
        public string BranchId { get; set; }
        public int PaidType { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsCheckIn { get; set; }
        public decimal TotalDebtAmount { get; set; }
        public bool ArActive { get; set; }
        public bool InvSel { get; set; }

        public Invoice() { }

        public Invoice(string invoiceNo, string period, string mruId, string caId, string caName, string absId, string aboId,
                            string pmId, string inBook, decimal paidAmount, decimal totalAmount, decimal gAmount, decimal vat, string branchId, 
                            int paidType, string modifiedBy, bool isCheckIn, decimal totalDebtAmount,
                            bool arActive, bool invSel)
        {
            InvoiceNo = invoiceNo;
            Period = period;
            BranchId = branchId;
            MruId = mruId;
            CaId = caId;
            CaName = caName;
            AbsId = absId;
            AboId = aboId;
            PmId = pmId;
            InBook = inBook;
            PaidAmount = paidAmount;
            TotalAmount = totalAmount;
            GAmount = gAmount;
            Vat = vat;
            PaidType = paidType;
            ModifiedBy = modifiedBy;
            IsCheckIn = isCheckIn;
            TotalDebtAmount = totalDebtAmount;
            ArActive = arActive;
            InvSel = invSel;
        }
    }

    public class InvoiceCollection : List<Invoice>, IEnumerable<SqlDataRecord>
    {
        IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
        {
            SqlDataRecord ret = new SqlDataRecord(
                new SqlMetaData("InvoiceNo", SqlDbType.Char, 22),
                new SqlMetaData("Period", SqlDbType.Char, 6),
                new SqlMetaData("BranchId", SqlDbType.Char, 6),
                new SqlMetaData("MruId", SqlDbType.Char, 4),
                new SqlMetaData("CaId", SqlDbType.Char, 12),
                new SqlMetaData("CaName", SqlDbType.VarChar, 100),
                new SqlMetaData("AbsId", SqlDbType.Char, 1),
                new SqlMetaData("AboId", SqlDbType.Char, 1),
                new SqlMetaData("PmId", SqlDbType.Char, 1),
                new SqlMetaData("InBook", SqlDbType.Char, 1),
                new SqlMetaData("PaidAmount", SqlDbType.Money),
                new SqlMetaData("TotalAmount", SqlDbType.Money),
                new SqlMetaData("GAmount", SqlDbType.Money),
                new SqlMetaData("Vat", SqlDbType.Money),
                new SqlMetaData("PaidType", SqlDbType.Int),
                new SqlMetaData("ModifiedBy", SqlDbType.Char, 8),
                new SqlMetaData("IsCheckIn", SqlDbType.Int),
                new SqlMetaData("TotalDebtAmount", SqlDbType.Money),
                new SqlMetaData("ArActive", SqlDbType.Int),
                new SqlMetaData("InvSel", SqlDbType.Int)
                );

            foreach (Invoice inv in this)
            {
                ret.SetString(0,  inv.InvoiceNo);
                ret.SetString(1,  inv.Period);
                ret.SetString(2, inv.BranchId);
                ret.SetString(3,  inv.MruId);
                ret.SetString(4,  inv.CaId);
                ret.SetString(5,  inv.CaName);
                ret.SetString(6,  inv.AbsId);
                ret.SetString(7,  inv.AboId);
                ret.SetString(8,  inv.PmId);
                ret.SetString(9,  inv.InBook);
                ret.SetDecimal(10, inv.PaidAmount);
                ret.SetDecimal(11, inv.TotalAmount);
                ret.SetDecimal(12, inv.GAmount);
                ret.SetDecimal(13, inv.Vat);
                ret.SetInt32(14, inv.PaidType);
                ret.SetString(15, inv.ModifiedBy);
                ret.SetInt32(16, inv.IsCheckIn ? 1 : 0);
                ret.SetDecimal(17, inv.TotalDebtAmount);
                ret.SetInt32(18, inv.ArActive ? 1 : 0);
                ret.SetInt32(19, inv.InvSel ? 1 : 0);
                yield return ret;
            }
        }

      

    }
}
