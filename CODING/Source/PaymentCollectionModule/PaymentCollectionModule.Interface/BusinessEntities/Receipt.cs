using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [Serializable]
    public class Receipt
    {
        private string _receiptId;
        public string ReceiptId
        {
            get { return _receiptId; }
            set { _receiptId = value; }
        }

        private string _displayReceiptId;
        public string DisplayReceiptId
        {
            get { return _displayReceiptId; }
            set { _displayReceiptId = value; }
        }

        private short? _printingSequence;
        public short? PrintingSequence
        {
            get { return _printingSequence; }
            set { _printingSequence = value; }
        }

        private DateTime? _receiptDate;
        public DateTime? ReceiptDate
        {
            get { return _receiptDate; }
            set { _receiptDate = value; }
        }

        private string _customerId;
        public string CustomerId
        {
            get { return _customerId; }
            set { _customerId = value; }
        }

        private string _customerName;
        public string CustomerName
        {
            get { return _customerName; }
            set { _customerName = value; }
        }

        private string _customerAddress;
        public string CustomerAddress
        {
            get { return _customerAddress; }
            set { _customerAddress = value; }
        }

        private string _paymentId;
        public string PaymentId
        {
            get { return _paymentId; }
            set { _paymentId = value; }
        }

        private string _paymentType;
        public string PaymentType
        {
            get { return _paymentType; }
            set { _paymentType = value; }
        }

        private string _posId;
        public string PosId
        {
            get { return _posId; }
            set { _posId = value; }
        }

        private string _cashierName;
        public string CashierName
        {
            get { return _cashierName; }
            set { _cashierName = value; }
        }

        private decimal? _gAmount;
        public decimal? GAmount
        {
            get { return _gAmount; }
            set { _gAmount = value; }
        }

        private Receipt _toBeCancelledReceipt;
        public Receipt ToBeCancelledReceipt
        {
            get { return _toBeCancelledReceipt; }
            set { _toBeCancelledReceipt = value; }
        }

        private List<Receipt> _relatedReceipts;
        public List<Receipt> RelatedReceipts
        {
            get { return _relatedReceipts; }
            set { _relatedReceipts = value; }
        }

        private List<PaymentTypeInfo> _paymentTypeInfo;
        public List<PaymentTypeInfo> PmInfo
        {
            get { return _paymentTypeInfo; }
            set { _paymentTypeInfo = value; }
        }

        public Receipt()
        {
            _relatedReceipts = new List<Receipt>();
        }



        private string      _groupReceiptId;
        private decimal?    _groupReceiptQty;
        private decimal?    _groupReceiptAmount;
        private string      _groupReceiptPeriodText;
        private string      _groupReceiptInstallmentText;  //--[5]
        private string      _groupReceiptInvoiceNo;
        private decimal?    _groupReceiptVatTotal;
        private decimal?    _groupReceiptTotal;
        private bool?       _groupReceiptIsMain;
        private bool?       _groupReceiptIsItems;
        private string      _groupRefReceiptId;
        private string      _groupReceiptMeterIdText;
        private string      _groupReceiptRateTypeText;
        

        public string GroupReceiptId
        {
            get { return _groupReceiptId; }
            set { _groupReceiptId = value; }
        }

        public string GroupReceiptInvoiceNo
        {
            get { return _groupReceiptInvoiceNo; }
            set { _groupReceiptInvoiceNo = value; }
        }

        public decimal? GroupReceiptQty
        {
            get { return _groupReceiptQty; }
            set { _groupReceiptQty = value; }
        }

        public decimal? GroupReceiptAmount
        {
            get { return _groupReceiptAmount; }
            set { _groupReceiptAmount = value; }
        }

        public string GroupReceiptPeriodText
        {
            get { return _groupReceiptPeriodText; }
            set { _groupReceiptPeriodText = value; }
        }

        public string GroupReceiptInstallmentText
        {
            get { return _groupReceiptInstallmentText; }
            set { _groupReceiptInstallmentText = value; }
        }

        public decimal? GroupReceiptVatTotal
        {
            get { return _groupReceiptVatTotal; }
            set { _groupReceiptVatTotal = value; }
        }

        public decimal? GroupReceiptTotal
        {
            get { return _groupReceiptTotal; }
            set { _groupReceiptTotal = value; }
        }

        public bool? GroupReceiptIsMain
        {
            get { return _groupReceiptIsMain; }
            set { _groupReceiptIsMain = value; }
        }

        public bool? GroupReceiptIsItems
        {
            get { return _groupReceiptIsItems; }
            set { _groupReceiptIsItems = value; }
        }

        public string GroupRefReceiptId
        {
            get { return _groupRefReceiptId; }
            set { _groupRefReceiptId = value; }
        }

        public string GroupReceiptMeterIdText
        {
            get { return _groupReceiptMeterIdText; }
            set { _groupReceiptMeterIdText = value; }
        }

        public string GroupReceiptRateTypeText
        {
            get { return _groupReceiptRateTypeText; }
            set { _groupReceiptRateTypeText = value; }
        }

       
    }

    // รวมใบเสร็จแผนผ่อน  2021-09-27 Uthen.P
    // เพิ่ม Class ExtReceiptIdMapping สำหรับ เก็บความสัมพันธ์ ของ ExtReceiptId กับ ReceiptId ที่ขึ้นต้นด้วย X
    //[DataContract, Serializable]
    //public class ExtReceiptIdMapping
    //{
    //    public string ExtReceiptId  { get; set; }
    //    public string ReceiptId     { get; set; }
    //    public string InvoiceNo     { get; set; }
    //}

    [Serializable]
    public class PaymentTypeInfo
    {
        private string _receiptId;
        public string ReceiptId
        {
            get { return _receiptId; }
            set { _receiptId = value; }
        }

        private string _ptId;
        public string PtId
        {
            get { return _ptId; }
            set { _ptId = value; }
        }

        private string _bankKey;
        public string BankKey
        {
            get { return _bankKey; }
            set { _bankKey = value; }
        }

        private string _chqNo;
        public string ChqNo
        {
            get { return _chqNo; }
            set { _chqNo = value; }
        }

        private string _chqAccNo;
        public string ChqAccNo
        {
            get { return _chqAccNo; }
            set { _chqAccNo = value; }
        }

        private DateTime _chqDt;
        public DateTime ChqDt
        {
            get { return _chqDt; }
            set { _chqDt = value; }
        }

        private decimal? amount;
        public decimal? Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public PaymentTypeInfo()
        {
            
        }

        public PaymentTypeInfo(string receiptId, string ptId, string bankKey, string chqNo, string chqAccNo, DateTime chqDt, Decimal? amount)
        {
            ReceiptId = receiptId;
            PtId = ptId;
            BankKey = bankKey;
            ChqNo = chqNo;
            ChqAccNo = chqAccNo;
            ChqDt = chqDt;
            Amount = amount;
        }
    }

}
