using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities
{
    [DataContract]
    public class EPayClearify
    {
        private string _issueId;
        private EpayUploadItem _ePayUploadItem;
        private string _newInvoice;
        private string _fixedType;
        private DateTime? _fixedDt;
        private string _fixedBy;
        private string _refDocNo;
        private string _recNo;
        private string _syncFlag;
        private DateTime? _postDate;
        private string _postBranchId;
        private DateTime? _modifiedDt;
        private string _modifiedBy;
        private string _active;

        public EPayClearify()
        {
            _ePayUploadItem = new EpayUploadItem();
        }


        [DataMember(Order=1)]
        public string IssueId
        {
            get { return this._issueId; }
            set { this._issueId = value; }
        }


        [DataMember(Order=2)]
        public EpayUploadItem EpayUploadItems
        {
            get { return this._ePayUploadItem; }
            set { this._ePayUploadItem = value; }
        }


        [DataMember(Order=3)]
        public string NewInvoiceNo
        {
            get { return this._newInvoice; }
            set { this._newInvoice = value; }
        }


        [DataMember(Order=4)]
        public string FixedType
        {
            get { return this._fixedType; }
            set { this._fixedType = value; }
        }


        [DataMember(Order=5)]
        public DateTime? FixedDt
        {
            get { return this._fixedDt; }
            set { this._fixedDt = value; }
        }


        [DataMember(Order=6)]
        public string FixedBy
        {
            get { return this._fixedBy; }
            set { this._fixedBy = value; }
        }


        [DataMember(Order=7)]
        public string RefDocNo
        {
            get { return this._refDocNo; }
            set { this._refDocNo = value; }
        }


        [DataMember(Order=8)]
        public string SyncFlag
        {
            get { return this._syncFlag; }
            set { this._syncFlag = value; }
        }


        [DataMember(Order=9)]
        public DateTime? PostDt
        {
            get { return this._postDate; }
            set { this._postDate = value; }
        }


        [DataMember(Order=10)]
        public string PostBranchId
        {
            get { return this._postBranchId; }
            set { this._postBranchId = value; }
        }


        [DataMember(Order=11)]
        public DateTime? ModifiedDt
        {
            get { return this._modifiedDt; }
            set { this._modifiedDt = value; }
        }


        [DataMember(Order=12)]
        public string ModifiedBy
        {
            get { return this._modifiedBy; }
            set { this._modifiedBy = value; }
        }


        [DataMember(Order=13)]
        public string Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

    }
}
