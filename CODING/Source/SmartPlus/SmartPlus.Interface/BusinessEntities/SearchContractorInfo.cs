using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.SmartPlus.Interface.BusinessEntities
{
    [DataContract]
    public class SearchContractorServiceInfoX
    {            
        private string _branchId;
        private string _caId;
        private string _period;
        private decimal _gAmount;
        private decimal _ftAmount;
        private decimal _vatAmount;
        private string _capMId;
        private string _accountClassId;
        private string _accountClassName;
        private string _modifiedFlag;
        private string _billBookFlag;
        private string _disconnectionFlag;
        private string _keeperFlag;
        private string _paymentOrderFlag;
        private string _groupInvoiceFlag;
        private decimal _qty;
        private string _receiveStatus;
        private string _arPmId;
        private string _invoiceNo;
        private string _dueDt;



        [DataMember(Order=1)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }



        [DataMember(Order=2)]
        public string CaId
        {
            get { return this._caId; }
            set { this._caId = value; }
        }



        [DataMember(Order=3)]
        public string Period
        {
            get { return this._period; }
            set { this._period = value; }
        }



        [DataMember(Order=4)]
        public decimal GAmount
        {
            get { return this._gAmount; }
            set { this._gAmount = value; }
        }



        [DataMember(Order=5)]
        public decimal FTAmount
        {
            get { return this._ftAmount; }
            set { this._ftAmount = value; }
        }



        [DataMember(Order=6)]
        public decimal VatAmount
        {
            get { return this._vatAmount; }
            set { this._vatAmount = value; }
        }



        [DataMember(Order=7)]
        public string CAPmId
        {
            get { return this._capMId; }
            set { this._capMId = value; }
        }



        [DataMember(Order=8)]
        public string AccountClassId
        {
            get { return this._accountClassId; }
            set { this._accountClassId = value; }
        }



        [DataMember(Order=9)]
        public string AccountClassName
        {
            get { return this._accountClassName; }
            set { this._accountClassName = value; }
        }


        [DataMember(Order=10)]
        public string ModifiedFlag
        {
            get { return this._modifiedFlag; }
            set { this._modifiedFlag = value; }
        }



        [DataMember(Order=11)]
        public string BillBookFlag
        {
            get { return this._billBookFlag; }
            set { this._billBookFlag = value; }
        }



        [DataMember(Order=12)]
        public string DisconnectionFlag
        {
            get { return this._disconnectionFlag; }
            set { this._disconnectionFlag = value; }
        }



        [DataMember(Order=13)]
        public string KeeperFlag
        {
            get { return this._keeperFlag; }
            set { this._keeperFlag = value; }
        }



        [DataMember(Order=14)]
        public string PaymentOrderFlag
        {
            get { return this._paymentOrderFlag; }
            set { this._paymentOrderFlag = value; }
        }



        [DataMember(Order=15)]
        public string GroupInvoiceFlag
        {
            get { return this._groupInvoiceFlag; }
            set { this._groupInvoiceFlag = value; }
        }



        [DataMember(Order=16)]
        public decimal Qty
        {
            get { return this._qty; }
            set { this._qty = value; }
        }



        [DataMember(Order=17)]
        public string ReceiveStatus
        {
            get { return this._receiveStatus; }
            set { this._receiveStatus = value; }
        }



        [DataMember(Order=18)]
        public string ARPmId
        {
            get { return this._arPmId; }
            set { this._arPmId = value; }
        }



        [DataMember(Order=19)]
        public string InvoiceNo
        {
            get { return this._invoiceNo; }
            set { this._invoiceNo = value; }
        }


        [DataMember(Order=20)]
        public string DueDt
        {
            get { return this._dueDt; }
            set { this._dueDt = value; }
        }
    
    }
}
