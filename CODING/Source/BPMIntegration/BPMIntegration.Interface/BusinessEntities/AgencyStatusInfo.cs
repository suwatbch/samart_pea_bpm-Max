using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    public class AgencyStatusInfo
    {
        string _asId;
        public string AsId
        {
            get { return this._asId; }
            set { this._asId = value; }
        }

        string _asDesc;
        public string AsDesc
        {
            get { return this._asDesc; }
            set { this._asDesc = value; }
        }

        string _syncFlag;
        public string SyncFlag
        {
            get { return this._syncFlag; }
            set { this._syncFlag = value; }
        }

        DateTime? _modifiedDt;
        public DateTime? ModifiedDt
        {
            get { return this._modifiedDt; }
            set { this._modifiedDt = value; }
        }

        string _modifiedBy;
        public string ModifiedBy
        {
            get { return this._modifiedBy; }
            set { this._modifiedBy = value; }
        }

        bool _active;
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

    }
}
