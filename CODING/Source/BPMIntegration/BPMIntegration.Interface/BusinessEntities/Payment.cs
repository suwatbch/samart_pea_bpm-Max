using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class Payment
    {
        private string _PaymentId;
        private DateTime? _PaymentDt;
        private string _PosId;
        private string _TerminalCode;
        private string _CashierId;
        private string _CashierName;
        private string _BranchId;
        private string _BranchName;
        private string _OriginalPaymentId;
        private Byte? _PaidChannel;
        private Byte? _CmScope;
        private string _WorkId;
        private Byte? _WorkFlag;
        private string _SyncFlag;
        private DateTime? _PostDt;
        private string _PostBranchServerId;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string PaymentId
        {
            get { return _PaymentId; }
            set { _PaymentId = value; }
        }
        [DataMember(Order = 2)]
        public DateTime? PaymentDt
        {
            get { return _PaymentDt; }
            set { _PaymentDt = value; }
        }
        [DataMember(Order = 3)]
        public string PosId
        {
            get { return _PosId; }
            set { _PosId = value; }
        }
        [DataMember(Order = 4)]
        public string TerminalCode
        {
            get { return _TerminalCode; }
            set { _TerminalCode = value; }
        }
        [DataMember(Order = 5)]
        public string CashierId
        {
            get { return _CashierId; }
            set { _CashierId = value; }
        }
        [DataMember(Order = 6)]
        public string CashierName
        {
            get { return _CashierName; }
            set { _CashierName = value; }
        }
        [DataMember(Order = 7)]
        public string BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }
        [DataMember(Order = 8)]
        public string BranchName
        {
            get { return _BranchName; }
            set { _BranchName = value; }
        }
        [DataMember(Order = 9)]
        public string OriginalPaymentId
        {
            get { return _OriginalPaymentId; }
            set { _OriginalPaymentId = value; }
        }
        [DataMember(Order = 10)]
        public Byte? PaidChannel
        {
            get { return _PaidChannel; }
            set { _PaidChannel = value; }
        }
        [DataMember(Order = 11)]
        public Byte? CmScope
        {
            get { return _CmScope; }
            set { _CmScope = value; }
        }
        [DataMember(Order = 12)]
        public string WorkId
        {
            get { return _WorkId; }
            set { _WorkId = value; }
        }
        [DataMember(Order = 13)]
        public Byte? WorkFlag
        {
            get { return _WorkFlag; }
            set { _WorkFlag = value; }
        }
        [DataMember(Order = 14)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 15)]
        public DateTime? PostDt
        {
            get { return _PostDt; }
            set { _PostDt = value; }
        }
        [DataMember(Order = 16)]
        public string PostBranchServerId
        {
            get { return _PostBranchServerId; }
            set { _PostBranchServerId = value; }
        }
        [DataMember(Order = 17)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 18)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 19)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 20)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }
    }
}
