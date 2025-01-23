using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports
{
    //wait to verify by TongKung    
    [DataContract]
    public enum ReportName
    {
        [EnumMember]
        CAC01_1,
        [EnumMember]
        CAC01_2,
        [EnumMember]
        CAC03_1,
        [EnumMember]
        CAC03_2,
        [EnumMember]
        CAC03_3,
        [EnumMember]
        CAC03_4,
        [EnumMember]
        CAC04_1,
        [EnumMember]
        CAC04_2,
        [EnumMember]
        CAC04_3,
        [EnumMember]
        CAC04_4,
        [EnumMember]
        CAC05_1,
        [EnumMember]
        CAC05_2,
        [EnumMember]
        CAC06_1,
        [EnumMember]
        CAC06_2,
        [EnumMember]
        CAC06_3,
        [EnumMember]
        CAC07_1,
        [EnumMember]
        CAC07_2,
        [EnumMember]
        CAC09_1,
        [EnumMember]
        CAC09_2,
        [EnumMember]
        CAC10,
        [EnumMember]
        CAC11,
        [EnumMember]
        CAC12,
        [EnumMember]
        CAC13,
        [EnumMember]
        CAC14,
        [EnumMember]
        AP,
        [EnumMember]
        CAC15, //All Payment(+Offline)
        //TODO: INSTALLMENT CASE
        //[EnumMember]
        //CAC16
        [EnumMember]
        CAC18,
        [EnumMember]
        CAC19,
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
