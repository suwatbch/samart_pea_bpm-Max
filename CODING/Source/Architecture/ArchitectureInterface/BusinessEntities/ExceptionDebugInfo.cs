using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities
{
    [DataContract]
    public class ExceptionDebugInfo
    {

        [DataMember(Order=1)]
        public string ErrorCode = "";

        [DataMember(Order=2)]
        public string DebuggingId = "";


        [DataMember(Order=3)]
        public string THMessage = "";

        [DataMember(Order=4)]
        public string Cause = "";

        [DataMember(Order=5)]
        public string Resolve = "";

        [DataMember(Order=6)]
        public string HelpURL = "";

        [DataMember(Order=7)]
        public bool CanContinue = true;
    }
}
