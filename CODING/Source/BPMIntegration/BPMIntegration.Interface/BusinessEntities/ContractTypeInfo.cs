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
    public class ContractTypeInfo : IListUtility<ContractTypeInfo>
    {
        private string _CtId;
        private string _CtName;
        private string _SyncFlag;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string CtId
        {
            get { return _CtId; }
            set { _CtId = value; }
        }
        [DataMember(Order = 2)]
        public string CtName
        {
            get { return _CtName; }
            set { _CtName = value; }
        }
        [DataMember(Order = 3)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 4)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 5)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 6)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }

        [DataMember(Order = 7)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }


        #region IListUtility<ContractTypeInfo> Members
        
        public string ToStream()
        {
            string[] template = { CtId, CtName, Action };
            return string.Join("\t", template);
        }

        public ContractTypeInfo ParseExtract(string txt)
        {
            ContractTypeInfo it = new ContractTypeInfo();
            string[] sp = txt.Split('|');

            int colFixed = InterfaceColumns.ContractType;
            int colCounted = sp.Length - 1;
            if (colCounted != colFixed)
                throw new Exception("ไม่สามารถทำรายการได้ เนื่องจาก Columns ใน text file มีจำนวน " + colCounted.ToString() + " แต่ Columns ที่กำหนดไว้มีจำนวน " + colFixed.ToString());


            int i = 1;
            it.CtId = StringConvert.ToString(sp[i++].Trim());
            it.CtName = StringConvert.ToString(sp[i++].Trim());
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
