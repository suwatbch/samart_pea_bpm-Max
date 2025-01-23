using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;


using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using System.ComponentModel;

namespace PEA.BPM.AgencyManagementModule.Interface.Services
{
     public interface IAgencyCommonService
    {
        AgentInfo FindAndDisplayAgentSearchInfo(string agentId);
        PeaInfo FindAndDisplayBranchSearchInfo(string basedBranchId, string branchId);
        BindingList<LineInfo> FindAndDisplayLineSearchInfo(string branchId, string lineKey);
        HashInfoCollection GetPmList(string pmId);
        HashInfoCollection GetAbsList(string absId);
        string GetContractTypeList(string ctId);
        HashInfoCollection GetBillStatusList(string bsId);
        List<PeaInfo> GetBranches(string branchId); //all branch
    }
}
