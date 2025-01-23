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
    /// Summary for BatchRequestPausedEventArgs class
    /// </summary>
    public class BatchRequestPausedEventArgs : EventArgs
    {
        private Guid _requestKey;
        private string _requestName;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requestKey">System.Guid</param>
        /// <param name="requestName">System.String</param>
        public BatchRequestPausedEventArgs(Guid requestKey, string requestName)
        {
            _requestKey = requestKey;
            _requestName = requestName;            
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
    }
}
