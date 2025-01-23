using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.EcsModule.Interface.BusinessEntities
{
    [DataContract]
    public class PrintInvoiceInfo
    {

        private string _branchId;
        private string _branchName;
        private string _branchAddress;
        private string _caName;
        private string _caAddress;
        private string _meterId;
        private string _rateTypeId;
        private string _caBranchId;
        private string _caBranchName;
        private string _caId;
        private string _period;
        private string _meterReadDt;
        private decimal _lastUnit;
        private decimal _readUnit;
        private decimal _qty;
        private String _baseAmount;
        private decimal _ftUnitPrice;
        private String _ftAmount;
        private String _amount;
        private decimal _taxCode;
        private String _vatAmount;
        private string _gAmount;
        private string _paymentDt;
        private string _controllerId;
        private string _invoiceNo;
        private string _invoiceDt;
        private string _businessArea;

        [DataMember(Order = 1)]
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
        public string BranchAddress
        {
            get { return this._branchAddress; }
            set { this._branchAddress = value; }
        }

        [DataMember(Order=4)]
        public string CaName
        {
            get { return this._caName; }
            set { this._caName = value; }
        }

        [DataMember(Order=5)]
        public string CaAddress
        {
            get { return this._caAddress; }
            set { this._caAddress = value; }
        }

        [DataMember(Order=6)]
        public string MeterId
        {
            get { return this._meterId; }
            set { this._meterId = value; }
        }

        [DataMember(Order=7)]
        public string RateTypeId
        {
            get { return this._rateTypeId; }
            set { this._rateTypeId = value; }
        }

        [DataMember(Order = 8)]
        public string CaBranchId
        {
            get { return this._caBranchId; }
            set { this._caBranchId = value; }
        }

        [DataMember(Order = 9)]
        public string CaBranchName
        {
            get { return this._caBranchName; }
            set { this._caBranchName = value; }
        }

        [DataMember(Order = 10)]
        public string CaId
        {
            get { return this._caId; }
            set { this._caId = value; }
        }

        [DataMember(Order = 11)]
        public string Period
        {
            get { return this._period; }
            set { this._period = value; }
        }

        [DataMember(Order = 12)]
        public string MeterReadDt
        {
            get { return this._meterReadDt; }
            set { this._meterReadDt = value; }
        }

        [DataMember(Order = 13)]
        public decimal LastUnit
        {
            get { return this._lastUnit; }
            set { this._lastUnit = value; }
        }

        [DataMember(Order = 14)]
        public decimal ReadUnit
        {
            get { return this._readUnit; }
            set { this._readUnit = value; }
        }

        [DataMember(Order = 15)]
        public decimal Qty
        {
            get { return this._qty; }
            set { this._qty = value; }
        }

        [DataMember(Order = 16)]
        public String BaseAmount
        {
            get { return this._baseAmount; }
            set { this._baseAmount = value; }
        }

        [DataMember(Order = 17)]
        public decimal FTUnitPrice
        {
            get { return this._ftUnitPrice; }
            set { this._ftUnitPrice = value; }
        }

        [DataMember(Order = 18)]
        public String FTAmount
        {
            get { return this._ftAmount; }
            set { this._ftAmount = value; }
        }

        [DataMember(Order = 19)]
        public String Amount
        {
            get { return this._amount; }
            set { this._amount = value; }
        }

        [DataMember(Order = 20)]
        public decimal TaxCode
        {
            get { return this._taxCode; }
            set { this._taxCode = value; }
        }

        [DataMember(Order = 21)]
        public String VatAmount
        {
            get { return this._vatAmount; }
            set { this._vatAmount = value; }
        }

        [DataMember(Order = 22)]
        public string GAmount
        {
            get { return this._gAmount; }
            set { this._gAmount = value; }
        }

        [DataMember(Order = 23)]
        public string PaymentDt
        {
            get { return this._paymentDt; }
            set { this._paymentDt = value; }
        }

        [DataMember(Order = 24)]
        public string ControllerId
        {
            get { return this._controllerId; }
            set { this._controllerId = value; }
        }

        [DataMember(Order = 25)]
        public string InvoiceNo
        {
            get { return this._invoiceNo; }
            set { this._invoiceNo = value; }
        }

        [DataMember(Order = 26)]
        public string InvoiceDt
        {
            get { return this._invoiceDt; }
            set { this._invoiceDt = value; }
        }

        [DataMember(Order = 27)]
        public string BusinessArea
        {
            get { return this._businessArea; }
            set { this._businessArea = value; }
        }
        

    }
}
