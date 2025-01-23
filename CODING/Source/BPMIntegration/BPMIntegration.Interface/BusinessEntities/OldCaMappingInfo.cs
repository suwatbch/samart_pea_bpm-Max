using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class OldCaMappingInfo
    {
        private string _OldCaID;
        private string _NewCaID;
        private DateTime? _ModifiedDt;

        [DataMember(Order = 1)]
        public string OldCaID
        {
            get { return _OldCaID; }
            set { _OldCaID = value; }
        }
        [DataMember(Order = 2)]
        public string NewCaID
        {
            get { return _NewCaID; }
            set { _NewCaID = value; }
        }
        [DataMember(Order = 3)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }

    }
}
