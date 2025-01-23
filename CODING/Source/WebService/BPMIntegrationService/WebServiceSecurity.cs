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
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using System.Collections.Generic;
using System.Security;
using System.Threading;
using PEA.BPM.Integration.BPMIntegration.Interface.Constants;

/// <summary>
/// Summary description for WebServiceSecurity
/// </summary>
namespace PEA.BPM.WebService.Integration
{    
    public class WebServiceSecurity
    {
        private static WebServiceSecurity _instant = new WebServiceSecurity();

        private WebServiceSecurity()
        {
        }

        public static void CheckAuthorization(AuthenInfo authInfo)
        {
            _instant.CheckToken(authInfo.UserId, authInfo.AuthToken);
        }

        public void CheckToken(string userId, string token)
        {
            if (WebServiceIntegrationToken.Value != token)
            {
                throw new SecurityException("Invalid Token");
            }
        }
    }
}
