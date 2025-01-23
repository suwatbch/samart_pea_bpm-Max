using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class ReceiptInfo
    {
        string _receiptId;

        [DataMember(Order=1)]
        public string ReceiptId
        {
            get { return this._receiptId; }
            set { this._receiptId = value; }
        }

        string _paymentId;

        [DataMember(Order=2)]
        public string PaymentId
        {
            get { return this._paymentId; }
            set { this._paymentId = value; }
        }

        int? _printingSequence;

        [DataMember(Order=3)]
        public int? PrintingSequence
        {
            get { return _printingSequence; }
            set { _printingSequence = value; }
        }

        int? _totalReceipt;

        [DataMember(Order=4)]
        public int? TotalReceipt
        {
            get { return _totalReceipt; }
            set { _totalReceipt = value; }
        }

        string _caId;

        [DataMember(Order=5)]
        public string CaId
        {
            get { return this._caId; }
            set { this._caId = value; }
        }

        string _name;

        [DataMember(Order=6)]
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        string _address;

        [DataMember(Order=7)]
        public string Address
        {
            get { return this._address; }
            set { this._address = value; }
        }

        string _isNameAddrModified;

        [DataMember(Order=8)]
        public string IsNameAddrModified
        {
            get { return _isNameAddrModified; }
            set { _isNameAddrModified = value; }
        }

        int? _noOfPrinting;

        [DataMember(Order=9)]
        public int? NoOfPrinting
        {
            get { return this._noOfPrinting; }
            set { this._noOfPrinting = value; }
        }

        DateTime? _cancelDt;

        [DataMember(Order=10)]
        public DateTime? CancelDt
        {
            get { return this._cancelDt; }
            set { this._cancelDt = value; }
        }

        string _cancelReason;

        [DataMember(Order=11)]
        public string CancelReason
        {
            get { return this._cancelReason; }
            set { this._cancelReason = value; }
        }

        string _receiptType;

        [DataMember(Order=12)]
        public string ReceiptType
        {
            get { return this._receiptType; }
            set { this._receiptType = value; }
        }

        string _extReceiptId;

        [DataMember(Order=13)]
        public string ExtReceiptId
        {
            get { return _extReceiptId; }
            set { _extReceiptId = value; }
        }

        DateTime? _extReceiptDt;

        [DataMember(Order=14)]
        public DateTime? ExtReceiptDt
        {
            get { return _extReceiptDt; }
            set { _extReceiptDt = value; }
        }

        //--START--POS2------------------------------
        string _caDoc;

        [DataMember(Order=15)]
        public string CaDoc
        {
            get { return _caDoc; }
            set { _caDoc = value; }
        }

        string _description;

        [DataMember(Order=16)]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        string _invoiceNo;

        [DataMember(Order=17)]
        public string InvoiceNo
        {
            get { return _invoiceNo; }
            set { _invoiceNo = value; }
        }

        DateTime? _invoiceDt;

        [DataMember(Order=18)]
        public DateTime? InvoiceDt
        {
            get { return _invoiceDt; }
            set { _invoiceDt = value; }
        }

        string _originalInvoiceNo;

        [DataMember(Order=19)]
        public string OriginalInvoiceNo
        {
            get { return _originalInvoiceNo; }
            set { _originalInvoiceNo = value; }
        }

        DateTime? _originalInvoiceDt;

        [DataMember(Order=20)]
        public DateTime? OriginalInvoiceDt
        {
            get { return _originalInvoiceDt; }
            set { _originalInvoiceDt = value; }
        }

        int? _installmentPeriod;

        [DataMember(Order=21)]
        public int? InstallmentPeriod
        {
            get { return _installmentPeriod; }
            set { _installmentPeriod = value; }
        }

        int? _installmentTotalPeriod;

        [DataMember(Order=22)]
        public int? InstallmentTotalPeriod
        {
            get { return _installmentTotalPeriod; }
            set { _installmentTotalPeriod = value; }
        }

        string _meterId;

        [DataMember(Order=23)]
        public string MeterId
        {
            get { return _meterId; }
            set { _meterId = value; }
        }

        string _rateTypeId;

        [DataMember(Order=24)]
        public string RateTypeId
        {
            get { return _rateTypeId; }
            set { _rateTypeId = value; }
        }

        string _branchId;

        [DataMember(Order=25)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }

        string _period;

        [DataMember(Order=26)]
        public string Period
        {
            get { return _period; }
            set { _period = value; }
        }

        string _groupInvoiceNo;

        [DataMember(Order=27)]
        public string GroupInvoiceNo
        {
            get { return _groupInvoiceNo; }
            set { _groupInvoiceNo = value; }
        }

        string _billBookId;

        [DataMember(Order=28)]
        public string BillBookId
        {
            get { return _billBookId; }
            set { _billBookId = value; }
        }

        string _spotBillInvoiceNo;

        [DataMember(Order=29)]
        public string SpotBillInvoiceNo
        {
            get { return _spotBillInvoiceNo; }
            set { _spotBillInvoiceNo = value; }
        }

        DateTime? _meterReadDt;

        [DataMember(Order=30)]
        public DateTime? MeterReadDt
        {
            get { return _meterReadDt; }
            set { _meterReadDt = value; }
        }

        Decimal? _readUnit;

        [DataMember(Order=31)]
        public Decimal? ReadUnit
        {
            get { return _readUnit; }
            set { _readUnit = value; }
        }

        Decimal? _lastUnit;

        [DataMember(Order=32)]
        public Decimal? LastUnit
        {
            get { return _lastUnit; }
            set { _lastUnit = value; }
        }

        Decimal? _fullBaseAmount;

        [DataMember(Order=33)]
        public Decimal? FullBaseAmount
        {
            get { return _fullBaseAmount; }
            set { _fullBaseAmount = value; }
        }

        Decimal? _fullFTAmount;

        [DataMember(Order=34)]
        public Decimal? FullFTAmount
        {
            get { return _fullFTAmount; }
            set { _fullFTAmount = value; }
        }

        Decimal? _fullQty;

        [DataMember(Order=35)]
        public Decimal? FullQty
        {
            get { return _fullQty; }
            set { _fullQty = value; }
        }

        Decimal? _fullAmount;

        [DataMember(Order=36)]
        public Decimal? FullAmount
        {
            get { return _fullAmount; }
            set { _fullAmount = value; }
        }

        Decimal? _fullVatAmount;

        [DataMember(Order=37)]
        public Decimal? FullVatAmount
        {
            get { return _fullVatAmount; }
            set { _fullVatAmount = value; }
        }

        Decimal? _fullGAmount;

        [DataMember(Order=38)]
        public Decimal? FullGAmount
        {
            get { return _fullGAmount; }
            set { _fullGAmount = value; }
        }

        Decimal? _baseAmount;

        [DataMember(Order=39)]
        public Decimal? BaseAmount
        {
            get { return _baseAmount; }
            set { _baseAmount = value; }
        }

        Decimal? _fTUnitPrice;

        [DataMember(Order=40)]
        public Decimal? FTUnitPrice
        {
            get { return _fTUnitPrice; }
            set { _fTUnitPrice = value; }
        }

        Decimal? _fTAmount;

        [DataMember(Order=41)]
        public Decimal? FTAmount
        {
            get { return _fTAmount; }
            set { _fTAmount = value; }
        }

        Decimal? _unitPrice;

        [DataMember(Order=42)]
        public Decimal? UnitPrice
        {
            get { return _unitPrice; }
            set { _unitPrice = value; }
        }

        Decimal? _qty;

        [DataMember(Order=43)]
        public Decimal? Qty
        {
            get { return _qty; }
            set { _qty = value; }
        }

        Decimal? _amount;

        [DataMember(Order=44)]
        public Decimal? Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        Decimal? _vatAmount;

        [DataMember(Order=45)]
        public Decimal? VatAmount
        {
            get { return _vatAmount; }
            set { _vatAmount = value; }
        }

        Decimal? _gAmount;

        [DataMember(Order=46)]
        public Decimal? GAmount
        {
            get { return _gAmount; }
            set { _gAmount = value; }
        }

        Decimal? _qtyInstallment;

        [DataMember(Order=47)]
        public Decimal? QtyInstallment
        {
            get { return _qtyInstallment; }
            set { _qtyInstallment = value; }
        }

        Decimal? _amountInstallment;

        [DataMember(Order=48)]
        public Decimal? AmountInstallment
        {
            get { return _amountInstallment; }
            set { _amountInstallment = value; }
        }

        Decimal? _vatAmountInstallment;

        [DataMember(Order=49)]
        public Decimal? VatAmountInstallment
        {
            get { return _vatAmountInstallment; }
            set { _vatAmountInstallment = value; }
        }

        Decimal? _gAmountInstallment;

        [DataMember(Order=50)]
        public Decimal? GAmountInstallment
        {
            get { return _gAmountInstallment; }
            set { _gAmountInstallment = value; }
        }

        string _dtId;

        [DataMember(Order=51)]
        public string DtId
        {
            get { return _dtId; }
            set { _dtId = value; }
        }

        string _dtName;

        [DataMember(Order=52)]
        public string DtName
        {
            get { return _dtName; }
            set { _dtName = value; }
        }

        string _controllerId;

        [DataMember(Order=53)]
        public string ControllerId
        {
            get { return _controllerId; }
            set { _controllerId = value; }
        }

        string _taxCode;

        [DataMember(Order=54)]
        public string TaxCode
        {
            get { return _taxCode; }
            set { _taxCode = value; }
        }

        Decimal? _taxRate;

        [DataMember(Order=55)]
        public Decimal? TaxRate
        {
            get { return _taxRate; }
            set { _taxRate = value; }
        }

        Byte? _partialPayment;

        [DataMember(Order=56)]
        public Byte? PartialPayment
        {
            get { return _partialPayment; }
            set { _partialPayment = value; }
        }

        string _groupIvReceiptType;

        [DataMember(Order=57)]
        public string GroupIvReceiptType
        {
            get { return _groupIvReceiptType; }
            set { _groupIvReceiptType = value; }
        }
        //--END--POS2--------------------------------

        DateTime? _postDt;

        [DataMember(Order=58)]
        public DateTime? PostDt
        {
            get { return _postDt; }
            set { _postDt = value; }
        }

        private string _postBranchServerId;

        [DataMember(Order=59)]
        public string PostBranchServerId
        {
            get { return _postBranchServerId; }
            set { _postBranchServerId = value; }
        }

        string _syncFlag;

        [DataMember(Order=60)]
        public string SyncFlag
        {
            get { return _syncFlag; }
            set { _syncFlag = value; }
        }

        private DateTime? _modifiedDt;

        [DataMember(Order=61)]
        public DateTime? ModifiedDt
        {
            get { return _modifiedDt; }
            set { _modifiedDt = value; }
        }

        private string _modifiedBy;

        [DataMember(Order=62)]
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        private bool _active;

        [DataMember(Order=63)]
        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }
    }
}
