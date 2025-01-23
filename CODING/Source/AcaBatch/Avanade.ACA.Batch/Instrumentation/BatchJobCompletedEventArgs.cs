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
    /// Summary for BatchJobCompletedEventArgs class
    /// </summary>
    public class BatchJobCompletedEventArgs : EventArgs
    {
        private Guid _requestKey;
        private string _requestName;
        private string _status;
        private string _jobName;
        private int _sequence;
        private DateTime _startTime;
        private TimeSpan _executionDuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requestKey">System.Guid</param>
        /// <param name="requestName">System.String</param>
        /// <param name="jobName">System.String</param>
        /// <param name="status">System.String</param>
        /// <param name="sequence">System.Int32</param>
        /// <param name="startTime">System.DateTime</param>
        public BatchJobCompletedEventArgs(Guid requestKey, string requestName, string jobName, string status, int sequence, DateTime startTime)
        {
            _requestKey = requestKey;
            _requestName = requestName;
            _status = status;
            _jobName = jobName;
            _sequence = sequence;
            _startTime = startTime;
            DateTime endTime = DateTime.Now;
            this._executionDuration = endTime.Subtract(startTime);
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

        /// <summary>
        /// Gets StartTime
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                return this._startTime;
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
