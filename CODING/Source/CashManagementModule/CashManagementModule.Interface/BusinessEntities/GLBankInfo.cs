using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.CashManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class GLBankInfo
    {
        private string _bankKey;
        private string _bankHouse;
        private string _bankName;


        [DataMember(Order=1)]
        public string BankKey
        {
            set { _bankKey = value; }
            get { return _bankKey; }
        }


        [DataMember(Order=2)]
        public string BankHouse
        {
            set { _bankHouse = value; }
            get { return _bankHouse; }
        }


        [DataMember(Order=3)]
        public string BankName
        {
            set { _bankName = value; }
            get { return _bankName; }
        }

    }
}
