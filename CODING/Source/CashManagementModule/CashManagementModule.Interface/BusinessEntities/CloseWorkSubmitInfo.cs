using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.CashManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CloseWorkSubmitInfo
    {
        private string _workId;
        private string _postedBranchId;
        private string _cashierId;
        private string _closeWorkBy;
        private string _branchId;
        private string _posId;

        //auto fill
        private decimal? _cashNextWork;
        private decimal? _chqNextWork;


        [DataMember(Order=1)]
        public string WorkId
        {
            set { _workId = value; }
            get { return _workId; }
        }


        [DataMember(Order=2)]
        public decimal? CashNextWork
        {
            set { _cashNextWork = value; }
            get { return _cashNextWork; }
        }


        [DataMember(Order=3)]
        public decimal? ChqNextWork
        {
            set { _chqNextWork = value; }
            get { return _chqNextWork; }
        }


        [DataMember(Order=4)]
        public string PostedBranchId
        {
            set { _postedBranchId = value; }
            get { return _postedBranchId; }
        }


        [DataMember(Order=5)]
        public string CashierId
        {
            set { _cashierId = value; }
            get { return _cashierId; }
        }


        [DataMember(Order=6)]
        public string CloseWorkBy
        {
            set { _closeWorkBy = value; }
            get { return _closeWorkBy; }
        }


        [DataMember(Order=7)]
        public string BranchId
        {
            set { _branchId = value; }
            get { return _branchId; }
        }


        [DataMember(Order=8)]
        public string PosId
        {
            set { _posId = value; }
            get { return _posId; }
        }
    }
}
