using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class BillStatusInfoInfo
    {
        private string _InvoiceNo;
        private string _BranchId;
        private string _MruId;
        private string _CaId;
        private string _Period;
        private string _AbsId;
        private string _AboId;
        private string _PmId;
        private string _AllowRepeated;
        private string _InBook;
        private string _RateCatId;
        private string _PaidBy;
        private decimal? _Ft;
        private decimal? _Vat;
        private decimal? _BaseAmount;
        private decimal? _TotalAmount;
        private string _PaidType;
        private string _PrintBranch;
        private string _OrgDoc;
        private DateTime? _PostDt;
        private string _PostBranchServerId;
        private string _SyncFlag;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string InvoiceNo
        {
            get { return _InvoiceNo; }
            set { _InvoiceNo = value; }
        }
        [DataMember(Order = 2)]
        public string BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }
        [DataMember(Order = 3)]
        public string MruId
        {
            get { return _MruId; }
            set { _MruId = value; }
        }
        [DataMember(Order = 4)]
        public string CaId
        {
            get { return _CaId; }
            set { _CaId = value; }
        }
        [DataMember(Order = 5)]
        public string Period
        {
            get { return _Period; }
            set { _Period = value; }
        }
        [DataMember(Order = 6)]
        public string AbsId
        {
            get { return _AbsId; }
            set { _AbsId = value; }
        }
        [DataMember(Order = 7)]
        public string AboId
        {
            get { return _AboId; }
            set { _AboId = value; }
        }
        [DataMember(Order = 8)]
        public string PmId
        {
            get { return _PmId; }
            set { _PmId = value; }
        }
        [DataMember(Order = 9)]
        public string AllowRepeated
        {
            get { return _AllowRepeated; }
            set { _AllowRepeated = value; }
        }
        [DataMember(Order = 10)]
        public string InBook
        {
            get { return _InBook; }
            set { _InBook = value; }
        }
        [DataMember(Order = 11)]
        public string RateCatId
        {
            get { return _RateCatId; }
            set { _RateCatId = value; }
        }
        [DataMember(Order = 12)]
        public string PaidBy
        {
            get { return _PaidBy; }
            set { _PaidBy = value; }
        }
        [DataMember(Order = 13)]
        public decimal? Ft
        {
            get { return _Ft; }
            set { _Ft = value; }
        }
        [DataMember(Order = 14)]
        public decimal? Vat
        {
            get { return _Vat; }
            set { _Vat = value; }
        }
        [DataMember(Order = 15)]
        public decimal? BaseAmount
        {
            get { return _BaseAmount; }
            set { _BaseAmount = value; }
        }
        [DataMember(Order = 16)]
        public decimal? TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount = value; }
        }
        [DataMember(Order = 17)]
        public string PaidType
        {
            get { return _PaidType; }
            set { _PaidType = value; }
        }
        [DataMember(Order = 18)]
        public string PrintBranch
        {
            get { return _PrintBranch; }
            set { _PrintBranch = value; }
        }
        [DataMember(Order = 19)]
        public string OrgDoc
        {
            get { return _OrgDoc; }
            set { _OrgDoc = value; }
        }
        [DataMember(Order = 20)]
        public DateTime? PostDt
        {
            get { return _PostDt; }
            set { _PostDt = value; }
        }
        [DataMember(Order = 21)]
        public string PostBranchServerId
        {
            get { return _PostBranchServerId; }
            set { _PostBranchServerId = value; }
        }
        [DataMember(Order = 22)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 23)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 24)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 25)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 26)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

    }
}
