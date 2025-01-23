using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class ContractAccountInfo
    {

        string _caId;

        [DataMember(Order=1)]
        public string CaId
        {
            get { return this._caId; }
            set { this._caId = value; }
        }

        string _techBranchId;

        [DataMember(Order=2)]
        public string TechBranchId
        {
            get { return this._techBranchId; }
            set { this._techBranchId = value; }
        }

        string _commBranchId;

        [DataMember(Order=3)]
        public string CommBranchId
        {
            get { return this._commBranchId; }
            set { this._commBranchId = value; }
        }

        string _mruId;

        [DataMember(Order=4)]
        public string MruId
        {
            get { return this._mruId; }
            set { this._mruId = value; }
        }

        string _bpId;

        [DataMember(Order=5)]
        public string BpId
        {
            get { return this._bpId; }
            set { this._bpId = value; }
        }

        string _caName;

        [DataMember(Order=6)]
        public string CaName
        {
            get { return this._caName; }
            set { this._caName = value; }
        }

        string _caAddress;

        [DataMember(Order=7)]
        public string CaAddress
        {
            get { return this._caAddress; }
            set { this._caAddress = value; }
        }

        string _ctId;

        [DataMember(Order=8)]
        public string CtId
        {
            get { return this._ctId; }
            set { this._ctId = value; }
        }

        string _pmId;

        [DataMember(Order=9)]
        public string PmId
        {
            get { return this._pmId; }
            set { this._pmId = value; }
        }

        string _accountClassId;

        [DataMember(Order=10)]
        public string AccountClassId
        {
            get { return this._accountClassId; }
            set { this._accountClassId = value; }
        }

        Decimal? _securityDeposit;

        [DataMember(Order=11)]
        public Decimal? SecurityDeposit
        {
            get { return this._securityDeposit; }
            set { this._securityDeposit = value; }
        }

        string _meterSizeId;

        [DataMember(Order=12)]
        public string MeterSizeId
        {
            get { return this._meterSizeId; }
            set { this._meterSizeId = value; }
        }

        DateTime? _meterInstallDt;

        [DataMember(Order=13)]
        public DateTime? MeterInstallDt
        {
            get { return this._meterInstallDt; }
            set { this._meterInstallDt = value; }
        }

        string _collectArea;

        [DataMember(Order=14)]
        public string CollectArea
        {
            get { return this._collectArea; }
            set { this._collectArea = value; }
        }
        
        string _meterId;

        [DataMember(Order=15)]
        public string MeterId
        {
            get { return this._meterId; }
            set { this._meterId = value; }
        }

        string _rateTypeId;

        [DataMember(Order=16)]
        public string RateTypeId
        {
            get { return this._rateTypeId; }
            set { this._rateTypeId = value; }
        }

        string _interestKey;

        [DataMember(Order=17)]
        public string InterestKey
        {
            get { return _interestKey; }
            set { _interestKey = value; }
        }

        Decimal? _transportHelp;

        [DataMember(Order=18)]
        public Decimal? TransportHelp
        {
            get { return this._transportHelp; }
            set { this._transportHelp = value; }
        }

        decimal? _farLandHelp;

        [DataMember(Order=19)]
        public decimal? FarLandHelp
        {
            get { return this._farLandHelp; }
            set { this._farLandHelp = value; }
        }

        decimal? _extraMoneyHelp;

        [DataMember(Order=20)]
        public decimal? ExtraMoneyHelp
        {
            get { return this._extraMoneyHelp; }
            set { this._extraMoneyHelp = value; }
        }

        DateTime? _signContractDt;

        [DataMember(Order=21)]
        public DateTime? SignContractDt
        {
            get { return this._signContractDt; }
            set { this._signContractDt = value; }
        }

        string _paidBy;

        [DataMember(Order=22)]
        public string PaidBy
        {
            get { return this._paidBy; }
            set { this._paidBy = value; }
        }

        DateTime? _contractValidFrom;

        [DataMember(Order=23)]
        public DateTime? ContractValidFrom
        {
            get { return this._contractValidFrom; }
            set { this._contractValidFrom = value; }
        }

        DateTime? _contractValidTo;

        [DataMember(Order=24)]
        public DateTime? ContractValidTo
        {
            get { return this._contractValidTo; }
            set { this._contractValidTo = value; }
        }

        string _receiptType;

        [DataMember(Order=25)]
        public string ReceiptType
        {
            get { return _receiptType; }
            set { _receiptType = value; }
        }

        string _receiptPrintName;

        [DataMember(Order=26)]
        public string ReceiptPrintName
        {
            get { return this._receiptPrintName; }
            set { this._receiptPrintName = value; }
        }   

        string _controllerId;

        [DataMember(Order=27)]
        public string ControllerId
        {
            get { return _controllerId; }
            set { _controllerId = value; }
        }

        string _penaltyWaiveFlag;

        [DataMember(Order=28)]
        public string PenaltyWaiveFlag
        {
            get { return _penaltyWaiveFlag; }
            set { _penaltyWaiveFlag = value; }
        }

        string _action;

        [DataMember(Order=29)]
        public string Action
        {
            get { return this._action; }
            set { this._action = value; }
        }

        string _syncFlag;

        [DataMember(Order=30)]
        public string SyncFlag
        {
            get { return this._syncFlag; }
            set { this._syncFlag = value; }
        }

        string _modifiedBy;

        [DataMember(Order=31)]
        public string ModifiedBy
        {
            get { return this._modifiedBy; }
            set { this._modifiedBy = value; }
        }

        DateTime? _modifiedDt;

        [DataMember(Order=32)]
        public DateTime? ModifiedDt
        {
            get { return this._modifiedDt; }
            set { this._modifiedDt = value; }
        }

        bool _active;

        [DataMember(Order=33)]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        string _fileType;

        [DataMember(Order=34)]
        public string FileType
        {
            get { return this._fileType; }
            set { this._fileType = value; }
        }

        string _caTaxId;

        [DataMember(Order = 35)]
        public string CaTaxId
        {
            get { return this._caTaxId; }
            set { this._caTaxId = value; }
        }

        string _caTaxBranch;

        [DataMember(Order = 36)]
        public string CaTaxBranch
        {
            get { return this._caTaxBranch; }
            set { this._caTaxBranch = value; }
        }
    }
}
