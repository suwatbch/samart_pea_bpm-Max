using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CAB04_03HeaderReportInfo
    {
        private string _printDate;
        private string _branchName;
        private string _period;
        private string _agencyName;
        private string _registerDate;
        private string _calculateDate;
        private string _timeNo;
        private bool _printPreview;


        [DataMember(Order=1)]
        public string PrintDate
        {
            get { return this._printDate; }
            set { this._printDate = value; }
        }

        [DataMember(Order=2)]
        public string BranchName
        {
            get { return this._branchName; }
            set { this._branchName = value; }
        }

        [DataMember(Order=3)]
        public string Period
        {
            get { return this._period; }
            set { this._period = value; }
        }       

        [DataMember(Order=4)]
        public string AgencyName
        {
            get { return this._agencyName; }
            set { this._agencyName = value; }
        }

        [DataMember(Order=5)]
        public string RegisterDate
        {
            get { return this._registerDate; }
            set { this._registerDate = value; }
        }

        [DataMember(Order=6)]
        public string CalculateDate
        {
            get { return this._calculateDate; }
            set { this._calculateDate = value; }
        }


        [DataMember(Order=7)]
        public string TimeNo
        {
            get { return this._timeNo; }
            set { this._timeNo = value; }
        }

        [DataMember(Order=8)]
        public bool PrintPreview
        {
            get { return this._printPreview; }
            set { this._printPreview = value; }
        }

    }
}
