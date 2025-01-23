using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{

    [DataContract]
    public class CommissionVoucherConditionPrintReport
    {
        #region "Local Variable"
        private bool _isPersonType = true;
        private string _agentId;
        private string _agentName;
        private string _periodBook;
        private DateTime? _billBookCreateDate;

        private int? _totalBillOutResident = 0;
        private int? _totalBillOutSmallBiz = 0;
        private int? _totalBillOutGoverment = 0;
        private int? _totalBillOutGovPaid = 0;

        private decimal? _amountBillOutResident = 0;
        private decimal? _amountBillOutSmallBiz = 0;
        private decimal? _amountBillOutGoverment = 0;
        private decimal? _amountBillOutGovPaid = 0;

        private int? _totalBillKeepResident = 0;
        private int? _totalBillKeepSmallBiz = 0;
        private int? _totalBillKeepGoverment = 0;
        private int? _totalBillKeepGovPaid = 0;

        private decimal? _amountBillKeepResident = 0;
        private decimal? _amountBillKeepSmallBiz = 0;
        private decimal? _amountBillKeepGoverment = 0;
        private decimal? _amountBillKeepGovPaid = 0;

        private int? _totalBillPaste = 0;
        private int? _totalBillPasteThreeMonth = 0;
        private string _cmdId ;
        private decimal? _fineSendMoneyLate = 0;
        private bool _printPreview;
        private decimal? _seventyFiveToNighty = 0;
        private decimal? _nightyUp = 0;
        private decimal? _hundread = 0;
        private bool _isBlank = false;
        private int? _bill75To90 = 0;
        private int? _bill90Up = 0;

        private string _branchid = "";

        private decimal? totalBillPasteCommission = 0;
        #endregion

        #region "Property"


        [DataMember(Order=1)]
        public bool IsPersonType
        {
            get { return this._isPersonType; }
            set { this._isPersonType = value; }
        }


        [DataMember(Order=2)]
        public decimal? SeventyFiveToNighty
        {
            get { return this._seventyFiveToNighty; }
            set { this._seventyFiveToNighty = value; }
        }

        [DataMember(Order=3)]
        public decimal? NightyUp
        {
            get { return this._nightyUp; }
            set { this._nightyUp = value; }
        }

        [DataMember(Order=4)]
        public decimal? Hundread
        {
            get { return this._hundread; }
            set { this._hundread = value; }
        }


        [DataMember(Order=5)]
        public string AgencyId
        {
            get { return this._agentId; }
            set { this._agentId = value; }
        }

        [DataMember(Order=6)]
        public string AgencyName
        {
            get { return this._agentName; }
            set { this._agentName = value; }
        }

        [DataMember(Order=7)]
        public string PeriodBook
        {
            get { return this._periodBook; }
            set { this._periodBook = value; }
        }

        [DataMember(Order=8)]
        public DateTime? CreateDate
        {
            get { return this._billBookCreateDate; }
            set { this._billBookCreateDate = value; }
        }

        [DataMember(Order=9)]
        public int? TotalBillOutResident
        {
            get { return this._totalBillOutResident; }
            set { this._totalBillOutResident = value; }
        }

        [DataMember(Order=10)]
        public int? TotalBillOutSmallBiz
        {
            get { return this._totalBillOutSmallBiz; }
            set { this._totalBillOutSmallBiz = value; }
        }

        [DataMember(Order=11)]
        public int? TotalBillOutGoverment
        {
            get { return this._totalBillOutGoverment; }
            set { this._totalBillOutGoverment = value; }
        }

        [DataMember(Order=12)]
        public decimal? AmountBillOutResident
        {
            get { return this._amountBillOutResident; }
            set { this._amountBillOutResident = value; }
        }

        [DataMember(Order=13)]
        public decimal? AmountBillOutSmallBiz
        {
            get { return this._amountBillOutSmallBiz; }
            set { this._amountBillOutSmallBiz = value; }
        }

        [DataMember(Order=14)]
        public decimal? AmountBillOutGoverment
        {
            get { return this._amountBillOutGoverment; }
            set { this._amountBillOutGoverment = value; }
        }

        [DataMember(Order=15)]
        public int? TotalBillKeepResident
        {
            get { return this._totalBillKeepResident; }
            set { this._totalBillKeepResident = value; }
        }

        [DataMember(Order=16)]
        public int? TotalBillKeepSmallBiz
        {
            get { return this._totalBillKeepSmallBiz; }
            set { this._totalBillKeepSmallBiz = value; }
        }

        [DataMember(Order=17)]
        public int? TotalBillKeepGoverment
        {
            get { return this._totalBillKeepGoverment; }
            set { this._totalBillKeepGoverment = value; }
        }

        [DataMember(Order=18)]
        public decimal? AmountBillKeepResident
        {
            get { return this._amountBillKeepResident; }
            set { this._amountBillKeepResident = value; }
        }

        [DataMember(Order=19)]
        public decimal? AmountBillKeepSmallBiz
        {
            get { return this._amountBillKeepSmallBiz; }
            set { this._amountBillKeepSmallBiz = value; }
        }

        [DataMember(Order=20)]
        public decimal? AmountBillKeepGoverment
        {
            get { return this._amountBillKeepGoverment; }
            set { this._amountBillKeepGoverment = value; }
        }

        [DataMember(Order=21)]
        public int? TotalBillPaste
        {
            get { return this._totalBillPaste; }
            set { this._totalBillPaste = value; }
        }

        [DataMember(Order=22)]
        public int? TotalBillPasteThreeMonth
        {
            get { return this._totalBillPasteThreeMonth; }
            set { this._totalBillPasteThreeMonth = value; }
        }

        [DataMember(Order=23)]
        public string CmdId
        {
            get { return this._cmdId; }
            set { this._cmdId = value; }
        }

        [DataMember(Order=24)]
        public decimal? FineSendMoneyLate
        {
            get { return this._fineSendMoneyLate; }
            set { this._fineSendMoneyLate = value; }
        }


        [DataMember(Order=25)]
        public bool PrintPreview
        {
            get { return this._printPreview; }
            set { this._printPreview = value; }
        }


        [DataMember(Order=26)]
        public bool IsBlank
        {
            get { return this._isBlank; }
            set { this._isBlank = value; }
        }


        [DataMember(Order=27)]
        public decimal? TotalBillPasteCommission
        {
            get { return this.totalBillPasteCommission; }
            set { this.totalBillPasteCommission = value; }
        }


        [DataMember(Order=28)]
        public int? Bill75To90
        {
            get { return this._bill75To90; }
            set { this._bill75To90 = value; }
        }

        [DataMember(Order=29)]
        public int? Bill90Up
        {
            get { return this._bill90Up; }
            set { this._bill90Up = value; }
        }



        [DataMember(Order=30)]
        public int? TotalBillOutGovPaid
        {
            get { return this._totalBillOutGovPaid; }
            set { this._totalBillOutGovPaid = value; }
        }


        [DataMember(Order=31)]
        public decimal? AmountBillOutGovPaid
        {
            get { return this._amountBillOutGovPaid; }
            set { this._amountBillOutGovPaid = value; }
        }


        [DataMember(Order=32)]
        public int? TotalBillKeepGovPaid
        {
            get { return this._totalBillKeepGovPaid; }
            set { this._totalBillKeepGovPaid = value; }
        }


        [DataMember(Order=33)]
        public decimal? AmountBillKeepGovPaid
        {
            get { return this._amountBillKeepGovPaid; }
            set { this._amountBillKeepGovPaid = value; }
        }

        [DataMember(Order = 34)]
        public string BranchId
        {
            get { return this._branchid; }
            set { this._branchid = value; }
        }
        #endregion
    }
}
