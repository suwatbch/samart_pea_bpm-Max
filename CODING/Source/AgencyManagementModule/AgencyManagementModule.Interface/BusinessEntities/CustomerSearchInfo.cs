using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CustomerSearchInfo
    {
        private string _peaCode;
        private string _lineId;
        private string _customerId;


        [DataMember(Order=1)]
        public string PeaCode
        {
            set { _peaCode = value; }
            get { return _peaCode; }
        }


        [DataMember(Order=2)]
        public string LineId
        {
            set { _lineId = value; }
            get { return _lineId; }
        }


        [DataMember(Order=3)]
        public string CustomerId
        {
            set { _customerId = value; }
            get { return _customerId; }
        }
    }
}
