using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports
{
    [DataContract]
    public class CompanyParamInfo
    {
        private string _companyId = null;
        private string _accountClassId = null;
        private ReportName _targetReport;

         
        [DataMember(Order=1)]
        public string CompanyId
        {
            get { return this._companyId; }
            set { this._companyId = value; }
        }


        [DataMember(Order=2)]
        public string AccountClassId
        {
            get { return this._accountClassId; }
            set { this._accountClassId = value; }
        }


        [DataMember(Order=3)]
        public ReportName TargetReport
        {
            get { return this._targetReport; }
            set { this._targetReport = value; }
        }
    }
}
