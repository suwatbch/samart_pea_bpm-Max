using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;
using System.Web.Services;

namespace Apm2Service
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
