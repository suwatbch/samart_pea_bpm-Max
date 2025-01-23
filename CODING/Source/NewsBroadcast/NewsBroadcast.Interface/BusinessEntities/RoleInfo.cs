using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.NewsBroadcast.Interface.BusinessEntities
{

    [DataContract]
    public class RoleInfo
    {
        private string roleId;


        [DataMember(Order=1)]
        public string RoleId
        {
            get { return roleId; }
            set { roleId = value; }
        }

        private string roleName;


        [DataMember(Order=2)]
        public string RoleName
        {
            get { return roleName; }
            set { roleName = value; }
        }

        private string description;


        [DataMember(Order=3)]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }


    }
}
