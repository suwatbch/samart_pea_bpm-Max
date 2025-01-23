using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class PA_7034ConditionReportInfo
    {
        private string _branchId = String.Empty;

        private string _agencyIdFrom = null;
        private string _agencyIdTo = null;
        private string _period = null;
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
            get { return this._agencyIdFrom;}
            set { this._agencyIdFrom = value; }
        }


        [DataMember(Order=3)]
        public string AgencyIdTo
        {
            get { return this._agencyIdTo; }
            set { this._agencyIdTo = value; }
        }


        [DataMember(Order=4)]
        public string Period
        {
            get { return this._period; }
            set { this._period = value; }
        }


        [DataMember(Order=5)]
        public bool PrintPreview
        {
            get { return this._printPreview; }
            set { this._printPreview = value; }
        }
    }
}
