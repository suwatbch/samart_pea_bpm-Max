using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.AgencyManagementModule.Interface.Constants;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class TravelHelpRateConditionInfo
    {
        private bool _isReprint;
        private string _branchId;
        private string _bookPeriod;
        private string _agencyId;
        private int _travelHelpType;
        private DateTime _createDate;
        private DateTime  _calculateDate;


        [DataMember(Order=1)]
        public bool IsReprint
        {
            get { return this._isReprint; }
            set { this._isReprint = value; }
        }


        [DataMember(Order=2)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }


        [DataMember(Order=3)]
        public string BookPeriod
        {
            get { return this._bookPeriod; }
            set { this._bookPeriod = value; }
        }


        [DataMember(Order=4)]
        public string AgencyId
        {
            get { return this._agencyId; }
            set { this._agencyId = value; }
        }


        [DataMember(Order=5)]
        public DateTime CreateDate
        {
            get { return this._createDate; }
            set { this._createDate = value; }
        }


        [DataMember(Order=6)]
        public DateTime CalculateDate
        {
            get { return this._calculateDate; }
            set { this._calculateDate = value; }
        }


        [DataMember(Order=7)]
        public int TravelHelpType
        {
            get { return this._travelHelpType; }
            set { this._travelHelpType = value; }
        }
    }
}
