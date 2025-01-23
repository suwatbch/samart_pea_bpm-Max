using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.BillPrintingModule.Interface.BusinessEntities
{
    [DataContract]
    public class ReportBillingStatus
    {
        private string _branchId;
        private string _branchName;
        private string _mruId;
        private string _portionId;
        private string _billPred;
        private int? _baseCount;
        private int? _procCount;
        private int? _actReceived;
        private int? _typeCount; //count for any specific bill type
        private int? _printCount;

        private int? _nBillCount;
        private int? _fBillCount;
        private int? _nA4AddCount;
        private int? _fA4AddCount;


        [DataMember(Order=1)]
        public int? NBillCount
        {
            get { return _nBillCount; }
            set { _nBillCount = value; }
        }


        [DataMember(Order=2)]
        public int? FBillCount
        {
            get { return _fBillCount; }
            set { _fBillCount = value; }
        }


        [DataMember(Order=3)]
        public int? NA4AddCount
        {
            get { return _nA4AddCount; }
            set { _nA4AddCount = value; }
        }


        [DataMember(Order=4)]
        public int? FA4AddCount
        {
            get { return _fA4AddCount; }
            set { _fA4AddCount = value; }
        }


        [DataMember(Order=5)]
        public string BranchId
        {
            set { _branchId = value; }
            get { return _branchId; }
        }


        [DataMember(Order=6)]
        public string BranchName
        {
            set { _branchName = value; }
            get { return _branchName; }
        }


        [DataMember(Order=7)]
        public string MruId
        {
            set { _mruId = value; }
            get { return _mruId; }
        }


        [DataMember(Order=8)]
        public string PortionId
        {
            set { _portionId = value; }
            get { return _portionId; }
        }


        [DataMember(Order=9)]
        public string BillPred
        {
            set { _billPred = value; }
            get { return _billPred; }
        }


        [DataMember(Order=10)]
        public int? BaseCount
        {
            set { _baseCount = value; }
            get { return _baseCount; }
        }


        [DataMember(Order=11)]
        public int? ProcCount
        {
            set { _procCount = value; }
            get { return _procCount; }
        }


        [DataMember(Order=12)]
        public int? ActReceived
        {
            set { _actReceived = value; }
            get { return _actReceived; }
        }


        [DataMember(Order=13)]
        public int? TypeCount
        {
            set { _typeCount = value; }
            get { return _typeCount; }
        }


        [DataMember(Order=14)]
        public int? PrintCount
        {
            set { _printCount = value; }
            get { return _printCount; }
        }



    }
}
