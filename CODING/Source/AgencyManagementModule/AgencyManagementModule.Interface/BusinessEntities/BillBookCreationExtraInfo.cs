using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class BillBookCreationExtraInfo
    {
        private string _peaCode;
        private string _lineId;
        private string _caId;
        private string _filterType;


        [DataMember(Order=1)]
        public string NPeaCode
        {
            set { _peaCode = value; }
            get { return _peaCode; }
        }


        [DataMember(Order=2)]
        public string NLineId
        {
            set { _lineId = value; }
            get { return _lineId; }
        }


        [DataMember(Order=3)]
        public string Number
        {
            set { _caId = value; }
            get { return _caId; }
        }


        [DataMember(Order=4)]
        public string FilterType
        {
            set { _filterType = value; }
            get { return _filterType; }
        }


    }
}
