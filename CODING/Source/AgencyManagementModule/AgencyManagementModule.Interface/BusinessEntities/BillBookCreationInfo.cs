using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class BillBookCreationInfo
    {
        private string _peaCode;
        private string _lineId;
        private string _billPeriod;
        private string _callingBill;



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
        public string BillPeriod
        {
            set { _billPeriod = value; }
            get { return _billPeriod; }
        }


        [DataMember(Order=4)]
        public string CallingBill
        {
            set { _callingBill = value; }
            get { return _callingBill; }
        }


    }
}
