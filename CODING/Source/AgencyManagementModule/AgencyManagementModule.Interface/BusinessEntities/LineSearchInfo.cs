using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class LineSearchInfo
    {
        private string _peaCode;
        private string _lineId;
        private int _billMonth;
        private int _billRep;
        private int _billCall;


        [DataMember(Order=1)]
        public string PeaCode
        {
            get { return _peaCode; }
            set { _peaCode = value; }
        }


        [DataMember(Order=2)]
        public string LineId
        {
            get { return _lineId; }
            set { _lineId = value; }
        }


        [DataMember(Order=3)]
        public int BillMonth
        {
            get { return _billMonth; }
            set { _billMonth = value; }
        }


        [DataMember(Order=4)]
        public int BillRepeat
        {
            get { return _billRep; }
            set { _billRep = value; }
        }


        [DataMember(Order=5)]
        public int BillCalling
        {
            get { return _billCall; }
            set { _billCall = value; }
        }
    }
}
