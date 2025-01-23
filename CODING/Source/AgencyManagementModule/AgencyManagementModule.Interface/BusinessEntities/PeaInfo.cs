using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    /// <summary>
    /// Find out what detail of the SAP - Portion/MRU that suitable to be seach keys
    /// </summary>
    [DataContract]
    public class PeaInfo
    {
        private string _id;
        private string _name;
        private string _address;
        private string _branchNo;
        private string _branchLevel;
        private int _numOfLines;


        [DataMember(Order=1)]
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }


        [DataMember(Order=2)]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
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
        public int NumOfLines
        {
            get { return _numOfLines; }
            set { _numOfLines = value; }
        }

    }
}
