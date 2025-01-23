using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;

namespace PEA.BPM.PaymentCollectionModule
{
    [Serializable]
    public class ToBeCancelledReceipt : Receipt
    {
        private bool _isChecked;
        public bool IsChecked
        {
            get { return _isChecked; }
            set { _isChecked = value; }
        }

        private Receipt _originalReceipt;
        public Receipt OriginalReceipt
        {
            get { return _originalReceipt; }
            set { _originalReceipt = value; }
        }

        public ToBeCancelledReceipt() { }

        public ToBeCancelledReceipt(Receipt receipt)
        {
            this.ReceiptId = receipt.ReceiptId;
            this.DisplayReceiptId = receipt.DisplayReceiptId;
            this.PrintingSequence = receipt.PrintingSequence;
            this.ReceiptDate = receipt.ReceiptDate;
            this.CustomerId = receipt.CustomerId;
            this.CustomerName = receipt.CustomerName;
            this.CustomerAddress = receipt.CustomerAddress;
            this.PaymentType = receipt.PaymentType;
            this.PosId = receipt.PosId;
            this.CashierName = receipt.CashierName;
            this.GAmount = receipt.GAmount;
            this.OriginalReceipt = receipt;
            receipt.ToBeCancelledReceipt = this;
        }
    }
}
