using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using System.Text;

namespace PEA.BPM.EcsModule.Interface.BusinessEntities
{
    [DataContract]
    public class AgentPayment
    {
        private string _agentId;
        private string _agentName;
        private string _agentAddr;
        private decimal? _gAmount;
        private string _prefix;
        private string _dtId;
        private string _pmId;
        private string _posId;
        private string _branchId;
        private string _ptId;
        private string _tranfAccNo;
        private DateTime? _tranfDt;
        private string _receiptType;
        private string _fileId;
        private decimal? _totalBillAmount;
        private DateTime? _postDt;
        private DateTime? _depositDt;
        private Bank _bank;
        private DateTime? _modifiedDt;
        private string _modifiedBy;
        private string _postBranchServerId;
        private bool _isSysData;

        public AgentPayment()
        {
            this._bank = new Bank();
        }


        [DataMember(Order=1)]
        public string AgentId
        {
            get { return this._agentId; }
            set { this._agentId = value; }
        }


        [DataMember(Order=2)]
        public string AgentName
        {
            get { return this._agentName; }
            set { this._agentName = value; }
        }


        [DataMember(Order=3)]
        public string AgentAddr
        {
            get { return this._agentAddr; }
            set { this._agentAddr = value; }
        }


        [DataMember(Order=4)]
        public decimal? GAmount
        {
            get { return this._gAmount; }
            set { this._gAmount = value; }
        }


        [DataMember(Order=5)]
        public string Prefix
        {
            get { return this._prefix; }
            set { this._prefix = value; }
        }


        [DataMember(Order=6)]
        public string DtId
        {
            get { return this._dtId; }
            set { this._dtId = value; }
        }


        [DataMember(Order=7)]
        public string PmId
        {
            get { return this._pmId; }
            set { this._pmId = value; }
        }


        [DataMember(Order=8)]
        public string PosId
        {
            get { return this._posId; }
            set { this._posId = value; }
        }


        [DataMember(Order=9)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }


        [DataMember(Order=10)]
        public string PtId
        {
            get { return this._ptId; }
            set { this._ptId = value; }
        }


        [DataMember(Order=11)]
        public string TranfAccNo
        {
            get { return this._tranfAccNo; }
            set { this._tranfAccNo = value; }
        }


        [DataMember(Order=12)]
        public DateTime? TranfDt
        {
            get { return this._tranfDt; }
            set { this._tranfDt = value; }
        }


        [DataMember(Order=13)]
        public string ReceiptType
        {
            get { return this._receiptType; }
            set { this._receiptType = value; }
        }


        [DataMember(Order=14)]
        public string FileId
        {
            get { return this._fileId; }
            set { this._fileId = value; }
        }


        [DataMember(Order=15)]
        public decimal? TotalBillAmount
        {
            get { return this._totalBillAmount; }
            set { this._totalBillAmount = value; }
        }


        [DataMember(Order=16)]
        public DateTime? DepositDt
        {
            get { return this._depositDt; }
            set { this._depositDt = value; }
        }


        [DataMember(Order=17)]
        public DateTime? PostDt
        {
            get { return this._postDt; }
            set { this._postDt = value; }
        }


        [DataMember(Order=18)]
        public Bank Banks
        {
            get { return this._bank; }
            set { this._bank = value; }
        }


        [DataMember(Order=19)]
        public string BankKey
        {
            get { return this._bank.BankKey; }
            set { this._bank.BankKey = value; }
        }


        [DataMember(Order=20)]
        public string BankName
        {
            get { return this._bank.BankName; }
            set { this._bank.BankName = value; }
        }


        [DataMember(Order=21)]
        public DateTime? ModifiedDt
        {
            get { return this._modifiedDt; }
            set { this._modifiedDt = value; }
        }


        [DataMember(Order=22)]
        public string ModifiedBy
        {
            get { return this._modifiedBy; }
            set { this._modifiedBy = value; }
        }


        [DataMember(Order=23)]
        public string PostBranchServerId
        {
            get { return this._postBranchServerId; }
            set { this._postBranchServerId = value; }
        }


        [DataMember(Order=24)]
        public bool IsSysData
        {
            get { return this._isSysData; }
            set { this._isSysData = value; }
        }
    }
}
