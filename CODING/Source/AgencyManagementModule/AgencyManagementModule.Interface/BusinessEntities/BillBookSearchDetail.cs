using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class BillBookSearchDetail
    {
        private string _lineId;
        private string _customerNo;
        private string _billPeriod;
        private double _electricCost;
        private string _callingType;     


        [DataMember(Order=1)]
        public string LineId
        {
            get { return _lineId; }
            set { _lineId = value; }
        }


        [DataMember(Order=2)]
        public string CustomerNo
        {
            get { return _customerNo; }
            set { _customerNo = value; }
        }


        [DataMember(Order=3)]
        public string BillPeriod
        {
            get { return _billPeriod; }
            set { _billPeriod = value; }
        }


        [DataMember(Order=4)]
        public double ElectricCost
        {
            get { return _electricCost; }
            set { _electricCost = value; }
        }


        [DataMember(Order=5)]
        public string CallingType
        {
            get { return _callingType; }
            set { _callingType = value; }
        }
    }
}
