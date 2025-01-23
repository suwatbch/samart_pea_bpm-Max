using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.BillPrintingModule.Interface.BusinessEntities;

namespace PEA.BPM.BillPrintingModule.Interface.Services
{
    public interface IReportServices
    {
        List<ReportDailyPrint> GetDailyPrintReport(ReportConditionParam param);
        List<ReportDailyUnprint> GetDailyUnprintReport(ReportConditionParam param);
        List<ReportBillDelivery> GetBillDeliveryReport(ReportConditionParam param);
        List<ReportStreetRoute> GetStreetRouteReport(ReportConditionParam param);
        List<ReportStreetRouteReceive> GetStreetRouteReceiveReport(ReportConditionParam param);
        List<ReportStreetRouteUnreceive> GetStreetRouteUnreceiveReport(ReportConditionParam param);
        List<ReportF16> GetF16Report(ReportConditionParam param);
        List<PrintableId> GetBranchForBillDeliveryReport(ReportConditionParam param);
        List<ReportGrpInvSummary> GetGrpInvSummaryReport(ReportConditionParam param);
        List<ReportPrintByBank> GetPrintGreenByBankReport(ReportConditionParam param);
        List<ReportGroupingCrossCheck> GetGroupingCrossCheckReport(ReportConditionParam param);
        List<ReportBillingStatus> GetBillingStatusReport(ReportConditionParam param);
        List<ReportCAUnprint> GetCAUnprintReport(ReportConditionParam param);
        List<ReportDirectDebitStatus> GetDirectDebitStatusReport(ReportConditionParam param);        
    }
}
