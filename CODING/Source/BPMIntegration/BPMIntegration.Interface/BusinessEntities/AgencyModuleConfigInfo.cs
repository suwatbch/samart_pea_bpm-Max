using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [Serializable]
    public class AgencyModuleConfigInfo
    {
        private string _debitAcc;
        public string DebitAcc
        {
            get { return _debitAcc; }
            set { _debitAcc = value; }
        }

        private string _cmCountBlock;
        public string CmCountBlock
        {
            get { return _cmCountBlock; }
            set { _cmCountBlock = value; }
        }

        private int? _cmCountLimit;
        public int? CmCountLimit
        {
            get { return _cmCountLimit; }
            set { _cmCountLimit = value; }
        }

        private Decimal? _vatRate;
        public Decimal? VatRate
        {
            get { return _vatRate; }
            set { _vatRate = value; }
        }

        private Decimal? _taxRate;
        public Decimal? TaxRate
        {
            get { return _taxRate; }
            set { _taxRate = value; }
        }

        private Decimal? _collectedPercent;
        public Decimal? CollectedPercent
        {
            get { return _collectedPercent; }
            set { _collectedPercent = value; }
        }

        private int? _caCount;
        public int? CaCount
        {
            get { return _caCount; }
            set { _caCount = value; }
        }

        private Decimal? _upperRate;
        public Decimal? UpperRate
        {
            get { return _upperRate; }
            set { _upperRate = value; }
        }

        private Decimal? _lowerRate;
        public Decimal? LowerRate
        {
            get { return _lowerRate; }
            set { _lowerRate = value; }
        }

        private string _penaltyWaiveFlag;
        public string PenaltyWaiveFlag
        {
            get { return _penaltyWaiveFlag; }
            set { _penaltyWaiveFlag = value; }
        }

        private string _syncFlag;
        public string SyncFlag
        {
            get { return _syncFlag; }
            set { _syncFlag = value; }
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



    }
}
