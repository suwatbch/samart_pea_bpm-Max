using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class APEntity
    {
        private string _APId;
        private string _APPmId;
        private string _paymentVoucher;
        private string _caId;
        private string _caName;
        private decimal? _gAmount;
        private decimal? _adjAmount;
        private DateTime? _paymentDt;
        private DateTime? _cancelDt;
        private string _cancelReason;
        private int? _apQty;
        private string _posId;
        private string _cashierId;
        private string _branchId;
        private string _fullname;
        private string _cashierName;


        [DataMember(Order=1)]
        public string APId
        {
            get { return _APId; }
            set { _APId = value; }
        }

        [DataMember(Order=2)]
        public string APPmId
        {
            get { return _APPmId; }
            set { _APPmId = value; }
        }

        [DataMember(Order=3)]
        public string PaymentVoucher
        {
            get { return _paymentVoucher; }
            set { _paymentVoucher = value; }
        }

        [DataMember(Order=4)]
        public string CaId
        {
            get { return _caId; }
            set { _caId = value; }
        }

        [DataMember(Order=5)]
        public string CaName
        {
            get { return _caName; }
            set { _caName = value; }
        }

        [DataMember(Order=6)]
        public string Fullname
        {
            get { return _fullname; }
            set { _fullname = value; }
        }

        [DataMember(Order=7)]
        public string CashierName
        {
            get { return _cashierName; }
            set { _cashierName = value; }
        }

        [DataMember(Order=8)]
        public decimal? GAmount
        {
            get { return _gAmount; }
            set { _gAmount = value; }
        }

        [DataMember(Order=9)]
        public decimal? AdjAmount
        {
            get { return _adjAmount; }
            set { _adjAmount = value; }
        }

        [DataMember(Order=10)]
        public DateTime? PaymentDt
        {
            get { return _paymentDt; }
            set { _paymentDt = value; }
        }

        [DataMember(Order=11)]
        public DateTime? CancelDt
        {
            get { return _cancelDt; }
            set { _cancelDt = value; }
        }

        [DataMember(Order=12)]
        public string CancelReason
        {
            get { return _cancelReason; }
            set { _cancelReason = value; }
        }

        [DataMember(Order=13)]
        public int? APQty
        {
            get { return _apQty; }
            set { _apQty = value; }
        }

        [DataMember(Order=14)]
        public string PosId
        {
            get { return _posId; }
            set { _posId = value; }
        }

        [DataMember(Order=15)]
        public string CashierId
        {
            get { return _cashierId; }
            set { _cashierId = value; }
        }

        [DataMember(Order=16)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }
    }
}
