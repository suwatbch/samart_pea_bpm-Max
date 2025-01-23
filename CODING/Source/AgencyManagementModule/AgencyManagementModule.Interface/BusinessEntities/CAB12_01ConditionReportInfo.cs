using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CAB12_01ConditionReportInfo
    {
        private string _branchId = null;
        private string _agencyIdFrom = null;
        private string _agencyIdTo = null;    
        private DateTime? _startPeriod;
        private DateTime? _endPeriod;
        private string _currPeriod;
        private bool _printPreview;


        [DataMember(Order=1)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }


        [DataMember(Order=2)]
        public string AgencyIdFrom
        {
            get { return this._agencyIdFrom; }
            set { this._agencyIdFrom = value; }
        }


        [DataMember(Order=3)]
        public string AgencyIdTo
        {
            get { return this._agencyIdTo; }
            set { this._agencyIdTo = value; }
        }



        [DataMember(Order=4)]
        public DateTime? StartPeriod
        {
            get { return this._startPeriod; }
            set { this._startPeriod = value; }
        }


        [DataMember(Order=5)]
        public DateTime? EndPeriod
        {
            get { return this._endPeriod; }
            set { this._endPeriod = value; }
        }


        [DataMember(Order=6)]
        public bool PrintPreview
        {
            get { return this._printPreview; }
            set { this._printPreview = value; }
        }


        [DataMember(Order=7)]
        public string CurrPeriod
        {
            get { return this._currPeriod; }
            set { this._currPeriod = value; }
        }
    }
}
