using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using PEA.BPM.Architecture.CommonUtilities;
using System.ComponentModel;

namespace PEA.BPM.CashManagementModule.DA
{
    public class CashReportDA
    {
        public const int CMD_TIMEOUT = 10000;       

        //point to report server
        public List<ReportAvailableInfo> GetWorkBetweenDate(DbTransaction trans, ReportParam param, string output)
        {
            List<ReportAvailableInfo> avList = new List<ReportAvailableInfo>();
            Database db = DatabaseFactory.CreateDatabase("CMReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_sel_workBetweenDate");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, param.FromDate);
            db.AddInParameter(cmd, "ToDate", DbType.DateTime, param.ToDate);
            db.AddInParameter(cmd, "CashierId", DbType.String, param.CashierId);
            db.AddInParameter(cmd, "PosId", DbType.String, param.PosId);
            db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                ReportAvailableInfo av = new ReportAvailableInfo();
                av.ItemId = DaHelper.GetString(dr, "WorkId");
                av.CashierId = DaHelper.GetString(dr, "CashierId");
                av.CashierName = DaHelper.GetString(dr, "CashierName");
                av.PosId = DaHelper.GetString(dr, "PosId");
                av.ItemDt = DaHelper.GetDate(dr, "OpenWorkDt");
                av.CashAmt = DaHelper.GetDecimal(dr, "CashAmt");
                av.ChequeAmt = DaHelper.GetDecimal(dr, "ChequeAmt");
                av.DayCount = DaHelper.GetInt(dr, "DayCount");
                av.PosId = DaHelper.GetString(dr, "PosId");
                av.CloseWorkBy = DaHelper.GetString(dr, "CloseWorkBy");
                avList.Add(av);
            }

