using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.ePaymentsModule.Interface.Constants;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities
{
    [DataContract]
    public class SaveClearifyInfo
    {
        private string _issueId;
        private BPMClearifyInfo _bpmClearifyItem;        
        private decimal _paidAmount;
        private decimal _totalAmount;
        private CLEARIFY_TYPE _clearifyType;
        private string _depReceiptType; 
        private decimal _overDebtOwner;
        private string _invoiceNo;
        private string _newInvoiceNo;
        private string _modifiedBy;
        private string _postBranchServerId;
        private string _saveCaid;
        private string _agentId;

        private DateTime _paymentDt;
        private string _posId;
        private string _branchId;
        private string _clearifyDesc;
        private string _receiptPrefix;
        private string _receiptType;

        public SaveClearifyInfo()
        {
            _issueId = String.Empty;
            _bpmClearifyItem = new BPMClearifyInfo();          
            _paidAmount = 0;
            _clearifyType = CLEARIFY_TYPE.CLEAR_PAYMENT;
            _totalAmount = 0;
            _invoiceNo = String.Empty;
            _newInvoiceNo = String.Empty;
            _modifiedBy = String.Empty;

            _postBranchServerId = String.Empty;
            _saveCaid = String.Empty;
            _posId = String.Empty;
            _receiptPrefix = String.Empty;
            _receiptType = String.Empty;
        }


        [DataMember(Order=1)]
        public string IssueId
        {
            get
            {
                return this._issueId;
            }
            set 
            {
                this._issueId = value;
            }
        }


        [DataMember(Order=2)]
        public BPMClearifyInfo BPMClearifyItem
        {
            get 
            {
                return this._bpmClearifyItem;
            }
            set
            {
                this._bpmClearifyItem = value;
            }
        }


        [DataMember(Order=3)]
        public decimal OverDebtOwner
        {
            get
            {
                return this._overDebtOwner;
            }
            set
            {
                this._overDebtOwner = value;
            }
        }


        [DataMember(Order=4)]
        public decimal PaidAmount
        {
            get
            {
                return this._paidAmount;
            }
            set
            {
                this._paidAmount = value;
            }
        }


        [DataMember(Order=5)]
        public CLEARIFY_TYPE ClearifyType
        {
            get  
            {
                return this._clearifyType;
            }
            set
            {
                this._clearifyType = value;
            }
        }


        [DataMember(Order=6)]
        public decimal TotalAmount
        {
            get
            {
                return this._totalAmount;
            }
            set 
            {
                this._totalAmount = value;
            }
        }


        [DataMember(Order=7)]
        public string InvoiceNo
        {
            get
            {
                return this._invoiceNo;
            }
            set 
            {
                this._invoiceNo = value;
            }
        }


        [DataMember(Order=8)]
        public string NewInvoiceNo
        {
            get
            {
                return this._newInvoiceNo;
            }
            set
            {
                this._newInvoiceNo = value;
            }
        }


        [DataMember(Order=9)]
        public string ModifeidBy
        {
            get
            {
                return this._modifiedBy;
            }
            set 
            {
                this._modifiedBy = value;
            }
        }


        [DataMember(Order=10)]
        public string PostBranchServerId
        {
            get 
            { 
                return this._postBranchServerId; 
            }
            set 
            { 
                this._postBranchServerId = value; 
            }
        }


        [DataMember(Order=11)]
        public string SaveCaId
        {
            get
            {
                return this._saveCaid;
            }
            set 
            {
                this._saveCaid = value;
            }
        }


        [DataMember(Order=12)]
        public string AgentId
        {
            get
            {
                return this._agentId;
            }
            set 
            {
                this._agentId = value;
            }
        }


        [DataMember(Order=13)]
        public DateTime PaymentDt
        {
            get 
            {
                return this._paymentDt;
            }
            set 
            {
                this._paymentDt = value;
            }
        }


        [DataMember(Order=14)]
        public string PostId
        {
            get 
            {
                return this._posId;
            }
            set
            {
                this._posId = value;
            }
        }


        [DataMember(Order=15)]
        public string BranchId
        {
            get 
            {
                return this._branchId;
            }
            set 
            {
                this._branchId = value;
            }
        }


        [DataMember(Order=16)]
        public string ClearifyDes
        {
            get 
            {
                return this._clearifyDesc;
            }
            set
            {
                this._clearifyDesc = value;
            }
        }


        [DataMember(Order=17)]
        public string ReceiptPrefix
        {
            get
            {
                return this._receiptPrefix;
            }
            set
            {
                this._receiptPrefix = value;
            }
        }      


        [DataMember(Order=18)]
        public string DepReceiptType
        {
            get
            {
                return this._depReceiptType;
            }
            set
            {
                this._depReceiptType = value;
            }
        }      
    }
}
