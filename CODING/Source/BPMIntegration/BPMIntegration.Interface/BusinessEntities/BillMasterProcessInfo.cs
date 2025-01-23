using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class PWBBilzlStatusInfo
    {
        private string _branchId;
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }

        private string _mruId;
        public string MruId
        {
            get { return _mruId; }
            set { _mruId = value; }
        }

        private string _billPred;
        public string BillPred
        {
            get { return _billPred; }
            set { _billPred = value; }
        }

        private string _portionId;
        public string PortionId
        {
            get { return _portionId; }
            set { _portionId = value; }
        }

        private int? _baseCount;
        public int? BaseCount
        {
            get { return _baseCount; }
            set { _baseCount = value; }
        }

        private int? _procCount;
        public int? ProcCount
        {
            get { return _procCount; }
            set { _procCount = value; }
        }

        private int? _fixCount;
        public int? FixCount
        {
            get { return _fixCount; }
            set { _fixCount = value; }
        }

        private string _modifiedBy;
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        private DateTime? _modifiedDt;
        public DateTime? ModifiedDt
        {
            get { return _modifiedDt; }
            set { _modifiedDt = value; }
        }

        private bool? _active;
        public bool? Active
        {
            get { return _active; }
            set { _active = value; }
        }

        private string _branchName;
        public string BranchName
        {
            get { return _branchName; }
            set { _branchName = value; }
                
        }

    }
}
