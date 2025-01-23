using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class AgencyCommissionRateInfo
    {
        private int? _RtId;
        private string _BranchId;
        private decimal? _HouseRegRate;
        private decimal? _HouseGrpRate;
        private decimal? _CorpRegRate;
        private decimal? _CorpGrpRate;
        private decimal? _GovRegRate;
        private decimal? _GovGrpRate;
        private decimal? _BillingNinetyPercent;
        private decimal? _BillingNinetyNinePercent;
        private decimal? _BillingHundredPercent;
        private decimal? _MaxInvPercent;
        private decimal? _InvRate;
        private decimal? _InvPastThreeMonthRate;
        private decimal? _FineRatePerBill;
        private int? _CmCountBlock;
        private int? _CmCountLimit;
        private decimal? _VatRate;
        private decimal? _CollectedPercent;
        private string _PenaltyWaive;
        private int? _CaCount;
        private decimal? _UpperRate;
        private decimal? _LowerRate;
        private decimal? _TaxRate;
        private string _SyncFlag;
        private string _ModifiedBy;
        private DateTime? _ModifiedDt;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public int? RtId
        {
            get { return _RtId; }
            set { _RtId = value; }
        }
        [DataMember(Order = 2)]
        public string BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }
        [DataMember(Order = 3)]
        public decimal? HouseRegRate
        {
            get { return _HouseRegRate; }
            set { _HouseRegRate = value; }
        }
        [DataMember(Order = 4)]
        public decimal? HouseGrpRate
        {
            get { return _HouseGrpRate; }
            set { _HouseGrpRate = value; }
        }
        [DataMember(Order = 5)]
        public decimal? CorpRegRate
        {
            get { return _CorpRegRate; }
            set { _CorpRegRate = value; }
        }
        [DataMember(Order = 6)]
        public decimal? CorpGrpRate
        {
            get { return _CorpGrpRate; }
            set { _CorpGrpRate = value; }
        }
        [DataMember(Order = 7)]
        public decimal? GovRegRate
        {
            get { return _GovRegRate; }
            set { _GovRegRate = value; }
        }
        [DataMember(Order = 8)]
        public decimal? GovGrpRate
        {
            get { return _GovGrpRate; }
            set { _GovGrpRate = value; }
        }
        [DataMember(Order = 9)]
        public decimal? BillingNinetyPercent
        {
            get { return _BillingNinetyPercent; }
            set { _BillingNinetyPercent = value; }
        }
        [DataMember(Order = 10)]
        public decimal? BillingNinetyNinePercent
        {
            get { return _BillingNinetyNinePercent; }
            set { _BillingNinetyNinePercent = value; }
        }
        [DataMember(Order = 11)]
        public decimal? BillingHundredPercent
        {
            get { return _BillingHundredPercent; }
            set { _BillingHundredPercent = value; }
        }
        [DataMember(Order = 12)]
        public decimal? MaxInvPercent
        {
            get { return _MaxInvPercent; }
            set { _MaxInvPercent = value; }
        }
        [DataMember(Order = 13)]
        public decimal? InvRate
        {
            get { return _InvRate; }
            set { _InvRate = value; }
        }
        [DataMember(Order = 14)]
        public decimal? InvPastThreeMonthRate
        {
            get { return _InvPastThreeMonthRate; }
            set { _InvPastThreeMonthRate = value; }
        }
        [DataMember(Order = 15)]
        public decimal? FineRatePerBill
        {
            get { return _FineRatePerBill; }
            set { _FineRatePerBill = value; }
        }
        [DataMember(Order = 16)]
        public int? CmCountBlock
        {
            get { return _CmCountBlock; }
            set { _CmCountBlock = value; }
        }
        [DataMember(Order = 17)]
        public int? CmCountLimit
        {
            get { return _CmCountLimit; }
            set { _CmCountLimit = value; }
        }
        [DataMember(Order = 18)]
        public decimal? VatRate
        {
            get { return _VatRate; }
            set { _VatRate = value; }
        }
        [DataMember(Order = 19)]
        public decimal? CollectedPercent
        {
            get { return _CollectedPercent; }
            set { _CollectedPercent = value; }
        }
        [DataMember(Order = 20)]
        public string PenaltyWaive
        {
            get { return _PenaltyWaive; }
            set { _PenaltyWaive = value; }
        }
        [DataMember(Order = 21)]
        public int? CaCount
        {
            get { return _CaCount; }
            set { _CaCount = value; }
        }
        [DataMember(Order = 22)]
        public decimal? UpperRate
        {
            get { return _UpperRate; }
            set { _UpperRate = value; }
        }
        [DataMember(Order = 23)]
        public decimal? LowerRate
        {
            get { return _LowerRate; }
            set { _LowerRate = value; }
        }
        [DataMember(Order = 24)]
        public decimal? TaxRate
        {
            get { return _TaxRate; }
            set { _TaxRate = value; }
        }
        [DataMember(Order = 25)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 26)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 27)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 28)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 29)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

    }
}
