using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using BPMInternalServicePools.BS;
using BPMInternalServicePools.Model;


namespace BPMInternalServicePools
{
    /// <summary>
    /// Summary description for WebBillingServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebBillingServices : System.Web.Services.WebService
    {

        [WebMethod]
        public List<WebBillingModel> GetElectricBillingByCaid(string caid)
        {
            var bs = new WebBillingBS();
            return bs.GetElectricBillingByCaid(caid);
        }
    }
}
