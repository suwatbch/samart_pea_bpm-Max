using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.BillPrintingModule.Interface.BusinessEntities
{
    [DataContract]
    public class ReportDailyUnprint
    {
        string _branchId;
        string _branchName;
        string _mruId;
        string _portionNo;
        string _zone;
        string _printBranch;
        string _groupBranchId;
        string _groupBranchName;
        int? _countMruId;
        string _invoiceFrom;
        string _invoiceTo;
        int? _totBill;
        int? _a4Bill;
        int? _greenBill;
        int? _blueBill;
        int? _spotBill;
        int? _a4Addition;
        int? _totNoPrint;
        Decimal? _totUnit;
        Decimal? _totFt;
        Decimal? _totVat;
        Decimal? _totGAmt;


        [DataMember(Order=1)]
        public int? TotNoPrint
        {
            get { return _totNoPrint; }
            set { _totNoPrint = value; }
        }


        [DataMember(Order=2)]
        public string PrintBranch
        {
            get { return _printBranch; }
            set { _printBranch = value; }
        }


        [DataMember(Order=3)]
        public string GroupBranchId
        {
            get { return _groupBranchId; }
            set { _groupBranchId = value; }
        }


        [DataMember(Order=4)]
        public string GroupBranchName
        {
            get { return _groupBranchName; }
            set { _groupBranchName = value; }
        }


        [DataMember(Order=5)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
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
        public string PortionNo
        {
            get { return _portionNo; }
            set { _portionNo = value; }
        }


        [DataMember(Order=9)]
        public string Zone
        {
            get { return _zone; }
            set { _zone = value; }
        }


        [DataMember(Order=10)]
        public int? CountMruId
        {
            get { return _countMruId; }
            set { _countMruId = value; }
        }


        [DataMember(Order=11)]
        public string InvoiceFrom
        {
            get { return _invoiceFrom; }
            set { _invoiceFrom = value; }
        }
        

        [DataMember(Order=12)]
        public string InvoiceTo
        {
            get { return _invoiceTo; }
            set { _invoiceTo = value; }
        }


        [DataMember(Order=13)]
        public int? TotBill
        {
            get { return _totBill; }
            set { _totBill = value; }
        }


        [DataMember(Order=14)]
        public int? A4Bill
        {
            get { return _a4Bill; }
            set { _a4Bill = value; }
        }


        [DataMember(Order=15)]
        public int? BlueBill
        {
            get { return _blueBill; }
            set { _blueBill = value; }
        }


        [DataMember(Order=16)]
        public int? GreenBill
        {
            get { return _greenBill; }
            set { _greenBill = value; }
        }


        [DataMember(Order=17)]
        public int? SpotBill
        {
            get { return _spotBill; }
            set { _spotBill = value; }
        }


        [DataMember(Order=18)]
        public int? A4Addition
        {
            get { return _a4Addition; }
            set { _a4Addition = value; }
        }


        [DataMember(Order=19)]
        public Decimal? TotUnit
        {
            get { return _totUnit; }
            set { _totUnit = value; }
        }


        [DataMember(Order=20)]
        public Decimal? TotFt
        {
            get { return _totFt; }
            set { _totFt = value; }
        }


        [DataMember(Order=21)]
        public Decimal? TotVat
        {
            get { return _totVat; }
            set { _totVat = value; }
        }


        [DataMember(Order=22)]
        public Decimal? TotGAmt
        {
            get { return _totGAmt; }
            set { _totGAmt = value; }
        }
    }
}
