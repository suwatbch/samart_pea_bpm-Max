using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports
{
    [DataContract]
    public class CAC14Report
    {
        private string _caId;
        private string _nameAddress;
        private string _period;
        private string _receiptId;
        private string _paymentDoc;
        private DateTime? _paymentDt;
        private decimal? _qty;
        private decimal? _unit;
        private decimal? _amountExtVat;
        private decimal? _vat;
        private decimal? _gAmount;
        private string _branchId;
        private string _branchName;
        private string _cashierId;
        private string _ca_BranchId;
        private string _ca_BranchName;
        private string _caTaxId;
        private string _caTaxBranch;
        private string _dtId;
        private string _dtName;
        private string _subGroupInvoiceNo;
        private int? _rowNumber;
        private string _groupInvoiceHeaderText;


        [DataMember(Order=1)]
        public string CaId
        {
            get { return _caId; }
            set { _caId = value; }
        }


        [DataMember(Order=2)]
        public string NameAddress
        {
            get { return _nameAddress; }
            set { _nameAddress = value; }
        }


        [DataMember(Order=3)]
        public string Period
        {
            get { return _period; }
            set { _period = value; }
        }


        [DataMember(Order=4)]
        public string ReceiptId
        {
            get { return _receiptId; }
            set { _receiptId = value; }
        }


        [DataMember(Order=5)]
        public string PaymentDoc
        {
            get { return _paymentDoc; }
            set { _paymentDoc = value; }
        }


        [DataMember(Order=6)]
        public DateTime? PaymentDt
        {
            get { return _paymentDt; }
            set { _paymentDt = value; }
        }


        [DataMember(Order=7)]
        public decimal? Qty
        {
            get { return _qty; }
            set { _qty = value; }
        }


        [DataMember(Order=8)]
        public decimal? Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }


        [DataMember(Order=9)]
        public decimal? AmountExtVat
        {
            get { return _amountExtVat; }
            set { _amountExtVat = value; }
        }


        [DataMember(Order=10)]
        public decimal? Vat
        {
            get { return _vat; }
            set { _vat = value; }
        }


        [DataMember(Order=11)]
        public decimal? GAmount
        {
            get { return _gAmount; }
            set { _gAmount = value; }
        }


        [DataMember(Order=12)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }


        [DataMember(Order=13)]
        public string BranchName
        {
            get { return _branchName; }
            set { _branchName = value; }
        }


        [DataMember(Order=14)]
        public string CashierId
        {
            get { return _cashierId; }
            set { _cashierId = value; }
        }


        [DataMember(Order=15)]
        public string CA_BranchId
        {
            get { return _ca_BranchId; }
            set { _ca_BranchId = value; }
        }


        [DataMember(Order=16)]
        public string CA_BranchName
        {
            get { return _ca_BranchName; }
            set { _ca_BranchName = value; }
        }

        [DataMember(Order = 17)]
        public string CaTaxId
        {
            get { return _caTaxId; }
            set { _caTaxId = value; }
        }

        [DataMember(Order = 18)]
        public string CaTaxBranch
        {
            get { return _caTaxBranch; }
            set { _caTaxBranch = value; }
        }

        [DataMember(Order = 19)]
        public string DtId
        {
            get { return _dtId; }
            set { _dtId = value; }
        }

        [DataMember(Order = 20)]
        public string DtName
        {
            get { return _dtName; }
            set { _dtName = value; }
        }

        [DataMember(Order = 21)]
        public string SubGroupInvoiceNo
        {
            get { return _subGroupInvoiceNo; }
            set { _subGroupInvoiceNo = value; }
        }

        [DataMember(Order = 22)]
        public int? RowNumber
        {
            get { return _rowNumber; }
            set { _rowNumber = value; }
        }

        [DataMember(Order = 23)]
        public string GroupInvoiceHeaderText
        {
            get { return _groupInvoiceHeaderText; }
            set { _groupInvoiceHeaderText = value; }
        }

        

    }
}
