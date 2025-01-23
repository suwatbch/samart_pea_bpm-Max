using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Infrastructure.Interface.BusinessEntities
{
    [DataContract]
    public class AppSetting
    {
        private string _code;

        [DataMember(Order=1)]
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        private string _module;

        [DataMember(Order=2)]
        public string Module
        {
            get { return _module; }
            set { _module = value; }
        }

        private string _value;

        [DataMember(Order=3)]
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public AppSetting() { }

        public AppSetting(string code, string module, string value)
        {
            _code = code;
            _module = module;
            _value = value;
        }
    }
}
