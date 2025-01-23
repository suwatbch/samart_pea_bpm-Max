using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class DepositBillBookAmountInfo
    {
        private bool _isOverLimit = false;
        private decimal? _advPaidAmount = 0;
        private decimal? _totalBookAmount = 0;
        private decimal _totalBillAmount = 0;
        private decimal? _totalAsset = 0;
        private decimal? _grandBookTotal = 0;
        private decimal? _remainAmount = 0;


        [DataMember(Order=1)]
        public decimal? AdvPaidAmount
        {
            get { return _advPaidAmount; }
            set { _advPaidAmount = value; }
        }


        [DataMember(Order=2)]
        public bool IsOverLimit
        {
            get { return _isOverLimit; }
            set { _isOverLimit = value; }
        }


        [DataMember(Order=3)]
        public decimal? TotalBookAmount
        {
            get { return _totalBookAmount; }
            set { _totalBookAmount = value; }
        }


        [DataMember(Order=4)]
        public decimal TotalBillAmount
        {
            get { return _totalBillAmount; }
            set { _totalBillAmount = value; }
        }


        [DataMember(Order=5)]
        public decimal? TotalAsset
        {
            get { return _totalAsset; }
            set { _totalAsset = value; }
        }


        [DataMember(Order=6)]
        public decimal? GrandBookTotal
        {
            get { return _grandBookTotal; }
            set { _grandBookTotal = value; }
        }


        [DataMember(Order=7)]
        public decimal? RemainAmount
        {
            get { return _remainAmount; }
            set { _remainAmount = value; }
        }

    }
}
