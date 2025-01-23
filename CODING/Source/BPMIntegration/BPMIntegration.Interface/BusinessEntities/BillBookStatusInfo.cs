using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class BillBookStatusInfo
    {
        private string _BsId;
        private string _BsName;
        private string _BsDesc;
        private string _SyncFlag;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string BsId
        {
            get { return _BsId; }
            set { _BsId = value; }
        }
        [DataMember(Order = 2)]
        public string BsName
        {
            get { return _BsName; }
            set { _BsName = value; }
        }
        [DataMember(Order = 3)]
        public string BsDesc
        {
            get { return _BsDesc; }
            set { _BsDesc = value; }
        }
        [DataMember(Order = 4)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 5)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 6)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 7)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 8)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }
    }
}
