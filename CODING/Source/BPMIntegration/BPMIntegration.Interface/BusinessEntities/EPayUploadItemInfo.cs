using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class EPayUploadItemInfo
    {
        private string _EPayItemId;
        private string _FileId;
        private string _Regional;
        private string _RegionalName;
        private string _BranchId;
        private string _BranchName;
        private string _MruId;
        private string _CaId;
        private string _CaName;
        private DateTime? _PayDt;
        private DateTime? _DueDt;
        private decimal? _OutSourceAmount;
        private decimal? _VatAmount;
        private string _RecNo;
        private string _Period;
        private string _CompanyId;
        private string _ActCode;
        private string _PosNo;
        private string _InvoiceNo;
        private string _UploadStatus;
        private string _SyncFlag;
        private string _PostBranchServerId;
        private DateTime? _PostDt;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string EPayItemId
        {
            get { return _EPayItemId; }
            set { _EPayItemId = value; }
        }
        [DataMember(Order = 2)]
        public string FileId
        {
            get { return _FileId; }
            set { _FileId = value; }
        }
        [DataMember(Order = 3)]
        public string Regional
        {
            get { return _Regional; }
            set { _Regional = value; }
        }
        [DataMember(Order = 4)]
        public string RegionalName
        {
            get { return _RegionalName; }
            set { _RegionalName = value; }
        }
        [DataMember(Order = 5)]
        public string BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }
        [DataMember(Order = 6)]
        public string BranchName
        {
            get { return _BranchName; }
            set { _BranchName = value; }
        }
        [DataMember(Order = 7)]
        public string MruId
        {
            get { return _MruId; }
            set { _MruId = value; }
        }
        [DataMember(Order = 8)]
        public string CaId
        {
            get { return _CaId; }
            set { _CaId = value; }
        }
        [DataMember(Order = 9)]
        public string CaName
        {
            get { return _CaName; }
            set { _CaName = value; }
        }
        [DataMember(Order = 10)]
        public DateTime? PayDt
        {
            get { return _PayDt; }
            set { _PayDt = value; }
        }
        [DataMember(Order = 11)]
        public DateTime? DueDt
        {
            get { return _DueDt; }
            set { _DueDt = value; }
        }
        [DataMember(Order = 12)]
        public decimal? OutSourceAmount
        {
            get { return _OutSourceAmount; }
            set { _OutSourceAmount = value; }
        }
        [DataMember(Order = 13)]
        public decimal? VatAmount
        {
            get { return _VatAmount; }
            set { _VatAmount = value; }
        }
        [DataMember(Order = 14)]
        public string RecNo
        {
            get { return _RecNo; }
            set { _RecNo = value; }
        }
        [DataMember(Order = 15)]
        public string Period
        {
            get { return _Period; }
            set { _Period = value; }
        }
        [DataMember(Order = 16)]
        public string CompanyId
        {
            get { return _CompanyId; }
            set { _CompanyId = value; }
        }
        [DataMember(Order = 17)]
        public string ActCode
        {
            get { return _ActCode; }
            set { _ActCode = value; }
        }
        [DataMember(Order = 18)]
        public string PosNo
        {
            get { return _PosNo; }
            set { _PosNo = value; }
        }
        [DataMember(Order = 19)]
        public string InvoiceNo
        {
            get { return _InvoiceNo; }
            set { _InvoiceNo = value; }
        }
        [DataMember(Order = 20)]
        public string UploadStatus
        {
            get { return _UploadStatus; }
            set { _UploadStatus = value; }
        }
        [DataMember(Order = 21)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 22)]
        public string PostBranchServerId
        {
            get { return _PostBranchServerId; }
            set { _PostBranchServerId = value; }
        }
        [DataMember(Order = 23)]
        public DateTime? PostDt
        {
            get { return _PostDt; }
            set { _PostDt = value; }
        }
        [DataMember(Order = 24)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 25)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 26)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 27)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }
    }
}
