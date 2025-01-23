using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.OneTouchModule.Interface.BusinessEntities
{
    [DataContract]
    public class OneTouchInfo
    {
       
        private string _invoiceNo;
        private string _debtId;
        private string _receiptId;
        private decimal? _gAmount;
        private string _notificationNo;


        [DataMember(Order = 1)]
        public string InvoiceNo
        {
            get { return _invoiceNo; }
            set { _invoiceNo = value; }
        }

        [DataMember(Order = 2)]
        public string DebtId
        {
            get { return _debtId; }
            set { _debtId = value; }
        }

        [DataMember(Order = 3)]
        public string ReceiptId
        {
            get { return _receiptId; }
            set { _receiptId = value; }
        }

        [DataMember(Order = 4)]
        public decimal? GAmount
        {
            get { return _gAmount; }
            set { _gAmount = value; }
        }


      
        [DataMember(Order = 5)]
        public string NotificationNo
        {
            get { return _notificationNo; }
            set { _notificationNo = value; }
        }

    }
}
