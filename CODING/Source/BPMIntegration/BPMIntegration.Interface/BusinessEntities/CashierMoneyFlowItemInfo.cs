using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class CashierMoneyFlowItemInfo
    {
        private string _FlowId;
        private string _ChqItemId;
        private string _BranchId;
        private string _BankKey;
        private string _ChqNo;
        private string _ChqAccNo;
        private DateTime? _ChqDt;
        private decimal? _Amount;
        private string _ValidFlag;
        private DateTime? _PostDt;
        private string _SyncFlag;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string FlowId
        {
            get { return _FlowId; }
            set { _FlowId = value; }
        }
        [DataMember(Order = 2)]
        public string ChqItemId
        {
            get { return _ChqItemId; }
            set { _ChqItemId = value; }
        }
        [DataMember(Order = 3)]
        public string BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }
        [DataMember(Order = 4)]
        public string BankKey
        {
            get { return _BankKey; }
            set { _BankKey = value; }
        }
        [DataMember(Order = 5)]
        public string ChqNo
        {
            get { return _ChqNo; }
            set { _ChqNo = value; }
        }
        [DataMember(Order = 6)]
        public string ChqAccNo
        {
            get { return _ChqAccNo; }
            set { _ChqAccNo = value; }
        }
        [DataMember(Order = 7)]
        public DateTime? ChqDt
        {
            get { return _ChqDt; }
            set { _ChqDt = value; }
        }
        [DataMember(Order = 8)]
        public decimal? Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        [DataMember(Order = 9)]
        public string ValidFlag
        {
            get { return _ValidFlag; }
            set { _ValidFlag = value; }
        }
        [DataMember(Order = 10)]
        public DateTime? PostDt
        {
            get { return _PostDt; }
            set { _PostDt = value; }
        }
        [DataMember(Order = 11)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 12)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 13)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 14)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 15)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

    }
}
