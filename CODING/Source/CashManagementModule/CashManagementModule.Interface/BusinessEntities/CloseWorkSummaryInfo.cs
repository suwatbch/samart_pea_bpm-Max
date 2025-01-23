using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.CashManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CloseWorkSummaryInfo
    {
        private string _workId;
        private List<FlowSummaryInfo> _flowList = new List<FlowSummaryInfo>();
        private List<FlowSummaryInfo> _cancelledFlowList = new List<FlowSummaryInfo>();
        private List<FlowSummaryInfo> _outboundFlowList = new List<FlowSummaryInfo>();
        private decimal? _sum_cashIn;
        private decimal? _sum_chequeIn;
        private decimal? _sum_cashOut;
        private decimal? _sum_chequeOut;
        private decimal? _totalCash;
        private decimal? _totalCheque;
        private decimal? _cash_lastWork;
        private decimal? _cheque_lastWork;
        private decimal? _cash_nextWork;
        private decimal? _cheque_nextWork;

        //
        private decimal? _totalPayIn;
        

        [DataMember(Order=1)]
        public string WorkId
        {
            set { _workId = value; }
            get { return _workId; }
        }


        [DataMember(Order=2)]
        public List<FlowSummaryInfo> FlowList
        {
            set { _flowList = value; }
            get { return _flowList; }
        }


        [DataMember(Order=3)]
        public List<FlowSummaryInfo> CancelledFlowList
        {
            set { _cancelledFlowList = value; }
            get { return _cancelledFlowList; }
        }


        [DataMember(Order=4)]
        public List<FlowSummaryInfo> OutboundFlowList
        {
            set { _outboundFlowList = value; }
            get { return _outboundFlowList; }
        }


        [DataMember(Order=5)]
        public decimal? SumCashIn
        {
            set { _sum_cashIn = value; }
            get { return _sum_cashIn; }
        }


        [DataMember(Order=6)]
        public decimal? SumChequeIn
        {
            set { _sum_chequeIn = value; }
            get { return _sum_chequeIn; }
        }


        [DataMember(Order=7)]
        public decimal? SumCashOut
        {
            set { _sum_cashOut = value; }
            get { return _sum_cashOut; }
        }


        [DataMember(Order=8)]
        public decimal? SumChequeOut
        {
            set { _sum_chequeOut = value; }
            get { return _sum_chequeOut; }
        }


        [DataMember(Order=9)]
        public decimal? TotalCash
        {
            set { _totalCash = value; }
            get { return _totalCash; }
        }


        [DataMember(Order=10)]
        public decimal? TotalCheque
        {
            set { _totalCheque = value; }
            get { return _totalCheque; }
        }


        [DataMember(Order=11)]
        public decimal? CashLastWork
        {
            set { _cash_lastWork = value; }
            get { return _cash_lastWork; }
        }


        [DataMember(Order=12)]
        public decimal? ChequeLastWork
        {
            set { _cheque_lastWork = value; }
            get { return _cheque_lastWork; }
        }


        [DataMember(Order=13)]
        public decimal? CashNextWork
        {
            set { _cash_nextWork = value; }
            get { return _cash_nextWork; }
        }


        [DataMember(Order=14)]
        public decimal? ChequeNextWork
        {
            set { _cheque_nextWork = value; }
            get { return _cheque_nextWork; }
        }


        [DataMember(Order=15)]
        public decimal? TotalPayIn
        {
            set { _totalPayIn = value; }
            get { return _totalPayIn; }
        }



    }
}
