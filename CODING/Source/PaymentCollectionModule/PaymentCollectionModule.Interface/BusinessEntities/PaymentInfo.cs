using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class PaymentInfo
    {
        private string _paymentId;

        [DataMember(Order=1)]
        public string PaymentId
        {
            get { return this._paymentId; }
            set { this._paymentId = value; ; }
        }

        private DateTime? _paymentDt;

        [DataMember(Order=2)]
        public DateTime? PaymentDt
        {
            get { return this._paymentDt; }
            set { this._paymentDt = value; }
        }

        private string _posId;

        [DataMember(Order=3)]
        public string PosId
        {
            get { return this._posId; }
            set { this._posId = value; }
        }

        private string _cashierid;

        [DataMember(Order=4)]
        public string CashierId
        {
            get { return this._cashierid; }
            set { this._cashierid = value; }
        }

        private string _branchId;

        [DataMember(Order=5)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }

        private string _originalPaymentId;

        [DataMember(Order=6)]
        public string OriginalPaymentId
        {
            get { return this._originalPaymentId; }
            set { this._originalPaymentId = value; }
        }

        private Byte? _paidChannel;

        [DataMember(Order=7)]
        public Byte? PaidChannel
        {
            get { return this._paidChannel; }
            set { this._paidChannel = value; }
        }

        private Byte? _cmScope;

        [DataMember(Order=8)]
        public Byte? CmScope
        {
            get { return this._cmScope; }
            set { this._cmScope = value; }
        }

        private DateTime? _postDt;

        [DataMember(Order=9)]
        public DateTime? PostDt
        {
            get { return _postDt; }
            set { _postDt = value; }
        }

        private string _postBranchServerId;

        [DataMember(Order=10)]
        public string PostBranchServerId
        {
            get { return _postBranchServerId; }
            set { _postBranchServerId = value; }
        }

        private string _workId;

        [DataMember(Order = 11)]
        public string WorkId
        {
            get { return _workId; }
            set { _workId = value; }
        }

        private Byte? _workFlag;

        [DataMember(Order = 12)]
        public Byte? WorkFlag
        {
            get { return _workFlag; }
            set { _workFlag = value; }
        }

        private string _syncFlag;

        [DataMember(Order=13)]
        public string SyncFlag
        {
            get { return _syncFlag; }
            set { _syncFlag = value; }
        }

        private DateTime? _modifiedDt;

        [DataMember(Order=14)]
        public DateTime? ModifiedDt
        {
            get { return _modifiedDt; }
            set { _modifiedDt = value; }
        }

        private string _modifiedBy;

        [DataMember(Order=15)]
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        private bool _active;

        [DataMember(Order=16)]
        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }
    }
}
