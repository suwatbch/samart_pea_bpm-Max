using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Infrastructure.Interface.BusinessEntities
{
    [DataContract]
    public class Branch : IListItem, IComparable<Branch>
    {
        private string _branchId;
        private string _branchName;
        private string _branchName2;
        //private string _Address;
        private string _branchLevel;
        //private string _cdType;
        private string _parentBranchId;

        //public Branch(string branchId, string branchName, string address, string branchLevel, string cdType, string parentBranchId)
        //{
        //    this._branchId = branchId;
        //    this._branchName = branchName;
        //    this._Address = address;
        //    this._branchLevel = branchLevel;
        //    this._cdType = cdType;
        //    this._parentBranchId = parentBranchId;
        //}

        public Branch()
        {
        }

        public Branch(string branchId, string branchName)
        {
            this._branchId = branchId;
            this._branchName = branchName;           
        }

        public Branch(string branchId, string branchName, string branchName2)
        {
            this._branchId = branchId;
            this._branchName = branchName;
            this._branchName2 = branchName2;
        }

        public Branch(string branchId, string branchName, string branchName2, string branchLevel, string parentBranchId)
        {
            this._branchId = branchId;
            this._branchName = branchName;
            this._branchName2 = branchName2;
            this._branchLevel = branchLevel;
            this._parentBranchId = parentBranchId;
        }


        [DataMember(Order=1)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }


        [DataMember(Order=2)]
        public string BranchName
        {
            get { return _branchName; }
            set { _branchName = value; }
        }


        [DataMember(Order=3)]
        public string BranchName2
        {
            get { return _branchName2; }
            set { _branchName2 = value; }
        }

        //public string Address
        //{
        //    get { return _Address; }
        //    set { _Address = value; }
        //}


        [DataMember(Order=4)]
        public string BranchLevel
        {
            get { return _branchLevel; }
            set { _branchLevel = value; }
        }

        //public string CdType
        //{
        //    get { return _cdType; }
        //    set { _cdType = value; }
        //}


        [DataMember(Order=5)]
        public string ParentBranchId
        {
            get { return _parentBranchId; }
            set { _parentBranchId = value; }
        }

        #region IListItem Members


        //[DataMember(Order=6)]
        public string DisplayText
        {
            get { return string.Format("{0}-{1}", _branchId, _branchName); }
        }

        #endregion


        #region IComparable Members

        public int CompareTo(Branch other)
        {
            return string.Compare(this.BranchId, other.BranchId);
        }

        #endregion
    }
}
