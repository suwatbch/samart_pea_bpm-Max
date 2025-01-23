using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;

namespace PEA.BPM.CashManagementModule.Interface.Services
{
    public interface ICashReportServices
    {
        //��§ҹ 1.11 
        List<ReportAvailableInfo> GetPayInOfDate(ReportParam param);
        List<ReportDailyPayInInfo> GetBankPayInDailyForReport(ReportParam param);

        //��§ҹ 1.12 
        List<ReportAvailableInfo> GetWorkBetweenDate(ReportParam param, string output);
        ReportWorkFlowSummary GetWorkFlowDelayedReport(string workId);

        //��§ҹ 1.13 (1)
        List<ReportAvailableInfo> GetCloseWorkOfDate(ReportParam param);
        List<ReportCloseWorkSummary> GetCloseWorkSummaryReport(ReportParam param);
        
        //moved to architecture
        //CashierInfo IsForcedToCloseWork(string workId);

    }
}
