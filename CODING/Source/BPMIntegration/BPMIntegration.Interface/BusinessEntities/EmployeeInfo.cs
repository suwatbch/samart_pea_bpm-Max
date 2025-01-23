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
    public class EmployeeInfo : IListUtility<EmployeeInfo>
    {
        private string _EmployeeId;
        private string _FirstName;
        private string _LastName;
        private string _Department;
        private string _BranchId;
        private string _SyncFlag;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string EmployeeId
        {
            get { return _EmployeeId; }
            set { _EmployeeId = value; }
        }
        [DataMember(Order = 2)]
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        [DataMember(Order = 3)]
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        [DataMember(Order = 4)]
        public string Department
        {
            get { return _Department; }
            set { _Department = value; }
        }
        [DataMember(Order = 5)]
        public string BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }
        [DataMember(Order = 6)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 7)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 8)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 9)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 10)]
        public string Action
        {
            get { return this._action; }
            set { this._action = value; }
        }

        #region IListUtility<EmployeeInfo> Members

        public string ToStream()
        {
            //string[] template = { EmployeeId, FirstName, LastName, Department, BranchId, BkId, Action };
            //return string.Join("\t", template);
            throw new Exception("The Method is not implement yet");
        }

        public EmployeeInfo ParseExtract(string txt)
        {
            EmployeeInfo it = new EmployeeInfo();
            string[] sp = txt.Split('|');

            int colFixed = InterfaceColumns.Employee;
            int colCounted = sp.Length - 1;
            if (colCounted != colFixed)
                throw new Exception("ไม่สามารถทำรายการได้ เนื่องจาก Columns ใน text file มีจำนวน " + colCounted.ToString() + " แต่ Columns ที่กำหนดไว้มีจำนวน " + colFixed.ToString());


            int i = 1;
            it.EmployeeId = StringConvert.ToString(sp[i++].Trim());
            it.FirstName = StringConvert.ToString(sp[i++].Trim());
            it.LastName = StringConvert.ToString(sp[i++].Trim());
            it.Department = StringConvert.ToString(sp[i++].Trim());
            it.BranchId = StringConvert.ToString(sp[i++].Trim());//RSG
            //it.BkId = sp[i++].Trim();
            it.Action = sp[i++].Trim();

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
