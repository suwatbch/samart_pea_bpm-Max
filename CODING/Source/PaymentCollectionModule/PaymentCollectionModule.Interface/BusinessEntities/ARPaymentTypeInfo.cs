using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class ARPaymentTypeInfo
    {
        string _arptId;

        [DataMember(Order=1)]
        public string ARPtId
        {
            get { return this._arptId; }
            set { this._arptId = value; }
        }

        string _paymentId;

        [DataMember(Order=2)]
        public string PaymentId
        {
            get { return this._paymentId; }
            set { this._paymentId = value; }
        }

        decimal? _amount;

        [DataMember(Order=3)]
        public decimal? Amount
        {
            get { return this._amount; }
            set { this._amount = value; }
        }

        decimal? _feeAmount;

        [DataMember(Order=4)]
        public decimal? FeeAmount
        {
            get { return this._feeAmount; }
            set { this._feeAmount = value; }
        }

        string _ptId;

        [DataMember(Order=5)]
        public string PtId
        {
            get { return this._ptId; }
            set { this._ptId = value; }
        }

        decimal? _changeAmount;

        [DataMember(Order=6)]
        public decimal? ChangeAmount
        {
            get { return this._changeAmount; }
            set { this._changeAmount = value; }
        }

        string _bankId;

        [DataMember(Order=7)]
        public string BankId
        {
            get { return this._bankId; }
            set { this._bankId = value; }
        }

        string _chqNo;

        [DataMember(Order=8)]
        public string ChqNo
        {
            get { return this._chqNo; }
            set { this._chqNo = value; }
        }

        string _chqAccNo;

        [DataMember(Order=9)]
        public string ChqAccNo
        {
            get { return this._chqAccNo; }
            set { this._chqAccNo = value; }
        }

        DateTime? _chqDt;

        [DataMember(Order=10)]
        public DateTime? ChqDt
        {
            get { return this._chqDt; }
            set { this._chqDt = value; }
        }

        string _draftFlag;

        [DataMember(Order=11)]
        public string DraftFlag
        {
            get { return _draftFlag; }
            set { _draftFlag = value; }
        }

        string _cashierChequeFlag;

        [DataMember(Order=12)]
        public string CashierChequeFlag
        {
            get { return _cashierChequeFlag; }
            set { _cashierChequeFlag = value; }
        }

        string _cancelARPtId;

        [DataMember(Order=13)]
        public string CancelARPtId
        {
            get { return _cancelARPtId; }
            set { _cancelARPtId = value; }
        }

        string _tranfAccNo;

        [DataMember(Order=14)]
        public string TranfAccNo
        {
            get { return this._tranfAccNo; }
            set { this._tranfAccNo = value; ; }
        }

        DateTime? _tranfDt;

        [DataMember(Order=15)]
        public DateTime? TranfDt
        {
            get { return this._tranfDt; }
            set { this._tranfDt = value; ; }
        }

        DateTime? _postDt;

        [DataMember(Order=16)]
        public DateTime? PostDt
        {
            get { return _postDt; }
            set { _postDt = value; }
        }

        private string _postBranchServerId;

        [DataMember(Order=17)]
        public string PostBranchServerId
        {
            get { return _postBranchServerId; }
            set { _postBranchServerId = value; }
        }

        private string _syncFlag;

        [DataMember(Order=18)]
        public string SyncFlag
        {
            get { return _syncFlag; }
            set { _syncFlag = value; }
        }

        private DateTime? _modifiedDt;

        [DataMember(Order=19)]
        public DateTime? ModifiedDt
        {
            get { return _modifiedDt; }
            set { _modifiedDt = value; }
        }

        private string _modifiedBy;

        [DataMember(Order=20)]
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        private bool _active;

        [DataMember(Order=21)]
        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }
    }
}
