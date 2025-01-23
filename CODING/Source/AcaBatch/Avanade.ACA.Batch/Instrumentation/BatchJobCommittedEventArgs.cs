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
    /// Summary for BatchJobCommittedEventArgs class
    /// </summary>
    public class BatchJobCommittedEventArgs : EventArgs
    {
        private long _countIncrement;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="countIncrement_">System.Int64</param>
        public BatchJobCommittedEventArgs(long countIncrement_)
        {
            _countIncrement = countIncrement_;
        }

        /// <summary>
        /// Gets Count increment
        /// </summary>
        public long CountIncrement
        {
            get
            {
                return _countIncrement;
            }
        }
    }
}
