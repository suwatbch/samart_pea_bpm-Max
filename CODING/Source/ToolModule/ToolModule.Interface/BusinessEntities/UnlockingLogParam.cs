using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ToolModule.Interface.BusinessEntities
{
    [DataContract]
    public class UnlockingLogParam
    {
        private DateTime _billPred;      
        private string[] _branchId;
        private string _fncId;


        [DataMember(Order=1)]
        public string[] BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }


        [DataMember(Order=2)]
        public DateTime BillPred
        {
            get { return _billPred; }
            set { _billPred = value; }
        }


        [DataMember(Order=3)]
        public string FunctionId
        {
            get { return _fncId; }
            set { _fncId = value; }
        }
    }
}
