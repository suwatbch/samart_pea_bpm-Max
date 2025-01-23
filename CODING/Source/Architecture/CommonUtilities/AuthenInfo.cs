using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services.Protocols;

namespace PEA.BPM.Architecture.CommonUtilities
{
    public class AuthenInfo : SoapHeader
    {
        public string UserId;
        public string AuthToken;
    }
}
