using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities.ReceiptPrinting
{
    [DataContract]
    public class PPrintedDeposit
    {
        private DateTime? _uploadDt;
        private DateTime? _payDt;
        private DateTime? _fixedDt;
        private string _fixedType;
        private string _receiptId;
        private string _refDocNo;
        private string _invoiceNo;
        private string _companyId;
        private string _companyName;
        private string _companyAddr;
        private string _caId;
        private string _caName;
        private string _caAddress;
        private decimal? _gAmount;
        private decimal? _debtAmount;
        private string _branchId;

        private string _depositName;
        private string _depositBranch;
        private string _depositAddr;
        private string _depositDt;
        private string _depositMonth;
        private string _depositYear;
        private string _depositDetail;
        private int? _qty;
        private string _uploadDetail;
        private string _strAmount;
        private string _accountClass;


        [DataMember(Order=1)]
        public DateTime? UploadDt
        {
            get { return this._uploadDt; }
            set { this._uploadDt = value; }
        }


        [DataMember(Order=2)]
        public DateTime? PayDt
        {
            get { return this._payDt; }
            set { this._payDt = value; }
        }


        [DataMember(Order=3)]
        public DateTime? FixedDt
        {
            get { return this._fixedDt; }
            set { this._fixedDt = value; }
        }


        [DataMember(Order=4)]
        public string FixedType
        {
            get { return this._fixedType; }
            set { this._fixedType = value; }
        }


        [DataMember(Order=5)]
        public string ReceiptId
        {
            get { return this._receiptId; }
            set { this._receiptId = value; }
        }


        [DataMember(Order=6)]
        public string RefDocNo
        {
            get { return this._refDocNo; }
            set { this._refDocNo = value; }
        }


        [DataMember(Order=7)]
        public string InvoiceNo
        {
            get { return this._invoiceNo; }
            set { this._invoiceNo = value; }
        }


        [DataMember(Order=8)]
        public string CompanyId
        {
            get { return this._companyId; }
            set { this._companyId = value; }
        }


        [DataMember(Order=9)]
        public string CompanyName
        {
            get { return this._companyName; }
            set { this._companyName = value; }
        }


        [DataMember(Order=10)]
        public string CompanyAddr
        {
            get { return this._companyAddr; }
            set { this._companyAddr = value; }
        }


        [DataMember(Order=11)]
        public string CaId
        {
            get { return this._caId; }
            set { this._caId = value; }
        }


        [DataMember(Order=12)]
        public string CaName
        {
            get { return this._caName; }
            set { this._caName = value; }
        }


        [DataMember(Order=13)]
        public string CaAddress
        {
            get { return this._caAddress; }
            set { this._caAddress = value; }
        }


        [DataMember(Order=14)]
        public decimal? GAmount
        {
            get { return this._gAmount; }
            set { this._gAmount = value; }
        }


        [DataMember(Order=15)]
        public decimal? DebtAmount
        {
            get { return this._debtAmount; }
            set { this._debtAmount = value; }
        }


        [DataMember(Order=16)]
        public string DepositName
        {
            get { return this._depositName; }
            set { this._depositName = value; }
        }


        [DataMember(Order=17)]
        public string DepositBranch
        {
            get { return this._depositBranch; }
            set { this._depositBranch = value; }
        }


        [DataMember(Order=18)]
        public string DepositAddr
        {
            get { return this._depositAddr; }
            set { this._depositAddr = value; }
        }


        [DataMember(Order=19)]
        public string DepositDt
        {
            get { return this._depositDt; }
            set { this._depositDt = value; }
        }


        [DataMember(Order=20)]
        public string DepositMonth
        {
            get { return this._depositMonth; }
            set { this._depositMonth = value; }
        }


        [DataMember(Order=21)]
        public string DepositYear
        {
            get { return this._depositYear; }
            set { this._depositYear = value; }
        }


        [DataMember(Order=22)]
        public string DepositDetail
        {
            get { return this._depositDetail; }
            set { this._depositDetail = value; }
        }


        [DataMember(Order=23)]
        public Int32? Qty
        {
            get { return this._qty; }
            set { this._qty = value; }
        }


        [DataMember(Order=24)]
        public string UploadDetail
        {
            get { return this._uploadDetail; }
            set { this._uploadDetail = value; }
        }


        [DataMember(Order=25)]
        public string StrAmount
        {
            get { return this._strAmount; }
            set { this._strAmount = value; }
        }


        [DataMember(Order=26)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }


        [DataMember(Order=27)]
        public string AccountClass
        {
            get { return this._accountClass; }
            set { this._accountClass = value; }
        }
        
    }
}
