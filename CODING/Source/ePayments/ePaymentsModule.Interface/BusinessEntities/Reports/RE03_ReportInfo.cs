using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports
{
    [DataContract]
    public class RE03_ReportInfo
    {
        private string _accountClassId;
        private string _accountClassName;
        private string _branchId;
        private string _branchName;
        private int? _caNumber;
        private decimal? _amount;
        private string _branchGroup;


        [DataMember(Order=1)]
        public string AccountClassId
        {
            get { return this._accountClassId; }
            set { this._accountClassId = value; }
        }


        [DataMember(Order=2)]
        public string AccountClassName
        {
            get { return this._accountClassName; }
            set { this._accountClassName = value; }
        }


        [DataMember(Order=3)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }


        [DataMember(Order=4)]
        public string BranchName
        {
            get { return this._branchName; }
            set { this._branchName = value; }
        }


        [DataMember(Order=5)]
        public int? CaNumber
        {
            get { return this._caNumber; }
            set { this._caNumber = value; }
        }


        [DataMember(Order=6)]
        public decimal? Amount
        {
            get { return this._amount; }
            set { this._amount = value; }
        }


        [DataMember(Order=7)]
        public string BranchGroup
        {
            get { return this._branchGroup; }
            set { this._branchGroup = value; }
        }
    }
}
