using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CallingBillSummaryInfo
    {
        private int _sequence;
        private string _peaCode;
        private string _lineId;
        private int _billCountCurrent;
        private decimal? _amountCurrent=0;
        private int _billCountPast;
        private decimal? _amountPast =0;
        private int _slipCount;
        private decimal? _amountSlip=0;
        private int _totalCount;
        private decimal? _totalAmount=0;


        [DataMember(Order=1)]
        public int Sequence
        {
            get { return _sequence; }
            set { _sequence = value; }
        }


        [DataMember(Order=2)]
        public string PeaCode
        {
            get { return _peaCode; }
            set { _peaCode = value; }
        }


        [DataMember(Order=3)]
        public string LineId
        {
            get { return _lineId; }
            set { _lineId = value; }
        }


        [DataMember(Order=4)]
        public int BillCountCurrent
        {
            get { return _billCountCurrent; }
            set { _billCountCurrent = value; }
        }


        [DataMember(Order=5)]
        public decimal? AmountCurrent
        {
            get { return _amountCurrent; }
            set { _amountCurrent = value; }
        }


        [DataMember(Order=6)]
        public int BillCountPast
        {
            get { return _billCountPast; }
            set { _billCountPast = value; }
        }


        [DataMember(Order=7)]
        public decimal? AmountPast
        {
            get { return _amountPast; }
            set { _amountPast = value; }
        }


        [DataMember(Order=8)]
        public int SlipCount
        {
            get { return _slipCount; }
            set { _slipCount = value; }
        }


        [DataMember(Order=9)]
        public decimal? AmountSlip
        {
            get { return _amountSlip; }
            set { _amountSlip = value; }
        }


        [DataMember(Order=10)]
        public int TotalCount
        {
            get { return _totalCount; }
            set { _totalCount = value; }
        }


        [DataMember(Order=11)]
        public decimal? TotalAmount
        {
            get { return _totalAmount; }
            set { _totalAmount = value; }
        }
    }
}
