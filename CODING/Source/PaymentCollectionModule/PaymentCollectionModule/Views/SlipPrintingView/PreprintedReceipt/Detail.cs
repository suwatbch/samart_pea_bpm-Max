using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Views.SlipPrintingView.PreprintedReceipt
{
    public class Detail
    {
        private string _accountId;
        private string _productDesc;
        private string _quantity;
        private decimal? _unitPrice;
        private decimal? _amount;
        

        public string AccountId
        {
            get { return _accountId; }
            set { _accountId = value; }
        }

        public string ProductDesc
        {
            get { return _productDesc; }
            set { _productDesc = value; }
        }

        public string Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        public decimal? UnitPrice
        {
            get { return _unitPrice; }
            set { _unitPrice = value; }
        }

        public decimal? Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        ////DCR รวมใบเสร็จแผนผ่อน 2021AUG (PrePrinted Detail Model)

        private string      _groupReceiptId;
        private decimal?    _groupReceiptQty;
        private decimal?    _groupReceiptAmount;
        private string      _groupReceiptPeriodText;
        private string      _groupReceiptInstallmentText;
        private string      _groupReceiptInvoiceNo;
        private decimal?    _groupReceiptVatTotal;
        private decimal?    _groupReceiptTotal;
        private string      _groupReceiptMeterIdText;
        private string      _groupReceiptRateTypeText;
        private string      _groupXReceiptId;

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

        public string GroupXReceiptId
        {
            get { return _groupXReceiptId; }
            set { _groupXReceiptId = value; }
        }       

    }
}
