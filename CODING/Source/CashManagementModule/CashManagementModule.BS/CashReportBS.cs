using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.CashManagementModule.Interface.Services;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;
using System.Data.Common;
using PEA.BPM.CashManagementModule.DA;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.ComponentModel;

namespace PEA.BPM.CashManagementModule.BS
{
    public class CashReportBS : ICashReportServices
    {     

        public List<ReportAvailableInfo> GetWorkBetweenDate(ReportParam param, string output)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try
                {
                    CashReportDA da = new CashReportDA();
                    List<ReportAvailableInfo> list = da.GetWorkBetweenDate(trans, param, output);

                    trans.Commit();
                    return list;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }     

        public List<ReportAvailableInfo> GetPayInOfDate(ReportParam param)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try
                {
                    CashReportDA da = new CashReportDA();
                    List<ReportAvailableInfo> list = da.GetPayInOfDate(trans, param.FromDate, param.BranchId);

                    trans.Commit();
                    return list;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }

        public List<ReportAvailableInfo> GetCloseWorkOfDate(ReportParam param)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try
                {
                    CashReportDA da = new CashReportDA();
                    List<ReportAvailableInfo> list = da.GetCloseWorkOfDate(trans, param.FromDate, param.BranchId);

                    trans.Commit();
                    return list;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }


        public List<ReportDailyPayInInfo> GetBankPayInDailyForReport(ReportParam param)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try
                {
                    List<ReportDailyPayInInfo> dailyPayInList = new List<ReportDailyPayInInfo>();
                    CashReportDA da = new CashReportDA();
                    if (param.ReportCondition == "1") // all
                    {
                        dailyPayInList.AddRange(da.GetBankPayInDailyForReport(trans, null, null, param.FromDate, param.BranchId));
                    }
                    else
                    {
                        int i = 1;
                        foreach (ReportAvailableInfo avInfo in param.AvList)
                        {
                            List<ReportDailyPayInInfo> piList = da.GetBankPayInDailyForReport(trans, avInfo.BankKey, avInfo.BankAccNo, param.FromDate, param.BranchId);
                            foreach (ReportDailyPayInInfo pi in piList)
                                pi.GroupCount = i;

                            dailyPayInList.AddRange(piList);
                            i++;
                        }
                    }

                    trans.Commit();
                    return dailyPayInList;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }

        public ReportWorkFlowSummary GetWorkFlowDelayedReport(string workId)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try
                {
                    ReportWorkFlowSummary ret = new ReportWorkFlowSummary();
                    CashReportDA da = new CashReportDA();
                    ret.FlowList = da.GetWorkFlow(trans, workId);

                    //fill header
                    WorkInfo workInfo = da.GetWorkInfo(trans, workId);
                    ret.CashierId = workInfo.CashierId;
                    ret.CashierName = workInfo.CashierName;
                    ret.PosId = workInfo.PosId;
                    ret.OpenWorkDt = workInfo.OpenWorkDt;
                    ret.CloseWorkDt = workInfo.CloseWorkDt;

                    trans.Commit();
                    return ret;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }

        public List<ReportCloseWorkSummary> GetCloseWorkSummaryReport(ReportParam param)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try
                {
                    List<ReportCloseWorkSummary> cwReportList = new List<ReportCloseWorkSummary>();
                    CashReportDA da = new CashReportDA();

                    if (param.ReportCondition == "1") // all
                    {
                        string cashierId = "%";
                        cwReportList.AddRange(da.GetCloseWorkSummaryReport(trans, param.FromDate, cashierId, param.BranchId, "0"));
                    }
                    else if (param.ReportCondition == "2")
                    {
                        string cashierId = "%";
                        cwReportList.AddRange(da.GetCloseWorkSummaryReport(trans, param.FromDate, cashierId, param.BranchId, "1"));
                    }
                    else
                    {
                        foreach (string cashierId in param.InputList)
                            cwReportList.AddRange(da.GetCloseWorkSummaryReport(trans, param.FromDate, cashierId, param.BranchId, "1"));
                    }

                    trans.Commit();
                    return cwReportList;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }        

    }
}
