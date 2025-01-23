using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using System.Data.Common;

namespace PEA.BPM.AgencyManagementModule.Interface.Services
{
    public interface ICommissionMgtService
    {
        CommissionInfo CalculateCommissionAndFine(BookSearchInfo searchInfo, FeeBaseElement comRate, BooniesCommissionInfo hp);
        string SaveCommission(DbTransaction trans, HelpOfferInfo flavour);
        FeeBaseElement LoadCommissionRate(BookSearchInfo searchInfo);
        bool IsCommissionCalculated(BookSearchInfo searchInfo);
        List<CalculatedCommissionInfo> GetCalCountOfPeriodByAgentId(BookSearchInfo searchInfo);
        int GetCommissionCountOfPeriod(BookSearchInfo searchInfo);
        decimal? GetAndDisplayAdvPaymentAmount(BookSearchInfo searchInfo);
        AgentInfo GetAgentHelpInformation(string agentId);
        List<string> GetListOfCreatedDate(BookSearchInfo searchInfo);
        List<string> BookListByCreateDate(BookSearchInfo searchInfo);
        bool IsBookAvailable(BookSearchInfo searchInfo);
        bool IsBookAlreadyPaid(BookSearchInfo searchInfo);
        TravelHelpRate GetTravelHelpRate(TravelHelpRateConditionInfo spcCondition);
        FeeBaseElement GetFeeBase(string branchId);
        bool IsBillBookCalculated(string billbookId);
        List<CommissionHistoryInfo> GetCommissionHistory(BookSearchInfo searchInfo);
    }
}
