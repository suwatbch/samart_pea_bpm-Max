using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class BillBookInfo
    {
        private string _BillBookId;
        private string _BookHolderId;
        private string _BookHolderName;
        private int? _BookLot;
        private DateTime? _CreateDate;
        private decimal? _AdvPayAmount;
        private DateTime? _AdvPayDueDate;
        private DateTime? _ReturnDueDate;
        private DateTime? _CheckInDate;
        private string _BookPeriod;
        private int? _ReceiveCount;
        private string _BkId;
        private string _CreatedBy;
        private string _Note;
        private string _BsId;
        private string _AboId;
        private int? _TotalBillCollected;
        private int? _TotalBill;
        private decimal? _BookTotalAmount;
        private decimal? _BookPaidAmount;
        private decimal? _BaseAmount;
        private decimal? _FTAmount;
        private decimal? _VatAmount;
        private string _CreatedBranchId;
        private string _CreatedBranchName;
        private string _CollectArea;
        private DateTime? _ContractValidFrom;
        private decimal? _SecurityDeposit;
        private string _AccountClassId;
        private string _BpTypeId;
        private DateTime? _PostDt;
        private string _PostBranchServerId;
        private string _SyncFlag;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string BillBookId
        {
            get { return _BillBookId; }
            set { _BillBookId = value; }
        }
        [DataMember(Order = 2)]
        public string BookHolderId
        {
            get { return _BookHolderId; }
            set { _BookHolderId = value; }
        }
        [DataMember(Order = 3)]
        public string BookHolderName
        {
            get { return _BookHolderName; }
            set { _BookHolderName = value; }
        }
        [DataMember(Order = 4)]
        public int? BookLot
        {
            get { return _BookLot; }
            set { _BookLot = value; }
        }
        [DataMember(Order = 5)]
        public DateTime? CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }
        [DataMember(Order = 6)]
        public decimal? AdvPayAmount
        {
            get { return _AdvPayAmount; }
            set { _AdvPayAmount = value; }
        }
        [DataMember(Order = 7)]
        public DateTime? AdvPayDueDate
        {
            get { return _AdvPayDueDate; }
            set { _AdvPayDueDate = value; }
        }
        [DataMember(Order = 8)]
        public DateTime? ReturnDueDate
        {
            get { return _ReturnDueDate; }
            set { _ReturnDueDate = value; }
        }
        [DataMember(Order = 9)]
        public DateTime? CheckInDate
        {
            get { return _CheckInDate; }
            set { _CheckInDate = value; }
        }
        [DataMember(Order = 10)]
        public string BookPeriod
        {
            get { return _BookPeriod; }
            set { _BookPeriod = value; }
        }
        [DataMember(Order = 11)]
        public int? ReceiveCount
        {
            get { return _ReceiveCount; }
            set { _ReceiveCount = value; }
        }
        [DataMember(Order = 12)]
        public string BkId
        {
            get { return _BkId; }
            set { _BkId = value; }
        }
        [DataMember(Order = 13)]
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [DataMember(Order = 14)]
        public string Note
        {
            get { return _Note; }
            set { _Note = value; }
        }
        [DataMember(Order = 15)]
        public string BsId
        {
            get { return _BsId; }
            set { _BsId = value; }
        }
        [DataMember(Order = 16)]
        public string AboId
        {
            get { return _AboId; }
            set { _AboId = value; }
        }
        [DataMember(Order = 17)]
        public int? TotalBillCollected
        {
            get { return _TotalBillCollected; }
            set { _TotalBillCollected = value; }
        }
        [DataMember(Order = 18)]
        public int? TotalBill
        {
            get { return _TotalBill; }
            set { _TotalBill = value; }
        }
        [DataMember(Order = 19)]
        public decimal? BookTotalAmount
        {
            get { return _BookTotalAmount; }
            set { _BookTotalAmount = value; }
        }
        [DataMember(Order = 20)]
        public decimal? BookPaidAmount
        {
            get { return _BookPaidAmount; }
            set { _BookPaidAmount = value; }
        }
        [DataMember(Order = 21)]
        public decimal? BaseAmount
        {
            get { return _BaseAmount; }
            set { _BaseAmount = value; }
        }
        [DataMember(Order = 22)]
        public decimal? FTAmount
        {
            get { return _FTAmount; }
            set { _FTAmount = value; }
        }
        [DataMember(Order = 23)]
        public decimal? VatAmount
        {
            get { return _VatAmount; }
            set { _VatAmount = value; }
        }
        [DataMember(Order = 24)]
        public string CreatedBranchId
        {
            get { return _CreatedBranchId; }
            set { _CreatedBranchId = value; }
        }
        [DataMember(Order = 25)]
        public string CreatedBranchName
        {
            get { return _CreatedBranchName; }
            set { _CreatedBranchName = value; }
        }
        [DataMember(Order = 26)]
        public string CollectArea
        {
            get { return _CollectArea; }
            set { _CollectArea = value; }
        }
        [DataMember(Order = 27)]
        public DateTime? ContractValidFrom
        {
            get { return _ContractValidFrom; }
            set { _ContractValidFrom = value; }
        }
        [DataMember(Order = 28)]
        public decimal? SecurityDeposit
        {
            get { return _SecurityDeposit; }
            set { _SecurityDeposit = value; }
        }
        [DataMember(Order = 29)]
        public string AccountClassId
        {
            get { return _AccountClassId; }
            set { _AccountClassId = value; }
        }
        [DataMember(Order = 30)]
        public string BpTypeId
        {
            get { return _BpTypeId; }
            set { _BpTypeId = value; }
        }
        [DataMember(Order = 31)]
        public DateTime? PostDt
        {
            get { return _PostDt; }
            set { _PostDt = value; }
        }
        [DataMember(Order = 32)]
        public string PostBranchServerId
        {
            get { return _PostBranchServerId; }
            set { _PostBranchServerId = value; }
        }
        [DataMember(Order = 33)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 34)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 35)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 36)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 37)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }
    }
}
