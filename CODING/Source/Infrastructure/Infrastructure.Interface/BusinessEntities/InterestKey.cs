using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Infrastructure.Interface.BusinessEntities
{
    [DataContract]
    public class InterestKey
    {
        private string _key;

        [DataMember(Order=1)]
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        private string _name;

        [DataMember(Order=2)]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private Decimal? _rate;

        [DataMember(Order=3)]
        public Decimal? Rate
        {
            get { return _rate; }
            set { _rate = value; }
        }

        public InterestKey(string key, string name, decimal? rate)
        {
            _key = key;
            _name = name;
            _rate = rate;
        }
    }
}
