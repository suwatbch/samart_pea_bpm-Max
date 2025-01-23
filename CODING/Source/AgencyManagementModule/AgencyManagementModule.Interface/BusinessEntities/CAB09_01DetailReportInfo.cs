using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CAB09_01DetailReportInfo
    {
        private int? _seqNo = 0;
        private string _branchId = null;
        private string _branchName = null;
        private string _portionId = null;
        private string _bookCreateDay = null;
        private List<string> _mruList = new List<string>();
        private List<string> _agencyIdList = new List<string>();
		private int? _actCaCount = 0;
		private decimal? _actTotalAmount = 0;
		private int? _planCaCount = 0;
		private decimal? _planTotalAmount = 0;
        private int? _agCount= 0;


        [DataMember(Order=1)]
        public int? SeqNo
        {
            get { return this._seqNo; }
            set { this._seqNo = value; }
        }


        [DataMember(Order=2)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }


        [DataMember(Order=3)]
        public string BranchName
        {
            get { return this._branchName; }
            set { this._branchName = value; }
        }
     

        [DataMember(Order=4)]
        public string PortionId
        {
            get { return this._portionId; }
            set { this._portionId = value; }
        }

        [DataMember(Order=5)]
        public string BookCreateDay
        {
            get { return this._bookCreateDay; }
            set { this._bookCreateDay = value; }
        }       


        [DataMember(Order=6)]
        public List<string> MruList
        {
            get { return _mruList; }
            set { _mruList = value; }
        }

        [DataMember(Order=7)]
        public List<string> AgencyIdList
        {
            get { return _agencyIdList; }
            set { _agencyIdList = value; }
        }

        //Checked TongKung
        public string MruLine
        {
            get 
            {
                if (MruList == null || MruList.Count == 0)
                {
                    return String.Empty;
                } if (MruList.Count == 1)
                {
                    return MruList[0];
                }
                else
                {
                    return StringConvert.FormatMru(MruList);
                }
            }
        }

        //Checked TongKung
        public int? MruCount
        {
            get
            { 
                if ((MruList == null ) || (MruList.Count == 0))
                {
                    return 0;
                }
                else
                {
                    return MruList.Count;
                }
            }
        }

        [DataMember(Order=9)]
        public int? ActCaCount
        {
            get { return this._actCaCount; }
            set { this._actCaCount = value; }
        }


        [DataMember(Order=10)]
        public decimal? ActTotalAmount
        {
            get { return this._actTotalAmount; }
            set { this._actTotalAmount = value; }
        }

        [DataMember(Order=11)]
        public int? PlanCaCount
        {
            get { return this._planCaCount; }
            set { this._planCaCount = value; }
        }

        [DataMember(Order=12)]
        public decimal? PlanTotalAmount
        {
            get { return this._planTotalAmount; }
            set { this._planTotalAmount = value; }
        }

        //Checked TongKung
        public int? AgCount
        {
            get
            {               
                if ((AgencyIdList == null) || (AgencyIdList.Count == 0))
                {
                    return 0;
                }
                else
                {
                    return AgencyIdList.Count;
                }
            }
        }


        /*
        #region "Local Variable"
        private int? _seqNo = 0;
        private string _keepPeriodNo;
        private string _areaCode;
        private string _paidDate;
        private string _rangeOfLine;
        private int? _lineOfQuanlity = 0;
        private decimal? _customerOfQuanlity = 0;
        private decimal? _percentOfCustomer = 0;
        private decimal? _totalAmount = 0;
        private decimal? _percentOfAmount = 0;
        private int? _quanlityOfAgency = 0;
        private string _prefix;
        private string _branchId;
        private string _branchName;
        private decimal? _percentOfKeepTimeNo = 0;
        private decimal? _percentOfBranch = 0;
        private decimal? _percentMoneyOfKeepTimeNo = 0;
        private decimal? _percentMoneyOfBranch = 0;
        #endregion

        #region "Property"

        [DataMember(Order=15)]
        public int? SeqNo
        {
            get { return _seqNo; }
            set { this._seqNo = value; }
        }

        [DataMember(Order=16)]
        public string KeepPeriodNo
        {
            get { return _keepPeriodNo; }
            set { this._keepPeriodNo = value; }
        }

        [DataMember(Order=17)]
        public string AreaCode
        {
            get { return _areaCode; }
            set { this._areaCode = value; }
        }

        [DataMember(Order=18)]
        public string PaidDate
        {
            get { return _paidDate; }
            set { this._paidDate = value; }
        }

        [DataMember(Order=19)]
        public string RangeOfLine
        {
            get { return _rangeOfLine; }
            set { this._rangeOfLine = value; }
        }

        [DataMember(Order=20)]
        public int? LineOfQuanlity
        {
            get { return _lineOfQuanlity; }
            set { this._lineOfQuanlity = value; }
        }

        [DataMember(Order=21)]
        public decimal? CustomerOfQuanlity
        {
            get { return _customerOfQuanlity; }
            set { this._customerOfQuanlity = value; }
        }

        [DataMember(Order=22)]
        public decimal? PercentOfCustomer
        {
            get { return _percentOfCustomer; }
            set { this._percentOfCustomer = value; }
        }

        [DataMember(Order=23)]
        public decimal? TotalAmount
        {
            get { return _totalAmount; }
            set { this._totalAmount = value; }
        }

        [DataMember(Order=24)]
        public decimal? PercentOfAmount
        {
            get { return _percentOfAmount; }
            set { this._percentOfAmount = value; }
        }

        [DataMember(Order=25)]
        public int? QuanlityOfAgency
        {
            get { return _quanlityOfAgency; }
            set { this._quanlityOfAgency = value; }
        }

        [DataMember(Order=26)]
        public string Prefix
        {
            get { return _prefix; }
            set { this._prefix = value; }
        }

        [DataMember(Order=27)]
        public string BranchId
        {
            get { return _branchId; }
            set { this._branchId = value; }
        }

        [DataMember(Order=28)]
        public string BranchName
        {
            get { return _branchName; }
            set { this._branchName = value; }
        }

        [DataMember(Order=29)]
        public decimal? PercentOfKeepTimeNo
        {
            get { return _percentOfKeepTimeNo; }
            set { this._percentOfKeepTimeNo = value; }
        }

        [DataMember(Order=30)]
        public decimal? PercentOfBranch
        {
            get { return _percentOfBranch; }
            set { this._percentOfBranch = value; }
        }

        [DataMember(Order=31)]
        public decimal? PercentMoneyOfKeepTimeNo
        {
            get { return _percentMoneyOfKeepTimeNo; }
            set { this._percentMoneyOfKeepTimeNo = value; }
        }

        [DataMember(Order=32)]
        public decimal? PercentMoneyOfBranch
        {
            get { return _percentMoneyOfBranch; }
            set { this._percentMoneyOfBranch = value; }
        }
        #endregion
       */
    }
}
