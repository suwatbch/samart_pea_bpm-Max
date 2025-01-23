using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace PEA.BPM.CashManagementModule.Interface.BusinessEntities
{
    //nested in CloseWorkSummaryInfo
    [DataContract]
    public class FlowSummaryInfo
    {
        public FlowSummaryInfo()
        {
            _cashIn = 0;
            _chequeIn = 0;
            _cashOut = 0;
            _chequeOut = 0;
        }

        private string _flowCat;
        private string _workId;
        private string _flowId;
        private string _flowType;
        private string _flowDesc;
        private string   _description;
        private decimal? _cashIn;
        private decimal? _chequeIn;
        private decimal? _cashOut;
        private decimal? _chequeOut;
        private string _accNo;
        private string _bankKey;
        private string _bankName;
        private string _transferId;
        private DateTime _modifiedDt;
        private string _modifiedBy;

        //special used
        private string _cashierId;
        private string _cashierName;
        private string _branchId;
        private string _posId;
        private string _postedBranchId;


        [DataMember(Order=1)]
        public string WorkId
        {
            get { return _workId; }
            set { _workId = value; }
        }
        //1 normal , 2= cancel

        [DataMember(Order=2)]
        public string FlowCat
        {
            get { return _flowCat; }
            set { _flowCat = value; }
        }


        [DataMember(Order=3)]
        public string FlowId
        {
            get { return _flowId; }
            set { _flowId = value; }
        }


        [DataMember(Order=4)]
        public string TransferId
        {
            get { return _transferId; }
            set { _transferId = value; }
        }


        [DataMember(Order=5)]
        public string FlowType
        {
            get { return _flowType; }
            set { _flowType = value; }
        }

        [DataMember(Order=6)]
        public string FlowDesc
        {
            get { return _flowDesc; }
            set { _flowDesc = value; }
        }

        [DataMember(Order=7)]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }


        [DataMember(Order=8)]
        public decimal? CashIn
        {
            get { return _cashIn; }
            set { _cashIn = value; }
        }


        [DataMember(Order=9)]
        public decimal? ChequeIn
        {
            get { return _chequeIn; }
            set { _chequeIn = value; }
        }


        [DataMember(Order=10)]
        public decimal? CashOut
        {
            get { return _cashOut; }
            set { _cashOut = value; }
        }


        [DataMember(Order=11)]
        public decimal? ChequeOut
        {
            get { return _chequeOut; }
            set { _chequeOut = value; }
        }


        [DataMember(Order=12)]
        public string BankKey
        {
            get { return _bankKey; }
            set { _bankKey = value; }
        }

        [DataMember(Order = 13)]
        public string BankName
        {
            get { return _bankName; }
            set { _bankName = value; }
        }

        [DataMember(Order=14)]
        public string AccountNo
        {
            get { return _accNo; }
            set { _accNo = value; }
        }


        [DataMember(Order=15)]
        public string TongKung
        {
            get;
            set;
        }

        //Checked TongKung
        public string FlowDate
        {
            get
            {
                CultureInfo culture = new CultureInfo("th-TH");
                return _modifiedDt.ToString("dd/MM/yyyy", culture);
            }
        }

        //Checked TongKung
        public string FlowTime
        {
            get { return _modifiedDt.ToString("HH:mm", new CultureInfo("th-TH")); }
        }


        [DataMember(Order=16)]
        public DateTime ModifiedDt
        {
            get { return _modifiedDt; }
            set { _modifiedDt = value; }
        }


        [DataMember(Order=17)]
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }


        [DataMember(Order=18)]
        public string CashierId
        {
            get { return _cashierId; }
            set { _cashierId = value; }
        }


        [DataMember(Order=19)]
        public string CashierName
        {
            get { return _cashierName; }
            set { _cashierName = value; }
        }


        [DataMember(Order=20)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }


        [DataMember(Order=21)]
        public string PosId
        {
            get { return _posId; }
            set { _posId = value; }
        }


        [DataMember(Order=22)]
        public string PostedBranchId
        {
            get { return _postedBranchId; }
            set { _postedBranchId = value; }
        }

    }
}
