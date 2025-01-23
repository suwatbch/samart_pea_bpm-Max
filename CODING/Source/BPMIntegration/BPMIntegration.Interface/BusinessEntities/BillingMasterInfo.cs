using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [Serializable]
    public class BillingMasterInfo
    {
        private string _branchId;
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }

        private string _mruId;
        public string MruId
        {
            get { return _mruId; }
            set { _mruId = value; }
        }

        private string _billPred;
        public string BillPred
        {
            get { return _billPred; }
            set { _billPred = value; }
        }

        private int? _billPred_Log;
        public int? BillPred_Log
        {
            get { return _billPred_Log; }
            set { _billPred_Log = value; }
        }

        private string _invoiceFrom;
        public string InvoiceFrom
        {
            get { return _invoiceFrom; }
            set { _invoiceFrom = value; }
        }

        private string _invoiceTo;
        public string InvoiceTo
        {
            get { return _invoiceTo; }
            set { _invoiceTo = value; }
        }

        private int? _totBill;
        public int? TotBill
        {
            get { return _totBill; }
            set { _totBill = value; }
        }

        private Decimal? _totUnit;
        public Decimal? TotUnit
        {
            get { return _totUnit; }
            set { _totUnit = value; }
        }

        private Decimal? _totAmt;
        public Decimal? TotAmt
        {
            get { return _totAmt; }
            set { _totAmt = value; }
        }

        private Decimal? _totFt;
        public Decimal? TotFt
        {
            get { return _totFt; }
            set { _totFt = value; }
        }

        private Decimal? _totVat;
        public Decimal? TotVat
        {
            get { return _totVat; }
            set { _totVat = value; }
        }

        private Decimal? _totGAmt;
        public Decimal? TotGAmt
        {
            get { return _totGAmt; }
            set { _totGAmt = value; }
        }

        private Decimal? _totModVat;
        public Decimal? TotModVat
        {
            get { return _totModVat; }
            set { _totModVat = value; }
        }

        private Decimal? _totDiscount;
        public Decimal? TotDiscount
        {
            get { return _totDiscount; }
            set { _totDiscount = value; }
        }

        private Decimal? _totGElecAmt;
        public Decimal? TotGElecAmt
        {
            get { return _totGElecAmt; }
            set { _totGElecAmt = value; }
        }

        private Decimal? _totFreeUnit;
        public Decimal? TotFreeUnit
        {
            get { return _totFreeUnit; }
            set { _totFreeUnit = value; }
        }

        private string _portionNo;
        public string PortionNo
        {
            get { return _portionNo; }
            set { _portionNo = value; }
        }

        private string _controllerId;
        public string ControllerId
        {
            get { return _controllerId; }
            set { _controllerId = value; }
        }

        private string _billFlag;
        public string BillFlag
        {
            get { return _billFlag; }
            set { _billFlag = value; }
        }

        private string _a4PrintFlag;
        public string A4PrintFlag
        {
            get { return _a4PrintFlag; }
            set { _a4PrintFlag = value; }
        }

        private string _blPrintFlag;
        public string BlPrintFlag
        {
            get { return _blPrintFlag; }
            set { _blPrintFlag = value; }
        }

        private string _grPrintFlag;
        public string GrPrintFlag
        {
            get { return _grPrintFlag; }
            set { _grPrintFlag = value; }
        }

        private string _typeFlag;
        public string TypeFlag
        {
            get { return _typeFlag; }
            set { _typeFlag = value; }
        }

        private string _mtNo;
        public string MtNo
        {
            get { return _mtNo; }
            set { _mtNo = value; }
        }

        private string _rootMru;
        public string RootMru
        {
            get { return _rootMru; }
            set { _rootMru = value; }
        }

        private string _paidBy;
        public string PaidBy
        {
            get { return _paidBy; }
            set { _paidBy = value; }
        }

        private string _bankId;
        public string BankId
        {
            get { return _bankId; }
            set { _bankId = value; }
        }

        private string _dueDate;
        public string DueDate
        {
            get { return _dueDate; }
            set { _dueDate = value; }
        }

        private DateTime? _modifiedDt;
        public DateTime? ModifiedDt
        {
            get { return _modifiedDt; }
            set { _modifiedDt = value; }
        }

        private string _modifiedBy;
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        private bool _active;
        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        private string _syncFlag;
        public string SyncFlag
        {
            get { return _syncFlag; }
            set { _syncFlag = value; }
        }

    }
}
