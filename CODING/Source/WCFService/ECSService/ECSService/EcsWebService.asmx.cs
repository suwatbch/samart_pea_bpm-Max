using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using PEA.BPM.EcsModule.BS;
using PEA.BPM.EcsModule.Interface.BusinessEntities;
//using PEA.BPM.EcsModule.Interface.BusinessEntities.ReceiptPrinting;
using PEA.BPM.Architecture.CommonUtilities;
using System.Text;

namespace EcsService
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class EcsWebService : BaseWebService//System.Web.Services.WebService
    {
        private EcsBS _EcsService;

        public EcsWebService()
        {
            _EcsService = new EcsBS();
        }

        private bool VerifyUserAndPassword(string User, String Password)
        {
            if ((User == "00840001") && (Password == "123"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool VerifyTerminal(string Terminal)
        {
            if (Terminal == "51767")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        [WebMethod]
        public string UpdatePaymentStatus(string caId, string InvoiceNo, string TerminalId,string User,string Password)
        {
            if (VerifyUserAndPassword(User.Trim(), Password.Trim()) == false)
            {
                return "Invalid user or password.";
            }
            else if(VerifyTerminal(TerminalId.Trim())== false)
            {
                return "Invalid terminal id.";
            }
            else 
            {
                try
                {  
                    var result     = _EcsService.UpdatePaymentStatus(caId.Trim(), InvoiceNo.Trim(), TerminalId.Trim(), User.Trim(), Password.Trim());
                    if (result.Trim() == "Success")
                    {
                        return "Success";
                    }
                    else
                    {
                        return "Failure";
                    }
                }
                catch
                {
                    return "Failure";
                }
            }
            
        }
      
        [WebMethod]
        public string CancelPaymentStatus(string caId, string InvoiceNo, string TerminalId,string User,string Password)
        {
            if (VerifyUserAndPassword(User.Trim(), Password.Trim()) == false)
            {
                return "Invalid user or password.";
            }
            else if (VerifyTerminal(TerminalId.Trim()) == false)
            {
                return "Invalid terminal id.";
            }
            else 
            {
                try
                {
                    var result = _EcsService.CancelPaymentStatus(caId.Trim(), InvoiceNo.Trim(), TerminalId.Trim(), User.Trim(), Password.Trim());
                    if (result != null)
                    {
                        return "Success";
                    }
                    else
                    {
                        return "Failure";
                    }
                }
                catch
                {
                    return "Failure";
                }
            }
        }
    }
}
