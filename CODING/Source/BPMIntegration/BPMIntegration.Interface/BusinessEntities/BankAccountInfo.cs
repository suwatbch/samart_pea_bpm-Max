using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using System.Globalization;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Integration.BPMIntegration.Interface.Constants;

using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class BankAccountInfo : IListUtility<BankAccountInfo>
    {
        private string _Bankkey;
        private string _AccountNo;
        private string _BusinessPlace;
        private string _ClearingAccNo;
        private string _HouseBank;
        private string _AccountType;
        private string _SyncFlag;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string BankKey
        {
            get { return _Bankkey; }
            set { _Bankkey = value; }
        }
        [DataMember(Order = 2)]
        public string AccountNo
        {
            get { return _AccountNo; }
            set { _AccountNo = value; }
        }
        [DataMember(Order = 3)]
        public string BusinessPlace
        {
            get { return _BusinessPlace; }
            set { _BusinessPlace = value; }
        }
        [DataMember(Order = 4)]
        public string ClearingAccNo
        {
            get { return _ClearingAccNo; }
            set { _ClearingAccNo = value; }
        }
        [DataMember(Order = 5)]
        public string HouseBank
        {
            get { return _HouseBank; }
            set { _HouseBank = value; }
        }
        [DataMember(Order = 6)]
        public string AccountType
        {
            get { return _AccountType; }
            set { _AccountType = value; }
        }
        [DataMember(Order = 7)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 8)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 9)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 10)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 11)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

        #region IListUtility<BankAccountInfo> Members

        public string ToStream()
        {
            //IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("th-TH");
            //string[] template = { BankKey, AccountNo, AccountName, BusinessPlace, Action };
            //return string.Join("\t", template);
            throw new Exception("The Method is not implement yet");
        }

        public BankAccountInfo ParseExtract(string txt)
        {
            IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("en-EN");
            BankAccountInfo it = new BankAccountInfo();
            string[] sp = txt.Split('|');

            int colFixed = InterfaceColumns.BankAccounts;
            int colCounted = sp.Length - 1;
            if (colCounted != colFixed)
                throw new Exception("ไม่สามารถทำรายการได้ เนื่องจาก Columns ใน text file มีจำนวน " + colCounted.ToString() + " แต่ Columns ที่กำหนดไว้มีจำนวน " + colFixed.ToString());


            int i = 1;
            it.BankKey = StringConvert.ToString(sp[i++].Trim());
            it.AccountNo = StringConvert.ToString(sp[i++].Trim());
            it.BusinessPlace = StringConvert.ToString(sp[i++].Trim());
            it.ClearingAccNo = StringConvert.ToString(sp[i++].Trim());
            it.HouseBank = StringConvert.ToString(sp[i++].Trim());
            it.AccountType = StringConvert.ToString(sp[i++].Trim());
            it.Action = sp[i++].Trim();

            if (it.Action != "O" && it.Action != "1" && it.Action != "2" && it.Action != "3")
                throw new Exception("ไม่สามารถทำรายการได้ เนื่องจากข้อมูลของ [Action] มีค่าเท่ากับ " + it.Action + "] ซึ่งไม่ตรงตามรูปแบบที่กำหนดไว้");

            it.ModifiedBy = "BATCH";
            it.ModifiedDt = DateTime.Now;
            it.SyncFlag = "1";
            it.Active = true;
            return it;
        }

        #endregion
    }
}
