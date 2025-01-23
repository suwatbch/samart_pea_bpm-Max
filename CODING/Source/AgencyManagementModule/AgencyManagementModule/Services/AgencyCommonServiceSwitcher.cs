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
    [Service(typeof(IAgencyCommonService))]
    public class AgencyCommonServiceSwitcher : IAgencyCommonService
    {
        public AgencyCommonServiceSwitcher()
		{
        }

        #region Service Factory
        public IAgencyCommonService GetService()
        {
            return GetService(false);
        }

        public IAgencyCommonService GetService(bool serverService)
        {
            if (serverService || Session.Branch.OnlineConnection)
            {
                return new AgencyCommonSG(true);
            }
            else
            {
                return new AgencyCommonSG(false);
            }
        }
        #endregion

        #region IAgencyCommonService Members

        public AgentInfo FindAndDisplayAgentSearchInfo(string agentId)
        {
            return GetService().FindAndDisplayAgentSearchInfo(agentId);
        }

        public PeaInfo FindAndDisplayBranchSearchInfo(string basedBranchId, string branchId)
        {
            return GetService().FindAndDisplayBranchSearchInfo(basedBranchId, branchId);
        }

        public System.ComponentModel.BindingList<LineInfo> FindAndDisplayLineSearchInfo(string branchId, string lineKey)
        {
            return GetService().FindAndDisplayLineSearchInfo(branchId, lineKey);
        }

        public HashInfoCollection GetPmList(string pmId)
        {
            return GetService().GetPmList(pmId);
        }

        public HashInfoCollection GetAbsList(string absId)
        {
            return GetService().GetAbsList(absId);
        }

        public string GetContractTypeList(string ctId)
        {
            return GetService().GetContractTypeList(ctId);
        }

        public HashInfoCollection GetBillStatusList(string bsId)
        {
            return GetService().GetBillStatusList(bsId);
        }

        public List<PeaInfo> GetBranches(string branchId)
        {
            return GetService().GetBranches( branchId);
        }

        #endregion        
    }
}
