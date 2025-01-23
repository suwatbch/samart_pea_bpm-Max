using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class UnlockRemarkInfo
    {
        private string _FncId;
        private string _RemarkId;
        private string _Description;
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
        public string RemarkId
        {
            get { return _RemarkId; }
            set { _RemarkId = value; }
        }
        [DataMember(Order = 3)]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
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
    }
}
