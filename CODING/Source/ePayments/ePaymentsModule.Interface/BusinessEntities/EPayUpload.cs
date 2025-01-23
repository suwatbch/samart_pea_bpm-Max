using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities
{
    [DataContract]
    public class EPayUpload
    {
        private string _fileId;
        private string _fileName;
        private DateTime? _uploadDt;
        private Company _company;
        private int? _billCount;
        private decimal? _billAmount;
        private int? _totalBillCount;
        private decimal? _totalBillAmount;
        private string _syncFlag;
        private DateTime? _postDate;
        private string _postBranchId;
        private DateTime? _modifiedDt;
        private string _modifiedBy;
        private string _active;
        private string _PosId;

        public EPayUpload()
        {
            _company = new Company();
        }


        [DataMember(Order=1)]
        public string FileId
        {
            get { return this._fileId; }
            set { this._fileId = value; }
        }


        [DataMember(Order=2)]
        public string FileName
        {
            get { return this._fileName; }
            set { this._fileName = value; }
        }



        [DataMember(Order=3)]
        public DateTime? UploadDt
        {
            get { return this._uploadDt; }
            set { this._uploadDt = value; }
        }


        [DataMember(Order=4)]
        public Company Companys
        {
            get { return this._company; }
            set { this._company = value; }
        }


        [DataMember(Order=5)]
        public string CompanyId
        {
            get { return this._company.CompanyId; }
            set { this._company.CompanyId = value; }
        }


        [DataMember(Order=6)]
        public string CompanyName
        {
            get { return this._company.CompanyName; }
            set { this._company.CompanyName = value; }
        }


        [DataMember(Order=7)]
        public int? BillCount
        {
            get { return this._billCount; }
            set { this._billCount = value; }
        }


        [DataMember(Order=8)]
        public decimal? BillAmount
        {
            get { return this._billAmount; }
            set { this._billAmount = value; }
        }


        [DataMember(Order=9)]
        public int? TotalBillCount
        {
            get { return this._totalBillCount; }
            set { this._totalBillCount = value; }
        }


        [DataMember(Order=10)]
        public decimal? TotalBillAmount
        {
            get { return this._totalBillAmount; }
            set { this._totalBillAmount = value; }
        }


        [DataMember(Order=11)]
        public string SyncFlag
        {
            get { return this._syncFlag; }
            set { this._syncFlag = value; }
        }


        [DataMember(Order=12)]
        public DateTime? PostDt
        {
            get { return this._postDate; }
            set { this._postDate = value; }
        }


        [DataMember(Order=13)]
        public string PostBranchId
        {
            get { return this._postBranchId; }
            set { this._postBranchId = value; }
        }


        [DataMember(Order=14)]
        public DateTime? ModifiedDt
        {
            get { return this._modifiedDt; }
            set { this._modifiedDt = value; }
        }


        [DataMember(Order=15)]
        public string ModifiedBy
        {
            get { return this._modifiedBy; }
            set { this._modifiedBy = value; }
        }


        [DataMember(Order=16)]
        public string Active
        {
            get { return this._active; }
            set { this._active = value; }
        }


        [DataMember(Order=17)]
        public string PosId
        {
            get { return this._PosId; }
            set { this._PosId = value; }
        }
    }
}
