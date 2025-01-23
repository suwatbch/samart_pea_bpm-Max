using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class EvaluateAgencyInfo
    {
        #region "Local Variable"
        private string _peaCode;
        private string _peaName;
        private int? _agentPersonType = 0;
        private int? _agentCompanyType = 0;
        private int? _totalAgent = 0;
        private int? _billOut = 0;
        private decimal? _amountBillOut = 0;
        private int? _canKeepBill = 0;
        private decimal? _canKeepPercent = 0;
        private decimal? _totalMoney = 0;
        private decimal? _totalMoneyPercent = 0;
        private int? _putInvoiceItem = 0;
        private decimal? _sendInvoive = 0;
        private decimal? _extraMoney = 0;
        #endregion
        #region "Property"

        [DataMember(Order=1)]
        public string PeaCode
        {
            get { return _peaCode; }
            set { this._peaCode = value; }
        }

        [DataMember(Order=2)]
        public string PeaName
        {
            get { return _peaName; }
            set { this._peaName = value; }
        }

        [DataMember(Order=3)]
        public int? AgentPersonType
        {
            get { return _agentPersonType; }
            set { this._agentPersonType = value; }
        }

        [DataMember(Order=4)]
        public int? AgentCompanyType
        {
            get { return _agentCompanyType; }
            set { this._agentCompanyType = value; }
        }

        [DataMember(Order=5)]
        public int? TotalAgent
        {
            get { return _totalAgent; }
            set { this._totalAgent = value; }
        }

        [DataMember(Order=6)]
        public int? BillOut
        {
            get { return _billOut; }
            set { this._billOut = value; }
        }

        [DataMember(Order=7)]
        public decimal? AmountBillOut
        {
            get { return _amountBillOut; }
            set { this._amountBillOut = value; }
        }

        [DataMember(Order=8)]
        public int? CanKeepBill
        {
            get { return _canKeepBill; }
            set { this._canKeepBill = value; }
        }

        [DataMember(Order=9)]
        public decimal? CanKeepPercent
        {
            get { return _canKeepPercent; }
            set { this._canKeepPercent = value; }
        }

        [DataMember(Order=10)]
        public decimal? TotalMoney
        {
            get { return _totalMoney; }
            set { this._totalMoney = value; }
        }

        [DataMember(Order=11)]
        public decimal? TotalMoneyPercent
        {
            get { return _totalMoneyPercent; }
            set { this._totalMoneyPercent = value; }
        }

        [DataMember(Order=12)]
        public int? PutInvoiceItem
        {
            get { return _putInvoiceItem; }
            set { this._putInvoiceItem = value; }
        }

        [DataMember(Order=13)]
        public decimal? SendInvoive
        {
            get { return _sendInvoive; }
            set { this._sendInvoive = value; }
        }

        [DataMember(Order=14)]
        public decimal? ExtraMoney
        {
            get { return _extraMoney; }
            set { this._extraMoney = value; }
        }
        #endregion
    }
}
