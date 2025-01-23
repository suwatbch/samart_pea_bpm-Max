using System.Collections.Generic;
using PEA.BPM.Architecture.ArchitectureBS;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.BillPrintingModule.BS;
using PEA.BPM.BillPrintingModule.Interface.BusinessEntities;
using WCFExtras.Soap;


namespace BPMService.BLAN
{

    public class BillPrintingReportWCFService : IBillPrintingReportWCFService
    {
        private ReportBS _bs;
        private AuthInfo authInfo = SoapHeaderHelper<AuthInfo>.GetInputHeader("AuthInfoValue");

        public BillPrintingReportWCFService()
        {
            _bs = new ReportBS();
        }

        public CompressData GetDailyPrintReport(ReportConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingReportWCFService", "GetDailyPrintReport",
                () =>
                {
                    return ServiceHelper.CompressData<List<ReportDailyPrint>>(_bs.GetDailyPrintReport(param));
                });
        }

        public CompressData GetDailyUnprintReport(ReportConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingReportWCFService", "GetDailyUnprintReport",
                () =>
                {
                    return ServiceHelper.CompressData<List<ReportDailyUnprint>>(_bs.GetDailyUnprintReport(param));
                });
        }

        public CompressData GetBillDeliveryReport(ReportConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingReportWCFService", "GetBillDeliveryReport",
                () =>
                {
                    return ServiceHelper.CompressData<List<ReportBillDelivery>>(_bs.GetBillDeliveryReport(param));
                });
        }

        public CompressData GetStreetRouteReport(ReportConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingReportWCFService", "GetStreetRouteReport",
                () =>
                {
                    return ServiceHelper.CompressData<List<ReportStreetRoute>>(_bs.GetStreetRouteReport(param));
                });
        }

        public CompressData GetStreetRouteReceiveReport(ReportConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingReportWCFService", "GetStreetRouteReceiveReport",
                () =>
                {
                    return ServiceHelper.CompressData<List<ReportStreetRouteReceive>>(_bs.GetStreetRouteReceiveReport(param));
                });
        }

        public CompressData GetStreetRouteUnreceiveReport(ReportConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingReportWCFService", "GetStreetRouteUnreceiveReport",
                () =>
                {
                    return ServiceHelper.CompressData<List<ReportStreetRouteUnreceive>>(_bs.GetStreetRouteUnreceiveReport(param));
                });
        }

        public CompressData GetF16Report(ReportConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingReportWCFService", "GetF16Report",
                () =>
                {
                    return ServiceHelper.CompressData<List<ReportF16>>(_bs.GetF16Report(param));
                });
        }

        public List<PrintableId> GetBranchForBillDeliveryReport(ReportConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingReportWCFService", "GetBranchForBillDeliveryReport",
                () =>
                {
                    return _bs.GetBranchForBillDeliveryReport(param);
                });
        }

        public CompressData GetGrpInvSummaryReport(ReportConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingReportWCFService", "GetGrpInvSummaryReport",
                () =>
                {
                    return ServiceHelper.CompressData<List<ReportGrpInvSummary>>(_bs.GetGrpInvSummaryReport(param));
                });
        }

        public CompressData GetBillingStatusReport(ReportConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingReportWCFService", "GetBillingStatusReport",
                () =>
                {
                    return ServiceHelper.CompressData<List<ReportBillingStatus>>(_bs.GetBillingStatusReport(param));
                });
        }

        public CompressData GetPrintGreenByBankReport(ReportConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingReportWCFService", "GetPrintGreenByBankReport",
                () =>
                {
                    return ServiceHelper.CompressData<List<ReportPrintByBank>>(_bs.GetPrintGreenByBankReport(param));
                });
        }

        public CompressData GetCAUnprintReport(ReportConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingReportWCFService", "GetCAUnprintReport",
                () =>
                {
                    return ServiceHelper.CompressData<List<ReportCAUnprint>>(_bs.GetCAUnprintReport(param));
                });
        }

        public CompressData GetDirectDebitStatusReport(ReportConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingReportWCFService", "GetDirectDebitStatusReport",
                () =>
                {
                    return ServiceHelper.CompressData<List<ReportDirectDebitStatus>>(_bs.GetDirectDebitStatusReport(param));
                });
        }

        public List<ReportGroupingCrossCheck> GetGroupingCrossCheckReport(ReportConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingReportWCFService", "GetGroupingCrossCheckReport",
                () =>
                {
                    return _bs.GetGroupingCrossCheckReport(param);
                });
        }

    }
}
