using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports
{
    [DataContract]
    public class CAC01Param: ReportParam
    {

        [DataMember(Order=1)]
        public string BranchId;

        [DataMember(Order=2)]
        public string ControllerId;

        [DataMember(Order=3)]
        public DateTime? TransDate;

        [DataMember(Order=4)]
        public string FromTime;

        [DataMember(Order=5)]
        public string ToTime;

        [DataMember(Order=6)]
        public string BankKey;

        [DataMember(Order=7)]
        public string BankName;

        [DataMember(Order=8)]
        public DateTime? TransFromDate;

        [DataMember(Order=9)]
        public DateTime? TransToDate;
    }
}
