using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class BillBookHeaderInfo
    {
        private int? _bookLot = 0;
        private string _branchId;
        private string _branchName;
        private string _printDate;
        private string _period;
        private byte? _receiveCount = 0;
        private DateTime? _advPayDt;
        private DateTime? _returnedDt;
        private string _agentId;
        private string _agentName;
        private string _agentStatus;
        private string _agentType;
        private decimal? _totalAsset = 0;
        private string _bkId;
        private string _billBookId; //created after saved
        private bool _isFirstPaid;
        private bool _isAgency; //otherwise it's employee
        private decimal? _advPayAmount = 0;
        private  bool _isPrintPreview;
        private bool _isPrintDetail;
        private string _createFailReason;
        private string _bookHolderBranchId;  //for employee book holder
        private bool _bookAmountOverAsset;
        private string _originalBillBookId;
       // private string _runningBranchId; // running branchId\

        //New added May, 16 for Reprint function        
        private bool _isPrintBillbook = true;

        private decimal? _totalBookAmount;              
        private int? _totalBillCount;

        //private string _controllerId;

        //optional - for sync up purpose
        private DateTime? _createDate;
        private DateTime? _checkInDate;
        //private string _createdBy;
        private string _note;
        private string _bsId;
        private string _aboId;
        private int? _totalBillCollected;
        private decimal? _bookPaidAmount;
        private decimal? _baseAmount;
        private decimal? _ftAmount;
        private decimal? _vatAmount;

        private string _billKeeperName;
        private string _modifiedBy;

        private bool _isEditBillBook;

        private string _creatorName;

        public void Clear()
        {
            _bookLot = 0;
            _printDate = null;
            _branchId = null;
            _branchName = null;
            _period = null;
            _receiveCount = (byte)0;
            _advPayDt = Session.BpmDateTime.Now;
            _returnedDt = Session.BpmDateTime.Now;
            _agentId = null;
            _agentName = null;
            _agentStatus = null;
            _agentType = null;
            _totalAsset = (decimal)0.00;
            _bkId = null;
            _billBookId = null;
            _originalBillBookId = null;
            _isFirstPaid = true;
            _isAgency = true;
            _advPayAmount = null;
            _createFailReason = null;
            _bookHolderBranchId = null;
            _bookAmountOverAsset = false;
            _isPrintBillbook = true;
            _totalBillCount = 0;
            _totalBookAmount = 0;
            
            //optional for sync up
            _createDate = Session.BpmDateTime.Now;
            _checkInDate = Session.BpmDateTime.Now;
            //_createdBy = null;
            _note = null;
            _bsId = null ;
            _aboId = null;
            _totalBillCollected = 0;
            _bookPaidAmount = 0;
            _baseAmount = 0;
            _ftAmount = 0;
            _vatAmount = 0;
            _billKeeperName = String.Empty;
            _modifiedBy = String.Empty;
            _isEditBillBook = false;
            _creatorName = String.Empty;
        }


        [DataMember(Order=1)]
        public int? BookLot
        {
            get { return _bookLot; }
            set { _bookLot = value; }
        }



        [DataMember(Order=2)]
        public string BranchName
        {
            get { return _branchName; }
            set { _branchName = value; }
        }


        [DataMember(Order=3)]
        public string PrintDate
        {
            get { return _printDate; }
            set { _printDate = value; }
        }


        [DataMember(Order=4)]
        public string RunningBranchId
        {
            get { return _bookHolderBranchId; }
            set { _bookHolderBranchId = value; }
        }


        [DataMember(Order=5)]
        public bool BookAmountOverAsset
        {
            get { return _bookAmountOverAsset; }
            set { _bookAmountOverAsset = value; }
        }


        [DataMember(Order=6)]
        public string BookHolderBranchId
        {
            get { return _bookHolderBranchId; }
            set { _bookHolderBranchId = value; }
        }


        [DataMember(Order=7)]
        public string CreateFailReason
        {
            get { return _createFailReason; }
            set { _createFailReason = value; }
        }


        [DataMember(Order=8)]
        public decimal? AdvPayAmount
        {
            get { return _advPayAmount; }
            set { _advPayAmount = value; }
        }


        [DataMember(Order=9)]
        public string Period
        {
            get { return _period; }
            set { _period = value; }
        }


        [DataMember(Order=10)]
        public string AgentType
        {
            get { return _agentType; }
            set { _agentType = value; }
        }


        [DataMember(Order=11)]
        public byte? ReceiveCount
        {
            get { return _receiveCount; }
            set { _receiveCount = value; }
        }


        [DataMember(Order=12)]
        public DateTime? AdvancePaymentDt
        {
            get { return _advPayDt; }
            set { _advPayDt = value; }
        }


        [DataMember(Order=13)]
        public DateTime? ReturnedBillDt
        {
            get { return _returnedDt; }
            set { _returnedDt = value; }
        }
        

        [DataMember(Order=14)]
        public string AgentId
        {
            get { return _agentId; }
            set { _agentId = value; }
        }


        [DataMember(Order=15)]
        public string AgentName
        {
            get { return _agentName; }
            set { _agentName = value; }
        }


        [DataMember(Order=16)]
        public string AgentStatus
        {
            get { return _agentStatus; }
            set { _agentStatus = value; }
        }


        [DataMember(Order=17)]
        public decimal? TotalAsset
        {
            get { return _totalAsset; }
            set { _totalAsset = value; }
        }


        [DataMember(Order=18)]
        public string BillBookId
        {
            get { return _billBookId; }
            set { _billBookId = value; }
        }


        [DataMember(Order=19)]
        public string OriginalBillBookId
        {
            get { return _originalBillBookId; }
            set { _originalBillBookId = value; }
        }


        [DataMember(Order=20)]
        public string ControllerId
        {
            get { return _bkId; }
            set { _bkId = value; }
        }


        [DataMember(Order=21)]
        public bool IsFirstPaid
        {
            get { return _isFirstPaid; }
            set { _isFirstPaid = value; }
        }


        [DataMember(Order=22)]
        public bool IsAgency
        {
            get { return _isAgency; }
            set { _isAgency = value; }
        }


        [DataMember(Order=23)]
        public bool IsPrintPreview
        {
            get { return this._isPrintPreview; }
            set { this._isPrintPreview = value; }
        }

        [DataMember(Order=24)]
        public bool IsPrintDetail
        {
            get { return this._isPrintDetail; }
            set { this._isPrintDetail = value; }
        }


        [DataMember(Order=25)]
        public bool IsPrintBillbook
        {
            get { return _isPrintBillbook; }
            set { _isPrintBillbook = value; }
        }


        [DataMember(Order=26)]
        public int? TotalBillCount
        {
            get { return _totalBillCount; }
            set { _totalBillCount = value; }
        }


        [DataMember(Order=27)]
        public decimal? TotalBookAmount
        {
            get { return _totalBookAmount; }
            set { _totalBookAmount = value; }
        }


        //optional for syncup


        [DataMember(Order=28)]
        public DateTime? CreateDate
        {
            get { return _createDate; }
            set { _createDate = value; }
        }


        [DataMember(Order=29)]
        public DateTime? CheckInDate
        {
            get { return _checkInDate; }
            set { _checkInDate = value; }
        }

        //public string CreatedBy
        //{
        //    get { return _createdBy; }
        //    set { _createdBy = value; }
        //}


        [DataMember(Order=30)]
        public string Note
        {
            get { return _note; }
            set { _note = value; }
        }


        [DataMember(Order=31)]
        public string BsId
        {
            get { return _bsId; }
            set { _bsId = value; }
        }


        [DataMember(Order=32)]
        public string AboId
        {
            get { return _aboId; }
            set { _aboId = value; }
        }


        [DataMember(Order=33)]
        public int? TotalBillCollected
        {
            get { return _totalBillCollected; }
            set { _totalBillCollected = value; }
        }


        [DataMember(Order=34)]
        public decimal? BookPaidAmount
        {
            get { return _bookPaidAmount; }
            set { _bookPaidAmount = value; }
        }


        [DataMember(Order=35)]
        public decimal? BaseAmount
        {
            get { return _baseAmount; }
            set { _baseAmount = value; }
        }


        [DataMember(Order=36)]
        public decimal? FtAmount
        {
            get { return _ftAmount; }
            set { _ftAmount = value; }
        }


        [DataMember(Order=37)]
        public decimal? VatAmount
        {
            get { return _vatAmount; }
            set { _vatAmount = value; }
        }

        [DataMember(Order=38)]
        public string BillKeeperName
        {
            get { return this._billKeeperName; }
            set { this._billKeeperName = value; }
        }


        [DataMember(Order=39)]
        public string ModifiedBy
        {
            get { return this._modifiedBy; }
            set { this._modifiedBy = value; }
        }


        [DataMember(Order=40)]
        public bool IsEditBillBook
        {
            get  { return this._isEditBillBook; }
            set  { this._isEditBillBook = value;  }
        }


        [DataMember(Order=41)]
        public string CreatorName
        {
            get { return this._creatorName; }
            set { this._creatorName = value; }
        }
    }
}
