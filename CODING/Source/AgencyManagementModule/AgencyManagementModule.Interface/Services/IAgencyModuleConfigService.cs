using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;

namespace PEA.BPM.AgencyManagementModule.Interface.Services
{
    public interface IAgencyModuleConfigService
    {
        //NonWorkingDayInfo GetNonWorkingDayInfo(ArrayList parem);
        //int? GetWorkingDaysBetweenDates(ArrayList parem);
        //DateTime GetNextDate(ArrayList parem);
        //List<NonWorkingDayInfo> LoadCalendarForBranch(string branchId);
        FeeBaseElement GetBaseCommissionRate(string branchId);
        bool UpdateCommissionRate(FeeBaseElement feeBase);
    }
}
