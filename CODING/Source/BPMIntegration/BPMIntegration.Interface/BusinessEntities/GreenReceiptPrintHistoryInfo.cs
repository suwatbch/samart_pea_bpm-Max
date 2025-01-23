using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [Serializable]
    public class GreenReceiptPrintHistoryInfo
    {
        private string _receiptNo;
        public string ReceiptNo
        {
            get { return _receiptNo; }
            set { _receiptNo = value; }
        }

        private string _printBranch;
        public string PrintBranch
        {
            get { return _printBranch; }
            set { _printBranch = value; }
        }

        private int? _printLog;
        public int? PrintLog
        {
            get { return _printLog; }
            set { _printLog = value; }
        }

        private DateTime? _printDate;
        public DateTime? PrintDate
        {
            get { return _printDate; }
            set { _printDate = value; }
        }

        private int? _billSeqNo;
        public int? BillSeqNo
        {
            get { return _billSeqNo; }
            set { _billSeqNo = value; }
        }

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

        private string _receiptPeriod;
        public string ReceiptPeriod
        {
            get { return _receiptPeriod; }
            set { _receiptPeriod = value; }
        }

        private string _caId;
        public string CaId
        {
            get { return _caId; }
            set { _caId = value; }
        }

        private string _bankId;
        public string BankId
        {
            get { return _bankId; }
            set { _bankId = value; }
        }

        private string _orgDoc;
        public string OrgDoc
        {
            get { return _orgDoc; }
            set { _orgDoc = value; }
        }

        private DateTime? _postDt;
        public DateTime? PostDt
        {
            get { return _postDt; }
            set { _postDt = value; }
        }

        private string _postBranchServerId;
        public string PostBranchServerId
        {
            get { return _postBranchServerId; }
            set { _postBranchServerId = value; }
        }

        private string _syncFlag;
        public string SyncFlag
        {
            get { return _syncFlag; }
            set { _syncFlag = value; }
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

        private bool? _active;
        public bool? Active
        {
            get { return _active; }
            set { _active = value; }
        }

    }
}
