using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class PaymentTypeInfo
    {
        private string _PtId;
        private string _PtName;
        private string _PaymentSeq;
        private string _SyncFlag;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string PtId
        {
            get { return _PtId; }
            set { _PtId = value; }
        }
        [DataMember(Order = 2)]
        public string PtName
        {
            get { return _PtName; }
            set { _PtName = value; }
        }
        [DataMember(Order = 3)]
        public string PaymentSeq
        {
            get { return _PaymentSeq; }
            set { _PaymentSeq = value; }
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
