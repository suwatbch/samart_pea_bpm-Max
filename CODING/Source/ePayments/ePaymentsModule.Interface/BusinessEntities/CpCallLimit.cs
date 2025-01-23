using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.BPMOnlineCp.Interface.BusinessEntities
{
    [DataContract]
    public class CpCallLimit
    {
        private int _CallLimitPerSec;

        [DataMember(Order = 1)]
        public int CallLimitPerSec
        {
            get { return _CallLimitPerSec; }
            set { _CallLimitPerSec = value; }
        }
    }
}
