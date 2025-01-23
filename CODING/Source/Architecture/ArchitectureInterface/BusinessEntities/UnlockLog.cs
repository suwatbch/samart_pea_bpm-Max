using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities
{
    [DataContract]
    public class UnlockLog
    {
        private string _functionId;


        [DataMember(Order=1)]
        public string FunctionId
        {
            get { return _functionId; }
            set { _functionId = value; }
        }

        private string _currentUserId;


        [DataMember(Order=2)]
        public string CurrentUserId
        {
            get { return _currentUserId; }
            set { _currentUserId = value; }
        }

        private string _unlockUserId;


        [DataMember(Order=3)]
        public string UnlockUserId
        {
            get { return _unlockUserId; }
            set { _unlockUserId = value; }
        }

        private string _description;


        [DataMember(Order=4)]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private string _remark;


        [DataMember(Order=5)]
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        private string _branchId;

        [DataMember(Order=6)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }            
        }

        private string _branchName;

        [DataMember(Order = 7)]
        public string BranchName
        {
            get { return _branchName; }
            set { _branchName = value; }
        }
    }
}
