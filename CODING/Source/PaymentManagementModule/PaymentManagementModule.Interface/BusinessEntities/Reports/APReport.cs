using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.CommonUtilities;
using System.Globalization;

namespace PEA.BPM.PaymentManagementModule.Interface.BusinessEntities.Reports
{
    [DataContract]
    public class APReport
    {
        private string _branchId;
        private string _paymentVoucher;
        private string _caId;
        private string _caName;
        private string _posId;
        private string _posName;
        private string _cashierId;

        private DateTime? _paymentDt;
        private decimal? _adjAmount;
        private decimal? _adjAmount1;
        private decimal? _adjAmount2;
        private decimal? _gAmount;
        private decimal? _gAmount1;
        private decimal? _gAmount2;

        private DateTime? _cancelDt;
        private string _cancelReason;
        private bool _cancelFlag;


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
        public string PosName
        {
            get { return _posName; }
            set { _posName = value; }
        }


        [DataMember(Order=7)]
        public string CashierId
        {
            get { return _cashierId; }
            set { _cashierId = value; }
        }


        [DataMember(Order=8)]
        public DateTime? PaymentDt
        {
            get { return _paymentDt; }
            set { _paymentDt = value; }
        }


        //[DataMember(Order=9)]
        public string PaymentDate
        {
            get { return _paymentDt.Value.ToString("dd/MM/yyyy  HH:mm", new CultureInfo("th-TH")); }
        }


        [DataMember(Order=10)]
        public decimal? AdjAmount
        {
            get { return _adjAmount; }
            set { _adjAmount = value; }
        }


        [DataMember(Order=11)]
        public decimal? AdjAmount1
        {
            get { return _adjAmount1; }
            set { _adjAmount1 = value; }
        }


        [DataMember(Order=12)]
        public decimal? AdjAmount2
        {
            get { return _adjAmount2; }
            set { _adjAmount2 = value; }
        }


        [DataMember(Order=13)]
        public decimal? GAmount
        {
            get { return _gAmount; }
            set { _gAmount = value; }
        }


        [DataMember(Order=14)]
        public decimal? GAmount1
        {
            get { return _gAmount1; }
            set { _gAmount1 = value; }
        }


        [DataMember(Order=15)]
        public decimal? GAmount2
        {
            get { return _gAmount2; }
            set { _gAmount2 = value; }
        }


        [DataMember(Order=16)]
        public DateTime? CancelDt
        {
            get { return _cancelDt; }
            set { _cancelDt = value; }
        }


        //[DataMember(Order=17)]
        public string CancelDate
        {
            get { return (_cancelDt == null ? "" : _cancelDt.Value.ToString("dd/MM/yyyy  HH:mm", new CultureInfo("th-TH"))); }
        }


        [DataMember(Order=18)]
        public string CancelReason
        {
            get { return _cancelReason; }
            set { _cancelReason = value; }
        }

        [DataMember(Order=19)]
        public bool CancelFlag
        {
            get { return _cancelFlag; }
            set { _cancelFlag = value; }
        }
    }
}

//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace PEA.BPM.PaymentManagementModule.Interface.BusinessEntities.Reports
//{
//    class APReport
//    {
//    }
//}
