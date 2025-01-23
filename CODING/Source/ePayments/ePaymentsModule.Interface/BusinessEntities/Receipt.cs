using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities
{
    [DataContract]
    public class Receipt
    {
        private string _receiptId;
		private string _paymentId;
        private string _aRPmId;
        private string _aRPtId;
		private int _printingSequence;
		private int _totalReceipt;
		private string _caId;
		private string _name;
		private string _address;
		private string _isNameAddrModified;
	    private int _noOfPrinting;
		private string _receiptType;
		private string _extReceiptId;
		private DateTime? _extReceiptDt;
		private string _syncFlag;
		private DateTime? _postDt;
		private string _postBranchServerId;
		private DateTime? _modifiedDt;
		private string _modifiedBy;
        private string _active;



        [DataMember(Order=1)]
        public string ReceiptId
        {
            get { return this._receiptId; }
            set { this._receiptId = value; }
        }


        [DataMember(Order=2)]
        public string PaymentId
        {
            get { return this._paymentId; }
            set { this._paymentId = value; }
        }


        [DataMember(Order=3)]
        public string ARPmId
        {
            get { return this._aRPmId; }
            set { this._aRPmId = value; }
        }


        [DataMember(Order=4)]
        public string ARPtId
        {
            get { return this._aRPtId; }
            set { this._aRPtId = value; }
        }


        [DataMember(Order=5)]
        public int PrintingSequence
        {
            get { return this._printingSequence; }
            set { this._printingSequence = value; }
        }


        [DataMember(Order=6)]
        public int TotalReceipt
        {
            get { return this._totalReceipt; }
            set { this._totalReceipt = value; }
        }


        [DataMember(Order=7)]
        public string CaId
        {
            get { return this._caId; }
            set { this._caId = value; }
        }


        [DataMember(Order=8)]
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }


        [DataMember(Order=9)]
        public string Address
        {
            get { return this._address; }
            set { this._address = value; }
        }


        [DataMember(Order=10)]
        public string IsNameAddrModified
        {
            get { return this._isNameAddrModified; }
            set { this._isNameAddrModified = value; }
        }


        [DataMember(Order=11)]
        public int NoOfPrinting
        {
            get { return this._noOfPrinting; }
            set { this._noOfPrinting = value; }
        }


        [DataMember(Order=12)]
        public string ReceiptType
        {
            get { return this._receiptType; }
            set { this._receiptType = value; }
        }


        [DataMember(Order=13)]
        public string ExtReceiptId
        {
            get { return this._extReceiptId; }
            set { this._extReceiptId = value; }
        }


        [DataMember(Order=14)]
        public DateTime? ExtReceiptDt
        {
            get { return this._extReceiptDt; }
            set { this._extReceiptDt = value; }
        }


        [DataMember(Order=15)]
        public string SyncFlag
        {
            get { return this._syncFlag; }
            set { this._syncFlag = value; }
        }


        [DataMember(Order=16)]
        public DateTime? PostDt
        {
            get { return this._postDt; }
            set { this._postDt = value; }
        }


        [DataMember(Order=17)]
        public string PostBranchServerId
        {
            get { return this._postBranchServerId; }
            set { this._postBranchServerId = value; }
        }


        [DataMember(Order=18)]
        public DateTime? ModifiedDt
        {
            get { return this._modifiedDt; }
            set { this._modifiedDt = value; }
        }


        [DataMember(Order=19)]
        public string ModifiedBy
        {
            get { return this._modifiedBy; }
            set { this._modifiedBy = value; }
        }


        [DataMember(Order=20)]
        public string Active
        {
            get { return this._active; }
            set { this._active = value; }
        }
    }
}
