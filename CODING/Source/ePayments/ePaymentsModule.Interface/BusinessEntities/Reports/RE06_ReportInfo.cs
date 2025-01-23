using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports
{
    [DataContract]
    public class RE06_ReportInfo
    {
        
        private string _accountClassId = String.Empty;
        private string _accountClassName = String.Empty;
        private string _companyId = String.Empty;
        private string _companyName = String.Empty;
        private string _mainBranchId = String.Empty;
        private string _mainBranchName = String.Empty;
        private string _branchId = String.Empty;
        private string _branchName = String.Empty;
        private string  _paidDate = String.Empty;
        private int _caCount = 0;
        private decimal? _totalAmount = 0;


        [DataMember(Order=1)]
        public string AccountClassId
        {
            get { return this._accountClassId; }
            set { this._accountClassId = value; }
        }


        [DataMember(Order=2)]
        public string AccountClassName
        {
            get { return this._accountClassName; }
            set { this._accountClassName = value; }
        }


        [DataMember(Order=3)]
        public string CompanyId
        {
            get { return this._companyId; }
            set { this._companyId = value; }
        }


        [DataMember(Order=4)]
        public string CompanyName
        {
            get { return this._companyName; }
            set { this._companyName = value; }
        }


        [DataMember(Order=5)]
        public string MainBranchId
        {
            get { return this._mainBranchId; }
            set { this._mainBranchId = value; }
        }
        

        [DataMember(Order=6)]
        public string MainBranchName
        {
            get { return this._mainBranchId; }
            set { this._mainBranchId = value; }
        }


        [DataMember(Order=7)]
        public string  BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }


        [DataMember(Order=8)]
        public string BranchName
        {
            get { return this._branchName; }
            set { this._branchName = value; }
        }


        [DataMember(Order=9)]
        public string  PaidDate
        {
            get { return this._paidDate; }
            set { this._paidDate = value; }
        }


        [DataMember(Order=10)]
        public int CaCount
        {
            get { return this._caCount; }
            set { this._caCount = value; }
        }


        [DataMember(Order=11)]
        public decimal? TotalAmount
        {
            get { return this._totalAmount; }
            set
            {
                this._totalAmount = value;
            }
        }


    }
}
