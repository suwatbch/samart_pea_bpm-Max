using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class BusinessPartnerInfo
    {
        string _bpId;

        [DataMember(Order=1)]
        public string BpId
        {
            get { return this._bpId; }
            set { this._bpId = value; }
        }

        string _bpTypeId;

        [DataMember(Order=2)]
        public string BpTypeId
        {
            get { return this._bpTypeId; }
            set { this._bpTypeId = value; }
        }

        string _taxCode;

        [DataMember(Order=3)]
        public string TaxCode
        {
            get { return this._taxCode; }
            set { this._taxCode = value; }
        }

        string _citizenId;

        [DataMember(Order=4)]
        public string CitizenId
        {
            get { return this._citizenId; }
            set { this._citizenId = value; }
        }

        string _passportNo;

        [DataMember(Order=5)]
        public string PassportNo
        {
            get { return this._passportNo; }
            set { this._passportNo = value; }
        }

        string _registerId;

        [DataMember(Order=6)]
        public string RegisterId
        {
            get { return this._registerId; }
            set { this._registerId = value; }
        }

        string _vatCode;

        [DataMember(Order=7)]
        public string VatCode
        {
            get { return this._vatCode; }
            set { this._vatCode = value; }
        }

        string _syncFlag;

        [DataMember(Order=8)]
        public string SyncFlag
        {
            get { return this._syncFlag; }
            set { this._syncFlag = value; }
        }

        DateTime? _modifiedDt;

        [DataMember(Order=9)]
        public DateTime? ModifiedDt
        {
            get { return this._modifiedDt; }
            set { this._modifiedDt = value; }
        }

        string _modifiedBy;

        [DataMember(Order=10)]
        public string ModifiedBy
        {
            get { return this._modifiedBy; }
            set { this._modifiedBy = value; }
        }

        bool _active;

        [DataMember(Order=11)]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        string _action;

        [DataMember(Order=12)]
        public string Action
        {
            get { return this._action; }
            set { this._action = value; }
        }

        string _fileType;

        [DataMember(Order=13)]
        public string FileType
        {
            get { return this._fileType; }
            set { this._fileType = value; }
        }
    }
}
