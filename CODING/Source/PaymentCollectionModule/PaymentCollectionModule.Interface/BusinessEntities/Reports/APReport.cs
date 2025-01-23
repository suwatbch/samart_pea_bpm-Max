using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports
{
    [DataContract]
    public class APReport
    {
        private string _branchId;
        private string _paymentVoucher;
        private string _caId;
        private string _caName;
        private string _posId;
        private string _cashierId;

        private DateTime? paymentDt;
        private decimal? _adjAmount;
        private decimal? _gAmount;


        [DataMember(Order=1)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }


        [DataMember(Order=2)]
        public string PaymentVoucher
        {
            get { return _paymentVoucher; }
            set { _paymentVoucher = value; }
        }


        [DataMember(Order=3)]
        public string CaId
        {
            get { return _caId; }
            set { _caId = value; }
        }


        [DataMember(Order=4)]
        public string CaName
        {
            get { return _caName; }
            set { _caName = value; }
        }


        [DataMember(Order=5)]
        public string PosId
        {
            get { return _posId; }
            set { _posId = value; }
        }


        [DataMember(Order=6)]
        public string CashierId
        {
            get { return _cashierId; }
            set { _cashierId = value; }
        }


        [DataMember(Order=7)]
        public DateTime? PaymentDt
        {
            get { return paymentDt; }
            set { paymentDt = value; }
        }


        [DataMember(Order=8)]
        public decimal? AdjAmount
        {
            get { return _adjAmount; }
            set { _adjAmount = value; }
        }


        [DataMember(Order=9)]
        public decimal? GAmount
        {
            get { return _gAmount; }
            set { _gAmount = value; }
        }

    }
}

//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports
//{
//    class APReport
//    {
//    }
//}
