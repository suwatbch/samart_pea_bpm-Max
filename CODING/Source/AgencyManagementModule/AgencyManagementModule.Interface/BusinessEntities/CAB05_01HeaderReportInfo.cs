using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CAB05_01HeaderReportInfo
    {
        string _branchName = null;
        bool _printPreview;
        string _printDate = null;


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
        public bool PrintPreview
        {
            get { return this._printPreview; }
            set { this._printPreview = value; }
        }
    }
}
