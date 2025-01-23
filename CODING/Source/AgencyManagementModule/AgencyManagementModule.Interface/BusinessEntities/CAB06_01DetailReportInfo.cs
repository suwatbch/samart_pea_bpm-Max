using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CAB06_01DetailReportInfo : IComparable<CAB06_01DetailReportInfo>
    {
        #region "Local Variable"       
        private string _area;
        private string _areaName;
        private string _peaCode;
        private string _peaName;
        private int? _agentPersonType = 0;
        private int? _agentCompanyType = 0;
        private int? _totalAgent = 0;
        private int? _keepMore90Percent = 0;      
        private int? _billOut = 0;
        private decimal? _amountBillOut =  0;
        private int? _canKeepBill = 0;       
        private decimal? _totalMoney = 0;        
        private decimal? _baseCommission = 0;
        private decimal? _spacialCommission = 0;
        private decimal? _sendInvoice = 0;
        private decimal? _extraMoney = 0;
        private decimal? _allTotalPaid = 0;        
        private int? _allCanKeep = 0;
        private string _firstBranch;
        private string _lastBranch;

        private string _branchLevel;
        private string _branchGroup;
        
        #endregion

        #region "Property"   


        [DataMember(Order=1)]
        public string AreaName
        {
            get { return this._areaName; }
            set { this._areaName = value; }
        }


        [DataMember(Order=2)]
        public string Area
        {
            get { return this._area; }
            set { this._area = value; }
        }


        [DataMember(Order=3)]
        public string PeaCode
        {
            get { return _peaCode; }
            set { this._peaCode = value; }
        }


        [DataMember(Order=4)]
        public string PeaName
        {
            get { return _peaName; }
            set { this._peaName = value; }
        }


        [DataMember(Order=5)]
        public int? AgentPersonType
        {
            get { return _agentPersonType; }
            set { this._agentPersonType = value; }
        }


        [DataMember(Order=6)]
        public int? AgentCompanyType
        {
            get { return _agentCompanyType; }
            set { this._agentCompanyType = value; }
        }


        [DataMember(Order=7)]
        public int? TotalAgent
        {
            get { return _totalAgent; }
            set { this._totalAgent = value; }
        }


        [DataMember(Order=8)]
        public int? KeepMore90Percent
        {
            get { return _keepMore90Percent; }
            set { this._keepMore90Percent = value; }
        }
       

        [DataMember(Order=9)]
        public int? BillOut
        {
            get { return _billOut; }
            set { this._billOut = value; }
        }
        

        [DataMember(Order=10)]
        public decimal? AmountBillOut
        {
            get { return _amountBillOut; }
            set { this._amountBillOut = value; }
        }


        [DataMember(Order=11)]
        public int? CanKeepBill
        {
            get { return _canKeepBill; }
            set { this._canKeepBill = value; }
        }
       

        [DataMember(Order=12)]
        public decimal? TotalMoney
        {
            get { return _totalMoney; }
            set { this._totalMoney = value; }
        }
       

        [DataMember(Order=13)]
        public decimal? BaseCommission
        {
            get { return _baseCommission; }
            set { this._baseCommission = value; }
        }


        [DataMember(Order=14)]
        public decimal? SpacialCommission
        {
            get { return _spacialCommission; }
            set { this._spacialCommission = value; }
        }


        [DataMember(Order=15)]
        public decimal? SendInvoice
        {
            get { return _sendInvoice; }
            set { this._sendInvoice = value; }
        }


        [DataMember(Order=16)]
        public decimal? ExtraMoney
        {
            get { return _extraMoney; }
            set { this._extraMoney = value; }
        }


        [DataMember(Order=17)]
        public decimal? AllTotalPaid
        {
            get { return _allTotalPaid; }
            set { this._allTotalPaid = value; }
        }


        [DataMember(Order=18)]
        public int? AllCanKeep
        {
            get { return _allCanKeep; }
            set { this._allCanKeep = value; }
        }


        [DataMember(Order=19)]
        public string FirstBranch
        {
            get { return _firstBranch; }
            set { this._firstBranch = value; }
        }


        [DataMember(Order=20)]
        public string LastBranch
        {
            get { return _lastBranch; }
            set { this._lastBranch = value; }
        }


        [DataMember(Order=21)]
        public string BranchLevel
        {
            get { return this._branchLevel; }
            set { this._branchLevel = value; }
        }


        [DataMember(Order=22)]
        public string BranchGroup
        {
            get { return this._branchGroup; }
            set { this._branchGroup = value; }
        }

        #endregion


        #region IComparable<EvaluateAgentInfoReport> Members

        public int CompareTo(CAB06_01DetailReportInfo reportItem)
        {
            return PeaCode.CompareTo(reportItem.PeaCode);
        }

        #endregion
    }

}
