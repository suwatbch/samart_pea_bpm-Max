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
    public class TaxCodeInfo : IListUtility<TaxCodeInfo>
    {

        private string _TaxCode;
        private string _TaxName;
        private decimal? _TaxRate;
        private string _SyncFlag;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;
        private string _taxable;

        [DataMember(Order = 1)]
        public string TaxCode
        {
            get { return _TaxCode; }
            set { _TaxCode = value; }
        }
        [DataMember(Order = 2)]
        public string TaxName
        {
            get { return _TaxName; }
            set { _TaxName = value; }
        }
        [DataMember(Order = 3)]
        public decimal? TaxRate
        {
            get { return _TaxRate; }
            set { _TaxRate = value; }
        }
        [DataMember(Order = 4)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 5)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 6)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 7)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 8)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }
        [DataMember(Order = 9)]
        public string Taxable
        {
            get { return _taxable; }
            set { _taxable = value; }
        }


        #region IListUtility<TaxCodeInfo> Members

        public string ToStream()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public TaxCodeInfo ParseExtract(string txt)
        {
            TaxCodeInfo it = new TaxCodeInfo();
            string[] sp = txt.Split('|');
            string taxRate = null;

            int colFixed = InterfaceColumns.TaxCode;
            int colCounted = sp.Length - 1;
            if (colCounted != colFixed)
                throw new Exception("ไม่สามารถทำรายการได้ เนื่องจาก Columns ใน text file มีจำนวน " + colCounted.ToString() + " แต่ Columns ที่กำหนดไว้มีจำนวน " + colFixed.ToString());

            int i = 1;
            it.TaxCode = StringConvert.ToString(sp[i++].Trim());
            it.TaxName = StringConvert.ToString(sp[i++].Trim());
            it.Taxable = StringConvert.ToString(sp[i++].Trim());
            
            if (it.Taxable == "0")
            {
                it.TaxRate = null;
                i++;
            }
            else
                taxRate = sp[i++].Trim();
            
            it.Action = sp[i++].Trim();

            if(it.Action != "3")
                it.TaxRate = StringConvert.ToDecimal(taxRate);

            if (it.Action != "O" && it.Action != "1" && it.Action != "2" && it.Action != "3")
                throw new Exception("ไม่สามารถทำรายการได้ เนื่องจากข้อมูลของ [Action] มีค่าเท่ากับ " + it.Action + "] ซึ่งไม่ตรงตามรูปแบบที่กำหนดไว้");

            it.SyncFlag = "1";
            it.ModifiedBy = "BATCH";
            it.ModifiedDt = DateTime.Now;
            it.Active = true;
            return it;
        }

        #endregion
    }
}

