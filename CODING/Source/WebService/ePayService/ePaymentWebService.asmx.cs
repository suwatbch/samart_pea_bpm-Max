using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using PEA.BPM.ePaymentsModule.BS;
using System.ComponentModel;
using System.Collections.Generic;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities.ReceiptPrinting;
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
        private BillingBS _ePaymentBillingService;

        public ePaymentWebService()
        {
            _ePaymentCommonService = new CommonBS();
            _ePaymentBillingService = new BillingBS();
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public string VerifyContractorService(string caId, string period, decimal debtAmount)
        {
            try
            {
                WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);
                return _ePaymentCommonService.VerifyContractorService(caId.Trim(), period, debtAmount);
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

        [SoapHeader("AuthInfoValue"), WebMethod]
        public string UpdateBillMarkFlagService(string caId, string invoiceNo, string modifiedBy, string postServerId)
        {
            try
            {
                WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);
                string pId = PEA.BPM.Architecture.CommonUtilities.Session.Branch.PostedServerId;
                return _ePaymentBillingService.UpdateBillMarkFlagService(caId.Trim(), invoiceNo.Trim(), modifiedBy, pId);
            }
            catch
            {
                throw;
            }
        }

    }
}