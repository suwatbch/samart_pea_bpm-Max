using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CAB02_HederReportInfo
    {
        private string _printDate;    
        private string _branchName;
        private bool _printPreview;
        private string _agencyIdFrom;
        private string _agencyIdTo;
        private string _periodFrom;
        private string _periodTo;
       
        private List<CAB02_DetailReportInfo> _dataList = new List<CAB02_DetailReportInfo>();


        [DataMember(Order=1)]
        public string  PrintDate
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
        public List<CAB02_DetailReportInfo> DataList
        {
            get { return this._dataList; }
            set { this._dataList = value; }
        }


        [DataMember(Order=4)]
        public bool PrintPreview
        {
            get { return this._printPreview; }
            set { this._printPreview = value; }
        }

        [DataMember(Order=5)]
        public string AgencyIdFrom
        {
            get { return this._agencyIdFrom; }
            set { this._agencyIdFrom = value ; }
        }

        [DataMember(Order=6)]
        public string AgencyIdTo
        {
            get { return this._agencyIdTo; }
            set { this._agencyIdTo = value; }
        }

        [DataMember(Order=7)]
        public string PeriodFrom
        {
            get { return this._periodFrom; }
            set { this._periodFrom = value; }
        }

        [DataMember(Order=8)]
        public string PeriodTo
        {
            get { return this._periodTo; }
            set { this._periodTo = value; }
        }
    }
}
