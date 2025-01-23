using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Infrastructure.Interface.BusinessEntities
{
    [DataContract]
    public class OpenWorkParam
    {
        string _cashierId;
        [DataMember(Order = 1)]
        public string CashierId
        {
            get { return _cashierId; }
            set { _cashierId = value; }
        }

        string _cashierName;
        [DataMember(Order = 2)]
        public string CashierName
        {
            get { return _cashierName; }
            set { _cashierName = value; }
        }

        string _branchId;
        [DataMember(Order = 3)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }

        string _posId;
        [DataMember(Order = 4)]
        public string PosId
        {
            get { return _posId; }
            set { _posId = value; }
        }

        string _TerminalCode;
        [DataMember(Order = 5)]
        public string TerminalCode
        {
            get { return _TerminalCode; }
            set { _TerminalCode = value; }
        }

        string _flowId;
        [DataMember(Order = 6)]
        public string FlowId
        {
            get { return _flowId; }
            set { _flowId = value; }
        }

        string _flowType;
        [DataMember(Order = 7)]
        public string FlowType
        {
            get { return _flowType; }
            set { _flowType = value; }
        }

        string _status;
        [DataMember(Order = 8)]
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        string _modifiedBy;
        [DataMember(Order = 9)]
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        string _input;
        [DataMember(Order = 10)]
        public string Input
        {
            get { return _input; }
            set { _input = value; }
        }

        string _postedBranchId;
        [DataMember(Order = 11)]
        public string PostedBranchId
        {
            get { return _postedBranchId; }
            set { _postedBranchId = value; }
        }

        bool _IsSystemInit;
        [DataMember(Order = 12)]
        public bool IsSystemInit
        {
            get { return _IsSystemInit; }
            set { _IsSystemInit = value; }
        }

        DateTime _paymentDt;
        [DataMember(Order = 13)]
        public DateTime PaymentDt
        {
            get { return _paymentDt; }
            set { _paymentDt = value; }
        }

    }
}