using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{

    [DataContract]
    public class TotBillItemAndAmountGroupByBillBookInfo
    {
        #region "Loacl Variable"
        private string _billBookId;
        private int? _billOutResident = 0;
        private decimal? _amountBillOutResident = 0;
        private int? _billOutSmallBiz = 0;
        private decimal? _amountBillOutSmallBiz = 0;
        private int? _billOutGoverment = 0;
        private decimal? _amountBillOutGoverment = 0;
        private int? _canKeepBillResident = 0;
        private decimal? _amountCanKeepBillResident = 0;
        private int? _canKeepBillSmallBiz = 0;
        private decimal? _amountCanKeepSmallBiz = 0;
        private int? _canKeepBillGoverment = 0;
        private decimal? _amountCanKeepBillGoverment = 0;
        private int? _bookLot = 0;
        #endregion

        #region "Property"

        [DataMember(Order=1)]
        public string BillBookId
        {
            get { return _billBookId; }
            set { this._billBookId = value; } 
        }

        [DataMember(Order=2)]
        public int? BillOutResident
        {
            get { return _billOutResident; }
            set { this._billOutResident = value; }
        }

        [DataMember(Order=3)]
        public decimal? AmountBillOutResident
        {
            get { return _amountBillOutResident; }
            set { this._amountBillOutResident = value; }
        }

        [DataMember(Order=4)]
        public int? BillOutSmallBiz
        {
            get { return _billOutSmallBiz; }
            set { this._billOutSmallBiz = value; }
        }

        [DataMember(Order=5)]
        public decimal? AmountBillOutSmallBiz
        {
            get { return _amountBillOutSmallBiz; }
            set { this._amountBillOutSmallBiz = value; }
        }

        [DataMember(Order=6)]
        public int? BillOutGoverment
        {
            get { return _billOutGoverment; }
            set { this._billOutGoverment = value; }
        }

        [DataMember(Order=7)]
        public decimal? AmountBillOutGoverment
        {
            get { return _amountBillOutGoverment; }
            set { this._amountBillOutGoverment = value; }
        }

        [DataMember(Order=8)]
        public int? CanKeepBillResident
        {
            get { return _canKeepBillResident; }
            set { this._canKeepBillResident = value; }
        }

        [DataMember(Order=9)]
        public decimal? AmountCanKeepBillResident
        {
            get { return _amountCanKeepBillResident; }
            set { this._amountCanKeepBillResident = value; }
        }

        [DataMember(Order=10)]
        public int? CanKeepBillSmallBiz
        {
            get { return _canKeepBillSmallBiz; }
            set { this._canKeepBillSmallBiz = value; }
        }

        [DataMember(Order=11)]
        public decimal? AmountCanKeepBillSmallBiz
        {
            get { return _amountCanKeepSmallBiz; }
            set { this._amountCanKeepSmallBiz = value; }
        }

        [DataMember(Order=12)]
        public int? CanKeepBillGoverment
        {
            get { return _canKeepBillGoverment; }
            set { this._canKeepBillGoverment = value; }
        }

        [DataMember(Order=13)]
        public decimal? AmountCanKeepBillGoverment
        {
            get { return _amountCanKeepBillGoverment; }
            set { this._amountCanKeepBillGoverment = value; }
        }

        [DataMember(Order=14)]
        public int? BookLot
        {
            get { return _bookLot; }
            set { this._bookLot = value; }
        }
        #endregion
    }
}
