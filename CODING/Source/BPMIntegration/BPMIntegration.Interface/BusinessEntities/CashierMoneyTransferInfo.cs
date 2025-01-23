using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class CashierMoneyTransferInfo
    {
        private string _TransferId;
        private string _BranchId;
        private string _Sender;
        private string _SenderName;
        private string _Receiver;
        private string _ReceiverName;
        private string _Commander;
        private string _Status;
        private DateTime? _RequestDt;
        private string _RequestPosId;
        private DateTime? _ResponseDt;
        private string _ResponsePosId;
        private DateTime? _PostDt;
        private string _SyncFlag;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string TransferId
        {
            get { return _TransferId; }
            set { _TransferId = value; }
        }
        [DataMember(Order = 2)]
        public string BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }
        [DataMember(Order = 3)]
        public string Sender
        {
            get { return _Sender; }
            set { _Sender = value; }
        }
        [DataMember(Order = 4)]
        public string SenderName
        {
            get { return _SenderName; }
            set { _SenderName = value; }
        }
        [DataMember(Order = 5)]
        public string Receiver
        {
            get { return _Receiver; }
            set { _Receiver = value; }
        }
        [DataMember(Order = 6)]
        public string ReceiverName
        {
            get { return _ReceiverName; }
            set { _ReceiverName = value; }
        }
        [DataMember(Order = 7)]
        public string Commander
        {
            get { return _Commander; }
            set { _Commander = value; }
        }
        [DataMember(Order = 8)]
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        [DataMember(Order = 9)]
        public DateTime? RequestDt
        {
            get { return _RequestDt; }
            set { _RequestDt = value; }
        }
        [DataMember(Order = 10)]
        public string RequestPosId
        {
            get { return _RequestPosId; }
            set { _RequestPosId = value; }
        }
        [DataMember(Order = 11)]
        public DateTime? ResponseDt
        {
            get { return _ResponseDt; }
            set { _ResponseDt = value; }
        }
        [DataMember(Order = 12)]
        public string ResponsePosId
        {
            get { return _ResponsePosId; }
            set { _ResponsePosId = value; }
        }
        [DataMember(Order = 13)]
        public DateTime? PostDt
        {
            get { return _PostDt; }
            set { _PostDt = value; }
        }
        [DataMember(Order = 14)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 15)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 16)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 17)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 18)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

    }
}
