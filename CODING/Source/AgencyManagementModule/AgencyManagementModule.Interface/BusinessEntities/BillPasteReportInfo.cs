using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.AgencyManagementModule.Interface.Constants;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class BillPasteReportInfo
    {
        #region "Local Variable"
        private string _mruId;
        private string _biilbbokRef;
        private string _electricNo;
        private string _billPeriod;
        private decimal? _baseAmount;
        private decimal? _electricPrice = 0;
        private decimal? _ftPrice = 0;
        private decimal? _vatValue = 0;
        private DateTime? _checkInDate;
        private string _pmId;
        #endregion

        #region "Property"

        [DataMember(Order=1)]
        public string MruId
        {
            get { return this._mruId; }
            set { this._mruId = value; }
        }

        [DataMember(Order=2)]
        public string BillBookRef
        {
            get { return this._biilbbokRef; }
            set { this._biilbbokRef = value; }
        }

        [DataMember(Order=3)]
        public string ElectricNo
        {
            get { return this._electricNo; }
            set { this._electricNo = value; }
        }

        [DataMember(Order=4)]
        public string BillPeroid
        {
            get { return this._billPeriod; }
            set { this._billPeriod = value; }
        }

        [DataMember(Order=5)]
        public decimal? ElectricPrice
        {
            get { return this._electricPrice; }
            set { this._electricPrice = value; }
        }

        [DataMember(Order=6)]
        public decimal? FTPrice
        {
            get { return this._ftPrice; }
            set { this._ftPrice = value; }
        }

        [DataMember(Order=7)]
        public decimal? VatPrice
        {
            get { return this._vatValue; }
            set { this._vatValue = value; }
        }

        [DataMember(Order=8)]
        public decimal? BaseAmount
        {
            get { return this._baseAmount; }
            set { this._baseAmount = value; }
        }


        [DataMember(Order=9)]
        public DateTime? CheckInDate
        {
            get { return this._checkInDate; }
            set { this._checkInDate = value; }
        }


        [DataMember(Order=10)]
        public string PmId
        {
            get { return this._pmId; }
            set { this._pmId = value; }
        }
        #endregion
    }
}
