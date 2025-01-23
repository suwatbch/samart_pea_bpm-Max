using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.AgencyManagementModule.Interface.Services;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.AgencyManagementModule.SG;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.AgencyManagementModule.Services
{
    [Service(typeof(IReportMgtService))]
    public class ReportMgtServiceSwitcher : IReportMgtService
    {
        public ReportMgtServiceSwitcher()
        {
        }

        #region Service Factory
        public IReportMgtService GetService()
        {
            return GetService(false);
        }

        public IReportMgtService GetService(bool serverService)
        {
            if (serverService || Session.Branch.OnlineConnection)
            {
                return new ReportMgtSG(true);
            }
            else
            {
                return new ReportMgtSG(false);
            }
        }
        #endregion

        #region IReportMgtService Members

        public List<BillBookDetailReportListInfo> GetBillBookDetailReportList(string billBookId)
        {
            return GetService().GetBillBookDetailReportList(billBookId);
        }

        public List<BillBookInfoDetailReport> GetBillBookInfoDetailReport(string billBookId)
        {
            return GetService().GetBillBookInfoDetailReport(billBookId);
        }

        public List<CAB02_DetailReportInfo> GetAgencyMoneyReturnReport(string agencyIdFrom, string agencyIdTo, string periodFrom, string periodTo, string branchId)
        {
            return GetService().GetAgencyMoneyReturnReport(agencyIdFrom, agencyIdTo, periodFrom, periodTo, branchId);
        }

        public List<AgencyBillCollectionMasterReport> GetAgencyBillCollectionReport(string period, string branchId, string branchName)
        {
            return GetService().GetAgencyBillCollectionReport(period, Session.Branch.Id, Session.Branch.Name);
        }

        public decimal GetAdvPaidByBillBookId(string billBookId)
        {
            return GetService().GetAdvPaidByBillBookId(billBookId);
        }

        public List<CommissionVoucherInfoReport> FindAndDisplayAgencyCommissionReportInfo(CommissionVoucherConditionPrintReport commissionCoditionInfo)
        {
            return GetService().FindAndDisplayAgencyCommissionReportInfo(commissionCoditionInfo);
        }

        public List<CommissionRegistInfoReport> FindAndDisplayAgencyCommissionRegistryReportInfo(CommissionVoucherConditionPrintReport commissionCoditionInfo)
        {
            return GetService().FindAndDisplayAgencyCommissionRegistryReportInfo(commissionCoditionInfo);
        }

        public List<CAB04_03DetailReportInfo> FindAndDisplayCAB04_03Detail(CommissionVoucherConditionPrintReport commissionConditionInfo)
        {
            return GetService().FindAndDisplayCAB04_03Detail(commissionConditionInfo);
        }

        public List<ReturnBillBookBillPasteStatusReportInfo> FindAndDisplayPasteBillStatus(CheckInBillBookConditionInfoReport conditionPrintInfo)
        {
            return GetService().FindAndDisplayPasteBillStatus(conditionPrintInfo);
        }

        public CAB03_02DetailReportInfo FindDisplayIssueBillARInfoDetail(CheckInBillBookConditionInfoReport conditionPrintInfo)
        {
            return GetService().FindDisplayIssueBillARInfoDetail(conditionPrintInfo);
        }
       
        public List<CAB07_01DetailReportInfo> FindDetailOfDataIntoRev701460Report(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition)
        {
            return GetService().FindDetailOfDataIntoRev701460Report(myCondition);
        }

        public List<CAB08_01DetailReportInfo> FindDetailOfDataIntoRev701461Report(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition)
        {
            return GetService().FindDetailOfDataIntoRev701461Report(myCondition);
        }

        public List<CAB08_01AgencyInfo> FindAgentListIntoRev701461Report(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition)
        {
            return GetService().FindAgentListIntoRev701461Report(myCondition);
        }

        public List<CAB09_01DetailReportInfo> FindDetailOfDataIntoRev701462Report(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition)
        {
            return GetService().FindDetailOfDataIntoRev701462Report(myCondition);
        }

        public List<CAB08_02DetailReportInfo> FindDetailOfDataIntoRev701463Report(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition)
        {
            return GetService().FindDetailOfDataIntoRev701463Report(myCondition);
        }

        public List<CAB10_01DetailReportInfo> FindDetailOfDataIntoRev701464Report(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition)
        {
            return GetService().FindDetailOfDataIntoRev701464Report(myCondition);
        }


        public List<CAB06_01DetailReportInfo> FindAndDisplayCAB06_01Detail(EvaluateAgencyReportCondition myComdition)
        {
            return GetService().FindAndDisplayCAB06_01Detail(myComdition);
        }

        public CAB06_01HeaderReportInfo FindAndDisplayCAB06_01Header(EvaluateAgencyReportCondition myComdition, List<CAB06_01DetailReportInfo> reportDetail)
        {
            return GetService().FindAndDisplayCAB06_01Header(myComdition, reportDetail);
        }

        public CAB03_HeaderReportInfo FindAndDisplayCAB03_Header(CheckInBillBookConditionInfoReport conditionPrintInfo)
        {
            return GetService().FindAndDisplayCAB03_Header(conditionPrintInfo);
        }

        public List<CAB03_DetailReportInfo> FindAndDisplayCAB03_Detail(CheckInBillBookConditionInfoReport conditionPrintInfo)
        {
            return GetService().FindAndDisplayCAB03_Detail(conditionPrintInfo);
        }

        public CAB04_03HeaderReportInfo FindAndDisplayCAB04_03Header(CommissionVoucherConditionPrintReport commissionCondition)
        {
            return GetService().FindAndDisplayCAB04_03Header(commissionCondition);
        }

        public List<CAB05_01DetailReportInfo> FindAndDisplayCAB05_01Detail(CAB05_01ConditionReportInfo conditionInfo)
        {
            return GetService().FindAndDisplayCAB05_01Detail(conditionInfo);
        }

        public List<PA_7034DetailReportInfo> FindAndDisplayPA_7034Detail(PA_7034ConditionReportInfo conditionInfo)
        {
            return GetService().FindAndDisplayPA_7034Detail(conditionInfo);
        }

        public List<CAB12_01DetailReportInfo> FindAndDisplayCAB12_01Detail(CAB12_01ConditionReportInfo conditionInfo)
        {
            return GetService().FindAndDisplayCAB12_01Detail(conditionInfo);
        }

        public CAB12_01HeaderReportInfo FindAndDisplayCAB12_01Header(List<CAB12_01DetailReportInfo> detail, string branchName, string branchId)
        {
            return GetService().FindAndDisplayCAB12_01Header(detail, branchName, branchId);
        }

        public List<CAB12_02DetailReportInfo> FindAndDisplayCAB12_02Detail(CAB12_01ConditionReportInfo conditionInfo)
        {
            return GetService().FindAndDisplayCAB12_02Detail(conditionInfo);
        }

        public List<CAN34_01DetailReportInfo> FindAndDisplayCAN34_01Detail(CAN34_01CondtionReportInfo conditionInfo)
        {
            return GetService().FindAndDisplayCAN34_01Detail(conditionInfo);
        }

        public string GetBillKeeperNameByBillBook(string billBookId)
        {
            return GetService().GetBillKeeperNameByBillBook(billBookId);
        }

        public decimal? GetIntownReceive(string billBookId)
        {
            return GetService().GetIntownReceive(billBookId);
        }

        public CAB03_02ReportInfo FindAndDisplayCAB03_02Report(CheckInBillBookConditionInfoReport condition)
        {
            return GetService().FindAndDisplayCAB03_02Report(condition);
        }

        public CAB03_03ReportInfo FindAndDisplayCAB03_03Report(CheckInBillBookConditionInfoReport condition)
        {
            return GetService().FindAndDisplayCAB03_03Report(condition);
        }

        public CAB03_04ReportInfo FindAndDisplayCAB03_04Report(CheckInBillBookConditionInfoReport condition)
        {
            return GetService().FindAndDisplayCAB03_04Report(condition);
        }
    
        public BillBookInfoMasterReport GetBillBookInfoReport(BillBookHeaderInfo bookHeader)
        {
            return GetService().GetBillBookInfoReport(bookHeader);
        }


        public List<CAB13_01RptInfo> GetRptCAB13_01RptInfo(CAB13_01ConditionRptInfo condition)
        {
            try
            {
                return GetService().GetRptCAB13_01RptInfo(condition);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

       
    }
}
