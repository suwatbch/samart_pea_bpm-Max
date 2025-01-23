using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Integration.BPMIntegration.Interface.Constants;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [Serializable]
    public class CompanyInfo : IListUtility<CompanyInfo>
    {
        string _syncFlag;
        public string SyncFlag
        {
            get { return _syncFlag; }
            set { _syncFlag = value; }
        }

        DateTime? _modifiedDt;
        public DateTime? ModifiedDt
        {
            get { return _modifiedDt; }
            set { _modifiedDt = value; }
        }

        string _modifiedBy;
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        bool? _active;
        public bool? Active
        {
            get { return _active; }
            set { _active = value; }
        }

        string _action;
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

        #region IListUtility<CompanyInfo> Members

        public string ToStream()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public CompanyInfo ParseExtract(string txt)
        {
            CompanyInfo obj = new CompanyInfo();
            string[] sp = txt.Split('|');

            int colFixed = InterfaceColumns.Company;
            int colCounted = sp.Length - 1;
            if (colCounted != colFixed)
                throw new Exception("ไม่สามารถทำรายการได้ เนื่องจาก Columns ใน text file มีจำนวน " + colCounted.ToString() + " แต่ Columns ที่กำหนดไว้มีจำนวน " + colFixed.ToString());


            int i = 1;
         
            obj.Action = StringConvert.ToString(sp[i++].Trim());

            if (obj.Action != "O" && obj.Action != "1" && obj.Action != "2" && obj.Action != "3")
                throw new Exception("ไม่สามารถทำรายการได้ เนื่องจากข้อมูลของ [Action] มีค่าเท่ากับ " + obj.Action + "] ซึ่งไม่ตรงตามรูปแบบที่กำหนดไว้");

            obj.ModifiedBy = "BATCH";
            obj.ModifiedDt = DateTime.Now;
            obj.SyncFlag = "1";
            obj.Active = true;
            return obj;
        }

        #endregion
    }
}
