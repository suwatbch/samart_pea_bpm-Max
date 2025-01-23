using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CAB03_HeaderReportInfo
    {
        private string _printDate;
        private string _branchName;
        private string _period;
        private string _agencyTaxNo;
        private string _billBookNo;
        private string _agencyName;
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
            set { this._period = value ; }
        }

        [DataMember(Order=4)]
        public string AgencyTaxNo
        {
            get { return this._agencyTaxNo; }
            set { this._agencyTaxNo = value; }
        }

        [DataMember(Order=5)]
        public string BillBookNo
        {
            get { return this._billBookNo; }
            set { this._billBookNo = value; }
        }

        [DataMember(Order=6)]
        public string AgencyName
        {
            get { return this._agencyName; }
            set { this._agencyName = value; }
        }


        [DataMember(Order=7)]
        public bool PrintPreview
        {
            get { return this._printPreview; }
            set { this._printPreview = value; }
        }
    }
}
