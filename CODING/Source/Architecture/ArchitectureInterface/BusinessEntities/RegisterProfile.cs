using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities
{
    [DataContract]
    public class RegisterProfile
    {
        private string _branchId;
        private string _branchName;
        private string _branchName2;
        private string _branchLevel;
        private string _address;
        private string _branchNo;
        private string _businessArea;
        private string _peaTaxCode;
        private string _transUri;
        private string _reportUri;
        private string _backupUri;

        public RegisterProfile() { }

        [DataMember(Order = 1)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }


        [DataMember(Order = 2)]
        public string BranchName
        {
            get { return _branchName; }
            set { _branchName = value; }
        }


        [DataMember(Order = 3)]
        public string BranchName2
        {
            get { return _branchName2; }
            set { _branchName2 = value; }
        }


        [DataMember(Order = 4)]
        public string BranchLevel
        {
            get { return _branchLevel; }
            set { _branchLevel = value; }
        }

        [DataMember(Order = 5)]
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        [DataMember(Order = 6)]
        public string BranchNo
        {
            get { return _branchNo; }
            set { _branchNo = value; }
        }

        [DataMember(Order = 7)]
        public string BusinessArea
        {
            get { return _businessArea; }
            set { _businessArea = value; }
        }

        [DataMember(Order = 8)]
        public string PeaTaxCode
        {
            get { return _peaTaxCode; }
            set { _peaTaxCode = value; }
        }

        [DataMember(Order = 9)]
        public string TransUri
        {
            get { return _transUri; }
            set { _transUri = value; }
        }

        [DataMember(Order = 10)]
        public string ReportUri
        {
            get { return _reportUri; }
            set { _reportUri = value; }
        }

        [DataMember(Order = 11)]
        public string BackupUri
        {
            get { return _backupUri; }
            set { _backupUri = value; }
        }
    }
}
