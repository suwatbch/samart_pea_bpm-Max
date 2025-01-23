using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.BillPrintingModule.Interface.BusinessEntities
{
    [DataContract]
    public class BillPrintingConditionParam
    {
        private int? _billType;
        private string _billPeriod;
        private int? _billPrintingCondition;
        private string _branchId;
        private string _mruId;
        private string _toMruId;
        private string _userId;
        private string _fromNumberId;
        private string _toNumberId;
        private string _mtNo;
        private string _comment;
        private int? _lotNo;
        private string _bankId;
        private string _dueDate;
        private string[] _groupMruId;
        private string[] _groupElectricId;//B,M,U
        private string[] _groupFromToElectricId;//M,R,From U,To U
        private string[] _groupFromToNumberId;//From Num, To Num
        private bool _isReprintBill;
        private string _commBranchId;  //printbranch
        private string _commBranchName;  //printBranchName
        private string _printedBy;
        private string _approverId;
        private string _approverName;
        private string _approverPosition;
        private int? _hasOrgDoc;  //normal or fixed bill
        private int? _a4Reprint; //0=only unprinted, 1 = print all
        private int? _printerChoice;

        //to store registered branchId, crsg
        #region ISSUE NEW FORM
        [DataMember(Order = 1)]
        public string CommBranchId
        {
            get { return _commBranchId; }
            set { _commBranchId = value; }
        }


        [DataMember(Order = 2)]
        public int? A4Reprint
        {
            get { return _a4Reprint; }
            set { _a4Reprint = value; }
        }


        [DataMember(Order = 3)]
        public int? HasOrgDoc
        {
            get { return _hasOrgDoc; }
            set { _hasOrgDoc = value; }
        }


        [DataMember(Order = 4)]
        public string ApproverName
        {
            get { return _approverName; }
            set { _approverName = value; }
        }


        [DataMember(Order = 5)]
        public string ApproverPosition
        {
            get { return _approverPosition; }
            set { _approverPosition = value; }
        }


        [DataMember(Order = 6)]
        public string PrintedBy
        {
            get { return _printedBy; }
            set { _printedBy = value; }
        }


        [DataMember(Order = 7)]
        public string DueDate
        {
            get { return _dueDate; }
            set { _dueDate = value; }
        }


        [DataMember(Order = 8)]
        public string MtNo
        {
            get { return _mtNo; }
            set { _mtNo = value; }
        }


        [DataMember(Order = 9)]
        public int? LotNo
        {
            get { return _lotNo; }
            set { _lotNo = value; }
        }


        [DataMember(Order = 10)]
        public string BankId
        {
            get { return _bankId; }
            set { _bankId = value; }
        }


        [DataMember(Order = 11)]
        public string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }


        [DataMember(Order = 12)]
        public string ToMruId
        {
            get { return _toMruId; }
            set { _toMruId = value; }
        }


        [DataMember(Order = 13)]
        public string[] GroupMruId
        {
            get { return _groupMruId; }
            set { _groupMruId = value; }
        }


        [DataMember(Order = 14)]
        public bool IsReprintBill
        {
            get { return _isReprintBill; }
            set { _isReprintBill = value; }
        }



        [DataMember(Order = 15)]
        public string[] GroupFromToElectricId
        {
            get { return _groupFromToElectricId; }
            set { _groupFromToElectricId = value; }
        }


        [DataMember(Order = 16)]
        public string[] GroupFromToNumberId
        {
            get { return _groupFromToNumberId; }
            set { _groupFromToNumberId = value; }
        }


        [DataMember(Order = 17)]
        public string FromNumberId
        {
            get { return _fromNumberId; }
            set { _fromNumberId = value; }
        }


        [DataMember(Order = 18)]
        public string ToNumberId
        {
            get { return _toNumberId; }
            set { _toNumberId = value; }
        }


        [DataMember(Order = 19)]
        public int? BillType
        {
            get { return _billType; }
            set { _billType = value; }
        }


        [DataMember(Order = 20)]
        public string BillPeriod
        {
            get { return _billPeriod; }
            set { _billPeriod = value; }
        }


        [DataMember(Order = 21)]
        public int? BillPrintingCondition
        {
            get { return _billPrintingCondition; }
            set { _billPrintingCondition = value; }
        }

        //Property also can be array

        [DataMember(Order = 22)]
        public string[] GroupElectricId
        {
            get { return _groupElectricId; }
            set { _groupElectricId = value; }
        }


        [DataMember(Order = 23)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }


        [DataMember(Order = 24)]
        public string MruId
        {
            get { return _mruId; }
            set { _mruId = value; }
        }


        [DataMember(Order = 25)]
        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        [DataMember(Order = 26)]
        public string CommBranchName
        {
            get { return _commBranchName; }
            set { _commBranchName = value; }
        }

        [DataMember(Order = 27)]
        public string ApproverId
        {
            get { return _approverId; }
            set { _approverId = value; }
        }
        #endregion

        [DataMember(Order = 28)]
        public int? PrinterChoice
        {
            get { return _printerChoice; }
            set { _printerChoice = value; }
        }


    }
}
