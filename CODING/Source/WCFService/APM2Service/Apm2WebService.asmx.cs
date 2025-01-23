using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using PEA.BPM.Apm2.BS;
using PEA.BPM.Apm2.Interface.BusinessEntities;
//using PEA.BPM.ApmModule.Interface.BusinessEntities.ReceiptPrinting;
using PEA.BPM.Architecture.CommonUtilities;

namespace Apm2Service
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Apm2WebService : BaseWebService//System.Web.Services.WebService
    {


        private Apm2BS _Apm2Service;

        public Apm2WebService()
        {
            _Apm2Service = new Apm2BS();
        }

        [WebMethod]
        public Credential Login(string userId, string password, string localIP)
        {
            try
            {
                return _Apm2Service.Login(userId, password, localIP);
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

                //_Apm2Service.CheckToken(this.AuthInfoValue.UserId,this.AuthInfoValue.AuthToken);
                return _Apm2Service.SearchInvoiceByCaId(caId.Trim(), TerminalId.Trim());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //[SoapHeader("AuthInfoValue"), WebMethod]
        [WebMethod]
        public List<PrintInvoiceInfo> UpdatePaymentStatus(string caId, string InvoiceNo, string TerminalId)
        {
            try
            {
                //WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);
                //TEMP NOT CHECK TOKEN UTHEN 2021_09_21
                //_Apm2Service.CheckToken(this.AuthInfoValue.UserId, this.AuthInfoValue.AuthToken);
                return _Apm2Service.UpdatePaymentStatus(caId.Trim(),InvoiceNo.Trim(), TerminalId.Trim());
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
      


    }
}
