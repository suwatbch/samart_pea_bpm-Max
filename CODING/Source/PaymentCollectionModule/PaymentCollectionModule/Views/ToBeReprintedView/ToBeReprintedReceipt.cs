using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;

namespace PEA.BPM.PaymentCollectionModule
{
    [Serializable]
    public class ToBeReprintedReceipt : Receipt
    {
        private bool _isChecked;

        public bool IsChecked
        {
            get { return _isChecked; }
            set { _isChecked = value; }
        }

        public ToBeReprintedReceipt() { }

        public ToBeReprintedReceipt(Receipt receipt)
        {
            this.ReceiptId = receipt.ReceiptId;
            this.ReceiptDate = receipt.ReceiptDate;
            this.CustomerId = receipt.CustomerId;
            this.CustomerName = receipt.CustomerName;
            this.CustomerAddress = receipt.CustomerAddress;
            this.PaymentType = receipt.PaymentType;
            this.PosId = receipt.PosId;
            this.CashierName = receipt.CashierName;
            this.GAmount = receipt.GAmount;
            this.IsChecked = true;
        }
    }
}
