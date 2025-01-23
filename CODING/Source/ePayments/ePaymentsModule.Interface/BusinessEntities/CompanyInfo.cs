using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities
{
    [DataContract]
    public class Company
    {
        private string _companyId;
        private string _companyName;

        public Company()
        {
            //Default Constructor
        }

        public Company(string compId, string compName)
        {
            this._companyId = compId;
            this._companyName = compName;
        }


        [DataMember(Order=1)]
        public string CompanyId
        {
            get { return this._companyId; }
            set { this._companyId = value; }
        }


        [DataMember(Order=2)]
        public string CompanyName
        {
            get { return this._companyName; }
            set { this._companyName = value; }
        }


        //[DataMember(Order=3)]
        public string DisplayName
        {
            get
            {
                return String.Format("{0} : {1}", CompanyId, CompanyName);
            }
        }
    }
}
