using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.BillPrintingModule.Interface.BusinessEntities
{
    [DataContract, Serializable]
    public class ReportF16
    {
        string _invoiceNo;
        string _branchId;
        string _branchName;
        string _mruId;
        string _caId;
        string _dateCur;
        string _eType;
        string _ePower;
        string _unitCur;
        string _unitPrev;
        Decimal? _unit;
        Decimal? _baseAmt;
        Decimal? _discountAmt;
        Decimal? _ftAmt;
        Decimal? _amt;
        Decimal? _vatAmt;
        Decimal? _gAmt;
        Decimal? _gAmtRounded;
        Decimal? _freeUnit;
        string _collectType;
        string _note;

        //Move from DB to web server, Sep, 01 09
        string _w_171_ettat_code;
        string _w_1861_crsg;
        string _w_1570_account_number;
        string _w_1910_org_doc;
        string _w_1830_payment_method;
        string _w_1960_acct_class;
        string _w_1980_spotbill;
        string _w_1990_addition;
        string _w_2000_dispctrl;


        [DataMember(Order=1)]
        public string W_2000_dispctrl
        {
            get { return _w_2000_dispctrl; }
            set { _w_2000_dispctrl = value; }
        }


        [DataMember(Order=2)]
        public string W_1990_addition
        {
            get { return _w_1990_addition; }
            set { _w_1990_addition = value; }
        }


        [DataMember(Order=3)]
        public string W_1980_spotbill
        {
            get { return _w_1980_spotbill; }
            set { _w_1980_spotbill = value; }
        }


        [DataMember(Order=4)]
        public string W_1960_acct_class
        {
            get { return _w_1960_acct_class; }
            set { _w_1960_acct_class = value; }
        }


        [DataMember(Order=5)]
        public string W_171_ettat_code
        {
            get { return _w_171_ettat_code; }
            set { _w_171_ettat_code = value; }
        }


        [DataMember(Order=6)]
        public string W_1861_crsg
        {
            get { return _w_1861_crsg; }
            set { _w_1861_crsg = value; }
        }


        [DataMember(Order=7)]
        public string W_1570_account_number
        {
            get { return _w_1570_account_number; }
            set { _w_1570_account_number = value; }
        }


        [DataMember(Order=8)]
        public string W_1910_org_doc
        {
            get { return _w_1910_org_doc; }
            set { _w_1910_org_doc = value; }
        }


        [DataMember(Order=9)]
        public string W_1830_payment_method
        {
            get { return _w_1830_payment_method; }
            set { _w_1830_payment_method = value; }
        }


        [DataMember(Order=10)]
        public string InvoiceNo
        {
            get { return _invoiceNo; }
            set { _invoiceNo = value; }
        }


        [DataMember(Order=11)]
        public string CaId
        {
            get { return _caId; }
            set { _caId = value; }
        }


        [DataMember(Order=12)]
        public string DateCur
        {
            get { return _dateCur; }
            set { _dateCur = value; }
        }


        [DataMember(Order=13)]
        public string EType
        {
            get { return _eType; }
            set { _eType = value; }
        }


        [DataMember(Order=14)]
        public string EPower
        {
            get { return _ePower; }
            set { _ePower = value; }
        }


        [DataMember(Order=15)]
        public string UnitCur
        {
            get { return _unitCur; }
            set { _unitCur = value; }
        }


        [DataMember(Order=16)]
        public string UnitPrev
        {
            get { return _unitPrev; }
            set { _unitPrev = value; }
        }


        [DataMember(Order=17)]
        public Decimal? Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }


        [DataMember(Order=18)]
        public Decimal? BaseAmt
        {
            get { return _baseAmt; }
            set { _baseAmt = value; }
        }


        [DataMember(Order=19)]
        public Decimal? DiscountAmt
        {
            get { return _discountAmt; }
            set { _discountAmt = value; }
        }


        [DataMember(Order=20)]
        public Decimal? FtAmt
        {
            get { return _ftAmt; }
            set { _ftAmt = value; }
        }


        [DataMember(Order=21)]
        public Decimal? Amt
        {
            get { return _amt; }
            set { _amt = value; }
        }


        [DataMember(Order=22)]
        public Decimal? VatAmt
        {
            get { return _vatAmt; }
            set { _vatAmt = value; }
        }


        [DataMember(Order=23)]
        public Decimal? GAmt
        {
            get { return _gAmt; }
            set { _gAmt = value; }
        }


        [DataMember(Order=24)]
        public Decimal? GAmtRounded
        {

            get { return _gAmtRounded; }
            set { _gAmtRounded = value; }
        }


        [DataMember(Order=25)]
        public Decimal? FreeUnit
        {
            get { return _freeUnit; }
            set { _freeUnit = value; }
        }


        [DataMember(Order=26)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }

        [DataMember(Order=27)]
        public string BranchName
        {
            get { return _branchName; }
            set { _branchName = value; }
        }


        [DataMember(Order=28)]
        public string MruId
        {
            get { return _mruId; }
            set { _mruId = value; }
        }


        [DataMember(Order=29)]
        public string CollectType
        {
            get { return _collectType; }
            set { _collectType = value; }
        }


        [DataMember(Order=30)]
        public string Note
        {
            get { return _note; }
            set { _note = value; }
        }

    }
}
