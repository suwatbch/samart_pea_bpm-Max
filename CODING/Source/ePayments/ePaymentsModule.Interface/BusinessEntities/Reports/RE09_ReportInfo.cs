using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports
{
    [DataContract]
    public class RE09_ReportInfo
    {
        string _payDate = String.Empty;
        string _accountClassName = String.Empty;
        string _companyId = String.Empty;
        string _companyName = String.Empty;
        string _branchid = String.Empty;
        string _branchName = String.Empty;
        int? _billCount = 0;
        decimal? _totalAmount = 0;


        [DataMember(Order=1)]
        public string PayDate
        {
            get { return this._payDate; }
            set { this._payDate = value; }
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
        public string Branchid
        {
            get { return this._branchid; }
            set { this._branchid = value; }
        }


        [DataMember(Order=6)]
        public string BranchName
        {
            get { return this._branchName; }
            set { this._branchName = value; }
        }

        [DataMember(Order=7)]
        public int? BillCount
        {
            get { return this._billCount; }
            set { this._billCount = value; }
        }

        [DataMember(Order=8)]
        public decimal? TotalAmount
        {
            get { return this._totalAmount; }
            set { this._totalAmount = value; }
        }
    }
}
