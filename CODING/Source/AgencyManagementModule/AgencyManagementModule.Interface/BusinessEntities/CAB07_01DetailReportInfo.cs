using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CAB07_01DetailReportInfo
    {
        #region "Local Variable"      
        private string _collectCount = null;
        private int? _printTime = 0;
        private string _mruId = null;
        private int? _caCount = 0;
        private decimal? _totalAmount = 0;
        private string _portionId = null;
        private string _agentId = null;
        private string _remark = null;
        private string _branchId = null;
        private string _branchName = null;
        #endregion

        #region "Property"

        [DataMember(Order=1)]
        public string CollectCount
        {
            get { return this._collectCount; }
            set { this._collectCount = value; }
        }


        [DataMember(Order=2)]
        public int? PrintTime
        {
            get { return _printTime; }
            set { this._printTime = value; }
        }
      

        [DataMember(Order=3)]
        public string MRUId
        {
            get { return _mruId; }
            set { this._mruId = value; }
        }

        [DataMember(Order=4)]
        public int? CaCount
        {
            get { return _caCount; }
            set { this._caCount = value; }
        }

        [DataMember(Order=5)]
        public decimal? TotalAmount
        {
            get { return _totalAmount; }
            set { this._totalAmount = value; }
        }

        [DataMember(Order=6)]
        public string PortionId
        {
            get { return _portionId; }
            set { this._portionId = value; }
        }

        [DataMember(Order=7)]
        public string AgentId
        {
            get { return _agentId; }
            set { this._agentId = value; }
        }

        [DataMember(Order=8)]
        public string Remark
        {
            get { return _remark; }
            set { this._remark = value; }
        }


        [DataMember(Order=9)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }


        [DataMember(Order=10)]
        public string BranchName
        {
            get { return this._branchName; }
            set { this._branchName = value; }
        }
        #endregion
    }
}
