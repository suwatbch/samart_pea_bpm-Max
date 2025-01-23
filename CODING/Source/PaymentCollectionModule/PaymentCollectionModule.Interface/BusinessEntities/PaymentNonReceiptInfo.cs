using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class PaymentNonReceiptInfo
    {
        private string _receiptId;
        private string _caId;
        private string _caName;
        private string _branchId;
        private string _invoiceNo;
        private string _pmId;
        private string _groupInvoiceNo;
        private string _pmName;
        private DateTime _paymentDt;
        private decimal _gAmount;
        private string _cashier;
        private string _postBranchId;
        private string _modifiedBy;
        private string _dtId;
        private string _paperSize;
        private string _taxCode;
        private decimal? _taxRate;
        private string _receiptType;
        private string _arpmId;


        [DataMember(Order=1)]
        public string ReceiptId
        {
            get { return _receiptId; }
            set { _receiptId = value; }
        }


        [DataMember(Order=2)]
        public string CaId
        {
            get { return _caId; }
            set { _caId = value; }
        }


        [DataMember(Order=3)]
        public string CaName
        {
            get { return _caName; }
            set { _caName = value; }
        }


        [DataMember(Order=4)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }


        [DataMember(Order=5)]
        public string InvoiceNo
        {
            get { return _invoiceNo; }
            set { _invoiceNo = value; }
        }


        [DataMember(Order=6)]
        public string PmId
        {
            get { return _pmId; }
            set { _pmId = value; }
        }


        [DataMember(Order=7)]
        public string GroupInvoiceNo
        {
            get { return _groupInvoiceNo; }
            set { _groupInvoiceNo = value; }
        }


        [DataMember(Order=8)]
        public string PmName
        {
            get { return _pmName; }
            set { _pmName = value; }
        }


        [DataMember(Order=9)]
        public DateTime PaymentDt
        {
            get { return _paymentDt; }
            set { _paymentDt = value; }
        }


        [DataMember(Order=10)]
        public decimal GAmount
        {
            get { return _gAmount; }
            set { _gAmount = value; }
        }


        [DataMember(Order=11)]
        public string Cashier
        {
            get { return _cashier; }
            set { _cashier = value; }
        }


        [DataMember(Order=12)]
        public string PostBranchId
        {
            get { return _postBranchId; }
            set { _postBranchId = value; }
        }


        [DataMember(Order=13)]
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }



        [DataMember(Order=14)]
        public string DtId
        {
            get { return _dtId; }
            set { _dtId = value; }
        }


        [DataMember(Order=15)]
        public string TaxCode
        {
            get { return _taxCode; }
            set { _taxCode = value; }
        }


        [DataMember(Order=16)]
        public string PaperSize
        {
            get { return _paperSize; }
            set { _paperSize = value; }
        }


        [DataMember(Order=17)]
        public decimal? TaxRate
        {
            get { return _taxRate; }
            set { _taxRate = value; }
        }


        [DataMember(Order=18)]
        public string ReceiptType
        {
            get { return _receiptType; }
            set { _receiptType = value; }
        }


        [DataMember(Order=19)]
        public string ArpmId
        {
            get { return _arpmId; }
            set { _arpmId = value; }
        }
    }
}
