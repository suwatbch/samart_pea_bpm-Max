using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class ReturnBillBookBillPasteStatusReportInfo
    {
        #region "Local Variable"
        private string _branchId;
        private string _branchName;
        private string _mruId;
        private string _pmId;
        private string _printDate;
        private string _checkInDate;
        private string _taxNo;
        private string _agentId;
        private string _agentName;
        private string _billBookId;
        private string _electricNo;
        private string _period;
        private decimal? _electricPrice = 0;
        private decimal? _ftPrice = 0;
        private decimal? _vatValue = 0;
        private decimal? _baseAmount = 0;
        #endregion

        #region "Property"

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
        public string MruId
        {
            get { return this._mruId; }
            set { this._mruId = value; }
        }


        [DataMember(Order=4)]
        public string PmId
        {
            get { return this._pmId; }
            set { this._pmId = value; }
        }      

        [DataMember(Order=5)]
        public string PrintDate
        {
            get { return this._printDate; }
            set { this._printDate = value; }
        }

        [DataMember(Order=6)]
        public string CheckInDate
        {
            get { return this._checkInDate; }
            set { this._checkInDate = value; }
        }

        [DataMember(Order=7)]
        public string TaxNo
        {
            get { return this._taxNo; }
            set { this._taxNo = value; }
        }

        [DataMember(Order=8)]
        public string AgentId
        {
            get { return this._agentId; }
            set { this._agentId = value; }
        }

        [DataMember(Order=9)]
        public string AgentName
        {
            get { return this._agentName; }
            set { this._agentName = value; }
        }

        [DataMember(Order=10)]
        public string RefBillBookId
        {
            get { return this._billBookId; }
            set { this._billBookId = value; }
        }

        [DataMember(Order=11)]
        public string Period
        {
            get { return this._period; }
            set { this._period = value; }
        }

        [DataMember(Order=12)]
        public string ElectricNo
        {
            get { return this._electricNo; }
            set { this._electricNo = value; }
        }

        [DataMember(Order=13)]
        public decimal? ElectricPrice
        {
            get { return this._electricPrice; }
            set { this._electricPrice = value; }
        }

        [DataMember(Order=14)]
        public decimal? FTPrice
        {
            get { return this._ftPrice; }
            set { this._ftPrice = value; }
        }

        [DataMember(Order=15)]
        public decimal? VatPrice
        {
            get { return this._vatValue; }
            set { this._vatValue = value; }
        }


        [DataMember(Order=16)]
        public decimal? BaseAmount
        {
            get { return this._baseAmount; }
            set { this._baseAmount = value; }
        }

        #endregion
    }
}
