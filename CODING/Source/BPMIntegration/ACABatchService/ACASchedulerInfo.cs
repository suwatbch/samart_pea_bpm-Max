using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Integration.ACABatchService
{
    public class Scheduler
    {
        int? _entityId;
        string _entityName;
        DateTime? _lastUpdateDtDt;
        string _jobType;
        bool? _active;

        public int? EntityId
        {
            get { return this._entityId; }
            set { this._entityId = value; }
        }

        public string EntityName
        {
            get { return this._entityName; }
            set { this._entityName = value; }
        }

        public DateTime? LastUpdateDt
        {
            get { return this._lastUpdateDtDt; }
            set { this._lastUpdateDtDt = value; }
        }

        public string JobType
        {
            get { return this._jobType; }
            set { this._jobType = value; }
        }

        public bool? Active
        {
            get { return this._active; }
            set { this._active = value; }
        }
    }
}
