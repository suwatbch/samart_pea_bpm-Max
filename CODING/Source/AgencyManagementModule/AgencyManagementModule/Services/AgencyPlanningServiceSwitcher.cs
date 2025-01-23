using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.AgencyManagementModule.Interface.Services;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using System.ComponentModel;
using PEA.BPM.AgencyManagementModule.SG;
using PEA.BPM.Architecture.CommonUtilities;
using System.Data.Common;

namespace PEA.BPM.AgencyManagementModule.Services
{
    [Service(typeof(IAgencyPlanningService))]
    public class AgencyPlanningServiceSwitcher : IAgencyPlanningService
    {
        public AgencyPlanningServiceSwitcher()
		{
        }

        #region Service Factory
        public IAgencyPlanningService GetService()
        {
            return GetService(false);
        }

        public IAgencyPlanningService GetService(bool serverService)
        {
            if (serverService || Session.Branch.OnlineConnection)
            {
                return new AgencyPlanningSG(true);
            }
            else
            {
                return new AgencyPlanningSG(true);
            }
        }
        #endregion

        #region IAgencyPlanningService Members

        public bool SaveAssignedLineofAgent(DbTransaction trans, BindingList<LineInfo> asiLineList)
        {
            return GetService().SaveAssignedLineofAgent(trans, asiLineList);
        }

        public BindingList<LineInfo> FindAndDisplayLineOfAgentSearchInfo(string agentId)
        {
            return GetService().FindAndDisplayLineOfAgentSearchInfo(agentId);
        }

        public List<AgentInfo> AcquireAgentAssetSearchInformation(AgentSearchInfo searchInfo)
        {
            return GetService().AcquireAgentAssetSearchInformation(searchInfo);
        }

        public List<PeaInfo> FindAndDisplayBranchByKeyword(string keyword, string branchId)
        {
            return GetService().FindAndDisplayBranchByKeyword(keyword, branchId);
        }

        public BindingList<LineInfo> FindAndDisplayLineByKeyword(LineSearchBoxInfo searchInfo)
        {
            return GetService().FindAndDisplayLineByKeyword(searchInfo);
        }

    
       

        #endregion

       
    }
}
