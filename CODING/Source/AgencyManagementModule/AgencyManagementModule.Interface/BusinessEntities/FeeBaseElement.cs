using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class FeeBaseElement
    {
        private string _branchId = null;
        private decimal? _houseRegRate=0;
        private decimal? _houseGrpRate=0;
        private decimal? _corpRegRate=0;
        private decimal? _corpGrpRate = 0;
        private decimal? _govRegRate=0;
        private decimal? _govGrpRate = 0;
        private decimal? _maxInvPercent = 0;
        private decimal? _invRate = 0;
        private decimal? _invoicePastThreeMonthRate = 0;
        private decimal? _billingNinetyPercent = 0;
        private decimal? _billingNinetyNinePercent = 0;
        private decimal? _billingHundredPercent = 0;
        private bool _blockCommissionCalCount;
        private bool _penaltyWaive;
        private int? _maxCommissionCalCount=0;
        private decimal? _fineRatePerBill = 0;
        private decimal? _vatRate = 0;
        private decimal? _taxRate = 0;

        private decimal? _collectedPercent = 0;
        private int? _caCount = 0;
        private decimal? _upperRate = 0;
        private decimal? _lowerRate = 0;



        [DataMember(Order=1)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }


        [DataMember(Order=2)]
        public decimal? VatRate
        {
            get { return _vatRate; }
            set { _vatRate = value; }
        }


        [DataMember(Order=3)]
        public decimal? TaxRate
        {
            get { return _taxRate; }
            set { _taxRate = value; }
        }

        //public string DebitAccount
        //{
        //    get { return _debitAccount; }
        //    set { _debitAccount = value; }
        //}


        [DataMember(Order=4)]
        public decimal? FineRatePerBill
        {
            get { return _fineRatePerBill; }
            set { _fineRatePerBill = value; }
        }


        [DataMember(Order=5)]
        public decimal? HouseRegRate
        {
            get { return _houseRegRate; }
            set { _houseRegRate = value; }
        }


        [DataMember(Order=6)]
        public decimal? HouseGrpRate
        {
            get { return _houseGrpRate; }
            set { _houseGrpRate = value; }
        }


        [DataMember(Order=7)]
        public decimal? CorpRegRate
        {
            get { return _corpRegRate; }
            set { _corpRegRate = value; }
        }


        [DataMember(Order=8)]
        public decimal? CorpGrpRate
        {
            get { return _corpGrpRate; }
            set { _corpGrpRate = value; }
        }


        [DataMember(Order=9)]
        public decimal? GovRegRate
        {
            get { return _govRegRate; }
            set { _govRegRate = value; }
        }


        [DataMember(Order=10)]
        public decimal? GovGrpRate
        {
            get { return _govGrpRate; }
            set { _govGrpRate = value; }
        }


        [DataMember(Order=11)]
        public decimal? MaxInvoicePercent
        {
            get { return _maxInvPercent; }
            set { _maxInvPercent = value; }
        }


        [DataMember(Order=12)]
        public decimal? InvoiceRate
        {
            get { return _invRate; }
            set { _invRate = value; }
        }


        [DataMember(Order=13)]
        public decimal? InvoicePastThreeMonthRate
        {
            get { return _invoicePastThreeMonthRate; }
            set { _invoicePastThreeMonthRate = value; }
        }


        [DataMember(Order=14)]
        public decimal? BillingNinetyPercent
        {
            get { return _billingNinetyPercent; }
            set { _billingNinetyPercent = value; }
        }


        [DataMember(Order=15)]
        public decimal? BillingNinetyNinePercent
        {
            get { return _billingNinetyNinePercent; }
            set { _billingNinetyNinePercent = value; }
        }


        [DataMember(Order=16)]
        public decimal? BillingHundredPercent
        {
            get { return _billingHundredPercent; }
            set { _billingHundredPercent = value; }
        }


        [DataMember(Order=17)]
        public bool HasCommissionCalLimit
        {
            get { return _blockCommissionCalCount; }
            set { _blockCommissionCalCount = value; }
        }


        [DataMember(Order=18)]
        public bool PenaltyWaive
        {
            get { return _penaltyWaive; }
            set { _penaltyWaive = value; }
        }


        [DataMember(Order=19)]
        public int? MaxCommissionCalCount
        {
            get { return _maxCommissionCalCount; }
            set { _maxCommissionCalCount = value; }
        }


        [DataMember(Order=20)]
        public decimal? CollectedPercent
        {
            get { return _collectedPercent; }
            set { _collectedPercent = value; }
        }


        [DataMember(Order=21)]
        public int? CaCount
        {
            get { return _caCount; }
            set { _caCount = value; }
        }
        

        [DataMember(Order=22)]
        public decimal? UpperRate
        {
            get { return _upperRate; }
            set { _upperRate = value; }
        }


        [DataMember(Order=23)]
        public decimal? LowerRate
        {
            get { return _lowerRate; }
            set { _lowerRate = value; }
        }

    }
}
