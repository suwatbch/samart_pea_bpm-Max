using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.BillPrintingModule.Interface.BusinessEntities;
using PEA.BPM.BillPrintingModule.Interface.Services;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.BillPrintingModule.SG;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.BillPrintingModule.Services
{
    [Service(typeof(IReportServices))]
    public class ReportServices : IReportServices
    {
        public ReportServices()
        {
        }

        #region Service Factory
        public IReportServices GetService()
        {
            return GetService(false);
        }

        public IReportServices GetService(bool serverService)
        {
            if (serverService || Session.Branch.OnlineConnection)
            {
                return new ReportSG(true);
            }
            else
            {
                return new ReportSG(false);
            }
        }
        #endregion
        
        #region "IReportServices Members"

        public List<ReportDailyPrint> GetDailyPrintReport(ReportConditionParam param)
        {
            IReportServices bs = GetService();
            return bs.GetDailyPrintReport(param);
        }

        public List<ReportDailyUnprint> GetDailyUnprintReport(ReportConditionParam param)
        {
            IReportServices bs = GetService();
            return bs.GetDailyUnprintReport(param);
        }

        public List<ReportBillDelivery> GetBillDeliveryReport(ReportConditionParam param)
        {
            IReportServices bs = GetService();
            return bs.GetBillDeliveryReport(param);
        }

        public List<ReportStreetRoute> GetStreetRouteReport(ReportConditionParam param)
        {
            IReportServices bs = GetService();
            return bs.GetStreetRouteReport(param);
        }

        public List<ReportStreetRouteReceive> GetStreetRouteReceiveReport(ReportConditionParam param)
        {
            IReportServices bs = GetService();
            return bs.GetStreetRouteReceiveReport(param);
        }

        public List<ReportStreetRouteUnreceive> GetStreetRouteUnreceiveReport(ReportConditionParam param)
        {
            IReportServices bs = GetService();
            return bs.GetStreetRouteUnreceiveReport(param);
        }

        public List<ReportF16> GetF16Report(ReportConditionParam param)
        {
            IReportServices bs = GetService();
            return bs.GetF16Report(param);
        }
        
        public List<PrintableId> GetBranchForBillDeliveryReport(ReportConditionParam param)
        {
            IReportServices bs = GetService();
            return bs.GetBranchForBillDeliveryReport(param);
        }

        public List<ReportGrpInvSummary> GetGrpInvSummaryReport(ReportConditionParam param)
        {
            IReportServices bs = GetService();
            return bs.GetGrpInvSummaryReport(param);
        }

        public List<ReportPrintByBank> GetPrintGreenByBankReport(ReportConditionParam param)
        {
            IReportServices bs = GetService();
            return bs.GetPrintGreenByBankReport(param);
        }

        public List<ReportBillingStatus> GetBillingStatusReport(ReportConditionParam param)
        {
            IReportServices bs = GetService();
            return bs.GetBillingStatusReport(param);
        }

        public List<ReportGroupingCrossCheck> GetGroupingCrossCheckReport(ReportConditionParam param)
        {
            IReportServices bs = GetService();
            return bs.GetGroupingCrossCheckReport(param);
        }

        public List<ReportCAUnprint> GetCAUnprintReport(ReportConditionParam param)
        {
            IReportServices bs = GetService();
            return bs.GetCAUnprintReport(param);
        }

        public List<ReportDirectDebitStatus> GetDirectDebitStatusReport(ReportConditionParam param)
        {
            IReportServices bs = GetService();
            return bs.GetDirectDebitStatusReport(param);
        }

        #endregion
    }
}
