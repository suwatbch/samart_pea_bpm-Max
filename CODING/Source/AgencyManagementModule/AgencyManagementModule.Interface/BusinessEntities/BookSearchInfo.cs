using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class BookSearchInfo
    {
        private string _branchId;
        private string _billPeriod;
        private DateTime _createDt;
        private string _agentId;
        private decimal? _totalCommission = 0;
        private decimal? _totalFine = 0;
        private bool _reprint = false;
        private bool _allowCalculate = false;
        private bool _penaltyWaiveFlag = true;
        private bool _isCalculated = false;


        [DataMember(Order=1)]
        public string BranchId
        {
            set { _branchId = value; }
            get { return _branchId; }
        }


        [DataMember(Order=2)]
        public bool AllowCalculate
        {
            set { _allowCalculate = value; }
            get { return _allowCalculate; }
        }


        [DataMember(Order=3)]
        public bool IsReprint
        {
            set { _reprint = value; }
            get { return _reprint; }
        }


        [DataMember(Order=4)]
        public string BillPeriod
        {
            set { _billPeriod = value; }
            get {
                if (_billPeriod.Length == 7)
                {
                    return string.Format("{0}{1}", _billPeriod.Substring(3, 4), _billPeriod.Substring(0, 2));
                }
                else 
                {
                    return _billPeriod;
                }
            }
        }


        [DataMember(Order=5)]
        public string AgentId
        {
            set { _agentId = value; }
            get { return _agentId; }
        }


        [DataMember(Order=6)]
        public DateTime CreateDate
        {
            set { _createDt = value; }
            get { return _createDt; }
        }


        [DataMember(Order=7)]
        public decimal? TotalCommission
        {
            set { _totalCommission = value; }
            get { return _totalCommission; }
        }


        [DataMember(Order=8)]
        public decimal? TotalFine
        {
            set { _totalFine = value; }
            get { return _totalFine; }
        }


        [DataMember(Order=9)]
        public bool PenaltyWaiveFlag
        {
            set { _penaltyWaiveFlag = value; }
            get { return _penaltyWaiveFlag; }
        }


        [DataMember(Order=10)]
        public bool IsCalculated
        {
            set { _isCalculated = value; }
            get { return _isCalculated; }
        }
    }
}
