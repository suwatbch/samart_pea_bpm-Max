using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CommissionVoucherInfoReport
    {
        #region "Local Varible"
        private string _peaCode;
        private string _peaName;
        private string _printDate;
        private string _voucherCode;
        private string _voucherDate;
        private string _issueCode;
        private string _issueName;
        private string _issueTypeName;
        private string _issueTaxNo;
        private string _issueAddress;
        private string _accountCode;
        private int? _totalItemOfResidentType = 0;
        private decimal? _rateOfResident = 0;
        private decimal? _amountCommissionOfResidentType = 0;
        private int? _totalItemOfSmallBizType = 0;
        private decimal? _rateOfSmallBiz = 0;
        private decimal? _amountCommissionOfSmallBizType = 0;
        private int? _totalItemOfGovermentDepartmentType = 0;
        private decimal? _rateOfGovermentDepartment = 0;
        private decimal? _amountCommissionOfGovermentDepartmentType = 0;

        private int? _totalItemOfGovermentPaidType = 0;
        private decimal? _rateOfGovermentPaid = 0;
        private decimal? _amountCommissionOfGovermentPaidType = 0;
        
        private int? _totalItemOfSentBillType = 0;
        private decimal? _rateOfSentBill = 0;
        private decimal? _amountCommissionOfSentBillType = 0;
        private decimal? _transportOfCarPrice = 0;
        private decimal? _transportOfWaterPrice = 0;
        private decimal? _allTransportPrice = 0;
        private decimal? _totalMoneySpacialCase = 0;
        private decimal? _totalAllCommission = 0;
        private decimal? _fineSendMoneyLate = 0;
        private decimal? _realCommissionPaidAgency = 0;
        private string _totalAllCommissionText;
        private decimal? _taxPrice = 0;
        private decimal? _vat7Percent = 0;
        private decimal? _debtAmount = 0;
        #endregion

        #region "Porperty"

        [DataMember(Order=1)]
        public string PEACode
        {
            get { return this._peaCode; }
            set { this._peaCode = value; }
        }


        [DataMember(Order=2)]
        public string PEAName
        {
            get { return this._peaName; }
            set { this._peaName = value; }
        }


        [DataMember(Order=3)]
        public string PrintDate
        {
            get { return this._printDate; }
            set { this._printDate = value; }
        }


        [DataMember(Order=4)]
        public string VoucherCode
        {
            get { return this._voucherCode; }
            set { this._voucherCode = value; }
        }


        [DataMember(Order=5)]
        public string VoucherDate
        {
            get { return this._voucherDate; }
            set { this._voucherDate = value; }
        }


        [DataMember(Order=6)]
        public string IssueCode
        {
            get { return this._issueCode; }
            set { this._issueCode = value; }
        }
        

        [DataMember(Order=7)]
        public string IssueName
        {
            get { return this._issueName; }
            set { this._issueName = value; }
        }


        [DataMember(Order=8)]
        public string IssueTypeName
        {
            get { return this._issueTypeName; }
            set { this._issueTypeName = value; }
        }


        [DataMember(Order=9)]
        public string IssueTaxNo
        {
            get { return this._issueTaxNo; }
            set { this._issueTaxNo = value; }
        }


        [DataMember(Order=10)]
        public string IssueAddress
        {
            get { return this._issueAddress; }
            set { this._issueAddress = value; }
        }       


        [DataMember(Order=11)]
        public int? TotalItemOfResidentType
        {
            get { return this._totalItemOfResidentType; }
            set { this._totalItemOfResidentType = value; }
        }


        [DataMember(Order=12)]
        public decimal? RateOfResidentType
        {
            get { return this._rateOfResident; }
            set { this._rateOfResident = value; }
        }


        [DataMember(Order=13)]
        public decimal? AmountCommissionOfResidentType
        {
            get { return this._amountCommissionOfResidentType; }
            set { this._amountCommissionOfResidentType = value; }
        }


        [DataMember(Order=14)]
        public int? TotalItemOfSmallBizType
        {
            get { return this._totalItemOfSmallBizType; }
            set { this._totalItemOfSmallBizType = value; }
        }


        [DataMember(Order=15)]
        public decimal? RateOfSmallBizType
        {
            get { return this._rateOfSmallBiz; }
            set { this._rateOfSmallBiz = value; }
        }


        [DataMember(Order=16)]
        public decimal? AmountCommissionOfSmallBizType
        {
            get { return this._amountCommissionOfSmallBizType; }
            set { this._amountCommissionOfSmallBizType = value; }
        }


        [DataMember(Order=17)]
        public int? TotalItemOfGovermentDepartmentType
        {
            get { return this._totalItemOfGovermentDepartmentType; }
            set { this._totalItemOfGovermentDepartmentType = value; }
        }


        [DataMember(Order=18)]
        public decimal? RateOfGovermentDepartmentType
        {
            get { return this._rateOfGovermentDepartment; }
            set { this._rateOfGovermentDepartment = value; }
        }


        [DataMember(Order=19)]
        public decimal? AmountCommissionOfGovermentDepartmentType
        {
            get { return this._amountCommissionOfGovermentDepartmentType; }
            set { this._amountCommissionOfGovermentDepartmentType = value; }
        }


        [DataMember(Order=20)]
        public int? TotalItemOfGovermentPaidType
        {
            get { return this._totalItemOfGovermentPaidType; }
            set { this._totalItemOfGovermentPaidType = value; }
        }


        [DataMember(Order=21)]
        public decimal? RateOfGovermentPaidType
        {
            get { return this._rateOfGovermentPaid; }
            set { this._rateOfGovermentPaid = value; }
        }


        [DataMember(Order=22)]
        public decimal? AmountCommissionOfGovermentPaidType
        {
            get { return this._amountCommissionOfGovermentPaidType; }
            set { this._amountCommissionOfGovermentPaidType = value; }
        }


        [DataMember(Order=23)]
        public int? TotalItemOfSentBillType
        {
            get { return this._totalItemOfSentBillType; }
            set { this._totalItemOfSentBillType = value; }
        }


        [DataMember(Order=24)]
        public decimal? RateOfSentBillType
        {
            get { return this._rateOfSentBill; }
            set { this._rateOfSentBill = value; }
        }


        [DataMember(Order=25)]
        public decimal? AmountCommissionOfSentBillType
        {
            get { return this._amountCommissionOfSentBillType; }
            set { this._amountCommissionOfSentBillType = value; }
        }


        [DataMember(Order=26)]
        public decimal? TransportOfCarPrice
        {
            get { return this._transportOfCarPrice; }
            set { this._transportOfCarPrice = value; }
        }


        [DataMember(Order=27)]
        public decimal? TransportOfWaterPrice
        {
            get { return this._transportOfWaterPrice; }
            set { this._transportOfWaterPrice = value; }
        }


        [DataMember(Order=28)]
        public decimal? AllTransportPrice
        {
            get { return this._allTransportPrice; }
            set { this._allTransportPrice = value; }
        }
 

        [DataMember(Order=29)]
        public decimal? TotalMoneySpacialCase
        {
            get { return this._totalMoneySpacialCase; }
            set { this._totalMoneySpacialCase = value; }
        }


        [DataMember(Order=30)]
        public decimal? TotalAllCommission
        {
            get { return this._totalAllCommission; }
            set { this._totalAllCommission = value; }
        }


        [DataMember(Order=31)]
        public decimal? FineSendMoneyLate
        {
            get { return this._fineSendMoneyLate; }
            set { this._fineSendMoneyLate = value; }
        }


        [DataMember(Order=32)]
        public decimal? RealCommissionPaidAgency
        {
            get { return this._realCommissionPaidAgency; }
            set { this._realCommissionPaidAgency = value; }
        }


        [DataMember(Order=33)]
        public string TotalAllCommissionText
        {
            get { return this._totalAllCommissionText; }
            set { this._totalAllCommissionText = value; }
        }

        [DataMember(Order=34)]
        public decimal? TaxAmount
        {
            get { return this._taxPrice; }
            set { this._taxPrice = value; }
        }

        [DataMember(Order=35)]
        public decimal? Vat7Percent
        {
            get { return this._vat7Percent; }
            set { this._vat7Percent = value; }
        }


        [DataMember(Order=36)]
        public decimal? DebtAmount
        {
            get { return this._debtAmount; }
            set { this._debtAmount = value; }
        }
        #endregion
    }
}
