using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class Receipt
    {
        private string _ReceiptId;
        private string _PaymentId;
        private int? _PrintingSequence;
        private int? _TotalReceipt;
        private string _CaId;
        private string _Name;
        private string _Address;
        private string _IsNameAddrModified;
        private int? _NoOfPrinting;
        private DateTime? _CancelDt;
        private string _CancelReason;
        private string _ReceiptType;
        private string _ExtReceiptId;
        private DateTime? _ExtReceiptDt;
        private string _CaDoc;
        private string _Description;
        private string _InvoiceNo;
        private DateTime? _InvoiceDt;
        private string _OriginalInvoiceNo;
        private DateTime? _OriginalInvoiceDt;
        private int? _InstallmentPeriod;
        private int? _InstallmentTotalPeriod;
        private string _MruId;
        private string _MeterId;
        private string _RateTypeId;
        private string _BranchId;
        private string _TechBranchName;
        private string _CommBranchId;
        private string _CommBranchName;
        private string _Period;
        private string _GroupInvoiceNo;
        private string _BillBookId;
        private string _SpotBillInvoiceNo;
        private DateTime? _MeterReadDt;
        private decimal? _ReadUnit;
        private decimal? _LastUnit;
        private decimal? _FullBaseAmount;
        private decimal? _FullFTAmount;
        private decimal? _FullQty;
        private decimal? _FullAmount;
        private decimal? _FullVatAmount;
        private decimal? _FullGAmount;
        private decimal? _BaseAmount;
        private decimal? _FTUnitPrice;
        private decimal? _FTAmount;
        private decimal? _UnitPrice;
        private decimal? _Qty;
        private decimal? _Amount;
        private decimal? _VatAmount;
        private decimal? _GAmount;
        private decimal? _QtyInstallment;
        private decimal? _AmountInstallment;
        private decimal? _VatAmountInstallment;
        private decimal? _GAmountInstallment;
        private string _DtId;
        private string _DtName;
        private string _ControllerId;
        private string _ControllerName;
        private string _TaxCode;
        private decimal? _TaxRate;
        private int? _PartialPayment;
        private string _GroupIvReceiptType;
        private string _PaymentBranchId;
        private string _SyncFlag;
        private DateTime? _PostDt;
        private string _PostBranchServerId;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string ReceiptId
        {
            get { return _ReceiptId; }
            set { _ReceiptId = value; }
        }
        [DataMember(Order = 2)]
        public string PaymentId
        {
            get { return _PaymentId; }
            set { _PaymentId = value; }
        }
        [DataMember(Order = 3)]
        public int? PrintingSequence
        {
            get { return _PrintingSequence; }
            set { _PrintingSequence = value; }
        }
        [DataMember(Order = 4)]
        public int? TotalReceipt
        {
            get { return _TotalReceipt; }
            set { _TotalReceipt = value; }
        }
        [DataMember(Order = 5)]
        public string CaId
        {
            get { return _CaId; }
            set { _CaId = value; }
        }
        [DataMember(Order = 6)]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        [DataMember(Order = 7)]
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        [DataMember(Order = 8)]
        public string IsNameAddrModified
        {
            get { return _IsNameAddrModified; }
            set { _IsNameAddrModified = value; }
        }
        [DataMember(Order = 9)]
        public int? NoOfPrinting
        {
            get { return _NoOfPrinting; }
            set { _NoOfPrinting = value; }
        }
        [DataMember(Order = 10)]
        public DateTime? CancelDt
        {
            get { return _CancelDt; }
            set { _CancelDt = value; }
        }
        [DataMember(Order = 11)]
        public string CancelReason
        {
            get { return _CancelReason; }
            set { _CancelReason = value; }
        }
        [DataMember(Order = 12)]
        public string ReceiptType
        {
            get { return _ReceiptType; }
            set { _ReceiptType = value; }
        }
        [DataMember(Order = 13)]
        public string ExtReceiptId
        {
            get { return _ExtReceiptId; }
            set { _ExtReceiptId = value; }
        }
        [DataMember(Order = 14)]
        public DateTime? ExtReceiptDt
        {
            get { return _ExtReceiptDt; }
            set { _ExtReceiptDt = value; }
        }
        [DataMember(Order = 15)]
        public string CaDoc
        {
            get { return _CaDoc; }
            set { _CaDoc = value; }
        }
        [DataMember(Order = 16)]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        [DataMember(Order = 17)]
        public string InvoiceNo
        {
            get { return _InvoiceNo; }
            set { _InvoiceNo = value; }
        }
        [DataMember(Order = 18)]
        public DateTime? InvoiceDt
        {
            get { return _InvoiceDt; }
            set { _InvoiceDt = value; }
        }
        [DataMember(Order = 19)]
        public string OriginalInvoiceNo
        {
            get { return _OriginalInvoiceNo; }
            set { _OriginalInvoiceNo = value; }
        }
        [DataMember(Order = 20)]
        public DateTime? OriginalInvoiceDt
        {
            get { return _OriginalInvoiceDt; }
            set { _OriginalInvoiceDt = value; }
        }
        [DataMember(Order = 21)]
        public int? InstallmentPeriod
        {
            get { return _InstallmentPeriod; }
            set { _InstallmentPeriod = value; }
        }
        [DataMember(Order = 22)]
        public int? InstallmentTotalPeriod
        {
            get { return _InstallmentTotalPeriod; }
            set { _InstallmentTotalPeriod = value; }
        }
        [DataMember(Order = 23)]
        public string MruId
        {
            get { return _MruId; }
            set { _MruId = value; }
        }
        [DataMember(Order = 24)]
        public string MeterId
        {
            get { return _MeterId; }
            set { _MeterId = value; }
        }
        [DataMember(Order = 25)]
        public string RateTypeId
        {
            get { return _RateTypeId; }
            set { _RateTypeId = value; }
        }
        [DataMember(Order = 26)]
        public string BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }
        [DataMember(Order = 27)]
        public string TechBranchName
        {
            get { return _TechBranchName; }
            set { _TechBranchName = value; }
        }
        [DataMember(Order = 28)]
        public string CommBranchId
        {
            get { return _CommBranchId; }
            set { _CommBranchId = value; }
        }
        [DataMember(Order = 29)]
        public string CommBranchName
        {
            get { return _CommBranchName; }
            set { _CommBranchName = value; }
        }
        [DataMember(Order = 30)]
        public string Period
        {
            get { return _Period; }
            set { _Period = value; }
        }
        [DataMember(Order = 31)]
        public string GroupInvoiceNo
        {
            get { return _GroupInvoiceNo; }
            set { _GroupInvoiceNo = value; }
        }
        [DataMember(Order = 32)]
        public string BillBookId
        {
            get { return _BillBookId; }
            set { _BillBookId = value; }
        }
        [DataMember(Order = 33)]
        public string SpotBillInvoiceNo
        {
            get { return _SpotBillInvoiceNo; }
            set { _SpotBillInvoiceNo = value; }
        }
        [DataMember(Order = 34)]
        public DateTime? MeterReadDt
        {
            get { return _MeterReadDt; }
            set { _MeterReadDt = value; }
        }
        [DataMember(Order = 35)]
        public decimal? ReadUnit
        {
            get { return _ReadUnit; }
            set { _ReadUnit = value; }
        }
        [DataMember(Order = 36)]
        public decimal? LastUnit
        {
            get { return _LastUnit; }
            set { _LastUnit = value; }
        }
        [DataMember(Order = 37)]
        public decimal? FullBaseAmount
        {
            get { return _FullBaseAmount; }
            set { _FullBaseAmount = value; }
        }
        [DataMember(Order = 38)]
        public decimal? FullFTAmount
        {
            get { return _FullFTAmount; }
            set { _FullFTAmount = value; }
        }
        [DataMember(Order = 39)]
        public decimal? FullQty
        {
            get { return _FullQty; }
            set { _FullQty = value; }
        }
        [DataMember(Order = 40)]
        public decimal? FullAmount
        {
            get { return _FullAmount; }
            set { _FullAmount = value; }
        }
        [DataMember(Order = 41)]
        public decimal? FullVatAmount
        {
            get { return _FullVatAmount; }
            set { _FullVatAmount = value; }
        }
        [DataMember(Order = 42)]
        public decimal? FullGAmount
        {
            get { return _FullGAmount; }
            set { _FullGAmount = value; }
        }
        [DataMember(Order = 43)]
        public decimal? BaseAmount
        {
            get { return _BaseAmount; }
            set { _BaseAmount = value; }
        }
        [DataMember(Order = 44)]
        public decimal? FTUnitPrice
        {
            get { return _FTUnitPrice; }
            set { _FTUnitPrice = value; }
        }
        [DataMember(Order = 45)]
        public decimal? FTAmount
        {
            get { return _FTAmount; }
            set { _FTAmount = value; }
        }
        [DataMember(Order = 46)]
        public decimal? UnitPrice
        {
            get { return _UnitPrice; }
            set { _UnitPrice = value; }
        }
        [DataMember(Order = 47)]
        public decimal? Qty
        {
            get { return _Qty; }
            set { _Qty = value; }
        }
        [DataMember(Order = 48)]
        public decimal? Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        [DataMember(Order = 49)]
        public decimal? VatAmount
        {
            get { return _VatAmount; }
            set { _VatAmount = value; }
        }
        [DataMember(Order = 50)]
        public decimal? GAmount
        {
            get { return _GAmount; }
            set { _GAmount = value; }
        }
        [DataMember(Order = 51)]
        public decimal? QtyInstallment
        {
            get { return _QtyInstallment; }
            set { _QtyInstallment = value; }
        }
        [DataMember(Order = 52)]
        public decimal? AmountInstallment
        {
            get { return _AmountInstallment; }
            set { _AmountInstallment = value; }
        }
        [DataMember(Order = 53)]
        public decimal? VatAmountInstallment
        {
            get { return _VatAmountInstallment; }
            set { _VatAmountInstallment = value; }
        }
        [DataMember(Order = 54)]
        public decimal? GAmountInstallment
        {
            get { return _GAmountInstallment; }
            set { _GAmountInstallment = value; }
        }
        [DataMember(Order = 55)]
        public string DtId
        {
            get { return _DtId; }
            set { _DtId = value; }
        }

        [DataMember(Order = 56)]
        public string DtName
        {
            get { return _DtName; }
            set { _DtName = value; }
        }
        [DataMember(Order = 57)]
        public string ControllerId
        {
            get { return _ControllerId; }
            set { _ControllerId = value; }
        }
        [DataMember(Order = 58)]
        public string ControllerName
        {
            get { return _ControllerName; }
            set { _ControllerName = value; }
        }
        [DataMember(Order = 59)]
        public string TaxCode
        {
            get { return _TaxCode; }
            set { _TaxCode = value; }
        }
        [DataMember(Order = 60)]
        public decimal? TaxRate
        {
            get { return _TaxRate; }
            set { _TaxRate = value; }
        }
        [DataMember(Order = 61)]
        public int? PartialPayment
        {
            get { return _PartialPayment; }
            set { _PartialPayment = value; }
        }
        [DataMember(Order = 62)]
        public string GroupIvReceiptType
        {
            get { return _GroupIvReceiptType; }
            set { _GroupIvReceiptType = value; }
        }
        [DataMember(Order = 63)]
        public string PaymentBranchId
        {
            get { return _PaymentBranchId; }
            set { _PaymentBranchId = value; }
        }
        [DataMember(Order = 64)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 65)]
        public DateTime? PostDt
        {
            get { return _PostDt; }
            set { _PostDt = value; }
        }
        [DataMember(Order = 66)]
        public string PostBranchServerId
        {
            get { return _PostBranchServerId; }
            set { _PostBranchServerId = value; }
        }
        [DataMember(Order = 67)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 68)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 69)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }

        [DataMember(Order = 70)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

    }
}
