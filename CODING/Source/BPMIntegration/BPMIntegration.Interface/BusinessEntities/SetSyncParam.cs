using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [Serializable]
    public class SetSyncParam
    {
        private DateTime _syncDate;
        public DateTime SyncDate
        {
            get { return _syncDate; }
            set { _syncDate = value; }
        }

        private List<string> _syncBranches;
        public List<string> SyncBranches
        {
            get { return _syncBranches; }
            set { _syncBranches = value; }
        }

        public SetSyncParam() { }

        public SetSyncParam(DateTime date, List<string> branches)
        {
            _syncDate = date;
            _syncBranches = branches;
        }
    }
}
