using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.BillPrintingModule.Interface.BusinessEntities
{
    [DataContract]
    public class ReportGroupingCrossCheck
    {
        private string _flag;
        private string _bankId;
        private string _bankName;
        private string _bankDueDate;
        private string _groupingDate;
        private string _invoiceNo;
        private string _branchId;
        private string _mruId;
        private string _caId;
        private decimal? _amount;


        [DataMember(Order=1)]
        public string Flag
        {
            set { _flag = value; }
            get { return _flag; }
        }


        [DataMember(Order=2)]
        public string BankId
        {
            set { _bankId = value; }
            get { return _bankId; }
        }


        [DataMember(Order=3)]
        public string BankName
        {
            set { _bankName = value; }
            get { return _bankName; }
        }


        [DataMember(Order=4)]
        public string BankDueDate
        {
            set { _bankDueDate = value; }
            get { return _bankDueDate; }
        }


        [DataMember(Order=5)]
        public string GroupingDate
        {
            set { _groupingDate = value; }
            get { return _groupingDate; }
        }


        [DataMember(Order=6)]
        public string InvoiceNo
        {
            set { _invoiceNo = value; }
            get { return _invoiceNo; }
        }


        [DataMember(Order=7)]
        public string BranchId
        {
            set { _branchId = value; }
            get { return _branchId; }
        }


        [DataMember(Order=8)]
        public string MruId
        {
            set { _mruId = value; }
            get { return _mruId; }
        }


        [DataMember(Order=9)]
        public string CaId
        {
            set { _caId = value; }
            get { return _caId; }
        }


        [DataMember(Order=10)]
        public Decimal? Amount
        {
            set { _amount = value; }
            get { return _amount; }
        }

    }
}
