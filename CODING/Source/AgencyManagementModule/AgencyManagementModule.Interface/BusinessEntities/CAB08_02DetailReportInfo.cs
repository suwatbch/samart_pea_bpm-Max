using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CAB08_02DetailReportInfo
    {
        private int _rowNo = 0;
        private string _branchId = null;
        private string _branchName = null;
        private string _portionId = null;
        private string _agencyId = null;
        private decimal? _securityDeposit;
        private int? _caCount;
        private string _bookCreateDay = null;
        private List<string> _mruList = new List<string>();       
        private int? _planCaCount;
        private decimal? _planTotalAmount;
        private int? _mruCount;


        [DataMember(Order=1)]
        public int RowNo
        {
            get { return this._rowNo; }
            set { this._rowNo = value; }
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
        public string AgencyId
        {
            get { return this._agencyId; }
            set { this._agencyId = value; }
        }

        [DataMember(Order=6)]
        public decimal? SecurityDeposit
        {
            get { return this._securityDeposit; }
            set { this._securityDeposit = value; }
        }

        [DataMember(Order=7)]
        public int? CaCount
        {
            get { return this._caCount; }
            set { this._caCount = value; }
        }

        [DataMember(Order=8)]
        public string BookCreateDay
        {
            get { return this._bookCreateDay; }
            set { this._bookCreateDay = value; }
        }

        [DataMember(Order=9)]
        public List<string> MruList
        {
            get { return _mruList; }
            set { _mruList = value; }
        }

        //Checked TongKung
        //[DataMember(Order=10)]
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


        [DataMember(Order=13)]
        public int? MruCount
        {
            get { return this._mruCount; }
            set { this._mruCount = value; }
        }


        /*
        #region "Local Variable"        
        private string _portionNo;
        private string _branchId;
        private string _branchName;
        private string _keepTimeNo;
        private string _areaCode;
        private string _paidDate;
        private string _agentId;
        private decimal? _creditMoney = 0;
        private string _rangeOfLine;
        private int? _customerOfQuanlity = 0;
        private decimal? _totalAmount = 0;
        private string _prefix;
        #endregion

        #region "Property"

        [DataMember(Order=14)]
        public int? SeqNo
        {
            get { return _seqNo; }
            set { this._seqNo = value; }
        }

        [DataMember(Order=15)]
        public string PortionNo
        {
            get { return this._portionNo ; }
            set { this._portionNo = value; }
        }


        [DataMember(Order=16)]
        public string BranchId
        {
            get { return _branchId; }
            set { this._branchId = value; }
        }

        [DataMember(Order=17)]
        public string BranchName
        {
            get { return _branchName; }
            set { this._branchName = value; }
        }

        [DataMember(Order=18)]
        public string KeepTimeNo
        {
            get { return _keepTimeNo; }
            set { this._keepTimeNo = value; }
        }

        [DataMember(Order=19)]
        public string AreaCode
        {
            get { return _areaCode; }
            set { this._areaCode = value; }
        }

        [DataMember(Order=20)]
        public string PaidDate
        {
            get { return _paidDate; }
            set { this._paidDate = value; }
        }

        [DataMember(Order=21)]
        public string AgentId
        {
            get { return _agentId; }
            set { this._agentId = value; }
        }

        [DataMember(Order=22)]
        public decimal? CreditMoney
        {
            get { return _creditMoney; }
            set { this._creditMoney = value; }
        }

        [DataMember(Order=23)]
        public string RangeOfLine
        {
            get { return _rangeOfLine; }
            set { this._rangeOfLine = value; }
        }

        [DataMember(Order=24)]
        public int? CustomerOfQuanlity
        {
            get { return _customerOfQuanlity; }
            set { this._customerOfQuanlity = value; }
        }

        [DataMember(Order=25)]
        public decimal? TotalAmount
        {
            get { return _totalAmount; }
            set { this._totalAmount = value; }
        }

        [DataMember(Order=26)]
        public string Prefix
        {
            get { return _prefix; }
            set { this._prefix = value; }
        }
        #endregion
        */
    }
}
