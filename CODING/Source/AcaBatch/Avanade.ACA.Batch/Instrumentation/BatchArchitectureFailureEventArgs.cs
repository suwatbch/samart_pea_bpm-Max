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
    /// Summary for BatchArchitectureFailureEventArgs class
    /// </summary>
    public class BatchArchitectureFailureEventArgs : EventArgs
    {
        private string _message;
        private Exception _exception;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">System.String</param>
        /// <param name="exception">System.Exception</param>
        public BatchArchitectureFailureEventArgs(string message, Exception exception)
        {
            _message = message;
            _exception = exception;
        }

        /// <summary>
        /// Gets Message
        /// </summary>
        public string Message
        {
            get
            {
                return _message;
            }
        }

        /// <summary>
        /// Gets Exception
        /// </summary>
        [IgnoreMember]
        public Exception Exception
        {
            get
            {
                return _exception;
            }
        }
    }
}
