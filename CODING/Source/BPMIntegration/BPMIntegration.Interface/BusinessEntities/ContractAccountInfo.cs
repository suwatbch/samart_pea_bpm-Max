using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using System.Globalization;
using System.IO;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Integration.BPMIntegration.Interface.Constants;
using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
   [DataContract]
    public class ContractAccountInfo : IListUtility<ContractAccountInfo>, IComparable<ContractAccountInfo>
    {

       // string _caId;
       // public string CaId
       // {
       //     get { return this._caId; }
       //     set { this._caId = value; }
       // }

       // string _techBranchId;
       // public string TechBranchId
       // {
       //     get { return this._techBranchId; }
       //     set { this._techBranchId = value; }
       // }

       //string _commBranchId;
       // public string CommBranchId
       // {
       //     get { return this._commBranchId; }
       //     set { this._commBranchId = value; }
       // }

       // string _mruId;
       // public string MruId
       // {
       //     get { return this._mruId; }
       //     set { this._mruId = value; }
       // }

       // string _bpId;
       // public string BpId
       // {
       //     get { return this._bpId; }
       //     set { this._bpId = value; }
       // }

       // string _caName;
       // public string CaName
       // {
       //     get { return this._caName; }
       //     set { this._caName = value; }
       // }

       // string _caAddress;
       // public string CaAddress
       // {
       //     get { return this._caAddress; }
       //     set { this._caAddress = value; }
       // }

       // string _ctId;
       // public string CtId
       // {
       //     get { return this._ctId; }
       //     set { this._ctId = value; }
       // }

       // string _pmId;
       // public string PmId
       // {
       //     get { return this._pmId; }
       //     set { this._pmId = value; }
       // }

       // string _accountClassId;
       // public string AccountClassId
       // {
       //     get { return this._accountClassId; }
       //     set { this._accountClassId = value; }
       // }

       // Decimal? _securityDeposit;
       // public Decimal? SecurityDeposit
       // {
       //     get { return this._securityDeposit; }
       //     set { this._securityDeposit = value; }
       // }

       // string _meterId;
       // public string MeterId
       // {
       //     get { return this._meterId; }
       //     set { this._meterId = value; }
       // }

       // string _meterSizeId;
       // public string MeterSizeId
       // {
       //     get { return this._meterSizeId; }
       //     set { this._meterSizeId = value; }
       // }

       // string _rateTypeId;
       // public string RateTypeId
       // {
       //     get { return this._rateTypeId; }
       //     set { this._rateTypeId = value; }
       // }

       // Decimal? _transportHelp;
       // public Decimal? TransportHelp
       // {
       //     get { return this._transportHelp; }
       //     set { this._transportHelp = value; }
       // }

        //decimal? _farLandHelp;
        //public decimal? FarLandHelp
        //{
        //    get { return this._farLandHelp; }
        //    set { this._farLandHelp = value; }
        //}

        //decimal? _extraMoneyHelp;
        //public decimal? ExtraMoneyHelp
        //{
        //    get { return this._extraMoneyHelp; }
        //    set { this._extraMoneyHelp = value; }
        //}

        //DateTime? _signContractDt;
        //public DateTime? SignContractDt
        //{
        //    get { return this._signContractDt; }
        //    set { this._signContractDt = value; }
        //}

       //string _paidBy;
       //public string PaidBy
       //{
       //    get { return this._paidBy; }
       //    set { this._paidBy = value; }
       //}

       // DateTime? _contractValidFrom;
       // public DateTime? ContractValidFrom
       // {
       //     get { return this._contractValidFrom; }
       //     set { this._contractValidFrom = value; }
       // }

       // DateTime? _contractValidTo;
       // public DateTime? ContractValidTo
       // {
       //     get { return this._contractValidTo; }
       //     set { this._contractValidTo = value; }
       // }

        //string _receiptType;
        //public string ReceiptType
        //{
        //    get { return _receiptType; }
        //    set { _receiptType = value; }
        //}

       // string _receiptPrintName;
       // public string ReceiptPrintName
       // {
       //     get { return this._receiptPrintName; }
       //     set { this._receiptPrintName = value; }
       // }

       // string _interestKey;
       // public string InterestKey
       // {
       //     get { return _interestKey; }
       //     set { _interestKey = value; }
       // }

       // string _controllerId;
       // public string ControllerId
       // {
       //     get { return _controllerId; }
       //     set { _controllerId = value; }
       // }

       // string _penaltyWaiveFlag;
       // public string PenaltyWaiveFlag
       // {
       //     get { return _penaltyWaiveFlag; }
       //     set { _penaltyWaiveFlag = value; }
       // }

       // string _action;
       // public string Action
       // {
       //     get { return this._action; }
       //     set { this._action = value; }
       // }

       // string _syncFlag;
       // public string SyncFlag
       // {
       //     get { return this._syncFlag; }
       //     set { this._syncFlag = value; }
       // }

       // string _modifiedBy;
       // public string ModifiedBy
       // {
       //     get { return this._modifiedBy; }
       //     set { this._modifiedBy = value; }
       // }

       // DateTime? _modifiedDt;
       // public DateTime? ModifiedDt
       // {
       //     get { return this._modifiedDt; }
       //     set { this._modifiedDt = value; }
       // }

       // bool _active;
       // public bool Active
       // {
       //     get { return this._active; }
       //     set { this._active = value; }
       // }

       //DateTime? _meterInstallDt;
       //public DateTime? MeterInstallDt
       //{
       //    get { return this._meterInstallDt; }
       //    set { this._meterInstallDt = value; }
       //}

        //string _collectArea;
        //public string CollectArea
        //{
        //    get { return this._collectArea; }
        //    set { this._collectArea = value; }
        //}

        private string _CaId;
        private string _TechBranchId;
        private string _CommBranchId;
        private string _MruId;
        private string _BpId;
        private string _CaName;
        private string _ReceiptPrintName;
        private string _CaAddress;
        private string _CtId;
        private string _PmId;
        private string _AccountClassId;
        private decimal? _SecurityDeposit;
        private string _InterestKey;
        private string _PaidBy;
        private DateTime? _ContractValidFrom;
        private DateTime? _ContractValidTo;
        private string _MeterSizeId;
        private DateTime? _MeterInstallDt;
        private string _ControllerId;
        private string _GroupIvReceiptType;
        private decimal? _TransportHelp;
        private string _PenaltyWaiveFlag;
        private string _SyncFlag;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        private string _caTaxId;
        private string _caTaxBranch;

        //--add for Import
        private string _collectArea;
        private decimal? _farLandHelp;
        private decimal? _extraMoneyHelp;
        private DateTime? _signContractDt;
        private string _receiptType;

        [DataMember(Order = 1)]
        public string CaId
        {
            get { return _CaId; }
            set { _CaId = value; }
        }
        [DataMember(Order = 2)]
        public string TechBranchId
        {
            get { return _TechBranchId; }
            set { _TechBranchId = value; }
        }
        [DataMember(Order = 3)]
        public string CommBranchId
        {
            get { return _CommBranchId; }
            set { _CommBranchId = value; }
        }
        [DataMember(Order = 4)]
        public string MruId
        {
            get { return _MruId; }
            set { _MruId = value; }
        }
        [DataMember(Order = 5)]
        public string BpId
        {
            get { return _BpId; }
            set { _BpId = value; }
        }
        [DataMember(Order = 6)]
        public string CaName
        {
            get { return _CaName; }
            set { _CaName = value; }
        }
        [DataMember(Order = 7)]
        public string ReceiptPrintName
        {
            get { return _ReceiptPrintName; }
            set { _ReceiptPrintName = value; }
        }
        [DataMember(Order = 8)]
        public string CaAddress
        {
            get { return _CaAddress; }
            set { _CaAddress = value; }
        }
        [DataMember(Order = 9)]
        public string CtId
        {
            get { return _CtId; }
            set { _CtId = value; }
        }
        [DataMember(Order = 10)]
        public string PmId
        {
            get { return _PmId; }
            set { _PmId = value; }
        }
        [DataMember(Order = 11)]
        public string AccountClassId
        {
            get { return _AccountClassId; }
            set { _AccountClassId = value; }
        }
        [DataMember(Order = 12)]
        public decimal? SecurityDeposit
        {
            get { return _SecurityDeposit; }
            set { _SecurityDeposit = value; }
        }
        [DataMember(Order = 13)]
        public string InterestKey
        {
            get { return _InterestKey; }
            set { _InterestKey = value; }
        }
        [DataMember(Order = 14)]
        public string PaidBy
        {
            get { return _PaidBy; }
            set { _PaidBy = value; }
        }
        [DataMember(Order = 15)]
        public DateTime? ContractValidFrom
        {
            get { return _ContractValidFrom; }
            set { _ContractValidFrom = value; }
        }
        [DataMember(Order = 16)]
        public DateTime? ContractValidTo
        {
            get { return _ContractValidTo; }
            set { _ContractValidTo = value; }
        }
        [DataMember(Order = 17)]
        public string MeterSizeId
        {
            get { return _MeterSizeId; }
            set { _MeterSizeId = value; }
        }
        [DataMember(Order = 18)]
        public DateTime? MeterInstallDt
        {
            get { return _MeterInstallDt; }
            set { _MeterInstallDt = value; }
        }
        [DataMember(Order = 19)]
        public string ControllerId
        {
            get { return _ControllerId; }
            set { _ControllerId = value; }
        }
        [DataMember(Order = 20)]
        public string GroupIvReceiptType
        {
            get { return _GroupIvReceiptType; }
            set { _GroupIvReceiptType = value; }
        }
        [DataMember(Order = 21)]
        public decimal? TransportHelp
        {
            get { return _TransportHelp; }
            set { _TransportHelp = value; }
        }
        [DataMember(Order = 22)]
        public string PenaltyWaiveFlag
        {
            get { return _PenaltyWaiveFlag; }
            set { _PenaltyWaiveFlag = value; }
        }
        [DataMember(Order = 23)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 24)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 25)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 26)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 27)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }
        //add for Import
        [DataMember(Order = 28)]
        public string CollectArea
        {
            get { return this._collectArea; }
            set { this._collectArea = value; }
        }

        [DataMember(Order = 29)]
        public decimal? FarLandHelp
        {
            get { return this._farLandHelp; }
            set { this._farLandHelp = value; }
        }

        [DataMember(Order = 30)]
        public decimal? ExtraMoneyHelp
        {
            get { return this._extraMoneyHelp; }
            set { this._extraMoneyHelp = value; }
        }

        [DataMember(Order = 31)]
        public DateTime? SignContractDt
        {
            get { return this._signContractDt; }
            set { this._signContractDt = value; }
        }

        [DataMember(Order = 32)]
        public string ReceiptType
        {
            get { return _receiptType; }
            set { _receiptType = value; }
        }

        [DataMember(Order = 33)]
        public string CaTaxId
        {
            get { return this._caTaxId; }
            set { this._caTaxId = value; }
        }

        [DataMember(Order = 34)]
        public string CaTaxBranch
        {
            get { return _caTaxBranch; }
            set { _caTaxBranch = value; }
        }

        #region IListUtility<ContractAccountInfo> Members

        public string ToStream()
        {
            //IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("th-TH");
            //string[] template = { CaId, TechBranchId, CommBranchId, MruId, BpId,  CaName, CaAddress, CtId, PmId,
            //                        AccountClassId, SecurityDeposit.ToString(), MeterId, MeterSizeId,
            //                        RateTypeId, TransportHelp.Value.ToString(), FarLandHelp.Value.ToString(), 
            //                        ExtraMoneyHelp.Value.ToString(), SignContractDt.Value.ToString("dd/MM/yyyy", formatDate),
            //                        PaidBy, ContractValidFrom.Value.ToString("dd/MM/yyyy", formatDate), 
            //                        ContractValidTo.Value.ToString("dd/MM/yyyy", formatDate),  Action};
            //return string.Join("\t", template);
            throw new Exception("The Method is not implemented yet");
        }
       
        public ContractAccountInfo ParseExtract(string txt)
        {
            IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("en-EN");
            ContractAccountInfo it = new ContractAccountInfo();
            string[] sp = txt.Split('|');

            int colFixed = InterfaceColumns.ContractAccount;
            int colCounted = sp.Length - 1;
            if (colCounted != colFixed)
                throw new Exception("ไม่สามารถทำรายการได้ เนื่องจาก Columns ใน text file มีจำนวน " + colCounted.ToString() + " แต่ Columns ที่กำหนดไว้มีจำนวน " + colFixed.ToString());
            
            it.CaId = StringConvert.ToString(sp[1].Trim());
            it.BpId = StringConvert.ToString(sp[2].Trim());
            it.TechBranchId = StringConvert.ToString(sp[3].Trim());
            it.CommBranchId = StringConvert.ToString(sp[4].Trim());

            string tmp = StringConvert.ToString(sp[5].Trim());
            if (tmp != null)
                if (tmp.Length >= 4)
                    it.MruId = tmp.Substring(tmp.Length - 4, 4);
            
            it.CaName = StringConvert.ToString(sp[6].Trim());
            it.CaAddress = StringConvert.ToString(sp[7].Trim());
            it.CtId = StringConvert.ToString(sp[8].Trim());
            it.PmId = StringConvert.ToString(sp[9].Trim());
            it.AccountClassId = StringConvert.ToString(sp[10].Trim());
            it.SecurityDeposit = StringConvert.ToDecimal(sp[11].Trim());
            it.MeterInstallDt = StringConvert.ToDateTime(sp[12].Trim());
            it.MeterSizeId = StringConvert.ToString(sp[13].Trim());
            it.CollectArea = StringConvert.ToString(sp[14].Trim());
            it.PaidBy = StringConvert.ToString(sp[15].Trim());
            it.TransportHelp = StringConvert.ToDecimal(sp[16].Trim());
            it.PenaltyWaiveFlag = StringConvert.ToString(sp[17].Trim());
            it.FarLandHelp = StringConvert.ToDecimal(sp[18].Trim());
            it.ExtraMoneyHelp = StringConvert.ToDecimal(sp[19].Trim());
            it.SignContractDt = StringConvert.ToDateTime(sp[20].Trim());
            it.ContractValidFrom = StringConvert.ToDateTime(sp[21].Trim());
            it.ContractValidTo = StringConvert.ToDateTime(sp[22].Trim());
            it.ReceiptType = StringConvert.ToString(sp[23].Trim());
            it.ReceiptPrintName = StringConvert.ToString(sp[24].Trim());
            it.InterestKey = StringConvert.ToString(sp[25].Trim());
            it.ControllerId = StringConvert.ToString(sp[26].Trim());

            string cancelFlag = StringConvert.ToString(sp[27].Trim());
            if (cancelFlag == "1")
                it.Action = "3";
            else
                it.Action = StringConvert.ToString(sp[28].Trim());

            //Tax13
            it.CaTaxId = StringConvert.ToString(sp[29].Trim());
            it.CaTaxBranch = StringConvert.ToString(sp[30].Trim());

            if (it.Action != "O" && it.Action != "0" && it.Action != "1" && it.Action != "2" && it.Action != "3")
                throw new Exception("ไม่สามารถทำรายการได้ เนื่องจากข้อมูลของ [Action] มีค่าเท่ากับ " + it.Action + "] ซึ่งไม่ตรงตามรูปแบบที่กำหนดไว้");

            it.ModifiedBy = "BATCH";
            it.ModifiedDt = DateTime.Now;
            it.SyncFlag = "1";
            it.Active = true;

            
            return it;            
        }

        public int CompareTo(ContractAccountInfo other)
        {
            return DateTime.Compare(this.ModifiedDt.Value, other.ModifiedDt.Value);
        }

        #endregion
    }
}

