using System;
using System.Collections.Generic;
using PEA.BPM.Architecture.ArchitectureBS;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports;
using PEA.BPM.WebService;
using WCFExtras.Soap;
using CommonBS = PEA.BPM.ePaymentsModule.BS.CommonBS;


namespace BPMService.ePayment
{

    public class EPayCommonWCFService : IEPayCommonWCFService
    {
        private CommonBS _ePaymentCommonSV;
        private AuthInfo authInfo = SoapHeaderHelper<AuthInfo>.GetInputHeader("AuthInfoValue");

        public EPayCommonWCFService()
        {
            _ePaymentCommonSV = new CommonBS();
        }

        public CompressData GetAgencyAllService_Compress(Agency agency)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "EPayCommonWCFService", "GetAgencyAllService_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<Agency>>(_ePaymentCommonSV.GetAgencyAllService(agency));
                });
        }

        public CompressData GetAccountClassList_Compress(AccountClassInfo acParam)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "EPayCommonWCFService", "GetAccountClassList_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<AccountClassInfo>>(_ePaymentCommonSV.GetAccountClassList(acParam));
                });
        }

        public CompressData GetCompanyList_Compress(CompanyParamInfo acParam)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "EPayCommonWCFService", "GetCompanyList_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<Company>>(_ePaymentCommonSV.GetCompanyList(acParam));
                });
        }

        public CompressData GetCompanyByUploadDtList_Compress(DateTime? uploadDt)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "EPayCommonWCFService", "GetCompanyByUploadDtList_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<Company>>(_ePaymentCommonSV.GetCompanyByUploadDtList(uploadDt));
                });
        }


    }
}
