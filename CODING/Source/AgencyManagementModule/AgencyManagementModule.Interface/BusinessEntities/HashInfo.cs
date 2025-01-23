using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class HashInfo
    {
        private string _id;
        private string _value;


        [DataMember(Order=1)]
        public string  Id
        {
            get { return this._id; }
            set { this._id = value; }
        }


        [DataMember(Order=2)]
        public string Value
        {
            get { return this._value; }
            set { this._value = value; }
        }      
    }
}
