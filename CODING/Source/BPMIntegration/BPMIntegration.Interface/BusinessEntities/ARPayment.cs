using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class ARPayment
    {
        private string _ARPmId;
        private string _InvoiceNo;
        private string _PmId;
        private string _DocType;
        private decimal? _Qty;
        private decimal? _VatAmount;
        private decimal? _GAmount;
        private decimal? _AdjAmount;
        private string _CancelARPmId;
        private DateTime? _PaymentDt;
        private string _Pending;
        private int? _PaidChannel;
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
        public string ARPmId
        {
            get { return _ARPmId; }
            set { _ARPmId = value; }
        }
        [DataMember(Order = 2)]
        public string InvoiceNo
        {
            get { return _InvoiceNo; }
            set { _InvoiceNo = value; }
        }
        [DataMember(Order = 3)]
        public string PmId
        {
            get { return _PmId; }
            set { _PmId = value; }
        }
        [DataMember(Order = 4)]
        public string DocType
        {
            get { return _DocType; }
            set { _DocType = value; }
        }
        [DataMember(Order = 5)]
        public decimal? Qty
        {
            get { return _Qty; }
            set { _Qty = value; }
        }
        [DataMember(Order = 6)]
        public decimal? VatAmount
        {
            get { return _VatAmount; }
            set { _VatAmount = value; }
        }
        [DataMember(Order = 7)]
        public decimal? GAmount
        {
            get { return _GAmount; }
            set { _GAmount = value; }
        }
        [DataMember(Order = 8)]
        public decimal? AdjAmount
        {
            get { return _AdjAmount; }
            set { _AdjAmount = value; }
        }
        [DataMember(Order = 9)]
        public string CancelARPmId
        {
            get { return _CancelARPmId; }
            set { _CancelARPmId = value; }
        }
        [DataMember(Order = 10)]
        public DateTime? PaymentDt
        {
            get { return _PaymentDt; }
            set { _PaymentDt = value; }
        }
        [DataMember(Order = 11)]
        public string Pending
        {
            get { return _Pending; }
            set { _Pending = value; }
        }
        [DataMember(Order = 12)]
        public int? PaidChannel
        {
            get { return _PaidChannel; }
            set { _PaidChannel = value; }
        }
        [DataMember(Order = 13)]
        public string BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }
        [DataMember(Order = 14)]
        public string TechBranchId
        {
            get { return _TechBranchId; }
            set { _TechBranchId = value; }
        }
        [DataMember(Order = 15)]
        public string CommBranchId
        {
            get { return _CommBranchId; }
            set { _CommBranchId = value; }
        }
        [DataMember(Order = 16)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
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
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 20)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 21)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 22)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

    }
}
