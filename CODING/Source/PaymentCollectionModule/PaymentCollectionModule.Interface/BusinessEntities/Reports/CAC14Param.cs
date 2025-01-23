using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports
{
    [DataContract]
    public class CAC14Param : ReportParam
    {

        [DataMember(Order=1)]
        public string GroupInvoiceNo;

        [DataMember(Order=2)]
        public string PaymentId;

        [DataMember(Order=3)]
        public string ReceiptId;

        [DataMember(Order=4)]
        public string BranchId;
    }
}
