using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;


namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities
{
    [DataContract]
    public class AccountClassInfo
    {
        private string _accountClassId = String.Empty;
        private string _accountClassName = String.Empty;


        public AccountClassInfo()
        {
        } 
       

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


        //[DataMember(Order=3)]
        public string DisplayName
        {
            get
            {
                return String.Format("{0} : {1}", AccountClassId, AccountClassName);
            }
        }
    }
}
