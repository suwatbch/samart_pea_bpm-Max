using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    /// <summary>
    /// Fill customer information that belong to this agent
    /// Add more properties when we are sure on the requirement
    /// </summary>
    [DataContract]
    public class CustomerDetail
    {
        private string _id;
        private string _name;
        private string _address;


        [DataMember(Order=1)]
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }


        [DataMember(Order=2)]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        [DataMember(Order=3)]
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

       
    }
}
