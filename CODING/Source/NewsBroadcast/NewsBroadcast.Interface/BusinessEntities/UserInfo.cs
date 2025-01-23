using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.NewsBroadcast.Interface.BusinessEntities
{

    [DataContract]
    public class UserInfo
    {
        private string userId;


        [DataMember(Order=1)]
        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        private string fullName;


        [DataMember(Order=2)]
        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }

        private string branchId;


        [DataMember(Order=3)]
        public string BranchId
        {
            get { return branchId; }
            set { branchId = value; }
        }

        private string postBranchServerId;


        [DataMember(Order=4)]
        public string PostBranchServerId
        {
            get { return postBranchServerId; }
            set { postBranchServerId = value; }
        }

    }
}
