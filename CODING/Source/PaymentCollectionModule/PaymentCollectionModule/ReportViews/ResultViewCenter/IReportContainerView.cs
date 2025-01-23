using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports;
using System.ComponentModel;

namespace PEA.BPM.PaymentCollectionModule.ReportViews.ResultViewCenter
{
    public interface IReportContainerView
    {
        //void Preview(ReportWorkFlowSummary reportData, string reportCode);
        //void Preview(ReportBankPayInDetailInfo reportData);
        //void Preview(List<ReportDailyRemainInfo> reportData);
        //void Preview(List<ReportDailyPayInInfo> reportData);
        //void Preview(List<ReportCloseWorkSummary> reportData);
        //void Preview(BindingList<ChequeInfo> reportData, string closeWorkDate);
        void Preview(List<CAC19Report> reportData, List<CAC19SummaryReport> reportDataSummary , DateTime fromDate, DateTime toDate);
        string SetLabel { set; get; }
    }
}
