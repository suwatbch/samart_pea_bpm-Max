using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.CommonUtilities;
using System.Globalization;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    //[XmlRootAttribute(ElementName = "Invoice", IsNullable = false)]
    [DataContract]
    public class InstallmentInvoice
    {
        private string _invoiceNo;

        [DataMember(Order=1)]
        public string InvoiceNo
        {
            get { return _invoiceNo; }
            set { _invoiceNo = value; }
        }

        private string _caId;

        [DataMember(Order=2)]
        public string CaId
        {
            get { return _caId; }
            set { _caId = value; }
        }

        private Decimal? _gAmount;

        [DataMember(Order=3)]
        public Decimal? GAmount
        {
            get { return _gAmount; }
            set { _gAmount = value; }
        }

        private int? _installmentPeriod;

        [DataMember(Order=4)]
        public int? InstallmentPeriod
        {
            get { return _installmentPeriod; }
            set { _installmentPeriod = value; }
        }

        private int? _installmentTotalPeriod;

        [DataMember(Order=5)]
        public int? InstallmentTotalPeriod
        {
            get { return _installmentTotalPeriod; }
            set { _installmentTotalPeriod = value; }
        }
    }
}
