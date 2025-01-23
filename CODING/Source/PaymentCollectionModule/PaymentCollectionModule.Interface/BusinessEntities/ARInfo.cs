using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class ARInfo
    {
        private string _itemId;

        [DataMember(Order=1)]
        public string ItemId
        {
            get { return _itemId; }
            set { _itemId = value; }
        }

        private string _caId;

        [DataMember(Order=2)]
        public string CaId
        {
            get { return _caId; }
            set { _caId = value; }
        }

        private string _dtId;

        [DataMember(Order=3)]
        public string DtId
        {
            get { return _dtId; }
            set { _dtId = value; }
        }

        private string _caDoc;

        [DataMember(Order=4)]
        public string CaDoc
        {
            get { return _caDoc; }
            set { _caDoc = value; }
        }

        private string _description;

        [DataMember(Order=5)]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private string _invoiceNo;

        [DataMember(Order=6)]
        public string InvoiceNo
        {
            get { return _invoiceNo; }
            set { _invoiceNo = value; }
        }

        private string _originalInvoiceNo;

        [DataMember(Order=7)]
        public string OriginalInvoiceNo
        {
            get { return _originalInvoiceNo; }
            set { _originalInvoiceNo = value; }
        }

        private DateTime? _invoiceDt;

        [DataMember(Order=8)]
        public DateTime? InvoiceDt
        {
            get { return _invoiceDt; }
            set { this._invoiceDt = value; }
        }

        private string _groupInvoiceNo;

        [DataMember(Order=9)]
        public string GroupInvoiceNo
        {
            get { return _groupInvoiceNo; }
            set { _groupInvoiceNo = value; }
        }

        private string _billBookId;

        [DataMember(Order=10)]
        public string BillBookId
        {
            get { return _billBookId; }
            set { _billBookId = value; }
        }

        private string _period;

        [DataMember(Order=11)]
        public string Period
        {
            get { return _period; }
            set { _period = value; }
        }

        private DateTime? _meterReadDt;

        [DataMember(Order=12)]
        public DateTime? MeterReadDt
        {
            get { return _meterReadDt; }
            set { _meterReadDt = value; }
        }

        private Decimal? _readUnit;

        [DataMember(Order=13)]
        public Decimal? ReadUnit
        {
            get { return _readUnit; }
            set { _readUnit = value; }
        }

        private Decimal? _lastUnit;

        [DataMember(Order=14)]
        public Decimal? LastUnit
        {
            get { return _lastUnit; }
            set { _lastUnit = value; }
        }

        private decimal? _baseAmount;

        [DataMember(Order=15)]
        public decimal? BaseAmount
        {
            get { return _baseAmount; }
            set { _baseAmount = value; }
        }

        private decimal? _ftUnitPrice;

        [DataMember(Order=16)]
        public decimal? FTUnitPrice
        {
            get { return _ftUnitPrice; }
            set { _ftUnitPrice = value; }
        }

        private decimal? _ftAmount;

        [DataMember(Order=17)]
        public decimal? FTAmount
        {
            get { return _ftAmount; }
            set { _ftAmount = value; }
        }

        private decimal? _unitPrice;

        [DataMember(Order=18)]
        public decimal? UnitPrice
        {
            get { return _unitPrice; }
            set { _unitPrice = value; }
        }

        private decimal? _qty;

        [DataMember(Order=19)]
        public decimal? Qty
        {
            get { return _qty; }
            set { _qty = value; }
        }

        private string _unitTypeId;

        [DataMember(Order=20)]
        public string UnitTypeId
        {
            get { return _unitTypeId; }
            set { _unitTypeId = value; }
        }

        private decimal? _amount;

        [DataMember(Order=21)]
        public decimal? Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        private string _taxCode;

        [DataMember(Order=22)]
        public string TaxCode
        {
            get { return _taxCode; }
            set { _taxCode = value; }
        }

        private decimal? _vatAmount;

        [DataMember(Order=23)]
        public decimal? VatAmount
        {
            get { return _vatAmount; }
            set { _vatAmount = value; }
        }

        private decimal? _gAmount;

        [DataMember(Order=24)]
        public decimal? GAmount
        {
            get { return _gAmount; }
            set { _gAmount = value; }
        }

        private DateTime? _dueDt;

        [DataMember(Order=25)]
        public DateTime? DueDt
        {
            get { return _dueDt; }
            set { _dueDt = value; }
        }

        private DateTime? _dueDt2;

        [DataMember(Order=26)]
        public DateTime? DueDt2
        {
            get { return _dueDt2; }
            set { _dueDt2 = value; }
        }

        private decimal? _paidQty;

        [DataMember(Order=27)]
        public decimal? PaidQty
        {
            get { return _paidQty; }
            set { _paidQty = value; }
        }

        private decimal? _paidVatAmount;

        [DataMember(Order=28)]
        public decimal? PaidVatAmount
        {
            get { return _paidVatAmount; }
            set { _paidVatAmount = value; }
        }

        private decimal? _paidGAmount;

        [DataMember(Order=29)]
        public decimal? PaidGAmount
        {
            get { return _paidGAmount; }
            set { _paidGAmount = value; }
        }


        private DateTime? _disconnectDt;

        [DataMember(Order=30)]
        public DateTime? DisconnectDt
        {
            get { return _disconnectDt; }
            set { _disconnectDt = value; }
        }

        private string _disconnectDoc;

        [DataMember(Order=31)]
        public string DisconnectDoc
        {
            get { return _disconnectDoc; }
            set { _disconnectDoc = value; }
        }

        private string _substDocNo;

        [DataMember(Order=32)]
        public string SubstDocNo
        {
            get { return _substDocNo; }
            set { _substDocNo = value; }
        }

        private string _spotBillInvoiceNo;

        [DataMember(Order=33)]
        public string SpotBillInvoiceNo
        {
            get { return _spotBillInvoiceNo; }
            set { this._spotBillInvoiceNo = value; }
        }

        private string _interestLockFlag;

        [DataMember(Order=34)]
        public string InterestLockFlag
        {
            get { return _interestLockFlag; }
            set { _interestLockFlag = value; }
        }

        private string _installmentFlag;

        [DataMember(Order=35)]
        public string InstallmentFlag
        {
            get { return _installmentFlag; }
            set { _installmentFlag = value; }
        }

        private int? _installmentPeriod;

        [DataMember(Order=36)]
        public int? InstallmentPeriod
        {
            get { return _installmentPeriod; }
            set { _installmentPeriod = value; }
        }

        private int? _installmentTotalPeriod;

        [DataMember(Order=37)]
        public int? InstallmentTotalPeriod
        {
            get { return _installmentTotalPeriod; }
            set { _installmentTotalPeriod = value; }
        }

        private string _paymentOrderFlag;

        [DataMember(Order=38)]
        public string PaymentOrderFlag
        {
            get { return _paymentOrderFlag; }
            set { _paymentOrderFlag = value; }
        }

        private DateTime? _paymentOrderDt;

        [DataMember(Order=39)]
        public DateTime? PaymentOrderDt
        {
            get { return _paymentOrderDt; }
            set { _paymentOrderDt = value; }
        }

        private string _checkInRefNo;

        [DataMember(Order=40)]
        public string CheckInRefNo
        {
            get { return _checkInRefNo; }
            set { _checkInRefNo = value; }
        }

        private string _cancelFlag;

        [DataMember(Order=41)]
        public string CancelFlag
        {
            get { return _cancelFlag; }
            set { _cancelFlag = value; }
        }

        private string _modifiedFlag;

        [DataMember(Order=42)]
        public string ModifiedFlag
        {
            get { return _modifiedFlag; }
            set { _modifiedFlag = value; }
        }

        private string _posDebtFlag;

        [DataMember(Order=43)]
        public string POSDebtFlag
        {
            get { return _posDebtFlag; }
            set { _posDebtFlag = value; }
        }

        private string _syncFlag;

        [DataMember(Order=44)]
        public string SyncFlag
        {
            get { return _syncFlag; }
            set { _syncFlag = value; }
        }

        private string _modifiedBy;

        [DataMember(Order=45)]
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        private bool _active;

        [DataMember(Order=46)]
        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        private DateTime? _postDt;

        [DataMember(Order=47)]
        public DateTime? PostDt
        {
            get { return _postDt; }
            set { _postDt = value; }
        }

        private string _postBranchServerId;

        [DataMember(Order=48)]
        public string PostBranchServerId
        {
            get { return _postBranchServerId; }
            set { _postBranchServerId = value; }
        }

        private DateTime? _modifiedDt;

        [DataMember(Order=49)]
        public DateTime? ModifiedDt
        {
            get { return _modifiedDt; }
            set { _modifiedDt = value; }
        }

        private string _action;

        [DataMember(Order=50)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

        private string _exitOnDoubleInsert;

        [DataMember(Order=51)]
        public string ExitOnDoubleInsert
        {
            get { return _exitOnDoubleInsert; }
            set { _exitOnDoubleInsert = value; }
        }

        private string _fileType;

        [DataMember(Order=52)]
        public string FileType
        {
            get { return _fileType; }
            set { _fileType = value; }
        }

        private string _meterId;

        [DataMember(Order=53)]
        public string MeterId
        {
            get { return _meterId; }
            set { _meterId = value; }
        }

        private string _rateTypeId;

        [DataMember(Order=54)]
        public string RateTypeId
        {
            get { return _rateTypeId; }
            set { _rateTypeId = value; }
        }

        private string _interestKey;

        [DataMember(Order=55)]
        public string InterestKey
        {
            get { return _interestKey; }
            set { _interestKey = value; }
        }

        private DateTime? _createdDt;

        [DataMember(Order=56)]
        public DateTime? CreatedDt
        {
            get { return _createdDt; }
            set { _createdDt = value; }
        }

        private string _fileName;

        [DataMember(Order=57)]
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }
    }
}
