using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class SummaryBillViewSummaryFooterInfo
    {
        private string _header;
        private int _lineCount;
        private int _currentBillCount;
        private decimal? _currentBillAmount = 0;
        private int _pastBillCount;
        private decimal? _pastBillAmount = 0;
        private int _invCount;
        private decimal? _invAmount = 0;
        private int _overallCount;
        private decimal? _overallAmount =0;


        [DataMember(Order=1)]
        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }


        [DataMember(Order=2)]
        public int LineCount
        {
            get { return _lineCount; }
            set { _lineCount = value; }
        }


        [DataMember(Order=3)]
        public int CurrentBillCount
        {
            get { return _currentBillCount; }
            set { _currentBillCount = value; }
        }


        [DataMember(Order=4)]
        public decimal? CurrentBillAmount
        {
            get { return _currentBillAmount; }
            set { _currentBillAmount = value; }
        }


        [DataMember(Order=5)]
        public int PastBillCount
        {
            get { return _pastBillCount; }
            set { _pastBillCount = value; }
        }


        [DataMember(Order=6)]
        public decimal? PastBillAmount
        {
            get { return _pastBillAmount; }
            set { _pastBillAmount = value; }
        }


        [DataMember(Order=7)]
        public int InvCount
        {
            get { return _invCount; }
            set { _invCount = value; }
        }


        [DataMember(Order=8)]
        public decimal? InvAmount
        {
            get { return _invAmount; }
            set { _invAmount = value; }
        }


        [DataMember(Order=9)]
        public int OverallCount
        {
            get { return _overallCount; }
            set { _overallCount = value; }
        }


        [DataMember(Order=10)]
        public decimal? OverallAmount
        {
            get { return _overallAmount; }
            set { _overallAmount = value; }
        }

    }
}
