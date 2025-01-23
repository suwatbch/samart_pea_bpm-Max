using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using PEA.BPM.QueueModule.BS;
using PEA.BPM.QueueModule.Interface.BusinessEntities;
//using PEA.BPM.QueueModule.Interface.BusinessEntities.ReceiptPrinting;
using PEA.BPM.Architecture.CommonUtilities;

namespace QueueService
{
    /// <summary>
    /// Summary description for QueueWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class QueueWebService : BaseWebService//System.Web.Services.WebService
    {

        private QueueBS _QueueService;

        public QueueWebService()
        {
            _QueueService = new QueueBS();
        }

        [WebMethod]
        public Credential Login(string userId, string password, string localIP)
        {
            try
            {
                return _QueueService.Login(userId, password, localIP);
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
                if (caId.Trim().Length < 12)
                    caId = caId.Trim().PadLeft(12, '0');
                //WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);
                _QueueService.CheckToken(this.AuthInfoValue.UserId, this.AuthInfoValue.AuthToken);
                return _QueueService.SearchInvoiceByCaId(caId.Trim(), TerminalId.Trim());
            }
            catch
            {
                throw new Exception("Token Not Match");
            }
        }
    }
}
