using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class FunctionInfo
    {
        private string _FncId;
        private string _Module;
        private string _FncName;
        private string _SubFncName;
        private string _Description;
        private string _UnlockableFlag;
        private string _UnlockRemarkFlag;
        private int? _Internal;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string FncId
        {
            get { return _FncId; }
            set { _FncId = value; }
        }
        [DataMember(Order = 2)]
        public string Module
        {
            get { return _Module; }
            set { _Module = value; }
        }
        [DataMember(Order = 3)]
        public string FncName
        {
            get { return _FncName; }
            set { _FncName = value; }
        }
        [DataMember(Order = 4)]
        public string SubFncName
        {
            get { return _SubFncName; }
            set { _SubFncName = value; }
        }
        [DataMember(Order = 5)]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        [DataMember(Order = 6)]
        public string UnlockableFlag
        {
            get { return _UnlockableFlag; }
            set { _UnlockableFlag = value; }
        }
        [DataMember(Order = 7)]
        public string UnlockRemarkFlag
        {
            get { return _UnlockRemarkFlag; }
            set { _UnlockRemarkFlag = value; }
        }
        [DataMember(Order = 8)]
        public int? Internal
        {
            get { return _Internal; }
            set { _Internal = value; }
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

    }
}
