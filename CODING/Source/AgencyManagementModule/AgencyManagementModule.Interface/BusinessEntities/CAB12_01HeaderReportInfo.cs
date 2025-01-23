using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CAB12_01HeaderReportInfo
    {
        private string _printDate = null;
        private string _branchName = null;
        private bool _printPreview;
        private int _past1_3Month = 0;
        private int _past4_6Month = 0;
        private int _past7_12Month = 0;
        private int _past1Year = 0;

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


        [DataMember(Order=4)]
        public int Past1_3Month
        {
            get { return this._past1_3Month; }
            set { this._past1_3Month = value; }
        }

        [DataMember(Order=5)]
        public int Past4_6Month
        {
            get { return this._past4_6Month; }
            set { this._past4_6Month = value; }
        }

        [DataMember(Order=6)]
        public int Past7_12Month
        {
            get { return this._past7_12Month; }
            set { this._past7_12Month = value; }
        }

        [DataMember(Order=7)]
        public int Past1Year
        {
            get { return this._past1Year; }
            set { this._past1Year = value; }
        }
    }
}
