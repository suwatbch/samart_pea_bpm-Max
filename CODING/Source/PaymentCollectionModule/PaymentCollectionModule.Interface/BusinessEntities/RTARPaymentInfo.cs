using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class RTARPaymentInfo
    {
        string _arptId;

        [DataMember(Order=1)]
        public string ARPtId
        {
            get { return this._arptId; }
            set { this._arptId = value; }
        }

        string _arpmId;

        [DataMember(Order=2)]
        public string ARPmId
        {
            get { return this._arpmId; }
            set { this._arpmId = value; }
        }

        decimal? _amount;

        [DataMember(Order=3)]
        public decimal? Amount
        {
            get { return this._amount; }
            set { this._amount = value; }
        }

        private DateTime? _modifiedDt;

        [DataMember(Order=4)]
        public DateTime? ModifiedDt
        {
            get { return _modifiedDt; }
            set { _modifiedDt = value; }
        }

        private DateTime? _postDt;

        [DataMember(Order=5)]
        public DateTime? PostDt
        {
            get { return _postDt; }
            set { _postDt = value; }
        }

        private string _postBranchServerId;

        [DataMember(Order=6)]
        public string PostBranchServerId
        {
            get { return _postBranchServerId; }
            set { _postBranchServerId = value; }
        }

        private string _syncFlag;

        [DataMember(Order=7)]
        public string SyncFlag
        {
            get { return _syncFlag; }
            set { _syncFlag = value; }
        }

        private string _modifiedBy;

        [DataMember(Order=8)]
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        private bool _active;

        [DataMember(Order=9)]
        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }
    }
}
