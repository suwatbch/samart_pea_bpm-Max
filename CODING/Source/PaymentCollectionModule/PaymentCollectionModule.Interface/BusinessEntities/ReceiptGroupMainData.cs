using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [Serializable]
    public class ReceiptGroupMainData
    {
        private string      _groupReceiptId;
        private decimal?    _groupReceiptQty;
        private decimal?    _groupReceiptAmount;
        private string      _groupReceiptPeriodText;
        private string      _groupReceiptInstallmentText;  //--[5]
        private string      _groupReceiptInvoiceNo;
        private decimal?    _groupReceiptVatTotal;
        private decimal?    _groupReceiptTotal;
        private string      _groupReceiptMeterIdText;
        private string      _groupReceiptRateTypeText;
        private string      _groupReceiptXReceiptIds;

        
        public string GroupReceiptId
        {
            get { return _groupReceiptId;   }
            set { _groupReceiptId = value;  }
        }

        public string GroupReceiptInvoiceNo
        {
            get { return _groupReceiptInvoiceNo;  }
            set { _groupReceiptInvoiceNo = value; }
        }

        public decimal? GroupReceiptQty
        {
            get { return _groupReceiptQty;  }
            set { _groupReceiptQty = value; }
        }

        public decimal? GroupReceiptAmount
        {
            get { return _groupReceiptAmount;  }
            set { _groupReceiptAmount = value; }
        }

        public string GroupReceiptPeriodText
        {
            get { return _groupReceiptPeriodText;  }
            set { _groupReceiptPeriodText = value; }
        }

        public string GroupReceiptInstallmentText
        {
            get { return _groupReceiptInstallmentText;  }
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
        
        public string GroupReceiptXReceiptId
        {
            get { return _groupReceiptXReceiptIds; }
            set { _groupReceiptXReceiptIds = value; }
        }
    }
}
