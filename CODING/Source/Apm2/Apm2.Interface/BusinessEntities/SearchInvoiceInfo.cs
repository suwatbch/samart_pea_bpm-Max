using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Apm2.Interface.BusinessEntities
{
    [DataContract]
    public class SearchInvoiceInfo
    {
        private string _returnType;
        private string _caId;
        private string _caName;
        private string _dtId;
        private string _dtName;
        private string _invoiceNo;
        private string _period;
        private string _dueDate;
        private string _amount;
        private string _gAmount;
        private string _remark;
        private string _pmId;
        private string _rateTypeId;
        


        [DataMember(Order=1)]
        public string ReturnType
        {
            get { return this._returnType; }
            set { this._returnType = value; }
        }

        [DataMember(Order=2)]
        public string CaId
        {
            get { return this._caId; }
            set { this._caId = value; }
        }

        [DataMember(Order=3)]
        public string CaName
        {
            get { return this._caName; }
            set { this._caName = value; }
        }

        [DataMember(Order = 4)]
        public string DtId
        {
            get { return this._dtId; }
            set { this._dtId = value; }
        }

        [DataMember(Order = 5)]
        public string DtName
        {
            get { return this._dtName; }
            set { this._dtName = value; }
        }

        [DataMember(Order=6)]
        public string InvoiceNo
        {
            get { return this._invoiceNo; }
            set { this._invoiceNo = value; }
        }

        [DataMember(Order=7)]
        public string Period
        {
            get { return this._period; }
            set { this._period = value; }
        }

        [DataMember(Order=8)]
        public string DueDate
        {
            get { return this._dueDate; }
            set { this._dueDate = value; }
        }

        [DataMember(Order=9)]
        public string Amount
        {
            get { return this._amount; }
            set { this._amount = value; }
        }

        [DataMember(Order = 10)]
        public string GAmount
        {
            get { return this._gAmount; }
            set { this._gAmount = value; }
        }

        [DataMember(Order=11)]
        public string Remark
        {
            get { return this._remark; }
            set { this._remark = value; }
        }

        [DataMember(Order = 12)]
        public string PmId
        {
            get { return this._pmId; }
            set { this._pmId = value; }
        }

        [DataMember(Order = 13)]
        public string RateTypeId
        {
            get { return this._rateTypeId; }
            set { this._rateTypeId = value; }
        }
    
    }
}
