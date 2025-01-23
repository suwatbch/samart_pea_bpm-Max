using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class OneTouchLogInfo
    {
       
        private string _invoiceNo;
        private string _debtId;
        private string _receiptId;
        private decimal? _gAmount;
        private decimal? _vatAmount;
        private DateTime? _modifiedDt;
        private string _modifiedBy;
        private string _notificationNo;
        private string _syncFlag;
        private string _action;


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
        public DateTime? ModifiedDt
        {
            get { return _modifiedDt; }
            set { _modifiedDt = value; }
        }

        [DataMember(Order = 6)]
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }
        
        [DataMember(Order = 7)]
        public string NotificationNo
        {
            get { return _notificationNo; }
            set { _notificationNo = value; }
        }

        [DataMember(Order = 8)]
        public string SyncFlag
        {
            get { return _syncFlag; }
            set { _syncFlag = value; }
        }

        [DataMember(Order = 9)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

        [DataMember(Order = 10)]
        public decimal? VatAmount
        {
            get { return _vatAmount; }
            set { _vatAmount = value; }
        }
    }
}
