using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ToolModule.Interface.BusinessEntities
{
    [DataContract]
    public class ReportUnlockingLog
    {
        string _branchId;
        string _branchName;
        string _unlockDt;
        string _currentUserId;
        string _unlockUserId;
        string _uDescription;
        string _remark;
        string _fncId;
        string _module;
        string _fncName;
        string _subFncName;
        string _fDescription;


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
        public string UnlockDt
        {
            get { return _unlockDt; }
            set { _unlockDt = value; }
        }
        

        [DataMember(Order=4)]
        public string CurrentUserId
        {
            get { return _currentUserId; }
            set { _currentUserId = value; }
        }
        

        [DataMember(Order=5)]
        public string UnlockUserId
        {
            get { return _unlockUserId; }
            set { _unlockUserId = value; }
        }
        

        [DataMember(Order=6)]
        public string UDescription
        {
            get { return _uDescription; }
            set { _uDescription = value; }
        }
        
        

        [DataMember(Order=7)]
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        

        [DataMember(Order=8)]
        public string FncId
        {
            get { return _fncId; }
            set { _fncId = value; }
        }
        

        [DataMember(Order=9)]
        public string Module
        {
            get { return _module; }
            set { _module = value; }
        }
        

        [DataMember(Order=10)]
        public string FncName
        {
            get { return _fncName; }
            set { _fncName = value; }
        }
        


        [DataMember(Order=11)]
        public string SubFncName
        {
            get { return _subFncName; }
            set { _subFncName = value; }
        }


        [DataMember(Order=12)]
        public string FDescription
        {
            get { return _fDescription; }
            set { _fDescription = value; }
        }        
    }
}
