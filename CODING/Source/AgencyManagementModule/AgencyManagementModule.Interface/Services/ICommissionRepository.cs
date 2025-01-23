using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;

namespace PEA.BPM.AgencyManagementModule.Interface.Services
{
    public interface ICommissionRepository
    {
        #region "Get Function"

        bool IsCommissionCalculated(string billBookId);
        List<BranchIdInfoItem> GetAllBranch();
        List<BranchIdInfoItem> GetBranchSameArea(BranchIdInfoItem branch);
        List<BranchIdInfoItem> GetBranchUnderParent(BranchIdInfoItem branch);
        List<BranchIdInfoItem> GetFromToBranch(string branchFrom, string branchTo, List<BranchIdInfoItem> scope);

        List<BillBookCommissionBranchInfo> GetBillBookCommissionBranch(string branchId, string periodFrom,
                                                                       string periodTo);

        CommissionRateInfo GetRateCommissionForCalculate(string branchid);
        string GetAgencyTypeById(string agencyId);
        decimal? GetAgencyDebtAmount(string agencyId);
        string GetBranchElectricCode(string agencyId);
        string GetBusinessTypeDeesc(string bpTypeId);
        string GetBillBookIdByAgentIdPeriodAndCreateDate(string agentId, string billBookPeriod, string createDate);
        ArrayList GetDetailAgencyById(string agencyId);
        ArrayList GetExtraMoneyHelpCommissionById(string commissionId);
        string GetBranchNameByAgentId(string agentId);
        List<BillPasteReportInfo> GetBillPasteInBillBookReturn(string agentId, string billbookId);
        string GetNameAgencyById(string agencyId);
        string GetBranchNameByBranchId(string branchId);
        string GetHeaderCodeOfPEAByBranchId(string peaCode);
        string GetHeaderNameOfPEAByBranchId(string peaCode);
        BranchIdInfoItem GetBranchByBillBookId(string billBookId);
        string GetBranchIdByAgentId(string agentId);
        List<CAB03_DetailReportInfo> GetCAB03(string billBookId, string absId, string pmId);

        List<CAB04_03DetailReportInfo> GetTotalBillItemAndAmountGroupByBillBookId(string agentId, string bookPeriod,
                                                                                  string createDate);

        List<BillInfoInEachBillBookIdInfo> GetBillInfoInEachBillBookId(string billBookId);
        EvaluateTotalBillInfo GetEvaluateBillInfoMonthly(string branchId, int periodMonth, int periodYear);
        EvaluateTotalBillInfo GetEvaluateBillInfoPeroid(string branchId, int sMonth, int eMonth, int yearPriod);

        List<EvaluateAgencyCounter> GetIssueBillAndAmountOfAgentInEachBranchForPeriod(string branchId, int sMonth,
                                                                                      int eMonth, int yearPriod);

        List<EvaluateAgencyCounter> GetIssueBillAndAmountOfAgentInEachBranchForMonthly(string branchId, int periodMonth,
                                                                                       int yearPriod);

        List<BranchIdInfoItem> GetBranchUnderByBranchId(string branchId);
        List<CAB08_01AgencyInfo> GetCAB08_01AgencyList(string branchId, string billPeriod);
        decimal? GetAmountOfBillArrOfBranch(string startBranchId, string endBranchId, string periodBill);
        decimal? GetAmountMoneyOfBillArrOfBranch(string startBranchId, string endBranchId, string periodBill);
        List<TravelHelpRate> GetTravelHelpRate(TravelHelpRateConditionInfo spcCommission);
        string GetTaxIdInAgencyByAgentId(string agentId);
        DateTime? GetBillBookCheckInDate(string billBookId);
        decimal? GetAgencyVatRate(string agentId);
        decimal? GetAgencyTaxRate(string agentId);
        int GetMaxCalculatedCommission(string agencyId, string period);
        bool IsFarLandHelpCommissionCalculated(string agencyId, string period);
        TravelHelpRate GetTravelHelpHistory(TravelHelpRateConditionInfo spc);
        Hashtable GetFarLandCollectedCount(string agencyId, string bookPeriod, string branchId);
        Hashtable GetWaterHelpCollectedCount(string agencyId, string bookPeriod, string branchId, DateTime createDate);
        #endregion

        #region "Generate Function"

        string GenerateVoucherId(string strYear);

        #endregion

        #region "Update Function"

        bool UpdateVoucherIdByCmdId(int cmdId, string voucherId);

        #endregion

        #region "Delete Function"

        bool DeleteCommissionByBillBook(DbTransaction trans, string billbookId, string modifiedBy, DateTime modifiedDt);
        #endregion

    }
}
