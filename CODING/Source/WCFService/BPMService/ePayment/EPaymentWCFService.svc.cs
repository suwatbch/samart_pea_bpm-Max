using System.Collections.Generic;
using PEA.BPM.Architecture.ArchitectureBS;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities;
using PEA.BPM.WebService;
using WCFExtras.Soap;
using CommonBS = PEA.BPM.ePaymentsModule.BS.CommonBS;


namespace BPMService.ePayment
{

    public class EPaymentWCFService : IEPaymentWCFService
    {
        private CommonBS _ePaymentCommonService;
        private AuthInfo authInfo = SoapHeaderHelper<AuthInfo>.GetInputHeader("AuthInfoValue");

        public EPaymentWCFService()
        {
            _ePaymentCommonService = new CommonBS();      
        }

        public string VerifyContractorService(string caId, string period, decimal debtAmount)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "EPaymentWCFService", "VerifyContractorService",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _ePaymentCommonService.VerifyContractorService(caId, period, debtAmount);
                });
        }

        public List<SearchContractorInfo> SearchContractorService(string caId, string billFlag)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "EPaymentWCFService", "SearchContractorService",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _ePaymentCommonService.SearchContractorService(caId.Trim(), billFlag.Trim());
                });
        }

        public List<SearchConAccountInfo> SearchConAccountService(string caId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "EPaymentWCFService", "SearchConAccountService",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _ePaymentCommonService.SearchConAccountService(caId.Trim());
                });
        }

    }
}
