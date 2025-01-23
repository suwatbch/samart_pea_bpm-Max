using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities
{
    [DataContract]
    public class EpayUploadItem 
    {
        private string _ePayItemId;
        private EPayUpload _epayUpload;
        private string _branchId;
        private string _caId;
        private string _caName;
        private string _caAddress;
        private DateTime? _payDt;
        private DateTime? _dueDt;
        private decimal? _outsourceAmount;
        private decimal? _vatAmount;
        private string _recNo;
        private string _period;
        private string _actCode;
        private string _posNo;
        private string _invoiceNo;
        private string _uploadStatus;
        private string _syncFlag;
        private DateTime? _postDate;
        private string _postBranchId;
        private DateTime? _modifiedDt;
        private string _modifiedBy;
        private string _active;
        private string _pmId;

        private string _sysCaId;
        private string _sysBranchId;
        private DateTime? _sysDueDate;
        private decimal? _sysGAmount;
        private string _sysInvoiceNo;
        private string _sysPeriod;
        private decimal? _sysVatAmount;
        private string _sysPending;
        private string _syspmId;

        public EpayUploadItem()
        {
            _epayUpload = new EPayUpload();
        }


        public string CompanyInfo
        {
            get { return this.EpayUploads.CompanyId + ":" + this.EpayUploads.CompanyName; }
        }


        public string PayUploadFormat
        {
            get { return this.EpayUploads.CompanyId + ":" + this.BranchId + ":" + this.CaId + ":" + this.InvoiceNo; }
        }


        [DataMember(Order=3)]
        public string CompanyId
        {
            get { return this._epayUpload.CompanyId; }
            set { this._epayUpload.CompanyId = value; }
        }


        [DataMember(Order=4)]
        public string EpayItemId
        {
            get { return this._ePayItemId; }
            set { this._ePayItemId = value; }
        }


        [DataMember(Order=5)]
        public EPayUpload EpayUploads
        {
            get { return this._epayUpload; }
            set { this._epayUpload = value; }
        }


        [DataMember(Order=6)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }


        [DataMember(Order=7)]
        public string CaId
        {
            get { return this._caId; }
            set { this._caId = value; }
        }


        [DataMember(Order=8)]
        public string CaName
        {
            get { return this._caName; }
            set { this._caName = value; }
        }


        [DataMember(Order=9)]
        public string CaAddress
        {
            get { return this._caAddress; }
            set { this._caAddress = value; }
        }


        [DataMember(Order=10)]
        public DateTime? PayDt
        {
            get { return this._payDt; }
            set { this._payDt = value; }
        }


        [DataMember(Order=11)]
        public DateTime? DueDt
        {
            get { return this._dueDt; }
            set { this._dueDt = value; }
        }


        [DataMember(Order=12)]
        public decimal? OutSourceAmount
        {
            get { return this._outsourceAmount; }
            set { this._outsourceAmount = value; }
        }


        [DataMember(Order=13)]
        public decimal? VatAmount
        {
            get { return this._vatAmount; }
            set { this._vatAmount = value; }
        }


        [DataMember(Order=14)]
        public string  RecNo
        {
            get { return this._recNo; }
            set { this._recNo = value; }
        }


        [DataMember(Order=15)]
        public string Period
        {
            get { return this._period; }
            set { this._period = value; }
        }


        [DataMember(Order=16)]
        public string ActCode
        {
            get { return this._actCode; }
            set { this._actCode = value; }
        }


        [DataMember(Order=17)]
        public string PosNo
        {
            get { return this._posNo; }
            set { this._posNo = value; }
        }


        [DataMember(Order=18)]
        public string InvoiceNo
        {
            get { return this._invoiceNo; }
            set { this._invoiceNo = value; }
        }


        [DataMember(Order=19)]
        public string UploadStatus
        {
            get { return this._uploadStatus; }
            set { this._uploadStatus = value; }
        }


        [DataMember(Order=20)]
        public string SyncFlag
        {
            get { return this._syncFlag; }
            set { this._syncFlag = value; }
        }


        [DataMember(Order=21)]
        public DateTime? PostDt
        {
            get { return this._postDate; }
            set { this._postDate = value; }
        }


        [DataMember(Order=22)]
        public string PostBranchId
        {
            get { return this._postBranchId; }
            set { this._postBranchId = value; }
        }


        [DataMember(Order=23)]
        public DateTime? ModifiedDt
        {
            get { return this._modifiedDt; }
            set { this._modifiedDt = value; }
        }


        [DataMember(Order=24)]
        public string ModifiedBy
        {
            get { return this._modifiedBy; }
            set { this._modifiedBy = value; }
        }


        [DataMember(Order=25)]
        public string Active
        {
            get { return this._active; }
            set { this._active = value; }
        }


        [DataMember(Order=26)]
        public string SysCaId
        {
            get { return this._sysCaId; }
            set { this._sysCaId = value; }
        }


        [DataMember(Order=27)]
        public string SysBranchId
        {
            get { return this._sysBranchId; }
            set { this._sysBranchId = value; }
        }


        [DataMember(Order=28)]
        public string SysInvoiceNo
        {
            get { return this._sysInvoiceNo; }
            set { this._sysInvoiceNo = value; }
        }


        [DataMember(Order=29)]
        public string SysPeriod
        {
            get { return this._sysPeriod; }
            set { this._sysPeriod = value; }
        }


        [DataMember(Order=30)]
        public DateTime? SysDueDate
        {
            get { return this._sysDueDate; }
            set { this._sysDueDate = value; }
        }


        [DataMember(Order=31)]
        public decimal? SysGAmount
        {
            get { return this._sysGAmount; }
            set { this._sysGAmount = value; }
        }


        [DataMember(Order=32)]
        public decimal? SysVatAmount
        {
            get { return this._sysVatAmount; }
            set { this._sysVatAmount = value; }
        }


        [DataMember(Order=33)]
        public string SysPending
        {
            get { return this._sysPending; }
            set { this._sysPending = value; }
        }


        [DataMember(Order=34)]
        public string PmId
        {
            get { return this._pmId; }
            set { this._pmId = value; }
        }


        [DataMember(Order=35)]
        public string SysPmId
        {
            get { return this._syspmId; }
            set { this._syspmId = value; }
        }
    }
}
