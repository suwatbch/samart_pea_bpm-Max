using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ComponentModel;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using WCFExtras.Soap;


namespace BPMReportService.AGENCY
{
    [SoapHeaders, ServiceContract(Namespace = "http://tempuri.org")]
    public interface IReportMgtWCFService
    {
        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        List<BillBookDetailReportListInfo> GetBillBookDetailReportList(string billBookId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData GetBillBookDetailReportList_Compress(string billBookId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        List<BillBookInfoDetailReport> GetBillBookInfoDetailReport(string billBookId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData GetBillBookInfoDetailReport_Compress(string billBookId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        List<CAB02_DetailReportInfo> GetAgencyMoneyReturnReport(string agencyIdFrom, string agencyIdTo, string periodFrom, string periodTo, string branchId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData GetAgencyMoneyReturnReport_Compress(string agencyIdFrom, string agencyIdTo, string periodFrom, string periodTo, string branchId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        List<AgencyBillCollectionMasterReport> GetAgencyBillCollectionReport(string period, string branchId, string branchName);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData GetAgencyBillCollectionReport_Compress(string period, string branchId, string branchName);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        decimal GetAdvPaidByBillBookId(string billBookId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        List<CommissionVoucherInfoReport> FindAndDisplayAgencyCommissionReportInfo(CommissionVoucherConditionPrintReport commissionCoditionInfo);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData FindAndDisplayAgencyCommissionReportInfo_Compress(CompressData commissionCoditionInfo);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        List<CommissionRegistInfoReport> FindAndDisplayAgencyCommissionRegistryReportInfo(CommissionVoucherConditionPrintReport commissionCoditionInfo);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData FindAndDisplayAgencyCommissionRegistryReportInfo_Compress(CompressData commissionCoditionInfo);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        List<CAB04_03DetailReportInfo> FindCommissionRegistryDetailReport(CommissionVoucherConditionPrintReport commissionConditionInfo);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData FindCommissionRegistryDetailReport_Compress(CompressData commissionConditionInfo);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        List<ReturnBillBookBillPasteStatusReportInfo> FindAndDisplayPasteBillStatus(CheckInBillBookConditionInfoReport conditionPrintInfo);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData FindAndDisplayPasteBillStatus_Compress(CompressData conditionPrintInfo);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CAB03_02DetailReportInfo FindDisplayIssueBillARInfoDetail(CheckInBillBookConditionInfoReport conditionPrintInfo);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData FindDisplayIssueBillARInfoDetail_Compress(CompressData conditionPrintInfo);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        List<CAB07_01DetailReportInfo> FindDetailOfDataIntoRev701460Report(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData FindDetailOfDataIntoRev701460Report_Compress(CompressData myCondition);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        List<CAB08_01DetailReportInfo> FindDetailOfDataIntoRev701461Report(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData FindDetailOfDataIntoRev701461Report_Compress(CompressData myCondition);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        List<CAB08_01AgencyInfo> FindAgentListIntoRev701461Report(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData FindAgentListIntoRev701461Report_Compress(CompressData myCondition);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        List<CAB09_01DetailReportInfo> FindDetailOfDataIntoRev701462Report(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData FindDetailOfDataIntoRev701462Report_Compress(CompressData myCondition);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        List<CAB08_02DetailReportInfo> FindDetailOfDataIntoRev701463Report(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData FindDetailOfDataIntoRev701463Report_Compress(CompressData myCondition);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        List<CAB10_01DetailReportInfo> FindDetailOfDataIntoRev701464Report(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData FindDetailOfDataIntoRev701464Report_Compress(CompressData myCondition);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CAB06_01HeaderReportInfo FindAndDisplayCAB06_01Header(EvaluateAgencyReportCondition myComdition, List<CAB06_01DetailReportInfo> reportDetail);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData FindAndDisplayCAB06_01Header_Compress(CompressData myComdition, CompressData reportDetail);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        List<CAB06_01DetailReportInfo> FindAndDisplayCAB06_01Detail(EvaluateAgencyReportCondition myComdition);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData FindAndDisplayCAB06_01Detail_Compress(CompressData myComdition);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CAB03_HeaderReportInfo FindAndDisplayCAB03_Header(CheckInBillBookConditionInfoReport conditionPrintInfo);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData FindAndDisplayCAB03_Header_Compress(CompressData conditionPrintInfo);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        List<CAB03_DetailReportInfo> FindAndDisplayCAB03_Detail(CheckInBillBookConditionInfoReport conditionPrintInfo);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData FindAndDisplayCAB03_Detail_Compress(CompressData conditionPrintInfo);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CAB04_03HeaderReportInfo FindAndDisplayCAB04_03Header(CommissionVoucherConditionPrintReport commissionConditionInfo);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData FindAndDisplayCAB04_03Header_Compress(CompressData commissionConditionInfo);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        List<CAB05_01DetailReportInfo> FindAndDisplayCAB05_01Detail(CAB05_01ConditionReportInfo conditionInfo);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData FindAndDisplayCAB05_01Detail_Compress(CompressData conditionInfo);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        List<PA_7034DetailReportInfo> FindAndDisplayPA_7034Detail(PA_7034ConditionReportInfo conditionInfo);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData FindAndDisplayPA_7034Detail_Compress(CompressData conditionInfo);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        List<CAB12_01DetailReportInfo> FindAndDisplayCAB12_01Detail(CAB12_01ConditionReportInfo conditionInfo);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData FindAndDisplayCAB12_01Detail_Compress(CompressData conditionInfo);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CAB12_01HeaderReportInfo FindAndDisplayCAB12_01Header(List<CAB12_01DetailReportInfo> detail, string branchName, string branchId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData FindAndDisplayCAB12_01Header_Compress(CompressData detail, string branchName, string branchId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        List<CAN34_01DetailReportInfo> FindAndDisplayCAN34_01Detail(CAN34_01CondtionReportInfo conditionInfo);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData FindAndDisplayCAN34_01Detail_Compress(CompressData conditionInfo);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        string GetBillKeeperNameByBillBook(string billBookId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        decimal? GetIntownReceive(string billBookId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        List<CAB12_02DetailReportInfo> FindAndDisplayCAB12_02Detail(CAB12_01ConditionReportInfo conditionInfo);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData FindAndDisplayCAB12_02Detail_Compress(CompressData conditionInfo);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData FindAndDisplayCAB03_02Report_Compress(CompressData conditionInfo);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData FindAndDisplayCAB03_03Report_Compress(CompressData condition);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData FindAndDisplayCAB03_04Report_Compress(CompressData condition);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData GetBillBookInfoReport_Compress(CompressData condition);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData GetRptCAB13_01RptInfo_Commpress(CAB13_01ConditionRptInfo condition);

    }
}
