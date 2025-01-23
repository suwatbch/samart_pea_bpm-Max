using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    public class BillingDetailStatusInfo : IListUtility<BillingDetailStatusInfo>
    {
        private string _branchId;
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }

        private string _portionId;
        public string PortionId
        {
            get { return _portionId; }
            set { _portionId = value; }
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


        #region IListUtility<BillingDetailStatusInfo> Members

        public string ToStream()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public BillingDetailStatusInfo ParseExtract(string txt)
        {
            BillingDetailStatusInfo obj = new BillingDetailStatusInfo();

            string[] sp = txt.Split('|');

            obj.BranchId = StringConvert.ToString(sp[1]);
            obj.PortionId = StringConvert.ToString(sp[2]);
            obj.MruId = StringConvert.ToString(sp[3]);
            obj.BillPred = StringConvert.ToString(sp[4]);
            obj.BaseCount = StringConvert.ToInt32(sp[5]);
            obj.ProcCount = StringConvert.ToInt32(sp[6]);
            obj.FixCount = StringConvert.ToInt32(sp[7]);
            obj.ModifiedBy = "BATCH";

            return obj;
        }

        #endregion
    }
}
