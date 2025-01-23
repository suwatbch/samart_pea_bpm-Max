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
    /// Summary for BatchJobWorkUnitCompletedEventArgs class
    /// </summary>
    public class BatchJobWorkUnitCompletedEventArgs : EventArgs
    {
        private long[] _counterIncrements;
        private TimeSpan _executionDuration;
        private int _workUnits;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="duration_">System.TimeSpan</param>
        /// <param name="workUnits_">System.Int32</param>
        public BatchJobWorkUnitCompletedEventArgs(TimeSpan duration_, int workUnits_)
        {
            this._executionDuration = duration_;
            this._workUnits = workUnits_;            
            long[] counterIncrements = new long[] { duration_.Ticks, (long)workUnits_ };
            this._counterIncrements = counterIncrements;
        }

        /// <summary>
        /// Gets Counter Increments
        /// </summary>
        public long[] CounterIncrements
        {
            get
            {
                return this._counterIncrements;
            }
        }
        
        /// <summary>
        /// Gets WorkUnits
        /// </summary>
        public int WorkUnits
        {
            get
            {
                return this._workUnits;
            }
        }

        /// <summary>
        /// Gets ExecutionDuration
        /// </summary>
        public TimeSpan ExecutionDuration
        {
            get
            {
                return _executionDuration;
            }
        }
    }
}
