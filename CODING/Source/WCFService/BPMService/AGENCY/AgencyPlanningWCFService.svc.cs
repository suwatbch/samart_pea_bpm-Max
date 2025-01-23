using System.Collections.Generic;
using System.ComponentModel;
using PEA.BPM.AgencyManagementModule.BS;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.ArchitectureBS;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.WebService;
using WCFExtras.Soap;


namespace BPMService.AGENCY
{

    public class AgencyPlanningWCFService : IAgencyPlanningWCFService
    {
        private AgencyPlanningService _agencyPlanningService;
        private AuthInfo authInfo = SoapHeaderHelper<AuthInfo>.GetInputHeader("AuthInfoValue");

        public AgencyPlanningWCFService()
        {
            _agencyPlanningService = new AgencyPlanningService();
        }

        public bool SaveAssignedLineofAgent(CompressData asiLineList)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "AgencyPlanningWCFService", "SaveAssignedLineofAgent",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _agencyPlanningService.SaveAssignedLineofAgent(null, ServiceHelper.DecompressData<BindingList<LineInfo>>(asiLineList));
                });
        }

        public BindingList<LineInfo> FindAndDisplayLineOfAgentSearchInfo(string agentId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "AgencyPlanningWCFService", "FindAndDisplayLineOfAgentSearchInfo",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _agencyPlanningService.FindAndDisplayLineOfAgentSearchInfo(agentId);
                });
        }

        public CompressData FindAndDisplayLineOfAgentSearchInfo_Compress(string agentId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "AgencyPlanningWCFService", "FindAndDisplayLineOfAgentSearchInfo_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<BindingList<LineInfo>>(_agencyPlanningService.FindAndDisplayLineOfAgentSearchInfo(agentId));
                });
        }

        public List<AgentInfo> AcquireAgentAssetSearchInformation(AgentSearchInfo searchInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "AgencyPlanningWCFService", "AcquireAgentAssetSearchInformation",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _agencyPlanningService.AcquireAgentAssetSearchInformation(searchInfo);
                });
        }

        public CompressData AcquireAgentAssetSearchInformation_Compress(CompressData searchInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "AgencyPlanningWCFService", "AcquireAgentAssetSearchInformation_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<AgentInfo>>(_agencyPlanningService.AcquireAgentAssetSearchInformation(ServiceHelper.DecompressData<AgentSearchInfo>(searchInfo)));
                });
        }

        public List<PeaInfo> FindAndDisplayBranchByKeyword(string keyword, string branchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "AgencyPlanningWCFService", "FindAndDisplayBranchByKeyword",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _agencyPlanningService.FindAndDisplayBranchByKeyword(keyword, branchId);
                });
        }

        public CompressData FindAndDisplayBranchByKeyword_Compress(string keyword, string branchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "AgencyPlanningWCFService", "FindAndDisplayBranchByKeyword_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<PeaInfo>>(_agencyPlanningService.FindAndDisplayBranchByKeyword(keyword, branchId));
                });
        }

        public BindingList<LineInfo> FindAndDisplayLineByKeyword(LineSearchBoxInfo searchInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "AgencyPlanningWCFService", "FindAndDisplayLineByKeyword",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _agencyPlanningService.FindAndDisplayLineByKeyword(searchInfo);
                });
        }

        public CompressData FindAndDisplayLineByKeyword_Compress(CompressData searchInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "AgencyPlanningWCFService", "FindAndDisplayLineByKeyword_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<BindingList<LineInfo>>(_agencyPlanningService.FindAndDisplayLineByKeyword(ServiceHelper.DecompressData<LineSearchBoxInfo>(searchInfo)));
                });
        }

    }
}
