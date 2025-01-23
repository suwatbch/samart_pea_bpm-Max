using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.CashManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CancelMoneyCheckedInInfo
    {
        string _workId;
        [DataMember(Order=1)]
        public string WorkId
        {
            get { return _workId; }
            set { _workId = value; }
        }

        string _sapRefNo;
        [DataMember(Order=2)]
        public string SapRefNo
        {
            get { return _sapRefNo; }
            set { _sapRefNo = value; }
        }

        DateTime? _paymentDt;
        [DataMember(Order=3)]
        public DateTime? PaymentDt
        {
            get { return _paymentDt; }
            set { _paymentDt = value; }
        }

        string _posId;
        [DataMember(Order=4)]
        public string PosId
        {
            get { return _posId; }
            set { _posId = value; }
        }

        string _cashierId;
        [DataMember(Order=5)]
        public string CashierId
        {
            get { return _cashierId; }
            set { _cashierId = value; }
        }

        string _cashierName;
        [DataMember(Order=6)]
        public string CashierName
        {
            get { return _cashierName; }
            set { _cashierName = value; }
        }

        string _branchId;
        [DataMember(Order=7)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }

        string _modifiedBy;
        [DataMember(Order=8)]
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        string _postedBranchId;
        [DataMember(Order=9)]
        public string PostedBranchId
        {
            get { return _postedBranchId; }
            set { _postedBranchId = value; }
        }
    }
}
