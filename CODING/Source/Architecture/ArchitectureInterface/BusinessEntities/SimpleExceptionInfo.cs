using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities
{
    [DataContract]
    public class SimpleExceptionInfo
    {

        [DataMember(Order=1)]
        public string Message = "";

        [DataMember(Order=2)]
        public string StackTrace = "";

        [DataMember(Order=3)]
        public string Additional = "";

        [DataMember(Order=4)]
        public string TypeName = "";
    }
}
