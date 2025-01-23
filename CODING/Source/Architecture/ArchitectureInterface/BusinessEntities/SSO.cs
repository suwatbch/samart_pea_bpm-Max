using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities
{
    [DataContract, Serializable]
    public class SSOResponse
    {

        [DataMember(Order = 1)]
        public SSOResponseData Data { get; set; }

        [DataMember(Order = 2)]
        public bool Status { get; set; }

        [DataMember(Order = 3)]
        public string ErrorMessage { get; set; }

        [DataMember(Order = 4)]
        public string Message { get; set; }
    }

    [DataContract, Serializable]
    public class SSOResponseData
    {
        [DataMember(Order = 1)]
        public string SSOTransactionId { get; set; }

        [DataMember(Order = 2)]
        public bool Status1 { get; set; }

        [DataMember(Order = 3)]
        public bool Status2 { get; set; }

        [DataMember(Order = 4)]
        public string Message { get; set; }


    }
}
