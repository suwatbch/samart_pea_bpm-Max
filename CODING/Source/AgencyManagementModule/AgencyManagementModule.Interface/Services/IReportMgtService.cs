using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;

namespace PEA.BPM.AgencyManagementModule.Interface.Services
{
    public interface IReportMgtService
    {
        //void LoadBillBookMasterDataByAgencyID(int locAgentId, LoadBillBookMasterDataByAgencyIDCallback callback);
        //void LoadBillBookDetailDataByAgencyID(int locAgentId, string locLineNo, LoadBillBookDetailDataByAgencyIDCallback callback);

        List<BillBookDetailReportListInfo> GetBillBookDetailReportList(string billBookId);
        List<BillBookInfoDetailReport> GetBillBookInfoDetailReport(string billBookId);
        string GetBillKeeperNameByBillBook(string billBookId);
        decimal? GetIntownReceive(string billBookId);
        List<CAB02_DetailReportInfo> GetAgencyMoneyReturnReport(string agencyIdFrom, string agencyIdTo,
                                                                           string periodFrom, string periodTo, string branchId);
        List<AgencyBillCollectionMasterReport> GetAgencyBillCollectionReport(string period, string branchId, string branchName);
        decimal GetAdvPaidByBillBookId(string billBookId);       

        List<CommissionVoucherInfoReport> FindAndDisplayAgencyCommissionReportInfo(CommissionVoucherConditionPrintReport commissionCoditionInfo);
        List<CommissionRegistInfoReport> FindAndDisplayAgencyCommissionRegistryReportInfo(CommissionVoucherConditionPrintReport commissionCoditionInfo);
        CAB04_03HeaderReportInfo FindAndDisplayCAB04_03Header(CommissionVoucherConditionPrintReport commissionConditionInfo);
        List<CAB04_03DetailReportInfo> FindAndDisplayCAB04_03Detail(CommissionVoucherConditionPrintReport commissionConditionInfo);        

        List<ReturnBillBookBillPasteStatusReportInfo> FindAndDisplayPasteBillStatus(CheckInBillBookConditionInfoReport conditionPrintInfo);
        //List<ReturnBillBookIssueBillInBillBookHeadReportInfo> FindDisplayIssueBillARInfoMaster(CheckInBillBookConditionInfoReport conditionPrintInfo);
        CAB03_02DetailReportInfo FindDisplayIssueBillARInfoDetail(CheckInBillBookConditionInfoReport conditionPrintInfo);        
        List<CAB07_01DetailReportInfo> FindDetailOfDataIntoRev701460Report(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition);
        List<CAB08_01DetailReportInfo> FindDetailOfDataIntoRev701461Report(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition);
        List<CAB08_01AgencyInfo> FindAgentListIntoRev701461Report(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition);
        List<CAB09_01DetailReportInfo> FindDetailOfDataIntoRev701462Report(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition);
        List<CAB08_02DetailReportInfo> FindDetailOfDataIntoRev701463Report(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition);
        List<CAB10_01DetailReportInfo> FindDetailOfDataIntoRev701464Report(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition);
        CAB06_01HeaderReportInfo FindAndDisplayCAB06_01Header(EvaluateAgencyReportCondition myComdition, List<CAB06_01DetailReportInfo> reportDetail);
        List<CAB06_01DetailReportInfo> FindAndDisplayCAB06_01Detail(EvaluateAgencyReportCondition myComdition);

        CAB03_HeaderReportInfo FindAndDisplayCAB03_Header(CheckInBillBookConditionInfoReport conditionPrintInfo);
        List<CAB03_DetailReportInfo> FindAndDisplayCAB03_Detail(CheckInBillBookConditionInfoReport conditionPrintInfo);
        List<CAB05_01DetailReportInfo> FindAndDisplayCAB05_01Detail(CAB05_01ConditionReportInfo conditionInfo);
        List<PA_7034DetailReportInfo> FindAndDisplayPA_7034Detail(PA_7034ConditionReportInfo conditionInfo);
        List<CAN34_01DetailReportInfo> FindAndDisplayCAN34_01Detail(CAN34_01CondtionReportInfo conditionInfo);
        List<CAB12_01DetailReportInfo> FindAndDisplayCAB12_01Detail(CAB12_01ConditionReportInfo conditionInfo);
        List<CAB12_02DetailReportInfo> FindAndDisplayCAB12_02Detail(CAB12_01ConditionReportInfo conditionInfo);
        CAB12_01HeaderReportInfo FindAndDisplayCAB12_01Header(List<CAB12_01DetailReportInfo> detail, string branchName, string branchId);


        CAB03_02ReportInfo FindAndDisplayCAB03_02Report(CheckInBillBookConditionInfoReport condition);
        CAB03_03ReportInfo FindAndDisplayCAB03_03Report(CheckInBillBookConditionInfoReport condition);
        CAB03_04ReportInfo FindAndDisplayCAB03_04Report(CheckInBillBookConditionInfoReport condition);

        BillBookInfoMasterReport GetBillBookInfoReport(BillBookHeaderInfo bookHeader);

        List<CAB13_01RptInfo> GetRptCAB13_01RptInfo(CAB13_01ConditionRptInfo condition);
    }
}

