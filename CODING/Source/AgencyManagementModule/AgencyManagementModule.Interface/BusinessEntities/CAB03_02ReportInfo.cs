using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    // Optimize web service
    [DataContract]
    public class CAB03_02ReportInfo
    {
        #region "Local variable"
        CAB03_HeaderReportInfo _header;
        CAB03_02DetailReportInfo _myDeatil;
        #endregion

        #region "Public variable"

        [DataMember(Order=1)]
        public CAB03_HeaderReportInfo Header
        {
            get { return this._header; }
            set { this._header = value; }
        }


        [DataMember(Order=2)]
        public CAB03_02DetailReportInfo Detail
        {
            get { return this._myDeatil; }
            set { this._myDeatil = value; }
        }
        #endregion


    }
}
