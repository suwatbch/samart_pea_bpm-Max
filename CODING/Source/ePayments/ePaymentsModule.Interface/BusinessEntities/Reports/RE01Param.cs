using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports
{
    [DataContract]
    public class RE01Param
    {
        private string _branchId;
        private string _accountClassId;
        private string _companyId;
        private DateTime? _uploadDt;
        private string _runningBranchId;


        [DataMember(Order=1)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }


        [DataMember(Order=2)]
        public string AccountClassId
        {
            get { return this._accountClassId; }
            set { this._accountClassId = value; }
        }


        [DataMember(Order=3)]
        public string CompanyId
        {
            get { return this._companyId; }
            set { this._companyId = value; }
        }


        [DataMember(Order=4)]
        public DateTime? UploadDt
        {
            get { return this._uploadDt; }
            set { this._uploadDt = value; }
        }


        [DataMember(Order=5)]
        public string RunningBranchId
        {
            get { return this._runningBranchId; }
            set { this._runningBranchId = value; }
        }
    }
}
