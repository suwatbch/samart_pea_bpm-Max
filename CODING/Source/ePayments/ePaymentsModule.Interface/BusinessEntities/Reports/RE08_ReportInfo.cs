using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports
{
    [DataContract]
    public class RE08_ReportInfo
    {
        string _accountClassId = String.Empty ;
		string _accountClassName = String.Empty;
		string _companyId = String.Empty;
        string _companyName = String.Empty;
		string _uploadDate = String.Empty;
        string _payDate = String.Empty;
        int? _billCount = 0;
        decimal? _billAmount = 0;
		int? _totalBillCount = 0;
		decimal? _totalBillAmount = 0;
		


        [DataMember(Order=1)]
        public string AccountClassId
        {
            get 
            {
                return this._accountClassId;
            }
            set  
            {
                this._accountClassId = value;
            }
        }

        [DataMember(Order=2)]
		public string AccountClassName
        {
            get { return this._accountClassName; }
            set  { this._accountClassName = value;}
        }
		

        [DataMember(Order=3)]
        public string CompanyId
        {
            get { return this._companyId;}
            set { this._companyId = value;}
        }

        [DataMember(Order=4)]
        public string CompanyName
        {
            get { return this._companyName; }
            set { this._companyName = value; }
        }


        [DataMember(Order=5)]
		public string UploadDate
        {
            get { return this._uploadDate;}
            set { this._uploadDate = value;}
        }


        [DataMember(Order=6)]
        public string PayDate
        {
            get { return this._payDate; }
            set { this._payDate = value; }
        }


        [DataMember(Order=7)]
		public int? BillCount
        {
            get { return this._billCount;}
            set { this._billCount = value;}
        }

        [DataMember(Order=8)]
		public decimal? BillAmount
        {
            get { return this._billAmount; }
            set { this._billAmount = value; }
        }


        [DataMember(Order=9)]
        public int? TotalBillCount
        {
            get { return this._totalBillCount; }
            set { this._totalBillCount = value; }
        }

        [DataMember(Order=10)]
        public decimal? TotalBillAmount
        {
            get { return this._totalBillAmount; }
            set { this._totalBillAmount = value; }
        }		
    }
}
