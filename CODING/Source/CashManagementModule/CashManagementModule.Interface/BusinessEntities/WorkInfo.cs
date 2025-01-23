using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace PEA.BPM.CashManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class WorkInfo
    {
        private string _workId;
        private string _cashierId;
        private string _cashierName;
        private string _branchId;
        private string _posId;
        private int? _dayCount;
        private DateTime? _openWorkDt;
        private DateTime? _closeWorkDt;
        private string _status;

        //special used
        private decimal? _workCashAmt;
        private decimal? _workChequeAmt;
        private decimal? _totalWorkMoneyAmt;
        private string _closeWorkBy;


        [DataMember(Order=1)]
        public string WorkId
        {
            set { _workId = value; }
            get { return _workId; }
        }


        [DataMember(Order=2)]
        public string Status
        {
            set { _status = value; }
            get { return _status; }
        }


        [DataMember(Order=3)]
        public decimal? WorkCashAmt
        {
            set { _workCashAmt = value; }
            get { return _workCashAmt; }
        }


        [DataMember(Order=4)]
        public decimal? TotalWorkMoneyAmt
        {
            set { _totalWorkMoneyAmt = value; }
            get { return _totalWorkMoneyAmt; }
        }


        [DataMember(Order=5)]
        public int? DayCount
        {
            set { _dayCount = value; }
            get { return _dayCount; }
        }


        [DataMember(Order=6)]
        public decimal? WorkChequeAmt
        {
            set { _workChequeAmt = value; }
            get { return _workChequeAmt; }
        }


        [DataMember(Order=7)]
        public string CashierId
        {
            set { _cashierId = value; }
            get { return _cashierId; }
        }


        [DataMember(Order=8)]
        public string CashierName
        {
            set { _cashierName = value; }
            get { return _cashierName; }
        }


        [DataMember(Order=9)]
        public string CloseWorkBy
        {
            set { _closeWorkBy = value; }
            get { return _closeWorkBy; }
        }


        [DataMember(Order=10)]
        public string BranchId
        {
            set { _branchId = value; }
            get { return _branchId; }
        }


        [DataMember(Order=11)]
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


        [DataMember(Order=13)]
        public DateTime? OpenWorkDt
        {
            set { _openWorkDt = value; }
            get { return _openWorkDt; }
        }

        [DataMember(Order=14)]
        public DateTime? CloseWorkDt
        {
            set { _closeWorkDt = value; }
            get { return _closeWorkDt; }
        }

        //Checked TongKung
        //[DataMember(Order=15)]
        public string CashierDesc
        {
            get { return "(" + _cashierId + ") " + _cashierName; }
        }


    }
}
