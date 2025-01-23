using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Infrastructure.Interface.BusinessEntities
{
    [DataContract,Serializable]
    public class Bank : IListItem
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

        private string _groupBankName;

        [DataMember(Order = 5)]
        public string GroupBankName
        {
            get { return _groupBankName; }
            set { _groupBankName = value; }
        }

        public Bank() { }

        public Bank(string bankKey, string bankName)
        {
            this._bankKey = bankKey;
            this._bankName = bankName;
        }

        public Bank(string bankKey, string bankName, string businessPlace, string clearingAccNo, string groupBankName)
        {
            this._bankKey = bankKey;
            this._bankName = bankName;
            this._businessPlace = businessPlace;
            this._clearingAccNo = clearingAccNo;
            this._groupBankName = groupBankName;
        }

        #region IListItem Members


        //[DataMember(Order=5)]
        public string DisplayText
        {
            get { return string.Format("{0}-{1}", _bankKey, _bankName); }
        }

        #endregion
    }
}
