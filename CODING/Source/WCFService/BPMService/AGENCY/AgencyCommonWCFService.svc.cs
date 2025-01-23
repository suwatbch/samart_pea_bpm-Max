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

    public class AgencyCommonWCFService : IAgencyCommonWCFService
    {
        private AgencyCommonService _agencyCommonService;
        private AuthInfo authInfo = SoapHeaderHelper<AuthInfo>.GetInputHeader("AuthInfoValue");

        public AgencyCommonWCFService()
        {
            _agencyCommonService = new AgencyCommonService();
        }

        public AgentInfo FindAndDisplayAgentSearchInfo(string agentId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "AgencyCommonWCFService", "FindAndDisplayAgentSearchInfo",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _agencyCommonService.FindAndDisplayAgentSearchInfo(agentId);
                });
        }

        public CompressData FindAndDisplayAgentSearchInfo_Compress(string agentId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "AgencyCommonWCFService", "FindAndDisplayAgentSearchInfo_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<AgentInfo>(_agencyCommonService.FindAndDisplayAgentSearchInfo(agentId));
                });
        }

        public PeaInfo FindAndDisplayBranchSearchInfo(string basedBranchId, string branchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "AgencyCommonWCFService", "FindAndDisplayBranchSearchInfo",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _agencyCommonService.FindAndDisplayBranchSearchInfo(basedBranchId, branchId);
                });
        }

        public CompressData FindAndDisplayBranchSearchInfo_Compress(string basedBranchId, string branchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "AgencyCommonWCFService", "FindAndDisplayBranchSearchInfo_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<PeaInfo>(_agencyCommonService.FindAndDisplayBranchSearchInfo(basedBranchId, branchId));
                });
        }

        public BindingList<LineInfo> FindAndDisplayLineSearchInfo(string branchId, string lineKey)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "AgencyCommonWCFService", "FindAndDisplayLineSearchInfo",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _agencyCommonService.FindAndDisplayLineSearchInfo(branchId, lineKey);
                });
        }

        public CompressData FindAndDisplayLineSearchInfo_Compress(string branchId, string lineKey)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "AgencyCommonWCFService", "FindAndDisplayLineSearchInfo_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<BindingList<LineInfo>>(_agencyCommonService.FindAndDisplayLineSearchInfo(branchId, lineKey));
                });
        }

        public HashInfoCollection GetPmList(string pmId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "AgencyCommonWCFService", "GetPmList",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _agencyCommonService.GetPmList(pmId);
                });
        }

        public CompressData GetPmList_Compress(string pmId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "AgencyCommonWCFService", "GetPmList_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<HashInfoCollection>(_agencyCommonService.GetPmList(pmId));
                });
        }

        public HashInfoCollection GetAbsList(string absId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "AgencyCommonWCFService", "GetAbsList",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _agencyCommonService.GetAbsList(absId);
                });
        }

        public CompressData GetAbsList_Compress(string absId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "AgencyCommonWCFService", "GetAbsList_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<HashInfoCollection>(_agencyCommonService.GetAbsList(absId));
                });
        }

        public string GetContractTypeList(string ctId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "AgencyCommonWCFService", "GetContractTypeList",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _agencyCommonService.GetContractTypeList(ctId);
                });
        }

        public HashInfoCollection GetBillStatusList(string bsId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "AgencyCommonWCFService", "GetBillStatusList",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _agencyCommonService.GetBillStatusList(bsId);
                });
        }

        public CompressData GetBillStatusList_Compress(string bsId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "AgencyCommonWCFService", "GetBillStatusList_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<HashInfoCollection>(_agencyCommonService.GetBillStatusList(bsId));
                });
        }

        public List<PeaInfo> GetBranches(string branchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "AgencyCommonWCFService", "GetBranches",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _agencyCommonService.GetBranches(branchId);
                });
        }

        public CompressData GetBranches_Compress(string branchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "AgencyCommonWCFService", "GetBranches_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<PeaInfo>>(_agencyCommonService.GetBranches(branchId));
                });
        }

    }
}