            return avList;

        }

        //point to report server
        public List<ReportAvailableInfo> GetPayInOfDate(DbTransaction trans, DateTime procDt, string branchId)
        {
            List<ReportAvailableInfo> retList = new List<ReportAvailableInfo>();
            Database db = DatabaseFactory.CreateDatabase("CMReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_sel_bankOfDate");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "ProcDt", DbType.DateTime, procDt);
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                ReportAvailableInfo dp = new ReportAvailableInfo();
                dp.BankKey = DaHelper.GetString(dr, "BankKey");
                dp.BankName = DaHelper.GetString(dr, "BankName");
                dp.BankAccNo = DaHelper.GetString(dr, "BankAccNo");
                dp.ChequeAmt = DaHelper.GetDecimal(dr, "ChequeAmount");
                dp.CashAmt = DaHelper.GetDecimal(dr, "CashAmount");
                dp.ItemDt = DaHelper.GetDate(dr, "PaymentDt");
                retList.Add(dp);
            }

            return retList;
        }

        //point to report server
        public List<ReportAvailableInfo> GetCloseWorkOfDate(DbTransaction trans, DateTime procDt, string branchId)
        {
            //list cashier 
            List<ReportAvailableInfo> retList = new List<ReportAvailableInfo>();
            Database db = DatabaseFactory.CreateDatabase("CMReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_sel_cashierOfDate");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "ProcDt", DbType.DateTime, procDt);
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                ReportAvailableInfo dp = new ReportAvailableInfo();
                dp.CashierId = DaHelper.GetString(dr, "CashierId");
                dp.CashierName = DaHelper.GetString(dr, "FullName");
                retList.Add(dp);
            }

            return retList;
        }

        //point to report server
        public List<ReportDailyPayInInfo> GetBankPayInDailyForReport(DbTransaction trans, string bankKey, string bankAccNo, DateTime payIdDt, string branchId)
        {
            List<ReportDailyPayInInfo> retList = new List<ReportDailyPayInInfo>();
            Database db = DatabaseFactory.CreateDatabase("CMReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_sel_ReportPayDaily");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "BankKey", DbType.String, bankKey);
            db.AddInParameter(cmd, "BankAccNo", DbType.String, bankAccNo);
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "PayInDt", DbType.DateTime, payIdDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                ReportDailyPayInInfo dp = new ReportDailyPayInInfo();
                dp.GroupCount = DaHelper.GetInt(dr, "GroupCount");
                dp.BankKey = DaHelper.GetString(dr, "GLBankKey");
                dp.BankName = DaHelper.GetString(dr, "GLBankName");
                dp.AccNo = DaHelper.GetString(dr, "GLAccountNo");
                dp.ClearingAccNo = DaHelper.GetString(dr, "ClearingAccNo");
                dp.ClearingAccNo = dp.ClearingAccNo != null ? dp.ClearingAccNo.TrimStart('0') : null;
                //dp.Cashier = string.Format("({0}) {1}", DaHelper.GetString(dr, "CashierId").TrimStart('0'), DaHelper.GetString(dr, "CashierName"));
                dp.Cashier = DaHelper.GetString(dr, "CashierName");
                dp.ItemName = DaHelper.GetString(dr, "ItemName");
                dp.Amount = DaHelper.GetDecimal(dr, "TotalAmt");
                dp.PayInAmount = DaHelper.GetDecimal(dr, "PayInAmt");
                dp.ProcDt = DaHelper.GetDate(dr, "PaymentDt");
                dp.PayInDt = payIdDt;
                dp.BranchId = branchId;
                retList.Add(dp);
            }

            return retList;
        }

        //point to report server
        public List<ReportCloseWorkSummary> GetCloseWorkSummaryReport(DbTransaction trans, DateTime closeWorkDt, string cashierId, string branchId, string output)
        {
            try
            {
                List<ReportCloseWorkSummary> cwList = new List<ReportCloseWorkSummary>();
                Database db = DatabaseFactory.CreateDatabase("CMReportDatabase");
                DbCommand cmd = db.GetStoredProcCommand("cm_sel_ReportCloseWorkSummary");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "CloseWorkDt", DbType.DateTime, closeWorkDt);
                db.AddInParameter(cmd, "CashierId", DbType.String, cashierId != "%" ? cashierId.PadLeft(8, '0') : "%");
                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
                db.AddInParameter(cmd, "Output", DbType.String, output);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    ReportCloseWorkSummary dp = new ReportCloseWorkSummary();
                    dp.CashierId = DaHelper.GetString(dr, "CashierId");
                    dp.CashierName = DaHelper.GetString(dr, "CashierName");
                    dp.FlowType = DaHelper.GetString(dr, "FlowType");
                    dp.Description = DaHelper.GetString(dr, "FlowName");
                    dp.CashIn = DaHelper.GetDecimal(dr, "CashIn");
                    dp.ChequeIn = DaHelper.GetDecimal(dr, "ChequeIn");
                    dp.CashOut = DaHelper.GetDecimal(dr, "CashOut");
                    dp.ChequeOut = DaHelper.GetDecimal(dr, "ChequeOut");
                    dp.CloseWorkDt = closeWorkDt;
                    cwList.Add(dp);
                }

                return cwList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public WorkInfo GetWorkInfo(DbTransaction trans, string workId)
        {
            WorkInfo workInfo = new WorkInfo();
            Database db = DatabaseFactory.CreateDatabase("CMReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_sel_workInfo");
            db.AddInParameter(cmd, "WorkId", DbType.String, workId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            //force one row only
            foreach (DataRow dr in dt.Rows)
            {
                workInfo.CashierId = DaHelper.GetString(dr, "CashierId");
                workInfo.CashierName = DaHelper.GetString(dr, "CashierName");
                workInfo.PosId = DaHelper.GetString(dr, "PosId");
                workInfo.OpenWorkDt = DaHelper.GetDate(dr, "OpenWorkDt");
                workInfo.CloseWorkDt = DaHelper.GetDate(dr, "CloseWorkDt");
            }

            return workInfo;
        }

        private decimal? Positive(decimal? money)
        {
            decimal? ret = 0;
            if (money < 0)
                ret = money * (-1);
            else
                ret = money;

            return ret;
        } 

        private void FilterFlowType(ref FlowSummaryInfo flow, DataRow dr)
        {
            // +++++++++++++++++++++++++++++++
            if (flow.FlowType == FlowType.SystemInitialCash ||
                flow.FlowType == FlowType.MoneyFromBank ||
                flow.FlowType == FlowType.MoneyFromAnotherCashier ||
                flow.FlowType == FlowType.MoneyReceivedFromPOS ||
                flow.FlowType == FlowType.MoneyOpeningBalance ||
                flow.FlowType == FlowType.CancelledPOSPaymentable ||
                flow.FlowType == FlowType.CancelledBankDelivery ||
                flow.FlowType == FlowType.Adjust_CashOutFromPOS_Plus ||
                flow.FlowType == FlowType.Adjust_MoneyDepositToBank_Plus ||
                flow.FlowType == FlowType.Adjust_MoneyFromBank_Plus ||
                flow.FlowType == FlowType.Adjust_MoneyReceivedFromPOS_Plus)
            {
                flow.CashIn = Positive(DaHelper.GetDecimal(dr, "CashAmt"));
                flow.ChequeIn = Positive(DaHelper.GetDecimal(dr, "ChequeAmt"));
            }
            // -------------------------------
            else if (flow.FlowType == FlowType.MoneyTransferedToAnotherCashier ||
                    flow.FlowType == FlowType.MoneyDepositToBank ||
                    flow.FlowType == FlowType.CashOutFromPOS ||
                    flow.FlowType == FlowType.MoneyClosingBalance ||
                    flow.FlowType == FlowType.CancelledPOSReceivable ||
                    flow.FlowType == FlowType.CancelledMoneyCheckIn ||
                    flow.FlowType == FlowType.Adjust_CashOutFromPOS_Minus ||
                    flow.FlowType == FlowType.Adjust_MoneyDepositToBank_Minus ||
                    flow.FlowType == FlowType.Adjust_MoneyFromBank_Minus ||
                    flow.FlowType == FlowType.Adjust_MoneyReceivedFromPOS_Minus)
            {
                flow.CashOut = Positive(DaHelper.GetDecimal(dr, "CashAmt"));
                flow.ChequeOut = Positive(DaHelper.GetDecimal(dr, "ChequeAmt"));
            }
            else
            {
                throw new Exception("Flow ในระบบไม่สอดคล้องกับที่มีอยู่");
            }
        }   

        public List<FlowSummaryInfo> GetWorkFlow(DbTransaction trans, string workId)
        {
            List<FlowSummaryInfo> flowList = new List<FlowSummaryInfo>();
            Database db = DatabaseFactory.CreateDatabase("CMReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_sel_closeWorkSummary");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "WorkId", DbType.String, workId);
            DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                FlowSummaryInfo flow = new FlowSummaryInfo();
                flow.FlowType = DaHelper.GetString(dr, "FlowType");
                if (flow.FlowType != null)
                {
                    flow.FlowCat = DaHelper.GetString(dr, "FlowCat");
                    flow.FlowId = DaHelper.GetString(dr, "FlowId");
                    flow.Description = DaHelper.GetString(dr, "FlowDesc");
                    flow.BankKey = DaHelper.GetString(dr, "BankKey");
                    flow.AccountNo = DaHelper.GetString(dr, "RefNo");
                    flow.ModifiedDt = DaHelper.GetDateTime(dr, "FlowDt");
                    FilterFlowType(ref flow, dr);
                    flow.CashIn = flow.CashIn == null ? 0 : flow.CashIn;
                    flow.CashOut = flow.CashOut == null ? 0 : flow.CashOut;
                    flow.ChequeIn = flow.ChequeIn == null ? 0 : flow.ChequeIn;
                    flow.ChequeOut = flow.ChequeOut == null ? 0 : flow.ChequeOut;
                    flow.WorkId = workId;
                    flowList.Add(flow);
                }
            }

            return flowList;
        }

    }
}
