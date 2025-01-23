using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CAB06_01HeaderReportInfo
    {
        #region "Local Variable"
        private string _branchName;
        private string _printDate;
        private string _startBranchId;
        private string _startBranchName;
        private string _endBranchId;
        private string _endBranchName;
        private string _periodStartMonth;
        private string _periodEndtMonth;        
        private string _periodYear;
        private int? _totalBillCollected;
        private decimal? _totalCommission;
        private bool _printPreview;         
        #endregion
        #region "Property"


        [DataMember(Order=1)]
        public string BranchName
        {
            get { return _branchName; }
            set { _branchName = value; }
        }


        [DataMember(Order=2)]
        public string PrintDate
        {
            get { return this._printDate; }
            set { this._printDate = value; }
        }

        [DataMember(Order=3)]
        public string StartBranchId
        {
            get { return _startBranchId; }
            set { this._startBranchId = value; }
        }

        [DataMember(Order=4)]
        public string StartBranchName
        {
            get { return _startBranchName; }
            set { this._startBranchName = value; }
        }

        [DataMember(Order=5)]
        public string EndBranchId
        {
            get { return _endBranchId; }
            set { this._endBranchId = value; }
        }

        [DataMember(Order=6)]
        public string EndBranchName
        {
            get { return _endBranchName; }
            set { this._endBranchName = value; }
        }

        [DataMember(Order=7)]
        public string PeriodStartMonth
        {
            get { return _periodStartMonth; }
            set { this._periodStartMonth = value; }
        }

        [DataMember(Order=8)]
        public string PeriodEndMonth
        {
            get { return _periodEndtMonth; }
            set { this._periodEndtMonth = value; }
        }

        [DataMember(Order=9)]
        public string PeriodYear
        {
            get { return _periodYear; }
            set { this._periodYear = value; }
        }


        [DataMember(Order=10)]
        public int? TotalBillCollected
        {
            get { return _totalBillCollected; }
            set { this._totalBillCollected = value; }
        }

        [DataMember(Order=11)]
        public decimal? TotalCommission
        {
            get { return this._totalCommission; }
            set { this._totalCommission = value; }
        }

        [DataMember(Order=12)]
        public bool PrintPreview
        {
            get { return this._printPreview; }
            set { this._printPreview = value; }
        }
        #endregion
    }
}
