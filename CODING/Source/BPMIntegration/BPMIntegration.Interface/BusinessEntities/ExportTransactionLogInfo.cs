using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class ExportTransactionLogInfo
    {
        private string _BranchId;
        private DateTime? _CreatedDt;
        private string _ExportType;
        private string _ItemId;
        private string _ARPmId;
        private string _ARPtId;
        private string _PaymentId;
        private string _ReceiptId;
        private string _APId;
        private string _ChqItemId;
        private int? _ExportFlag;
        private DateTime? _ExportDt;
        private string _WorkId;
        private DateTime? _CloseWorkDt;
        private string _SyncFlag;
        private string _PostBranchServerId;
        private DateTime? _PostDt;
        private DateTime? _ModifiedDt;

        [DataMember(Order = 1)]
        public string BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }
        [DataMember(Order = 2)]
        public DateTime? CreatedDt
        {
            get { return _CreatedDt; }
            set { _CreatedDt = value; }
        }
        [DataMember(Order = 3)]
        public string ExportType
        {
            get { return _ExportType; }
            set { _ExportType = value; }
        }
        [DataMember(Order = 4)]
        public string ItemId
        {
            get { return _ItemId; }
            set { _ItemId = value; }
        }
        [DataMember(Order = 5)]
        public string ARPmId
        {
            get { return _ARPmId; }
            set { _ARPmId = value; }
        }
        [DataMember(Order = 6)]
        public string ARPtId
        {
            get { return _ARPtId; }
            set { _ARPtId = value; }
        }
        [DataMember(Order = 7)]
        public string PaymentId
        {
            get { return _PaymentId; }
            set { _PaymentId = value; }
        }
        [DataMember(Order = 8)]
        public string ReceiptId
        {
            get { return _ReceiptId; }
            set { _ReceiptId = value; }
        }
        [DataMember(Order = 9)]
        public string APId
        {
            get { return _APId; }
            set { _APId = value; }
        }
        [DataMember(Order = 10)]
        public string ChqItemId
        {
            get { return _ChqItemId; }
            set { _ChqItemId = value; }
        }
        [DataMember(Order = 11)]
        public int? ExportFlag
        {
            get { return _ExportFlag; }
            set { _ExportFlag = value; }
        }
        [DataMember(Order = 12)]
        public DateTime? ExportDt
        {
            get { return _ExportDt; }
            set { _ExportDt = value; }
        }
        [DataMember(Order = 13)]
        public string WorkId
        {
            get { return _WorkId; }
            set { _WorkId = value; }
        }
        [DataMember(Order = 14)]
        public DateTime? CloseWorkDt
        {
            get { return _CloseWorkDt; }
            set { _CloseWorkDt = value; }
        }
        [DataMember(Order = 15)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 16)]
        public string PostBranchServerId
        {
            get { return _PostBranchServerId; }
            set { _PostBranchServerId = value; }
        }
        [DataMember(Order = 17)]
        public DateTime? PostDt
        {
            get { return _PostDt; }
            set { _PostDt = value; }
        }
        [DataMember(Order = 18)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
    }
}
