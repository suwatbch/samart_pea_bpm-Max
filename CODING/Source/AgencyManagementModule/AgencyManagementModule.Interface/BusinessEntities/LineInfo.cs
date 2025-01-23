using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using PEA.BPM.AgencyManagementModule.Interface.Constants;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    /// <summary>
    /// Find out what detail of the SAP - Portion/MRU that suitable to be seach keys
    /// </summary>
    [DataContract]
    public class LineInfo
    {
        private string _peaCode;
        private string _id;
        private int _numOfCustomer;       
        private string _portionDesc;
        private string _agentId;
        private string _agentName;
        private bool _advPayment;
        private string _controllerId;
        private string _modifiedBy;
        private DateTime? _modifiedDt;
        private bool _agNeedUpdate = false;
        private bool _advNeedUpdate = false;
        private int? _travelHelp;
        //private DateTime? _validFrom;
        //private DateTime? _validTo;
        private int? _collectCount = 0;
        private string _agencyName;        

        public LineInfo Clone()
        {
            LineInfo that = new LineInfo();
            that.BranchId = this.BranchId;
            that.LineId = this.LineId;
            that.NumOfCustomer = this.NumOfCustomer;
            that.PortionDesc = this.PortionDesc;
            that.AgentId = this.AgentId;
            that.AgentName = this.AgentName;
            that.AgencyName = this.AgencyName;
            that.AdvPayment = this.AdvPayment;
            that.TravelHelp = this.TravelHelp;
            that.CollectCount = this.CollectCount;
            that.ModifiedBy = this.ModifiedBy;
            that.ModifiedDt = this.ModifiedDt;
            that.ControllerId = this.ControllerId;
            return that;
        }


        [DataMember(Order=1)]
        public string BranchId
        {
            get { return _peaCode; }
            set { _peaCode = value; }
        }


        [DataMember(Order=2)]
        public string LineId
        {
            get { return _id; }
            set { _id = value; }
        }


        [DataMember(Order=3)]
        public string PortionDesc
        {
            get { return _portionDesc; }
            set { _portionDesc = value; }
        }


        [DataMember(Order=4)]
        public int NumOfCustomer
        {
            get { return _numOfCustomer; }
            set { _numOfCustomer = value; }
        }           

        [DataMember(Order=5)]
        public bool AdvPayment
        {
            get { return _advPayment; }
            set { _advPayment = value; }
        }


        [DataMember(Order=6)]
        public string ControllerId
        {
            get { return _controllerId; }
            set { _controllerId = value; }
        }

        [DataMember(Order=7)]
        public string AgentId
        {
            get { return _agentId; }
            set { _agentId = value; }
        }


        [DataMember(Order=8)]
        public string AgentName
        {
            get { return _agentName; }
            set { _agentName = value; }
        }

        //Checked TongKung
        public string AgentLongName
        {
            get { return string.Format("{0}: {1}", AgentId, AgentName); }
        }


        [DataMember(Order=9)]
        public DateTime? ModifiedDt
        {
            get { return _modifiedDt; }
            set { _modifiedDt = value; }
        }


        [DataMember(Order=10)]
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }


        [DataMember(Order=11)]
        public bool AgentNeedUpdate
        {
            get { return _agNeedUpdate; }
            set { _agNeedUpdate = value; }
        }


        [DataMember(Order=12)]
        public bool AdvNeedUpdate
        {
            get { return _advNeedUpdate; }
            set { _advNeedUpdate = value; }
        }


        [DataMember(Order=13)]
        public int? TravelHelp
        {
            get { return _travelHelp; }
            set { _travelHelp = value; }
        }

        //public DateTime? ValidFrom
        //{
        //    get { return this._validFrom; }
        //    set { this._validFrom = value;}
        //}
        //public DateTime? ValidTo
        //{
        //    get { return this._validTo; }
        //    set { this._validTo = value; }
        //}

        //public string ValidFromText
        //{
        //    get
        //    {
        //        string retVal = String.Empty;
        //        if (_validFrom != null)
        //            retVal = _validFrom.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
        //        return retVal;
        //    }
        //}

        // public string ValidToText
        //{
        //    get
        //    {
        //        string retVal = String.Empty;
        //        if (_validTo != null)
        //            retVal = _validTo.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
        //        return retVal;
        //    }
        //}


        [DataMember(Order=14)]
        public int? CollectCount
        {
            get 
            {
                return this._collectCount;
            }
            set 
            {
                this._collectCount = value;
            }
        }


        [DataMember(Order=15)]
        public string AgencyName
        {
            get { return this._agencyName; }
            set { this._agencyName = value; }
        }
    }
}
