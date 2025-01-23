using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class InterestLog
    {
        private string _caDoc;

        [DataMember(Order = 1)]
        public string CaDoc
        {
            get { return _caDoc; }
            set { _caDoc = value; }
        }

        private string _subCaDoc;

        [DataMember(Order = 2)]
        public string SubCaDoc
        {
            get { return _subCaDoc; }
            set { _subCaDoc = value; }
        }

        private string _invoiceNo;

        [DataMember(Order = 3)]
        public string InvoiceNo
        {
            get { return _invoiceNo; }
            set { _invoiceNo = value; }
        }

        private string _paymentBranchId;

        [DataMember(Order = 4)]
        public string PaymentBranchId
        {
            get { return _paymentBranchId; }
            set { _paymentBranchId = value; }
        }

        private string _techBranchId;

        [DataMember(Order = 5)]
        public string TechBranchId
        {
            get { return _techBranchId; }
            set { _techBranchId = value; }
        }

        private DateTime _paymentDt;

        [DataMember(Order = 6)]
        public DateTime PaymentDt
        {
            get { return _paymentDt; }
            set { _paymentDt = value; }
        }

        private string _cashierId;

        [DataMember(Order = 7)]
        public string CashierId
        {
            get { return _cashierId; }
            set { _cashierId = value; }
        }

        private string _cashierName;

        [DataMember(Order = 8)]
        public string CashierName
        {
            get { return _cashierName; }
            set { _cashierName = value; }
        }

    }
}
