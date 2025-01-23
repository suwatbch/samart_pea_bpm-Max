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
    public class AccountClassInfo : IListUtility<AccountClassInfo>
    {
        private string _AccountClassId;
        private string _AccountClassName;
        private string _SyncFlag;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string AccountClassId
        {
            get { return _AccountClassId; }
            set { _AccountClassId = value; }
        }
        [DataMember(Order = 2)]
        public string AccountClassName
        {
            get { return _AccountClassName; }
            set { _AccountClassName = value; }
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


        #region IListUtility<AccountClassInfo> Members

        public string ToStream()
        {
            //string[] template = { AccountClassId, AccountClassName, Action };
            //return string.Join("\t", template);
            throw new Exception("The method is not implement yet");
        }

        public AccountClassInfo ParseExtract(string txt)
        {
            AccountClassInfo it = new AccountClassInfo();
            string[] sp = txt.Split('|');

            int colFixed = InterfaceColumns.AccountClass;
            int colCounted = sp.Length - 1;
            if (colCounted != colFixed)
                throw new Exception("�������ö����¡���� ���ͧ�ҡ Columns � text file �ըӹǹ " + colCounted.ToString() + " �� Columns ����˹�����ըӹǹ " + colFixed.ToString());


            int i = 1;
            it.AccountClassId = StringConvert.ToString(sp[i++].Trim());
            it.AccountClassName = StringConvert.ToString(sp[i++].Trim());
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
