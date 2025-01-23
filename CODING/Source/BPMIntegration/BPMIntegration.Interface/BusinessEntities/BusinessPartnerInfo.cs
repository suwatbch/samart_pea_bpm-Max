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
    public class BusinessPartnerInfo : IListUtility<BusinessPartnerInfo>
    {
        private string _BpId;
        private string _BpTypeId;
        private string _TaxCode;
        private string _CitizenId;
        private string _PassportNo;
        private string _RegisterId;
        private string _VatCode;
        private string _SyncFlag;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string BpId
        {
            get { return _BpId; }
            set { _BpId = value; }
        }
        [DataMember(Order = 2)]
        public string BpTypeId
        {
            get { return _BpTypeId; }
            set { _BpTypeId = value; }
        }
        [DataMember(Order = 3)]
        public string TaxCode
        {
            get { return _TaxCode; }
            set { _TaxCode = value; }
        }
        [DataMember(Order = 4)]
        public string CitizenId
        {
            get { return _CitizenId; }
            set { _CitizenId = value; }
        }
        [DataMember(Order = 5)]
        public string PassportNo
        {
            get { return _PassportNo; }
            set { _PassportNo = value; }
        }
        [DataMember(Order = 6)]
        public string RegisterId
        {
            get { return _RegisterId; }
            set { _RegisterId = value; }
        }
        [DataMember(Order = 7)]
        public string VatCode
        {
            get { return _VatCode; }
            set { _VatCode = value; }
        }
        [DataMember(Order = 8)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 9)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 10)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 11)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 12)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

       #region IListUtility<BusinessPartnerInfo> Members

       public string ToStream()
       {
           //IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("th-TH");
           //string[] template = { BpId, BpTypeId, TaxCode, CitizenId, PassportNo,  RegisterId, VatCode, Action};
           //return string.Join("\t", template);
           throw new Exception("The method or operation is not implemented.");
       }

       public BusinessPartnerInfo ParseExtract(string txt)
       {
           BusinessPartnerInfo it = new BusinessPartnerInfo();
           string[] sp = txt.Split('|');

           int colFixed = InterfaceColumns.BusinessPartner;
           int colCounted = sp.Length - 1;
           if (colCounted != colFixed)
               throw new Exception("ไม่สามารถทำรายการได้ เนื่องจาก Columns ใน text file มีจำนวน " + colCounted.ToString() + " แต่ Columns ที่กำหนดไว้มีจำนวน " + colFixed.ToString());


           int i = 1;
           it.BpId = StringConvert.ToString(sp[i++].Trim());
           it.BpTypeId = StringConvert.ToString(sp[i++].Trim());
           if (it.BpTypeId == null) it.BpTypeId = "4";
           it.TaxCode = StringConvert.ToString(sp[i++].Trim());
           it.CitizenId = StringConvert.ToString(sp[i++].Trim());
           it.PassportNo = StringConvert.ToString(sp[i++].Trim());
           it.RegisterId = StringConvert.ToString(sp[i++].Trim());
           it.VatCode = StringConvert.ToString(sp[i++].Trim());

           string cancelFlag = StringConvert.ToString(sp[i++].Trim());

           if (cancelFlag == "1")
               it.Action = "3";
           else
               it.Action = sp[i++].Trim();

           if (it.Action != "O" && it.Action != "0" && it.Action != "1" && it.Action != "2" && it.Action != "3")
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
