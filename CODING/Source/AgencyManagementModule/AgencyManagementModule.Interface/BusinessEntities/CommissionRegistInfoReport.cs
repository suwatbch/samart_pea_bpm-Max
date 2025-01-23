using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CommissionRegistInfoReport
    {
     
    #region "Local Variable"
        private string _peaName;
        private string _peaBranch;
        private string _agencyName;
        private string _agencyCode;
        private string _agencyType;
        private string _agencyTaxNo;
        private string _registerDate;
        private string  _calculateDate;
        private string _printDate;
        private string _billMonth;
        private string _timeNo;
        private int? _billOutToAgencyForResident = 0;
        private decimal? _valueOfBillOutForResident = 0;
        private int? _canKeepElectricForResident = 0;
        private decimal? _valueOfKeepElectricForResident = 0;
        private decimal? _rateAgencyPersonTypeForResident = 0;
        private decimal? _rateAgencyCompanyTypeForResident = 0;
        private decimal? _amountCommissionOfResident = 0;
        private int? _billOutToAgencyForSmallBiz = 0;
        private decimal? _valueOfBillOutForSmallBiz = 0;
        private int? _canKeepElectricForSmallBiz = 0;
        private decimal? _valueOfKeepElectricForSmallBiz = 0;
        private decimal? _rateAgencyPersonTypeForSmallBiz = 0;
        private decimal? _rateAgencyCompanyTypeForSmallBiz = 0;
        private decimal? _amountCommissionOfSmallBiz = 0;
        private int? _billOutToAgencyForGovermentDepartment = 0;
        private decimal? _valueOfBillOutForGovermentDepartment = 0;
        private int? _canKeepElectricForGovermentDepartment = 0;
        private decimal? _valueOfKeepElectricForGovermentDepartment = 0;
        private decimal? _rateAgencyPersonTypeForGovermentDepartment = 0;
        private decimal? _rateAgencyCompanyTypeForGovermentDepartment = 0;
        private decimal? _amountCommissionOfGovermentDepartment = 0;


        private int? _billOutToAgencyForGovermentPaid = 0;
        private decimal? _valueOfBillOutForGovermentPaid = 0;
        private int? _canKeepElectricForGovermentPaid = 0;
        private decimal? _valueOfKeepElectricForGovermentPaid = 0;
        private decimal? _rateAgencyPersonTypeForGovermentPaid = 0;
        private decimal? _rateAgencyCompanyTypeForGovermentPaid = 0;
        private decimal? _amountCommissionOfGovermentPaid = 0;

        private int? _totalItemRepeatBillOfCommissionSpecial = 0;
        private decimal? _valueRepeatBillOfCommissionSpecial = 0;
        private int? _totalItemRepeatBillOfCommissionSpecialFollowStandard = 0;
        private decimal? _valueRepeatBillOfCommissionSpecialFollowStandard = 0;
        private int? _totalBillItemOf75To90PercentForCommissionSpecial = 0;
        private decimal? _rateOf75To90PercentForCommissionSpecial = 0;
        private decimal? _amountCommission75To90PercentForCommissionSpecial = 0;
        private int? _totalBillItemOfMorethan90PercentForCommissionSpecial = 0;
        private decimal? _rateOfMorethan90PercentForCommissionSpecial = 0;
        private decimal? _amountCommissionMorethan90PercentForCommissionSpecial = 0;
        private int? _totalItemInclude20PercentForCommissionBase = 0;
        private decimal? _amountInclude20PercentForCommissionBase = 0;
        private int? _totalItemSendBill = 0;
        private decimal? _rateOfSendBill = 0;
        private decimal? _amountOfSendBill = 0;
        private int? _totalItemSendBillThreeMonth = 0;
        private decimal? _rateOfSendBillThreeMonth = 0;
        private decimal? _amountOfSendBillThreeMonth = 0;
        private decimal? _transportOfCarPrice = 0;
        private decimal? _transportOfWaterPrice = 0;
        private decimal? _amountAllCommission = 0;
        private decimal? _fineSendMoneyLate = 0;
        private decimal? _finalAmountCommission = 0;
        private decimal? _rate100PercentForAllKeep = 0;       
        private decimal? _vat7Percent = 0;
        private decimal?  _taxRevenue = 0;
        private bool _isPersonType = true;
        
    #endregion

    #region "Porprety"


        [DataMember(Order=1)]
        public bool IsPersonType
        {
            get { return this._isPersonType; }
            set { this._isPersonType = value; }
        }


        [DataMember(Order=2)]
        public string PEAName
        {
            get { return this._peaName; }
            set { this._peaName = value; }
        }


        [DataMember(Order=3)]
        public string PEABranch
        {
            get { return this._peaBranch; }
            set { this._peaBranch = value; }
        }


        [DataMember(Order=4)]
        public string AgencyName
        {
            get { return this._agencyName; }
            set { this._agencyName = value; }
        }


        [DataMember(Order=5)]
        public string AgencyCode
        {
            get { return this._agencyCode; }
            set { this._agencyCode = value; }
        }


        [DataMember(Order=6)]
        public string AgencyType
        {
            get { return this._agencyType; }
            set { this._agencyType = value; }
        }


        [DataMember(Order=7)]
        public string AgencyTaxNo
        {
            get { return this._agencyTaxNo; }
            set { this._agencyTaxNo = value; }
        }


        [DataMember(Order=8)]
        public string  RegisterDate
        {
            get { return this._registerDate; }
            set { this._registerDate = value; }
        }


        [DataMember(Order=9)]
        public string BillMonth
        {
            get { return this._billMonth; }
            set { this._billMonth = value; }
        }


        [DataMember(Order=10)]
        public string TimeNo
        {
            get { return this._timeNo; }
            set { this._timeNo = value; }
        }              


        [DataMember(Order=11)]
        public int? BillOutToAgencyForResident
        {
            get { return this._billOutToAgencyForResident; }
            set { this._billOutToAgencyForResident = value; }
        }


        [DataMember(Order=12)]
        public decimal? ValueOfBillOutForResident
        {
            get { return this._valueOfBillOutForResident; }
            set { this._valueOfBillOutForResident = value; }
        }


        [DataMember(Order=13)]
        public int? CanKeepElectricForResident
        {
            get { return this._canKeepElectricForResident; }
            set { this._canKeepElectricForResident = value; }
        }


        [DataMember(Order=14)]
        public decimal? ValueOfKeepElectricForResident
        {
            get { return this._valueOfKeepElectricForResident; }
            set { this._valueOfKeepElectricForResident = value; }
        }


        [DataMember(Order=15)]
        public decimal? RateAgencyPersonTypeForResident
        {
            get { return this._rateAgencyPersonTypeForResident; }
            set { this._rateAgencyPersonTypeForResident = value; }
        }


        [DataMember(Order=16)]
        public decimal? RateAgencyCompanyTypeForResident
        {
            get { return this._rateAgencyCompanyTypeForResident; }
            set { this._rateAgencyCompanyTypeForResident = value; }
        }


        [DataMember(Order=17)]
        public decimal? AmountCommissionOfResident
        {
            get { return this._amountCommissionOfResident; }
            set { this._amountCommissionOfResident = value; }
        }


        [DataMember(Order=18)]
        public int? BillOutToAgencyForSmallBiz
        {
            get { return this._billOutToAgencyForSmallBiz; }
            set { this._billOutToAgencyForSmallBiz = value; }
        }


        [DataMember(Order=19)]
        public decimal? ValueOfBillOutForSmallBiz
        {
            get { return this._valueOfBillOutForSmallBiz; }
            set { this._valueOfBillOutForSmallBiz = value; }
        }


        [DataMember(Order=20)]
        public int? CanKeepElectricForSmallBiz
        {
            get { return this._canKeepElectricForSmallBiz; }
            set { this._canKeepElectricForSmallBiz = value; }
        }


        [DataMember(Order=21)]
        public decimal? ValueOfKeepElectricForSmallBiz
        {
            get { return this._valueOfKeepElectricForSmallBiz; }
            set { this._valueOfKeepElectricForSmallBiz = value; }
        }


        [DataMember(Order=22)]
        public decimal? RateAgencyPersonTypeForSmallBiz
        {
            get { return this._rateAgencyPersonTypeForSmallBiz; }
            set { this._rateAgencyPersonTypeForSmallBiz = value; }
        }


        [DataMember(Order=23)]
        public decimal? RateAgencyCompanyTypeForSmallBiz
        {
            get { return this._rateAgencyCompanyTypeForSmallBiz; }
            set { this._rateAgencyCompanyTypeForSmallBiz = value; }
        }


        [DataMember(Order=24)]
        public decimal? AmountCommissionOfSmallBiz
        {
            get { return this._amountCommissionOfSmallBiz; }
            set { this._amountCommissionOfSmallBiz = value; }
        }


        [DataMember(Order=25)]
        public int? BillOutToAgencyForGovermentDepartment
        {
            get { return this._billOutToAgencyForGovermentDepartment; }
            set { this._billOutToAgencyForGovermentDepartment = value; }
        }


        [DataMember(Order=26)]
        public decimal? ValueOfBillOutForGovermentDepartment
        {
            get { return this._valueOfBillOutForGovermentDepartment; }
            set { this._valueOfBillOutForGovermentDepartment = value; }
        }


        [DataMember(Order=27)]
        public int? CanKeepElectricForGovermentDepartment
        {
            get { return this._canKeepElectricForGovermentDepartment; }
            set { this._canKeepElectricForGovermentDepartment = value; }
        }


        [DataMember(Order=28)]
        public decimal? ValueOfKeepElectricForGovermentDepartment
        {
            get { return this._valueOfKeepElectricForGovermentDepartment; }
            set { this._valueOfKeepElectricForGovermentDepartment = value; }
        }


        [DataMember(Order=29)]
        public decimal? RateAgencyPersonTypeForGovermentDepartment
        {
            get { return this._rateAgencyPersonTypeForGovermentDepartment; }
            set { this._rateAgencyPersonTypeForGovermentDepartment = value; }
        }


        [DataMember(Order=30)]
        public decimal? RateAgencyCompanyTypeForGovermentDepartment
        {
            get { return this._rateAgencyCompanyTypeForGovermentDepartment; }
            set { this._rateAgencyCompanyTypeForGovermentDepartment = value; }
        }


        [DataMember(Order=31)]
        public decimal? AmountCommissionOfGovermentDepartment
        {
            get { return this._amountCommissionOfGovermentDepartment; }
            set { this._amountCommissionOfGovermentDepartment = value; }
        }


        [DataMember(Order=32)]
        public int? TotalItemRepeatBillOfCommissionSpecial
        {
            get { return this._totalItemRepeatBillOfCommissionSpecial; }
            set { this._totalItemRepeatBillOfCommissionSpecial = value; }
        }


        [DataMember(Order=33)]
        public decimal? ValueRepeatBillOfCommissionSpecial
        {
            get { return this._valueRepeatBillOfCommissionSpecial; }
            set { this._valueRepeatBillOfCommissionSpecial = value; }
        }


        [DataMember(Order=34)]
        public int? TotalItemRepeatBillOfCommissionSpecialFollowStandard
        {
            get { return this._totalItemRepeatBillOfCommissionSpecialFollowStandard; }
            set { this._totalItemRepeatBillOfCommissionSpecialFollowStandard = value; }
        }


        [DataMember(Order=35)]
        public decimal? ValueRepeatBillOfCommissionSpecialFollowStandard
        {
            get { return this._valueRepeatBillOfCommissionSpecialFollowStandard; }
            set { this._valueRepeatBillOfCommissionSpecialFollowStandard = value; }
        }


        [DataMember(Order=36)]
        public int? TotalBillItemOf75To90PercentForCommissionSpecial
        {
            get { return this._totalBillItemOf75To90PercentForCommissionSpecial; }
            set { this._totalBillItemOf75To90PercentForCommissionSpecial = value; }
        }


        [DataMember(Order=37)]
        public decimal? RateOf75To90PercentForCommissionSpecial
        {
            get { return this._rateOf75To90PercentForCommissionSpecial; }
            set { this._rateOf75To90PercentForCommissionSpecial = value; }
        }


        [DataMember(Order=38)]
        public decimal? AmountCommission75To90PercentForCommissionSpecial
        {
            get { return this._amountCommission75To90PercentForCommissionSpecial; }
            set { this._amountCommission75To90PercentForCommissionSpecial = value; }
        }


        [DataMember(Order=39)]
        public int? TotalBillItemOfMorethan90PercentForCommissionSpecial
        {
            get { return this._totalBillItemOfMorethan90PercentForCommissionSpecial; }
            set { this._totalBillItemOfMorethan90PercentForCommissionSpecial = value; }
        }


        [DataMember(Order=40)]
        public decimal? RateOfMorethan90PercentForCommissionSpecial
        {
            get { return this._rateOfMorethan90PercentForCommissionSpecial; }
            set { this._rateOfMorethan90PercentForCommissionSpecial = value; }
        }


        [DataMember(Order=41)]
        public decimal? AmountCommissionMorethan90PercentForCommissionSpecial
        {
            get { return this._amountCommissionMorethan90PercentForCommissionSpecial; }
            set { this._amountCommissionMorethan90PercentForCommissionSpecial = value; }
        }


        [DataMember(Order=42)]
        public int? TotalItemInclude20PercentForCommissionBase
        {
            get { return this._totalItemInclude20PercentForCommissionBase; }
            set { this._totalItemInclude20PercentForCommissionBase = value; }
        }


        [DataMember(Order=43)]
        public decimal? AmountInclude20PercentForCommissionBase
        {
            get { return this._amountInclude20PercentForCommissionBase; }
            set { this._amountInclude20PercentForCommissionBase = value; }
        }


        [DataMember(Order=44)]
        public int? TotalItemSendBill
        {
            get { return this._totalItemSendBill; }
            set { this._totalItemSendBill = value; }
        }


        [DataMember(Order=45)]
        public decimal? RateOfSendBill
        {
            get { return this._rateOfSendBill; }
            set { this._rateOfSendBill = value; }
        }


        [DataMember(Order=46)]
        public decimal? AmountOfSendBill
        {
            get { return this._amountOfSendBill; }
            set { this._amountOfSendBill = value; }
        }


        [DataMember(Order=47)]
        public int? TotalItemSendBillThreeMonth
        {
            get { return this._totalItemSendBillThreeMonth; }
            set { this._totalItemSendBillThreeMonth = value; }
        }


        [DataMember(Order=48)]
        public decimal? RateOfSendBillThreeMonth
        {
            get { return this._rateOfSendBillThreeMonth; }
            set { this._rateOfSendBillThreeMonth = value; }
        }


        [DataMember(Order=49)]
        public decimal? AmountOfSendBillThreeMonth
        {
            get { return this._amountOfSendBillThreeMonth; }
            set { this._amountOfSendBillThreeMonth = value; }
        }


        [DataMember(Order=50)]
        public decimal? TransportOfCarPrice
        {
            get { return this._transportOfCarPrice; }
            set { this._transportOfCarPrice = value; }
        }


        [DataMember(Order=51)]
        public decimal? TransportOfWaterPrice
        {
            get { return this._transportOfWaterPrice; }
            set { this._transportOfWaterPrice = value; }
        }


        [DataMember(Order=52)]
        public decimal? AmountAllCommission
        {
            get { return this._amountAllCommission; }
            set { this._amountAllCommission = value; }
        }


        [DataMember(Order=53)]
        public decimal? FineSendMoneyLate
        {
            get { return this._fineSendMoneyLate; }
            set { this._fineSendMoneyLate = value; }
        }


        [DataMember(Order=54)]
        public decimal? FinalAmountCommission
        {
            get { return this._finalAmountCommission; }
            set { this._finalAmountCommission = value; }
        }


        [DataMember(Order=55)]
        public decimal? Rate100PercentForAllKeep
        {
            get { return this._rate100PercentForAllKeep; }
            set { this._rate100PercentForAllKeep = value; }
        }


        [DataMember(Order=56)]
        public string  PrintDate
        {
            get { return this._printDate; }
            set { this._printDate = value; }
        }
               

        [DataMember(Order=57)]
        public decimal? Vat7Percent
        {
            get { return this._vat7Percent; }
            set { this._vat7Percent = value; }
        }


        [DataMember(Order=58)]
        public decimal?  TaxAmount
        {
            get { return this._taxRevenue; }
            set { this._taxRevenue = value; }
        }


        [DataMember(Order=59)]
        public string CalculateDate
        {
            get { return this._calculateDate; }
            set { this._calculateDate = value; }
        }



        [DataMember(Order=60)]
        public int? BillOutToAgencyForGovermentPaid
        {
            get { return this._billOutToAgencyForGovermentPaid; }
            set { this._billOutToAgencyForGovermentPaid = value; }
        }

        [DataMember(Order=61)]
        public decimal? ValueOfBillOutForGovermentPaid
        {
            get { return this._valueOfBillOutForGovermentPaid; }
            set { this._valueOfBillOutForGovermentPaid = value; }
        }

        [DataMember(Order=62)]
        public int? CanKeepElectricForGovermentPaid
        {
            get { return this._canKeepElectricForGovermentPaid; }
            set { this._canKeepElectricForGovermentPaid = value; ; }
        }

        [DataMember(Order=63)]
        public decimal? ValueOfKeepElectricForGovermentPaid
        {
            get { return this._valueOfKeepElectricForGovermentPaid; }
            set { this._valueOfKeepElectricForGovermentPaid = value; }
        }

        [DataMember(Order=64)]
        public decimal? RateAgencyPersonTypeForGovermentPaid
        {
            get { return this._rateAgencyPersonTypeForGovermentPaid; }
            set { this._rateAgencyPersonTypeForGovermentPaid = value; }
        }

        [DataMember(Order=65)]
        public decimal? RateAgencyCompanyTypeForGovermentPaid
        {
            get { return this._rateAgencyCompanyTypeForGovermentPaid; }
            set { this._rateAgencyCompanyTypeForGovermentPaid = value; }
        }

        [DataMember(Order=66)]
        public decimal? AmountCommissionOfGovermentPaid
        {
            get { return this._amountCommissionOfGovermentPaid; }
            set { this._amountCommissionOfGovermentPaid = value; }
        }


    #endregion
    }
}
