using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Integration.ACABatchService
{
    public class ACADepLineInfo
    {
        private string _dlId;
        private string _fileKey;
        private string _status;
        private string _lastFailTb;

        public string DLId
        {
            get { return _dlId; }
            set { _dlId = value; }
        }

        public string FileKey
        {
            get { return _fileKey; }
            set { _fileKey = value; }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public string LastFailTb
        {
            get { return _lastFailTb; }
            set { _lastFailTb = value; }
        }


    }
}
