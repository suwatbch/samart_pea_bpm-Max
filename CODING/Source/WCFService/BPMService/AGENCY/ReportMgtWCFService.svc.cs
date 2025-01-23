using System.Collections.Generic;
using PEA.BPM.AgencyManagementModule.BS;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.ArchitectureBS;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.WebService;
using WCFExtras.Soap;


namespace BPMService.AGENCY
{

    public class ReportMgtWCFService : IReportMgtWCFService
    {
        private ReportMgtService _reportMgtService;
        private AuthInfo authInfo = SoapHeaderHelper<AuthInfo>.GetInputHeader("AuthInfoValue");

        public ReportMgtWCFService()
        {
            _reportMgtService = new ReportMgtService();
        }

        public List<BillBookDetailReportListInfo> GetBillBookDetailReportList(string billBookId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "GetBillBookDetailReportList",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _reportMgtService.GetBillBookDetailReportList(billBookId);
                });
        }

        public CompressData GetBillBookDetailReportList_Compress(string billBookId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "GetBillBookDetailReportList_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<BillBookDetailReportListInfo>>(_reportMgtService.GetBillBookDetailReportList(billBookId));
                });
        }

        public List<BillBookInfoDetailReport> GetBillBookInfoDetailReport(string billBookId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "GetBillBookInfoDetailReport",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _reportMgtService.GetBillBookInfoDetailReport(billBookId);
                });
        }

        public CompressData GetBillBookInfoDetailReport_Compress(string billBookId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "GetBillBookInfoDetailReport_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<BillBookInfoDetailReport>>(_reportMgtService.GetBillBookInfoDetailReport(billBookId));
                });
        }

        public List<CAB02_DetailReportInfo> GetAgencyMoneyReturnReport(string agencyIdFrom, string agencyIdTo, string periodFrom, string periodTo, string branchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "GetAgencyMoneyReturnReport",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _reportMgtService.GetAgencyMoneyReturnReport(agencyIdFrom, agencyIdTo, periodFrom, periodTo, branchId);
                });
        }

        public CompressData GetAgencyMoneyReturnReport_Compress(string agencyIdFrom, string agencyIdTo, string periodFrom, string periodTo, string branchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "GetAgencyMoneyReturnReport_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CAB02_DetailReportInfo>>(_reportMgtService.GetAgencyMoneyReturnReport(agencyIdFrom, agencyIdTo, periodFrom, periodTo, branchId));
                });
        }

        public List<AgencyBillCollectionMasterReport> GetAgencyBillCollectionReport(string period, string branchId, string branchName)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "GetAgencyBillCollectionReport",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _reportMgtService.GetAgencyBillCollectionReport(period, branchId, branchName);
                });
        }

        public CompressData GetAgencyBillCollectionReport_Compress(string period, string branchId, string branchName)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "GetAgencyBillCollectionReport_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<AgencyBillCollectionMasterReport>>(_reportMgtService.GetAgencyBillCollectionReport(period, branchId, branchName));
                });
        }

        public decimal GetAdvPaidByBillBookId(string billBookId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "GetAdvPaidByBillBookId",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _reportMgtService.GetAdvPaidByBillBookId(billBookId);
                });
        }

        public List<CommissionVoucherInfoReport> FindAndDisplayAgencyCommissionReportInfo(CommissionVoucherConditionPrintReport commissionCoditionInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindAndDisplayAgencyCommissionReportInfo",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _reportMgtService.FindAndDisplayAgencyCommissionReportInfo(commissionCoditionInfo);
                });
        }

        public CompressData FindAndDisplayAgencyCommissionReportInfo_Compress(CompressData commissionCoditionInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindAndDisplayAgencyCommissionReportInfo_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CommissionVoucherInfoReport>>(_reportMgtService.FindAndDisplayAgencyCommissionReportInfo(ServiceHelper.DecompressData<CommissionVoucherConditionPrintReport>(commissionCoditionInfo)));
                });
        }

        public List<CommissionRegistInfoReport> FindAndDisplayAgencyCommissionRegistryReportInfo(CommissionVoucherConditionPrintReport commissionCoditionInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindAndDisplayAgencyCommissionRegistryReportInfo",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _reportMgtService.FindAndDisplayAgencyCommissionRegistryReportInfo(commissionCoditionInfo);
                });
        }

        public CompressData FindAndDisplayAgencyCommissionRegistryReportInfo_Compress(CompressData commissionCoditionInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindAndDisplayAgencyCommissionRegistryReportInfo_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CommissionRegistInfoReport>>(_reportMgtService.FindAndDisplayAgencyCommissionRegistryReportInfo(ServiceHelper.DecompressData<CommissionVoucherConditionPrintReport>(commissionCoditionInfo)));
                });
        }

        public List<CAB04_03DetailReportInfo> FindCommissionRegistryDetailReport(CommissionVoucherConditionPrintReport commissionConditionInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindCommissionRegistryDetailReport",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _reportMgtService.FindAndDisplayCAB04_03Detail(commissionConditionInfo);
                });
        }

        public CompressData FindCommissionRegistryDetailReport_Compress(CompressData commissionConditionInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindCommissionRegistryDetailReport_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CAB04_03DetailReportInfo>>(_reportMgtService.FindAndDisplayCAB04_03Detail(ServiceHelper.DecompressData<CommissionVoucherConditionPrintReport>(commissionConditionInfo)));
                });
        }

        public List<ReturnBillBookBillPasteStatusReportInfo> FindAndDisplayPasteBillStatus(CheckInBillBookConditionInfoReport conditionPrintInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindAndDisplayPasteBillStatus",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _reportMgtService.FindAndDisplayPasteBillStatus(conditionPrintInfo);
                });
        }

        public CompressData FindAndDisplayPasteBillStatus_Compress(CompressData conditionPrintInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindAndDisplayPasteBillStatus_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<ReturnBillBookBillPasteStatusReportInfo>>(_reportMgtService.FindAndDisplayPasteBillStatus(ServiceHelper.DecompressData<CheckInBillBookConditionInfoReport>(conditionPrintInfo)));
                });
        }

        public CAB03_02DetailReportInfo FindDisplayIssueBillARInfoDetail(CheckInBillBookConditionInfoReport conditionPrintInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindDisplayIssueBillARInfoDetail",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _reportMgtService.FindDisplayIssueBillARInfoDetail(conditionPrintInfo);
                });
        }

        public CompressData FindDisplayIssueBillARInfoDetail_Compress(CompressData conditionPrintInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindDisplayIssueBillARInfoDetail_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<CAB03_02DetailReportInfo>(_reportMgtService.FindDisplayIssueBillARInfoDetail(ServiceHelper.DecompressData<CheckInBillBookConditionInfoReport>(conditionPrintInfo)));
                });
        }

        public List<CAB07_01DetailReportInfo> FindDetailOfDataIntoRev701460Report(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindDetailOfDataIntoRev701460Report",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _reportMgtService.FindDetailOfDataIntoRev701460Report(myCondition);
                });
        }

        public CompressData FindDetailOfDataIntoRev701460Report_Compress(CompressData myCondition)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindDetailOfDataIntoRev701460Report_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CAB07_01DetailReportInfo>>(_reportMgtService.FindDetailOfDataIntoRev701460Report(ServiceHelper.DecompressData<KeepMoneyPlaneOfAgencyConditionInfoReport>(myCondition)));
                });
        }

        public List<CAB08_01DetailReportInfo> FindDetailOfDataIntoRev701461Report(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindDetailOfDataIntoRev701461Report",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _reportMgtService.FindDetailOfDataIntoRev701461Report(myCondition);
                });
        }

        public CompressData FindDetailOfDataIntoRev701461Report_Compress(CompressData myCondition)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindDetailOfDataIntoRev701461Report_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CAB08_01DetailReportInfo>>(_reportMgtService.FindDetailOfDataIntoRev701461Report(ServiceHelper.DecompressData<KeepMoneyPlaneOfAgencyConditionInfoReport>(myCondition)));
                });
        }

        public List<CAB08_01AgencyInfo> FindAgentListIntoRev701461Report(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindAgentListIntoRev701461Report",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _reportMgtService.FindAgentListIntoRev701461Report(myCondition);
                });
        }

        public CompressData FindAgentListIntoRev701461Report_Compress(CompressData myCondition)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindAgentListIntoRev701461Report_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CAB08_01AgencyInfo>>(_reportMgtService.FindAgentListIntoRev701461Report(ServiceHelper.DecompressData<KeepMoneyPlaneOfAgencyConditionInfoReport>(myCondition)));
                });
        }

        public List<CAB09_01DetailReportInfo> FindDetailOfDataIntoRev701462Report(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindDetailOfDataIntoRev701462Report",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _reportMgtService.FindDetailOfDataIntoRev701462Report(myCondition);
                });
        }

        public CompressData FindDetailOfDataIntoRev701462Report_Compress(CompressData myCondition)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindDetailOfDataIntoRev701462Report_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CAB09_01DetailReportInfo>>(_reportMgtService.FindDetailOfDataIntoRev701462Report(ServiceHelper.DecompressData<KeepMoneyPlaneOfAgencyConditionInfoReport>(myCondition)));
                });
        }

        public List<CAB08_02DetailReportInfo> FindDetailOfDataIntoRev701463Report(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindDetailOfDataIntoRev701463Report",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _reportMgtService.FindDetailOfDataIntoRev701463Report(myCondition);
                });
        }

        public CompressData FindDetailOfDataIntoRev701463Report_Compress(CompressData myCondition)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindDetailOfDataIntoRev701463Report_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CAB08_02DetailReportInfo>>(_reportMgtService.FindDetailOfDataIntoRev701463Report(ServiceHelper.DecompressData<KeepMoneyPlaneOfAgencyConditionInfoReport>(myCondition)));
                });
        }

        public List<CAB10_01DetailReportInfo> FindDetailOfDataIntoRev701464Report(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindDetailOfDataIntoRev701464Report",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _reportMgtService.FindDetailOfDataIntoRev701464Report(myCondition);
                });
        }

        public CompressData FindDetailOfDataIntoRev701464Report_Compress(CompressData myCondition)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindDetailOfDataIntoRev701464Report_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CAB10_01DetailReportInfo>>(_reportMgtService.FindDetailOfDataIntoRev701464Report(ServiceHelper.DecompressData<KeepMoneyPlaneOfAgencyConditionInfoReport>(myCondition)));
                });
        }

        public CAB06_01HeaderReportInfo FindAndDisplayCAB06_01Header(EvaluateAgencyReportCondition myComdition, List<CAB06_01DetailReportInfo> reportDetail)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindAndDisplayCAB06_01Header",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _reportMgtService.FindAndDisplayCAB06_01Header(myComdition, reportDetail);
                });
        }

        public CompressData FindAndDisplayCAB06_01Header_Compress(CompressData myComdition, CompressData reportDetail)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindAndDisplayCAB06_01Header_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<CAB06_01HeaderReportInfo>(_reportMgtService.FindAndDisplayCAB06_01Header(ServiceHelper.DecompressData<EvaluateAgencyReportCondition>(myComdition), ServiceHelper.DecompressData<List<CAB06_01DetailReportInfo>>(reportDetail)));
                });
        }

        public List<CAB06_01DetailReportInfo> FindAndDisplayCAB06_01Detail(EvaluateAgencyReportCondition myComdition)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindAndDisplayCAB06_01Detail",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _reportMgtService.FindAndDisplayCAB06_01Detail(myComdition);
                });
        }

        public CompressData FindAndDisplayCAB06_01Detail_Compress(CompressData myComdition)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindAndDisplayCAB06_01Detail_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CAB06_01DetailReportInfo>>(_reportMgtService.FindAndDisplayCAB06_01Detail(ServiceHelper.DecompressData<EvaluateAgencyReportCondition>(myComdition)));
                });
        }

        public CAB03_HeaderReportInfo FindAndDisplayCAB03_Header(CheckInBillBookConditionInfoReport conditionPrintInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindAndDisplayCAB03_Header",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _reportMgtService.FindAndDisplayCAB03_Header(conditionPrintInfo);
                });
        }

        public CompressData FindAndDisplayCAB03_Header_Compress(CompressData conditionPrintInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindAndDisplayCAB03_Header_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<CAB03_HeaderReportInfo>(_reportMgtService.FindAndDisplayCAB03_Header(ServiceHelper.DecompressData<CheckInBillBookConditionInfoReport>(conditionPrintInfo)));
                });
        }

        public List<CAB03_DetailReportInfo> FindAndDisplayCAB03_Detail(CheckInBillBookConditionInfoReport conditionPrintInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindAndDisplayCAB03_Detail",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _reportMgtService.FindAndDisplayCAB03_Detail(conditionPrintInfo);
                });
        }

        public CompressData FindAndDisplayCAB03_Detail_Compress(CompressData conditionPrintInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindAndDisplayCAB03_Detail_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CAB03_DetailReportInfo>>(_reportMgtService.FindAndDisplayCAB03_Detail(ServiceHelper.DecompressData<CheckInBillBookConditionInfoReport>(conditionPrintInfo)));
                });
        }

        public CAB04_03HeaderReportInfo FindAndDisplayCAB04_03Header(CommissionVoucherConditionPrintReport commissionConditionInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindAndDisplayCAB04_03Header",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _reportMgtService.FindAndDisplayCAB04_03Header(commissionConditionInfo);
                });
        }

        public CompressData FindAndDisplayCAB04_03Header_Compress(CompressData commissionConditionInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindAndDisplayCAB04_03Header_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<CAB04_03HeaderReportInfo>(_reportMgtService.FindAndDisplayCAB04_03Header(ServiceHelper.DecompressData<CommissionVoucherConditionPrintReport>(commissionConditionInfo)));
                });
        }

        public List<CAB05_01DetailReportInfo> FindAndDisplayCAB05_01Detail(CAB05_01ConditionReportInfo conditionInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindAndDisplayCAB05_01Detail",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _reportMgtService.FindAndDisplayCAB05_01Detail(conditionInfo);
                });
        }

        public CompressData FindAndDisplayCAB05_01Detail_Compress(CompressData conditionInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindAndDisplayCAB05_01Detail_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CAB05_01DetailReportInfo>>(_reportMgtService.FindAndDisplayCAB05_01Detail(ServiceHelper.DecompressData<CAB05_01ConditionReportInfo>(conditionInfo)));
                });
        }

        public List<PA_7034DetailReportInfo> FindAndDisplayPA_7034Detail(PA_7034ConditionReportInfo conditionInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindAndDisplayPA_7034Detail",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _reportMgtService.FindAndDisplayPA_7034Detail(conditionInfo);
                });
        }

        public CompressData FindAndDisplayPA_7034Detail_Compress(CompressData conditionInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindAndDisplayPA_7034Detail_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<PA_7034DetailReportInfo>>(_reportMgtService.FindAndDisplayPA_7034Detail(ServiceHelper.DecompressData<PA_7034ConditionReportInfo>(conditionInfo)));
                });
        }

        public List<CAB12_01DetailReportInfo> FindAndDisplayCAB12_01Detail(CAB12_01ConditionReportInfo conditionInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindAndDisplayCAB12_01Detail",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _reportMgtService.FindAndDisplayCAB12_01Detail(conditionInfo);
                });
        }

        public CompressData FindAndDisplayCAB12_01Detail_Compress(CompressData conditionInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindAndDisplayCAB12_01Detail_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CAB12_01DetailReportInfo>>(_reportMgtService.FindAndDisplayCAB12_01Detail(ServiceHelper.DecompressData<CAB12_01ConditionReportInfo>(conditionInfo)));
                });
        }

        public CAB12_01HeaderReportInfo FindAndDisplayCAB12_01Header(List<CAB12_01DetailReportInfo> detail, string branchName, string branchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindAndDisplayCAB12_01Header",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _reportMgtService.FindAndDisplayCAB12_01Header(detail, branchName, branchId);
                });
        }

        public CompressData FindAndDisplayCAB12_01Header_Compress(CompressData detail, string branchName, string branchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindAndDisplayCAB12_01Header_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<CAB12_01HeaderReportInfo>(_reportMgtService.FindAndDisplayCAB12_01Header(ServiceHelper.DecompressData<List<CAB12_01DetailReportInfo>>(detail), branchName, branchId));
                });
        }

        public List<CAN34_01DetailReportInfo> FindAndDisplayCAN34_01Detail(CAN34_01CondtionReportInfo conditionInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindAndDisplayCAN34_01Detail",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _reportMgtService.FindAndDisplayCAN34_01Detail(conditionInfo);
                });
        }

        public CompressData FindAndDisplayCAN34_01Detail_Compress(CompressData conditionInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindAndDisplayCAN34_01Detail_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CAN34_01DetailReportInfo>>(_reportMgtService.FindAndDisplayCAN34_01Detail(ServiceHelper.DecompressData<CAN34_01CondtionReportInfo>(conditionInfo)));
                });
        }

        public string GetBillKeeperNameByBillBook(string billBookId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "GetBillKeeperNameByBillBook",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _reportMgtService.GetBillKeeperNameByBillBook(billBookId);
                });
        }

        public decimal? GetIntownReceive(string billBookId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "GetIntownReceive",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _reportMgtService.GetIntownReceive(billBookId);
                });
        }

        public List<CAB12_02DetailReportInfo> FindAndDisplayCAB12_02Detail(CAB12_01ConditionReportInfo conditionInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindAndDisplayCAB12_02Detail",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _reportMgtService.FindAndDisplayCAB12_02Detail(conditionInfo);
                });
        }

        public CompressData FindAndDisplayCAB12_02Detail_Compress(CompressData conditionInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindAndDisplayCAB12_02Detail_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CAB12_02DetailReportInfo>>(_reportMgtService.FindAndDisplayCAB12_02Detail(ServiceHelper.DecompressData<CAB12_01ConditionReportInfo>(conditionInfo)));
                });
        }

        public CompressData FindAndDisplayCAB03_02Report_Compress(CompressData conditionInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindAndDisplayCAB03_02Report_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<CAB03_02ReportInfo>(_reportMgtService.FindAndDisplayCAB03_02Report(ServiceHelper.DecompressData<CheckInBillBookConditionInfoReport>(conditionInfo)));
                });
        }

        public CompressData FindAndDisplayCAB03_03Report_Compress(CompressData condition)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindAndDisplayCAB03_03Report_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<CAB03_03ReportInfo>(_reportMgtService.FindAndDisplayCAB03_03Report(ServiceHelper.DecompressData<CheckInBillBookConditionInfoReport>(condition)));
                });
        }

        public CompressData FindAndDisplayCAB03_04Report_Compress(CompressData condition)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "FindAndDisplayCAB03_04Report_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<CAB03_04ReportInfo>(_reportMgtService.FindAndDisplayCAB03_04Report(ServiceHelper.DecompressData<CheckInBillBookConditionInfoReport>(condition)));
                });
        }

        public CompressData GetBillBookInfoReport_Compress(CompressData condition)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "GetBillBookInfoReport_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<BillBookInfoMasterReport>(_reportMgtService.GetBillBookInfoReport(ServiceHelper.DecompressData<BillBookHeaderInfo>(condition)));
                });
        }

        public CompressData GetRptCAB13_01RptInfo_Commpress(CAB13_01ConditionRptInfo condition)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "ReportMgtWCFService", "GetRptCAB13_01RptInfo_Commpress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CAB13_01RptInfo>>(_reportMgtService.GetRptCAB13_01RptInfo(condition));
                });
        }

    }
}
