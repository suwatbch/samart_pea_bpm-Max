using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class PrintPoolInfo
    {
        private string _PrintDoc;
        private int? _PrintType;
        private string _PrintBranch;
        private string _BranchId;
        private string _BranchName;
        private string _MruId;
        private string _CaId;
        private string _CaName;
        private string _BillPred;
        private string _AccountClass;
        private string _SpotBillFlag;
        private string _A4Addition;
        private int? _Reverse;
        private string _OrgDoc;
        private int? _PrintStatus;
        private int? _AgencyFlag;
        private DateTime? _PostDt;
        private string _PostBranchServerId;
        private string _SyncFlag;
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
        public string BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }
        [DataMember(Order = 5)]
        public string BranchName
        {
            get { return _BranchName; }
            set { _BranchName = value; }
        }
        [DataMember(Order = 6)]
        public string MruId
        {
            get { return _MruId; }
            set { _MruId = value; }
        }
        [DataMember(Order = 7)]
        public string CaId
        {
            get { return _CaId; }
            set { _CaId = value; }
        }
        [DataMember(Order = 8)]
        public string CaName
        {
            get { return _CaName; }
            set { _CaName = value; }
        }
        [DataMember(Order = 9)]
        public string BillPred
        {
            get { return _BillPred; }
            set { _BillPred = value; }
        }
        [DataMember(Order = 10)]
        public string AccountClass
        {
            get { return _AccountClass; }
            set { _AccountClass = value; }
        }
        [DataMember(Order = 11)]
        public string SpotBillFlag
        {
            get { return _SpotBillFlag; }
            set { _SpotBillFlag = value; }
        }
        [DataMember(Order = 12)]
        public string A4Addition
        {
            get { return _A4Addition; }
            set { _A4Addition = value; }
        }
        [DataMember(Order = 13)]
        public int? Reverse
        {
            get { return _Reverse; }
            set { _Reverse = value; }
        }
        [DataMember(Order = 14)]
        public string OrgDoc
        {
            get { return _OrgDoc; }
            set { _OrgDoc = value; }
        }
        [DataMember(Order = 15)]
        public int? PrintStatus
        {
            get { return _PrintStatus; }
            set { _PrintStatus = value; }
        }
        [DataMember(Order = 16)]
        public int? AgencyFlag
        {
            get { return _AgencyFlag; }
            set { _AgencyFlag = value; }
        }
        [DataMember(Order = 17)]
        public DateTime? PostDt
        {
            get { return _PostDt; }
            set { _PostDt = value; }
        }
        [DataMember(Order = 18)]
        public string PostBranchServerId
        {
            get { return _PostBranchServerId; }
            set { _PostBranchServerId = value; }
        }
        [DataMember(Order = 19)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 20)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 21)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 22)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 23)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }
    }
}
