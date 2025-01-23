using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.BillPrintingModule.Interface.BusinessEntities
{
    [DataContract]
    public class BarcodeMRU
    {
        string _billPred;


        [DataMember(Order=1)]
        public string BillPred
        {
            get { return _billPred; }
            set { _billPred = value; }
        }
        string _branchId;


        [DataMember(Order=2)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }
        string _mruId;


        [DataMember(Order=3)]
        public string MruId
        {
            get { return _mruId; }
            set { _mruId = value; }
        }
        bool _isPrinted;


        [DataMember(Order=4)]
        public bool IsPrinted
        {
            get { return _isPrinted; }
            set { _isPrinted = value; }
        }
        string _modifiedBy;


        [DataMember(Order=5)]
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }
        List<BarcodeMRU> list = new List<BarcodeMRU>();


        [DataMember(Order=6)]
        public List<BarcodeMRU> List
        {
            get { return list; }
            set { list = value; }
        }
    }
}
