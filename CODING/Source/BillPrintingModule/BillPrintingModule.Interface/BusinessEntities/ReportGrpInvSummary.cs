using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace PEA.BPM.BillPrintingModule.Interface.BusinessEntities
{
    [DataContract]
    public class ReportGrpInvSummary
    {
        private string _payer;
        private string _payerName;
        private string _mtNo;
        private string _branchId;
        private string _mruId;
        private string _groupingDate;
        private DateTime? _printDt;
        private decimal? _totAmount;
        private int? _totBill;
        private int? _printCount;
        private int? _remainCount;
        private string _printedBy;
        private string _printBranch;
        private string _branchName;

        private string _printDate;
        private string _printTime;


        [DataMember(Order=1)]
        public string Payer
        {
            get { return _payer; }
            set { _payer = value; }
        }


        [DataMember(Order=2)]
        public string PayerName
        {
            get { return _payerName; }
            set { _payerName = value; }
        }


        [DataMember(Order=3)]
        public string MtNo
        {
            get { return _mtNo; }
            set { _mtNo = value; }
        }


        [DataMember(Order=4)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }


        [DataMember(Order=5)]
        public string PrintBranch
        {
            get { return _printBranch; }
            set { _printBranch = value; }
        }


        [DataMember(Order=6)]
        public string BranchName
        {
            get { return _branchName; }
            set { _branchName = value; }
        }


        [DataMember(Order=7)]
        public string MruId
        {
            get { return _mruId; }
            set { _mruId = value; }
        }


        [DataMember(Order=8)]
        public string GroupDate
        {
            set { _groupingDate = value; }
            get { return _groupingDate; }
            //get { return _groupingDate.Substring(0, 2) + "/"+ _groupingDate.Substring(2, 2) + "/" + _groupingDate.Substring(4, 4); }
        }

        //Checked A
        //[DataMember(Order=9)]
        public string GroupDateText
        {
            get { return _groupingDate.Substring(0, 2) + "/" + _groupingDate.Substring(2, 2) + "/" + _groupingDate.Substring(4, 4); }
        }


        [DataMember(Order=10)]
        public DateTime? PrintDt
        {
            get { return _printDt; }
            set { 
                _printDt = value;
            }
        }

        //Checked A
        //[DataMember(Order=11)]
        public string PrintDate
        {            
            get { return _printDt.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")); }
        }

        //Checked A
        //[DataMember(Order=12)]
        public string PrintTime
        {
            get { return _printDt.Value.ToString("HH:mm:ss", new CultureInfo("th-TH")); }
        }


        [DataMember(Order=13)]
        public decimal? TotAmount
        {
            get { return _totAmount; }
            set { _totAmount = value; }
        }


        [DataMember(Order=14)]
        public int? TotBill
        {
            get { return _totBill; }
            set { _totBill = value; }
        }


        [DataMember(Order=15)]
        public int? PrintCount
        {
            get { return _printCount; }
            set { _printCount = value; }
        }


        [DataMember(Order=16)]
        public int? RemainCount
        {
            get { return _remainCount; }
            set { _remainCount = value; }
        }


        [DataMember(Order=17)]
        public string PrintedBy
        {
            get { return _printedBy; }
            set { _printedBy = value; }
        }


    }
}
