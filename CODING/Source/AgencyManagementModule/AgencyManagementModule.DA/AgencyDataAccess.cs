using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading;
using System.Data.Common;
using System.Globalization;
using System.ComponentModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PEA.BPM.AgencyManagementModule.Interface.Services;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.AgencyManagementModule.Interface.Constants;


namespace PEA.BPM.AgencyManagementModule.DA
{
    public class AgencyDataAccess : IAgencyRepository
    {
        private int loopCount;
        private DateTimeFormatInfo _th_dt;
        private Hashtable _invoicePaidBy;

        public AgencyDataAccess()
        {
            CultureInfo th_culture = new CultureInfo("th-TH");
            _th_dt = th_culture.DateTimeFormat;

            _invoicePaidBy = new Hashtable();
        }

        public IAgencyRepository NewInstance()
        {
            return new AgencyDataAccess();
        }

        #region Get ONE Returned Record/Value Members


        public decimal? GetAgentActiveBookAmount(string agentId)
        {
            decimal? totalBookAmount = 0;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_AgencyActiveBookTotalAmount");
            db.AddInParameter(cmd, "AgentId", DbType.String, agentId.PadLeft(12, '0'));
            object obj = db.ExecuteScalar(cmd);
            if (obj != System.DBNull.Value)
                totalBookAmount = (decimal)obj;

            return totalBookAmount;
        }

        public decimal? GetAgentBookAdvPaidAmount(string agentId)
        {
            decimal? advPaidAmount = 0;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_AgencyActiveBookAdvPaidAmount");
            db.AddInParameter(cmd, "AgentId", DbType.String, agentId.PadLeft(12, '0'));
            object obj = db.ExecuteScalar(cmd);
            if (obj != System.DBNull.Value)
                advPaidAmount = (decimal?)obj;

            return advPaidAmount;
        }

        public bool HasBillbookCaculated(string bookId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_HasBookCalForCommission");
            db.AddInParameter(cmd, "BillBookId", DbType.String, bookId);
            int ret = (int)db.ExecuteScalar(cmd);

            if (ret == 0)
                return false;
            else
                return true;
        }

        public bool IsInvoiceCancelled(string invoiceNo)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_IsInvoiceCancelled");
            db.AddInParameter(cmd, "InvoiceNo", DbType.String, invoiceNo);
            int ret = (int)db.ExecuteScalar(cmd);

            if (ret == 0)
                return false;
            else
                return true;
        }

        public int? GetBookLotNumber(string agentId, string period)
        {          
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_BookLot");
            db.AddInParameter(cmd, "AgentId", DbType.String, agentId.PadLeft(12, '0'));
            db.AddInParameter(cmd, "CreateDt", DbType.DateTime, Session.BpmDateTime.Now);
            db.AddInParameter(cmd, "Period", DbType.String, period);
            db.AddOutParameter(cmd, "NewLot", DbType.Int32, 4);
            db.ExecuteNonQuery(cmd);

            int? ret = (int)db.GetParameterValue(cmd, "NewLot");
            return ret;
        }

        public FeeBaseElement GetBaseCommissionRate(BookSearchInfo searchInfo)
        {
            FeeBaseElement feeBase = new FeeBaseElement();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_BaseCommissionBookRate");
            db.AddInParameter(cmd, "BookPeriod", DbType.String, searchInfo.BillPeriod);
            db.AddInParameter(cmd, "AgentId", DbType.String, searchInfo.AgentId.PadLeft(12, '0'));
            db.AddInParameter(cmd, "CreateDate", DbType.DateTime, searchInfo.CreateDate);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count == 0) return null ;

            DataRow r = dt.Rows[0];
            feeBase.HouseRegRate = DaHelper.GetDecimal(r, "HouseRegRate");
            feeBase.HouseGrpRate = DaHelper.GetDecimal(r, "HouseGrpRate");
            feeBase.CorpRegRate = DaHelper.GetDecimal(r, "CorpRegRate");
            feeBase.CorpGrpRate = DaHelper.GetDecimal(r, "CorpGrpRate");
            feeBase.GovRegRate = DaHelper.GetDecimal(r, "GovRegRate");
            feeBase.GovGrpRate = DaHelper.GetDecimal(r, "GovGrpRate");
            feeBase.MaxInvoicePercent = DaHelper.GetDecimal(r, "MaxInvPercent");
            feeBase.InvoiceRate = DaHelper.GetDecimal(r, "InvRate");
            feeBase.InvoicePastThreeMonthRate = DaHelper.GetDecimal(r, "InvPastThreeMonthRate");
            feeBase.BillingNinetyPercent = DaHelper.GetDecimal(r, "BillingNinetyPercent");
            feeBase.BillingNinetyNinePercent = DaHelper.GetDecimal(r, "BillingNinetyNinePercent");
            feeBase.BillingHundredPercent = DaHelper.GetDecimal(r, "BillingHundredPercent");
            feeBase.FineRatePerBill = DaHelper.GetDecimal(r, "FineRatePerBill");
            feeBase.HasCommissionCalLimit = DaHelper.GetString(dt.Rows[0], "CmCountBlock") == "1" ? true : false;
            feeBase.MaxCommissionCalCount = DaHelper.GetInt(dt.Rows[0], "CmCountLimit");
            feeBase.VatRate = DaHelper.GetDecimal(dt.Rows[0], "VatRate") == null ? 0 : DaHelper.GetDecimal(dt.Rows[0], "VatRate");
            feeBase.TaxRate = DaHelper.GetDecimal(dt.Rows[0], "TaxRate") == null ? 0 : DaHelper.GetDecimal(dt.Rows[0], "TaxRate");
            feeBase.CollectedPercent = DaHelper.GetDecimal(dt.Rows[0], "CollectedPercent");
            feeBase.CaCount = DaHelper.GetInt(dt.Rows[0], "CaCount");
            feeBase.UpperRate = DaHelper.GetDecimal(dt.Rows[0], "UpperRate");
            feeBase.LowerRate = DaHelper.GetDecimal(dt.Rows[0], "LowerRate");

            //cmd = db.GetStoredProcCommand("ag_get_AgencyConfig");
            //dt = db.ExecuteDataSet(cmd).Tables[0];
           
            //if (dt.Rows.Count != 0)
            //{                
            //    feeBase.HasCommissionCalLimit = DaHelper.GetString(dt.Rows[0], "CmCountBlock") == "1" ? true : false;
            //    feeBase.MaxCommissionCalCount = DaHelper.GetInt(dt.Rows[0], "CmCountLimit");
            //    feeBase.VatRate = DaHelper.GetDecimal(dt.Rows[0], "VatRate") == null ? 0 : DaHelper.GetDecimal(dt.Rows[0], "VatRate");
            //    feeBase.TaxRate = DaHelper.GetDecimal(dt.Rows[0], "TaxRate") == null ? 0 : DaHelper.GetDecimal(dt.Rows[0], "TaxRate");
            //    feeBase.CollectedPercent = DaHelper.GetDecimal(dt.Rows[0], "CollectedPercent");
            //    feeBase.CaCount = DaHelper.GetInt(dt.Rows[0], "CaCount");
            //    feeBase.UpperRate = DaHelper.GetDecimal(dt.Rows[0], "UpperRate");
            //    feeBase.LowerRate = DaHelper.GetDecimal(dt.Rows[0], "LowerRate");
            //}

