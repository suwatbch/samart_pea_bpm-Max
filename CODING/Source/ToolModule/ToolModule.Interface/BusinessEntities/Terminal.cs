using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ToolModule.Interface.BusinessEntities
{
    [DataContract]
    public class Terminal
    {
        string _terminalId;
        string _terminalCode;        
        string _branchId;
        string _branchLevel;
        string _ip;
        string _branchName;
        string _branchName2;
        string _branchNo;
        string _branchAddress;        
        string _taxCode;
        string _baCode;
        DateTime? _modifiedDt;
        string _modifiedBy;
        char _active;



        [DataMember(Order=1)]
        public string TerminalId
        {
            get { return this._terminalId; }
            set { this._terminalId = value; }
        }

        [DataMember(Order=2)]
        public string TerminalCode
        {
            get { return this._terminalCode; }
            set { this._terminalCode = value; }
        }        

        [DataMember(Order=3)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }

        [DataMember(Order=4)]
        public string BranchLevel
        {
            get { return this._branchLevel; }
            set { this._branchLevel = value; }
        }

        [DataMember(Order=5)]
        public string BranchName
        {
            get { return this._branchName; }
            set { this._branchName = value; }
        }

        [DataMember(Order=6)]
        public string BranchNo
        {
            get { return this._branchNo; }
            set { this._branchNo = value; }
        }

        [DataMember(Order=7)]
        public string BranchAddress
        {
            get { return this._branchAddress; }
            set { this._branchAddress = value; }
        }

        [DataMember(Order=8)]
        public string TaxCode
        {
            get { return this._taxCode; }
            set { this._taxCode = value; }
        }


        [DataMember(Order=9)]
        public DateTime? ModifiedDt
        {
            get { return this._modifiedDt; }
            set { this._modifiedDt = value; }
        }

        [DataMember(Order=10)]
        public string ModifiedBy
        {
            get { return this._modifiedBy; }
            set { this._modifiedBy = value; }
        }

        [DataMember(Order=11)]
        public char Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        [DataMember(Order=12)]
        public string IP
        {
            get { return this._ip; }
            set { this._ip = value; }
        }


        [DataMember(Order=13)]
        public string BACode
        {
            get { return this._baCode; }
            set { this._baCode = value; }
        }

        [DataMember(Order = 14)]
        public string BranchName2
        {
            get { return this._branchName2; }
            set { this._branchName2 = value; }
        }
    }
}
