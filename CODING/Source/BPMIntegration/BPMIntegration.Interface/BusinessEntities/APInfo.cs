using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class APInfo
    {
        private string _APId;
        private string _APPmId;
        private string _APDtId;
        private string _GLBankKey;
        private string _BankName;
        private string _ClearingAccNo;
        private string _RefNo;
        private string _CaId;
        private string _CaName;
        private decimal? _ChequeAmount;
        private decimal? _CashAmount;
        private decimal? _AdjAmount;
        private DateTime? _PaymentDt;
        private DateTime? _CancelDt;
        private string _CancelReason;
        private string _CancelCashierId;
        private string _CancelCashierName;
        private string _CashierId;
        private string _CashierName;
        private string _PosId;
        private string _TerminalCode;
        private int? _APQty;
        private string _SepCheque;
        private string _BranchId;
        private string _BranchName;
        private string _SyncFlag;
        private string _ExportedOnce;
        private string _PaidFlag;
        private string _CanceledFlag;
        private DateTime? _PostDt;
        private string _PostBranchServerId;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string APId
        {
            get { return _APId; }
            set { _APId = value; }
        }
        [DataMember(Order = 2)]
        public string APPmId
        {
            get { return _APPmId; }
            set { _APPmId = value; }
        }
        [DataMember(Order = 3)]
        public string APDtId
        {
            get { return _APDtId; }
            set { _APDtId = value; }
        }
        [DataMember(Order = 4)]
        public string GLBankKey
        {
            get { return _GLBankKey; }
            set { _GLBankKey = value; }
        }
        [DataMember(Order = 5)]
        public string BankName
        {
            get { return _BankName; }
            set { _BankName = value; }
        }
        [DataMember(Order = 6)]
        public string ClearingAccNo
        {
            get { return _ClearingAccNo; }
            set { _ClearingAccNo = value; }
        }
        [DataMember(Order = 7)]
        public string RefNo
        {
            get { return _RefNo; }
            set { _RefNo = value; }
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
        public decimal? ChequeAmount
        {
            get { return _ChequeAmount; }
            set { _ChequeAmount = value; }
        }
        [DataMember(Order = 11)]
        public decimal? CashAmount
        {
            get { return _CashAmount; }
            set { _CashAmount = value; }
        }
        [DataMember(Order = 12)]
        public decimal? AdjAmount
        {
            get { return _AdjAmount; }
            set { _AdjAmount = value; }
        }
        [DataMember(Order = 13)]
        public DateTime? PaymentDt
        {
            get { return _PaymentDt; }
            set { _PaymentDt = value; }
        }
        [DataMember(Order = 14)]
        public DateTime? CancelDt
        {
            get { return _CancelDt; }
            set { _CancelDt = value; }
        }
        [DataMember(Order = 15)]
        public string CancelReason
        {
            get { return _CancelReason; }
            set { _CancelReason = value; }
        }
        [DataMember(Order = 16)]
        public string CancelCashierId
        {
            get { return _CancelCashierId; }
            set { _CancelCashierId = value; }
        }
        [DataMember(Order = 17)]
        public string CancelCashierName
        {
            get { return _CancelCashierName; }
            set { _CancelCashierName = value; }
        }
        [DataMember(Order = 18)]
        public string CashierId
        {
            get { return _CashierId; }
            set { _CashierId = value; }
        }
        [DataMember(Order = 19)]
        public string CashierName
        {
            get { return _CashierName; }
            set { _CashierName = value; }
        }
        [DataMember(Order = 20)]
        public string PosId
        {
            get { return _PosId; }
            set { _PosId = value; }
        }
        [DataMember(Order = 21)]
        public string TerminalCode
        {
            get { return _TerminalCode; }
            set { _TerminalCode = value; }
        }
        [DataMember(Order = 22)]
        public int? APQty
        {
            get { return _APQty; }
            set { _APQty = value; }
        }
        [DataMember(Order = 23)]
        public string SepCheque
        {
            get { return _SepCheque; }
            set { _SepCheque = value; }
        }
        [DataMember(Order = 24)]
        public string BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }
        [DataMember(Order = 25)]
        public string BranchName
        {
            get { return _BranchName; }
            set { _BranchName = value; }
        }
        [DataMember(Order = 26)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 27)]
        public string ExportedOnce
        {
            get { return _ExportedOnce; }
            set { _ExportedOnce = value; }
        }
        [DataMember(Order = 28)]
        public string PaidFlag
        {
            get { return _PaidFlag; }
            set { _PaidFlag = value; }
        }
        [DataMember(Order = 29)]
        public string CanceledFlag
        {
            get { return _CanceledFlag; }
            set { _CanceledFlag = value; }
        }
        [DataMember(Order = 30)]
        public DateTime? PostDt
        {
            get { return _PostDt; }
            set { _PostDt = value; }
        }
        [DataMember(Order = 31)]
        public string PostBranchServerId
        {
            get { return _PostBranchServerId; }
            set { _PostBranchServerId = value; }
        }
        [DataMember(Order = 32)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 33)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 34)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 35)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }
    }
}
