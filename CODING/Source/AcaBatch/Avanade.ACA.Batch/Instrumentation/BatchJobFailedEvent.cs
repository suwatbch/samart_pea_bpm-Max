// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Diagnostics;
using System.Threading;

using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation;

namespace Avanade.ACA.Batch.Instrumentation
{
	/// <summary>
	/// Summary description for BatchJobFailedEvent.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class BatchJobFailedEvent : BatchJobEvent
	{
        private Exception _exception;

        /// <summary>BatchJobFailedEvent</summary>
        /// <param name="requestKey_">System.Guid</param>
        /// <param name="requestName_">System.String</param>
        /// <param name="jobName_">System.String</param>
        /// <param name="sequence_">System.Int32</param>
        /// <param name="status_">System.String</param>
        /// <param name="ex_">System.Exception</param>
        public BatchJobFailedEvent(Guid requestKey_, string requestName_, string jobName_, int sequence_, string status_, Exception ex_)
            : base(requestKey_, requestName_, jobName_, status_, sequence_)
        {
            this._exception = ex_;
        }
       
        #region Public Properties
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
                return this._exception.Message;
			}
		}
		#endregion
	}
}
