// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2001 - 2006 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Instrumentation;

namespace Avanade.ACA.Batch.Instrumentation
{
    /// <summary>
    /// Summary for BatchJobFailedEventArgs class
    /// </summary>
    public class BatchJobFailedEventArgs : EventArgs
    {
        
        private Guid _requestKey;
        private string _requestName;
        private string _status;
        private string _jobName;
        private int _sequence;        
        private Exception _exception;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requestKey">System.Guid</param>
        /// <param name="requestName">System.String</param>
        /// <param name="jobName">System.String</param>
        /// <param name="sequence">System.Int32</param>
        /// <param name="status">System.String</param>
        /// <param name="exception">System.Exception</param>
        public BatchJobFailedEventArgs(Guid requestKey, string requestName, string jobName, int sequence, string status, Exception exception)
        {
            _requestKey = requestKey;
            _requestName = requestName;
            _status = status;
            _jobName = jobName;
            _sequence = sequence;
            _exception = exception;
        }

        # region public properties
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
        /// 
        /// </summary>
        [IgnoreMember]
        public Exception Exception
        {
            get
            {
                return _exception;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ExceptionStackTrace
        {
            get
            {
                return _exception == null ? string.Empty : _exception.StackTrace;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ExceptionMessage
        {
            get
            {
                return _exception == null ? string.Empty : this._exception.Message;
            }
        }
        #endregion
    }
}
