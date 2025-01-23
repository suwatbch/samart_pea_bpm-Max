using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using PEA.BPM.ThirdParty.WebService.Security.Core;


namespace PEA.BPM.ThirdParty.WebService.Security
{
	/// <summary>
    /// Summary description for AuthenticationWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class AuthenticationWebService : System.Web.Services.WebService
    {  
        [WebMethod]
        public string CheckToken(string userid, string token)
        {
            string res = AuthenticationController.Instance.CheckToken(userid, token);
            return res;
        }
        [WebMethod]
        public string GetToken(string userid, string hashpwd)
        {
            string res = AuthenticationController.Instance.GetToken(userid, hashpwd);
            return res;
        }
    }
}
