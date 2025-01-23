using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CalculatedCommissionInfo
    {
        private string _cmId;
        private string _calCount;
      

        [DataMember(Order=1)]
        public string CmId
        {
            set { _cmId = value; }
            get { return _cmId; }
        }


        [DataMember(Order=2)]
        public string CalCount
        {
            set { _calCount = value; }
            get { return _calCount; }
        }

    }
}
