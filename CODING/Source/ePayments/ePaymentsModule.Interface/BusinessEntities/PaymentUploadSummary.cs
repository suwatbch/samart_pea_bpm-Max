using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities
{
    [DataContract]
    public class PaymentUploadSummary
    {
        private Company _comp;
        private decimal _sysDebtAmount;
        private decimal _fileDebtAmount;
        private int _sysDebtNo;
        private int _fileDebtNo;
        private decimal _clearDebtAmount;
        private decimal _backDebtAmount;
        private int _clearDebtNo;
        private int _backDebtNo;

        public PaymentUploadSummary()
        {
            _comp = new Company();
        }


        [DataMember(Order=1)]
        public string CompanyId
        {
            get { return this._comp.CompanyId; }
            set { this._comp.CompanyId = value; }
        }


        [DataMember(Order=2)]
        public string CompanyName
        {
            get { return this._comp.CompanyName; }
            set { this._comp.CompanyName = value; }
        }


        [DataMember(Order=3)]
        public decimal SysDebtAmount
        {
            get { return this._sysDebtAmount; }
            set { this._sysDebtAmount = value; }
        }


        [DataMember(Order=4)]
        public decimal FileDebtAmount
        {
            get { return this._fileDebtAmount; }
            set { this._fileDebtAmount = value; }
        }


        [DataMember(Order=5)]
        public int SysDebtNo
        {
            get { return this._sysDebtNo; }
            set { this._sysDebtNo = value; }
        }


        [DataMember(Order=6)]
        public int FileDebtNo
        {
            get { return this._fileDebtNo; }
            set { this._fileDebtNo = value; }
        }


        [DataMember(Order=7)]
        public decimal ClearDebtAmount
        {
            get { return this._clearDebtAmount; }
            set { this._clearDebtAmount = value; }
        }


        [DataMember(Order=8)]
        public decimal BackDebtAmount
        {
            get { return this._backDebtAmount; }
            set { this._backDebtAmount = value; }
        }


        [DataMember(Order=9)]
        public int ClearDebtNo
        {
            get { return this._clearDebtNo; }
            set { this._clearDebtNo = value; }
        }


        [DataMember(Order=10)]
        public int BackDebtNo
        {
            get { return this._backDebtNo; }
            set { this._backDebtNo = value; }
        }



    }
}
