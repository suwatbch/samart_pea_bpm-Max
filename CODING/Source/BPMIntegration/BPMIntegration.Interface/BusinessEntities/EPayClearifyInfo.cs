using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class EPayClarifyInfo
    {
        private string _IssueId;
        private string _EPayItemId;
        private string _InvoiceNo;
        private string _NewInvoiceNo;
        private string _ReceiptInvoiceNo;
        private string _FixedType;
        private DateTime? _FixedDt;
        private string _FixedBy;
        private string _RefDocNo;
        private string _SyncFlag;
        private DateTime? _PostDt;
        private string _PostBranchServerId;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string IssueId
        {
            get { return _IssueId; }
            set { _IssueId = value; }
        }
        [DataMember(Order = 2)]
        public string EPayItemId
        {
            get { return _EPayItemId; }
            set { _EPayItemId = value; }
        }
        [DataMember(Order = 3)]
        public string InvoiceNo
        {
            get { return _InvoiceNo; }
            set { _InvoiceNo = value; }
        }
        [DataMember(Order = 4)]
        public string NewInvoiceNo
        {
            get { return _NewInvoiceNo; }
            set { _NewInvoiceNo = value; }
        }
        [DataMember(Order = 5)]
        public string ReceiptInvoiceNo
        {
            get { return _ReceiptInvoiceNo; }
            set { _ReceiptInvoiceNo = value; }
        }
        [DataMember(Order = 6)]
        public string FixedType
        {
            get { return _FixedType; }
            set { _FixedType = value; }
        }
        [DataMember(Order = 7)]
        public DateTime? FixedDt
        {
            get { return _FixedDt; }
            set { _FixedDt = value; }
        }
        [DataMember(Order = 8)]
        public string FixedBy
        {
            get { return _FixedBy; }
            set { _FixedBy = value; }
        }
        [DataMember(Order = 9)]
        public string RefDocNo
        {
            get { return _RefDocNo; }
            set { _RefDocNo = value; }
        }
        [DataMember(Order = 10)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 11)]
        public DateTime? PostDt
        {
            get { return _PostDt; }
            set { _PostDt = value; }
        }
        [DataMember(Order = 12)]
        public string PostBranchServerId
        {
            get { return _PostBranchServerId; }
            set { _PostBranchServerId = value; }
        }
        [DataMember(Order = 13)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 14)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 15)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 16)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

    }
}
