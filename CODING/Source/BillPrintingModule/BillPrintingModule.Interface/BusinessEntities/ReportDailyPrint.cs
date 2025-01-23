using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.BillPrintingModule.Interface.BusinessEntities
{
    [DataContract]
    public class ReportDailyPrint
    {
        string _branchId;
        string _branchName;
        DateTime? _billPeriod; //printdate
        string _mruId;
        int? _billSeqNoFrom;
        int? _billSeqNoTo;
        int? _printCount;
        string _printType;
        string _printSubType;
        string _invoiceFrom;
        string _invoiceTo;
        int? _totBill;
        string _billFlag;
        int? _printLog;
        string _groupBranch;
        string _groupBranchName;
        
        //special used
        string _invoiceNo;


        [DataMember(Order=1)]
        public string InvoiceNo
        {
            get { return _invoiceNo; }
            set { _invoiceNo = value; }
        }


        [DataMember(Order=2)]
        public string GroupBranch
        {
            get { return _groupBranch; }
            set { _groupBranch = value; }
        }


        [DataMember(Order=3)]
        public string GroupBranchName
        {
            get { return _groupBranchName; }
            set { _groupBranchName = value; }
        }


        [DataMember(Order=4)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }


        [DataMember(Order=5)]
        public string BranchName
        {
            get { return _branchName; }
            set { _branchName = value; }
        }


        [DataMember(Order=6)]
        public DateTime? BillPeriod
        {
            get { return _billPeriod; }
            set { _billPeriod = value; }
        }


        [DataMember(Order=7)]
        public string MruId
        {
            get { return _mruId; }
            set { _mruId = value; }
        }


        [DataMember(Order=8)]
        public int? BillSeqNoFrom
        {
            get { return _billSeqNoFrom; }
            set { _billSeqNoFrom = value; }
        }


        [DataMember(Order=9)]
        public int? BillSeqNoTo
        {
            get { return _billSeqNoTo; }
            set { _billSeqNoTo = value; }
        }


        [DataMember(Order=10)]
        public int? PrintCount
        {
            get { return _printCount; }
            set { _printCount = value; }
        }


        [DataMember(Order=11)]
        public string PrintType
        {
            get { return _printType; }
            set { _printType = value; }
        }


        [DataMember(Order=12)]
        public string PrintSubType
        {
            get { return _printSubType; }
            set { _printSubType = value; }
        }


        [DataMember(Order=13)]
        public string InvoiceFrom
        {
            get { return _invoiceFrom; }
            set { _invoiceFrom = value; }
        }


        [DataMember(Order=14)]
        public string InvoiceTo
        {
            get { return _invoiceTo; }
            set { _invoiceTo = value; }
        }


        [DataMember(Order=15)]
        public int? TotBill
        {
            get { return _totBill; }
            set { _totBill = value; }
        }


        [DataMember(Order=16)]
        public string BillFlag
        {
            get { return _billFlag; }
            set { _billFlag = value; }
        }


        [DataMember(Order=17)]
        public int? PrintLog
        {
            get { return _printLog; }
            set { _printLog = value; }
        }
    }
}
