using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CAN34_01CondtionReportInfo
    {
        private string _branchId;        
        private string _agencyId;
        private string _period;
        private bool _printPreview;


        [DataMember(Order=1)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }


        [DataMember(Order=2)]
        public string AgencyId
        {
            get { return this._agencyId; }
            set { this._agencyId = value; }
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
