using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using PEA.BPM.WebService.Security.Core;

namespace PEA.BPM.WebService.Security
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
        public string CheckLogInDouble(string userid, string terminalip, int latency, int retrycount)
        {
            string clientip = this.Context.Request.UserHostAddress;
            string res = AuthenticationController.Instance.CheckLoginDouble(clientip, userid, terminalip, latency, retrycount);
            return res;
        }

        [WebMethod]
        public string UpdateCurIPReqFlag(string userid, string terminalip, string reqflag)
        {
            AuthenticationController.Instance.UpdateCurIPReqFlag(userid, terminalip, reqflag);
            return "";
        }

        //[WebMethod]
        //public string IsAuthenticated(string userid, string hashpassword, string postedserverid)
        //{
        //    string res = AuthenticationController.Instance.IsAuthenticated(userid, hashpassword, postedserverid);
        //    return res;
        //}

        //[WebMethod]
        //public string IsAuthenticatedWithBranchID(string userid, string hashpassword, string regbranchid, string postedserverid)
        //{
        //    string res = AuthenticationController.Instance.IsAuthenticated(userid, hashpassword, regbranchid, postedserverid);
        //    return res;
        //}

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

        //[WebMethod]
        //public UserProfile LoadUserProfile(string userid)
        //{
        //    return AuthenticationController.Instance.LoadUserProfile(userid);
        //}


    }
}
