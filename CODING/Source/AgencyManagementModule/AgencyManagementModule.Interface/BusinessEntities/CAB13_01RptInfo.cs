using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CAB13_01RptInfo
    {
        private int? _seqNo;
        private string _branchId;
        private string _branchName;
        private int? _totalPersonAgency;
        private int? _totalGovAgency;
        private int? _totalAgency;
        private int? _overNinety;
        private int? _totalBill;
        private decimal? _bookTotalAmount;
        private int? _totalBillCollect;
        private decimal? _bookPaidAmount;
        private decimal? _baseAmount;
        private decimal? _specialAmount;
        private decimal? _invCmAmount;
        private decimal? _transCost;
        private string _district;
        private bool _printPreview;


        [DataMember(Order=1)]
        public int? SeqNo
        {
            get { return this._seqNo; }
            set { this._seqNo = value; }
        }


        [DataMember(Order=2)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }


        [DataMember(Order=3)]
        public string BranchName
        {
            get { return this._branchName; }
            set { this._branchName = value; }
        }


        [DataMember(Order=4)]
        public int? TotalPersonAgency
        {
            get { return this._totalPersonAgency; }
            set { this._totalPersonAgency = value; }
        }


        [DataMember(Order=5)]
        public int? TotalGovAgency
        {
            get { return this._totalGovAgency; }
            set { this._totalGovAgency = value; }
        }


        [DataMember(Order=6)]
        public int? TotalAgency
        {
            get { return this._totalAgency; }
            set { this._totalAgency = value; }
        }


        [DataMember(Order=7)]
        public int? OverNinety
        {
            get { return this._overNinety; }
            set { this._overNinety = value; }
        }


        [DataMember(Order=8)]
        public int? TotalBill
        {
            get { return this._totalBill; }
            set { this._totalBill = value; }
        }


        [DataMember(Order=9)]
        public decimal? BookTotalAmount
        {
            get { return this._bookTotalAmount; }
            set { this._bookTotalAmount = value; }
        }


        [DataMember(Order=10)]
        public int? TotalBillCollect
        {
            get { return this._totalBillCollect; }
            set { this._totalBillCollect = value; }
        }


        [DataMember(Order=11)]
        public decimal? BookPaidAmount
        {
            get { return this._bookPaidAmount; }
            set { this._bookPaidAmount = value; }
        }


        [DataMember(Order=12)]
        public decimal? BaseAmount
        {
            get { return this._baseAmount; }
            set { this._baseAmount = value; }
        }


        [DataMember(Order=13)]
        public decimal? SpecialAmount
        {
            get { return this._specialAmount; }
            set { this._specialAmount = value; }
        }


        [DataMember(Order=14)]
        public decimal? InvCmAmount
        {
            get { return this._invCmAmount; }
            set { this._invCmAmount = value; }
        }


        [DataMember(Order=15)]
        public decimal? TransCost
        {
            get { return this._transCost; }
            set { this._transCost = value; }
        }


        [DataMember(Order=16)]
        public string District
        {
            get { return this._district; }
            set { this._district = value; }
        }


        [DataMember(Order=17)]
        public bool PrintPreview
        {
            get { return this._printPreview; }
            set { this._printPreview = value; }
        }

    }
}
