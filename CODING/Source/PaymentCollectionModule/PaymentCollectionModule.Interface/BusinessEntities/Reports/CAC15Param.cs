using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports
{
    [DataContract]
    public class CAC15Param : ReportParam
    {

        [DataMember(Order=1)]
        public string BranchId;

        [DataMember(Order=2)]
        public string CaId;

        [DataMember(Order=3)]
        public string ReceiptId;

        [DataMember(Order=4)]
        public DateTime TransDate;
    }
}
