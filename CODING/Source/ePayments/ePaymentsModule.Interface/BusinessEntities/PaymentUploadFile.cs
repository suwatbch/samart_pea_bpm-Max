using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities
{
    [DataContract]
    public class PaymentUploadFile
    {
        private Agency _agency;
        private string _branchId;
        private string _caId;
        private string _caName;
        private string _invoiceNo;
        private string _receiveNo;
        private string _period;
        private string _receivePlace;
        private string _caAddress;
        private DateTime? _receiveDate;
        private DateTime? _dueDate;
        private decimal? _gAmount;
        private decimal? _vatAmount;
        private bool _status;
        
        private string _fileBranchId;
        private DateTime? _fileDueDate;
        private decimal? _fileGAmount;
        private string _fileInvoiceNo;
        private string _filePeriod;
        private decimal? _fileVatAmount;
        private string _errorTypeId;
        private string _errorType;

        public PaymentUploadFile()
        {
            _agency = new Agency();
        }


        [DataMember(Order=1)]
        public Agency Agency
        {
            get { return this._agency; }
            set { this._agency = value; }
        }


        [DataMember(Order=2)]
        public string AgencyId
        {
            get { return this._agency.AgencyId; }
            set { this._agency.AgencyId = value; }
        }


        [DataMember(Order=3)]
        public string AgencyName
        {
            get { return this._agency.AgencyName; }
            set { this._agency.AgencyName = value; }
        }


        [DataMember(Order=4)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }


        [DataMember(Order=5)]
        public string FileBranchId
        {
            get { return this._fileBranchId; }
            set { this._fileBranchId = value; }
        }


        [DataMember(Order=6)]
        public string CaId
        {
            get { return this._caId; }
            set { this._caId = value; }
        }


        [DataMember(Order=7)]
        public string InvoiceNo
        {
            get { return this._invoiceNo; }
            set { this._invoiceNo = value; }
        }


        [DataMember(Order=8)]
        public string FileInvoiceNo
        {
            get { return this._fileInvoiceNo; }
            set { this._fileInvoiceNo = value; }
        }


        [DataMember(Order=9)]
        public string ReceiveNo
        {
            get { return this._receiveNo; }
            set { this._receiveNo = value; }
        }


        [DataMember(Order=10)]
        public string Period
        {
            get { return this._period; }
            set { this._period = value; }
        }


        [DataMember(Order=11)]
        public string FilePeriod
        {
            get { return this._filePeriod; }
            set { this._filePeriod = value; }
        }


        [DataMember(Order=12)]
        public DateTime? DueDate
        {
            get { return this._dueDate; }
            set { this._dueDate = value; }
        }


        [DataMember(Order=13)]
        public DateTime? FileDueDate
        {
            get { return this._fileDueDate; }
            set { this._fileDueDate = value; }
        }


        [DataMember(Order=14)]
        public DateTime? ReceiveDate
        {
            get { return this._receiveDate; }
            set { this._receiveDate = value; }
        }


        [DataMember(Order=15)]
        public decimal? GAmount
        {
            get { return this._gAmount; }
            set { this._gAmount = value; }
        }


        [DataMember(Order=16)]
        public decimal? FileGAmount
        {
            get { return this._fileGAmount; }
            set { this._fileGAmount = value; }
        }


        [DataMember(Order=17)]
        public decimal? VatAmount
        {
            get { return this._vatAmount; }
            set { this._vatAmount = value; }
        }


        [DataMember(Order=18)]
        public decimal? FileVatAmount
        {
            get { return this._fileVatAmount; }
            set { this._fileVatAmount = value; }
        }


        [DataMember(Order=19)]
        public bool Status
        {
            get { return this._status; }
            set { this._status = value; }
        }


        [DataMember(Order=20)]
        public string CaName
        {
            get { return this._caName; }
            set { this._caName = value; }
        }


        [DataMember(Order=21)]
        public string CaAddress
        {
            get { return this._caAddress; }
            set { this._caAddress = value; }
        }


        [DataMember(Order=22)]
        public string ReceivePlace
        {
            get { return this._receivePlace; }
            set { this._receivePlace = value; }
        }


        [DataMember(Order=23)]
        public string ErrorTypeId
        {
            get { return this._errorTypeId; }
            set { this._errorTypeId = value; }
        }


        [DataMember(Order=24)]
        public string ErrorType
        {
            get { return this._errorType; }
            set { this._errorType = value; }
        }

    }
}
