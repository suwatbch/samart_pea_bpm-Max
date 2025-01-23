using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities
{
    [DataContract, Serializable]
    public class Service
    {
        private string _id;
        private string _name;


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

        public Service() { }

        public Service(string id, string name)
        {
            _id = id;
            _name = name;
        }
    }
}
