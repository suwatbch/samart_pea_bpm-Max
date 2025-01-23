using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class CashierMoneyFlowInfo
    {
        private string _FlowId;
        private string _FlowType;
        private string _FlowDesc;
        private string _FlowCat;
        private string _GLBankKey;
        private string _BankName;
        private string _GLAccountNo;
        private decimal? _CashAmt;
        private decimal? _ChequeAmt;
        private string _WorkId;
        private string _CashierId;
        private string _BranchId;
        private string _TransferId;
        private DateTime? _PostDt;
        private string _SyncFlag;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string FlowId
        {
            get { return _FlowId; }
            set { _FlowId = value; }
        }
        [DataMember(Order = 2)]
        public string FlowType
        {
            get { return _FlowType; }
            set { _FlowType = value; }
        }
        [DataMember(Order = 3)]
        public string FlowDesc
        {
            get { return _FlowDesc; }
            set { _FlowDesc = value; }
        }
        [DataMember(Order = 4)]
        public string FlowCat
        {
            get { return _FlowCat; }
            set { _FlowCat = value; }
        }
        [DataMember(Order = 5)]
        public string GLBankKey
        {
            get { return _GLBankKey; }
            set { _GLBankKey = value; }
        }
        [DataMember(Order = 6)]
        public string BankName
        {
            get { return _BankName; }
            set { _BankName = value; }
        }
        [DataMember(Order = 7)]
        public string GLAccountNo
        {
            get { return _GLAccountNo; }
            set { _GLAccountNo = value; }
        }
        [DataMember(Order = 8)]
        public decimal? CashAmt
        {
            get { return _CashAmt; }
            set { _CashAmt = value; }
        }
        [DataMember(Order = 9)]
        public decimal? ChequeAmt
        {
            get { return _ChequeAmt; }
            set { _ChequeAmt = value; }
        }
        [DataMember(Order = 10)]
        public string WorkId
        {
            get { return _WorkId; }
            set { _WorkId = value; }
        }
        [DataMember(Order = 11)]
        public string CashierId
        {
            get { return _CashierId; }
            set { _CashierId = value; }
        }
        [DataMember(Order = 12)]
        public string BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }
        [DataMember(Order = 13)]
        public string TransferId
        {
            get { return _TransferId; }
            set { _TransferId = value; }
        }
        [DataMember(Order = 14)]
        public DateTime? PostDt
        {
            get { return _PostDt; }
            set { _PostDt = value; }
        }
        [DataMember(Order = 15)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
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
