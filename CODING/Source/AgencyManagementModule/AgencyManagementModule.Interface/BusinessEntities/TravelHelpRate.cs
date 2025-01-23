using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.AgencyManagementModule.Interface.Constants;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class TravelHelpRate
    {
        private DateTime _createDt;
        private string _billBookId;
        private string _period;
        private string _agencyId;
        private string _mruId;
        private int _travelHelpType;
        //private decimal? _travelHelp = 0;
        private decimal? _farlandHelp = 0;
        private decimal? _waterHelp = 0;
        private decimal? _transportHelp = 0;
        private decimal? _extraMoneyHelp = 0;
        private decimal? _collectedPercent = 0;
        private int? _caCount = 0;
        private decimal? _upperRate = 0;
        private decimal? _lowerRate = 0;
        //private DateTime? _helpValidFrom;
        //private DateTime? _helpValidTo;



        [DataMember(Order=1)]
        public DateTime CreateDt
        {
            get { return this._createDt; }
            set { this._createDt = value; }
        }


        [DataMember(Order=2)]
        public string BillBookId
        {
            get { return this._billBookId; }
            set { this._billBookId = value; }
        }


        [DataMember(Order=3)]
        public string Period
        {
            get { return this._period; }
            set { this._period = value; }
        }


        [DataMember(Order=4)]
        public string AgencyId
        {
            get
            {
                string retVal = _agencyId;
                if ((retVal != null && retVal.Length > ModuleConfigurationNames.AgentIdLength))
                    retVal = _agencyId.Substring(6, 6);
                return retVal;
            }
            set { this._agencyId = value; }
        }


        [DataMember(Order=5)]
        public string MURId
        {
            get { return this._mruId; }
            set { this._mruId = value; }
        }

        //public decimal? TravelHelp
        //{
        //    get { return this._travelHelp; }
        //    set { this._travelHelp = value; }
        //}


        [DataMember(Order=6)]
        public int TravelHelpType
        {
            get { return this._travelHelpType; }
            set { this._travelHelpType = value; }
        }


        [DataMember(Order=7)]
        public decimal? TransportHelp
        {
            get { return this._transportHelp; }
            set { this._transportHelp = value; }
        }


        [DataMember(Order=8)]
        public decimal? FarlandHelp
        {
            get { return this._farlandHelp; }
            set { this._farlandHelp = value; }
        }

        [DataMember(Order=9)]
        public decimal? WaterHelp
        {
            get { return this._waterHelp; }
            set { this._waterHelp = value; }
        }


        [DataMember(Order=10)]
        public decimal? ExtraMoneyHelp
        {
            get { return this._extraMoneyHelp; }
            set { this._extraMoneyHelp = value; }
        }


        [DataMember(Order=11)]
        public decimal? CollectedPercent
        {
            get { return this._collectedPercent; }
            set { this._collectedPercent = value; }
        }

        [DataMember(Order=12)]
        public int? CaCount
        {
            get { return this._caCount; }
            set { this._caCount = value; }
        }

        [DataMember(Order=13)]
        public decimal? UpperRate
        {
            get { return this._upperRate; }
            set { this._upperRate = value; }
        }
        

        [DataMember(Order=14)]
        public decimal? LowerRate
        {
            get { return this._lowerRate; }
            set { this._lowerRate = value; }
        }

        //public DateTime? HelpValidFrom
        //{
        //    get { return this._helpValidFrom; }
        //    set { this._helpValidFrom = value; }
        //}
        //public DateTime? HepValidTo
        //{
        //    get { return this._helpValidTo; }
        //    set { this._helpValidTo = value; }
        //}

    }
}
