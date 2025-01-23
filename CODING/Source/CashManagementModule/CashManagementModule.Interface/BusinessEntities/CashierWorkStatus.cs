using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace PEA.BPM.CashManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CashierWorkStatus
    {
        private string _closeWorkBy;
        private string _fullName;
        
        [DataMember(Order=1)]
        public string CloseWorkBy
        {
            set { _closeWorkBy = value; }
            get { return _closeWorkBy; }
        }


        [DataMember(Order=2)]
        public string FullName
        {
            set { _fullName = value; }
            get { return _fullName; }
        }

    }
}
