using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using PEA.BPM.OneTouchModule.BS;
using PEA.BPM.OneTouchModule.Interface.BusinessEntities;
//using PEA.BPM.OneTouchModule.Interface.BusinessEntities.ReceiptPrinting;
using PEA.BPM.Architecture.CommonUtilities;

namespace OneTouchService
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class OneTouchWebService : BaseWebService//System.Web.Services.WebService
    {


        private OneTouchBS _OneTouchService;

        public OneTouchWebService()
        {
            _OneTouchService = new OneTouchBS();
        }

        [WebMethod]
        public Credential Login(string userId, string password, string localIP)
        {
            try
            {
                return _OneTouchService.Login(userId, password, localIP);
            }
            catch
            {
                throw new Exception("Not Login");
            }
        }

 
        //[SoapHeader("AuthInfoValue"), WebMethod]
        [WebMethod]
        public List<OneTouchInfo> SearchPayment(string NotificationNo)
        {
            try
            {
                //WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);
                //_OneTouchService.CheckToken(this.AuthInfoValue.UserId,this.AuthInfoValue.AuthToken);
                return _OneTouchService.SearchPayment(NotificationNo);
            }
            catch
            {
                throw new Exception("Token Not Match");
            }
        }

        //[SoapHeader("AuthInfoValue"), WebMethod]
        [WebMethod]
        public string UpdatePayment(string NotificationNo, string InvoiceNo, string ReceiptId, string DebtId)
        {
            try
            {
                //WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);
                //_OneTouchService.CheckToken(this.AuthInfoValue.UserId, this.AuthInfoValue.AuthToken);
                return _OneTouchService.UpdatePayment(NotificationNo.Trim(), InvoiceNo.Trim(), ReceiptId.Trim(), DebtId.Trim());
            }
            catch
            {
                throw new Exception("Token Not Match");
            }
        }
      


    }
}