            return feeBase;
        }

        public FeeBaseElement GetBaseCommissionRate(string branchId)
        {
            FeeBaseElement feeBase = new FeeBaseElement();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_BaseCommissionRate");
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count == 0) return feeBase;

            DataRow r = dt.Rows[0];
            feeBase.HouseRegRate = DaHelper.GetDecimal(r, "HouseRegRate");
            feeBase.HouseGrpRate = DaHelper.GetDecimal(r, "HouseGrpRate");
            feeBase.CorpRegRate = DaHelper.GetDecimal(r, "CorpRegRate");
            feeBase.CorpGrpRate = DaHelper.GetDecimal(r, "CorpGrpRate");
            feeBase.GovRegRate = DaHelper.GetDecimal(r, "GovRegRate");
            feeBase.GovGrpRate = DaHelper.GetDecimal(r, "GovGrpRate");
            feeBase.MaxInvoicePercent = DaHelper.GetDecimal(r, "MaxInvPercent");
            feeBase.InvoiceRate = DaHelper.GetDecimal(r, "InvRate");
            feeBase.InvoicePastThreeMonthRate = DaHelper.GetDecimal(r, "InvPastThreeMonthRate");
            feeBase.BillingNinetyPercent = DaHelper.GetDecimal(r, "BillingNinetyPercent");
            feeBase.BillingNinetyNinePercent = DaHelper.GetDecimal(r, "BillingNinetyNinePercent");
            feeBase.BillingHundredPercent = DaHelper.GetDecimal(r, "BillingHundredPercent");
            feeBase.FineRatePerBill = DaHelper.GetDecimal(r, "FineRatePerBill");                        
            feeBase.HasCommissionCalLimit = DaHelper.GetInt(dt.Rows[0], "CmCountBlock") == 1 ? true : false;
            feeBase.MaxCommissionCalCount = DaHelper.GetInt(dt.Rows[0], "CmCountLimit");
            feeBase.VatRate = DaHelper.GetDecimal(dt.Rows[0], "VatRate") == null ? 0 : DaHelper.GetDecimal(dt.Rows[0], "VatRate");
            //feeBase.TaxRate = DaHelper.GetDecimal(dt.Rows[0], "TaxRate") == null ? 0 : DaHelper.GetDecimal(dt.Rows[0], "TaxRate");
            feeBase.CollectedPercent = DaHelper.GetDecimal(dt.Rows[0], "CollectedPercent");
            feeBase.CaCount = DaHelper.GetInt(dt.Rows[0], "CaCount");
            feeBase.UpperRate = DaHelper.GetDecimal(dt.Rows[0], "UpperRate");
            feeBase.LowerRate = DaHelper.GetDecimal(dt.Rows[0], "LowerRate");
            feeBase.PenaltyWaive = DaHelper.GetString(dt.Rows[0], "PenaltyWaive") == "1" ? true : false;            

            return feeBase;
        }

        public string GetAgentIdByBookId(string bookId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_AgentByBookId");
            db.AddInParameter(cmd, "BillBookId", DbType.String, bookId);
            string agentId = (string)db.ExecuteScalar(cmd);

            return agentId;
        }

        public int GetNewCalCountOfCommissionPeriod(string agentId, string period)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_NewCommissionCalCount");
            db.AddInParameter(cmd, "AgentId", DbType.String, agentId.PadLeft(12, '0'));
            db.AddInParameter(cmd, "Period", DbType.String, period);
            return (int)db.ExecuteScalar(cmd);
        }


        public int GetBillBookCountByCreateDate(string bookPeriod, string agentId, DateTime createDt)
        {
            //the book MUST not have been caculated for commission
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_BillBookCountByCreateDate");
            db.AddInParameter(cmd, "AgentId", DbType.String, agentId.PadLeft(12, '0'));
            db.AddInParameter(cmd, "BookPeriod", DbType.String, bookPeriod);
            db.AddInParameter(cmd, "CreateDt", DbType.DateTime, createDt);
            return (int)db.ExecuteScalar(cmd);
        }

        public bool GetBillBookCountNotPaid(string billBookId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_ARByBillBookId");
            db.AddInParameter(cmd, "BillBookId", DbType.String, billBookId);
            db.AddInParameter(cmd, "BsId", DbType.String, "T");
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public bool IsAlreadyAdvPaid(string billBookId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_IsAlreadyAdvPaid");
            db.AddInParameter(cmd, "BillBookId", DbType.String, billBookId);
            object obj = db.ExecuteScalar(cmd);

            if (obj != System.DBNull.Value)
            {
                decimal paid = (decimal)obj;
                if (paid > 0) return true;
                else return false;
            }
            else return false;
        }


        public BindingList<CallingBillInfo> RetreiveBookDetail(string billBookId)
        {
            BindingList<CallingBillInfo> billList = new BindingList<CallingBillInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_RetreiveBookDetail");
            db.AddInParameter(cmd, "BillBookId", DbType.String, billBookId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            //reteive it as billstatus format
            return FillCallingBillInfo(dt);
        }

        public BindingList<CallingBillInfo> RetreiveBookLineDetail(string billBookId, string[] line)
        {
            string branchId = line[0];
            string lineId = line[1];
            //get all bills of this book - filtered by BillPeriod type
            BindingList<CallingBillInfo> billList = new BindingList<CallingBillInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_RetreiveBookDetail");
            db.AddInParameter(cmd, "BillBookId", DbType.String, billBookId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            FilterByLineId(dt, branchId, lineId);
            //reteive it as billstatus format
            return FillCallingBillInfo(dt);
        }

        public BindingList<CallingBillSummaryInfo> RetreiveBookSummary(string billBookId, string period)
        {
            BindingList<CallingBillInfo> billList = new BindingList<CallingBillInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_RetreiveBookDetail");
            db.AddInParameter(cmd, "BillBookId", DbType.String, billBookId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            //reteive it as billstatus format
      
            return FillCallingBillSummaryInformation(dt, UnFormatPeriod(period));
        }


        public DataTable GetBillBookIdWithCutStatusByAgentId(string bookPeriod, string agentId, DateTime createDt)
        {
            //the book MUST not have been caculated for commission
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_AgencyBillBookIdWithCutStaus");
            db.AddInParameter(cmd, "AgentId", DbType.String, agentId.PadLeft(12, '0'));
            db.AddInParameter(cmd, "BookPeriod", DbType.String, bookPeriod);
            db.AddInParameter(cmd, "CreateDt", DbType.DateTime, createDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            return dt;
        }


        public DataTable GetBillBookIdWithCutStatusByAgentIdReprint(string bookPeriod, string agentId, DateTime createDt)
        {
            //the book MUST not have been caculated for commission
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_AgencyBillBookIdWithCutStaus_Reprint");
            db.AddInParameter(cmd, "AgentId", DbType.String, agentId.PadLeft(12, '0'));
            db.AddInParameter(cmd, "BookPeriod", DbType.String, bookPeriod);
            db.AddInParameter(cmd, "CreateDt", DbType.DateTime, createDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            return dt;
        }

        public bool IsBillBookAlreadyCheckedIn(string bookId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_IsBookIdAlreadyCheckedIn");
            db.AddInParameter(cmd, "BookId", DbType.String, bookId);
            int ret = (int)db.ExecuteScalar(cmd);

            if (ret == 0) return false;
            else return true;
        }

        public bool IsBillBookAlreadyCancel(string bookId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_IsBookIdAlreadyCancel");
            db.AddInParameter(cmd, "BookId", DbType.String, bookId);
            int ret = (int)db.ExecuteScalar(cmd);

            if (ret == 0) return false;
            else return true;
        }

        public bool IsBillBookAlreadyCreate(string bookId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_IsBookIdAlreadyCreate");
            db.AddInParameter(cmd, "BookId", DbType.String, bookId);
            int ret = (int)db.ExecuteScalar(cmd);

            if (ret == 0) return false;
            else return true;
        }

        public string GetBusinessPartnerTypeOfAgent(string agentId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_agencyBpType");
            db.AddInParameter(cmd, "CaId", DbType.String, agentId.PadLeft(12, '0'));
            string bpType = (string)db.ExecuteScalar(cmd);

            return bpType;
        }

        public AgentInfo GetEmployeeInformation(string employeeId)
        {           
            AgentInfo agentInfo = new AgentInfo();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_EmployeeInfo");
            db.AddInParameter(cmd, "EmployeeId", DbType.String, employeeId.PadRight(12, ' '));
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count == 0) return new AgentInfo() ;

            //expect only one row
            DataRow r = dt.Rows[0];
            agentInfo.Id = String.Format("EMP0{0}",DaHelper.GetString(r, "EmployeeID"));
            agentInfo.Name = string.Format("{0} {1}", r["FirstName"].ToString(), r["LastName"].ToString());
            agentInfo.Type = "พนักงาน กฟฟ.";
            agentInfo.IsAgency = false;
            agentInfo.Deposit = 0;
            agentInfo.Address = "N/A";
            agentInfo.TechBranchId = DaHelper.GetString(r, "BranchId");            
            return agentInfo;
        }


        //public TaxInfo GetAgentTaxInformation(string agentId)
        //{
        //    TaxInfo taxInfo = new TaxInfo();
        //    Database db = DatabaseFactory.CreateDatabase("POSDatabase");
        //    DbCommand cmd = db.GetStoredProcCommand("ag_get_AgentTaxInfo");
        //    db.AddInParameter(cmd, "AgentId", DbType.String, agentId.PadLeft(12, '0'));
        //    DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
        //    if (dt.Rows.Count == 0) return new TaxInfo();

        //    //expect only one row
        //    DataRow r = dt.Rows[0];
        //    taxInfo.AgentId = agentId;
        //    //taxInfo = DaHelper.GetString(r, "BranchId");
        //    taxInfo.ValidFrom = DaHelper.GetDate(r, "ValidFrom");
        //    taxInfo.ValidTo = DaHelper.GetDate(r, "ValidTo");
        //    taxInfo.TaxCodeId = DaHelper.GetString(r, "TaxId");
        //    taxInfo.Percent = DaHelper.GetDecimal(r, "PayPerc");
        //    return taxInfo;
        //}


        public AgentInfo GetAgentInformation(string agentId)
        {
            AgentInfo agentInfo = new AgentInfo();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_AgentInfo");
            db.AddInParameter(cmd, "AgentId", DbType.String, agentId.PadLeft(12, '0'));
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count == 0) return agentInfo;

            //expect only one row
            DataRow r = dt.Rows[0];
            agentInfo.Id = agentId;
            agentInfo.Name = DaHelper.GetString(r, "AgentName");
            agentInfo.Type = DaHelper.GetString(r, "AgentType");
            //agentInfo.TypeId = DaHelper.GetString(r, "TypeId");
            agentInfo.Deposit = DaHelper.GetDecimal(r, "TotalAsset");
            agentInfo.Address = DaHelper.GetString(r, "Address");
            agentInfo.TechBranchId = DaHelper.GetString(r, "TechBranchId");
            agentInfo.PenaltyWaiveFlag = DaHelper.GetString(r, "PenaltyWaiveFlag") == "0" ? false : true;
            agentInfo.ContractValidFrom = DaHelper.GetDate(r, "ContractValidFrom");
            agentInfo.ContractValidTo = DaHelper.GetDate(r, "ContractValidTo");
            agentInfo.IsAgency = true;
            agentInfo.IsPersonalBpType = DaHelper.GetString(r, "BpTypeId") == "2" ? false : true;
            return agentInfo;
        }

        public bool IsChildBranch(string parent, string child)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_IsChildBranch");
            db.AddInParameter(cmd, "ParentBranchId", DbType.String, parent);
            db.AddInParameter(cmd, "ChildBranchId", DbType.String, child);
            int ret = (int)db.ExecuteScalar(cmd);

            if (ret == 0) return false;
            else return true;
        }

        //return billbookId
        public string IsReceiveCountExist(BillBookHeaderInfo header)
        {
            string period = header.Period.Substring(3, 4) + header.Period.Substring(0, 2);
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_IsReceiveCountExist");
            db.AddInParameter(cmd, "AgentId", DbType.String, header.AgentId.PadLeft(12, '0'));
            db.AddInParameter(cmd, "Period", DbType.String, period);
            db.AddInParameter(cmd, "ReceiveCount", DbType.Byte, header.ReceiveCount);
            db.AddInParameter(cmd, "BookLot", DbType.Int32, header.BookLot);
            object result = db.ExecuteScalar(cmd);
            return (string)result;
        }

        public PeaInfo GetBranchInformation(string branchId)
        {
            PeaInfo peaInfo = new PeaInfo();

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_get_Branch");
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count == 0) return new PeaInfo();

            //expect only one row
            DataRow r = dt.Rows[0];
            peaInfo.Id = DaHelper.GetString(r, "BranchId");
            peaInfo.Name = DaHelper.GetString(r, "BranchName");
            peaInfo.Address = DaHelper.GetString(r, "Address");
            peaInfo.BranchNo = DaHelper.GetString(r, "BranchNo");
            peaInfo.BranchLevel = r["BranchLevel"].ToString();

            return peaInfo;
        }

        public string[] GetLineId(string caId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_MRUByCaId");
            db.AddInParameter(cmd, "CaId", DbType.String, caId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count == 0) return new string[1];
            //expect only one row
            DataRow r = dt.Rows[0];
            string[] ret = new string[2];
            ret[0] = DaHelper.GetString(r, "CommBranchId");
            ret[1] = DaHelper.GetString(r, "MruId");

            return ret;
        }

        public LineInfo GetLineInformation(string lineId, string branchId)
        {
            LineInfo lineInfo = new LineInfo();

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_MRU");
            db.AddInParameter(cmd, "MruId", DbType.String, lineId);
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count == 0) return new LineInfo();

            //expect only one row
            DataRow r = dt.Rows[0];
            lineInfo.BranchId = branchId;
            lineInfo.LineId = DaHelper.GetString(r, "MruId");
            lineInfo.PortionDesc = DaHelper.GetString(r, "PortionDesc");
            lineInfo.NumOfCustomer = GetNumOfCaByMruId(lineInfo.BranchId, lineInfo.LineId);
            lineInfo.ControllerId = DaHelper.GetString(r, "ControllerId");
            if (DaHelper.GetString(r, "AdvPay") == null)
            {
                lineInfo.AdvPayment = true;                
            }
            else
            {
                lineInfo.AdvPayment = DaHelper.GetString(r, "AdvPay") == "1" ? true : false;                
            }
            lineInfo.TravelHelp = DaHelper.GetString(r, "TravelHelp") == null ? (int)TravelHelpEnum.NORMALTRAVELHELP : Convert.ToInt32(DaHelper.GetString(r, "TravelHelp"));
            //lineInfo.ValidTo = DaHelper.GetDate(r, "HelpValidTo");
            //lineInfo.ValidFrom = DaHelper.GetDate(r, "HelpValidFrom");
            lineInfo.AgentId = DaHelper.GetString(r, "CaId");
            lineInfo.AgentId = lineInfo.AgentId == null ? string.Empty : lineInfo.AgentId.Substring(4, 8);
            lineInfo.AgentName = DaHelper.GetString(r, "CaName");

            return lineInfo;
        }

        public int? GetNewReceiveCountOfAgent(string agentId, string period, int bookLot)
        {
            int y = Convert.ToInt32(period.Substring(3, 4));
            string repPeriod = y + period.Substring(0, 2);
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            int? rc = null;
            DbCommand cmd = db.GetStoredProcCommand("ag_get_NewAgentReceiveCount");
            db.AddInParameter(cmd, "BookHolderId", DbType.String, agentId.PadLeft(12, '0'));
            db.AddInParameter(cmd, "Period", DbType.String, repPeriod);
            db.AddInParameter(cmd, "BookLot", DbType.Int32, bookLot.ToString().PadLeft(12, '0'));
            rc = (int)db.ExecuteScalar(cmd);
            return rc;
        }

        public DataTable GetBillBookHeaderInfo(string billBookId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_BillBookHeaderInfo");
            db.AddInParameter(cmd, "BillBookId", DbType.String, billBookId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            return dt;
        }



        /// <summary>
        /// Max collectcount of farLandhelp in mruplan
        /// </summary>
        /// <param name="agencyId"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public List<LineInfo> GetMaxLineCollecctCount(string agencyId, string period)
        {

            List<LineInfo> lines = new List<LineInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_MaxLineCollectCount");
            db.AddInParameter(cmd, "pAgencyId", DbType.String, agencyId);
            db.AddInParameter(cmd, "pPeriod", DbType.String, period);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow r in dt.Rows)
            {
                LineInfo line = new LineInfo();
                line.LineId = DaHelper.GetString(r, "MruId");
                line.CollectCount = DaHelper.GetString(r, "CollectCount") == null ? 0 : Convert.ToInt32(DaHelper.GetString(r, "CollectCount"));
                lines.Add(line);
            }

            return lines;
        }

        public List<LineInfo> GetLineByCreateDate(string agencyId, string Period, DateTime createDate)
        {
            List<LineInfo> lines = new List<LineInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_LineByCreateDate");
            db.AddInParameter(cmd, "pAgencyId", DbType.String, agencyId);
            db.AddInParameter(cmd, "pPeriod", DbType.String, Period);
            db.AddInParameter(cmd, "pCreateDate", DbType.Date, createDate);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow r in dt.Rows)
            {
                LineInfo line = new LineInfo();
                line.LineId = DaHelper.GetString(r, "MruId");
                lines.Add(line);
            }

            return lines;
        }

        #endregion

        #region Select Table Members

        public List<CalculatedCommissionInfo> SelectCalculatedAgentCommission(BookSearchInfo searchInfo)
        {
            List<CalculatedCommissionInfo> agcomList = new List<CalculatedCommissionInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_CalculatedCommissionCalCount");
            db.AddInParameter(cmd, "AgentId", DbType.String, searchInfo.AgentId.PadLeft(12, '0'));
            db.AddInParameter(cmd, "Period", DbType.String, searchInfo.BillPeriod);
            db.AddInParameter(cmd, "CreateDt", DbType.DateTime, searchInfo.CreateDate);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow r in dt.Rows)
            {
                CalculatedCommissionInfo cal = new CalculatedCommissionInfo();
                cal.CmId = DaHelper.GetString(r, "CmId");
                cal.CalCount = DaHelper.GetInt(r, "CalCount").Value.ToString();
                agcomList.Add(cal);
            }

            return agcomList;
        }

        public List<CommissionHistoryInfo> SelectCommissionHistory(BookSearchInfo searchInfo)
        {
            List<CommissionHistoryInfo> agcomList = new List<CommissionHistoryInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_CalculatedCommission");
            db.AddInParameter(cmd, "AgentId", DbType.String, searchInfo.AgentId.PadLeft(12, '0'));
            db.AddInParameter(cmd, "Period", DbType.String, searchInfo.BillPeriod);
            db.AddInParameter(cmd, "CreateDt", DbType.DateTime, searchInfo.CreateDate);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow r in dt.Rows)
            {
                CommissionHistoryInfo com = new CommissionHistoryInfo();
                com.FineAmount = DaHelper.GetDecimal(r, "FineAmount").Value;
                com.FarLandHelp = DaHelper.GetDecimal(r, "FarLandHelp").Value;
                com.SpecialMoney = DaHelper.GetDecimal(r, "SpecialMoney").Value;
                com.TransCost = DaHelper.GetDecimal(r, "TransCost").Value;
                agcomList.Add(com);
            }

            return agcomList;
        }

        public List<BillbookInfoReprint> SelectBillBookByIdKeyword(string branchId, BillbookInfoReprint searchCondition)
        {
            List<BillbookInfoReprint> billbookList = new List<BillbookInfoReprint>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_BookListByIdKeyword");
            db.AddInParameter(cmd, "BillBookId", DbType.String, string.Format("{0}%{1}%", branchId, searchCondition.BillBookId));
            db.AddInParameter(cmd, "BookStatus", DbType.Int32, (int)searchCondition.BookSearchStatus);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count == 0) return billbookList;

            foreach (DataRow r in dt.Rows)
            {
                BillbookInfoReprint bookInfo = new BillbookInfoReprint();
                bookInfo.BillBookId = DaHelper.GetString(r, "BillBookId");
                bookInfo.AgencyName = DaHelper.GetString(r, "CaName");
                bookInfo.CreatorName = DaHelper.GetString(r, "Employee");
                bookInfo.BranchId = DaHelper.GetString(r, "BranchId");
                string pe = DaHelper.GetString(r, "BookPeriod");
                bookInfo.Period = pe.Substring(4, 2) + "/" + pe.Substring(0, 4);
                bookInfo.ReceiveCount = DaHelper.GetByte(r, "ReceiveCount");
                DateTime? temp = DaHelper.GetDate(r, "CreateDate");
                bookInfo.CreateDt = temp.Value.ToString("dd/MM/yyyy", _th_dt);
                bookInfo.BillTotalCount = DaHelper.GetInt(r, "TotalBill");
                if (bookInfo.BillTotalCount == null) bookInfo.BillTotalCount = 0;

                decimal? bookTotalAmount = DaHelper.GetDecimal(r, "BookTotalAmount");
                bookInfo.BookTotalAmount = DaHelper.ToMoneyFormat(bookTotalAmount);
                bookInfo.Status = DaHelper.GetString(r, "BsName");
                bookInfo.StatusId = DaHelper.GetString(r, "BsId");
                bookInfo.BookLot = DaHelper.GetInt(r, "BookLot");
                billbookList.Add(bookInfo);
            }

            return billbookList;
        }

        public List<BillbookInfoReprint> SelectBillBookListByStatus(BookSearchStatusEnum status, string runningBranch)
        {
            List<BillbookInfoReprint> billbookList = new List<BillbookInfoReprint>();
            string st = null;

            //st = null mean ALL 
            if (status == BookSearchStatusEnum.CUT)
                st = "T";
            else if (status == BookSearchStatusEnum.NORMAL)
                st = "N";
            else if (status == BookSearchStatusEnum.CANCEL)
                st = "C";

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_BookListByStatus");
            db.AddInParameter(cmd, "St", DbType.String, st);
            db.AddInParameter(cmd, "BranchId", DbType.String, runningBranch);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count == 0) return billbookList;

            foreach (DataRow r in dt.Rows)
            {
                BillbookInfoReprint bookInfo = new BillbookInfoReprint();
                bookInfo.BillBookId = DaHelper.GetString(r, "BillBookId").Substring(6, 9);
                string pe = DaHelper.GetString(r, "BookPeriod");
                bookInfo.AgencyName = DaHelper.GetString(r, "CaName");
                bookInfo.CreatorName = String.Format("{0} {1}", DaHelper.GetString(r, "FirstName"), DaHelper.GetString(r, "LastName"));
                bookInfo.BranchId = DaHelper.GetString(r, "BranchId");
                bookInfo.Period = pe.Substring(4, 2) + "/" + pe.Substring(0, 4);
                bookInfo.ReceiveCount = DaHelper.GetByte(r, "ReceiveCount");
                DateTime? temp = DaHelper.GetDate(r, "CreateDate");
                bookInfo.CreateDt = temp.Value.ToString("dd/MM/yyyy", _th_dt);
                bookInfo.BillTotalCount = DaHelper.GetInt(r, "TotalBill");
                if (bookInfo.BillTotalCount == null) bookInfo.BillTotalCount = 0;

                decimal? bookTotalAmount = DaHelper.GetDecimal(r, "BookTotalAmount");
                bookInfo.BookTotalAmount = DaHelper.ToMoneyFormat(bookTotalAmount);
                bookInfo.Status = DaHelper.GetString(r, "BsName");
                bookInfo.StatusId = DaHelper.GetString(r, "BsId");
                bookInfo.BookLot = DaHelper.GetInt(r, "BookLot");
                billbookList.Add(bookInfo);
            }

            return billbookList;
        }

        public List<AgencyBillCollectionMasterReport> GetAgencyBillCollection(string period, string branchId, string branchName)
        {
            List<AgencyBillCollectionMasterReport> _retVals = new List<AgencyBillCollectionMasterReport>();
            AgencyBillCollectionMasterReport _header = new AgencyBillCollectionMasterReport();

            string year = period.Substring(3, 4);
            int _monthNo = Convert.ToInt16(period.Substring(0, 2));

            _header.QuarterNo = (_monthNo / 4) + 1;
            _header.PeriodName = String.Format(" {0} {1}", StringConvert.GetMonthName(_monthNo), year);
            _header.BillCollectionDetail = GetAgencyBillCollectionDetail(period);
            _header.PEACode = branchId;
            _header.PEAName = branchName;
            _header.PrintDate = Session.BpmDateTime.Now.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
            _retVals.Add(_header);
            return _retVals;
        }

        public List<AgencyBillCollectionDetailReport> GetAgencyBillCollectionDetail(string period)
        {
            //get calendar type
            List<AgencyBillCollectionDetailReport> _retVals = new List<AgencyBillCollectionDetailReport>();

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            string sql = @" SELECT 
                                    BookPeriod, BookHolderId, CaId, CaName, ContractValidFrom, 
                                    MaxReceiveCount,TotalAmount,
                                    TotalBillCount, TotalBillCollect, TotalCollectElective, 
                                    AssetValue, AssetType, TotalAsset
                            FROM 
                                    ag_bill_collection_report_view
                            WHERE 
                                    BookPeriod = '{0}'
                          ";
            sql = String.Format(sql, DaHelper.SetBillPeriod(period));
            DbCommand cmd = db.GetSqlStringCommand(sql);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];



            string _currentAgencyId = String.Empty;
            int _rowId = 1;
            foreach (DataRow r in dt.Rows)
            {
                string _newAgencyId = DaHelper.GetString(r, "BookHolderId");
                AgencyBillCollectionDetailReport _item = new AgencyBillCollectionDetailReport();

                _item.AssetValue = DaHelper.GetDecimal(r, "AssetValue");
                _item.AssetLimit = DaHelper.GetDecimal(r, "AssetLimit");
                _item.AssetType = DaHelper.GetString(r, "AssetType");

                if (_currentAgencyId != _newAgencyId)
                {
                    _item.RowId = _rowId++;
                    _item.AgencyId = DaHelper.GetString(r, "BookHolderId").Substring(0, 6).PadLeft(8,'0');
                    _item.AgencyName = DaHelper.GetString(r, "CaName");
                    _item.SignContractDt = DaHelper.GetDate(r, "ContractValidFrom");
                    _item.BillReceiveCount = DaHelper.GetByte(r, "MaxReceiveCount");
                    _item.TotalBillElective = DaHelper.GetDecimal(r, "TotalAmount");
                    _item.TotalBillReceive = DaHelper.GetInt(r, "TotalBillCount");
                    _item.TotalBillCollect = DaHelper.GetInt(r, "TotalBillCollect");
                    _item.TotalCollectElective = DaHelper.GetDecimal(r, "TotalCollectElective");
                    _item.TotalAsset = DaHelper.GetDecimal(r, "TotalAsset");
                    _item.TotalValue = _item.TotalBillElective - _item.TotalAsset;
                    _currentAgencyId = _newAgencyId;
                }
                else
                {
                    _item.AgencyId = String.Empty;
                    _item.AgencyName = String.Empty;
                    _item.SignContractDt = null;
                    _item.BillReceiveCount = null;
                    _item.TotalBillElective = null;
                    _item.TotalBillReceive = null;
                    _item.TotalBillCollect = null;
                    _item.TotalCollectElective = null;

                    _item.TotalAsset = null;
                    _item.TotalValue = null;

                }


                _retVals.Add(_item);
            }
            return _retVals;
        }

        public BindingList<LineInfo> SearchAgencyMru(string branchId, string mruId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                BindingList<LineInfo> lineList = new BindingList<LineInfo>();
                DbCommand cmd = db.GetStoredProcCommand("ag_sel_AgencyMru");
                db.AddInParameter(cmd, "BranchId", DbType.String,branchId);
                db.AddInParameter(cmd, "MruId", DbType.String, mruId);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                
                foreach (DataRow r in dt.Rows)
                {
                    LineInfo lineInfo = new LineInfo();
                    lineInfo.LineId = DaHelper.GetString(r, "MruId");
                    lineInfo.BranchId = DaHelper.GetString(r, "BranchId");
                    lineInfo.PortionDesc = DaHelper.GetString(r, "PortionDesc");
                    lineInfo.AdvPayment = DaHelper.GetString(r, "AdvPay") == "1" ? true : false;
                    lineInfo.NumOfCustomer = DaHelper.GetInt(r, "CaCount").Value;
                    lineInfo.TravelHelp = DaHelper.GetString(r, "TravelHelp") == null ? (int)TravelHelpEnum.NORMALTRAVELHELP : Convert.ToInt32(DaHelper.GetString(r, "TravelHelp"));
                    string agencyId = DaHelper.GetString(r, "CaId") == null ? String.Empty : DaHelper.GetString(r, "CaId").Substring(4);
                    string agencyName = DaHelper.GetString(r, "CaName");
                    lineInfo.AgencyName = String.Format("{0} : {1}", agencyId, agencyName);
                    //lineInfo.ValidFrom = DaHelper.GetDate(r, "HelpValidFrom");
                    //lineInfo.ValidTo = DaHelper.GetDate(r, "HepValidTo");
                    lineList.Add(lineInfo);
                }

                return lineList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int GetNumOfMruInBranch(string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_NumOfMruInBranch");
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            int mruCount = (int)db.ExecuteScalar(cmd);
            return mruCount;
        }

        //select Lv4 branch by perent branch which is this branch
        public List<PeaInfo> SelectBranchLevelFour(string branchId)
        {
            List<PeaInfo> branchList = new List<PeaInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_LvFourBranchesByParent");
            db.AddInParameter(cmd, "ParentBranchId", DbType.String, branchId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow r in dt.Rows)
            {
                PeaInfo branchInfo = new PeaInfo();
                branchInfo.Id = DaHelper.GetString(r, "BranchId");
                branchInfo.BranchLevel = DaHelper.GetString(r, "BranchLevel");
                branchList.Add(branchInfo);
            }

            return branchList;
        }

        public List<PeaInfo> SelectBranchByKeyword(string keyword, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            List<PeaInfo> branchList = new List<PeaInfo>();
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_BranchByKeyword");
            db.AddInParameter(cmd, "Keyword", DbType.String, keyword);
            db.AddInParameter(cmd, "ParentBranchId", DbType.String, branchId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow r in dt.Rows)
            {
                PeaInfo branchInfo = new PeaInfo();
                branchInfo.Id = DaHelper.GetString(r, "BranchId");
                branchInfo.Name = DaHelper.GetString(r, "BranchName");
                branchInfo.Address = DaHelper.GetString(r, "Address");
                branchInfo.BranchNo = DaHelper.GetString(r, "BranchNo");
                branchInfo.BranchLevel = DaHelper.GetString(r, "BranchLevel");
                branchInfo.NumOfLines = GetNumOfMruInBranch(branchInfo.Id);
                branchList.Add(branchInfo);
            }

            return branchList;
        }

        public List<AgentInfo> SelectAgentsByKeyword(string keyword, int searchType, string branchId)
        {
            try
            {
                string sql = null;
                if (keyword == "")  //find all 
                    sql = "WHERE ";
                else if (searchType == 1) // all            
                    sql = "WHERE (mt.ContractAccount.CaName LIKE '%{0}%' OR mt.ContractAccount.CaId  LIKE '%{0}%') AND ";
                //fix me - SAP caId format for agency , padleft , instead of padright 
                else if (searchType == 2) // Id 
                    sql = "WHERE mt.ContractAccount.CaId LIKE '%{0}%' AND ";
                else if (searchType == 3) //name
                    sql = "WHERE mt.ContractAccount.CaName LIKE '%{0}%'  AND ";

                sql += "CAST(mt.ContractAccount.CtId As char(2)) = '51' AND CAST(mt.ContractAccount.AccountClassId As char(4)) = 'ZB01' ";

                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ag_sel_AgentByKeyword");
                db.AddInParameter(cmd, "Criteria", DbType.String, string.Format(sql, keyword));
                db.AddInParameter(cmd, "ParentBranchId", DbType.String, branchId);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                return FillAgentInfo(dt);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<AgentInfo> SelectAgentsByDepositOperand(decimal deposit, string operand, string branchId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                string sql = @"
                            SELECT mt.ContractType.CtName, mt.ContractType.CtId, 
                                    mt.ContractAccount.CaName, mt.ContractAccount.CaAddress, 
                                    mt.ContractAccount.CaId, mt.ContractAccount.TechBranchId,
                                    mt.ContractAccount.SecurityDeposit, mt.ContractAccount.ContractValidFrom, 
                                    mt.ContractAccount.ContractValidTo,  mt.ContractAccount.PenaltyWaiveFlag
                             FROM mt.ContractAccount 
                                    INNER JOIN mt.ContractType ON mt.ContractAccount.CtId = mt.ContractType.CtId                            
                             WHERE mt.ContractAccount.SecurityDeposit {0} {1} AND CAST(mt.ContractAccount.CtId As char(2)) = '51' AND 
                             CAST(mt.ContractAccount.AccountClassId As char(4)) = 'ZB01' ";

                DbCommand cmd = db.GetStoredProcCommand("ag_sel_AgentsByDepositOperand");
                db.AddInParameter(cmd, "CustomSQL", DbType.String, string.Format(sql, operand, deposit.ToString()));
                db.AddInParameter(cmd, "ParentBranchId", DbType.String, branchId);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                return FillAgentInfo(dt);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private List<AgentInfo> FillAgentInfo(DataTable dt)
        {
            List<AgentInfo> agentInfoList = new List<AgentInfo>();

            foreach (DataRow r in dt.Rows)
            {
                AgentInfo agentInfo = new AgentInfo();
                agentInfo.Id = DaHelper.GetString(r, "CaId").Substring(4);
                agentInfo.Name = DaHelper.GetString(r, "CaName");
                agentInfo.Deposit = DaHelper.GetDecimal(r, "SecurityDeposit");
                agentInfo.Address = DaHelper.GetString(r, "CaAddress");
                agentInfo.Type = DaHelper.GetString(r, "AccountClassName");
                agentInfo.TechBranchId = DaHelper.GetString(r, "TechBranchId");
                agentInfo.ContractValidFrom = DaHelper.GetDate(r, "ContractValidFrom");
                agentInfo.ContractValidTo = DaHelper.GetDate(r, "ContractValidTo");
                agentInfo.PenaltyWaiveFlag = DaHelper.GetString(r, "PenaltyWaiveFlag") == "0" ? false : true;
                agentInfoList.Add(agentInfo);
            }

            return agentInfoList;
        }

        //by status
        public DataTable SelectBillbooksByAgentId(string agentId, string status)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_BillBooksByAgentId");
            db.AddInParameter(cmd, "AgentId", DbType.String, agentId.PadLeft(12, '0'));
            db.AddInParameter(cmd, "Status", DbType.String, status);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            return dt;
        }

        public DataTable SelectBillBookInputItemSet(string billBookId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_BillBookInputItemSet");
            db.AddInParameter(cmd, "BillBookId", DbType.String, billBookId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            return dt;
        }

        public DataTable SelectBillBookInputItem(string billBookId, string filterType)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_BillBookInputItem");
            db.AddInParameter(cmd, "BillBookId", DbType.String, billBookId);
            db.AddInParameter(cmd, "FilterType", DbType.String, filterType);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            return dt;
        }

        public DataTable SelectBillBookCreateDate(string agentId, string period)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_BillBookCreateDate");
            db.AddInParameter(cmd, "AgentId", DbType.String, agentId.PadLeft(12, '0'));
            db.AddInParameter(cmd, "Period", DbType.String, period);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            return dt;
        }

        public DateTime SelectBillBookReturnDate(string agentId, string period, DateTime createDt)
        {
            string day = createDt.Day.ToString();
            string month = createDt.Month.ToString().PadLeft(2, '0');
            string year = createDt.Year.ToString();

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_MaxBookReturnDate");
            db.AddInParameter(cmd, "AgentId", DbType.String, agentId.PadLeft(12, '0'));
            db.AddInParameter(cmd, "Period", DbType.String, period);
            db.AddInParameter(cmd, "Year", DbType.String, year);
            db.AddInParameter(cmd, "Month", DbType.String, month);
            db.AddInParameter(cmd, "Day", DbType.String, day);
            return (DateTime)db.ExecuteScalar(cmd);
        }

        public DataTable SelectBillBookCountRange(string agentId, string period, DateTime createDt)
        {
            string day = createDt.Day.ToString();
            string month = createDt.Month.ToString().PadLeft(2, '0');
            string year = createDt.Year.ToString();

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_BookReceiveCountRange");
            db.AddInParameter(cmd, "AgentId", DbType.String, agentId.PadLeft(12, '0'));
            db.AddInParameter(cmd, "Period", DbType.String, period);
            db.AddInParameter(cmd, "Year", DbType.String, year);
            db.AddInParameter(cmd, "Month", DbType.String, month);
            db.AddInParameter(cmd, "Day", DbType.String, day);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            return dt;
        }

        public DataTable SelectAgencyPaymentStatus()
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_AgencyPaymentStatus");
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            return dt;
        }

        public DataTable SelectCaBillStatusByBookId(string bookId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_BookItemByBookId");
            db.AddInParameter(cmd, "BillBookId", DbType.String, bookId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            return dt;
        }

        public DataTable SelectBillStatusInfoByCaId(string branchId, string mruId, string caId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_InvStatusByCaId");
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "MruId", DbType.String, mruId);
            db.AddInParameter(cmd, "CaId", DbType.String, caId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            return dt;
        }

        public string GetBookCallingStatus(string bookId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_BookCallingStatus");
            db.AddInParameter(cmd, "BillBookId", DbType.String, bookId);
            return (string)db.ExecuteScalar(cmd);
        }

        public DataTable SelectBillStatusInfoByCaId(string branchId, string mruId, string caId, string period, bool curMPeriod)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = null;

            if (curMPeriod)
                cmd = db.GetStoredProcCommand("ag_sel_InvPeriodStatusByCaId");
            else
                cmd = db.GetStoredProcCommand("ag_sel_InvOutPeriodStatusByCaId");

            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "MruId", DbType.String, mruId);
            db.AddInParameter(cmd, "CaId", DbType.String, caId);
            db.AddInParameter(cmd, "Period", DbType.String, period);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            return dt;

        }

        public DataTable SelectBillStatusInfo(string branchId, string mruId, string controllerId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_InvStatus");
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "MruId", DbType.String, mruId);
            db.AddInParameter(cmd, "ControllerId", DbType.String, controllerId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            return dt;

        }

        public DataTable SelectBillStatusInfo(string branchId, string mruId, string period, bool curMPeriod)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = null;

            if (curMPeriod)
                cmd = db.GetStoredProcCommand("ag_sel_InvPeriodStatus");
            else
                cmd = db.GetStoredProcCommand("ag_sel_InvOutPeriodStatus");

            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "MruId", DbType.String, mruId);
            db.AddInParameter(cmd, "Period", DbType.String, period);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            return dt;

        }

        public int SelectBookItemPutInvoiceByBookId(string bookId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_BookItemPutInvByBookId");
            db.AddInParameter(cmd, "BillBookId", DbType.String, bookId);
            return (int)db.ExecuteScalar(cmd);
        }

        public decimal? GetBookTotalAmount(string bookId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_BookTotalAmountByBookId");
            db.AddInParameter(cmd, "BillBookId", DbType.String, bookId);
            return (decimal?)db.ExecuteScalar(cmd);
        }

        public DataTable SelectARAdvPaymentAmount(string bookId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_ARAdvPayAmount");
            db.AddInParameter(cmd, "BillBookId", DbType.String, bookId);
            return db.ExecuteDataSet(cmd).Tables[0];
        }

        public DataTable SelectARBookPaymentAmount(string bookId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_ARBookPayAmount");
            cmd.CommandTimeout = 360;
            db.AddInParameter(cmd, "BillBookId", DbType.String, bookId);
            return db.ExecuteDataSet(cmd).Tables[0];
        }

        public DataTable GetPaymentDateBook(string bookId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_BookPayDateByBookId");
            db.AddInParameter(cmd, "BillBookId", DbType.String, bookId);
            return db.ExecuteDataSet(cmd).Tables[0];
        }

        public int GetBookItemPutInvPastThreeMonths(string bookId, string period)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_BookPutInvPastThreeMonths");
            db.AddInParameter(cmd, "BillBookId", DbType.String, bookId);
            db.AddInParameter(cmd, "Period", DbType.String, period);
            return (int)db.ExecuteScalar(cmd);
        }

        public int GetBookItemCount(string bookId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_BookItemCountByBookId");
            db.AddInParameter(cmd, "BillBookId", DbType.String, bookId);
            return (int)db.ExecuteScalar(cmd);
        }

        private int GetNumOfCaByMruId(string branchId, string mruId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_NumOfCaInMru");
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "MruId", DbType.String, mruId);
            return (int)db.ExecuteScalar(cmd);
        }

        public BindingList<LineInfo> SelectMRUsByAgentId(string agentId)
        {
            //IMPORTANT! Only [branchId, MruId] that this agent is taking care. 
            BindingList<LineInfo> lineInfoList = new BindingList<LineInfo>();

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_MRUsByAgentId");
            db.AddInParameter(cmd, "AgentId", DbType.String, agentId.PadLeft(12, '0'));
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow r in dt.Rows)
            {
                LineInfo lineInfo = new LineInfo();
                lineInfo.BranchId = DaHelper.GetString(r, "BranchId");
                lineInfo.LineId = DaHelper.GetString(r, "MruId");
                lineInfo.NumOfCustomer = GetNumOfCaByMruId(lineInfo.BranchId, lineInfo.LineId);
                lineInfo.PortionDesc = DaHelper.GetString(r, "PortionDesc");
                lineInfo.AdvPayment = DaHelper.GetString(r, "AdvPay") == "1" ? true : false;
                lineInfo.AgentId = DaHelper.GetString(r, "CaId");
                lineInfo.AgentId = lineInfo.AgentId == null ? String.Empty : lineInfo.AgentId.Substring(4);
                lineInfo.AgentName = DaHelper.GetString(r, "CaName");
                lineInfo.TravelHelp = DaHelper.GetString(r, "TravelHelp") == null ? (int)TravelHelpEnum.NORMALTRAVELHELP : Convert.ToInt32(DaHelper.GetString(r, "TravelHelp"));
                //lineInfo.ValidFrom = DaHelper.GetDate(r, "HelpValidFrom");
                //lineInfo.ValidTo = DaHelper.GetDate(r, "HelpValidTo");
                lineInfo.ControllerId = DaHelper.GetString(r, "ControllerId");
                lineInfoList.Add(lineInfo);
            }

            return lineInfoList;
        }

        public BindingList<LineInfo> SelectMRUsByEmpNoAgencyId(List<string> searchConn)
        {
            //IMPORTANT! Only [branchId, MruId] that this agent is taking care. 
            BindingList<LineInfo> lineInfoList = new BindingList<LineInfo>();

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_MRUsByEmpNoAgentId");
            db.AddInParameter(cmd, "pAgentId", DbType.String, searchConn[0].PadLeft(12, '0'));
            db.AddInParameter(cmd, "pControllerId", DbType.String, searchConn[1]);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow r in dt.Rows)
            {
                LineInfo lineInfo = new LineInfo();
                lineInfo.BranchId = DaHelper.GetString(r, "BranchId");
                lineInfo.LineId = DaHelper.GetString(r, "MruId");
                lineInfo.NumOfCustomer = GetNumOfCaByMruId(lineInfo.BranchId, lineInfo.LineId);
                lineInfo.PortionDesc = DaHelper.GetString(r, "PortionDesc");
                lineInfo.AdvPayment = DaHelper.GetString(r, "AdvPay") == "1" ? true : false;
                lineInfo.AgentId = DaHelper.GetString(r, "CaId");
                lineInfo.AgentId = lineInfo.AgentId == null ? String.Empty : lineInfo.AgentId.Substring(4);
                lineInfo.AgentName = DaHelper.GetString(r, "CaName");
                lineInfo.TravelHelp = DaHelper.GetString(r, "TravelHelp") == null ? (int)TravelHelpEnum.NORMALTRAVELHELP : Convert.ToInt32(DaHelper.GetString(r, "TravelHelp"));
                //lineInfo.ValidFrom = DaHelper.GetDate(r, "HelpValidFrom");
                //lineInfo.ValidTo = DaHelper.GetDate(r, "HelpValidTo");
                lineInfo.ControllerId =  DaHelper.GetString(r, "ControllerId");
                lineInfoList.Add(lineInfo);
            }

            return lineInfoList;
        }

        //public List<CallingBillInfo> SelectBillsByMru 

        #endregion

        #region [Insert] Record Members

        public void NewEmployeeContractAccount(DbTransaction trans, BillBookHeaderInfo header, string postedServerId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_ins_EmployeeContractAccount");
            //update advance payment
            db.AddInParameter(cmd, "CaId", DbType.String, header.AgentId.PadLeft(12, '0'));
            db.AddInParameter(cmd, "CaName", DbType.String, header.AgentName);
            db.AddInParameter(cmd, "BranchId", DbType.String, header.BookHolderBranchId);
            db.AddInParameter(cmd, "ContractValidFrom", DbType.DateTime, Session.BpmDateTime.Now.AddYears(-1));
            db.AddInParameter(cmd, "ContractValidTo", DbType.DateTime, Session.BpmDateTime.Now.AddYears(1));
            db.AddInParameter(cmd, "PostedServerId", DbType.String, postedServerId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, Session.User.Id);
            db.ExecuteNonQuery(cmd, trans);
        }

        public void NewEmployeeContractBranch(DbTransaction trans, string caId, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_ins_EmployeeAgencyContractBranch");
            //update advance payment
            db.AddInParameter(cmd, "CaId", DbType.String, caId.PadLeft(12, '0'));
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.ExecuteNonQuery(cmd, trans);
        }

        public HelpOfferInfo GetCommissionHelpInfo(BookSearchInfo searchInfo)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_CommissionHelpInfo");
            db.AddInParameter(cmd, "BookPeriod", DbType.String, searchInfo.BillPeriod);
            db.AddInParameter(cmd, "AgentId", DbType.String, searchInfo.AgentId.PadLeft(12, '0'));
            db.AddInParameter(cmd, "CreateDate", DbType.DateTime, searchInfo.CreateDate);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count == 0) return new HelpOfferInfo();

            HelpOfferInfo helpInfo = new HelpOfferInfo();
            //expect one row
            foreach (DataRow r in dt.Rows)
            {
                helpInfo.HelpTransport = DaHelper.GetDecimal(r, "TransCost");
                if (helpInfo.HelpTransport == null)
                    helpInfo.HelpTransport = 0;
                helpInfo.HelpSpecialMoney = DaHelper.GetDecimal(r, "SpecialMoney");
                if (helpInfo.HelpSpecialMoney == null)
                    helpInfo.HelpSpecialMoney = 0;
                helpInfo.HelpFarLand = DaHelper.GetDecimal(r, "FarLandHelp");
                if (helpInfo.HelpFarLand == null)
                    helpInfo.HelpFarLand = 0;
            }

            return helpInfo;

        }

        public bool IsFineEnabled(BookSearchInfo searchInfo)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_IsFineEnabled");
            //update advance payment
            db.AddInParameter(cmd, "AgentId", DbType.String, searchInfo.AgentId.PadLeft(12, '0'));
            db.AddInParameter(cmd, "BookPeriod", DbType.String, searchInfo.BillPeriod);
            db.AddInParameter(cmd, "CreateDate", DbType.String, searchInfo.CreateDate);
            string fine = (string)db.ExecuteScalar(cmd);
            if (fine == "0")
                return false;
            else
                return true;
        }

        public string SaveAgencyCommission(DbTransaction trans, decimal? transCost, decimal? fh, decimal? sp, int calCount, string runningBranchId,
                                                    decimal? taxAmount, decimal? cmAmount, decimal? fineAmount, decimal? vatAmount, string fineEnabled,
            //Aug, 10 07, for report
                                                    decimal? baseCmAmount, decimal? specialCmAmount, decimal? invCmAmount, bool overNinety, string modifiedBy, string postbranchserverId)
        {
            string cy = Session.BpmDateTime.Now.ToString("yyyy", _th_dt).Substring(2, 2);
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_ins_SaveAgencyCommission");
            db.AddInParameter(cmd, "TransCost", DbType.Decimal, transCost);
            db.AddInParameter(cmd, "FarLandHelp", DbType.Decimal, fh);
            db.AddInParameter(cmd, "SpecialMoney", DbType.Decimal, sp);
            db.AddInParameter(cmd, "CalCount", DbType.Int32, calCount);
            db.AddInParameter(cmd, "RunningBranch", DbType.String, runningBranchId);
            db.AddInParameter(cmd, "CurrentYear", DbType.String, cy);
            db.AddInParameter(cmd, "TaxAmount", DbType.Decimal, taxAmount);
            db.AddInParameter(cmd, "CmAmount", DbType.Decimal, cmAmount);
            db.AddInParameter(cmd, "FineAmount", DbType.Decimal, fineAmount);
            db.AddInParameter(cmd, "VatAmount", DbType.Decimal, vatAmount);
            db.AddInParameter(cmd, "FineEnabled", DbType.String, fineEnabled);
            db.AddInParameter(cmd, "BaseCmAmount", DbType.Decimal, baseCmAmount);
            db.AddInParameter(cmd, "SpecialCmAmount", DbType.Decimal, specialCmAmount);
            db.AddInParameter(cmd, "InvCmAmount", DbType.Decimal, invCmAmount);
            db.AddInParameter(cmd, "OverNinety", DbType.String, overNinety ? "Y" : "N");
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, modifiedBy);
            db.AddInParameter(cmd, "PostBranchServerId", DbType.String, postbranchserverId);
            string cmId = (string)db.ExecuteScalar(cmd, trans);
            return cmId;
        }

        public void InsertCommissionBookItem(DbTransaction trans, string cmId, string bookId, string modifiedBy, string postBranchServerId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_ins_CommissionBookItem");
            //update advance payment
            db.AddInParameter(cmd, "CmId", DbType.String, cmId);
            db.AddInParameter(cmd, "BillBookId", DbType.String, bookId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, modifiedBy);
            db.AddInParameter(cmd, "PostBranchServerId", DbType.String, postBranchServerId);
            db.ExecuteNonQuery(cmd, trans);
        }

        public void DeleteAgencyMru(DbTransaction trans, string agencyId, string branchId, string modifiedBy)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_del_AgentMru");
            db.AddInParameter(cmd, "pAgencyId", DbType.String, agencyId);
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pModifiedBy", DbType.String, modifiedBy);
            db.ExecuteNonQuery(cmd, trans);
        }

        public int InsUpdLineOfAgent(DbTransaction trans, string agentId, string lineId, string branchId, string postedServerId, string modifiedBy)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_ins_AgentMru");
            db.AddInParameter(cmd, "MruId", DbType.String, lineId);
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "AgentId", DbType.String, agentId);
            db.AddInParameter(cmd, "PostedServerId", DbType.String, postedServerId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, modifiedBy);
            return db.ExecuteNonQuery(cmd, trans);
        }        

        public string CreateBillBook(DbTransaction trans, BillBookHeaderInfo header, string postedServerId)
        {
            //need rollback if fails

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");         
            string billType = null;
            if (header.IsFirstPaid)
                billType = "N";
            else
                billType = "R";

            string daPeriod = header.Period.Substring(3, 4) + header.Period.Substring(0, 2);
            //string bookHolderId = header.AgentId.PadLeft(12, '0');

            DbCommand cmd = db.GetStoredProcCommand("ag_ins_CreateBillBook");
            db.AddInParameter(cmd, "BookId", DbType.String, header.BillBookId);
            db.AddInParameter(cmd, "BookHolderId", DbType.String, header.AgentId);
            db.AddInParameter(cmd, "BookLot", DbType.Int32, header.BookLot);
            db.AddInParameter(cmd, "AboId", DbType.String, billType);
            db.AddInParameter(cmd, "AdvPayAmount", DbType.Decimal, header.AdvPayAmount);
            db.AddInParameter(cmd, "AdvPayDueDt", DbType.DateTime, header.AdvancePaymentDt);
            db.AddInParameter(cmd, "ReturnDueDt", DbType.DateTime, header.ReturnedBillDt);
            db.AddInParameter(cmd, "BookPeriod", DbType.String, daPeriod);
            db.AddInParameter(cmd, "BkId", DbType.String, header.ControllerId);
            db.AddInParameter(cmd, "ReceiveCount", DbType.Byte, header.ReceiveCount);  //tinyint
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, header.ModifiedBy);
            db.AddInParameter(cmd, "BillTotalCount", DbType.Int32, header.TotalBillCount);
            db.AddInParameter(cmd, "BookTotalAmount", DbType.Decimal, header.TotalBookAmount);
            db.AddInParameter(cmd, "BranchId", DbType.String, header.RunningBranchId);
            db.AddInParameter(cmd, "PostedServerId", DbType.String, postedServerId);
            
            return (string)db.ExecuteScalar(cmd, trans);
        }

        public string GetNewBillBookId(string runningBranchId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");     
            DbCommand cmd = db.GetStoredProcCommand("ag_get_NewBookId");
            db.AddInParameter(cmd, "Filter", DbType.String, string.Format("{0}%", runningBranchId));
            object ret = db.ExecuteScalar(cmd);
            // ### Generate BillbookId
            string bookId = null;
            string bookIdTemp = null;
            string thisYear = Session.BpmDateTime.Now.ToString("dd/MM/yyyy", _th_dt);
            string y2 = thisYear.Substring(8, 2);
            if (ret == DBNull.Value)
            {
                //first install create default new id
                bookId = string.Format("{0}{1}", y2, "0000001");
            }
            else
            {
                // branchId(6) + year(2 digits) + running (7)
                bookIdTemp = (string)ret;
                string storedY2 = bookIdTemp.Substring(6, 2);

                //run new Id of this year
                if (y2 != storedY2)
                {
                    bookId = string.Format("{0}{1}", y2, "0000001");
                }
                else
                {
                    int bId = Convert.ToInt32(bookIdTemp.Substring(6, 9));
                    bId++;
                    bookId = bId.ToString();
                }
            }

            // #### Id much be unique for all branches, so concatinate with branchId
            bookId = string.Format("{0}{1}", runningBranchId, bookId);
            return bookId;
        }
        

        public void UpdateMRUPlanCreateBillBook(DateTime createDate, string bookId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_upd_MruPlanCreateBillBook");
            //update advance payment
            db.AddInParameter(cmd, "CreateDate", DbType.DateTime, createDate);
            db.AddInParameter(cmd, "BillBookId", DbType.String, bookId);              
            db.ExecuteNonQuery(cmd);
        }

        public void UpdateMRUPlanCheckInBillBook(DbTransaction trans, DateTime checkInDate, string bookId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_upd_MruPlanCheckInBillBook");
            cmd.CommandTimeout = 600;
            //update advance payment
            db.AddInParameter(cmd, "CheckInDate", DbType.DateTime, checkInDate);
            db.AddInParameter(cmd, "BillBookId", DbType.String, bookId);
            db.ExecuteNonQuery(cmd, trans);
        }

        public void InsertBillBookItem(DbTransaction trans, BillStatusInfo bsInfo, string postedServerId, string modifiedBy)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_ins_BillBookItem");
            //update advance payment
            db.AddInParameter(cmd, "BillBookId", DbType.String, bsInfo.BillBookId);
            db.AddInParameter(cmd, "InvoiceNo", DbType.String, bsInfo.InvoiceNo);
            db.AddInParameter(cmd, "Period", DbType.String, bsInfo.Period);
            db.AddInParameter(cmd, "BranchId", DbType.String, bsInfo.BranchId);
            db.AddInParameter(cmd, "MruId", DbType.String, bsInfo.MruId);
            db.AddInParameter(cmd, "CaId", DbType.String, bsInfo.CaId);
            db.AddInParameter(cmd, "AbsId", DbType.String, bsInfo.AbsId);
            db.AddInParameter(cmd, "PostedServerId", DbType.String, postedServerId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, modifiedBy);
            db.ExecuteNonQuery(cmd, trans);
        }

        public void InsertBillBookInputSet(DbTransaction trans, string bookId, string branchId, string mruId,
                                        string periodType, string outType,string postedServerId, string modifiedBy)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_ins_BillBookInputItemSet");
            //update advance payment
            db.AddInParameter(cmd, "BillBookId", DbType.String, bookId);
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "MruId", DbType.String, mruId);
            db.AddInParameter(cmd, "BillPeriodType", DbType.String, periodType);
            db.AddInParameter(cmd, "BillOutType", DbType.String, outType);
            db.AddInParameter(cmd, "PostedServerId", DbType.String, postedServerId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, modifiedBy);
            db.ExecuteNonQuery(cmd, trans);
        }

        public void InsertBillBookInputExtraItem(DbTransaction trans, string bookId, string branchId, 
                string mruId, string filterType, string caId, string postedServerId, string modifiedBy)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_ins_BillBookInputItem");
            //update advance payment
            db.AddInParameter(cmd, "BillBookId", DbType.String, bookId);
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "MruId", DbType.String, mruId);
            db.AddInParameter(cmd, "CaId", DbType.String, caId);
            db.AddInParameter(cmd, "FilterType", DbType.String, filterType);
            db.AddInParameter(cmd, "PostedServerId", DbType.String, postedServerId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, modifiedBy);
            db.ExecuteNonQuery(cmd, trans);
        }

        #endregion

        #region [Update] Record Members


        public void UpdateCommissionRate(DbTransaction trans, FeeBaseElement comRate)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_ins_CommissionRate");
            //update advance payment
            db.AddInParameter(cmd, "BranchId", DbType.String, comRate.BranchId);
            db.AddInParameter(cmd, "HouseRegRate", DbType.Decimal, comRate.HouseRegRate);
            db.AddInParameter(cmd, "HouseGrpRate", DbType.Decimal, comRate.HouseGrpRate);
            db.AddInParameter(cmd, "CorpRegRate", DbType.Decimal, comRate.CorpRegRate);
            db.AddInParameter(cmd, "CorpGrpRate", DbType.Decimal, comRate.CorpGrpRate);
            db.AddInParameter(cmd, "GovRegRate", DbType.Decimal, comRate.GovRegRate);
            db.AddInParameter(cmd, "GovGrpRate", DbType.Decimal, comRate.GovGrpRate);
            db.AddInParameter(cmd, "BillNinetyPercent", DbType.Decimal, comRate.BillingNinetyPercent);
            db.AddInParameter(cmd, "BillNinetyNinePercent", DbType.Decimal, comRate.BillingNinetyNinePercent);
            db.AddInParameter(cmd, "BillHundredPercent", DbType.Decimal, comRate.BillingHundredPercent);
            db.AddInParameter(cmd, "MaxInvPercent", DbType.Decimal, comRate.MaxInvoicePercent);
            db.AddInParameter(cmd, "InvRate", DbType.Decimal, comRate.InvoiceRate);
            db.AddInParameter(cmd, "InvPastThreeMonthRate", DbType.Decimal, comRate.InvoicePastThreeMonthRate);
            db.AddInParameter(cmd, "FineRate", DbType.Decimal, comRate.FineRatePerBill);
            db.AddInParameter(cmd, "CmCountBlock", DbType.String, comRate.HasCommissionCalLimit ? "1" : "0");
            db.AddInParameter(cmd, "CmCountLimit", DbType.Int32, comRate.MaxCommissionCalCount);
            db.AddInParameter(cmd, "VatRate", DbType.Decimal, comRate.VatRate);
            //db.AddInParameter(cmd1, "TaxRate", DbType.Decimal, comRate.TaxRate);
            db.AddInParameter(cmd, "CollectedPercent", DbType.Decimal, comRate.CollectedPercent);
            db.AddInParameter(cmd, "PenaltyWaiveFlag", DbType.String, comRate.PenaltyWaive == true ? "1" : "0");
            db.AddInParameter(cmd, "caCount", DbType.Int32, comRate.CaCount);
            db.AddInParameter(cmd, "UpperRate", DbType.Decimal, comRate.UpperRate);
            db.AddInParameter(cmd, "LowerRate", DbType.Decimal, comRate.LowerRate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, Session.User.Id);
            db.ExecuteNonQuery(cmd, trans);

            //DbCommand cmd1 = db.GetStoredProcCommand("ag_upd_AppConfig");
            //db.AddInParameter(cmd1, "BranchId", DbType.String, comRate.BranchId);
            //db.AddInParameter(cmd1, "CmCountBlock", DbType.String, comRate.HasCommissionCalLimit ? "1" : "0");
            //db.AddInParameter(cmd1, "CmCountLimit", DbType.Int32, comRate.MaxCommissionCalCount);
            //db.AddInParameter(cmd1, "VatRate", DbType.Decimal, comRate.VatRate);
            ////db.AddInParameter(cmd1, "TaxRate", DbType.Decimal, comRate.TaxRate);
            //db.AddInParameter(cmd1, "CollectedPercent", DbType.Decimal, comRate.CollectedPercent);
            //db.AddInParameter(cmd1, "PenaltyWaiveFlag", DbType.String, comRate.PenaltyWaive == true ? "1" : "0");
            //db.AddInParameter(cmd1, "caCount", DbType.Int32, comRate.CaCount);
            //db.AddInParameter(cmd1, "UpperRate", DbType.Decimal, comRate.UpperRate);
            //db.AddInParameter(cmd1, "LowerRate", DbType.Decimal, comRate.LowerRate);
            //db.AddInParameter(cmd1, "ModifiedBy", DbType.String, Session.User.Id);
            //db.ExecuteNonQuery(cmd1, trans);

        }

        public int UpdateMRUSpecialHelpAndAdvPay(DbTransaction trans, string lineId, string branchId, string advPay, string modifiedBy, LineInfo line)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_upd_MruSpecialHelpAndAdvPayment");
            //update advance payment
            db.AddInParameter(cmd, "MruId", DbType.String, lineId);
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "AdvPay", DbType.String, advPay);
            db.AddInParameter(cmd, "TravelHelp", DbType.Int32, (int)line.TravelHelp);
            //db.AddInParameter(cmd, "HelpValidFrom", DbType.DateTime, line.ValidFrom);
            //db.AddInParameter(cmd, "HelpValidTo", DbType.DateTime, line.ValidTo);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, modifiedBy);
            return db.ExecuteNonQuery(cmd, trans);
        }

        public void UpdateBillStatusInfoOfBeingCreatedBook(DbTransaction trans, string invoiceNo, string inBook, string aboId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_upd_BillStatusInfoOfBeingCreatedBook");
            //update advance payment
            db.AddInParameter(cmd, "InvoiceNo", DbType.String, invoiceNo);
            db.AddInParameter(cmd, "InBook", DbType.String, inBook);
            db.AddInParameter(cmd, "AboId", DbType.String, aboId);
            db.ExecuteNonQuery(cmd, trans);
        }


        public void UpdateAdvanceBookPayment(DbTransaction trans, string bookId, decimal? advPay)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_upd_AdvBookPayment");
            //update advance payment
            db.AddInParameter(cmd, "BillBookId", DbType.String, bookId);
            db.AddInParameter(cmd, "AdvPay", DbType.String, advPay);
            db.ExecuteNonQuery(cmd, trans);
        }

        public bool CancelBillBook(string bookId)
        {
            //[ag_upd_CancelBillBook]
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_upd_CancelBillBook");
            //update advance payment
            db.AddInParameter(cmd, "BookId", DbType.String, bookId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, Session.User.Id);
            return (db.ExecuteNonQuery(cmd) > 0);
        }

        public bool CancelBillBook(DbTransaction trans, string bookId)
        {
            //[ag_upd_CancelBillBook]
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_upd_CancelBillBook");
            //update advance payment
            db.AddInParameter(cmd, "BookId", DbType.String, bookId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, Session.User.Id);
            return (db.ExecuteNonQuery(cmd, trans) > 0);
        }


        #endregion

        #region Delete Record Members

        #endregion

        #region Bill Book Creation Exclusively Used

        private ArrayList GetPeriodList(string currentPeriod, int m)
        {
            ArrayList pList = new ArrayList();
            if (m > 12) return pList;

            char[] spliter = { '/' };
            string[] sp = currentPeriod.Split(spliter);
            int year = Convert.ToInt32(sp[1]);
            int month = Convert.ToInt32(sp[0]);

            int c = 0;
            if ((month - m) < 0)
            {
                c = m - month;
                for (int i = (12 - c); i <= 12; i++)
                {
                    string t = string.Format("{0}{1}", year - 1, i.ToString().PadLeft(2, '0'));
                    pList.Add(t);
                }
            }

            for (int j = 1; j <= m - c; j++)
            {
                string t = string.Format("{0}{1}", year, j.ToString().PadLeft(2, '0'));
                pList.Add(t);
            }

            return pList;
        }

        private void GetPaidByInvoice()
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_PaidByCentralBranch");
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count == 0) return;
            _invoicePaidBy.Clear();

            foreach (DataRow r in dt.Rows)
                _invoicePaidBy.Add(DaHelper.GetString(r, "InvoiceNo").ToUpper(), null);
        }

        private string UnFormatPeriod(string period)
        {
            string[] elements = period.Split('/');
            if (elements.Length == 2)
            {
                string month = elements[0];
                string year = elements[1];
                if (year.Length == 2)
                {
                    year = "25" + year;
                }

                return string.Format("{0}{1}", year, month);
            }
            else
            {
                return "000000";
            }
        }


        public DataTable PrepareBillBookItemList(BillBookItemListInputInfo billBookParem, ref DataTable billFilteredOut)
        {
            int countLineItem = 0;
            BindingList<BillBookCreationInfo> bbCIs = billBookParem.CreationItemList;
            BindingList<BillBookCreationExtraInfo> extraInfoList = billBookParem.BookExtraItems;
            BindingList<BillBookCreationExtraInfo> tempExtraInfoList = new BindingList<BillBookCreationExtraInfo>();
            BillBookHeaderInfo bookHeader = billBookParem.HeaderInfo;

            #region sync up bill printing
            //Replace this function when billing print blue bill
            //foreach (BillBookCreationInfo ci in bbCIs)
            //{
            //    //first time launch bills - defualt back within 3 months
            //    if (bookHeader.IsFirstPaid)
            //    {
            //        //changed to 12 by user requirement Aug, 2 '07
            //        ArrayList periodList = GetPeriodList(bookHeader.Period, 12);
            //        foreach (string period in periodList)
            //            SyncInvoiceFromBLAN(ci.PeaCode, ci.LineId, period);
            //    }
            //}

            //store the invoice (notToBook only) that have paidBy(CaId) in branchId 'Z00000'
            //keep the invoice in global variable _invoicePaidBy
            GetPaidByInvoice();

            #endregion


            //get table schema - expected not found.
            string period = UnFormatPeriod(bookHeader.Period);
            DataTable billStatusTb = SelectBillStatusInfo("", "", "");
            DataTable billNotToBook = SelectBillStatusInfo("", "", "");
            loopCount = 0;

            foreach (BillBookCreationInfo ci in bbCIs)
            {
                if (extraInfoList.Count > 0)
                {
                    if ((!ci.CallingBill.Equals("1")))
                    {
                        tempExtraInfoList = extraInfoList;
                    }
                    else
                    {
                        tempExtraInfoList = new BindingList<BillBookCreationExtraInfo>();
                    }
                }
              
                if (ci.BillPeriod == "1") //all month bills
                {
                    DataTable dt = SelectBillStatusInfo(ci.PeaCode, ci.LineId, billBookParem.HeaderInfo.ControllerId);
                    if (dt.Rows.Count > 0)
                    {
                        ProcessExtraBookItem(tempExtraInfoList, ci.BillPeriod, ref dt, ref billNotToBook, period, ci.LineId);
                        billStatusTb.Merge(dt);
                    }
                }
                else if (ci.BillPeriod == "2") // current month bills
                {
                    DataTable dt = SelectBillStatusInfo(ci.PeaCode, ci.LineId, billBookParem.HeaderInfo.ControllerId);
                    if (dt.Rows.Count > 0)
                    {
                        ProcessExtraBookItem(tempExtraInfoList, ci.BillPeriod, ref dt, ref billNotToBook, period, ci.LineId);
                        billStatusTb.Merge(dt);
                    }
                }
                else if (ci.BillPeriod == "3") //past month bills
                {
                    DataTable dt = SelectBillStatusInfo(ci.PeaCode, ci.LineId, billBookParem.HeaderInfo.ControllerId);
                    if (dt.Rows.Count > 0)
                    {
                        ProcessExtraBookItem(tempExtraInfoList, ci.BillPeriod, ref dt, ref billNotToBook, period, ci.LineId);
                        billStatusTb.Merge(dt);
                    }
                }
                countLineItem++;
            }

            FilterByInBookFlag(billStatusTb, billNotToBook);

            //remove bills that have status "Cancel"
            FilterByBillCancelStatus(billStatusTb, billNotToBook);

            //fillter by first or repeated status
            if (billBookParem.HeaderInfo.IsFirstPaid)
                FilterRepeatedBill(billStatusTb, 'N', billNotToBook); // New status ออกเก็บครั้งแรก
            else
                FilterRepeatedBill(billStatusTb, 'R', billNotToBook); // Repeated bill ออกเก็บทวน

            //status of calling bill must be ' ' ค้างชำระ
            FilterBillPaymentStatus(billStatusTb, ' ', true, billNotToBook);

            //paid by central branch
            FilterByPaidByCentralBranch(billStatusTb, billNotToBook);

            //remove exceptional bill
            RemoveExceptionalBill(billStatusTb, billBookParem.ExceptionalBill, billNotToBook);

            billFilteredOut = billNotToBook;

            return billStatusTb;
        }


        public BindingList<CallingBillSummaryInfo> FillCallingBillSummaryInformation(DataTable dt, string period)
        {
            //remove duplicated bill
            Hashtable billList = new Hashtable();

            using (DataTable tempTb = dt.Copy())
            {
                for (int j = tempTb.Rows.Count - 1; j >= 0; j--)
                {
                    string invoiceNo = DaHelper.GetString(tempTb.Rows[j], "InvoiceNo");
                    if (billList.ContainsKey(invoiceNo))
                        dt.Rows.RemoveAt(j);
                    else
                        billList.Add(invoiceNo, null);
                }
            }

            //calculate some fields before filling
            DataTable currBill = FilterBillPeriod(dt, period, true);
            DataTable pastBill = FilterBillPeriod(dt, period, false);

            //get all Mru first 
            Hashtable ht = new Hashtable();
            int i = 1;
            foreach (DataRow r in dt.Rows)
            {
                if (!ht.Contains(r["MruId"]))
                {
                    CallingBillSummaryInfo cbSumInfo = new CallingBillSummaryInfo();
                    cbSumInfo.PeaCode = r["BranchId"].ToString();
                    cbSumInfo.LineId = r["MruId"].ToString();
                    cbSumInfo.Sequence = i;
                    ht.Add(r["MruId"], cbSumInfo);
                    i++;
                }
            }

            foreach (DataRow cr in currBill.Rows)
            {
                string mruId = cr["MruId"].ToString();
                CallingBillSummaryInfo sumInfo = (CallingBillSummaryInfo)ht[mruId];
                sumInfo.BillCountCurrent++;
                sumInfo.AmountCurrent += DaHelper.GetDecimal(cr, "TotalAmount");
            }

            foreach (DataRow cr in pastBill.Rows)
            {
                string mruId = cr["MruId"].ToString();
                CallingBillSummaryInfo sumInfo = (CallingBillSummaryInfo)ht[mruId];
                sumInfo.BillCountPast++;
                sumInfo.AmountPast += DaHelper.GetDecimal(cr, "TotalAmount");
            }

            //Fix me! Find the way to add this to table 
            //billSummaryInfo.SlipCount = Convert.ToInt32(r["SlipCount"]);
            //billSummaryInfo.AmountSlip = Convert.ToDouble(r["AmountSlip"]);

            //sum total
            BindingList<CallingBillSummaryInfo> billSummaryInfoList = new BindingList<CallingBillSummaryInfo>();

            try
            {
                IDictionaryEnumerator id = ht.GetEnumerator();
                while (id.MoveNext())
                {
                    CallingBillSummaryInfo cSumInfo = (CallingBillSummaryInfo)id.Value;
                    cSumInfo.TotalCount = cSumInfo.BillCountPast + cSumInfo.BillCountCurrent + cSumInfo.SlipCount;
                    cSumInfo.TotalAmount = cSumInfo.AmountCurrent + cSumInfo.AmountPast + cSumInfo.AmountSlip;
                    billSummaryInfoList.Add(cSumInfo);
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return billSummaryInfoList;
        }

        //shared b/c three views have the same template
        public BindingList<CallingBillInfo> FillCallingBillInfo(DataTable dt)
        {
            Hashtable billHt = new Hashtable();
            BindingList<CallingBillInfo> billInfoList = new BindingList<CallingBillInfo>();
            try
            {
                CallingBillInfo billInfo = null;
                int i = 1;

                foreach (DataRow r in dt.Rows)
                {
                    //check duplicated bill
                    if (billHt.ContainsKey(DaHelper.GetString(r, "InvoiceNo")))
                        continue;

                    billInfo = new CallingBillInfo();
                    billInfo.Sequence = i; i++;
                    billInfo.PeaCode = DaHelper.GetString(r, "BranchId");
                    billInfo.LineId = DaHelper.GetString(r, "MruId");
                    billInfo.BillNo = DaHelper.GetString(r, "InvoiceNo");
                    billInfo.BillPeriod = DaHelper.GetString(r, "Period");
                    billInfo.CaId = DaHelper.GetString(r, "CaId");
                    billInfo.PaymentType = DaHelper.GetString(r, "PmName");

                    if (billInfo.PaymentType == null)
                        billInfo.PaymentType = "ค้างชำระ";
                    else if (billInfo.PaymentType == "0")
                        billInfo.PaymentType = "ค้างชำระ";

                    billInfo.Amount = DaHelper.GetDecimal(r, "TotalAmount");
                    billInfo.BillStatus = DaHelper.GetString(r, "AboName");
                    billInfo.CustomerName = DaHelper.GetString(r, "CaName");
                    billInfo.TransferCode = DaHelper.GetString(r, "TkCode");
                    billInfo.NoticeFlag = DaHelper.GetString(r, "NtFlag");
                    billInfo.InvoiceNo = DaHelper.GetString(r, "InvoiceNo");
                    billInfo.BillBookId = DaHelper.GetString(r, "BpId");
                    billInfo.FloodingId = DaHelper.GetString(r, "FdId");
                    billInfo.AllowRemove = DaHelper.GetInt(r, "AllowRemove");
                    //billInfo.Note = DaHelper.GetString(r, "Note");
                    billInfoList.Add(billInfo);
                    billHt.Add(billInfo.BillNo, null);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return billInfoList;

        }

        //summarizing billbook
        public BindingList<CallingBillSummaryInfo> FindBookSummarizdInformation(BillBookItemListInputInfo billBookParem)
        {
            string period = null;
            //get all bills of this book - filtered by BillPeriod type
            DataTable billNotToBook = null;
            DataTable bookItem = PrepareBillBookItemList(billBookParem, ref billNotToBook);
            period = UnFormatPeriod(billBookParem.HeaderInfo.Period);
            return FillCallingBillSummaryInformation(bookItem, period );
        }

        //calling bill for book - this is what we store in the book
        public BindingList<CallingBillInfo> FindCallingBillInformation(BillBookItemListInputInfo billBookParem)
        {
            //get all bills of this book - filtered by BillPeriod type
            DataTable billNotToBook = null;
            DataTable bookItem = PrepareBillBookItemList(billBookParem, ref billNotToBook);
            return FillCallingBillInfo(bookItem);
        }

        public BindingList<CallingBillInfo> FindNoneCallingBillInformation(BillBookItemListInputInfo billBookParem)
        {
            //get all bills of this book - filtered by BillPeriod type
            DataTable billNotToBook = null;
            DataTable bookItem = PrepareBillBookItemList(billBookParem, ref billNotToBook);
            //put Not for each type/reason here 

            return FillCallingBillInfo(billNotToBook);
        }

        public BindingList<CallingBillInfo> FindLineCallingBillInformation(BillBookItemListInputInfo billBookParem, List<string> line)
        {
            string branchId = line[0];
            string lineId = line[1];
            //get all bills of this book - filtered by BillPeriod type
            DataTable billNotToBook = null;
            DataTable bookItem = PrepareBillBookItemList(billBookParem, ref billNotToBook);
            FilterByLineId(bookItem, branchId, lineId);

            return FillCallingBillInfo(bookItem);
        }

        public BindingList<CallingBillInfo> FindLineNoneCallingBillInformation(BillBookItemListInputInfo billBookParem, List<string> line)
        {
            string branchId = line[0];
            string lineId = line[1];
            //get all bills of this book - filtered by BillPeriod type
            DataTable billNotToBook = null;
            DataTable bookItem = PrepareBillBookItemList(billBookParem, ref billNotToBook);
            FilterByLineId(billNotToBook, branchId, lineId);

            return FillCallingBillInfo(billNotToBook);
        }

        //must allowed
        private void FilterByAllowRepeated(DataTable billTb, DataTable billNotToBook)
        {
            using (DataTable retTb = billTb.Copy())
            {
                for (int i = retTb.Rows.Count - 1; i >= 0; i--)
                {
                    char ch = Convert.ToChar(retTb.Rows[i]["AllowRepeated"]);
                    if (ch != 'Y')
                    {
                        CopyRow(billNotToBook, billTb.Rows[i], false, "ออกเก็บทวนไปแล้ว");
                        billTb.Rows.RemoveAt(i);
                    }
                }
            }
        }

        public void FilterByPaidByCentralBranch(DataTable billTb, DataTable billNotToBook)
        {
            using (DataTable temp = billTb.Copy())
            {
                //first time launch bill or repeated bill
                for (int i = temp.Rows.Count - 1; i >= 0; i--)
                {
                    string invoiceNo = DaHelper.GetString(temp.Rows[i], "InvoiceNo").ToUpper();
                    if (_invoicePaidBy.Contains(invoiceNo))
                    {
                        CopyRow(billNotToBook, temp.Rows[i], false, "ชำระโดยกองการเงิน");
                        billTb.Rows.RemoveAt(i);
                    }
                }
            }
        }

        private void CopyRow(DataTable dt, DataRow r, bool allowRemove, string note)
        {
            if (dt != null)
            {
                DataRow nr = dt.NewRow();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "AllowRemove")
                        nr["AllowRemove"] = allowRemove ? 1 : 0;
                    else if (dt.Columns[i].ColumnName == "Note")
                        nr["Note"] = note;
                    else
                        nr[i] = r[i];
                }

                dt.Rows.Add(nr);
            }
        }

        public void FilterByBillCancelStatus(DataTable billTb, DataTable billNotToBook)
        {
            using (DataTable temp = billTb.Copy())
            {
                //first time launch bill or repeated bill
                for (int i = temp.Rows.Count - 1; i >= 0; i--)
                {
                    string invId = DaHelper.GetString(temp.Rows[i], "InvoiceNo");
                    if (IsInvoiceCancelled(invId))
                    {
                        CopyRow(billNotToBook, temp.Rows[i], false, "ถูกยกเลิก");
                        billTb.Rows.RemoveAt(i);
                    }
                }
            }
        }

        public void FilterByInBookFlag(DataTable billTb, DataTable billNotToBook)
        {
            //if InBook flag sets 'Y' , not allow to put this bill in new book
            using (DataTable retTb = billTb.Copy())
            {
                for (int i = retTb.Rows.Count - 1; i >= 0; i--)
                {
                    char ch = Convert.ToChar(retTb.Rows[i]["InBook"]);
                    if (ch == 'Y') //in active book , so remove out
                    {
                        CopyRow(billNotToBook, retTb.Rows[i], false, "ออกเก็บในสมุดเล่มอื่น");
                        billTb.Rows.RemoveAt(i);
                    }
                }
            }
        }

        public void FilterRepeatedBill(DataTable billTb, char outStatus, DataTable billNotToBook)
        {
            //permitted repeated bill
            FilterByAllowRepeated(billTb, billNotToBook);
            string note = null;
            if (outStatus == 'R')
                note = "ออกเก็บทวน";
            else if (outStatus == 'N')
                note = "ออกเก็บครั้งแรก";
            else
                note = "N/A"; //should not get here 

            using (DataTable temp = billTb.Copy())
            {
                //first time launch bill or repeated bill
                for (int i = temp.Rows.Count - 1; i >= 0; i--)
                {
                    char st = Convert.ToChar(temp.Rows[i]["AboId"]);
                    if (st != outStatus)
                    {
                        CopyRow(billNotToBook, temp.Rows[i], false, string.Format("นอกเหนือจาก '{0}'", note));
                        billTb.Rows.RemoveAt(i);
                    }
                }
            }

        }

        public void RemoveExceptionalBill(DataTable billTb, List<CallingBillInfo> exceptionalList, DataTable billNotToBook)
        {
            if (exceptionalList == null) return;

            using (DataTable temp = billTb.Copy())
            {
                for (int i = temp.Rows.Count - 1; i >= 0; i--)
                {
                    //workaround for webservice problem/ we cannot pass hashtable via soap
                    CallingBillInfo ci = new CallingBillInfo();
                    ci.InvoiceNo = temp.Rows[i]["InvoiceNo"].ToString().Trim();
                    exceptionalList.Sort();
                    int index = exceptionalList.BinarySearch(ci);

                    if (index >= 0) //found - contains
                    {
                        CopyRow(billNotToBook, billTb.Rows[i], true, "บิลป้อนยกเว้น");
                        billTb.Rows.RemoveAt(i);
                    }
                }
            }
        }

        //add new column to table - billPayment Status and then sync staus with POS
        public void FilterBillPaymentStatus(DataTable billTb, char payStatus, bool equal, DataTable billNotToBook)
        {
            DataTable retTb = billTb.Copy();

            if (equal)
            {
                for (int i = retTb.Rows.Count - 1; i >= 0; i--)
                {
                    if (payStatus == ' ') // null = ค้างชำระ
                    {
                        if (retTb.Rows[i]["PmId"] != System.DBNull.Value && Convert.ToChar(retTb.Rows[i]["PmId"]) != 'N')
                        {
                            CopyRow(billNotToBook, billTb.Rows[i], false, "-");
                            billTb.Rows.RemoveAt(i);
                        }
                    }
                    else
                    {
                        char ch = Convert.ToChar(retTb.Rows[i]["PmId"]);
                        if (ch != payStatus)
                        {
                            CopyRow(billNotToBook, billTb.Rows[i], false, "-");
                            billTb.Rows.RemoveAt(i);
                        }
                    }
                }
            }
            else
            {
                for (int i = retTb.Rows.Count - 1; i >= 0; i--)
                {
                    if (payStatus == ' ') // null = ค้างชำระ
                    {
                        if (retTb.Rows[i]["PmId"] == System.DBNull.Value || Convert.ToChar(retTb.Rows[i]["PmId"]) == 'N')
                        {
                            CopyRow(billNotToBook, billTb.Rows[i], false, "-");
                            billTb.Rows.RemoveAt(i);
                        }
                    }
                    else
                    {
                        char ch = Convert.ToChar(retTb.Rows[i]["PmId"]);
                        if (ch == payStatus)
                        {
                            CopyRow(billNotToBook, billTb.Rows[i], false, "-");
                            billTb.Rows.RemoveAt(i);
                        }
                    }
                }
            }

        }

        public void FilterByLineId(DataTable billTb, string peaCode, string lineId)
        {
            using (DataTable temp = billTb.Copy())
            {
                for (int i = temp.Rows.Count - 1; i >= 0; i--)
                {
                    if (!(string.Equals(temp.Rows[i]["BranchId"].ToString(), peaCode, StringComparison.CurrentCultureIgnoreCase)
                                && temp.Rows[i]["MruId"].ToString() == lineId))
                        billTb.Rows.RemoveAt(i);
                }
            }
        }

        public DataTable FilterBillPeriod(DataTable billTb, string period, bool rmOldBill)
        {
            //rmOldBill - true= memove old month's bills , false = remove current month's bills
            //DateTime nDt = Session.BpmDateTime.Now;
            ////string cDtStr = Session.BpmDateTime.Now.ToString("yyyyMMdd", _th_dt).Substring(0, 6);
            //string cDtStr = Session.BpmDateTime.Now.ToString("yyyyMM", _th_dt);
            //int curPeriod = Convert.ToInt32(cDtStr);
            int curPeriod = Convert.ToInt32(period);

            DataTable output = billTb.Copy();
            for (int i = billTb.Rows.Count - 1; i >= 0; i--)
            {
                int billPeriod = Convert.ToInt32(billTb.Rows[i]["Period"]);

                if (rmOldBill) //only current month bill permitted, otherwise discarded
                {
                    if (curPeriod != billPeriod)
                        output.Rows.RemoveAt(i);
                }
                else
                {
                    //only old bill permitted, otherwise discarded
                    if (curPeriod <= billPeriod)
                        output.Rows.RemoveAt(i);
                }
            }

            return output;
        }

        public DataTable FilterBillPeriod(DataTable billTb, bool rmOldBill)
        {
            //rmOldBill - true= memove old month's bills , false = remove current month's bills
            DateTime nDt = Session.BpmDateTime.Now;
            //string cDtStr = Session.BpmDateTime.Now.ToString("yyyyMMdd", _th_dt).Substring(0, 6);
            string cDtStr = Session.BpmDateTime.Now.ToString("yyyyMM", _th_dt);
            int curPeriod = Convert.ToInt32(cDtStr);


            DataTable output = billTb.Copy();
            for (int i = billTb.Rows.Count - 1; i >= 0; i--)
            {
                int billPeriod = Convert.ToInt32(billTb.Rows[i]["Period"]);

                if (rmOldBill) //only current month bill permitted, otherwise discarded
                {
                    if (curPeriod != billPeriod)
                        output.Rows.RemoveAt(i);
                }
                else
                {
                    //only old bill permitted, otherwise discarded
                    if (curPeriod <= billPeriod)
                        output.Rows.RemoveAt(i);
                }
            }

            return output;
        }

        //ทั้งหมด ป้อนยกเว้น
        private void FilterAllBillWithTypeTwo(BillBookCreationExtraInfo ec, ref DataTable billTb, DataTable billNotToBook, string line)
        {
            using (DataTable temp = billTb.Copy())
            {
                //first time launch bill or repeated bill
                for (int i = temp.Rows.Count - 1; i >= 0; i--)
                {
                    if (DaHelper.GetString(temp.Rows[i], "CaId") == ec.Number)
                    {
                        CopyRow(billNotToBook, temp.Rows[i], false, "บิลป้อนยกเว้น");
                        billTb.Rows.RemoveAt(i);
                    }
                }
            }
        }

        //ทั้งหมด ป้อนออกเก็บ
        private void FilterAllBillWithTypeThree(BillBookCreationExtraInfo ec, ref DataTable billTb, string period, string line)
        {
            //DataTable invs = SelectBillStatusInfoByCaId(ec.NPeaCode, ec.NLineId, ec.Number);
            //foreach (DataRow ivnr in invs.Rows)
            //    CopyRow(billTb, ivnr, true, "-");

            //DataTable curMonthBill = FilterBillPeriod(billTb, period, true); //current month bills
            //string curPeriod = UnFormatPeriod(period);
            //past month bills
            //DataTable invs = SelectBillStatusInfoByCaId(ec.NPeaCode, ec.NLineId, ec.Number, curPeriod, false);
            //foreach (DataRow ivnr in invs.Rows)
            //    CopyRow(billTb, ivnr, true, "-")
            DataTable invs = null;
            bool allowRemove = true;
            string note = "-";
            if (line.Trim().Equals(ec.NLineId))
            {
                invs = SelectBillStatusInfoByCaId(ec.NPeaCode, ec.NLineId, ec.Number, "100001", false);
            }
            else
            {
                invs = new DataTable();
            }
            if (loopCount == 0 && line.Trim().Equals(ec.NLineId))
            {
                billTb.Clear();
                loopCount++;
            }

            foreach (DataRow ivnr in invs.Rows)
            {
                DataRow nr = billTb.NewRow();
                for (int i = 0; i < billTb.Columns.Count; i++)
                {
                    if (billTb.Columns[i].ColumnName == "AllowRemove")
                        nr["AllowRemove"] = allowRemove ? 1 : 0;
                    else if (billTb.Columns[i].ColumnName == "Note")
                        nr["Note"] = note;
                    else
                        nr[i] = ivnr[i];
                }
                billTb.Rows.Add(nr);
            }
        }

        //ทั้งหมด (ปัจจุบัน + เดือนเก่าป้อนยกเว้น)
        private void FilterAllBillWithTypeFour(BillBookCreationExtraInfo ec, ref DataTable billTb, DataTable billNotToBook, string period, string line)
        {
            DataTable curMonthBill = FilterBillPeriod(billTb, period, true); //current month bills
            DataTable pastMonthBill = FilterBillPeriod(billTb, period, false); //past month bills

            using (DataTable temp = pastMonthBill.Copy())
            {
                for (int i = temp.Rows.Count - 1; i >= 0; i--)
                {
                    if (DaHelper.GetString(temp.Rows[i], "CaId") == ec.Number)
                    {
                        CopyRow(billNotToBook, temp.Rows[i], false, "บิลป้อนยกเว้น");
                        pastMonthBill.Rows.RemoveAt(i);
                    }
                }
            }

            SetAllowRemove(ref pastMonthBill, false);
            curMonthBill.Merge(pastMonthBill);
            billTb = curMonthBill;
        }

        //ทั้งหมด (ปัจจุบัน + เดือนเก่าป้อนออกเก็บ)
        private void FilterAllBillWithTypeFive(BillBookCreationExtraInfo ec, ref DataTable billTb, string period, string line)
        {
            //string curPd = Session.BpmDateTime.Now.ToString("yyyyMM", _th_dt);
            //past month bills
            //DataTable invs = SelectBillStatusInfoByCaId(ec.NPeaCode, ec.NLineId, ec.Number, curPeriod, false);
            //foreach (DataRow ivnr in invs.Rows)
            //    CopyRow(billTb, ivnr, true, "-")
            DataTable invs = null;
            bool allowRemove = true;
            string note = "-";
            DataTable curMonthBill = FilterBillPeriod(billTb, period, true); //current month bills
            
            if (line.Trim().Equals(ec.NLineId))
            {
                invs = SelectBillStatusInfoByCaId(ec.NPeaCode, ec.NLineId, ec.Number, period, false);
            }
            else
            {
                invs = new DataTable();
            }

            if (loopCount == 0 && line.Trim().Equals(ec.NLineId))
            {
                billTb = curMonthBill;
                loopCount++;
            }

            foreach (DataRow ivnr in invs.Rows)
            {
                DataRow nr = billTb.NewRow();
                for (int i = 0; i < billTb.Columns.Count; i++)
                {
                    if (billTb.Columns[i].ColumnName == "AllowRemove")
                        nr["AllowRemove"] = allowRemove ? 1 : 0;
                    else if (billTb.Columns[i].ColumnName == "Note")
                        nr["Note"] = note;
                    else
                        nr[i] = ivnr[i];
                }
                billTb.Rows.Add(nr);
            }
        }

        //ปัจจุบัน ป้อนยกเว้น
        private void FilterCurrentMonthBillWithTypeTwo(BillBookCreationExtraInfo ec, ref DataTable billTb, DataTable billNotToBook, string period, string line)
        {
            DataTable curMonthBill = FilterBillPeriod(billTb, period, true); //current month bills
            DataTable pastMonthBill = FilterBillPeriod(billTb, period, false); //past month bills

            using (DataTable temp = curMonthBill.Copy())
            {
                //first time launch bill or repeated bill
                for (int i = temp.Rows.Count - 1; i >= 0; i--)
                {
                    if (DaHelper.GetString(temp.Rows[i], "CaId") == ec.Number)
                    {
                        CopyRow(billNotToBook, temp.Rows[i], false, "บิลป้อนยกเว้น");
                        curMonthBill.Rows.RemoveAt(i);
                    }
                }
            }

            billTb = curMonthBill;
            SetAllowRemove(ref pastMonthBill, false);
            billNotToBook.Merge(pastMonthBill);
        }

        // ปัจจุบัน ป้อนออกเก็บ
        private void FilterCurrentMonthBillWithTypeThree(BillBookCreationExtraInfo ec, ref DataTable billTb, DataTable billNotToBook, string period, string line)
        {
            //string curPd = Session.BpmDateTime.Now.ToString("yyyyMM", _th_dt);
            //get current month bills
            //DataTable invs = SelectBillStatusInfoByCaId(ec.NPeaCode, ec.NLineId, ec.Number, curPd, true);

            //foreach (DataRow ivnr in invs.Rows)
            //    CopyRow(curMonthBill, ivnr, true, "-");
            //if (invs != null)
            //{
            //    for (int i = 0; i < invs.Rows.Count; i++)
            //    {
            //        curMonthBill.Rows.Add(invs.Rows[i]);
            //    }
            //}

            //if (invs != null)
            //{
            //    for (int i = 0; i < invs.Rows.Count; i++)
            //    {
            //        curMonthBill.Rows.Add(invs.Rows[i]);
            //    }
            //}

            DataTable curMonthBill = FilterBillPeriod(billTb, period, true); //current month bills
            DataTable pastMonthBill = FilterBillPeriod(billTb, period, false); //past month bills
            DataTable invs = null;
            bool allowRemove = true;
            string note = "-";
           
            if (line.Trim().Equals(ec.NLineId))
            {
                invs = SelectBillStatusInfoByCaId(ec.NPeaCode, ec.NLineId, ec.Number, period, true);
            }
            else
            {
                invs = new DataTable();
            }

            if (loopCount == 0 && line.Trim().Equals(ec.NLineId))
            {
                billTb.Clear();
                loopCount++;
            }
            curMonthBill = invs;
            foreach (DataRow ivnr in invs.Rows)
            {
                DataRow nr = billTb.NewRow();
                for (int i = 0; i < billTb.Columns.Count; i++)
                {
                    if (billTb.Columns[i].ColumnName == "AllowRemove")
                        nr["AllowRemove"] = allowRemove ? 1 : 0;
                    else if (billTb.Columns[i].ColumnName == "Note")
                        nr["Note"] = note;
                    else
                        nr[i] = ivnr[i];
                }
                billTb.Rows.Add(nr);
            }
            billNotToBook = pastMonthBill;
        }

        // เดือนเก่า + ป้อนยกเว้น
        private void FilterPastMonthBillWithTypeTwo(BillBookCreationExtraInfo ec, ref DataTable billTb, DataTable billNotToBook, string period, string line)
        {
            DataTable curMonthBill = FilterBillPeriod(billTb, period, true); //current month bills
            DataTable pastMonthBill = FilterBillPeriod(billTb, period, false); //past month bills

            using (DataTable temp = pastMonthBill.Copy())
            {
                //first time launch bill or repeated bill
                for (int i = temp.Rows.Count - 1; i >= 0; i--)
                {
                    if (DaHelper.GetString(temp.Rows[i], "CaId") == ec.Number)
                    {
                        CopyRow(billNotToBook, temp.Rows[i], false, "บิลป้อนยกเว้น");
                        pastMonthBill.Rows.RemoveAt(i);
                    }
                }
            }

            SetAllowRemove(ref curMonthBill, false);
            billNotToBook.Merge(curMonthBill);
            billTb = pastMonthBill;
        }

        //เดือนเก่าป้อนออกเก็บ
        private void FilterPastMonthBillWithTypeThree(BillBookCreationExtraInfo ec, ref DataTable billTb, DataTable billNotToBook, string period, string line)
        {
            //string curPd = Session.BpmDateTime.Now.ToString("yyyyMM", _th_dt);
            //string curPeriod = UnFormatPeriod(period);
            //DataTable curMonthBill = FilterBillPeriod(billTb, period, true); //current month bills
            //DataTable pastMonthBill = FilterBillPeriod(billTb, period, false); //past month bills

            //DataTable invs = SelectBillStatusInfoByCaId(ec.NPeaCode, ec.NLineId, ec.Number, curPeriod, false);

            //foreach (DataRow ivnr in invs.Rows)
            //    CopyRow(pastMonthBill, ivnr, true, "-");

            //billTb = pastMonthBill;
            //billNotToBook = curMonthBill;
            DataTable invs = null;
            bool allowRemove = true;
            string note = "-";
            string curPeriod = period;
            DataTable curMonthBill = FilterBillPeriod(billTb, period, true); //current month bills
            DataTable pastMonthBill = FilterBillPeriod(billTb, period, false); //past month bills
            
            if (line.Trim().Equals(ec.NLineId))
            {
                invs = SelectBillStatusInfoByCaId(ec.NPeaCode, ec.NLineId, ec.Number, curPeriod, false);
            }
            else
            {
                invs = new DataTable();
            }

            if (loopCount == 0 && line.Trim().Equals(ec.NLineId))
            {
                billTb.Clear();
                loopCount++;
            }

            pastMonthBill = invs;
            foreach (DataRow ivnr in invs.Rows)
            {
                DataRow nr = billTb.NewRow();
                for (int i = 0; i < billTb.Columns.Count; i++)
                {
                    if (billTb.Columns[i].ColumnName == "AllowRemove")
                        nr["AllowRemove"] = allowRemove ? 1 : 0;
                    else if (billTb.Columns[i].ColumnName == "Note")
                        nr["Note"] = note;
                    else
                        nr[i] = ivnr[i];
                }
                billTb.Rows.Add(nr);
            }
            billNotToBook = curMonthBill;
        }

        private void SetAllowRemove(ref DataTable billTb, bool allowRemove)
        {
            foreach (DataRow r in billTb.Rows)
                r["AllowRemove"] = allowRemove;
        }

        private void SetNote(ref DataTable billTb, string note)
        {
            foreach (DataRow r in billTb.Rows)
                r["Note"] = note;
        }

        public void ProcessExtraBookItem(BindingList<BillBookCreationExtraInfo> eInfo, string billPeriod, ref DataTable billTb, ref DataTable billNotToBook, string period, string line)
        {
            Hashtable extraHt = new Hashtable();
            if (eInfo.Count == 0)
            {
                DataTable curMonthBill = FilterBillPeriod(billTb, period, true); //current month bills
                DataTable pastMonthBill = FilterBillPeriod(billTb, period, false); //past month bills

                //no need to show billNotToBook for this case
                if (billPeriod == "2")
                {
                    billTb = curMonthBill;
                    //billNotToBook = pastMonthBill;    
                }
                else if (billPeriod == "3")
                {
                    billTb = pastMonthBill;
                    //billNotToBook = curMonthBill;
                }

                //SetAllowRemove(ref billNotToBook, false);
                return;
            }
            loopCount = 0;
            foreach (BillBookCreationExtraInfo ec in eInfo)
            {
                if (billPeriod == "1") //current+past
                {
                    switch (ec.FilterType)
                    {
                        case "1": //ignore
                            break;
                        case "2":
                            FilterAllBillWithTypeTwo(ec, ref billTb, billNotToBook, line);
                            break;
                        case "3":
                            FilterAllBillWithTypeThree(ec, ref billTb, period, line);
                            break;
                        case "4":
                            FilterAllBillWithTypeFour(ec, ref billTb, billNotToBook, period, line);
                            break;
                        case "5":
                            FilterAllBillWithTypeFive(ec, ref billTb, period, line);
                            break;
                    };
                }
                else if (billPeriod == "2") //current only
                {
                    billTb = FilterBillPeriod(billTb, period, true);
                    switch (ec.FilterType)
                    {
                        case "1":
                            break;
                        case "2":
                            FilterCurrentMonthBillWithTypeTwo(ec, ref billTb, billNotToBook, period, line);
                            break;
                        case "3":
                            FilterCurrentMonthBillWithTypeThree(ec, ref billTb, billNotToBook, period, line);
                            break;
                        case "4":
                            //FilterCurrentMonthBillWithTypeFour(ec, ref billTb, billNotToBook);
                            //not valid
                            break;
                        case "5":
                            //FilterCurrentMonthBillWithTypeFive(ec, ref billTb, billNotToBook);
                            //not valid
                            break;
                    };
                }
                else if (billPeriod == "3") //past only
                {
                    billTb = FilterBillPeriod(billTb, period, false);
                    switch (ec.FilterType)
                    {
                        case "1":
                            break;
                        case "2":
                            FilterPastMonthBillWithTypeTwo(ec, ref billTb, billNotToBook, period, line);
                            break;
                        case "3":
                            FilterPastMonthBillWithTypeThree(ec, ref billTb, billNotToBook, period, line);
                            break;
                        case "4":
                            //FilterPastMonthBillWithTypeTwo(ec, ref billTb, billNotToBook);
                            //not valid
                            break;
                        case "5":
                            //FilterPastMonthBillWithTypeTwo(ec, ref billTb, billNotToBook);
                            //not valid
                            break;
                    };
                }
            }
        }

        public PeaInfo GetBranchInfoByAgencyId(string agencyId)
        {
            PeaInfo peaInfo = new PeaInfo();

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_BranchByAgencyId");
            db.AddInParameter(cmd, "pAgencyId", DbType.String, agencyId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count == 0) return new PeaInfo();

            //expect only one row
            DataRow r = dt.Rows[0];
            peaInfo.Id = DaHelper.GetString(r, "BranchId");
            peaInfo.Name = DaHelper.GetString(r, "BranchName");
            peaInfo.Address = DaHelper.GetString(r, "Address");
            peaInfo.BranchNo = DaHelper.GetString(r, "BranchNo");
            peaInfo.BranchLevel = r["BranchLevel"].ToString();

            return peaInfo;
        }

        //BACode ของตัวแทนที่ลงทะเบียนภายใต้การไฟฟ้ายกฐานะ
        public string GetAgentBACode(string agencyTechBranchId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_AgencyBACode");
            db.AddInParameter(cmd, "AgencyTechBranchId", DbType.String, agencyTechBranchId);
            return db.ExecuteScalar(cmd).ToString();
        }
        #endregion


        #region IAgencyRepository Members


        public void UpdateMRUPlanCreateBillBook(DbTransaction trans, DateTime createDate, string bookId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

