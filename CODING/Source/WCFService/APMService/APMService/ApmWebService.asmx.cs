using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using PEA.BPM.ApmModule.BS;
using PEA.BPM.ApmModule.Interface.BusinessEntities;
//using PEA.BPM.ApmModule.Interface.BusinessEntities.ReceiptPrinting;
using PEA.BPM.Architecture.CommonUtilities;

namespace ApmService
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ApmWebService : BaseWebService//System.Web.Services.WebService
    {


        private ApmBS _ApmService;

        public ApmWebService()
        {
            _ApmService = new ApmBS();
        }

        [WebMethod]
        public Credential Login(string userId, string password, string localIP)
        {
            try
            {
                return _ApmService.Login(userId, password, localIP);
            }
            catch
            {
                throw new Exception("Not Login");
            }
        }

 
        [SoapHeader("AuthInfoValue"), WebMethod]
        public List<SearchInvoiceInfo> SearchInvoiceByCaId(string caId, string TerminalId)
        {
            try
            {
                //WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);
                _ApmService.CheckToken(this.AuthInfoValue.UserId,this.AuthInfoValue.AuthToken);
                return _ApmService.SearchInvoiceByCaId(caId.Trim(), TerminalId.Trim());
            }
            catch
            {
                throw new Exception("Token Not Match");
            }
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public List<PrintInvoiceInfo> UpdatePaymentStatus(string caId, string InvoiceNo, string TerminalId)
        {
            try
            {
                //WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);
                _ApmService.CheckToken(this.AuthInfoValue.UserId, this.AuthInfoValue.AuthToken);
                return _ApmService.UpdatePaymentStatus(caId.Trim(),InvoiceNo.Trim(), TerminalId.Trim());
            }
            catch
            {
                throw new Exception("Token Not Match");
            }
        }
      


    }
}
