using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.BillPrintingModule.Interface.BusinessEntities
{
    [DataContract]
    public class BillingMaster
    {
        private string _branchId;

        [DataMember(Order=1)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }

        private string _mruId;

        [DataMember(Order=2)]
        public string MruId
        {
            get { return _mruId; }
            set { _mruId = value; }
        }

        private string _billPred;

        [DataMember(Order=3)]
        public string BillPred
        {
            get { return _billPred; }
            set { _billPred = value; }
        }

        private int? _billPred_Log;

        [DataMember(Order=4)]
        public int? BillPred_Log
        {
            get { return _billPred_Log; }
            set { _billPred_Log = value; }
        }

        private string _invoiceFrom;

        [DataMember(Order=5)]
        public string InvoiceFrom
        {
            get { return _invoiceFrom; }
            set { _invoiceFrom = value; }
        }

        private string _invoiceTo;

        [DataMember(Order=6)]
        public string InvoiceTo
        {
            get { return _invoiceTo; }
            set { _invoiceTo = value; }
        }

        private int? _totBill;

        [DataMember(Order=7)]
        public int? TotBill
        {
            get { return _totBill; }
            set { _totBill = value; }
        }

        private Decimal? _totUnit;

        [DataMember(Order=8)]
        public Decimal? TotUnit
        {
            get { return _totUnit; }
            set { _totUnit = value; }
        }

        private Decimal? _totAmt;

        [DataMember(Order=9)]
        public Decimal? TotAmt
        {
            get { return _totAmt; }
            set { _totAmt = value; }
        }

        private Decimal? _totFt;

        [DataMember(Order=10)]
        public Decimal? TotFt
        {
            get { return _totFt; }
            set { _totFt = value; }
        }

        private Decimal? _totVat;

        [DataMember(Order=11)]
        public Decimal? TotVat
        {
            get { return _totVat; }
            set { _totVat = value; }
        }

        private Decimal? _totGAmt;

        [DataMember(Order=12)]
        public Decimal? TotGAmt
        {
            get { return _totGAmt; }
            set { _totGAmt = value; }
        }

        private Decimal? _totModVat;

        [DataMember(Order=13)]
        public Decimal? TotModVat
        {
            get { return _totModVat; }
            set { _totModVat = value; }
        }

        private Decimal? _totDiscount;

        [DataMember(Order=14)]
        public Decimal? TotDiscount
        {
            get { return _totDiscount; }
            set { _totDiscount = value; }
        }

        private Decimal? _totGElecAmt;

        [DataMember(Order=15)]
        public Decimal? TotGElecAmt
        {
            get { return _totGElecAmt; }
            set { _totGElecAmt = value; }
        }

        private Decimal? _totFreeUnit;

        [DataMember(Order=16)]
        public Decimal? TotFreeUnit
        {
            get { return _totFreeUnit; }
            set { _totFreeUnit = value; }
        }

        private string _portionNo;

        [DataMember(Order=17)]
        public string PortionNo
        {
            get { return _portionNo; }
            set { _portionNo = value; }
        }

        private string _controllerId;

        [DataMember(Order=18)]
        public string ControllerId
        {
            get { return _controllerId; }
            set { _controllerId = value; }
        }

        private string _billFlag;

        [DataMember(Order=19)]
        public string BillFlag
        {
            get { return _billFlag; }
            set { _billFlag = value; }
        }

        private string _a4PrintFlag;

        [DataMember(Order=20)]
        public string A4PrintFlag
        {
            get { return _a4PrintFlag; }
            set { _a4PrintFlag = value; }
        }

        private string _blPrintFlag;

        [DataMember(Order=21)]
        public string BlPrintFlag
        {
            get { return _blPrintFlag; }
            set { _blPrintFlag = value; }
        }

        private string _grPrintFlag;

        [DataMember(Order=22)]
        public string GrPrintFlag
        {
            get { return _grPrintFlag; }
            set { _grPrintFlag = value; }
        }

        private string _typeFlag;

        [DataMember(Order=23)]
        public string TypeFlag
        {
            get { return _typeFlag; }
            set { _typeFlag = value; }
        }

        private string _mtNo;

        [DataMember(Order=24)]
        public string MtNo
        {
            get { return _mtNo; }
            set { _mtNo = value; }
        }

        private string _rootMru;

        [DataMember(Order=25)]
        public string RootMru
        {
            get { return _rootMru; }
            set { _rootMru = value; }
        }

        private string _paidBy;

        [DataMember(Order=26)]
        public string PaidBy
        {
            get { return _paidBy; }
            set { _paidBy = value; }
        }

        private string _bankId;

        [DataMember(Order=27)]
        public string BankId
        {
            get { return _bankId; }
            set { _bankId = value; }
        }

        private string _dueDate;

        [DataMember(Order=28)]
        public string DueDate
        {
            get { return _dueDate; }
            set { _dueDate = value; }
        }

        private DateTime? _modifiedDt;

        [DataMember(Order=29)]
        public DateTime? ModifiedDt
        {
            get { return _modifiedDt; }
            set { _modifiedDt = value; }
        }

        private string _modifiedBy;

        [DataMember(Order=30)]
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        private int? _active;

        [DataMember(Order=31)]
        public int? Active
        {
            get { return _active; }
            set { _active = value; }
        }

        private string _syncFlag;

        [DataMember(Order=32)]
        public string SyncFlag
        {
            get { return _syncFlag; }
            set { _syncFlag = value; }
        }

    }
}
