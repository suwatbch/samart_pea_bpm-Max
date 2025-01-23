using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class Terminal
    {
        private string _TerminalId;
        private string _TerminalCode;
        private string _BranchId;
        private string _TaxCode;
        private string _IP;
        private string _SyncFlag;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string TerminalId
        {
            get { return _TerminalId; }
            set { _TerminalId = value; }
        }
        [DataMember(Order = 2)]
        public string TerminalCode
        {
            get { return _TerminalCode; }
            set { _TerminalCode = value; }
        }
        [DataMember(Order = 3)]
        public string BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }
        [DataMember(Order = 4)]
        public string TaxCode
        {
            get { return _TaxCode; }
            set { _TaxCode = value; }
        }
        [DataMember(Order = 5)]
        public string IP
        {
            get { return _IP; }
            set { _IP = value; }
        }
        [DataMember(Order = 6)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 7)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 8)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 9)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 10)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }
    }
}
