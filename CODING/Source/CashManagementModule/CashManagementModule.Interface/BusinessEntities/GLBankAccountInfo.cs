using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.CashManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class GLBankAccountInfo
    {
        private string _bankKey;
        private string _bankName;
        private string _bankHouse;
        private string _bankAccount;
        private string _accountType;
        private string _clearingAccNo;


        [DataMember(Order=1)]
        public string BankKey
        {
            set { _bankKey = value; }
            get { return _bankKey; }
        }

        [DataMember(Order=2)]
        public string BankName
        {
            set { _bankName = value; }
            get { return _bankName; }
        }

        [DataMember(Order=3)]
        public string BankHouse
        {
            set { _bankHouse = value; }
            get { return _bankHouse; }
        }


        [DataMember(Order=4)]
        public string BankAccount
        {
            set { _bankAccount = value; }
            get { return _bankAccount; }
        }


        [DataMember(Order=5)]
        public string AccountType
        {
            set { _accountType = value; }
            get { return _accountType; }
        }


        [DataMember(Order=6)]
        public string ClearingAccNo
        {
            set { _clearingAccNo = value; }
            get { return _clearingAccNo; }
        }

        //Checked TongKung
        //[DataMember(Order=5)]
        public string BankAccountWithAccountType
        {
            //get { return _accountType.ToUpper()=="S" ? string.Format("%0(%1)",_accountType, AccountType):_accountType; }
            get { return _accountType == String.Empty || _accountType == null ? _bankAccount : string.Format("{0}({1})", _bankAccount, AccountType); }
        }
    }
}
