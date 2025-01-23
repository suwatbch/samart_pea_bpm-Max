using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class KeepMoneyPlaneHeaderInfoReport
    {
        #region "Local Variable"
        private string _printDate;
        private string _branchMasterId;
        private string _btanchMasterName;       
        private string _period;
        private bool _printPreview;
        #endregion

        #region "Property"

        [DataMember(Order=1)]
        public string PrintDate
        {
            get { return _printDate; }
            set { this._printDate = value; }
        }      

        [DataMember(Order=2)]
        public string BranchMasterId
        {
            get { return _branchMasterId; }
            set { this._branchMasterId = value; }
        }

        [DataMember(Order=3)]
        public string BranchMasterName
        {
            get { return _btanchMasterName; }
            set { this._btanchMasterName = value; }
        }      

        [DataMember(Order=4)]
        public string Period
        {
            get { return _period; }
            set { this._period = value; }
        }


        [DataMember(Order=5)]
        public bool PrintPreview
        {
            get { return this._printPreview; }
            set { this._printPreview = value; }
        }
      
        #endregion
    }
}
