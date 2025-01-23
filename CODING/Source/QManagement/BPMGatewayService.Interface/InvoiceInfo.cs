using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BPMGatewayService.Interface
{
    [DataContract]
    public class InvoiceInfo
    {
        [DataMember(Order = 1)]
        public string ContractAccountId { set; get; }
        [DataMember(Order = 2)]
        public string ContractAccountName { set; get; }
        [DataMember(Order = 3)]
        public string ContractAccountAddress { set; get; }
        [DataMember(Order = 4)]
        public string DebtTypeName { set; get; }
        [DataMember(Order = 5)]
        public string InvoicePeriod { set; get; }
        [DataMember(Order = 6)]
        public DateTime PaymentDueDate { set; get; }
        [DataMember(Order = 7)]
        public decimal GrandAmount { set; get; }
    }
}
