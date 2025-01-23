using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports
{

    [DataContract]
    public enum ReportName
    {
        [EnumMember]
        RE_01,
        [EnumMember]
        RE_02,
        [EnumMember]
        RE_03,
        [EnumMember]
        RE_04,
        [EnumMember]
        RE_05,
        [EnumMember]
        RE_06,
        [EnumMember]
        RE_07,
        [EnumMember]
        RE_08,
        [EnumMember]
        RE_09
    }

    [DataContract]
    public abstract class ReportParam
    {
        private ReportName _report;


        [DataMember(Order=1)]
        public ReportName Report
        {
            get { return _report; }
            set { _report = value; }
        }
    }
}
