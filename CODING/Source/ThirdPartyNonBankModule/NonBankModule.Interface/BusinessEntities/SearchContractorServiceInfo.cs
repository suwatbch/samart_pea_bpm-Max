using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.NonBankModule.Interface.BusinessEntities
{
    [DataContract]
    public class SearchContractorServiceInfo
    {
        private string  _branchId;
        private string  _branchName;
        private string  _caId;        
        private string  _caPMId;
        private string  _accountClassId;
        private string  _accountClassName;
        private string  _mainSub;
        private string  _dtName;
        private string  _invoiceNo;
        private string  _caDoc;                     //10
        private string  _meterReadDt;               
        private string  _billingDt;         
        private string  _dueDt;
        private string  _dueDt2;
        private string  _dunningDt;
        private string  _period;
        private decimal _qty;
        private decimal _ftAmount;        
        private decimal _vatRate;        
        private decimal _vatAmount;                 //20
        private decimal _gAmount;
        private string _investigateFlag;
        private string  _disconnectionFlag;
        private string  _groupInvoiceFlag;  
        private string _installmentFlag;
        private string _paymentOrderFlag;
        private string _receiveStatus;
        private string _receiptId;
        private string _receiptDt;
        private string _receiptCh;
        private string _receiptBranchId;        //30
        private string _receiptBranchName;
        private string _taxId;
        private string _taxBranch;
        private string _remark;                 //32


        [DataMember(Order=1)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }

        [DataMember(Order=2)]
        public string BranchName
        {
            get { return this._branchName; }
            set { this._branchName = value; }
        }

        [DataMember(Order=3)]
        public string CaId
        {
            get { return this._caId; }
            set { this._caId = value; }
        }

        [DataMember(Order = 4)]
        public string CAPmId
        {
            get { return this._caPMId; }
            set { this._caPMId = value; }
        }       
        
        [DataMember(Order=5)]
        public string AccountClassId
        {
            get { return this._accountClassId; }
            set { this._accountClassId = value; }
        }

        [DataMember(Order=6)]
        public string AccountClassName
        {
            get { return this._accountClassName; }
            set { this._accountClassName = value; }
        }

        [DataMember(Order=7)]
        public string MainSub
        {
            get { return this._mainSub; }
            set { this._mainSub = value; }
        }

        [DataMember(Order=8)]
        public string DtName
        {
            get { return this._dtName; }
            set { this._dtName = value; }
        }
       
        [DataMember(Order=9)]
        public string InvoiceNo
        {
            get { return this._invoiceNo; }
            set { this._invoiceNo = value; }
        }

        [DataMember(Order = 10)]
        public string CaDoc
        {
            get { return this._caDoc; }
            set { this._caDoc = value; }
        }

        [DataMember(Order=11)]
        public string MeterReadDt
        {
            get { return this._meterReadDt; }
            set { this._meterReadDt = value; }
        }

        [DataMember(Order=12)]
        public string BillingDt
        {
            get { return this._billingDt; }
            set { this._billingDt = value; }
        }
        
        [DataMember(Order=13)]
        public string DueDt
        {
            get { return this._dueDt; }
            set { this._dueDt = value; }
        }

        [DataMember(Order = 14)]
        public string DueDt2
        {
            get { return this._dueDt2; }
            set { this._dueDt2 = value; }
        }       

        [DataMember(Order=15)]
        public string DunningDt
        {
            get { return this._dunningDt; }
            set { this._dunningDt = value; }
        }       
       
        [DataMember(Order=16)]
        public string Period
        {
            get { return this._period; }
            set { this._period = value; }
        }

        [DataMember(Order=17)]
        public decimal Qty
        {
            get { return this._qty; }
            set { this._qty = value; }
        }

        [DataMember(Order=18)]
        public decimal FTAmount
        {
            get { return this._ftAmount; }
            set { this._ftAmount = value; }
        }

        [DataMember(Order=19)]
        public decimal VatRate
        {
            get { return this._vatRate; }
            set { this._vatRate = value; }
        }
       
        [DataMember(Order=20)]
        public decimal VatAmount
        {
            get { return this._vatAmount; }
            set { this._vatAmount = value; }
        }

        [DataMember(Order=21)]
        public decimal GAmount
        {
            get { return this._gAmount; }
            set { this._gAmount = value; }
        }

        [DataMember(Order = 22)]
        public string InvestigateFlag
        {
            get { return this._investigateFlag; }
            set { this._investigateFlag = value; }
        }


        
        [DataMember(Order=23)]
        public string DisconnectionFlag
        {
            get { return this._disconnectionFlag; }
            set { this._disconnectionFlag = value; }
        }       

        [DataMember(Order=24)]
        public string GroupInvoiceFlag
        {
            get { return this._groupInvoiceFlag; }
            set { this._groupInvoiceFlag = value; }
        }        
       
        [DataMember(Order = 25)]
        public string InstallmentFlag
        {
            get { return this._installmentFlag; }
            set { this._installmentFlag = value; }
        }

        [DataMember(Order = 26)]
        public string PaymentOrderFlag
        {
            get { return this._paymentOrderFlag; }
            set { this._paymentOrderFlag = value; }
        }

        [DataMember(Order = 27)]
        public string ReceiveStatus
        {
            get { return this._receiveStatus; }
            set { this._receiveStatus = value; }
        }

        [DataMember(Order = 28)]
        public string ReceiptId
        {
            get { return this._receiptId; }
            set { this._receiptId = value; }
        }

        [DataMember(Order = 29)]
        public string ReceiptDt
        {
            get { return this._receiptDt; }
            set { this._receiptDt = value; }
        }

        [DataMember(Order = 30)]
        public string ReceiptCh
        {
            get { return this._receiptCh; }
            set { this._receiptCh = value; }
        }

        [DataMember(Order = 31)]
        public string ReceiptBranchId
        {
            get { return this._receiptBranchId; }
            set { this._receiptBranchId = value; }
        }

        [DataMember(Order = 32)]
        public string ReceiptBranchName
        {
            get { return this._receiptBranchName; }
            set { this._receiptBranchName = value; }
        }

        [DataMember(Order = 33)]
        public string TaxId
        {
            get { return this._taxId; }
            set { this._taxId = value; }
        }

        [DataMember(Order = 34)]
        public string TaxBranch
        {
            get { return this._taxBranch; }
            set { this._taxBranch = value; }
        }

        [DataMember(Order = 35)]
        public string Remark
        {
            get { return this._remark; }
            set { this._remark = value; }
        }
    
    }
}
