using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class PA_7034HeaderReportInfo
    {
        bool _printPreview;
        string _period = null;
        string _branchName = null;
        string _printDate = null;



        [DataMember(Order=1)]
        public bool PrintPreview
        {
            get { return this._printPreview; }
            set { this._printPreview = value; }
        }


        [DataMember(Order=2)]
        public string Period
        {
            get { return this._period; }
            set { this._period = value; }
        }


        [DataMember(Order=3)]
        public string BranchName
        {
            get { return this._branchName; }
            set { this._branchName = value; }
        }


        [DataMember(Order=4)]
        public string PrintDate
        {
            get { return this._printDate; }
            set { this._printDate = value; }
        }
    }
}
