using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities.ReceiptPrinting
{
    [DataContract]
    public class GreenReceipt
    {
        private string _receiptId;
        private string _postDt;
        private string _taxInvBranch;
        private string _caName;
        private string _caAddress;
        private string _meterId;
        private string _rateType;
        private string _caId;
        private string _period;
        private string _meterReadDt;
        private string _lastUnit;
        private string _readUnit;
        private string _qty;
        private decimal? _baseAmount;
        private decimal? _ftUnitPrice;
        private decimal? _ftAmount;
        private decimal? _taxRate;
        private decimal? _vatAmount;
        private decimal? _gAmount;


        [DataMember(Order=1)]
        public string ReceiptId
        {
            get { return this._receiptId; }
            set { this._receiptId = value; }
        }


        [DataMember(Order=2)]
        public string PostDt
        {
            get { return this._postDt; }
            set { this._postDt = value; }
        }


        [DataMember(Order=3)]
        public string TaxInvBranch
        {
            get { return this._taxInvBranch; }
            set { this._taxInvBranch = value; }
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
        public string RateType
        {
            get { return this._rateType; }
            set { this._rateType = value; }
        }


        [DataMember(Order=8)]
        public string CaId
        {
            get { return this._caId; }
            set { this._caId = value; }
        }


        [DataMember(Order=9)]
        public string Peroid
        {
            get { return this._period; }
            set { this._period = value; }
        }


        [DataMember(Order=10)]
        public string MeterReadDt
        {
            get { return this._meterReadDt ; }
            set { this._meterReadDt = value; }
        }


        [DataMember(Order=11)]
        public string LastUnit
        {
            get { return this._lastUnit; }
            set { this._lastUnit = value; }
        }


        [DataMember(Order=12)]
        public string ReadUnit
        {
            get { return this._readUnit; }
            set { this._readUnit = value; }
        }


        [DataMember(Order=13)]
        public string Qty
        {
            get { return this._qty; }
            set { this._qty = value; }
        }


        [DataMember(Order=14)]
        public decimal? BaseAmount
        {
            get { return this._baseAmount; }
            set { this._baseAmount = value; }
        }


        [DataMember(Order=15)]
        public decimal? FtUnitPrice
        {
            get { return this._ftUnitPrice; }
            set { this._ftUnitPrice = value; }
        }


        [DataMember(Order=16)]
        public decimal? FtAmount
        {
            get { return this._ftAmount; }
            set { this._ftAmount = value; }
        }


        [DataMember(Order=17)]
        public decimal? TaxRate
        {
            get { return this._taxRate; }
            set { this._taxRate = value; }
        }


        [DataMember(Order=18)]
        public decimal? VatAmount
        {
            get { return this._vatAmount; }
            set { this._vatAmount = value; }
        }


        [DataMember(Order=19)]
        public decimal? GAmount
        {
            get { return this._gAmount; }
            set { this._gAmount = value; }
        }

    }
}
