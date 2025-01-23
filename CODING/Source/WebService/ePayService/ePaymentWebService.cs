using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using PEA.BPM.ePaymentsModule.BS;
using System.ComponentModel;
using System.Collections.Generic;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;


namespace PEA.BPM.ePayService
{
    /// <summary>
    /// Summary description for ePaymentWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ePaymentWebService : BaseWebService
    {
        private CommonBS _ePaymentCommonService;

        public ePaymentWebService()
        {
            _ePaymentCommonService = new CommonBS();
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public string VerifyContractorService(string caId, string period, decimal debtAmount)
        {
            try
            {
                WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);
                return _ePaymentCommonService.VerifyContractorService(caId, period, debtAmount);
            }
            catch
            {
                throw;
            }
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public List<SearchContractorInfo> SearchContractorService(string caId, string billFlag)
        {
            try
            {
                WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);
                return _ePaymentCommonService.SearchContractorService(caId.Trim(), billFlag.Trim());
            }
            catch
            {
                throw;
            }
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public List<SearchConAccountInfo> SearchConAccountService(string caId)
        {
            try
            {
                WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);
                return _ePaymentCommonService.SearchConAccountService(caId.Trim());
            }
            catch
            {
                throw;
            }
        }
    }
}