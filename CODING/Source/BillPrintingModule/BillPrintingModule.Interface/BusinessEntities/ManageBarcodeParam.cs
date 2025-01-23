using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.BillPrintingModule.Interface.BusinessEntities
{
    [DataContract]
    public class ManageBarcodeParam
    {
        string _branchId;
        string _mruId;
        string _toMruId;


        [DataMember(Order=1)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }


        [DataMember(Order=2)]
        public string MruId
        {
            get { return _mruId; }
            set { _mruId = value; }
        }


        [DataMember(Order=3)]
        public string ToMruId
        {
            get { return _toMruId; }
            set { _toMruId = value; }
        }
    }
}
