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
    [Service(typeof(ICommissionMgtService))]
    public class CommissionMgtServiceSwitcher : ICommissionMgtService
    {
        public CommissionMgtServiceSwitcher()
		{
        }

        #region Service Factory
        public ICommissionMgtService GetService()
        {
            return GetService(false);
        }

        public ICommissionMgtService GetService(bool serverService)
        {
            if (serverService || Session.Branch.OnlineConnection)
            {
                return new CommissionMgtSG(true);
            }
            else
            {
                return new CommissionMgtSG(false);
            }
        }
        #endregion       

        #region ICommissionMgtService Members
        public CommissionInfo CalculateCommissionAndFine(BookSearchInfo searchInfo, FeeBaseElement comRate, BooniesCommissionInfo hp)
        {
            return GetService().CalculateCommissionAndFine(searchInfo, comRate, hp);
        }

        public string SaveCommission(System.Data.Common.DbTransaction trans, HelpOfferInfo flavour)
        {
            return GetService().SaveCommission(trans, flavour);
        }

        public FeeBaseElement LoadCommissionRate(BookSearchInfo searchInfo)
        {
            return GetService().LoadCommissionRate(searchInfo);
        }

        public bool IsCommissionCalculated(BookSearchInfo searchInfo)
        {
            return GetService().IsCommissionCalculated(searchInfo);
        }

        public List<CalculatedCommissionInfo> GetCalCountOfPeriodByAgentId(BookSearchInfo searchInfo)
        {
            return GetService().GetCalCountOfPeriodByAgentId(searchInfo);
        }

        public int GetCommissionCountOfPeriod(BookSearchInfo searchInfo)
        {
            return GetService().GetCommissionCountOfPeriod(searchInfo);
        }

        public decimal? GetAndDisplayAdvPaymentAmount(BookSearchInfo searchInfo)
        {
            return GetService().GetAndDisplayAdvPaymentAmount(searchInfo);
        }

        public AgentInfo GetAgentHelpInformation(string agentId)
        {
            return GetService().GetAgentHelpInformation(agentId);
        }

        public List<string> GetListOfCreatedDate(BookSearchInfo searchInfo)
        {
            return GetService().GetListOfCreatedDate(searchInfo);
        }

        public List<string> BookListByCreateDate(BookSearchInfo searchInfo)
        {
            return GetService().BookListByCreateDate(searchInfo);
        }

        public bool IsBookAvailable(BookSearchInfo searchInfo)
        {
            return GetService().IsBookAvailable(searchInfo);
        }
  
        public TravelHelpRate GetTravelHelpRate(TravelHelpRateConditionInfo spcCondition)
        {
            return GetService().GetTravelHelpRate(spcCondition);
        }

        public FeeBaseElement GetFeeBase(string branchId)
        {
            return GetService().GetFeeBase(branchId);
        }
        
        public bool IsBillBookCalculated(string billbookId)
        {
            return GetService().IsBillBookCalculated(billbookId);
        }
    
        public bool IsBookAlreadyPaid(BookSearchInfo searchInfo)
        {
            return GetService().IsBookAlreadyPaid(searchInfo);
        }

        public List<CommissionHistoryInfo> GetCommissionHistory(BookSearchInfo searchInfo)
        {
            return GetService().GetCommissionHistory(searchInfo);
        }

        #endregion
    }
}
