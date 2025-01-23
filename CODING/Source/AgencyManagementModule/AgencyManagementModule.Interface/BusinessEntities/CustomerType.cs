using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CustomerType
    {
        private int _id;
        private string _name;


        [DataMember(Order=1)]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }


        [DataMember(Order=2)]
        public string TypeName
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}
