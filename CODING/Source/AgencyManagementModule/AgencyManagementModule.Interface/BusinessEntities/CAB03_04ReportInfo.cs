using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
   public class CAB03_04ReportInfo
    {
        CAB03_HeaderReportInfo _header; 
        List<CAB03_DetailReportInfo> _details = new List<CAB03_DetailReportInfo>();


        [DataMember(Order=1)]
        public CAB03_HeaderReportInfo Header
        {
            get { return this._header; }
            set { this._header = value; }
        }


        [DataMember(Order=2)]
        public List<CAB03_DetailReportInfo> Details
        {
            get { return this._details; }
            set { this._details = value; }
        }
    }
}
