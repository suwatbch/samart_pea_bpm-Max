using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;

namespace PEA.BPM.AgencyManagementModule.Interface.Services
{
    public interface IReportRepository
    {
        List<CAB05_01DetailReportInfo> GetCAB05_01(string branchId, string startAgencyId, string endAgencyId,
                                                   string periodStart, string periodEnd);

        string GetMaxAgencyIdInBranch(string branchId);
        string GetMinAgencyIdInBranch(string branchId);
        List<PA_7034DetailReportInfo> GetPA_7034(PA_7034ConditionReportInfo conn);
        List<CAB12_01DetailReportInfo> GetCAB12_01(string branchId, CAB12_01ConditionReportInfo conn);
        List<CAB12_02DetailReportInfo> GetCAB12_02(string branchId, CAB12_01ConditionReportInfo conn);
        List<CAN34_01DetailReportInfo> GetCAN34_01(string branchId, CAN34_01CondtionReportInfo conn);
        List<CAB10_01DetailReportInfo> GetCAB10_01(string branchId, string billPeriod, string modifiedBy);
        decimal? GetIntownReceive(string billbookId);
        List<CAB07_01DetailReportInfo> GetCAB07_01(string period, string branchId, string modifiedBy);
        List<CAB08_01DetailReportInfo> GetCAB08_01(string branchId, string billPeriod, string modifiedBy);
        List<CAB09_01DetailReportInfo> GetCAB09_01(string branchId, string billPeriod, string modifiedBy);
        List<CAB08_02DetailReportInfo> GetCAB08_02(string branchId, string billPeriod, string modifiedBy);
        string GetCollectCountFromPortion(string portionId);
        List<CAB13_01RptInfo> GetRptCAB13_01RptInfoData(CAB13_01ConditionRptInfo condition);

    }
}
