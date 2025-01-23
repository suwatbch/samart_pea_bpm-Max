using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Apm2.Interface.BusinessEntities
{
    [DataContract]
    public class Credential
    {
        [DataMember(Order = 1)]
        public string Status { set; get; }
        [DataMember(Order = 2)]
        public string Token { set; get; }

    }
}
