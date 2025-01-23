using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class CashierWorkStatusInfo
    {
        private string _WorkId;
        private string _CashierId;
        private string _CashierName;
        private string _PosId;
        private string _TerminalCode;
        private string _Status;
        private DateTime? _OpenWorkDt;
        private DateTime? _CloseWorkDt;
        private string _CloseWorkBy;
        private string _BranchId;
        private int? _BaseLine;
        private string _MarkedBL;
        private DateTime? _PostDt;
        private string _SyncFlag;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string WorkId
        {
            get { return _WorkId; }
            set { _WorkId = value; }
        }
        [DataMember(Order = 2)]
        public string CashierId
        {
            get { return _CashierId; }
            set { _CashierId = value; }
        }
        [DataMember(Order = 3)]
        public string CashierName
        {
            get { return _CashierName; }
            set { _CashierName = value; }
        }
        [DataMember(Order = 4)]
        public string PosId
        {
            get { return _PosId; }
            set { _PosId = value; }
        }
        [DataMember(Order = 5)]
        public string TerminalCode
        {
            get { return _TerminalCode; }
            set { _TerminalCode = value; }
        }
        [DataMember(Order = 6)]
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        [DataMember(Order = 7)]
        public DateTime? OpenWorkDt
        {
            get { return _OpenWorkDt; }
            set { _OpenWorkDt = value; }
        }
        [DataMember(Order = 8)]
        public DateTime? CloseWorkDt
        {
            get { return _CloseWorkDt; }
            set { _CloseWorkDt = value; }
        }
        [DataMember(Order = 9)]
        public string CloseWorkBy
        {
            get { return _CloseWorkBy; }
            set { _CloseWorkBy = value; }
        }
        [DataMember(Order = 10)]
        public string BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }
        [DataMember(Order = 11)]
        public int? BaseLine
        {
            get { return _BaseLine; }
            set { _BaseLine = value; }
        }
        [DataMember(Order = 12)]
        public string MarkedBL
        {
            get { return _MarkedBL; }
            set { _MarkedBL = value; }
        }
        [DataMember(Order = 13)]
        public DateTime? PostDt
        {
            get { return _PostDt; }
            set { _PostDt = value; }
        }
        [DataMember(Order = 14)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 15)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 16)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 17)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 18)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }
    }
}
