using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class AgencyCommissionInfo
    {
        private string _CmId;
        private string _BranchId;
        private DateTime? _CalDt;
        private int? _CalCount;
        private decimal? _CmAmount;
        private decimal? _BaseCmAmount;
        private decimal? _SpecialCmAmount;
        private decimal? _InvCmAmount;
        private string _OverNinety;
        private decimal? _FineAmount;
        private decimal? _TaxAmount;
        private decimal? _VatAmount;
        private decimal? _TransCost;
        private decimal? _FarLandHelp;
        private decimal? _SpecialMoney;
        private int? _RtId;
        private string _FineEnabled;
        private DateTime? _PostDt;
        private string _PostBranchServerId;
        private string _SyncFlag;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string CmId
        {
            get { return _CmId; }
            set { _CmId = value; }
        }
        [DataMember(Order = 2)]
        public string BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }
        [DataMember(Order = 3)]
        public DateTime? CalDt
        {
            get { return _CalDt; }
            set { _CalDt = value; }
        }
        [DataMember(Order = 4)]
        public int? CalCount
        {
            get { return _CalCount; }
            set { _CalCount = value; }
        }
        [DataMember(Order = 5)]
        public decimal? CmAmount
        {
            get { return _CmAmount; }
            set { _CmAmount = value; }
        }
        [DataMember(Order = 6)]
        public decimal? BaseCmAmount
        {
            get { return _BaseCmAmount; }
            set { _BaseCmAmount = value; }
        }
        [DataMember(Order = 7)]
        public decimal? SpecialCmAmount
        {
            get { return _SpecialCmAmount; }
            set { _SpecialCmAmount = value; }
        }
        [DataMember(Order = 8)]
        public decimal? InvCmAmount
        {
            get { return _InvCmAmount; }
            set { _InvCmAmount = value; }
        }
        [DataMember(Order = 9)]
        public string OverNinety
        {
            get { return _OverNinety; }
            set { _OverNinety = value; }
        }
        [DataMember(Order = 10)]
        public decimal? FineAmount
        {
            get { return _FineAmount; }
            set { _FineAmount = value; }
        }
        [DataMember(Order = 11)]
        public decimal? TaxAmount
        {
            get { return _TaxAmount; }
            set { _TaxAmount = value; }
        }
        [DataMember(Order = 12)]
        public decimal? VatAmount
        {
            get { return _VatAmount; }
            set { _VatAmount = value; }
        }
        [DataMember(Order = 13)]
        public decimal? TransCost
        {
            get { return _TransCost; }
            set { _TransCost = value; }
        }
        [DataMember(Order = 14)]
        public decimal? FarLandHelp
        {
            get { return _FarLandHelp; }
            set { _FarLandHelp = value; }
        }
        [DataMember(Order = 15)]
        public decimal? SpecialMoney
        {
            get { return _SpecialMoney; }
            set { _SpecialMoney = value; }
        }
        [DataMember(Order = 16)]
        public int? RtId
        {
            get { return _RtId; }
            set { _RtId = value; }
        }
        [DataMember(Order = 17)]
        public string FineEnabled
        {
            get { return _FineEnabled; }
            set { _FineEnabled = value; }
        }
        [DataMember(Order = 18)]
        public DateTime? PostDt
        {
            get { return _PostDt; }
            set { _PostDt = value; }
        }
        [DataMember(Order = 19)]
        public string PostBranchServerId
        {
            get { return _PostBranchServerId; }
            set { _PostBranchServerId = value; }
        }
        [DataMember(Order = 20)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 21)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 22)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 23)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 24)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

    }
}
