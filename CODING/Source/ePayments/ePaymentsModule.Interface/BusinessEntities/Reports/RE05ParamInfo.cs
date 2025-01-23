using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports
{
    [DataContract]
    public class RE05ParamInfo
    {
        private string _accountClassId;
        private string _companyId;
        private DateTime? _payDate;
        private string _runningBranchId;


        [DataMember(Order=1)]
        public string AccountClassId
        {
            get { return this._accountClassId; }
            set { this._accountClassId = value; }
        }


        [DataMember(Order=2)]
        public string CompanyId
        {
            get { return this._companyId; }
            set { this._companyId = value; }
        }


        [DataMember(Order=3)]
        public DateTime? PayDate
        {
            get { return this._payDate; }
            set { this._payDate = value; }
        }


        [DataMember(Order=4)]
        public string RunningBranchId
        {
            get { return this._runningBranchId; }
            set { this._runningBranchId = value; }
        }
    }
}
