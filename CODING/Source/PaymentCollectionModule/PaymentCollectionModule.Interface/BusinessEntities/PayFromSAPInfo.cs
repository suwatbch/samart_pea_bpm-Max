using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class PayFromSAPInfo
    {
        string _paymentDocNo;

        [DataMember(Order=1)]
        public string PaymentDocNo
        {
            get { return _paymentDocNo; }
            set { _paymentDocNo = value; }
        }

        string _receiptNo;

        [DataMember(Order=2)]
        public string ReceiptNo
        {
            get { return _receiptNo; }
            set { _receiptNo = value; }
        }

        string _invoiceNo;

        [DataMember(Order=3)]
        public string InvoiceNo
        {
            get { return _invoiceNo; }
            set { _invoiceNo = value; }
        }

        string _caDoc;

        [DataMember(Order=4)]
        public string CaDoc
        {
            get { return _caDoc; }
            set { _caDoc = value; }
        }

        string _docType;

        [DataMember(Order=5)]
        public string DocType
        {
            get { return _docType; }
            set { _docType = value; }
        }

        DateTime? _paymentDt;

        [DataMember(Order=6)]
        public DateTime? PaymentDt
        {
            get { return _paymentDt; }
            set { _paymentDt = value; }
        }

        string _pmId;

        [DataMember(Order=7)]
        public string PmId
        {
            get { return _pmId; }
            set { _pmId = value; }
        }

        decimal? _vatAmount;

        [DataMember(Order=8)]
        public decimal? VatAmount
        {
            get { return _vatAmount; }
            set { _vatAmount = value; }
        }

        decimal? _amount;

        [DataMember(Order=9)]
        public decimal? Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        string _cancelFlag;

        [DataMember(Order=10)]
        public string CancelFlag
        {
            get { return _cancelFlag; }
            set { _cancelFlag = value; }
        }

        private string _syncFlag;

        [DataMember(Order=11)]
        public string SyncFlag
        {
            get { return _syncFlag; }
            set { _syncFlag = value; }
        }

        private string _modifiedBy;

        [DataMember(Order=12)]
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        private DateTime? _modifiedDt;

        [DataMember(Order=13)]
        public DateTime? ModifiedDt
        {
            get { return _modifiedDt; }
            set { _modifiedDt = value; }
        }

        string _action;

        [DataMember(Order=14)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

        string _arpmId;

        [DataMember(Order = 15)]
        public string ARPmId
        {
            get { return _arpmId; }
            set { _arpmId = value; }
        }

        string _arptId;

        [DataMember(Order = 16)]
        public string ARPtId
        {
            get { return _arptId; }
            set { _arptId = value; }
        }

        string _receiptId;

        [DataMember(Order = 17)]
        public string ReceiptId
        {
            get { return _receiptId; }
            set { _receiptId = value; }
        }

        decimal? _totalAmount;

        [DataMember(Order = 18)]
        public decimal? TotalAmount
        {
            get { return _totalAmount; }
            set { _totalAmount = value; }
        }

        DateTime? _dueDt;

        [DataMember(Order = 19)]
        public DateTime? DueDt
        {
            get { return _dueDt; }
            set { _dueDt = value; }
        }
    }

}
