using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class VSPPInfo
    {

        private string _caId;
        private string _caName;
        private string _caAddress;
        private string _techBranchId;
        private string _invoiceNo;
        private DateTime? _invoiceDt;
        private string _debtId;
        private string _debtType;
        private string _caTaxId;
        private string _caTaxBranch;
        private string _taxCode;
        private decimal? _taxRate;
        private decimal? _qty;
        private decimal? _amountExVat;
        private decimal? _vatAmount;
        private decimal? _gAmount;
        private string _notificationNo;

        [DataMember(Order = 1)]
        public string CaId
        {
            get { return _caId; }
            set { _caId = value; }
        }

        [DataMember(Order = 2)]
        public string CaName
        {
            get { return _caName; }
            set { _caName = value; }
        }

        [DataMember(Order = 3)]
        public string CaAddress
        {
            get { return _caAddress; }
            set { _caAddress = value; }
        }

        [DataMember(Order = 4)]
        public string TechBranchId
        {
            get { return _techBranchId; }
            set { _techBranchId = value; }
        }

        [DataMember(Order = 5)]
        public string InvoiceNo
        {
            get { return _invoiceNo; }
            set { _invoiceNo = value; }
        }

        [DataMember(Order = 6)]
        public DateTime? InvoiceDt
        {
            get { return _invoiceDt; }
            set { _invoiceDt = value; }
        }

        [DataMember(Order = 7)]
        public string DebtId
        {
            get { return _debtId; }
            set { _debtId = value; }
        }

        [DataMember(Order = 8)]
        public string DebtType
        {
            get { return _debtType; }
            set { _debtType = value; }
        }

        [DataMember(Order = 9)]
        public string CaTaxId
        {
            get { return _caTaxId; }
            set { _caTaxId = value; }
        }

        [DataMember(Order = 10)]
        public string CaTaxBranch
        {
            get { return _caTaxBranch; }
            set { _caTaxBranch = value; }
        }

        [DataMember(Order = 11)]
        public string TaxCode
        {
            get { return _taxCode; }
            set { _taxCode = value; }
        }

        [DataMember(Order = 12)]
        public decimal? TaxRate
        {
            get { return _taxRate; }
            set { _taxRate = value; }
        }

        [DataMember(Order = 13)]
        public decimal? Qty
        {
            get { return _qty; }
            set { _qty = value; }
        }

        [DataMember(Order = 14)]
        public decimal? AmountExVat
        {
            get { return _amountExVat; }
            set { _amountExVat = value; }
        }

        [DataMember(Order = 15)]
        public decimal? VatAmount
        {
            get { return _vatAmount; }
            set { _vatAmount = value; }
        }

        [DataMember(Order = 16)]
        public decimal? GAmount
        {
            get { return _gAmount; }
            set { _gAmount = value; }
        }

        [DataMember(Order = 17)]
        public string NotificationNo
        {
            get { return _notificationNo; }
            set { _notificationNo = value; }
        }

    }
}
