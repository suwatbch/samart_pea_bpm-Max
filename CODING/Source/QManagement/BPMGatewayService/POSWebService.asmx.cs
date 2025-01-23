using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;


namespace PEA.BPM.BPMGatewayService
{
    /// <summary>
    /// Summary description for BPMGatewayService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class POSWebService : BaseWebService
    {
        private BillingWrapper _bs;

        public POSWebService()
        {
            _bs = new BillingWrapper();
        }

        [WebMethod]
        public Credential Login(string userId, string password)
        {
            try
            {
                return _bs.Login(userId, password);
            }
            catch
            {
                throw;
            }
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public List<InvoiceInfo> SearchInvoiceByContractAccount(string contractAccount)
        {
            try
            {
                WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);
                return _bs.SearchInvoiceByCustomerId(contractAccount);
            }
            catch
            {
                throw;
            }
        }
    }
}