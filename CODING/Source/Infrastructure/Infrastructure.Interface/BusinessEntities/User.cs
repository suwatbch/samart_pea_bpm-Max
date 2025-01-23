using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Infrastructure.Interface.BusinessEntities
{
    [DataContract]
    public class User
    {
        private string _userId;
        private string _password;
        private string _fullName;
        private string _department;


        [DataMember(Order=1)]
        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }


        [DataMember(Order=2)]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
      

        [DataMember(Order=3)]
        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }


        [DataMember(Order=4)]
        public string Department
        {
            get { return _department; }
            set { _department = value; }
        }

        public User(string userId, string fullName, string department)
        {
            this._userId = userId;
            this._fullName = fullName;
            this._department = department;
        }
    }
}
