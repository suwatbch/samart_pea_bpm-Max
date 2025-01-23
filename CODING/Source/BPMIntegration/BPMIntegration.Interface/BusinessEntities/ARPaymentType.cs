using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class ARPaymentType
    {
        private string _ARPtId;
        private string _PaymentId;
        private decimal? _Amount;
        private string _PtId;
        private decimal? _ChangeAmount;
        private decimal? _FeeAmount;
        private string _BankKey;
        private string _BankName;
        private string _GroupBankName;
        private string _ChqNo;
        private string _ChqAccNo;
        private DateTime? _ChqDt;
        private string _DraftFlag;
        private string _CashierChequeFlag;
        private string _TranfAccNo;
        private DateTime? _TranfDt;
        private string _CancelARPtId;
        private string _ClearingAccNo;
        private string _BranchId;
        private string _SyncFlag;
        private DateTime? _PostDt;
        private string _PostBranchServerId;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string ARPtId
        {
            get { return _ARPtId; }
            set { _ARPtId = value; }
        }
        [DataMember(Order = 2)]
        public string PaymentId
        {
            get { return _PaymentId; }
            set { _PaymentId = value; }
        }
        [DataMember(Order = 3)]
        public decimal? Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        [DataMember(Order = 4)]
        public string PtId
        {
            get { return _PtId; }
            set { _PtId = value; }
        }
        [DataMember(Order = 5)]
        public decimal? ChangeAmount
        {
            get { return _ChangeAmount; }
            set { _ChangeAmount = value; }
        }
        [DataMember(Order = 6)]
        public decimal? FeeAmount
        {
            get { return _FeeAmount; }
            set { _FeeAmount = value; }
        }
        [DataMember(Order = 7)]
        public string BankKey
        {
            get { return _BankKey; }
            set { _BankKey = value; }
        }
        [DataMember(Order = 8)]
        public string BankName
        {
            get { return _BankName; }
            set { _BankName = value; }
        }
        [DataMember(Order = 9)]
        public string GroupBankName
        {
            get { return _GroupBankName; }
            set { _GroupBankName = value; }
        }
        [DataMember(Order = 10)]
        public string ChqNo
        {
            get { return _ChqNo; }
            set { _ChqNo = value; }
        }
        [DataMember(Order = 11)]
        public string ChqAccNo
        {
            get { return _ChqAccNo; }
            set { _ChqAccNo = value; }
        }
        [DataMember(Order = 12)]
        public DateTime? ChqDt
        {
            get { return _ChqDt; }
            set { _ChqDt = value; }
        }
        [DataMember(Order = 13)]
        public string DraftFlag
        {
            get { return _DraftFlag; }
            set { _DraftFlag = value; }
        }
        [DataMember(Order = 14)]
        public string CashierChequeFlag
        {
            get { return _CashierChequeFlag; }
            set { _CashierChequeFlag = value; }
        }
        [DataMember(Order = 15)]
        public string TranfAccNo
        {
            get { return _TranfAccNo; }
            set { _TranfAccNo = value; }
        }
        [DataMember(Order = 16)]
        public DateTime? TranfDt
        {
            get { return _TranfDt; }
            set { _TranfDt = value; }
        }
        [DataMember(Order = 17)]
        public string CancelARPtId
        {
            get { return _CancelARPtId; }
            set { _CancelARPtId = value; }
        }
        [DataMember(Order = 18)]
        public string ClearingAccNo
        {
            get { return _ClearingAccNo; }
            set { _ClearingAccNo = value; }
        }
        [DataMember(Order = 19)]
        public string BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }
        [DataMember(Order = 20)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 21)]
        public DateTime? PostDt
        {
            get { return _PostDt; }
            set { _PostDt = value; }
        }
        [DataMember(Order = 22)]
        public string PostBranchServerId
        {
            get { return _PostBranchServerId; }
            set { _PostBranchServerId = value; }
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
