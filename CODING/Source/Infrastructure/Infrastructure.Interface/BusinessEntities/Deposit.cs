using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Infrastructure.Interface.BusinessEntities
{
    [DataContract]
    public class Deposit : IListItem
    {
        private string _bankKey;

        [DataMember(Order=1)]
        public string BankKey
        {
            get { return _bankKey; }
            set { _bankKey = value; }
        }

        private string _bankName;

        [DataMember(Order=2)]
        public string BankName
        {
            get { return _bankName; }
            set { _bankName = value; }
        }

        private string _businessPlace;

        [DataMember(Order=3)]
        public string BusinessPlace
        {
            get { return _businessPlace; }
            set { _businessPlace = value; }
        }

        private string _clearingAccNo;

        [DataMember(Order=4)]
        public string ClearingAccNo
        {
            get { return _clearingAccNo; }
            set { _clearingAccNo = value; }
        }

        private string _accountNo;

        [DataMember(Order=5)]
        public string AccountNo
        {
            get { return _accountNo; }
            set { _accountNo = value; }
        }

        private string _accountType;

        [DataMember(Order = 6)]
        public string AccountType
        {
            get { return _accountType; }
            set { _accountType = value; }
        }

        private string _accountNoDesc;

        [DataMember(Order = 7)]
        public string AccountNoDesc
        {
            get { return _accountNoDesc; }
            set { _accountNoDesc = value; }
        }

        private string _groupBankName;

        [DataMember(Order = 8)]
        public string GroupBankName
        {
            get { return _groupBankName; }
            set { _groupBankName = value; }
        }

        public Deposit() { }

        public Deposit(string bankKey, string bankName, string businessPlace, string clearingAccNo, string accountNo, string accountType, string accountNoDesc, string groupBankName)
        {
            this._bankKey = bankKey;
            this._bankName = bankName;
            this._businessPlace = businessPlace;
            this._clearingAccNo = clearingAccNo;
            this._accountNo = accountNo;
            this._accountType = accountType;
            //this._accountNoDesc = accountNoDesc;
            this._accountNoDesc = accountType.Length == 0 || accountNo.Length == 0 ? accountNo : string.Format("{0}({1})", accountNo, AccountType);
            this._groupBankName = groupBankName;
        }

        #region IListItem Members


        //[DataMember(Order=6)]
        public string DisplayText
        {
            get { return string.Format("{0}-{1}", _bankKey, _clearingAccNo); }
        }

        #endregion
    }
}
