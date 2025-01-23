using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class BranchIdInfoItem : IComparable<BranchIdInfoItem>
    {
        #region "Local Variable"
        private string _branchId;
        private string _branchName;
        private string _branchGroupId;
        private string _branchLevel;
        #endregion

        public BranchIdInfoItem()
        { 
        }
        public BranchIdInfoItem(string branchid, string branchName)
        {
            _branchId = branchid;
            _branchName = branchName;
        }

        #region "Property"

        [DataMember(Order=1)]
        public string BranchId
        {
            get { return _branchId; }
            set { this._branchId = value; }
        }

        [DataMember(Order=2)]
        public string BranchName
        {
            get { return this._branchName; }
            set { this._branchName = value; }
        }


        [DataMember(Order=3)]
        public string BranchGroupId
        {
            get { return this._branchGroupId; }
            set { this._branchGroupId = value; }
        }

        [DataMember(Order=4)]
        public string BranchLevel
        {
            get { return this._branchLevel; }
            set { this._branchLevel = value; }
        }

        #endregion

    
#region IComparable<BranchIdInfoItem> Members

public int  CompareTo(BranchIdInfoItem other)
{
    return BranchId.CompareTo(other.BranchId);
}

#endregion
}
}
