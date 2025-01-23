using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports
{
    [DataContract]
    public class RE03ParamInfo
    {
        private string _accountClassId;
        private DateTime? _payDt;
        private string _runningBranchId;


        [DataMember(Order=1)]
        public string AccountClassId
        {
            get { return this._accountClassId; }
            set { this._accountClassId = value; }
        }


        [DataMember(Order=2)]
        public DateTime? PayDt
        {
            get { return this._payDt; }
            set { this._payDt = value; }
        }


        [DataMember(Order=3)]
        public string RunningBranchId
        {
            get { return this._runningBranchId; }
            set { this._runningBranchId = value; }
        }
    }
}
