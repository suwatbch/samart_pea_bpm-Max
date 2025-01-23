using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class DeliveryHistoryInfo
    {
        private string _PrintDoc;
        private int? _PrintType;
        private string _PrintBranch;
        private int? _SentLog;
        private int? _BillSeqNo;
        private string _BranchId;
        private string _BranchName;
        private string _MruId;
        private string _BillPred;
        private DateTime? _SentDt;
        private string _DeliveryPred;
        private string _DeliveryPlaceId;
        private string _DeliveryPlaceName;
        private string _OrgDoc;
        private string _SyncFlag;
        private string _PostBranchServerId;
        private DateTime? _PostDt;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string PrintDoc
        {
            get { return _PrintDoc; }
            set { _PrintDoc = value; }
        }
        [DataMember(Order = 2)]
        public int? PrintType
        {
            get { return _PrintType; }
            set { _PrintType = value; }
        }
        [DataMember(Order = 3)]
        public string PrintBranch
        {
            get { return _PrintBranch; }
            set { _PrintBranch = value; }
        }
        [DataMember(Order = 4)]
        public int? SentLog
        {
            get { return _SentLog; }
            set { _SentLog = value; }
        }
        [DataMember(Order = 5)]
        public int? BillSeqNo
        {
            get { return _BillSeqNo; }
            set { _BillSeqNo = value; }
        }
        [DataMember(Order = 6)]
        public string BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }
        [DataMember(Order = 7)]
        public string BranchName
        {
            get { return _BranchName; }
            set { _BranchName = value; }
        }
        [DataMember(Order = 8)]
        public string MruId
        {
            get { return _MruId; }
            set { _MruId = value; }
        }
        [DataMember(Order = 9)]
        public string BillPred
        {
            get { return _BillPred; }
            set { _BillPred = value; }
        }
        [DataMember(Order = 10)]
        public DateTime? SentDt
        {
            get { return _SentDt; }
            set { _SentDt = value; }
        }
        [DataMember(Order = 11)]
        public string DeliveryPred
        {
            get { return _DeliveryPred; }
            set { _DeliveryPred = value; }
        }
        [DataMember(Order = 12)]
        public string DeliveryPlaceId
        {
            get { return _DeliveryPlaceId; }
            set { _DeliveryPlaceId = value; }
        }
        [DataMember(Order = 13)]
        public string DeliveryPlaceName
        {
            get { return _DeliveryPlaceName; }
            set { _DeliveryPlaceName = value; }
        }
        [DataMember(Order = 14)]
        public string OrgDoc
        {
            get { return _OrgDoc; }
            set { _OrgDoc = value; }
        }
        [DataMember(Order = 15)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 16)]
        public string PostBranchServerId
        {
            get { return _PostBranchServerId; }
            set { _PostBranchServerId = value; }
        }
        [DataMember(Order = 17)]
        public DateTime? PostDt
        {
            get { return _PostDt; }
            set { _PostDt = value; }
        }
        [DataMember(Order = 18)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 19)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 20)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 21)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

    }
}
