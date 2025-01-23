using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class ReceiptItemInfo
    {
        private string _receiptId;

        [DataMember(Order=1)]
        public string ReceiptId
        {
            get { return this._receiptId; }
            set { this._receiptId = value; }
        }

        private string _arpmId;

        [DataMember(Order=2)]
        public string ARPmId
        {
            get { return this._arpmId; }
            set { this._arpmId = value; }
        }

        private DateTime? _postDt;

        [DataMember(Order=3)]
        public DateTime? PostDt
        {
            get { return _postDt; }
            set { _postDt = value; }
        }

        private string _postBranchServerId;

        [DataMember(Order=4)]
        public string PostBranchServerId
        {
            get { return _postBranchServerId; }
            set { _postBranchServerId = value; }
        }

        private string _syncFlag;

        [DataMember(Order=5)]
        public string SyncFlag
        {
            get { return _syncFlag; }
            set { _syncFlag = value; }
        }

        private DateTime? _modifiedDt;

        [DataMember(Order=6)]
        public DateTime? ModifiedDt
        {
            get { return _modifiedDt; }
            set { _modifiedDt = value; }
        }

        private string _modifiedBy;

        [DataMember(Order=7)]
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        private bool _active;

        [DataMember(Order=8)]
        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }
    }
}
