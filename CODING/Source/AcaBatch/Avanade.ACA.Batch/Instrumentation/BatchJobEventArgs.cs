// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2001 - 2006 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace Avanade.ACA.Batch.Instrumentation
{
    /// <summary>
    /// Summary for BatchJobEventArgs class
    /// </summary>
    public class BatchJobEventArgs : EventArgs
    {
        private Guid _requestKey;
        private string _requestName;
        private string _status;
        private string _jobName;
        private int _sequence;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requestKey">System.Guid</param>
        /// <param name="requestName">System.String</param>
        /// <param name="jobName">System.String</param>
        /// <param name="status">System.String</param>
        /// <param name="sequence">System.Int32</param>
        public BatchJobEventArgs(Guid requestKey, string requestName, string jobName, string status, int sequence)
        {
            _requestKey = requestKey;
            _requestName = requestName;
            _status = status;
            _jobName = jobName;
            _sequence = sequence;
        }

        /// <summary>
        /// Gets RequestKey
        /// </summary>
        public Guid RequestKey
        {
            get
            {
                return this._requestKey;
            }
        }

        /// <summary>
        /// Gets RequestName
        /// </summary>
        public string RequestName
        {
            get
            {
                return this._requestName;
            }
        }

        /// <summary>
        /// Gets Status
        /// </summary>
        public string Status
        {
            get
            {
                return this._status;
            }
        }

        /// <summary>
        /// Gets JobName
        /// </summary>
        public string JobName
        {
            get
            {
                return _jobName;
            }
        }

        /// <summary>
        /// Gets Sequence
        /// </summary>
        public int Sequence
        {
            get
            {
                return _sequence;
            }
        }
    }
}
