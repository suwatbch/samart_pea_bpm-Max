using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CommissionRateInfo
    {
        #region "Local Variable"
        private int? _rtId;
        private decimal? _personRateForResident = 0;
        private decimal? _companyRateForResident = 0;
        private decimal? _personRateForSmallBiz = 0;
        private decimal? _companyRateForSmallBiz = 0;
        private decimal? _personRateForGoverment = 0;
        private decimal? _companyRateForGoverment = 0;
        private decimal? _special70To90Rate = 0;
        private decimal? _specialMoreThan90Rate = 0;
        private decimal? _pasteBillRate = 0;
        private decimal? _pasteBillThreeMonthRate = 0;
        private decimal? _includeRateForKeepAllBill = 0;
        private decimal? _maxInvPercent;
        private decimal? _fineRatePerBill;
        #endregion

        #region "Property"


        [DataMember(Order=1)]
        public int? RtId
        {
            get { return _rtId; }
            set { _rtId = value; }
        }


        [DataMember(Order=2)]
        public decimal? PersonRateForResident
        {
            get { return this._personRateForResident; }
            set { this._personRateForResident = value; }
        }


        [DataMember(Order=3)]
        public decimal? CompanyRateForResident
        {
            get { return this._companyRateForResident; }
            set { this._companyRateForResident = value; }
        }


        [DataMember(Order=4)]
        public decimal? PersonRateForSmallBiz
        {
            get { return this._personRateForSmallBiz; }
            set { this._personRateForSmallBiz = value; }
        }


        [DataMember(Order=5)]
        public decimal? CompanyRateForSmallBiz
        {
            get { return this._companyRateForSmallBiz; }
            set { this._companyRateForSmallBiz = value; }
        }


        [DataMember(Order=6)]
        public decimal? PersonRateForGoverment
        {
            get { return this._personRateForGoverment; }
            set { this._personRateForGoverment = value; }
        }


        [DataMember(Order=7)]
        public decimal? CompanyRateForGoverment
        {
            get { return this._companyRateForGoverment; }
            set { this._companyRateForGoverment = value; }
        }


        [DataMember(Order=8)]
        public decimal? Special70To90Rate
        {
            get { return this._special70To90Rate; }
            set { this._special70To90Rate = value; }
        }


        [DataMember(Order=9)]
        public decimal? SpecialMoreThan90Rate
        {
            get { return this._specialMoreThan90Rate; }
            set { this._specialMoreThan90Rate = value; }
        }


        [DataMember(Order=10)]
        public decimal? PasteBill
        {
            get { return this._pasteBillRate; }
            set { this._pasteBillRate = value; }
        }


        [DataMember(Order=11)]
        public decimal? PasteBillThreeMonthRate
        {
            get { return this._pasteBillThreeMonthRate; }
            set { this._pasteBillThreeMonthRate = value; }
        }


        [DataMember(Order=12)]
        public decimal? IncludeRateForKeepAllBill
        {
            get { return this._includeRateForKeepAllBill; }
            set { this._includeRateForKeepAllBill = value; }
        }


        [DataMember(Order=13)]
        public decimal? MaxInvPercent
        {
            get { return _maxInvPercent; }
            set { _maxInvPercent = value; }
        }


        [DataMember(Order=14)]
        public decimal? FineRatePerBill
        {
            get { return _fineRatePerBill; }
            set { _fineRatePerBill = value; }
        }

        #endregion

    }
}
