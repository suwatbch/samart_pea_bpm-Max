using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports
{
    [DataContract]
    public class APParam : ReportParam
    {

        [DataMember(Order=1)]
        public string BranchId;

        [DataMember(Order=2)]
        public string cashierId;

        [DataMember(Order=3)]
        public string posId;

        [DataMember(Order=4)]
        public DateTime? TransFromDate;

        [DataMember(Order=5)]
        public DateTime? TransToDate;
    }
}
