using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using System.ComponentModel;
using System.Data.Common;

namespace PEA.BPM.AgencyManagementModule.Interface.Services
{
    public interface IAgencyPlanningService
    {
        bool SaveAssignedLineofAgent(DbTransaction trans, BindingList<LineInfo> asiLineList);
        BindingList<LineInfo> FindAndDisplayLineOfAgentSearchInfo(string agentId);
        List<AgentInfo> AcquireAgentAssetSearchInformation(AgentSearchInfo searchInfo);
        List<PeaInfo> FindAndDisplayBranchByKeyword(string keyword, string branchId);
        BindingList<LineInfo> FindAndDisplayLineByKeyword(LineSearchBoxInfo searchInfo);
    }
}
