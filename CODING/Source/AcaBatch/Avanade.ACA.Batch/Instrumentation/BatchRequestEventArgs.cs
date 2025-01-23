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
    /// Summary for BatchRequestEventArgs class
    /// </summary>
    public class BatchRequestEventArgs : EventArgs
    {
        private Guid _requestKey;
        private string _requestName;
        private string _status;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requestKey">System.Guid</param>
        /// <param name="requestName">System.String</param>
        /// <param name="status">System.String</param>
        public BatchRequestEventArgs(Guid requestKey, string requestName, string status)
        {
            _requestKey = requestKey;
            _requestName = requestName;
            _status = status;
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
    }
}
