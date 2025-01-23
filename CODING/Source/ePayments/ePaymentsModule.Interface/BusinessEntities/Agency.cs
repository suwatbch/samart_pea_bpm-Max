using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities
{
    [DataContract]
    public class Agency
    {
        private string _agencyId;
        private string _agencyName;
        private string _address;


        [DataMember(Order=1)]
        public string AgencyId
        {
            get { return this._agencyId; }
            set { this._agencyId = value; }
        }


        [DataMember(Order=2)]
        public string AgencyName
        {
            get { return this._agencyName; }
            set { this._agencyName = value; }
        }


        [DataMember(Order=3)]
        public string Address
        {
            get { return this._address; }
            set { this._address = value; }
        }
    }
}
