using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Infrastructure.Interface.BusinessEntities
{
    [DataContract]
    public class BankAccount: IListItem
    {
        private string _bankKey;

        [DataMember(Order=1)]
        public string BankKey
        {
            get { return _bankKey; }
            set { _bankKey = value; }
        }

        private string _accountNo;

        [DataMember(Order=2)]
        public string AccountNo
        {
            get { return _accountNo; }
            set { _accountNo = value; }
        }

        private string _businessPlace;

        [DataMember(Order=3)]
        public string BusinessPlace
        {
            get { return _businessPlace; }
            set { _businessPlace = value; }
        }               
  
        public BankAccount(string bankKey, string accountNo, string businessPlace)
        {
            _bankKey = bankKey;
            _accountNo = accountNo;
            _businessPlace = businessPlace;
        }

        #region IListItem Members


        //[DataMember(Order=4)]
        public string DisplayText
        {
            get { return _accountNo; }
        }

        #endregion
    }
}
