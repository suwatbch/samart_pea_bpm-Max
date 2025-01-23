using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace PEA.BPM.BillPrintingModule.Interface.BusinessEntities
{
    [DataContract]
    public class ReportPrintByBank
    {
        private string _bankId;
        private string _bankKey;
        private string _bankName;
        private string _branchId;
        private string _branchName;
        private string _mruId;
        private DateTime? _billProcDate;
        private DateTime? _printDt;
        private int? _fromBillSeqNo;
        private int? _toBillSeqNo;
        private int? _billCount;
        private decimal? _totalAmount;
        private string _printedBy;


        [DataMember(Order=1)]
        public string BankId
        {
            set { _bankId = value; }
            get { return _bankId; }
        }



        [DataMember(Order=2)]
        public string BankKey
        {
            set { _bankKey = value; }
            get { return _bankKey; }
        }


        [DataMember(Order=3)]
        public string BankName
        {
            set { _bankName = value; }
            get { return _bankName; }
        }


        [DataMember(Order=4)]
        public string BranchId
        {
            set { _branchId = value; }
            get { return _branchId; }
        }


        [DataMember(Order=5)]
        public string BranchName
        {
            set { _branchName = value; }
            get { return _branchName; }
        }


        [DataMember(Order=6)]
        public string MruId
        {
            set { _mruId = value; }
            get { return _mruId; }
        }


        [DataMember(Order=7)]
        public DateTime? BillProcDate
        {
            set { _billProcDate = value; }
            get { return _billProcDate; }
        }

        //Checked A
        //[DataMember(Order=8)]
        public string ProcDateStr
        {
            get { return _billProcDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")); }
        }


        [DataMember(Order=9)]
        public DateTime? PrintDt
        {
            set { _printDt = value; }
            get { return _printDt; }
        }

        //Checked A
        //[DataMember(Order=10)]
        public string PrintDate
        {
            get { return _printDt.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")); }
        }

        //checked A 
        //[DataMember(Order=11)]
        public string PrintTime
        {
            get { return _printDt.Value.ToString("HH:mm:ss", new CultureInfo("th-TH")); }
        }


        [DataMember(Order=12)]
        public int? FromBillSeqNo
        {
            set { _fromBillSeqNo = value; }
            get { return _fromBillSeqNo; }
        }


        [DataMember(Order=13)]
        public int? ToBillSeqNo
        {
            set { _toBillSeqNo = value; }
            get { return _toBillSeqNo; }
        }


        [DataMember(Order=14)]
        public int? BillCount
        {
            set { _billCount = value; }
            get { return _billCount; }
        }


        [DataMember(Order=15)]
        public decimal? TotalAmount
        {
            set { _totalAmount = value; }
            get { return _totalAmount; }
        }


        [DataMember(Order=16)]
        public string PrintedBy
        {
            set { _printedBy = value; }
            get { return _printedBy; }
        }


    }
}
