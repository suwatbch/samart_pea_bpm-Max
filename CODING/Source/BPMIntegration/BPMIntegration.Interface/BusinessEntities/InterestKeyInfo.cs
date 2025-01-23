using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class InterestKeyInfo
    {
        private string _InterestKey;
        private string _InterestName;
        private decimal? _InterestRate;
        private string _SyncFlag;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string InterestKey
        {
            get { return _InterestKey; }
            set { _InterestKey = value; }
        }
        [DataMember(Order = 2)]
        public string InterestName
        {
            get { return _InterestName; }
            set { _InterestName = value; }
        }
        [DataMember(Order = 3)]
        public decimal? InterestRate
        {
            get { return _InterestRate; }
            set { _InterestRate = value; }
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
