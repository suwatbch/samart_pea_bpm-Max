using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports
{
    [DataContract]
    public class RE02_ReportInfo
    {
        private string _branchId;
        private string _branchName;
        private string _accountClassName;
        private string _caName;
        private string _mruId;
        private string _caId;
        private string _period;
        private decimal? _outSourceAmount;
        private string _invoiceNo;
        private string _payDt;


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
        public string AccountClassName
        {
            get { return this._accountClassName; }
            set { this._accountClassName = value; }
        }


        [DataMember(Order=4)]
        public string CaName
        {
            get { return this._caName; }
            set { this._caName = value; }
        }



        [DataMember(Order=5)]
        public string MruId
        {
            get { return this._mruId; }
            set { this._mruId = value; }
        }


        [DataMember(Order=6)]
        public string CaId
        {
            get { return this._caId; }
            set { this._caId = value; }
        }


        [DataMember(Order=7)]
        public string Period
        {
            get { return this._period; }
            set { this._period = value; }
        }


        [DataMember(Order=8)]
        public decimal? OutSourceAmount
        {
            get { return this._outSourceAmount; }
            set { this._outSourceAmount = value; }
        }


        [DataMember(Order=9)]
        public string InvoiceNo
        {
            get { return this._invoiceNo; }
            set { this._invoiceNo = value; }
        }


        [DataMember(Order=10)]
        public string PayDt
        {
            get { return this._payDt; }
            set { this._payDt = value; }
        }
    }
}
