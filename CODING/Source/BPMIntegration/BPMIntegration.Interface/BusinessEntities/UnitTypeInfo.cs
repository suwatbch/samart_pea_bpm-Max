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
    public class UnitTypeInfo : IListUtility<UnitTypeInfo>
    {
        private string _UnitTypeId;
        private string _UnitTypeName;
        private string _SyncFlag;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string UnitTypeId
        {
            get { return _UnitTypeId; }
            set { _UnitTypeId = value; }
        }
        [DataMember(Order = 2)]
        public string UnitTypeName
        {
            get { return _UnitTypeName; }
            set { _UnitTypeName = value; }
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

        #region IListUtility<UnitTypeInfo> Members

        public string ToStream()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public UnitTypeInfo ParseExtract(string txt)
        {
            UnitTypeInfo it = new UnitTypeInfo();
            string[] sp = txt.Split('|');

            int colFixed = InterfaceColumns.UnitType;
            int colCounted = sp.Length - 1;
            if (colCounted != colFixed)
                throw new Exception("�������ö����¡���� ���ͧ�ҡ Columns � text file �ըӹǹ " + colCounted.ToString() + " �� Columns ����˹�����ըӹǹ " + colFixed.ToString());

            int i = 1;
            it.UnitTypeId = StringConvert.ToString(sp[i++].Trim());
            it.UnitTypeName = StringConvert.ToString(sp[i++].Trim());
            it.Action = sp[i++].Trim();

            if (it.Action != "O" && it.Action != "1" && it.Action != "2" && it.Action != "3")
                throw new Exception("�������ö����¡���� ���ͧ�ҡ�����Ţͧ [Action] �դ����ҡѺ " + it.Action + "] ������ç����ٻẺ����˹����");

            it.SyncFlag = "1";
            it.ModifiedBy = "BATCH";
            it.ModifiedDt = DateTime.Now;
            it.Active = true;
            return it;
        }

        #endregion
    }
}
