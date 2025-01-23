using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ToolModule.Interface.BusinessEntities
{
    /// <summary>
    /// Find out what detail of the SAP - Portion/MRU that suitable to be seach keys
    /// </summary>
    [DataContract]
    public class PeaInfo
    {
        private string _branchId;
        
        private string _branchName;
        private string _branchName2;
        private string _address;
        private string _branchNo;
        private string _branchLevel;
        private string _baCode;


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
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }


        [DataMember(Order=4)]
        public string BranchNo
        {
            get { return _branchNo; }
            set { _branchNo = value; }
        }


        [DataMember(Order=5)]
        public string BranchLevel
        {
            get { return _branchLevel; }
            set { _branchLevel = value; }
        }


        [DataMember(Order=6)]
        public string BACode
        {
            get { return  _baCode; }
            set { _baCode = value; }
        }

        [DataMember(Order = 7)]
        public string BranchName2
        {
            get { return _branchName2; }
            set { _branchName2 = value; }
        }
      
    }
}
