using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities
{
    [DataContract]
    public class WorkStatus
    {
        private string _closedWorkBy;
        private string _cashierName;


        [DataMember(Order=1)]
        public string CloseWorkBy
        {
            get { return _closedWorkBy; }
            set { _closedWorkBy = value; }
        }


        [DataMember(Order=2)]
        public string CashierName
        {
            get { return _cashierName; }
            set { _cashierName = value; }
        }
    }
}
