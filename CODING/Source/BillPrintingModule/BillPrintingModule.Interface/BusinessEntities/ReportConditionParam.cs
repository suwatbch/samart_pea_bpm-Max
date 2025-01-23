using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.BillPrintingModule.Interface.BusinessEntities
{
    [DataContract]
    public enum PrintConditionEnum
    {
        [EnumMember]
        All,
        [EnumMember]
        Branch,
        [EnumMember]
        Mru
    }

    [DataContract]
    public class InputParam : IComparable<InputParam>
    {
        private string _inputStr;
        private string _groupBranch;
        private string _groupBranchName;
        private int? _printType;


        [DataMember(Order=1)]
        public string InputStr
        {
            get { return _inputStr; }
            set { _inputStr = value; }
        }


        [DataMember(Order=2)]
        public int? PrintType
        {
            get { return _printType; }
            set { _printType = value; }
        }


        [DataMember(Order=3)]
        public string GroupBranch
        {
            get { return _groupBranch; }
            set { _groupBranch = value; }
        }


        [DataMember(Order=4)]
        public string GroupBranchName
        {
            get { return _groupBranchName; }
            set { _groupBranchName = value; }
        }


        #region IComparable<InputParam> Members

        public int CompareTo(InputParam other)
        {
            if (this.GroupBranch == other.GroupBranch)
                return string.Compare(this.InputStr, other.InputStr);
            else
                return string.Compare(this.GroupBranch, other.GroupBranch);
        }

        #endregion
    }

    [DataContract]
    public class ReportConditionParam
    {
        DateTime? _printDate; //use in reportBillDelivery
        string _billPeriod;
        DateTime? _dataReceiveDt;
        DateTime? _toDataReceiveDt;
        int? _billPeriodLog; //it is report vol. no.
        string _portion;
        string _portionNo;      
        int? _printingCondition; // print All, Branch
        /// <summary>
        /// _reportType is used in many situation as follow:
        /// normal(blue),reprint(blue),summary or detail report
        /// </summary>
        int? _reportType;
        string _electricId;
        string _billControllerId;
        string _approvedPerson;
        string _reportToPerson;
        string _saveNumber;
        string _deliveryPlace;
        string[] _groupBillPeriod;
        bool _save;
        bool _IsReprint;
        string _printedFlag;  //printed, notprint report

        List<InputParam> _inputParamList = new List<InputParam>();
       
        int? _printType; // blue, green , A4
        string[] _groupPrintType;

        private string _branchId;   //Use in F16
        private string _mruId;      //Use in F16
        private string _toMruId;    //Use in F16

        List<string> _listElectricId = new List<string>(); // used for reportBillDelivery
        List<string> _listPrintType = new List<string>(); // used for reportBillDelivery

        //new added March, 17 08
        private DateTime _fromDate;
        private DateTime _toDate;

        private string _fromTime;
        private string _toTime;

        // *** report signature
        private string _printedBy;
        private string _printBranch;
        private string _deliveryPlaceName;
        private string _toWhom;

        //** group inv, directdebit
        private string _mtNo;
        private string _bankId;
        private string _bankDueDate;


        [DataMember(Order=1)]
        public string FromTime
        {
            get { return _fromTime; }
            set { _fromTime = value; }
        }


        [DataMember(Order=2)]
        public string ToTime
        {
            get { return _toTime; }
            set { _toTime = value; }
        }


        [DataMember(Order=3)]
        public string MtNo
        {
            get { return _mtNo; }
            set { _mtNo = value; }
        }


        [DataMember(Order=4)]
        public string BankId
        {
            get { return _bankId; }
            set { _bankId = value; }
        }


        [DataMember(Order=5)]
        public string BankDueDate
        {
            get { return _bankDueDate; }
            set { _bankDueDate = value; }
        }


        [DataMember(Order=6)]
        public bool Save
        {
            get { return _save; }
            set { _save = value; }
        }


        [DataMember(Order=7)]
        public bool IsReprint
        {
            get { return _IsReprint; }
            set { _IsReprint = value; }
        }


        [DataMember(Order=8)]
        public string ToWhom
        {
            get { return _toWhom; }
            set { _toWhom = value; }
        }


        [DataMember(Order=9)]
        public DateTime FromDate
        {
            get { return _fromDate; }
            set { _fromDate = value; }
        }


        [DataMember(Order=10)]
        public DateTime ToDate
        {
            get { return _toDate; }
            set { _toDate = value; }
        }


        [DataMember(Order=11)]
        public DateTime? DataReceiveDt
        {
            get { return _dataReceiveDt; }
            set { _dataReceiveDt = value; }
        }


        [DataMember(Order=12)]
        public DateTime? ToDataReceiveDt
        {
            get { return _toDataReceiveDt; }
            set { _toDataReceiveDt = value; }
        }


        [DataMember(Order=13)]
        public string DeliveryPlaceName
        {
            get { return _deliveryPlaceName; }
            set { _deliveryPlaceName = value; }
        }


        [DataMember(Order=14)]
        public string PrintedBy
        {
            get { return _printedBy; }
            set { _printedBy = value; }
        }


        [DataMember(Order=15)]
        public string PrintBranch
        {
            get { return _printBranch; }
            set { _printBranch = value; }
        }


        [DataMember(Order=16)]
        public List<string> ListElectricId
        {
            get { return _listElectricId; }
            set { _listElectricId = value; }
        }


        [DataMember(Order=17)]
        public List<string> ListPrintType
        {
            get { return _listPrintType; }
            set { _listPrintType = value; }
        }


        [DataMember(Order=18)]
        public string BillPeriod
        {
            get { return _billPeriod; }
            set { _billPeriod = value; }
        }
        

        [DataMember(Order=19)]
        public string[] GroupBillPeriod
        {
            get { return _groupBillPeriod; }
            set { _groupBillPeriod = value; }
        }


        [DataMember(Order=20)]
        public List<InputParam> InputParamList
        {
            get   {
                return _inputParamList;
            }
            set { _inputParamList = value; }
        }


        [DataMember(Order=21)]
        public int? BillPeriodLog
        {
            get { return _billPeriodLog; }
            set { _billPeriodLog = value; }
        }


        [DataMember(Order=22)]
        public string SaveNumber
        {
            get { return _saveNumber; }
            set { _saveNumber = value; }
        }


        [DataMember(Order=23)]
        public string ReportToPerson
        {
            get { return _reportToPerson; }
            set { _reportToPerson = value; }
        }


        [DataMember(Order=24)]
        public string ApprovedPerson
        {
            get { return _approvedPerson; }
            set { _approvedPerson = value; }
        }


        [DataMember(Order=25)]
        public string BillControllerId
        {
            get { return _billControllerId; }
            set { _billControllerId = value; }
        }


        [DataMember(Order=26)]
        public int? ReportType
        {
            get { return _reportType; }
            set { _reportType = value; }
        }


        [DataMember(Order=27)]
        public string ElectricId
        {
            get { return _electricId; }
            set { _electricId = value; }
        }


        [DataMember(Order=28)]
        public int? PrintingCondition
        {
            get { return _printingCondition; }
            set { _printingCondition = value; }
        }


        [DataMember(Order=29)]
        public string Portion
        {
            get { return _portion; }
            set { _portion = value; }
        }


        [DataMember(Order=30)]
        public string PortionNo
        {
            get { return _portionNo; }
            set { _portionNo = value; }
        }


        [DataMember(Order=31)]
        public DateTime? PrintDate
        {
            get { return _printDate; }
            set { _printDate = value; }
        }


        [DataMember(Order=32)]
        public int? PrintType
        {
            get { return _printType; }
            set { _printType = value; }
        }


        [DataMember(Order=33)]
        public string[] GroupPrintType
        {
            get { return _groupPrintType; }
            set { _groupPrintType = value; }
        }
        

        [DataMember(Order=34)]
        public string DeliveryPlace
        {
            get { return _deliveryPlace; }
            set { _deliveryPlace = value; }
        }


        [DataMember(Order=35)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }


        [DataMember(Order=36)]
        public string MruId
        {
            get { return _mruId; }
            set { _mruId = value; }
        }


        [DataMember(Order=37)]
        public string ToMruId
        {
            get { return _toMruId; }
            set { _toMruId = value; }
        }


        [DataMember(Order=38)]
        public string PrintedFlag
        {
            get { return _printedFlag; }
            set { _printedFlag = value; }
        }
    }
}
