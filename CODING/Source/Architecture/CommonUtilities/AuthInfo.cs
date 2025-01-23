using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Services.Protocols;
using ProtoBuf;

namespace PEA.BPM.Architecture.CommonUtilities
{
    [ProtoContract]
    public class AuthInfo
    {
        [ProtoMember(1)]
        public string UserId { get; set; }

        [ProtoMember(2)]
        public string AuthToken { get; set; }
    
        [ProtoMember(3)]
        public string LocalIP { get; set; }
    }
}
