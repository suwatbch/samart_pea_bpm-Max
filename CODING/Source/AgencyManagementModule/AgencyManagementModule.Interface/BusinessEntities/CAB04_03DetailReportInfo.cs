using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CAB04_03DetailReportInfo
    {
        #region "Local Variable"       
        private string _billBookRef;
        private int? _billOutForResident = 0;
        private decimal? _amountBillOutForResident = 0;
        private int? _billOutForSmallBiz = 0;
        private decimal? _amountBillOutForSmallBiz = 0;
        private int? _billOutForGoverment = 0;
        private decimal? _amountBillOutForGoverment = 0;
        private int? _canKeepBillForResident = 0;
        private decimal? _amountCanKeepBillForResident = 0;
        private int? _canKeepBillForSmallBiz = 0;
        private decimal? _amountCanKeepBillForSmallBiz = 0;
        private int? _canKeepBillForGoverment = 0;
        private decimal? _amountCanKeepBillForGoverment = 0;
        private int? _receiveCount = 0;
        #endregion

        #region "Property"


        [DataMember(Order=1)]
        public string BillBookRef
        {
            get { return _billBookRef; }
            set { this._billBookRef = value; }
        }

        [DataMember(Order=2)]
        public int? BillOutForResident
        {
            get { return _billOutForResident; }
            set { this._billOutForResident = value; }
        }

        [DataMember(Order=3)]
        public decimal? AmountBillOutForResident
        {
            get { return _amountBillOutForResident; }
            set { this._amountBillOutForResident = value; }
        }

        [DataMember(Order=4)]
        public int? BillOutForSmallBiz
        {
            get { return _billOutForSmallBiz; }
            set { this._billOutForSmallBiz = value; }
        }

        [DataMember(Order=5)]
        public decimal? AmountBillOutForSmallBiz
        {
            get { return _amountBillOutForSmallBiz; }
            set { this._amountBillOutForSmallBiz = value; }
        }

        [DataMember(Order=6)]
        public int? BillOutForGoverment
        {
            get { return _billOutForGoverment; }
            set { this._billOutForGoverment = value; }
        }

        [DataMember(Order=7)]
        public decimal? AmountBillOutForGoverment
        {
            get { return _amountBillOutForGoverment; }
            set { this._amountBillOutForGoverment = value; }
        }

        [DataMember(Order=8)]
        public int? CanKeepBillForResident
        {
            get { return _canKeepBillForResident; }
            set { this._canKeepBillForResident = value; }
        }

        [DataMember(Order=9)]
        public decimal? AmountCanKeepBillForResident
        {
            get { return _amountCanKeepBillForResident; }
            set { this._amountCanKeepBillForResident = value; }
        }

        [DataMember(Order=10)]
        public int? CanKeepBillForSmallBiz
        {
            get { return _canKeepBillForSmallBiz; }
            set { this._canKeepBillForSmallBiz = value; }
        }

        [DataMember(Order=11)]
        public decimal? AmountCanKeepBillForSmallBiz
        {
            get { return _amountCanKeepBillForSmallBiz; }
            set { this._amountCanKeepBillForSmallBiz = value; }
        }

        [DataMember(Order=12)]
        public int? CanKeepBillForGoverment
        {
            get { return _canKeepBillForGoverment; }
            set { this._canKeepBillForGoverment = value; }
        }

        [DataMember(Order=13)]
        public decimal? AmountCanKeepBillForGoverment
        {
            get { return _amountCanKeepBillForGoverment; }
            set { this._amountCanKeepBillForGoverment = value; }
        }

        [DataMember(Order=14)]
        public int? ReceiveCount
        {
            get { return this._receiveCount; }
            set { this._receiveCount = value; }
        }
        #endregion
    }
}
