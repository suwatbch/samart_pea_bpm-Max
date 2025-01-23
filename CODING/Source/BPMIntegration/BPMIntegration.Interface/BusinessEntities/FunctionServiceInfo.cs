using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class FunctionServiceInfo
    {
        private string _RTId;
        private string _FncId;
        private string _SvcId;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string RTId
        {
            get { return _RTId; }
            set { _RTId = value; }
        }
        [DataMember(Order = 2)]
        public string FncId
        {
            get { return _FncId; }
            set { _FncId = value; }
        }
        [DataMember(Order = 3)]
        public string SvcId
        {
            get { return _SvcId; }
            set { _SvcId = value; }
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
