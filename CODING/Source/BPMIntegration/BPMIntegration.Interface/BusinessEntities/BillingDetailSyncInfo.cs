using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [Serializable]
    public class BillingDetailSyncInfo
    {
        private string _invoiceNo;
        public string InvoiceNo
        {
            get { return _invoiceNo; }
            set { _invoiceNo = value; }
        }

        private string _syncFlag;
        public string SyncFlag
        {
            get { return _syncFlag; }
            set { _syncFlag = value; }
        }

        private string _modifiedBy;
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        private DateTime? _modifiedDt;
        public DateTime? ModifiedDt
        {
            get { return _modifiedDt; }
            set { _modifiedDt = value; }
        }
    
        private bool _active;
        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        private string _a4PrintStatus;
        public string A4PrintStatus
        {
            get { return _a4PrintStatus; }
            set { _a4PrintStatus = value;}
        }

        private string _bluePrintStatus;
        public string BluePrintStatus
        {
            get { return _bluePrintStatus; }
            set { _bluePrintStatus = value; }
        }

        private string _greenPrintStatus;
        public string GreenPrintStatus
        {
            get { return _greenPrintStatus; }
            set { _greenPrintStatus = value; }
        }

        private string _grpInvPrintStatus;
        public string GrpInvPrintStatus
        {
            get { return _grpInvPrintStatus; }
            set { _grpInvPrintStatus = value; }
        }

        private string _blueBankPrintStatus;
        public string BlueBankPrintStatus
        {
            get { return _blueBankPrintStatus; }
            set { _blueBankPrintStatus = value; }
        }
    }
}
