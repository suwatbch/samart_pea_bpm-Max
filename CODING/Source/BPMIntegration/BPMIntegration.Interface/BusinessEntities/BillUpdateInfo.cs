using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [Serializable]
    public class BillUpdateInfo
    {
        private string _printDoc;

        public string PrintDoc
        {
            get { return _printDoc; }
            set { _printDoc = value; }
        }
        private string _bankId;

        public string BankId
        {
            get { return _bankId; }
            set { _bankId = value; }
        }
        private string _receiptNo;

        public string ReceiptNo
        {
            get { return _receiptNo; }
            set { _receiptNo = value; }
        }
        private string _bankDueDate;

        public string BankDueDate
        {
            get { return _bankDueDate; }
            set { _bankDueDate = value; }
        }
        private string _grpInvPaymentDueDate;

        public string GrpInvPaymentDueDate
        {
            get { return _grpInvPaymentDueDate; }
            set { _grpInvPaymentDueDate = value; }
        }
        private string _groupingDate;

        public string GroupingDate
        {
            get { return _groupingDate; }
            set { _groupingDate = value; }
        }

        private string _mtNo;

        public string MtNo
        {
            get { return _mtNo; }
            set { _mtNo = value; }
        }
        private string _caId;

        public string CaId
        {
            get { return _caId; }
            set { _caId = value; }
        }
        private string _crsg;

        public string Crsg
        {
            get { return _crsg; }
            set { _crsg = value; }
        }
        private string _trsg;

        public string Trsg
        {
            get { return _trsg; }
            set { _trsg = value; }
        }
        private string _mruId;

        public string MruId
        {
            get { return _mruId; }
            set { _mruId = value; }
        }
        private string _pmId;

        public string PmId
        {
            get { return _pmId; }
            set { _pmId = value; }
        }
        private Decimal? _totalAmt;

        public Decimal? TotalAmt
        {
            get { return _totalAmt; }
            set { _totalAmt = value; }
        }
        private string _fileName;

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }
        private DateTime? _modifiedDt;

        public DateTime? ModifiedDt
        {
            get { return _modifiedDt; }
            set { _modifiedDt = value; }
        }
        private string _action;

        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }
    }
}
