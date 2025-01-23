using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CAN34_01HeaderReportInfo
    {
        private string _printDate = null;
        private string _branchName = null;
        private string _period = null;
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
        public bool PrintPreview
        {
            get { return this._printPreview; }
            set { this._printPreview = value; }
        }
    }
}
