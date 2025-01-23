using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Infrastructure.Interface.BusinessEntities
{
    [DataContract]
    public class UnitType
    {
        private string _id;

        [DataMember(Order=1)]
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;

        [DataMember(Order=2)]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public UnitType(string id, string name)
        {
            _id = id;
            _name = name;
        }
    }
}
