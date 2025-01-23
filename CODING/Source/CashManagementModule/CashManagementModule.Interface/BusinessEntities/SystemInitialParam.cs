using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.CashManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class SystemInitialParam
    {
        decimal? _amount;


        [DataMember(Order=1)]
        public decimal? Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        string _flowType;


        [DataMember(Order=2)]
        public string FlowType
        {
            get { return _flowType; }
            set { _flowType = value; }
        }

        string _cashierId;


        [DataMember(Order=3)]
        public string CashierId
        {
            get { return _cashierId; }
            set { _cashierId = value; }
        }

        string _branchId;


        [DataMember(Order=4)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }

        string _posId;


        [DataMember(Order=5)]
        public string PosId
        {
            get { return _posId; }
            set { _posId = value; }
        }

        string _workId;


        [DataMember(Order=6)]
        public string WorkId
        {
            get { return _workId; }
            set { _workId = value; }
        }

        string _status;


        [DataMember(Order=7)]
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        string _modifiedBy;


        [DataMember(Order=8)]
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }
    }
}
