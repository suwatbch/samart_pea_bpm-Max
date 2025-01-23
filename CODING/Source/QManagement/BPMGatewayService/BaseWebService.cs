using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;
using System.Web.Services;
using System.Data;

namespace PEA.BPM.BPMGatewayService
{
    public class BaseWebService : WebService
    {
        public AuthInfo AuthInfoValue;
    }

    public class AuthInfo : SoapHeader
    {
        public string UserId;
        public string AuthToken;
    }


}
