using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PEA.BPM.AgencyManagementModule.Interface.Services;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.AgencyManagementModule.Interface.Constants;
using System.Globalization;

namespace PEA.BPM.AgencyManagementModule.DA
{
                                        
    public class CommissionDataAccess : ICommissionRepository
    {

        #region"Public Function"

        #region "Get Function"

        public bool IsCommissionCalculated(string billBookId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_CommissionCalculated");
            db.AddInParameter(cmd, "@pBillBookId", DbType.String, billBookId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            return dt.Rows.Count > 0;
        }

        public List<BranchIdInfoItem> GetAllBranch()
        {
            List<BranchIdInfoItem> retVals = new List<BranchIdInfoItem>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_AllBranches");
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    BranchIdInfoItem b = new BranchIdInfoItem();
                    b.BranchId = DaHelper.GetString(r, "BranchId");
                    b.BranchName = DaHelper.GetString(r, "BranchName");
                    b.BranchGroupId = DaHelper.GetString(r, "ParentBranchId") == null ? DaHelper.GetString(r, "BranchId") : DaHelper.GetString(r, "ParentBranchId");
                    b.BranchLevel = DaHelper.GetString(r, "BranchLevel");

                    retVals.Add(b);
                }
            }
            return retVals;
        }

        public List<BranchIdInfoItem> GetBranchSameArea(BranchIdInfoItem branch)
        {
            List<BranchIdInfoItem> retVals = new List<BranchIdInfoItem>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_BranchSameArea");
            db.AddInParameter(cmd, "@pBranchId", DbType.String, branch.BranchId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    BranchIdInfoItem b = new BranchIdInfoItem();
                    b.BranchId = DaHelper.GetString(r, "BranchId");
                    b.BranchName = DaHelper.GetString(r, "BranchName");
                    b.BranchLevel = DaHelper.GetString(r, "BranchLevel");
                    b.BranchGroupId = branch.BranchId;
                    retVals.Add(b);
                }
            }
            return retVals;
        }

        public List<BranchIdInfoItem> GetBranchUnderParent(BranchIdInfoItem branch)
        {
            List<BranchIdInfoItem> retVals = new List<BranchIdInfoItem>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_BranchesByParent");
            db.AddInParameter(cmd, "@ParentBranchId", DbType.String, branch.BranchId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    BranchIdInfoItem b = new BranchIdInfoItem();
                    b.BranchId = DaHelper.GetString(r, "BranchId");
                    b.BranchName = DaHelper.GetString(r, "BranchName");
                    b.BranchLevel = DaHelper.GetString(r, "BranchLevel");
                    b.BranchGroupId = branch.BranchId;
                    retVals.Add(b);
                }
            }
            retVals.Add(branch);
            return retVals;
        }

        public List<BranchIdInfoItem> GetFromToBranch(string branchFrom, string branchTo, List<BranchIdInfoItem> scope)
        {
            List<BranchIdInfoItem> retVals = new List<BranchIdInfoItem>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_BranchFromTo");
            db.AddInParameter(cmd, "@pBranchIdFrom", DbType.String, branchFrom);
            db.AddInParameter(cmd, "@pBranchIdTo", DbType.String, branchTo);

            bool found = false;

            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow r in dt.Rows)
            {
                BranchIdInfoItem b = new BranchIdInfoItem();
                b.BranchId = DaHelper.GetString(r, "BranchId");
                b.BranchName = DaHelper.GetString(r, "BranchName");

                foreach (BranchIdInfoItem bi in scope)
                {
                    if (bi.BranchId == b.BranchId)
                    {
                        found = true;
                        break;
                    }                   
                }
                if (found)
                {
                    retVals.Add(b);
                    found = false;
                }
            }
            return retVals;
        }

        public List<BillBookCommissionBranchInfo> GetBillBookCommissionBranch(string branchId, string periodFrom, string periodTo)
        {
            List<BillBookCommissionBranchInfo> commissionInfo = new List<BillBookCommissionBranchInfo>();
            Database db = DatabaseFactory.CreateDatabase("PosDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_BillBookCommissionBranch");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pPeriodFrom", DbType.String, periodFrom);
            db.AddInParameter(cmd, "pPeriodTo", DbType.String, periodTo);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow r in dt.Rows)
            {                
                BillBookCommissionBranchInfo bbc = new BillBookCommissionBranchInfo();
                bbc.Area = DaHelper.GetString(r, "Area");
                bbc.AreaName = DaHelper.GetString(r, "AreaName");
                bbc.BranchId = DaHelper.GetString(r, "BranchId");
                bbc.BranchName = DaHelper.GetString(r, "BranchName");
                bbc.CmId = DaHelper.GetString(r, "CmId");
                bbc.BaseCmAmount = DaHelper.GetDecimal(r, "BaseCmAmount");
                bbc.SpecialCmAmount = DaHelper.GetDecimal(r, "SpecialCmAmount");
                bbc.InvCmAmount = DaHelper.GetDecimal(r, "InvCmAmount");
                bbc.OverNinety = DaHelper.GetString(r, "OverNinety");
                bbc.TransCost = DaHelper.GetDecimal(r, "TransCost");
                bbc.FarLandHelp = DaHelper.GetDecimal(r, "FarLandHelp");
                bbc.SpecialMoney = DaHelper.GetDecimal(r, "SpecialMoney");
                bbc.AgencyId = DaHelper.GetString(r, "AgencyId");
                bbc.BookPeriod = DaHelper.GetString(r, "BookPeriod");
                bbc.BpTypeId = DaHelper.GetString(r, "BpTypeId");
                bbc.TotalBillCollected = DaHelper.GetInt(r, "TotalBillCollected");
                bbc.TotalCollectedAmount = DaHelper.GetDecimal(r, "TotalCollectedAmount");
                bbc.TotalBill = DaHelper.GetInt(r, "TotalBill");
                bbc.TotalBookAmount = DaHelper.GetDecimal(r, "TotalBookAmount");

                commissionInfo.Add(bbc);
            }
            return commissionInfo;

        }

        public CommissionRateInfo GetRateCommissionForCalculate(string branchid)
        {
            CommissionRateInfo commissionRate = new CommissionRateInfo();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            
            if (string.IsNullOrEmpty(branchid))
            {
                string user = Session.User.Id;
                string getuserquery = "select * from ta.[User] where UserId = '" + user + "'";
                DbCommand usercmd = db.GetSqlStringCommand(getuserquery);
                DataTable userdt = db.ExecuteDataSet(usercmd).Tables[0];
                branchid = userdt.Rows[0]["BranchId"].ToString().Trim() + "01"; // adhoc บวก 01 ต่อท้ายไปก่อน
            }

            string strSql = "Select top 1 * From mt.AgencyCommissionRate WHERE BranchId = '" + branchid + "' Order by RtId desc";
            DbCommand cmd = db.GetSqlStringCommand(strSql);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count != 0)
            {
                DataRow r = dt.Rows[0];
                commissionRate.PersonRateForResident = DaHelper.GetDecimal(r, "HouseRegRate");
                commissionRate.CompanyRateForResident = DaHelper.GetDecimal(r, "HouseGrpRate");
                commissionRate.PersonRateForSmallBiz = DaHelper.GetDecimal(r, "CorpRegRate");
                commissionRate.CompanyRateForSmallBiz = DaHelper.GetDecimal(r, "CorpGrpRate");
                commissionRate.PersonRateForGoverment = DaHelper.GetDecimal(r, "GovRegRate");
                commissionRate.CompanyRateForGoverment = DaHelper.GetDecimal(r, "GovGrpRate");
                commissionRate.Special70To90Rate = DaHelper.GetDecimal(r, "BillingNinetyPercent");
                commissionRate.SpecialMoreThan90Rate = DaHelper.GetDecimal(r, "BillingNinetyNinePercent");
                commissionRate.PasteBill = DaHelper.GetDecimal(r, "InvRate");
                commissionRate.PasteBillThreeMonthRate = DaHelper.GetDecimal(r, "InvPastThreeMonthRate");
                commissionRate.IncludeRateForKeepAllBill = DaHelper.GetDecimal(r, "BillingHundredPercent");
            }
            return commissionRate;
        }        

        public string GetAgencyTypeById(string agencyId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            string strSql = "Select c.BpTypeId From (mt.ContractAccount a inner join mt.BusinessPartner ";
            strSql = strSql + "b on a.BpId=b.Bpid)inner join mt.BusinessPartnerType c on c.BpTypeId=b.BpTypeId Where";
            strSql = strSql + " a.CaId = '" + agencyId.PadLeft(12, '0') + "'";
            DbCommand cmd = db.GetSqlStringCommand(strSql);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count == 0) return String.Empty;

            DataRow r = dt.Rows[0];
            string bpTypeId = DaHelper.GetString(r, "BpTypeId");
            return bpTypeId;
        }

        public decimal? GetAgencyDebtAmount(string agencyId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_AgencyDebtAmount");
            db.AddInParameter(cmd, "pAgencyId", DbType.String, agencyId);
            return (decimal?)db.ExecuteScalar(cmd);
        }

        public string GetBranchElectricCode(string agencyId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            string strSql = "Select distinct BranchId From ts.RTAgencyContractMru Where CaId = '" + agencyId.PadLeft(12, '0') + "'";
            DbCommand cmd = db.GetSqlStringCommand(strSql);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            BranchIdInfoItem b = new BranchIdInfoItem();
            if (dt.Rows.Count == 0) return String.Empty;

            DataRow r = dt.Rows[0];
            string result = DaHelper.GetString(r, "BranchID");
            return result;
        }

        public string GetBusinessTypeDeesc(string bpTypeId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            string strSql = "Select BpTypeDesc From mt.BusinessPartnerType Where";
            strSql = strSql + " BpTypeId  = '" + bpTypeId + "'";
            DbCommand cmd = db.GetSqlStringCommand(strSql);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count == 0) return String.Empty;

            DataRow r = dt.Rows[0];
            string bpTypeDesc = DaHelper.GetString(r, "BpTypeDesc");
            return bpTypeDesc;
        }      

        public string GetBillBookIdByAgentIdPeriodAndCreateDate(string agentId, string billBookPeriod, string createDate)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            string strSql = "Select BillBookId From ts.BillBook Where BookHolderId = '" + agentId.PadLeft(12, '0') + "' and BookPeriod = '" +
                    billBookPeriod + "' and Convert(Varchar(10),CreateDate,103) = '" + createDate + "'";
            DbCommand cmd = db.GetSqlStringCommand(strSql);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count == 0) return String.Empty;

            DataRow r = dt.Rows[0];
            string billBookId = DaHelper.GetString(r, "BillBookId");
            return billBookId;
        }

        public ArrayList GetDetailAgencyById(string agencyId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            string strSql = "Select * From mt.ContractAccount Where CaId = '" + agencyId.PadLeft(12, '0') + "'";
            DbCommand cmd = db.GetSqlStringCommand(strSql);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count == 0) return new ArrayList() ;

            DataRow r = dt.Rows[0];
            ArrayList myResult = new ArrayList();
            myResult.Add(DaHelper.GetString(r, "CaName"));
            myResult.Add(DaHelper.GetString(r, "CaAddress"));
            return myResult;
        }

        public ArrayList GetExtraMoneyHelpCommissionById(string commissionId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            string strSql = "Select * From ts.AgencyCommission Where cmId = '" + commissionId + "'";
            DbCommand cmd = db.GetSqlStringCommand(strSql);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count == 0) return new ArrayList();

            DataRow r = dt.Rows[0];
            ArrayList myResult = new ArrayList();
            myResult.Add(DaHelper.GetDecimal(r, "TransCost"));
            myResult.Add(DaHelper.GetDecimal(r, "FarLandHelp"));
            myResult.Add(DaHelper.GetDecimal(r, "SpecialMoney"));
            myResult.Add(DaHelper.GetDate(r, "CalDt"));
            if (DaHelper.GetInt(r, "CalCount") != null)
            {
                int? countNumber = DaHelper.GetInt(r, "CalCount");
                myResult.Add(countNumber);
            }
            else
            { myResult.Add("00"); }
            if (DaHelper.GetDecimal(r, "FineAmount") != null)
            { myResult.Add(DaHelper.GetDecimal(r, "FineAmount")); }
            else
            { myResult.Add("00"); }

            return myResult;
        }

        public string GetBranchNameByAgentId(string agentId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            string strSql = "Select b.BranchName From ts.RTAgencyContractMru a " +
                            "inner join mt.Branch b on a.BranchId=b.BranchId Where a.Caid='" + agentId.PadLeft(12, '0') + "'";
            DbCommand cmd = db.GetSqlStringCommand(strSql);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count == 0) return String.Empty;

            DataRow r = dt.Rows[0];
            string branchName = DaHelper.GetString(r, "BranchName");
            return branchName;
        }


        public List<BillPasteReportInfo> GetBillPasteInBillBookReturn(string agentId, string billbookId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_BillPasteStatusInBillBookReturn");
            db.AddInParameter(cmd, "AgentId", DbType.String, agentId);
            db.AddInParameter(cmd, "BillBookId", DbType.String, billbookId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            List<BillPasteReportInfo> billPasteList = new List<BillPasteReportInfo>();
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                DataRow r = dt.Rows[i];
                BillPasteReportInfo itemList = new BillPasteReportInfo();
                itemList.BillBookRef = DaHelper.GetString(r, "BillBookId");
                itemList.MruId = DaHelper.GetString(r, "MruId");
                itemList.ElectricNo = DaHelper.GetString(r, "CaId");
                itemList.BillPeroid = DaHelper.GetString(r, "Period");
                itemList.ElectricPrice = DaHelper.GetDecimal(r, "TotalAmount");
                itemList.BaseAmount = DaHelper.GetDecimal(r, "BaseAmount");
                itemList.FTPrice = DaHelper.GetDecimal(r, "FT");
                itemList.VatPrice = DaHelper.GetDecimal(r, "Vat");
                itemList.CheckInDate = DaHelper.GetDate(r, "CheckIndate");
                itemList.PmId = DaHelper.GetString(r, "PmId");
                billPasteList.Add(itemList);
            }
            return billPasteList;
        }

        public string GetNameAgencyById(string agencyId)
        {
            //System.Diagnostics.Debugger.Break();

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            string strSql = "Select * From mt.ContractAccount Where CaId = '{0}'";
            strSql = String.Format(strSql, agencyId.PadLeft(12, '0'));
            DbCommand cmd = db.GetSqlStringCommand(strSql);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count == 0) return String.Empty;

            DataRow r = dt.Rows[0];
            string agencyName = String.Format(" {0} {1}", DaHelper.GetString(r, "CaId").Substring(4), DaHelper.GetString(r, "CaName"));
            return agencyName;
        }

        public string GetBranchNameByBranchId(string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            string strSql = "Select * From mt.Branch Where BranchId = '" + branchId + "'";
            DbCommand cmd = db.GetSqlStringCommand(strSql);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count == 0) return String.Empty;

            DataRow r = dt.Rows[0];
            string branchName = DaHelper.GetString(r, "BranchName");
            return branchName;
        }

        public string GetHeaderCodeOfPEAByBranchId(string peaCode)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            string strSql = "Select BranchId From mt.Branch Where BranchId in (Select ParentBranchId From " +
                    "mt.Branch Where BranchId = '" + peaCode + "')";
            DbCommand cmd = db.GetSqlStringCommand(strSql);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count == 0) return String.Empty ;

            DataRow r = dt.Rows[0];
            string branchId = DaHelper.GetString(r, "BranchId");
            return branchId;
        }

        public string GetHeaderNameOfPEAByBranchId(string peaCode)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            string strSql = "Select BranchName From mt.Branch Where BranchId in (Select ParentBranchId From " +
                    "mt.Branch Where BranchId = '" + peaCode + "')";
            DbCommand cmd = db.GetSqlStringCommand(strSql);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count == 0) return String.Empty ;

            DataRow r = dt.Rows[0];
            string branchName = DaHelper.GetString(r, "BranchName");
            return branchName;
        }

        public BranchIdInfoItem GetBranchByBillBookId(string billBookId)
        {
            BranchIdInfoItem retVal = new BranchIdInfoItem();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_BranchByBillBookId");
            db.AddInParameter(cmd, "pBillBookId", DbType.String, billBookId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count == 1)
            {
                DataRow r = dt.Rows[0];
                retVal.BranchId = DaHelper.GetString(r, "BranchId");
                retVal.BranchName = DaHelper.GetString(r, "BranchName");
                retVal.BranchLevel = DaHelper.GetString(r, "BranchLevel");
            }
            return retVal;
        }

        public string GetBranchIdByAgentId(string agentId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            string strSql = "Select distinct BranchId From ts.RTAgencyContractMru where CaId = '" + agentId.PadLeft(12, '0') + "'";
            DbCommand cmd = db.GetSqlStringCommand(strSql);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count == 0) return String.Empty ;

            DataRow r = dt.Rows[0];
            string branchId = DaHelper.GetString(r, "BranchId");
            return branchId;
        }

        public List<CAB03_DetailReportInfo> GetCAB03(string billBookId, string absId, string pmId)
        {
            List<CAB03_DetailReportInfo> retVals = new List<CAB03_DetailReportInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_CAB03");
            db.AddInParameter(cmd, "pBillBookId", DbType.String, billBookId);
            db.AddInParameter(cmd, "pAbsId", DbType.String, absId);
            db.AddInParameter(cmd, "pPmId", DbType.String, pmId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow r in dt.Rows)
            {
                CAB03_DetailReportInfo item = new CAB03_DetailReportInfo();
                item.BranchId = DaHelper.GetString(r, "BranchId");
                item.CaId = DaHelper.GetString(r, "CaId");
                item.MruId = DaHelper.GetString(r, "MruId");
                item.CaName = DaHelper.GetString(r, "CaName");
                item.Period = DaHelper.GetBillPeriod(DaHelper.GetString(r, "Period"));
                DateTime? checkindate = DaHelper.GetDate(r, "CheckInDate");
                item.StrReceiveDate = checkindate == null ? "" : checkindate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                item.ReceiveDate = DaHelper.GetDate(r, "CheckInDate");
                item.TotalAmount = DaHelper.GetDecimal(r, "TotalAmount");
                item.PmId = DaHelper.GetString(r, "PmId");
                retVals.Add(item);
            }
            return retVals;
        }

        public List<CAB04_03DetailReportInfo> GetTotalBillItemAndAmountGroupByBillBookId(string agentId, string bookPeriod, string createDate)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_AmountItemBillAndMoneyInEachBillBook");
            db.AddInParameter(cmd, "AgentId", DbType.String, agentId);
            db.AddInParameter(cmd, "BillBookPeriod", DbType.String, bookPeriod);
            db.AddInParameter(cmd, "CreateDate", DbType.String, createDate);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            List<CAB04_03DetailReportInfo> billPasteList = new List<CAB04_03DetailReportInfo>();
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                DataRow r = dt.Rows[i];
                CAB04_03DetailReportInfo itemList = new CAB04_03DetailReportInfo();

                itemList.BillBookRef = DaHelper.GetString(r, "BillBookId") == null ? String.Empty : DaHelper.GetString(r, "BillBookId").Substring(ModuleConfigurationNames.BranchCodeLength, ModuleConfigurationNames.BillBookLengthOnly);
                itemList.BillOutForResident = DaHelper.GetInt(r, "BillOutItemForResident") == null ? 0 : DaHelper.GetInt(r, "BillOutItemForResident");
                itemList.AmountBillOutForResident = DaHelper.GetDecimal(r, "AmountBillOutForResident") == null ? (decimal)0.00 : DaHelper.GetDecimal(r, "AmountBillOutForResident");
                itemList.BillOutForSmallBiz = DaHelper.GetInt(r, "BillOutItemForSmallBiz") == null ? 0 : DaHelper.GetInt(r, "BillOutItemForSmallBiz");
                itemList.AmountBillOutForSmallBiz = DaHelper.GetDecimal(r, "AmountBillOutForSmallBiz") == null ? (decimal)0.00 : DaHelper.GetDecimal(r, "AmountBillOutForSmallBiz");
                itemList.BillOutForGoverment = DaHelper.GetInt(r, "BillOutItemForGoverment") == null ? 0 : DaHelper.GetInt(r, "BillOutItemForGoverment");
                itemList.AmountBillOutForGoverment = DaHelper.GetDecimal(r, "AmountBillOutForGoverment") == null ? (decimal)0.00 : DaHelper.GetDecimal(r, "AmountBillOutForGoverment");
                itemList.CanKeepBillForResident = DaHelper.GetInt(r, "KeepBillItemForResident") == null ? 0 : DaHelper.GetInt(r, "KeepBillItemForResident");
                itemList.AmountCanKeepBillForResident = DaHelper.GetDecimal(r, "AmountKeepBillForResident") == null ? (decimal)0.00 : DaHelper.GetDecimal(r, "AmountKeepBillForResident");
                itemList.CanKeepBillForSmallBiz = DaHelper.GetInt(r, "KeepBillItemForSmallBiz") == null ? 0 : DaHelper.GetInt(r, "KeepBillItemForSmallBiz");
                itemList.AmountCanKeepBillForSmallBiz = DaHelper.GetDecimal(r, "AmountKeepBillForSmallBiz") == null ? (decimal)0.00 : DaHelper.GetDecimal(r, "AmountKeepBillForSmallBiz");
                itemList.CanKeepBillForGoverment = DaHelper.GetInt(r, "KeepBillItemForGoverment") == null ? 0 : DaHelper.GetInt(r, "KeepBillItemForGoverment");
                itemList.AmountCanKeepBillForGoverment = DaHelper.GetDecimal(r, "AmountKeepBillForGoverment") == null ? (decimal)0.00 : DaHelper.GetDecimal(r, "AmountKeepBillForGoverment");
                itemList.ReceiveCount = DaHelper.GetByte(r, "ReceiveCount") == null ? 0 : DaHelper.GetByte(r, "ReceiveCount");
                billPasteList.Add(itemList);
            }
            return billPasteList;
        }

        public List<BillInfoInEachBillBookIdInfo> GetBillInfoInEachBillBookId(string billBookId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_IssueTotalBillOfEachTypeInBillBook");
            db.AddInParameter(cmd, "BillBookId", DbType.String, billBookId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            List<BillInfoInEachBillBookIdInfo> billInfoList = new List<BillInfoInEachBillBookIdInfo>();
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                DataRow r = dt.Rows[i];
                BillInfoInEachBillBookIdInfo billInfoItem = new BillInfoInEachBillBookIdInfo();
                billInfoItem.BillBookId = billBookId.Substring(6, billBookId.Length - 6);
                billInfoItem.BranchId = DaHelper.GetString(r, "BranchId");
                billInfoItem.LineNo = DaHelper.GetString(r, "MruId");
                billInfoItem.CaCount = DaHelper.GetInt(r, "CaCount");
                billInfoItem.Amount = DaHelper.GetDecimal(r, "TotalAmount");
                billInfoItem.BaseAmount = DaHelper.GetDecimal(r, "BaseAmount");
                billInfoItem.FT = DaHelper.GetDecimal(r, "Ft");
                billInfoItem.Vat = DaHelper.GetDecimal(r, "Vat");
                billInfoItem.AbsId = DaHelper.GetString(r, "AbsId");
                billInfoItem.PmId = DaHelper.GetString(r, "PmId");
                billInfoList.Add(billInfoItem);
            }
            return billInfoList;
        }

        public EvaluateTotalBillInfo GetEvaluateBillInfoMonthly(string branchId, int periodMonth, int periodYear)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_TotalCanKeepBillInEachBranchForMonthly");
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "PeriodMonth", DbType.Int32, periodMonth);
            db.AddInParameter(cmd, "PeriodYear", DbType.Int32, periodYear);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];


            EvaluateTotalBillInfo myResult = new EvaluateTotalBillInfo();
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                DataRow r = dt.Rows[i];
                myResult.BranchId = DaHelper.GetString(r, "BranchId");
                myResult.TotalBillKeepPersonResident = DaHelper.GetInt(r, "TotalBillCanKeepItemPersonRes");
                myResult.TotalBillKeepCompanyResident = DaHelper.GetInt(r, "TotalBillCanKeepItemCompanyRes");
                myResult.TotalBillKeepPersonSmallBiz = DaHelper.GetInt(r, "TotalBillCanKeepItemPersonSmall");
                myResult.TotalBillKeepCompanySmallBiz = DaHelper.GetInt(r, "TotalBillCanKeepItemCompanySmall");
                myResult.TotalBillKeepPersonGoverment = DaHelper.GetInt(r, "TotalBillCanKeepItemPersonCov");
                myResult.TotalBillKeepCompanyGoverment = DaHelper.GetInt(r, "TotalBillCanKeepItemCompanyCov");
                myResult.TotalBillPaste = DaHelper.GetInt(r, "TotalBillPaste");
                myResult.TotalBillPaste3Month = DaHelper.GetInt(r, "TotalBillPaste3Month");
            }
            return myResult;
        }

        public EvaluateTotalBillInfo GetEvaluateBillInfoPeroid(string branchId, int sMonth, int eMonth, int yearPriod)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_TotalCanKeepBillInEachBranchForPeriod");
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "PeriodFrom", DbType.Int32, sMonth);
            db.AddInParameter(cmd, "PeriodTo", DbType.Int32, eMonth);
            db.AddInParameter(cmd, "PeriodYear", DbType.Int32, yearPriod);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            EvaluateTotalBillInfo myResult = new EvaluateTotalBillInfo();
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                DataRow r = dt.Rows[i];
                myResult.BranchId = DaHelper.GetString(r, "BranchId");
                myResult.TotalBillKeepPersonResident = DaHelper.GetInt(r, "TotalBillCanKeepItemPersonRes");
                myResult.TotalBillKeepCompanyResident = DaHelper.GetInt(r, "TotalBillCanKeepItemCompanyRes");
                myResult.TotalBillKeepPersonSmallBiz = DaHelper.GetInt(r, "TotalBillCanKeepItemPersonSmall");
                myResult.TotalBillKeepCompanySmallBiz = DaHelper.GetInt(r, "TotalBillCanKeepItemCompanySmall");
                myResult.TotalBillKeepPersonGoverment = DaHelper.GetInt(r, "TotalBillCanKeepItemPersonCov");
                myResult.TotalBillKeepCompanyGoverment = DaHelper.GetInt(r, "TotalBillCanKeepItemCompanyCov");
                myResult.TotalBillPaste = DaHelper.GetInt(r, "TotalBillPaste");
                myResult.TotalBillPaste3Month = DaHelper.GetInt(r, "TotalBillPaste3Month");
            }
            return myResult;
        }

        public List<EvaluateAgencyCounter> GetIssueBillAndAmountOfAgentInEachBranchForPeriod(string branchId, int sMonth, int eMonth, int yearPriod)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_PercentOfItemAndAmountInEachAgencyForPeriod");
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "PeriodFrom", DbType.Int32, sMonth);
            db.AddInParameter(cmd, "PeriodTo", DbType.Int32, eMonth);
            db.AddInParameter(cmd, "PeriodYear", DbType.Int32, yearPriod);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            List<EvaluateAgencyCounter> myAllResume = new List<EvaluateAgencyCounter>();
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                DataRow r = dt.Rows[i];
                EvaluateAgencyCounter myResult = new EvaluateAgencyCounter();
                myResult.BranchId = DaHelper.GetString(r, "BranchId");
                myResult.AgentId = DaHelper.GetString(r, "BookHolderId");
                decimal totalBillOut = Convert.ToDecimal(DaHelper.GetInt(r, "TotalBillOut"));
                decimal totalCanBill = Convert.ToDecimal(DaHelper.GetInt(r, "TotalBillCanKeepItem"));
                myResult.PercentOfItem = Convert.ToDecimal(string.Format("{0:#,###.00}", (totalCanBill / totalBillOut) * 100));
                decimal? totalAmountOut = DaHelper.GetDecimal(r, "TotalAmountBillOut");
                decimal? totalAmountCanBill = DaHelper.GetDecimal(r, "TotalAmountBillCanKeep");
                myResult.PercentOfAmount = Convert.ToDecimal(string.Format("{0:#,###.00}", (totalAmountOut / totalAmountOut) * 100));
                myAllResume.Add(myResult);
            }
            return myAllResume;
        }

        public List<EvaluateAgencyCounter> GetIssueBillAndAmountOfAgentInEachBranchForMonthly(string branchId, int periodMonth, int yearPriod)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_PercentOfItemAndAmountInEachAgencyForMonthly");
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "PeriodMonth", DbType.Int32, periodMonth);
            db.AddInParameter(cmd, "PeriodYear", DbType.Int32, yearPriod);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            List<EvaluateAgencyCounter> myAllResume = new List<EvaluateAgencyCounter>();
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                DataRow r = dt.Rows[i];
                EvaluateAgencyCounter myResult = new EvaluateAgencyCounter();
                myResult.BranchId = DaHelper.GetString(r, "BranchId");
                myResult.AgentId = DaHelper.GetString(r, "BookHolderId");
                decimal totalBillOut = Convert.ToDecimal(DaHelper.GetInt(r, "TotalBillOut"));
                decimal totalCanBill = Convert.ToDecimal(DaHelper.GetInt(r, "TotalBillCanKeepItem"));
                myResult.PercentOfItem = Convert.ToDecimal(string.Format("{0:#,###.00}", (totalCanBill / totalBillOut) * 100));
                decimal? totalAmountOut = DaHelper.GetDecimal(r, "TotalAmountBillOut");
                decimal? totalAmountCanBill = DaHelper.GetDecimal(r, "TotalAmountBillCanKeep");
                myResult.PercentOfAmount = Convert.ToDecimal(string.Format("{0:#,###.00}", (totalAmountOut / totalAmountOut) * 100));
                myAllResume.Add(myResult);
            }
            return myAllResume;
        }

        public List<BranchIdInfoItem> GetBranchUnderByBranchId(string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_RptBranchesByParent");
            db.AddInParameter(cmd, "ParentBranchId", DbType.String, branchId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            List<BranchIdInfoItem> myAllResume = new List<BranchIdInfoItem>();
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                DataRow r = dt.Rows[i];
                BranchIdInfoItem myResult = new BranchIdInfoItem();
                myResult.BranchId = DaHelper.GetString(r, "BranchId");
                myResult.BranchName = DaHelper.GetString(r, "BranchName");
                myResult.BranchLevel = DaHelper.GetString(r, "BranchLevel");
                myAllResume.Add(myResult);
            }
            return myAllResume;
        }

        public List<CAB08_01AgencyInfo> GetCAB08_01AgencyList(string branchId, string billPeriod)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_CAB08_01AgencyList");
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "BillPeriod", DbType.String, billPeriod);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            List<CAB08_01AgencyInfo> myAllResume = new List<CAB08_01AgencyInfo>();
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                DataRow r = dt.Rows[i];
                CAB08_01AgencyInfo myResult = new CAB08_01AgencyInfo();
                myResult.AgentId = DaHelper.GetString(r, "AgencyId") == String.Empty ? String.Empty : DaHelper.GetString(r, "AgencyId").Substring(4);
                myResult.AgentName = DaHelper.GetString(r, "AgencyName");
                myAllResume.Add(myResult);
            }
            return myAllResume;
        }

        public decimal? GetAmountOfBillArrOfBranch(string startBranchId, string endBranchId, string periodBill)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            string strSql = "Select Convert(decimal(10,2),Count(invoiceNo)) as 'Amount' From ts.BillStatusInfo Where BranchId between '" +
                            startBranchId + "' and '" + endBranchId + "' and Period = '" + periodBill + "'";


            DbCommand cmd = db.GetSqlStringCommand(strSql);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count == 0) return 0;

            DataRow r = dt.Rows[0];
            decimal? amountMoney = DaHelper.GetDecimal(r, "Amount");
            return amountMoney;
        }

        public decimal? GetAmountMoneyOfBillArrOfBranch(string startBranchId, string endBranchId, string periodBill)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            string strSql = "Select Convert(decimal(10,2),sum(TotalAmount)) as 'Amount' From ts.BillStatusInfo Where BranchId between '" +
                            startBranchId + "' and '" + endBranchId + "' and Period = '" + periodBill + "'";
            DbCommand cmd = db.GetSqlStringCommand(strSql);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count == 0) return 0;

            DataRow r = dt.Rows[0];
            decimal? amountMoney = DaHelper.GetDecimal(r, "Amount");
            return amountMoney;
        }


        public List<TravelHelpRate> GetTravelHelpRate(TravelHelpRateConditionInfo spcCommission)
        {
            List<TravelHelpRate> spcs = new List<TravelHelpRate>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_AgencyTravelHelpRate");
            db.AddInParameter(cmd, "AgentId", DbType.String, spcCommission.AgencyId.PadLeft(12, '0'));
            db.AddInParameter(cmd, "BookPeriod", DbType.String, spcCommission.BookPeriod);
            db.AddInParameter(cmd, "CreateDt", DbType.DateTime, spcCommission.CreateDate);
            db.AddInParameter(cmd, "CalculateDt", DbType.DateTime, spcCommission.CalculateDate);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
           
            foreach (DataRow r in dt.Rows)
            {
                TravelHelpRate spc = new TravelHelpRate();
                spc.AgencyId = DaHelper.GetString(r, "BookHolderId");
                spc.Period = DaHelper.GetString(r, "BookPeriod");
                spc.BillBookId = DaHelper.GetString(r, "BillBookId");
                spc.MURId = DaHelper.GetString(r, "MruId");
                spc.TravelHelpType = DaHelper.GetString(r, "TravelHelp") == null ? (int)TravelHelpEnum.NORMALTRAVELHELP : Convert.ToInt32(DaHelper.GetString(r, "TravelHelp"));
                spc.TransportHelp = DaHelper.GetDecimal(r, "TransportHelp");
                spc.CollectedPercent = DaHelper.GetDecimal(r, "CollectedPercent");
                spc.CaCount = DaHelper.GetInt(r, "caCount");
                spc.UpperRate = DaHelper.GetDecimal(r, "UpperRate");
                spc.LowerRate = DaHelper.GetDecimal(r, "LowerRate");
                //spc.HelpValidFrom = DaHelper.GetDate(r, "HelpValidFrom");
                //spc.HepValidTo = DaHelper.GetDate(r, "HelpValidTo");
                spcs.Add(spc);                   
            }
            return spcs;                      
        }

        //public string GetAccountCodeInConfig()
        //{
        //    Database db = DatabaseFactory.CreateDatabase("POSDatabase");
        //    string strSql = "Select DebitAcc From ts.AgencyModuleConfig ";
        //    DbCommand cmd = db.GetSqlStringCommand(strSql);
        //    DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
        //    if (dt.Rows.Count == 0) return String.Empty;

        //    DataRow r = dt.Rows[0];
        //    string debitAcc = DaHelper.GetString(r, "DebitAcc");
        //    return debitAcc;
        //}

        public string GetTaxIdInAgencyByAgentId(string agentId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            string strSql = "Select b.TaxCode From mt.ContractAccount a inner join mt.BusinessPartner b on a.BpId=b.BpId Where a.CaId = '" + agentId.PadLeft(12, '0') + "'"; ;
            DbCommand cmd = db.GetSqlStringCommand(strSql);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count == 0) return String.Empty;

            DataRow r = dt.Rows[0];
            string taxId = DaHelper.GetString(r, "TaxCode");
            return taxId == null ? String.Empty : taxId;
        }

        public DateTime? GetBillBookCheckInDate(string billBookId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            string sql = "SELECT CheckIndate FROM ts.billbook WHERE active = 1 and BillBookId = '{0}'";
            sql = String.Format(sql, billBookId);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count == 0) return new DateTime();

            DataRow r = dt.Rows[0];
            DateTime? checkinDate = DaHelper.GetDate(r, "CheckIndate");
            return checkinDate;
        }

        public decimal? GetAgencyVatRate(string agentId)
        {
            decimal? retVal = 0;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_AgencyVatRate");
            db.AddInParameter(cmd, "pAgencyId", DbType.String, agentId.PadLeft(12, '0'));
            DataSet ds = db.ExecuteDataSet(cmd);
            if (ds.Tables.Count == 1)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    retVal = DaHelper.GetDecimal(dt.Rows[0], "VatRate");
                }
            }
            return retVal;
        }


        public decimal? GetAgencyTaxRate(string agentId)
        {
            decimal? retVal = 0;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_AgencyTaxRate");
            db.AddInParameter(cmd, "pAgencyId", DbType.String, agentId.PadLeft(12, '0'));
            DataSet ds = db.ExecuteDataSet(cmd);
            if (ds.Tables.Count == 1)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    retVal = DaHelper.GetDecimal(dt.Rows[0], "TaxRate");
                }
            }
            return retVal;
        }

        public int GetMaxCalculatedCommission(string agencyId, string period)
        {
            int retVal = 0;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_MaxCalculatedCommission");
            db.AddInParameter(cmd, "pAgencyId", DbType.String, agencyId);
            db.AddInParameter(cmd, "pPeriod", DbType.String, period);
            retVal = (int)db.ExecuteScalar(cmd);
            return retVal;
        }

        public bool IsFarLandHelpCommissionCalculated(string agencyId, string period)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_FarLandHelpCommission");
            db.AddInParameter(cmd, "pAgencyId", DbType.String, agencyId);
            db.AddInParameter(cmd, "pPeriod", DbType.String, period);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt == null)
                return false;
            else
                return dt.Rows.Count > 0;
        }

        public TravelHelpRate GetTravelHelpHistory(TravelHelpRateConditionInfo spc)
        {
            TravelHelpRate travelRate = new TravelHelpRate();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_TravelHelpHistory");
            db.AddInParameter(cmd, "pAgencyId", DbType.String, spc.AgencyId);
            db.AddInParameter(cmd, "pPeriod", DbType.String, spc.BookPeriod);
            db.AddInParameter(cmd, "pCreateDate", DbType.DateTime, spc.CreateDate);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                travelRate.AgencyId = spc.AgencyId;
                travelRate.CreateDt = spc.CreateDate;
                travelRate.ExtraMoneyHelp = DaHelper.GetDecimal(row, "SpecialMoney");
                travelRate.TransportHelp = DaHelper.GetDecimal(row, "TransCost");
                // in this case use farland help to store waterhelp or farlandhelp
                travelRate.FarlandHelp = DaHelper.GetDecimal(row, "FarLandHelp");
            }

            return travelRate;
        }

        public Hashtable GetFarLandCollectedCount(string agencyId, string bookPeriod, string branchId)
        {          
            Hashtable collectInfo = new Hashtable();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            int? totalCount = 0;
            int? collectCount = 0;

            DbCommand cmd = db.GetStoredProcCommand("ag_get_FarLandCollectedCount");
            db.AddInParameter(cmd, "pBookHolderId", DbType.String, agencyId);
            db.AddInParameter(cmd, "pBookPeriod", DbType.String, bookPeriod);           
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);           
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    collectCount += DaHelper.GetInt(row, "CollectCount") == null ? 0 : DaHelper.GetInt(row, "CollectCount");
                    totalCount += DaHelper.GetInt(row, "TotalCount") == null ? 0 : DaHelper.GetInt(row, "TotalCount");
                }
                collectInfo.Add("CollectCount", collectCount);
                collectInfo.Add("TotalCount", totalCount);
            }
            return collectInfo;
        }

        public Hashtable GetWaterHelpCollectedCount(string agencyId, string bookPeriod, string branchId, DateTime createDate)
        {
            Hashtable collectInfo = new Hashtable();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            int? totalCount = 0;
            int? collectCount = 0;

            DbCommand cmd = db.GetStoredProcCommand("ag_get_WaterHelpCollectedCount");
            db.AddInParameter(cmd, "pBookHolderId", DbType.String, agencyId);
            db.AddInParameter(cmd, "pBookPeriod", DbType.String, bookPeriod);
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pCreateDate", DbType.DateTime, createDate);

            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    collectCount += DaHelper.GetInt(row, "CollectCount") == null ? 0 : DaHelper.GetInt(row, "CollectCount");
                    totalCount += DaHelper.GetInt(row, "TotalCount") == null ? 0 : DaHelper.GetInt(row, "TotalCount");
                }
                collectInfo.Add("CollectCount", collectCount);
                collectInfo.Add("TotalCount", totalCount);
            }
            

            return collectInfo;
        }
        #endregion

        #region "Generate Function"

        public string GenerateVoucherId(string strYear)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                string strSql = "Select isnull(max(VoucherID),0) as Seqno From ts.AgencyCommission Where VoucherID like '" + strYear + "%'";
                DbCommand cmd = db.GetSqlStringCommand(strSql);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                DataRow r = dt.Rows[0];

                if (DaHelper.GetString(r, "Seqno") == "0")
                {
                    return strYear + "-00000001";
                }
                else
                {
                    string maxValue = DaHelper.GetString(r, "Seqno");
                    int seqNo = Convert.ToInt32(maxValue.Substring(3, maxValue.Length - 3));
                    seqNo += 1;
                    //seqNo.ToString("00000000");
                    //strSeqNo = string.Format("00000000", strSeqNo);
                    return strYear + "-" + seqNo.ToString("00000000");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region "Update Function"

        public bool UpdateVoucherIdByCmdId(int cmdId, string voucherId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                string strSql = "Update ts.AgencyCommission set VoucherID = '" + voucherId + "',ModifiedBy='Boy' Where";
                strSql = strSql + " cmId  = " + cmdId;
                int resultRow = db.ExecuteNonQuery(CommandType.Text, strSql);
                if (resultRow != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region "Delete Function"
        public bool DeleteCommissionByBillBook(DbTransaction trans, string billbookId, string modifiedBy, DateTime modifiedDt)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ag_upd_CancelCommissionByBillBook");

                //update advance payment
                db.AddInParameter(cmd, "pBillBookId", DbType.String, billbookId);
                db.AddInParameter(cmd, "pModifiedBy", DbType.String, modifiedBy);
                db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, modifiedDt);
                return db.ExecuteNonQuery(cmd, trans) > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion

    }
}
