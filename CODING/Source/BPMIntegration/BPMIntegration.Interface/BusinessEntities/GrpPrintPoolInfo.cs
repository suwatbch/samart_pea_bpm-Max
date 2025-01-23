using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]    
    public class GrpPrintPoolInfo
    {
        private string _PrintDoc;
        private int? _PrintType;
        private string _PrintBranch;
        private string _BranchId;
        private string _BranchName;
        private string _MruId;
        private string _CaId;
        private string _CaName;
        private string _BillPred;
        private string _ReceiptNo;
        private string _BankKey;
        private string _HouseBank;
        private string _BankName;
        private string _BankDueDate;
        private string _MtNo;
        private string _Payer;
        private string _PayerName;
        private string _GroupingDate;
        private string _GrpInvPaymentDueDate;
        private string _A4Addition;
        private int? _Reverse;
        private string _OrgDoc;
        private int? _PrintStatus;
        private DateTime? _PostDt;
        private string _PostBranchServerId;
        private string _SyncFlag;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string PrintDoc
        {
            get { return _PrintDoc; }
            set { _PrintDoc = value; }
        }
        [DataMember(Order = 2)]
        public int? PrintType
        {
            get { return _PrintType; }
            set { _PrintType = value; }
        }
        [DataMember(Order = 3)]
        public string PrintBranch
        {
            get { return _PrintBranch; }
            set { _PrintBranch = value; }
        }
        [DataMember(Order = 4)]
        public string BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }
        [DataMember(Order = 5)]
        public string BranchName
        {
            get { return _BranchName; }
            set { _BranchName = value; }
        }
        [DataMember(Order = 6)]
        public string MruId
        {
            get { return _MruId; }
            set { _MruId = value; }
        }
        [DataMember(Order = 7)]
        public string CaId
        {
            get { return _CaId; }
            set { _CaId = value; }
        }
        [DataMember(Order = 8)]
        public string CaName
        {
            get { return _CaName; }
            set { _CaName = value; }
        }
        [DataMember(Order = 9)]
        public string BillPred
        {
            get { return _BillPred; }
            set { _BillPred = value; }
        }
        [DataMember(Order = 10)]
        public string ReceiptNo
        {
            get { return _ReceiptNo; }
            set { _ReceiptNo = value; }
        }
        [DataMember(Order = 11)]
        public string BankKey
        {
            get { return _BankKey; }
            set { _BankKey = value; }
        }
        [DataMember(Order = 12)]
        public string HouseBank
        {
            get { return _HouseBank; }
            set { _HouseBank = value; }
        }
        [DataMember(Order = 13)]
        public string BankName
        {
            get { return _BankName; }
            set { _BankName = value; }
        }
        [DataMember(Order = 14)]
        public string BankDueDate
        {
            get { return _BankDueDate; }
            set { _BankDueDate = value; }
        }
        [DataMember(Order = 15)]
        public string MtNo
        {
            get { return _MtNo; }
            set { _MtNo = value; }
        }
        [DataMember(Order = 16)]
        public string Payer
        {
            get { return _Payer; }
            set { _Payer = value; }
        }
        [DataMember(Order = 17)]
        public string PayerName
        {
            get { return _PayerName; }
            set { _PayerName = value; }
        }
        [DataMember(Order = 18)]
        public string GroupingDate
        {
            get { return _GroupingDate; }
            set { _GroupingDate = value; }
        }
        [DataMember(Order = 19)]
        public string GrpInvPaymentDueDate
        {
            get { return _GrpInvPaymentDueDate; }
            set { _GrpInvPaymentDueDate = value; }
        }
        [DataMember(Order = 20)]
        public string A4Addition
        {
            get { return _A4Addition; }
            set { _A4Addition = value; }
        }
        [DataMember(Order = 21)]
        public int? Reverse
        {
            get { return _Reverse; }
            set { _Reverse = value; }
        }
        [DataMember(Order = 22)]
        public string OrgDoc
        {
            get { return _OrgDoc; }
            set { _OrgDoc = value; }
        }
        [DataMember(Order = 23)]
        public int? PrintStatus
        {
            get { return _PrintStatus; }
            set { _PrintStatus = value; }
        }
        [DataMember(Order = 24)]
        public DateTime? PostDt
        {
            get { return _PostDt; }
            set { _PostDt = value; }
        }
        [DataMember(Order = 25)]
        public string PostBranchServerId
        {
            get { return _PostBranchServerId; }
            set { _PostBranchServerId = value; }
        }
        [DataMember(Order = 26)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 27)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 28)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 29)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 30)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

    }
}
