using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class EPayUploadInfo
    {
        private string _FileId;
        private string _CompanyId;
        private string _AgentId;
        private string _AgentName;
        private string _AccountClassId;
        private string _AccountClassName;
        private string _FileName;
        private DateTime? _UploadDt;
        private int? _BillCount;
        private decimal? _BillAmount;
        private int? _TotalBillCount;
        private decimal? _TotalBillAmount;
        private string _SyncFlag;
        private DateTime? _PostDt;
        private string _PostBranchServerId;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string FileId
        {
            get { return _FileId; }
            set { _FileId = value; }
        }
        [DataMember(Order = 2)]
        public string CompanyId
        {
            get { return _CompanyId; }
            set { _CompanyId = value; }
        }
        [DataMember(Order = 3)]
        public string AgentId
        {
            get { return _AgentId; }
            set { _AgentId = value; }
        }
        [DataMember(Order = 4)]
        public string AgentName
        {
            get { return _AgentName; }
            set { _AgentName = value; }
        }
        [DataMember(Order = 5)]
        public string AccountClassId
        {
            get { return _AccountClassId; }
            set { _AccountClassId = value; }
        }
        [DataMember(Order = 6)]
        public string AccountClassName
        {
            get { return _AccountClassName; }
            set { _AccountClassName = value; }
        }
        [DataMember(Order = 7)]
        public string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }
        [DataMember(Order = 8)]
        public DateTime? UploadDt
        {
            get { return _UploadDt; }
            set { _UploadDt = value; }
        }
        [DataMember(Order = 9)]
        public int? BillCount
        {
            get { return _BillCount; }
            set { _BillCount = value; }
        }
        [DataMember(Order = 10)]
        public decimal? BillAmount
        {
            get { return _BillAmount; }
            set { _BillAmount = value; }
        }
        [DataMember(Order = 11)]
        public int? TotalBillCount
        {
            get { return _TotalBillCount; }
            set { _TotalBillCount = value; }
        }
        [DataMember(Order = 12)]
        public decimal? TotalBillAmount
        {
            get { return _TotalBillAmount; }
            set { _TotalBillAmount = value; }
        }
        [DataMember(Order = 13)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 14)]
        public DateTime? PostDt
        {
            get { return _PostDt; }
            set { _PostDt = value; }
        }
        [DataMember(Order = 15)]
        public string PostBranchServerId
        {
            get { return _PostBranchServerId; }
            set { _PostBranchServerId = value; }
        }
        [DataMember(Order = 16)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 17)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 18)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 19)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

    }
}
