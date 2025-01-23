using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace PEA.BPM.CashManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class ReportWorkFlowSummary
    {
        List<FlowSummaryInfo> _flowList = new List<FlowSummaryInfo>();
        private string _cashierId;
        private string _cashierName;
        private string _posId;
        private string _openWorkDate;
        private DateTime? _openWorkDt;
        private string _closeWorkDate;
        private DateTime? _closeWorkDt;
        private int? _dayCount;


        [DataMember(Order=1)]
        public List<FlowSummaryInfo> FlowList
        {
            set { _flowList = value; }
            get { return _flowList; }
        }


        [DataMember(Order=2)]
        public string CashierId
        {
            set { _cashierId = value; }
            get { return _cashierId; }
        }


        [DataMember(Order=3)]
        public string CashierName
        {
            set { _cashierName = value; }
            get { return _cashierName; }
        }


        [DataMember(Order=4)]
        public string PosId
        {
            set { _posId = value; }
            get { return _posId; }
        }

        //Checked TongKung
        public string OpenWorkDate
        {
            get { return _openWorkDt.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH")); }
        }

        //Checked TongKung
        public string CloseWorkDate
        {

            get { return _closeWorkDt.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH")); }
        }


        [DataMember(Order=7)]
        public DateTime? OpenWorkDt
        {
            set { _openWorkDt = value; }
            get { return _openWorkDt; }
        }


        [DataMember(Order=8)]
        public DateTime? CloseWorkDt
        {
            set { _closeWorkDt = value; }
            get { return _closeWorkDt; }
        }


        [DataMember(Order=9)]
        public int? DayCount
        {
            set { _dayCount = value; }
            get { return _dayCount; }
        }


    }
}
