using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using PEA.BPM.Integration.BPMIntegration.Interface.Constants;
using PEA.BPM.Architecture.CommonUtilities;
using System.Globalization;

using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class DisconnectionDocInfo : IListUtility<DisconnectionDocInfo>
    {
        private string _DiscNo;
        private DateTime? _CreatedDt;
        private string _DiscStatusId;
        private DateTime? _ReleaseDt;
        private string _WorkOrderNo;
        private DateTime? _DiscPlanStart;
        private DateTime? _DiscPlanEnd;
        private string _WorkCenter;
        private DateTime? _PostpConfirm;
        private DateTime? _FuseRemConfirm;
        private DateTime? _MeterRemConfirm;
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
        public DateTime? CreatedDt
        {
            get { return _CreatedDt; }
            set { _CreatedDt = value; }
        }
        [DataMember(Order = 3)]
        public string DiscStatusId
        {
            get { return _DiscStatusId; }
            set { _DiscStatusId = value; }
        }
        [DataMember(Order = 4)]
        public DateTime? ReleaseDt
        {
            get { return _ReleaseDt; }
            set { _ReleaseDt = value; }
        }
        [DataMember(Order = 5)]
        public string WorkOrderNo
        {
            get { return _WorkOrderNo; }
            set { _WorkOrderNo = value; }
        }
        [DataMember(Order = 6)]
        public DateTime? DiscPlanStart
        {
            get { return _DiscPlanStart; }
            set { _DiscPlanStart = value; }
        }
        [DataMember(Order = 7)]
        public DateTime? DiscPlanEnd
        {
            get { return _DiscPlanEnd; }
            set { _DiscPlanEnd = value; }
        }
        [DataMember(Order = 8)]
        public string WorkCenter
        {
            get { return _WorkCenter; }
            set { _WorkCenter = value; }
        }
        [DataMember(Order = 9)]
        public DateTime? PostpConfirm
        {
            get { return _PostpConfirm; }
            set { _PostpConfirm = value; }
        }
        [DataMember(Order = 10)]
        public DateTime? FuseRemConfirm
        {
            get { return _FuseRemConfirm; }
            set { _FuseRemConfirm = value; }
        }
        [DataMember(Order = 11)]
        public DateTime? MeterRemConfirm
        {
            get { return _MeterRemConfirm; }
            set { _MeterRemConfirm = value; }
        }
        [DataMember(Order = 12)]
        public string TechBranchId
        {
            get { return _TechBranchId; }
            set { _TechBranchId = value; }
        }
        [DataMember(Order = 13)]
        public string CommBranchId
        {
            get { return _CommBranchId; }
            set { _CommBranchId = value; }
        }
        [DataMember(Order = 14)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 15)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 16)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }

        [DataMember(Order = 17)]
        public string Action
        {
            get { return _Action; }
            set { _Action = value; }
        }
        [DataMember(Order = 18)]
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
        //DateTime? _createdDt;

        //public DateTime? CreatedDt
        //{
        //    get { return _createdDt; }
        //    set { _createdDt = value; }
        //}
        //string _discStatusId;

        //public string DiscStatusId
        //{
        //    get { return _discStatusId; }
        //    set { _discStatusId = value; }
        //}
        //DateTime? _releaseDt;

        //public DateTime? ReleaseDt
        //{
        //    get { return _releaseDt; }
        //    set { _releaseDt = value; }
        //}
        //string _workOrderNo;

        //public string WorkOrderNo
        //{
        //    get { return _workOrderNo; }
        //    set { _workOrderNo = value; }
        //}
        //string _workCenter;

        //public string WorkCenter
        //{
        //    get { return _workCenter; }
        //    set { _workCenter = value; }
        //}

        //DateTime? _discPlanStart;

        //public DateTime? DiscPlanStart
        //{
        //    get { return _discPlanStart; }
        //    set { _discPlanStart = value; }
        //}
        //DateTime? _discPlanEnd;

        //public DateTime? DiscPlanEnd
        //{
        //    get { return _discPlanEnd; }
        //    set { _discPlanEnd = value; }
        //}
        //DateTime? _postpConfirm;

        //public DateTime? PostpConfirm
        //{
        //    get { return _postpConfirm; }
        //    set { _postpConfirm = value; }
        //}
  
        //DateTime? _fuseRemConfirm;

        //public DateTime? FuseRemConfirm
        //{
        //    get { return _fuseRemConfirm; }
        //    set { _fuseRemConfirm = value; }
        //}
   
        //DateTime? _meterRemConfirm;

        //public DateTime? MeterRemConfirm
        //{
        //    get { return _meterRemConfirm; }
        //    set { _meterRemConfirm = value; }
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


        #region IListUtility<DisconnectionDocInfo> Members

        public string ToStream()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public DisconnectionDocInfo ParseExtract(string txt)
        {
            IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("en-EN");
            DisconnectionDocInfo obj = new DisconnectionDocInfo();
            string[] sp = txt.Split('|');

            int colFixed = InterfaceColumns.DisconnectionDoc;
            int colCounted = sp.Length - 1;
            if (colCounted != colFixed)
                throw new Exception("ไม่สามารถทำรายการได้ เนื่องจาก Columns ใน text file มีจำนวน " + colCounted.ToString() + " แต่ Columns ที่กำหนดไว้มีจำนวน " + colFixed.ToString());


            int i = 1;
            obj.DiscNo = StringConvert.ToString(sp[i++].Trim());
            obj.CreatedDt = StringConvert.ToDateTime(sp[i++].Trim());
            obj.DiscStatusId = StringConvert.ToString(sp[i++].Trim());
            obj.ReleaseDt = StringConvert.ToDateTime(sp[i++].Trim());
            obj.WorkOrderNo = StringConvert.ToString(sp[i++].Trim());
            obj.DiscPlanStart = StringConvert.ToDateTime(sp[i++].Trim());
            obj.DiscPlanEnd = StringConvert.ToDateTime(sp[i++].Trim());
            obj.WorkCenter = StringConvert.ToString(sp[i++].Trim());
            obj.PostpConfirm = StringConvert.ToDateTime(sp[i++].Trim());
            obj.FuseRemConfirm = StringConvert.ToDateTime(sp[i++].Trim());
            obj.MeterRemConfirm = StringConvert.ToDateTime(sp[i++].Trim());
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
