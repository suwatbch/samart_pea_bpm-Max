using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities
{
    [DataContract]
    public class CancelPayment
    {
        private string _receiptId;
        private string _receiverId;
        private string _receiverName;
        private string _agentId;
        private string _agentName;
        private DateTime? _paymentDate;
        private decimal? _gAmount;
        private string _reason;
        private string _posId;
        private string _syncFlag;
        private DateTime? _postDate;
        private string _postBranchId;
        private DateTime? _modifiedDt;
        private string _modifiedBy;
        private string _active;


        [DataMember(Order=1)]
        public string ReceiptId
        {
            get { return this._receiptId; }
            set { this._receiptId = value; }
        }


        [DataMember(Order=2)]
        public string ReceiverId
        {
            get { return this._receiverId; }
            set { this._receiverId = value; }
        }


        [DataMember(Order=3)]
        public string ReceiverName
        {
            get { return this._receiverName; }
            set { this._receiverName = value; }
        }


        [DataMember(Order=4)]
        public string AgentId
        {
            get { return this._agentId; }
            set { this._agentId = value; }
        }


        [DataMember(Order=5)]
        public string AgentName
        {
            get { return this._agentName; }
            set { this._agentName = value; }
        }


        [DataMember(Order=6)]
        public DateTime? PaymentDate
        {
            get { return this._paymentDate; }
            set { this._paymentDate = value; }
        }


        [DataMember(Order=7)]
        public decimal? GAmount
        {
            get { return this._gAmount; }
            set { this._gAmount = value; }
        }


        [DataMember(Order=8)]
        public string Reason
        {
            get { return this._reason; }
            set { this._reason = value; }
        }


        [DataMember(Order=9)]
        public string PosId
        {
            get { return this._posId; }
            set { this._posId = value; }
        }


        [DataMember(Order=10)]
        public string SyncFlag
        {
            get { return this._syncFlag; }
            set { this._syncFlag = value; }
        }


        [DataMember(Order=11)]
        public DateTime? PostDt
        {
            get { return this._postDate; }
            set { this._postDate = value; }
        }


        [DataMember(Order=12)]
        public string PostBranchId
        {
            get { return this._postBranchId; }
            set { this._postBranchId = value; }
        }


        [DataMember(Order=13)]
        public DateTime? ModifiedDt
        {
            get { return this._modifiedDt; }
            set { this._modifiedDt = value; }
        }


        [DataMember(Order=14)]
        public string ModifiedBy
        {
            get { return this._modifiedBy; }
            set { this._modifiedBy = value; }
        }


        [DataMember(Order=15)]
        public string Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

    }
}
