using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports
{
    [DataContract]
    public class RE05_ReportInfo
    {
        private string _accountClassId;
        private string _accountClassName;
        private string _companyId;
        private string _company;       
        private string _payDt;
        private int? _caNumber;
        private decimal? _amount;


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
        public string Company
        {
            get { return this._company; }
            set { this._company = value; }
        }       


        [DataMember(Order=5)]
        public string PayDt
        {
            get { return this._payDt; }
            set { this._payDt = value; }
        }


        [DataMember(Order=6)]
        public int? CaNumber
        {
            get { return this._caNumber; }
            set { this._caNumber = value; }
        }


        [DataMember(Order=7)]
        public decimal? Amount
        {
            get { return this._amount; }
            set { this._amount = value; }
        }

    }
}
