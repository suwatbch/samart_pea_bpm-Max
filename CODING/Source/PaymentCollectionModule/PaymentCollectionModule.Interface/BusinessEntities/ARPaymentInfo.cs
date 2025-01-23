using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class ARPaymentInfo
    {
        private string _arpmId;

        [DataMember(Order=1)]
        public string ArpmId
        {
            get { return _arpmId; }
            set { _arpmId = value; }
        }

        private string _invoiceNo;

        [DataMember(Order=2)]
        public string InvoiceNo
        {
            get { return _invoiceNo; }
            set { _invoiceNo = value; }
        }

        private string _pmId;

        [DataMember(Order=3)]
        public string PmId
        {
            get { return _pmId; }
            set { _pmId = value; }
        }

        private string _docType;

        [DataMember(Order=4)]
        public string DocType
        {
            get { return _docType; }
            set { _docType = value; }
        }

        private Decimal? _qty;

        [DataMember(Order=5)]
        public Decimal? Qty
        {
            get { return _qty; }
            set { _qty = value; }
        }

        private Decimal? _vatAmount;

        [DataMember(Order=6)]
        public Decimal? VatAmount
        {
            get { return _vatAmount; }
            set { _vatAmount = value; }
        }

        private Decimal? _gAmount;

        [DataMember(Order=7)]
        public Decimal? GAmount
        {
            get { return _gAmount; }
            set { _gAmount = value; }
        }

        private Decimal? _adjAmount;

        [DataMember(Order=8)]
        public Decimal? AdjAmount
        {
            get { return _adjAmount; }
            set { _adjAmount = value; }
        }

        private string _cancelArpmId;

        [DataMember(Order=9)]
        public string CancelArpmId
        {
            get { return _cancelArpmId; }
            set { _cancelArpmId = value; }
        }

        private DateTime? _paymentDt;

        [DataMember(Order=10)]
        public DateTime? PaymentDt
        {
            get { return _paymentDt; }
            set { _paymentDt = value; }
        }

        private string _pending;

        [DataMember(Order=11)]
        public string Pending
        {
            get { return _pending; }
            set { _pending = value; }
        }

        private Byte? _paidChannel;

        [DataMember(Order=12)]
        public Byte? PaidChannel
        {
            get { return _paidChannel; }
            set { _paidChannel = value; }
        }

        private DateTime? _postDt;

        [DataMember(Order=13)]
        public DateTime? PostDt
        {
            get { return _postDt; }
            set { _postDt = value; }
        }

        private string _postBranchServerId;

        [DataMember(Order=14)]
        public string PostBranchServerId
        {
            get { return _postBranchServerId; }
            set { _postBranchServerId = value; }
        }

        private string _syncFlag;

        [DataMember(Order=15)]
        public string SyncFlag
        {
            get { return _syncFlag; }
            set { _syncFlag = value; }
        }

        private DateTime? _modifiedDt;

        [DataMember(Order=16)]
        public DateTime? ModifiedDt
        {
            get { return _modifiedDt; }
            set { _modifiedDt = value; }
        }

        private string _modifiedBy;

        [DataMember(Order=17)]
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        private bool _active;

        [DataMember(Order=18)]
        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }
    }
}
