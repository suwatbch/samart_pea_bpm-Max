using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;
using System.Web.Services;

namespace PEA.BPM.HelpDeskUnlockMarkFlagServices
{
    public class HelpDeskUnlockBaseWebService : WebService
    {
        public AuthInfo AuthInfoValue;
    }

    public class AuthInfo : SoapHeader
    {
        public string UserId;
        public string AuthToken;
    }
}
