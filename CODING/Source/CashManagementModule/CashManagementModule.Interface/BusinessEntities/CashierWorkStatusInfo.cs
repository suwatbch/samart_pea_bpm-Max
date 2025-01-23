using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.CashManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CashierWorkStatusInfo
    {
        string _workId;


        [DataMember(Order=1)]
        public string WorkId
        {
            get { return _workId; }
            set { _workId = value; }
        }
        string _cashierId;


        [DataMember(Order=2)]
        public string CashierId
        {
            get { return _cashierId; }
            set { _cashierId = value; }
        }
        string _posId;


        [DataMember(Order=3)]
        public string PosId
        {
            get { return _posId; }
            set { _posId = value; }
        }
        string _status;


        [DataMember(Order=4)]
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
        DateTime? _openWorkDt;


        [DataMember(Order=5)]
        public DateTime? OpenWorkDt
        {
            get { return _openWorkDt; }
            set { _openWorkDt = value; }
        }
        DateTime? _closeWorkDt;


        [DataMember(Order=6)]
        public DateTime? CloseWorkDt
        {
            get { return _closeWorkDt; }
            set { _closeWorkDt = value; }
        }
        string _branchId;


        [DataMember(Order=7)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }
        string _syncFlag;


        [DataMember(Order=8)]
        public string SyncFlag
        {
            get { return _syncFlag; }
            set { _syncFlag = value; }
        }
        DateTime? _modifiedDt;


        [DataMember(Order=9)]
        public DateTime? ModifiedDt
        {
            get { return _modifiedDt; }
            set { _modifiedDt = value; }
        }
        DateTime? _modifiedBy;


        [DataMember(Order=10)]
        public DateTime? ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }
        string _active;


        [DataMember(Order=11)]
        public string Active
        {
            get { return _active; }
            set { _active = value; }
        }

        int? _dayCount;

        [DataMember(Order=12)]
        public int? DayCount
        {
            get { return _dayCount; }
            set { _dayCount = value; }
        }

        bool _isStartOpenBalance;

        [DataMember(Order=13)]
        public bool IsStartOpenBalance
        {
            get { return _isStartOpenBalance; }
            set { _isStartOpenBalance = value; }
        }

    }
}
