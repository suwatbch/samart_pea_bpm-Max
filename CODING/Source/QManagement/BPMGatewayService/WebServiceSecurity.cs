using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;
using System.Collections.Generic;
//using PEA.BPM.Architecture.ArchitectureBS;
using System.Security;
using System.Threading;
using System.Web.Services.Protocols;

/// <summary>
/// Summary description for WebServiceSecurity
/// </summary>
namespace PEA.BPM.BPMGatewayService
{
    public class WebServiceSecurity
    {
        private static WebServiceSecurity _instant = new WebServiceSecurity();
        //private SecurityBS _bs;

        private WebServiceSecurity()
        {
            //_bs = new SecurityBS();
        }

        public static WebServiceSecurity Instant()
        {
            return _instant;
        }

        public static void CheckAuthorization(AuthInfo authInfo)
        {
            if (authInfo == null) throw new SecurityException("NoToken");
            if (authInfo.UserId == Session.User.Id && authInfo.AuthToken == Session.User.Token.Id)
            {
            }
            else
            {
                throw new SecurityException("TokenNotMatch");
            }

            //_instant.CheckToken(authInfo.UserId, authInfo.AuthToken);

            //IIdentity identity = new GenericIdentity("WS-" + authInfo.UserId);
            //GenericPrincipal principal = new GenericPrincipal(identity, new string[] { });
            //Thread.CurrentPrincipal = principal;

            //StackTrace stackTrace = new StackTrace();
            //StackFrame stackFrame = stackTrace.GetFrame(1);
            //MethodBase methodBase = stackFrame.GetMethod();
        }

        //public void CheckToken(string userId, string token)
        //{
        //    string result = _bs.CheckToken(userId, token);

        //    switch (result)
        //    {
        //        case "NotFoundUser":
        //        case "TokenNotMatch":
        //        case "TokenExpired":
        //            throw new SecurityException(result);
        //        default:
        //            break;
        //    }
        //}
    }
}
