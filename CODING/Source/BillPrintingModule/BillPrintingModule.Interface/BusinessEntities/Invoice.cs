using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace PEA.BPM.BillPrintingModule.Interface.BusinessEntities
{
    [DataContract]
    public class Invoice
    {
        private string _invoiceNo;

        [DataMember(Order=1)]
        public string InvoiceNo
        {
            get { return _invoiceNo; }
            set { _invoiceNo = value; }
        }

        private string _receiptNo;

        [DataMember(Order=2)]
        public string ReceiptNo
        {
            get { return _receiptNo; }
            set { _receiptNo = value; }
        }

        private int? _printLog;

        [DataMember(Order=3)]
        public int? PrintLog
        {
            get { return _printLog; }
            set { _printLog = value; }
        }

        private DateTime? _printDt;

        [DataMember(Order=4)]
        public DateTime? PrintDt
        {
            get { return _printDt; }
            set { _printDt = value; }
        }

        private string _bpmPrintDate;

        [DataMember(Order=5)]
        public string BpmPrintDate
        {
            get { return _printDt==null?"":_printDt.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH")); }
            set { _bpmPrintDate = value; }
        }

        private string _w_01_print_doc;

        [DataMember(Order=6)]
        public string W_01_print_doc
        {
            get { return _w_01_print_doc; }
            set { _w_01_print_doc = value; }
        }

        private string _w_40_sname;

        [DataMember(Order=7)]
        public string W_40_sname
        {
            get { return _w_40_sname; }
            set { _w_40_sname = value; }
        }

        private string _caName;

        [DataMember(Order=8)]
        public string CaName
        {
            get { return _caName; }
            set { _caName = value; }
        }

        private string _w_130_period;

        [DataMember(Order=9)]
        public string W_130_period
        {
            get { return _w_130_period; }
            set { _w_130_period = value; }
        }

        private string _w_150_cont_acct;

        [DataMember(Order=10)]
        public string W_150_cont_acct
        {
            get { return _w_150_cont_acct; }
            set { _w_150_cont_acct = value; }
        }

        private string _w_1510_total_amnt_txt;

        [DataMember(Order=11)]
        public string W_1510_total_amnt_txt
        {
            get { return _w_1510_total_amnt_txt; }
            set { _w_1510_total_amnt_txt = value; }
        }

        private string _w_1830_payment_method;

        [DataMember(Order=12)]
        public string W_1830_payment_method
        {
            get { return _w_1830_payment_method; }
            set { _w_1830_payment_method = value; }
        }

        private string _w_1860_trsg;

        [DataMember(Order=13)]
        public string W_1860_trsg
        {
            get { return _w_1860_trsg; }
            set { _w_1860_trsg = value; }
        }

        private string _w_1861_crsg;

        [DataMember(Order=14)]
        public string W_1861_crsg
        {
            get { return _w_1861_crsg; }
            set { _w_1861_crsg = value; }
        }

        private DateTime? _w_1971_print_time;

        [DataMember(Order=15)]
        public DateTime? W_1971_print_time
        {
            get { return _w_1971_print_time; }
            set { _w_1971_print_time = value; }
        }

        private string _printDate;

        [DataMember(Order=16)]
        public string PrintDate
        {
            get { return _w_1971_print_time.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH")); }
            set { _printDate = value; }
        }

        private string _w_800_mr_kw_12_2_txt;

        [DataMember(Order=17)]
        public string W_800_mr_kw_12_2_txt
        {
            get { return _w_800_mr_kw_12_2_txt; }
            set { _w_800_mr_kw_12_2_txt = value; }
        }

        private string _w_790_mr_kw_12_1_txt;

        [DataMember(Order=18)]
        public string W_790_mr_kw_12_1_txt
        {
            get { return _w_790_mr_kw_12_1_txt; }
            set { _w_790_mr_kw_12_1_txt = value; }
        }

        private string _w_190_multi_n;

        [DataMember(Order=19)]
        public string W_190_multi_n
        {
            get { return _w_190_multi_n; }
            set { _w_190_multi_n = value; }
        }

        private string _w_171_ettat_code;

        [DataMember(Order=20)]
        public string W_171_ettat_code
        {
            get { return _w_171_ettat_code; }
            set { _w_171_ettat_code = value; }
        }

        private string _w_160_device;

        [DataMember(Order=21)]
        public string W_160_device
        {
            get { return _w_160_device; }
            set { _w_160_device = value; }
        }

        private string _w_180_voltage;

        [DataMember(Order=22)]
        public string W_180_voltage
        {
            get { return _w_180_voltage; }
            set { _w_180_voltage = value; }
        }

        private string _w_200_mr_date;

        [DataMember(Order=23)]
        public string W_200_mr_date
        {
            get { return _w_200_mr_date; }
            set { _w_200_mr_date = value; }
        }

        private string _w_1960_acct_class;

        [DataMember(Order=24)]
        public string W_1960_acct_class
        {
            get { return _w_1960_acct_class; }
            set { _w_1960_acct_class = value; }
        }

        private string _w_1980_spotbill;

        [DataMember(Order=25)]
        public string W_1980_spotbill
        {
            get { return _w_1980_spotbill; }
            set { _w_1980_spotbill = value; }
        }

        private string _w_1990_addition;

        [DataMember(Order=26)]
        public string W_1990_addition
        {
            get { return _w_1990_addition; }
            set { _w_1990_addition = value; }
        }

        private string _w_2000_dispctrl;

        [DataMember(Order=27)]
        public string W_2000_dispctrl
        {
            get { return _w_2000_dispctrl; }
            set { _w_2000_dispctrl = value; }
        }

        private string _w_2010_noprnt;

        [DataMember(Order=28)]
        public string W_2010_noprnt
        {
            get { return _w_2010_noprnt; }
            set { _w_2010_noprnt = value; }
        }

        private int? _reverseCount;

        [DataMember(Order=29)]
        public int? ReverseCount
        {
            get { return _reverseCount; }
            set { _reverseCount = value; }
        }

        private string _w_1910_org_doc;

        [DataMember(Order=30)]
        public string W_1910_org_doc
        {
            get { return _w_1910_org_doc; }
            set { _w_1910_org_doc = value; }
        }

        private string _active;

        [DataMember(Order=31)]
        public string Active
        {
            get { return _active; }
            set { _active = value; }
        }

        private string _status;

        [DataMember(Order=32)]
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        private string _w_10_doc_date;

        [DataMember(Order=33)]
        public string W_10_doc_date
        {
            get { return _w_10_doc_date; }
            set { _w_10_doc_date = value; }
        }

        private DateTime? _RePrintDate;

        [DataMember(Order = 34)]
        public DateTime? RePrintDate
        {
            get { return _RePrintDate; }
            set { _RePrintDate = value; }
        }

        private string _bpmRePrintDate;

        [DataMember(Order = 35)]
        public string BpmRePrintDate
        {
            get { return _RePrintDate == null ? "" : _RePrintDate.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH")); }
            set { _bpmRePrintDate = value; }
        }
        
    }
}
