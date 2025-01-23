using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Integration.BPMIntegration.Interface.Constants;

using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class MainSubInfo : IListUtility<MainSubInfo>
    {
        private string _DtId;
        private string _DtName;
        private string _DefaultPaperSize;
        private string _DefaultTaxCode;
        private string _DefaultInterestKey;
        private string _NonInvoicePaymentFlag;
        private string _CategoryPaymentCode;
        private string _ModReceiptHeaderFlag;
        private string _IndividualReceiptFlag;
        private string _SyncFlag;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        private string _accountNo;
        private string _taxCode;
        private string _accountCat;

        [DataMember(Order = 1)]
        public string DtId
        {
            get { return _DtId; }
            set { _DtId = value; }
        }
        [DataMember(Order = 2)]
        public string DtName
        {
            get { return _DtName; }
            set { _DtName = value; }
        }
        [DataMember(Order = 3)]
        public string DefaultPaperSize
        {
            get { return _DefaultPaperSize; }
            set { _DefaultPaperSize = value; }
        }
        [DataMember(Order = 4)]
        public string DefaultTaxCode
        {
            get { return _DefaultTaxCode; }
            set { _DefaultTaxCode = value; }
        }
        [DataMember(Order = 5)]
        public string DefaultInterestKey
        {
            get { return _DefaultInterestKey; }
            set { _DefaultInterestKey = value; }
        }
        [DataMember(Order = 6)]
        public string NonInvoicePaymentFlag
        {
            get { return _NonInvoicePaymentFlag; }
            set { _NonInvoicePaymentFlag = value; }
        }
        [DataMember(Order = 7)]
        public string CategoryPaymentCode
        {
            get { return _CategoryPaymentCode; }
            set { _CategoryPaymentCode = value; }
        }
        [DataMember(Order = 8)]
        public string ModReceiptHeaderFlag
        {
            get { return _ModReceiptHeaderFlag; }
            set { _ModReceiptHeaderFlag = value; }
        }
        [DataMember(Order = 9)]
        public string IndividualReceiptFlag
        {
            get { return _IndividualReceiptFlag; }
            set { _IndividualReceiptFlag = value; }
        }
        [DataMember(Order = 10)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 11)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 12)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 13)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 14)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

        [DataMember(Order = 15)]
        public string AccountNo
        {
            get { return _accountNo; }
            set { _accountNo = value; }
        }
        [DataMember(Order = 16)]
        public string TaxCode
        {
            get { return _taxCode; }
            set { _taxCode = value; }
        }
        [DataMember(Order = 17)]
        public string AccountCat
        {
            get { return _accountCat; }
            set { _accountCat = value; }
        }

        #region IListUtility<MainSubInfo> Members

        public string ToStream()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public MainSubInfo ParseExtract(string txt)
        {
            MainSubInfo it = new MainSubInfo();
            string[] sp = txt.Split('|');

            int colFixed = InterfaceColumns.MainSub;
            int colCounted = sp.Length - 1;
            if (colCounted != colFixed)
                throw new Exception("ไม่สามารถทำรายการได้ เนื่องจาก Columns ใน text file มีจำนวน " + colCounted.ToString() + " แต่ Columns ที่กำหนดไว้มีจำนวน " + colFixed.ToString());


            int i = 1;
            it.DtId = StringConvert.ToString(sp[i++].Trim()) + StringConvert.ToString(sp[i++].Trim());
            it.DtName = StringConvert.ToString(sp[i++].Trim());
            it.TaxCode = StringConvert.ToString(sp[i++].Trim());
            it.AccountCat = StringConvert.ToString(sp[i++].Trim());
            it.NonInvoicePaymentFlag = StringConvert.ToString(sp[i++].Trim());
            it.Action = StringConvert.ToString(sp[i++].Trim());

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
