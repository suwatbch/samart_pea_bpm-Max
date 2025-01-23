using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.BillPrintingModule.Interface.BusinessEntities
{
    [DataContract]
    public class ReportBillDelivery
    {
        private string _branchId;
        private string _branchName;       
        private string _mruId;
        private string _printType;
        private string _caId;
        private string _billPred;
        private string _invoiceFrom;
        private string _invoiceTo;
        private int? _totBill;
        private Decimal? _totUnit;
        private Decimal? _totAmt;
        private Decimal? _totVat;
        private Decimal? _totFt;
        private Decimal? _totModVat;
        private Decimal? _totGAmt;
        private Decimal? _totDiscount;
        private Decimal? _totGElecAmt;
        private Decimal? _totFreeUnit;
        private int? _billSeqNoFrom;
        private int? _billSeqNoTo;
        private int? _nonPrint;
        private string _rateType;
        private string _oInvoiceNo;
        private Decimal? _oUnit;
        private Decimal? _oAmt;
        private Decimal? _oFt;
        private Decimal? _oVat;
        private Decimal? _oGAmt;
        private Decimal? _oModVat;
        private Decimal? _oDiscount;
        private Decimal? _oGElecAmt;
        private string _nInvoiceNo;
        private Decimal? _nUnit;
        private Decimal? _nAmt;
        private Decimal? _nFt;
        private Decimal? _nVat;
        private Decimal? _nGAmt;
        private Decimal? _nModVat;
        private Decimal? _nDiscount;
        private Decimal? _nGElecAmt;
        private DateTime? _sentDt;


        [DataMember(Order=1)]
        public DateTime? SentDt
        {
            get { return _sentDt; }
            set { _sentDt = value; }
        }


        [DataMember(Order=2)]
        public string BranchName
        {
            get { return _branchName; }
            set { _branchName = value; }
        }
        

        [DataMember(Order=3)]
        public string MruId
        {
            get { return _mruId; }
            set { _mruId = value; }
        }


        [DataMember(Order=4)]
        public string PrintType
        {
            get { return _printType; }
            set { _printType = value; }
        }


        [DataMember(Order=5)]
        public string InvoiceFrom
        {
            get { return _invoiceFrom; }
            set { _invoiceFrom = value; }
        }       


        [DataMember(Order=6)]
        public string InvoiceTo
        {
            get { return _invoiceTo; }
            set { _invoiceTo = value; }
        }


        [DataMember(Order=7)]
        public int? TotBill
        {
            get { return _totBill; }
            set { _totBill = value; }
        }
       

        [DataMember(Order=8)]
        public Decimal? TotUnit
        {
            get { return _totUnit; }
            set { _totUnit = value; }
        }


        [DataMember(Order=9)]
        public Decimal? TotAmt
        {
            get { return _totAmt; }
            set { _totAmt = value; }
        }


        [DataMember(Order=10)]
        public Decimal? TotVat
        {
            get { return _totVat; }
            set { _totVat = value; }
        }


        [DataMember(Order=11)]
        public Decimal? TotFt
        {
            get { return _totFt; }
            set { _totFt = value; }
        }


        [DataMember(Order=12)]
        public Decimal? TotGAmt
        {
            get { return _totGAmt; }
            set { _totGAmt = value; }
        }


        [DataMember(Order=13)]
        public Decimal? TotModVat
        {
            get { return _totModVat; }
            set { _totModVat = value; }
        }
        

        [DataMember(Order=14)]
        public Decimal? TotDiscount
        {
            get { return _totDiscount; }
            set { _totDiscount = value; }
        }
        

        [DataMember(Order=15)]
        public Decimal? TotGElecAmt
        {
            get { return _totGElecAmt; }
            set { _totGElecAmt = value; }
        }
        

        [DataMember(Order=16)]
        public Decimal? TotFreeUnit
        {
            get { return _totFreeUnit; }
            set { _totFreeUnit = value; }
        }
       

        [DataMember(Order=17)]
        public int? BillSeqNoFrom
        {
            get { return _billSeqNoFrom; }
            set { _billSeqNoFrom = value; }
        }


        [DataMember(Order=18)]
        public int? BillSeqNoTo
        {
            get { return _billSeqNoTo; }
            set { _billSeqNoTo = value; }
        }


        [DataMember(Order=19)]
        public int? NonPrint
        {
            get { return _nonPrint; }
            set { _nonPrint = value; }
        }


        [DataMember(Order=20)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }


        [DataMember(Order=21)]
        public string CaId
        {
            get { return _caId; }
            set { _caId = value; }
        }


        [DataMember(Order=22)]
        public string RateType
        {
            get { return _rateType; }
            set { _rateType = value; }
        }


        [DataMember(Order=23)]
        public string BillPred
        {
            get { return _billPred; }
            set { _billPred = value; }
        }


        [DataMember(Order=24)]
        public string OInvoiceNo
        {
            get { return _oInvoiceNo; }
            set { _oInvoiceNo = value; }
        }


        [DataMember(Order=25)]
        public Decimal? OUnit
        {
            get { return _oUnit; }
            set { _oUnit = value; }
        }


        [DataMember(Order=26)]
        public Decimal? OAmt
        {
            get { return _oAmt; }
            set { _oAmt = value; }
        }


        [DataMember(Order=27)]
        public Decimal? OFt
        {
            get { return _oFt; }
            set { _oFt = value; }
        }


        [DataMember(Order=28)]
        public Decimal? OVat
        {
            get { return _oVat; }
            set { _oVat = value; }
        }


        [DataMember(Order=29)]
        public Decimal? OGAmt
        {
            get { return _oGAmt; }
            set { _oGAmt = value; }
        }


        [DataMember(Order=30)]
        public Decimal? OModVat
        {
            get { return _oModVat; }
            set { _oModVat = value; }
        }


        [DataMember(Order=31)]
        public Decimal? ODiscount
        {
            get { return _oDiscount; }
            set { _oDiscount = value; }
        }


        [DataMember(Order=32)]
        public Decimal? OGElecAmt
        {
            get { return _oGElecAmt; }
            set { _oGElecAmt = value; }
        }


        [DataMember(Order=33)]
        public string NInvoiceNo
        {
            get { return _nInvoiceNo; }
            set { _nInvoiceNo = value; }
        }


        [DataMember(Order=34)]
        public Decimal? NUnit
        {
            get { return _nUnit; }
            set { _nUnit = value; }
        }


        [DataMember(Order=35)]
        public Decimal? NAmt
        {
            get { return _nAmt; }
            set { _nAmt = value; }
        }


        [DataMember(Order=36)]
        public Decimal? NFt
        {
            get { return _nFt; }
            set { _nFt = value; }
        }


        [DataMember(Order=37)]
        public Decimal? NVat
        {
            get { return _nVat; }
            set { _nVat = value; }
        }


        [DataMember(Order=38)]
        public Decimal? NGAmt
        {
            get { return _nGAmt; }
            set { _nGAmt = value; }
        }


        [DataMember(Order=39)]
        public Decimal? NModVat
        {
            get { return _nModVat; }
            set { _nModVat = value; }
        }


        [DataMember(Order=40)]
        public Decimal? NDiscount
        {
            get { return _nDiscount; }
            set { _nDiscount = value; }
        }


        [DataMember(Order=41)]
        public Decimal? NGElecAmt
        {
            get { return _nGElecAmt; }
            set { _nGElecAmt = value; }
        }
    }
}
