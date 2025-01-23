using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports
{
    [DataContract]
    public class RE07_ReportInfo
    {
        string _accountClassId = String.Empty ;
		string _accountClassName = String.Empty;
		string _companyId = String.Empty;
        string _companyName = String.Empty;
		string _uploadDate = String.Empty;
		int? _totalBillCount = 0;
		decimal? _totalBillAmount = 0;
		string _receiptId = String.Empty;
		string _bankKey = String.Empty;
		string _bankName = String.Empty ;
		string _tranfAccNo = String.Empty;
		string _tranfDate = String.Empty;
		decimal? _amount = 0;


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
		public int? TotalBillCount
        {
            get { return this._totalBillCount;}
            set { this._totalBillCount = value;}
        }

        [DataMember(Order=7)]
		public decimal? TotalBillAmount
        {
            get { return this._totalBillAmount; }
            set { this._totalBillAmount = value; }
        }
		

        [DataMember(Order=8)]
        public string ReceiptId
        {
            get { return this._receiptId; }
            set { this._receiptId =value; }
        }

        [DataMember(Order=9)]
		public string BankKey
        {
            get { return this._bankKey;}
            set { this._bankKey = value;}
        }

        [DataMember(Order=10)]
		public string BankName
        {
            get { return this._bankName;}
            set { this._bankName = value;}
        }

        [DataMember(Order=11)]
		public string TranfAccNo
        {
            get { return this._tranfAccNo; }
            set { this._tranfAccNo = value;}
        }

        [DataMember(Order=12)]
		public string TranfDate
        {
            get { return this._tranfDate; }
            set { this._tranfDate = value; }
        }

        [DataMember(Order=13)]
        public decimal? Amount
        {
            get { return this._amount; }
            set { this._amount = value; }
        }
    }
}
