using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports
{
    [DataContract]
    public class CAC12Report
    {
        private string _branchId;
        private string _branchName;
        private string _rateTypeId;
        private int? _quantity;
        private decimal? _baseAmount;
        private decimal? _ftAmount;
        private decimal? _vatAmount;
        private decimal? _gAmount;


        [DataMember(Order=1)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }


        [DataMember(Order=2)]
        public string BranchName
        {
            get { return _branchName; }
            set { _branchName = value; }
        }


        [DataMember(Order=3)]
        public string RateTypeId
        {
            get { return _rateTypeId; }
            set { _rateTypeId = value; }
        }


        [DataMember(Order=4)]
        public int? Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }


        [DataMember(Order=5)]
        public decimal? BaseAmount
        {
            get { return _baseAmount; }
            set { _baseAmount = value; }
        }


        [DataMember(Order=6)]
        public decimal? FtAmount
        {
            get { return _ftAmount; }
            set { _ftAmount = value; }
        }


        [DataMember(Order=7)]
        public decimal? VatAmount
        {
            get { return _vatAmount; }
            set { _vatAmount = value; }
        }


        [DataMember(Order=8)]
        public decimal? GAmount
        {
            get { return _gAmount; }
            set { _gAmount = value; }
        }
    }
}
