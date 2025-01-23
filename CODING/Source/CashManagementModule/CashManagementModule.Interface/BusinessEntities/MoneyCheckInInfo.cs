using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.CashManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class MoneyCheckInInfo
    {
        string _sapRefNo;
        [DataMember(Order=1)]
        public string SAPRefNo
        {
            get { return _sapRefNo; }
            set { _sapRefNo = value; }
        }

        string _dtId;
        [DataMember(Order=2)]
        public string DtId
        {
            get { return _dtId; }
            set { _dtId = value; }
        }

        string _posId;
        [DataMember(Order=3)]
        public string PosId
        {
            get { return _posId; }
            set { _posId = value; }
        }

        string _cashierId;
        [DataMember(Order=4)]
        public string CashierId
        {
            get { return _cashierId; }
            set { _cashierId = value; }
        }

        string _cashierName;
        [DataMember(Order=5)]
        public string CashierName
        {
            get { return _cashierName; }
            set { _cashierName = value; }
        }

        string _branchId;
        [DataMember(Order=6)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }

        DateTime? _paymentDt;
        [DataMember(Order=7)]
        public DateTime? PaymentDt
        {
            get { return _paymentDt; }
            set { _paymentDt = value; }
        }

        string _pmId;
        [DataMember(Order=8)]
        public string PmId
        {
            get { return _pmId; }
            set { _pmId = value; }
        }

        string _pending;
        [DataMember(Order=9)]
        public string Pending
        {
            get { return _pending; }
            set { _pending = value; }
        }

        List<PaymentMethodInfo> _paymentMethodList = new List<PaymentMethodInfo>();
        [DataMember(Order=10)]
        public List<PaymentMethodInfo> PaymentMethodList
        {
            get { return _paymentMethodList; }
            set { _paymentMethodList = value; }
        }

        decimal? _totalAmount;
        [DataMember(Order=11)]
        public decimal? TotalAmount
        {
            get { return _totalAmount; }
            set { _totalAmount = value; }
                
        }

        decimal? _AdjAmount;
        [DataMember(Order=12)]
        public decimal? AdjAmount
        {
            get { return _AdjAmount; }
            set { _AdjAmount = value; }
        }

        decimal? _Amount;
        [DataMember(Order=13)]
        public decimal? Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        string _modifiedBy;
        [DataMember(Order=14)]
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        string _postedBranchId;
        [DataMember(Order=15)]
        public string PostedBranchId
        {
            get { return _postedBranchId; }
            set { _postedBranchId = value; }
        }

        //system startup
        string _workId;
        [DataMember(Order=16)]
        public string WorkId
        {
            get { return _workId; }
            set { _workId = value; }
        }

        string _flowCat;
        [DataMember(Order=17)]
        public string FlowCat
        {
            get { return _flowCat; }
            set { _flowCat = value; }
        }

        string _flowType;
        [DataMember(Order=18)]
        public string FlowType
        {
            get { return _flowType; }
            set { _flowType = value; }
        }

        string _flowdesc;
        [DataMember(Order = 19)]
        public string FlowDesc
        {
            get { return _flowdesc; }
            set { _flowdesc = value; }
        }

        bool _editMode = false;  //default
        [DataMember(Order=20)]
        public bool EditMode
        {
            get { return _editMode; }
            set { _editMode = value; }
        }
    }
}
