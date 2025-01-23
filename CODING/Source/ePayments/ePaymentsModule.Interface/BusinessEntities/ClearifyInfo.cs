using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.ePaymentsModule.Interface.Constants;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities
{
    [DataContract]
    public class ClearifyInfo
    {
        Agency _agency;
        string _issueId = String.Empty;
        string _companyId = String.Empty;
        string _agentName = String.Empty;
        string _bpmBranchId = String.Empty;
        string _fileBranchId = String.Empty;
        string _bpmCaId = String.Empty;
        string _fileCaId = String.Empty;
        string _caName = String.Empty;
        string _bpmInvoiceNo = String.Empty;
        string _fileInvoiceNo = String.Empty;        
        string _bpmPeriod = String.Empty;
        string _filePeriod = String.Empty;
        DateTime? _uploadDt;
        DateTime? _fileDueDt;
        DateTime? _bpmDueDt;
        DateTime? _filePaymentDt;
        decimal _fileAmount = 0;
        decimal _bpmAmount = 0;
        decimal _fileVatAmount = 0;
        decimal _bpmVatAmount = 0;
        string _uploadStatus = String.Empty;



        [DataMember(Order=1)]
        public Agency agency
        {
            get { return this._agency; }
            set { this._agency = value; }
        }


        [DataMember(Order=2)]
        public string IssueId
        {
            get { return this._issueId; }
            set { this._issueId = value; }
        }


        [DataMember(Order=3)]
        public string CompanyId
        {
            get { return this._companyId; }
            set { this._companyId = value; }
        }

        public string AgencyName
        {
            get 
            {
                if (agency != null)
                {
                    return String.Format("{0}:{1}", CompanyId, agency.AgencyName);
                }
                else
                    return string.Empty;
            }
        }


        [DataMember(Order=4)]
        public string BPMBranchId
        {
            get { return this._bpmBranchId; }
            set { this._bpmBranchId = value; }
        }


        [DataMember(Order=5)]
        public string FileBranchId
        {
            get { return this._fileBranchId; }
            set { this._fileBranchId = value; }
        }


        [DataMember(Order=6)]
        public string BPMCaId
        {
            get { return this._bpmCaId; }
            set { this._bpmCaId = value; }
        }
        

        [DataMember(Order=7)]
        public string FileCaId
        {
            get { return this._fileCaId; }
            set { this._fileCaId = value; }
        }


        [DataMember(Order=8)]
        public string CaName
        {
            get { return this._caName; }
            set { this._caName = value; }
        }
        

        [DataMember(Order=9)]
        public string BPMInvoiceNo
        {
            get { return this._bpmInvoiceNo; }
            set { this._bpmInvoiceNo = value; }
        }

        public string DisplayBPMInvoiceNo
        {
            get
            {
                string retVal = String.Empty;
                if (BPMInvoiceNo != String.Empty)
                {
                    retVal = BPMInvoiceNo.Substring(4, 12);
                }
                return retVal;
            }
        }


        [DataMember(Order=10)]
        public string FileInvoiceNo
        {
            get { return this._fileInvoiceNo; }
            set { this._fileInvoiceNo = value; }
        }


        public string DisplayFileInvoiceNo
        {
            get
            {
                string retVal = String.Empty;
                if (FileInvoiceNo != String.Empty)
                {
                    retVal = FileInvoiceNo.Substring(4, 12);
                }
                return retVal; 
            }
        }


        [DataMember(Order=11)]
        public string BPMPeriod
        {
            get { return this._bpmPeriod; }
            set { this._bpmPeriod = value; }
 
        }


        [DataMember(Order=12)]
        public string FilePeriod
        {
            get { return this._filePeriod; }
            set { this._filePeriod = value; }
        }


        [DataMember(Order=13)]
        public DateTime? UploadDt
        {
            get { return this._uploadDt; }
            set { this._uploadDt = value; }
        }


        [DataMember(Order=14)]
        public DateTime? FileDueDt
        {
            get { return this._fileDueDt; }
            set { this._fileDueDt = value; }
        }


        [DataMember(Order=15)]
        public DateTime? BPMDueDt
        {
            get { return this._bpmDueDt; }
            set { this._bpmDueDt = value; }
        }


        [DataMember(Order=16)]
        public DateTime? FilePaymentDt
        {
            get { return this._filePaymentDt; }
            set { this._filePaymentDt = value; }
        }


        [DataMember(Order=17)]
        public decimal FileAmount
        {
            get { return this._fileAmount; }
            set { this._fileAmount = value; }
        }


        [DataMember(Order=18)]
        public decimal BPMAmount
        {
            get { return this._bpmAmount; }
            set { this._bpmAmount = value; }
        }


        [DataMember(Order=19)]
        public decimal FileVatAmount
        {
            get { return this._fileVatAmount; }
            set { this._fileVatAmount = value; }
        }


        [DataMember(Order=20)]
        public decimal BPMVatAmount
        {
            get { return this._bpmVatAmount; }
            set { this._bpmVatAmount = value; }
 
        }
        

        [DataMember(Order=21)]
        public string UploadStatus
        {
            get { return this._uploadStatus; }
            set { this._uploadStatus = value; }
        }


        //[DataMember(Order=25)]
        public string UploadStatusName
        {
            get 
            {
                return EPaymentUploadStatus.GetStatusMessage(UploadStatus);
            }
        }

    }
}
