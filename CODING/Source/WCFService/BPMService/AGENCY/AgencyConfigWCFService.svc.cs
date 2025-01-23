using PEA.BPM.AgencyManagementModule.BS;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.ArchitectureBS;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.WebService;
using WCFExtras.Soap;


namespace BPMService.AGENCY
{

    public class AgencyConfigWCFService : IAgencyConfigWCFService
    {
        private AgencyModuleConfigService _agencyModuleConfigService;
        private AuthInfo authInfo = SoapHeaderHelper<AuthInfo>.GetInputHeader("AuthInfoValue");

        public AgencyConfigWCFService()
        {
            _agencyModuleConfigService = new AgencyModuleConfigService();
        }

        public FeeBaseElement GetBaseCommissionRate(string branchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "AgencyConfigWCFService", "GetBaseCommissionRate",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _agencyModuleConfigService.GetBaseCommissionRate(branchId);
                });

        }

        public bool UpdateCommissionRate(CompressData feeBase)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "AgencyConfigWCFService", "UpdateCommissionRate",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _agencyModuleConfigService.UpdateCommissionRate(ServiceHelper.DecompressData<FeeBaseElement>(feeBase));
                });
        }

    }
}
