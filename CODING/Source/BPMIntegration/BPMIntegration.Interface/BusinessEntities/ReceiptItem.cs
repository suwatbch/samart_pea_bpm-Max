using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class ReceiptItem
    {
        private string _ReceiptId;
        private string _ARPmId;
        private string _BranchId;
        private string _TechBranchId;
        private string _CommBranchId;
        private string _SyncFlag;
        private DateTime? _PostDt;
        private string _PostBranchServerId;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string ReceiptId
        {
            get { return _ReceiptId; }
            set { _ReceiptId = value; }
        }
        [DataMember(Order = 2)]
        public string ARPmId
        {
            get { return _ARPmId; }
            set { _ARPmId = value; }
        }
        [DataMember(Order = 3)]
        public string BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }
        [DataMember(Order = 4)]
        public string TechBranchId
        {
            get { return _TechBranchId; }
            set { _TechBranchId = value; }
        }
        [DataMember(Order = 5)]
        public string CommBranchId
        {
            get { return _CommBranchId; }
            set { _CommBranchId = value; }
        }
        [DataMember(Order = 6)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 7)]
        public DateTime? PostDt
        {
            get { return _PostDt; }
            set { _PostDt = value; }
        }
        [DataMember(Order = 8)]
        public string PostBranchServerId
        {
            get { return _PostBranchServerId; }
            set { _PostBranchServerId = value; }
        }
        [DataMember(Order = 9)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 10)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 11)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 12)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

    }
}
