using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class ReportParameterInfo
    {
        private object _paramerValue;
        private bool _printPerview;


        [DataMember(Order=1)]
        public object ParameterValue
        {
            get
            {
                return this._paramerValue;
            }
            set 
            {
                this._paramerValue = value;
            }
        }


        [DataMember(Order=2)]
        public bool PrintPreview
        {
            get 
            {
                return this._printPerview;
            }
            set 
            {
                this._printPerview = value;
            }
        }

    }
}
