using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports
{
    [DataContract]
    public class CAC11Param : ReportParam
    {

        [DataMember(Order=1)]
        public string BranchId;

        [DataMember(Order=2)]
        public string PosId;

        [DataMember(Order=3)]
        public string ControllerId;

        [DataMember(Order=4)]
        public DateTime? TransFromDate;

        [DataMember(Order=5)]
        public DateTime? TransToDate;
    }
}
