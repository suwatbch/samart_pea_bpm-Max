using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities
{

    [DataContract]
    public class PaymentInfo
    {
        string _branchId;
        string _mruId;
        string _caId;
        DateTime _paymentDt;
        DateTime _dueDt;
        decimal _gAmount;
        string _receiptType;
        string _receiptNo;
        string _period;
        string _counterId;
        string _receivePlace;
        decimal _vatAmount;
        decimal _totalAmount;
        string _invoiceNo;
        string _postServerId;
        string _modifiedBy;


        [DataMember(Order=2)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }


        [DataMember(Order=3)]
        public string MruId
        {
            get { return this._mruId; }
            set { this._mruId = value; }
        }


        [DataMember(Order=4)]
        public string CaId
        {
            get { return this._caId; }
            set { this._caId = value; }
        }


        [DataMember(Order=5)]
        public DateTime PaymentDt
        {
            get { return this._paymentDt; }
            set { this._paymentDt = value; }
        }


        [DataMember(Order=6)]
        public DateTime DueDt
        {
            get { return this._dueDt; }
            set { this._dueDt = value; }
        }


        [DataMember(Order=7)]
        public decimal GAmount
        {
            get { return this._gAmount; }
            set { this._gAmount = value; }
        }


        [DataMember(Order=8)]
        public string ReceiptType
        {
            get { return this._receiptType; }
            set { this._receiptType = value; }
        }


        [DataMember(Order=9)]
        public string ReceiptNo
        {
            get { return this._receiptNo; }
            set { this._receiptNo = value; }
        }

        [DataMember(Order=10)]
        public string Period
        {
            get { return this._period; }
            set { this._period = value; }
        }

        [DataMember(Order=11)]
        public string CounterId
        {
            get { return this._counterId; }
            set { this._counterId = value; }
        }

        [DataMember(Order=12)]
        public string ReceivePlace
        {
            get { return this._receivePlace; }
            set { this._receivePlace = value; }
        }

        [DataMember(Order=13)]
        public decimal VatAmount
        {
            get { return this._vatAmount; }
            set { this._vatAmount = value; }
        }


        [DataMember(Order=14)]
        public decimal TotalAmount
        {
            get { return this._totalAmount; }
            set { this._totalAmount = value; }
        }


        [DataMember(Order=15)]
        public string InvoiceNo
        {
            get { return this._invoiceNo; }
            set { this._invoiceNo = value; }
        }


        [DataMember(Order=16)]
        public string PostServerId
        {
            get { return this._postServerId; }
            set { this._postServerId = value; }
        }


        [DataMember(Order=17)]
        public string ModifiedBy
        {
            get { return this._modifiedBy; }
            set { this._modifiedBy = value; }
        }
    }
}
