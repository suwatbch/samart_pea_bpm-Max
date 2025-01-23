using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using System.Globalization;
using PEA.BPM.Integration.BPMIntegration.Interface.Constants;
using PEA.BPM.Architecture.CommonUtilities;

using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class RTDisconnectionDocCaDocInfo : IListUtility<RTDisconnectionDocCaDocInfo>
    {
        private string _DiscNo;
        private string _CaDocNo;
        private string _CancelFlag;
        private string _TechBranchId;
        private string _CommBranchId;
        private string _ModifiedBy;
        private DateTime? _ModifiedDt;
        private bool _Active;
        private string _Action;
        private string _SyncFlag;

        [DataMember(Order = 1)]
        public string DiscNo
        {
            get { return _DiscNo; }
            set { _DiscNo = value; }
        }
        [DataMember(Order = 2)]
        public string CaDocNo
        {
            get { return _CaDocNo; }
            set { _CaDocNo = value; }
        }
        [DataMember(Order = 3)]
        public string CancelFlag
        {
            get { return _CancelFlag; }
            set { _CancelFlag = value; }
        }
        [DataMember(Order = 4)]
        public string TechBranchId
        {
            get { return _TechBranchId; }
            set { _TechBranchId = value; }
        }
        [DataMember(Order = 5)]
        public string CommBranchId
        {
            get { return _CommBranchId; }
            set { _CommBranchId = value; }
        }
        [DataMember(Order = 6)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 7)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 8)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }

        [DataMember(Order = 9)]
        public string Action
        {
            get { return _Action; }
            set { _Action = value; }
        }
        [DataMember(Order = 10)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }



        //string _discNo;

        //public string DiscNo
        //{
        //    get { return _discNo; }
        //    set { _discNo = value; }
        //}
        //string _caDocNo;

        //public string CaDocNo
        //{
        //    get { return _caDocNo; }
        //    set { _caDocNo = value; }
        //}
        //string _cancelFlag;

        //public string CancelFlag
        //{
        //    get { return _cancelFlag; }
        //    set { _cancelFlag = value; }
        //}        
        //string _action;

        //public string Action
        //{
        //    get { return _action; }
        //    set { _action = value; }
        //}
        //DateTime? _modifiedDt;

        //public DateTime? ModifiedDt
        //{
        //    get { return _modifiedDt; }
        //    set { _modifiedDt = value; }
        //}
        //string _modifiedBy;

        //public string ModifiedBy
        //{
        //    get { return _modifiedBy; }
        //    set { _modifiedBy = value; }
        //}
        //bool _active;

        //public bool Active
        //{
        //    get { return _active; }
        //    set { _active = value; }
        //}
        //string _syncFlag;

        //public string SyncFlag
        //{
        //    get { return _syncFlag; }
        //    set { _syncFlag = value; }
        //}

        #region IListUtility<RTDisconnectionDocCaDocInfo> Members

        public string ToStream()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public RTDisconnectionDocCaDocInfo ParseExtract(string txt)
        {
            IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("en-EN");
            RTDisconnectionDocCaDocInfo obj = new RTDisconnectionDocCaDocInfo();
            string[] sp = txt.Split('|');

            int colFixed = InterfaceColumns.RTDisconnectionDocCaDoc;
            int colCounted = sp.Length - 1;
            if (colCounted != colFixed)
                throw new Exception("ไม่สามารถทำรายการได้ เนื่องจาก Columns ใน text file มีจำนวน " + colCounted.ToString() + " แต่ Columns ที่กำหนดไว้มีจำนวน " + colFixed.ToString());


            int i = 1;
            obj.DiscNo = StringConvert.ToString(sp[i++].Trim());
            obj.CaDocNo = StringConvert.ToString(sp[i++].Trim());
            obj.CancelFlag = StringConvert.ToString(sp[i++].Trim());
            obj.Action = sp[i++].Trim();

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
