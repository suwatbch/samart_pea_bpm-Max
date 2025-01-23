using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPIMWcfService
{

    [DataContract]
    public class Payment
    {
        [DataMember]
        public string CaId { get; set; }

        [DataMember]
        public string CaName { get; set; }


        [DataMember]
        public string CaAddress { get; set; }

        [DataMember]
        public string TechBranchId { get; set; }

        [DataMember]
        public string InvoiceNo { get; set; }

        [DataMember]
        public DateTime? InvoiceDt { get; set; }

        [DataMember]
        public string DebtId { get; set; }

        [DataMember]
        public string DebtType { get; set; }

        [DataMember]
        public string CaTaxId { get; set; }

        [DataMember]
        public string CaTaxBranch { get; set; }

        [DataMember]
        public string TaxCode { get; set; }

        [DataMember]
        public decimal? TaxRate { get; set; }

        [DataMember]
        public decimal? Qty { get; set; }

        [DataMember]
        public decimal? AmountExVat { get; set; }

        [DataMember]
        public decimal? VatAmount { get; set; }

        [DataMember]
        public decimal? GAmount { get; set; }

        [DataMember]
        public string NotificationNo { get; set; }
    }
}