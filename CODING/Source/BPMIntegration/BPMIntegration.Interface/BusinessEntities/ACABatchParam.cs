using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    public class ACABatchParam
    {
        private Guid _batchKey;
        private string _fileName;
        private bool _overwrite;
        private string _branchId;
        private string _shortFileName;
        private string _bulkFormatPath;

        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }

        public Guid BatchKey
        {
            get { return _batchKey; }
            set { _batchKey = value; }
        }

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        public bool Overwrite
        {
            get { return _overwrite; }
            set { _overwrite = value; }
        }

        public string ShortFileName
        {
            get { return _shortFileName; }
            set { _shortFileName = value; }
        }

        public string BulkFormatPath
        {
            get { return _bulkFormatPath; }
            set { _bulkFormatPath = value; }
        }

    }
}
