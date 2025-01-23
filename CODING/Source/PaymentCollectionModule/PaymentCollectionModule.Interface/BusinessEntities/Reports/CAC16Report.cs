using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports
{
    [DataContract]
    public class CAC16Report
    {
        private string _caDoc;
        private string _subCaDoc;
        private string _invoiceNo;
        private string _paymentBranchId;
        private string _techBranchId;
        private DateTime? _paymentDt;
        private string _cashierId;
        private string _cashierName;


        [DataMember(Order=1)]
        public string CaDoc
        {
            get { return _caDoc; }
            set { _caDoc = value; }
        }


        [DataMember(Order=2)]
        public string SubCaDoc
        {
            get { return _subCaDoc; }
            set { _subCaDoc = value; }
        }


        [DataMember(Order=3)]
        public string InvoiceNo
        {
            get { return _invoiceNo; }
            set { _invoiceNo = value; }
        }


        [DataMember(Order=4)]
        public string PaymentBranchId
        {
            get { return _paymentBranchId; }
            set { _paymentBranchId = value; }
        }


        [DataMember(Order=5)]
        public string TechBranchId
        {
            get { return _techBranchId; }
            set { _techBranchId = value; }
        }


        [DataMember(Order=6)]
        public DateTime? PaymentDt
        {
            get { return _paymentDt; }
            set { _paymentDt = value; }
        }


        [DataMember(Order=7)]
        public string CashierId
        {
            get { return _cashierId; }
            set { _cashierId = value; }
        }


        [DataMember(Order=8)]
        public string CashierName
        {
            get { return _cashierName; }
            set { _cashierName = value; }
        }

        public string PaymentDate
        {
            get { return _paymentDt.Value.ToString("dd/MM/yyyy  HH:mm", new CultureInfo("th-TH")); }
        }
    }
}
