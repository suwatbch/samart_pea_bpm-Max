using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class CaAndBpInfo
    {
        BusinessPartnerInfo _bpObj;

        [DataMember(Order=1)]
        public BusinessPartnerInfo BpObj
        {
            get { return this._bpObj; }
            set { this._bpObj = value; }
        }

        ContractAccountInfo _caObj;

        [DataMember(Order=2)]
        public ContractAccountInfo CaObj
        {
            get { return this._caObj; }
            set { this._caObj = value; } 
        }

        [DataContract]
        public class BusinessPartnerInfo
        {
            CaAndBpInfo _obj1;

        [DataMember(Order=1)]
            public CaAndBpInfo Obj1
            {
                get { return _obj1; }
                set { this._obj1 = value; }
            }

            string _bpId;

        [DataMember(Order=2)]
            public string BpId
            {
                get { return this._bpId; }
                set { this._bpId = value; }
            }

            string _bpTypeId;

        [DataMember(Order=3)]
            public string BpTypeId
            {
                get { return this._bpTypeId; }
                set { this._bpTypeId = value; }
            }

            string _taxCode;

        [DataMember(Order=4)]
            public string TaxCode
            {
                get { return this._taxCode; }
                set { this._taxCode = value; }
            }

            string _citizenId;

        [DataMember(Order=5)]
            public string CitizenId
            {
                get { return this._citizenId; }
                set { this._citizenId = value; }
            }

            string _passportNo;

        [DataMember(Order=6)]
            public string PassportNo
            {
                get { return this._passportNo; }
                set { this._passportNo = value; }
            }

            string _registerId;

        [DataMember(Order=7)]
            public string RegisterId
            {
                get { return this._registerId; }
                set { this._registerId = value; }
            }

            string _vatCode;

        [DataMember(Order=8)]
            public string VatCode
            {
                get { return this._vatCode; }
                set { this._vatCode = value; }
            }

            string _syncFlag;

        [DataMember(Order=9)]
            public string SyncFlag
            {
                get { return this._syncFlag; }
                set { this._syncFlag = value; }
            }

            DateTime? _modifiedDt;

        [DataMember(Order=10)]
            public DateTime? ModifiedDt
            {
                get { return this._modifiedDt; }
                set { this._modifiedDt = value; }
            }

            string _modifiedBy;

        [DataMember(Order=11)]
            public string ModifiedBy
            {
                get { return this._modifiedBy; }
                set { this._modifiedBy = value; }
            }

            bool _active;

        [DataMember(Order=12)]
            public bool Active
            {
                get { return this._active; }
                set { this._active = value; }
            }

            string _action;

        [DataMember(Order=13)]
            public string Action
            {
                get { return this._action; }
                set { this._action = value; }
            }
        }

        [DataContract]
        public class ContractAccountInfo
        {

            CaAndBpInfo _obj2;

        [DataMember(Order=1)]
            public CaAndBpInfo Obj2
            {
                get { return _obj2; }
                set { this._obj2 = value; }
            }

            string _caId;

        [DataMember(Order=2)]
            public string CaId
            {
                get { return this._caId; }
                set { this._caId = value; }
            }

            string _techBranchId;

        [DataMember(Order=3)]
            public string TechBranchId
            {
                get { return this._techBranchId; }
                set { this._techBranchId = value; }
            }

            string _commBranchId;

        [DataMember(Order=4)]
            public string CommBranchId
            {
                get { return this._commBranchId; }
                set { this._commBranchId = value; }
            }

            string _mruId;

        [DataMember(Order=5)]
            public string MruId
            {
                get { return this._mruId; }
                set { this._mruId = value; }
            }

            string _bpId;

        [DataMember(Order=6)]
            public string BpId
            {
                get { return this._bpId; }
                set { this._bpId = value; }
            }

            string _caName;

        [DataMember(Order=7)]
            public string CaName
            {
                get { return this._caName; }
                set { this._caName = value; }
            }

            string _caAddress;

        [DataMember(Order=8)]
            public string CaAddress
            {
                get { return this._caAddress; }
                set { this._caAddress = value; }
            }

            string _ctId;

        [DataMember(Order=9)]
            public string CtId
            {
                get { return this._ctId; }
                set { this._ctId = value; }
            }

            string _pmId;

        [DataMember(Order=10)]
            public string PmId
            {
                get { return this._pmId; }
                set { this._pmId = value; }
            }

            string _accountClassId;

        [DataMember(Order=11)]
            public string AccountClassId
            {
                get { return this._accountClassId; }
                set { this._accountClassId = value; }
            }

            Decimal? _securityDeposit;

        [DataMember(Order=12)]
            public Decimal? SecurityDeposit
            {
                get { return this._securityDeposit; }
                set { this._securityDeposit = value; }
            }

            string _meterId;

        [DataMember(Order=13)]
            public string MeterId
            {
                get { return this._meterId; }
                set { this._meterId = value; }
            }

            string _meterSizeId;

        [DataMember(Order=14)]
            public string MeterSizeId
            {
                get { return this._meterSizeId; }
                set { this._meterSizeId = value; }
            }

            string _rateTypeId;

        [DataMember(Order=15)]
            public string RateTypeId
            {
                get { return this._rateTypeId; }
                set { this._rateTypeId = value; }
            }

            Decimal? _transportHelp;

        [DataMember(Order=16)]
            public Decimal? TransportHelp
            {
                get { return this._transportHelp; }
                set { this._transportHelp = value; }
            }

            decimal? _farLandHelp;

        [DataMember(Order=17)]
            public decimal? FarLandHelp
            {
                get { return this._farLandHelp; }
                set { this._farLandHelp = value; }
            }

            decimal? _extraMoneyHelp;

        [DataMember(Order=18)]
            public decimal? ExtraMoneyHelp
            {
                get { return this._extraMoneyHelp; }
                set { this._extraMoneyHelp = value; }
            }

            DateTime? _signContractDt;

        [DataMember(Order=19)]
            public DateTime? SignContractDt
            {
                get { return this._signContractDt; }
                set { this._signContractDt = value; }
            }

            string _paidBy;

        [DataMember(Order=20)]
            public string PaidBy
            {
                get { return this._paidBy; }
                set { this._paidBy = value; }
            }

            DateTime? _contractValidFrom;

        [DataMember(Order=21)]
            public DateTime? ContractValidFrom
            {
                get { return this._contractValidFrom; }
                set { this._contractValidFrom = value; }
            }

            DateTime? _contractValidTo;

        [DataMember(Order=22)]
            public DateTime? ContractValidTo
            {
                get { return this._contractValidTo; }
                set { this._contractValidTo = value; }
            }

            string _receiptType;

        [DataMember(Order=23)]
            public string ReceiptType
            {
                get { return _receiptType; }
                set { _receiptType = value; }
            }

            string _receiptPrintName;

        [DataMember(Order=24)]
            public string ReceiptPrintName
            {
                get { return this._receiptPrintName; }
                set { this._receiptPrintName = value; }
            }

            string _interestKey;

        [DataMember(Order=25)]
            public string InterestKey
            {
                get { return _interestKey; }
                set { _interestKey = value; }
            }

            string _controllerId;

        [DataMember(Order=26)]
            public string ControllerId
            {
                get { return _controllerId; }
                set { _controllerId = value; }
            }

            string _penaltyWaiveFlag;

        [DataMember(Order=27)]
            public string PenaltyWaiveFlag
            {
                get { return _penaltyWaiveFlag; }
                set { _penaltyWaiveFlag = value; }
            }

            string _action;

        [DataMember(Order=28)]
            public string Action
            {
                get { return this._action; }
                set { this._action = value; }
            }

            string _syncFlag;

        [DataMember(Order=29)]
            public string SyncFlag
            {
                get { return this._syncFlag; }
                set { this._syncFlag = value; }
            }

            string _modifiedBy;

        [DataMember(Order=30)]
            public string ModifiedBy
            {
                get { return this._modifiedBy; }
                set { this._modifiedBy = value; }
            }

            DateTime? _modifiedDt;

        [DataMember(Order=31)]
            public DateTime? ModifiedDt
            {
                get { return this._modifiedDt; }
                set { this._modifiedDt = value; }
            }

            bool _active;

        [DataMember(Order=32)]
            public bool Active
            {
                get { return this._active; }
                set { this._active = value; }
            }

            DateTime? _meterInstallDt;

        [DataMember(Order=33)]
            public DateTime? MeterInstallDt
            {
                get { return this._meterInstallDt; }
                set { this._meterInstallDt = value; }
            }

            string _collectArea;

        [DataMember(Order=34)]
            public string CollectArea
            {
                get { return this._collectArea; }
                set { this._collectArea = value; }
            }
        }
    }
}
