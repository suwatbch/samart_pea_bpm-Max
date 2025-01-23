using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
   public class CAB05_01ConditionReportInfo
    {
        private string _branchId = null;
        private string _startAgencyId = null;
        private string _endAgencyId = null;
        private string _startPeriod = null;
        private string _endPeriod = null;
       private bool _printPreview;


        [DataMember(Order=1)]
       public string BranchId
       {
           get { return this._branchId; }
           set { this._branchId = value; }
       }

        [DataMember(Order=2)]
       public string StartAgencyId 
       {
           get { return this._startAgencyId; }
           set { this._startAgencyId = value; }
       }

        [DataMember(Order=3)]
       public string EndAgencyId
       {
           get { return this._endAgencyId; }
           set { this._endAgencyId = value; }
       }

        [DataMember(Order=4)]
       public string StartPeriod
       {
           get { return this._startPeriod;}
           set { this._startPeriod = value;}
       }

        [DataMember(Order=5)]
       public string EndPeriod
       {
           get { return this._endPeriod;}
           set { this._endPeriod = value;}
       }


        [DataMember(Order=6)]
       public bool PrintPreview
       {
           get { return this._printPreview; }
           set { this._printPreview = value; }
       }

    }
}
