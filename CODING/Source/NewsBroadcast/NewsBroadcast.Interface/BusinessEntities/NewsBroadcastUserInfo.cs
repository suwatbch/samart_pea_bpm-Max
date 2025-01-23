using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.NewsBroadcast.Interface.BusinessEntities
{

    [DataContract]
    public class NewsBroadcastUserInfo
    {
        private int broadcastId;
        private string userId;
        private bool isRead;
        private bool isOpened;
        private string areaId;
        private string branchId;
        private string branchName2;
        private string roleId;
        private string roleName;
        private DateTime readDt;
        private DateTime openedDt;
        private DateTime modifiedDt;
        private bool active;


        [DataMember(Order=1)]
        public int BroadcastId
        {
            get { return broadcastId; }
            set { broadcastId = value; }
        }

        [DataMember(Order=2)]
        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        [DataMember(Order=3)]
        public bool IsRead
        {
            get { return isRead; }
            set { isRead = value; }
        }

        [DataMember(Order=4)]
        public bool IsOpened
        {
            get { return isOpened; }
            set { isOpened = value; }
        }

        [DataMember(Order=5)]
        public string AreaId
        {
            get { return areaId; }
            set { areaId = value; }
        }

        [DataMember(Order=6)]
        public string BranchId
        {
            get { return branchId; }
            set { branchId = value; }
        }

        [DataMember(Order=7)]
        public string BranchName2
        {
            get { return branchName2; }
            set { branchName2 = value; }
        }

        [DataMember(Order=8)]
        public string RoleId
        {
            get { return roleId; }
            set { roleId = value; }
        }

        [DataMember(Order=9)]
        public string RoleName
        {
            get { return roleName; }
            set { roleName = value; }
        }

        [DataMember(Order=10)]
        public DateTime ReadDt
        {
            get { return readDt; }
            set { readDt = value; }
        }

        [DataMember(Order=11)]
        public DateTime OpenedDt
        {
            get { return openedDt; }
            set { openedDt = value; }
        }


        [DataMember(Order=12)]
        public DateTime ModifiedDt
        {
            get { return modifiedDt; }
            set { modifiedDt = value; }
        }


        [DataMember(Order=13)]
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

    }
}
