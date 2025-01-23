using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace PEA.BPM.CashManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class ReportCloseWorkSummary
    {
        public ReportCloseWorkSummary()
        {
            _cashIn = 0;
            _chequeIn = 0;
            _cashOut = 0;
            _chequeOut = 0;
            _cashCancelled = 0;
            _chequeCancelled = 0;
            _cashNet = 0;
            _chequeNet = 0;
        }

        private string _workId;
        private string _flowId;
        private string _flowType;
        private string   _description;
        private decimal? _cashIn;
        private decimal? _chequeIn;
        private decimal? _cashOut;
        private decimal? _chequeOut;
        private decimal? _cashCancelled;
        private decimal? _chequeCancelled;
        private decimal? _cashNet;
        private decimal? _chequeNet;
        private string _cashierId;
        private string _cashierName;
        private string _reportCondition;
        private DateTime _closeWorkDt;


        [DataMember(Order=1)]
        public string WorkId
        {
            get { return _workId; }
            set { _workId = value; }
        }


        [DataMember(Order=2)]
        public string FlowId
        {
            get { return _flowId; }
            set { _flowId = value; }
        }


        [DataMember(Order=3)]
        public string FlowType
        {
            get { return _flowType; }
            set { _flowType = value; }
        }


        [DataMember(Order=4)]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }


        [DataMember(Order=5)]
        public decimal? CashIn
        {
            get { return _cashIn; }
            set { _cashIn = value; }
        }


        [DataMember(Order=6)]
        public decimal? ChequeIn
        {
            get { return _chequeIn; }
            set { _chequeIn = value; }
        }


        [DataMember(Order=7)]
        public decimal? CashOut
        {
            get { return _cashOut; }
            set { _cashOut = value; }
        }


        [DataMember(Order=8)]
        public decimal? ChequeOut
        {
            get { return _chequeOut; }
            set { _chequeOut = value; }
        }


        [DataMember(Order=9)]
        public decimal? CashCancelled
        {
            get { return _cashCancelled; }
            set { _cashCancelled = value; }
        }


        [DataMember(Order=10)]
        public decimal? ChequeCancelled
        {
            get { return _chequeCancelled; }
            set { _chequeCancelled = value; }
        }


        [DataMember(Order=11)]
        public decimal? CashNet
        {
            get { return _cashNet; }
            set { _cashNet = value; }
        }


        [DataMember(Order=12)]
        public decimal? ChequeNet
        {
            get { return _chequeNet; }
            set { _chequeNet = value; }
        }

        //Checked TongKung
        public string CloseWorkDate
        {
            get
            {
                CultureInfo culture = new CultureInfo("th-TH");
                return _closeWorkDt.ToString("dd/MM/yyyy", culture);
            }
        }


        [DataMember(Order=14)]
        public DateTime CloseWorkDt
        {
            get { return _closeWorkDt; }
            set { _closeWorkDt = value; }
        }


        [DataMember(Order=15)]
        public string CashierId
        {
            get { return _cashierId; }
            set { _cashierId = value; }
        }


        [DataMember(Order=16)]
        public string CashierName
        {
            get { return _cashierName; }
            set { _cashierName = value; }
        }


        [DataMember(Order=17)]
        public string ReportCondition
        {
            get { return _reportCondition; }
            set { _reportCondition = value; }
        }

    }
}
