using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class KeepMoneyPlaneOfAgencyConditionInfoReport
    {
        #region "Local Variabled"
        private string _peaCode;
        private string _peaName;
        private string _period;
        private string _modifiedBy;
        private bool _printPreview;
        private string _baCode;
        private string _branchCon;
        #endregion

        #region "Property"

        [DataMember(Order=1)]
        public string PEACode
        {
            get { return _peaCode; }
            set { this._peaCode = value; }
        }


        [DataMember(Order=2)]
        public string PEAName
        {
            get { return _peaName; }
            set { _peaName = value; }
        }

        [DataMember(Order=3)]
        public string Period
        {
            get { return _period; }
            set { this._period = value; }
        }


        [DataMember(Order=4)]
        public string ModifiedBy
        {
            get { return this._modifiedBy; }
            set { this._modifiedBy = value; }
        }


        [DataMember(Order=5)]
        public bool PrintPreview
        {
            get { return this._printPreview; }
            set { this._printPreview = value; }
        }


        [DataMember(Order=6)]
        public string BaCode
        {
            get { return this._baCode; }
            set { this._baCode = value; }
        }


        [DataMember(Order=7)]
        public string BranchCon
        {
            get { return this._branchCon; }
            set { this._branchCon = value; }
        }

        #endregion
    }
}
