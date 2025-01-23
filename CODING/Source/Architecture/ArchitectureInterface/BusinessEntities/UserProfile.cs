using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities
{
    [DataContract, Serializable]
    public class UserProfile
    {
        private string _userId;
        private string _name;
        private string _department;
        private DateTime? _loginTime;

        public UserProfile() { }

        public UserProfile(string userId, string name, string department, DateTime loginTime)
        {
            _userId = userId;
            _name = name;
            _department = department;
            _loginTime = loginTime;
        }


        [DataMember(Order=1)]
        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }


        [DataMember(Order=2)]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        [DataMember(Order=3)]
        public string Department
        {
            get { return _department; }
            set { _department = value; }
        }


        [DataMember(Order=4)]
        public DateTime? LoginTime
        {
            get { return _loginTime; }
            set { _loginTime = value; }
        }
    }
}
