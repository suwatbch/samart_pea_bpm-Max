using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class BillBook
    {
        private string _billBookId;

        [DataMember(Order=1)]
        public string BillBookId
        {
            get { return _billBookId; }
            set { _billBookId = value; }
        }

        //Checked TongKung
        //[DataMember(Order=2)]
        public string ShortBillBookId
        {
            get { return _billBookId.Substring(6); }
        }
      
        private string _agentId;

        [DataMember(Order=3)]
        public string AgentId
        {
            get { return _agentId; }
            set { _agentId = value; }
        }

        private string _contractTypeId;

        [DataMember(Order=4)]
        public string ContractTypeId
        {
            get { return _contractTypeId; }
            set { _contractTypeId = value; }
        }

        private string _agentName;

        [DataMember(Order=5)]
        public string AgentName
        {
            get { return _agentName; }
            set { _agentName = value; }
        }

        private string _agentAddress;

        [DataMember(Order=6)]
        public string AgentAddress
        {
            get { return _agentAddress; }
            set { _agentAddress = value; }
        }

        private string _agentBranchId;

        [DataMember(Order=7)]
        public string AgentBranchId
        {
            get { return _agentBranchId; }
            set { _agentBranchId = value; }
        }

        private string _period;

        [DataMember(Order=8)]
        public string Period
        {
            get { return _period; }
            set { _period = value; }
        }

        //Checked TongKung
        //[DataMember(Order=9)]
        public string PeriodString
        {
            get { return StringConvert.FormatPeriod(_period); }
        }     

        private string _controllerId;

        [DataMember(Order=10)]
        public string ControllerId
        {
            get { return _controllerId; }
            set { _controllerId = value; }
        }

        private decimal? _advancePayment;

        [DataMember(Order=11)]
        public decimal? AdvancePayment
        {
            get { return _advancePayment; }
            set { _advancePayment = value; }
        }

        private DateTime? _dueDate;

        [DataMember(Order=12)]
        public DateTime? DueDate
        {
            get { return _dueDate; }
            set { _dueDate = value; }
        }

        private int? _paidCountNumber;

        [DataMember(Order=13)]
        public int? PaidCountNumber
        {
            get { return _paidCountNumber; }
            set { _paidCountNumber = value; }
        }

        private int? _totalBill;

        [DataMember(Order=14)]
        public int? TotalBill
        {
            get { return _totalBill; }
            set { _totalBill = value; }
        }

        private DateTime? _bookCreateDt;

        [DataMember(Order=15)]
        public DateTime? BookCreateDt
        {
            get { return _bookCreateDt; }
            set { _bookCreateDt = value; }
        }

        private decimal? _bookTotalVatAmount;

        [DataMember(Order=16)]
        public decimal? BookTotalVatAmount
        {
            get { return _bookTotalVatAmount; }
            set { _bookTotalVatAmount = value; }
        }

        private decimal? _bookTotalGAmount;

        [DataMember(Order=17)]
        public decimal? BookTotalGAmount
        {
            get { return _bookTotalGAmount; }
            set { _bookTotalGAmount = value; }
        }

        private decimal? _paidGAmount;

        [DataMember(Order=18)]
        public decimal? PaidGAmount
        {
            get { return _paidGAmount; }
            set { _paidGAmount = value; }
        }

        private decimal? _leftAmount;

        [DataMember(Order=19)]
        public decimal? LeftAmount
        {
            get { return _leftAmount; }
            set { _leftAmount = value; }
        }

        private decimal? _toPayAmount;

        [DataMember(Order=20)]
        public decimal? ToPayAmount
        {
            get { return _toPayAmount; }
            set { _toPayAmount = value; }
        }

        private string _techBranchName;

        [DataMember(Order = 21)]
        public string TechBranchName
        {
            get { return _techBranchName; }
            set { _techBranchName = value; }
        }

        private string _commBranchId;

        [DataMember(Order = 22)]
        public string CommBranchId
        {
            get { return _commBranchId; }
            set { _commBranchId = value; }
        }

        private string _commBranchName;

        [DataMember(Order = 23)]
        public string CommBranchName
        {
            get { return _commBranchName; }
            set { _commBranchName = value; }
        }
    }
}
