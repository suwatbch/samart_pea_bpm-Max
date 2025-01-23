using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ToolModule.Interface.BusinessEntities
{
    [DataContract]
    public class SyncCrossCheckedParam
    {
        string _billPred;
        string _branchId;
        string _reportType;


        [DataMember(Order=1)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }


        [DataMember(Order=2)]
        public string ReportType
        {
            get { return _reportType; }
            set { _reportType = value; }
        }


        [DataMember(Order=3)]
        public string BillPred
        {
            get { return _billPred; }
            set { _billPred = value; }
        }
    }
}
