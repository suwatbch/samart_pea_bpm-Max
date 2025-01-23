using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Views.SlipPrintingView.PreprintedReceipt
{
    public class ReceiptPaymentMethod
    {
        private string _paymentDetail;

        public string PaymentDetail
        {
            get { return _paymentDetail; }
            set { _paymentDetail = value; }
        }
    }
}
