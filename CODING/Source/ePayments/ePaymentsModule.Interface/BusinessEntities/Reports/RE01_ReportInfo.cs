using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports
{
    [DataContract]
    public class RE01_ReportInfo
    {
        private string _branchId;
        private string _mruId;
        private string _caId;
        private string _period;
        private string _caName;
        private decimal? _bpmGAmount;
        private decimal? _bpmVatAmount;
        private decimal? _outSourceAmount;
        private decimal? _vatAmount;
        private decimal? _debtBalance;
        private string _uploadStatus;
        private string _payDt;
        private string _recNo;
        private string _refDocNo;
        private string _fixedType;
        private int? _totalUpload;
        private decimal? _totalAmount;
        private decimal? _totalVatAmount;


        [DataMember(Order=1)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }


        [DataMember(Order=2)]
        public string MruId
        {
            get { return this._mruId; }
            set { this._mruId = value; }
        }


        [DataMember(Order=3)]
        public string CaId
        {
            get { return this._caId; }
            set { this._caId = value; }
        }


        [DataMember(Order=4)]
        public string Period
        {
            get { return this._period; }
            set { this._period = value; }
        }


        [DataMember(Order=5)]
        public string CaName
        {
            get { return this._caName; }
            set { this._caName = value; }
        }


        [DataMember(Order=6)]
        public decimal? BpmGAmount
        {
            get { return this._bpmGAmount; }
            set { this._bpmGAmount = value; }
        }


        [DataMember(Order=7)]
        public decimal? BpmVatAmount
        {
            get { return this._bpmVatAmount; }
            set { this._bpmVatAmount = value; }
        }


        [DataMember(Order=8)]
        public decimal? OutSourceAmount
        {
            get { return this._outSourceAmount; }
            set { this._outSourceAmount = value; }
        }


        [DataMember(Order=9)]
        public decimal? VatAmount
        {
            get { return this._vatAmount; }
            set { this._vatAmount = value; }
        }


        [DataMember(Order=10)]
        public decimal? DebtBalance
        {
            get { return this._debtBalance; }
            set { this._debtBalance = value; }
        }


        [DataMember(Order=11)]
        public string UploadStatus
        {
            get { return this._uploadStatus; }
            set { this._uploadStatus = value; }
        }


        [DataMember(Order=12)]
        public string PayDt
        {
            get { return this._payDt; }
            set { this._payDt = value; }
        }


        [DataMember(Order=13)]
        public string RecNo
        {
            get { return this._recNo; }
            set { this._recNo = value; }
        }


        [DataMember(Order=14)]
        public string RefDocNo
        {
            get { return this._refDocNo; }
            set { this._refDocNo = value; }
        }


        [DataMember(Order=15)]
        public string FixedType
        {
            get { return this._fixedType; }
            set { this._fixedType = value; }
        }


        [DataMember(Order=16)]
        public int? TotalUpload
        {
            get { return this._totalUpload; }
            set { this._totalUpload = value; }
        }


        [DataMember(Order=17)]
        public decimal? TotalAmount
        {
            get { return this._totalAmount; }
            set { this._totalAmount = value; }
        }


        [DataMember(Order=18)]
        public decimal? TotalVatAmount
        {
            get { return this._totalVatAmount; }
            set { this._totalVatAmount = value; }
        }

    }
}
