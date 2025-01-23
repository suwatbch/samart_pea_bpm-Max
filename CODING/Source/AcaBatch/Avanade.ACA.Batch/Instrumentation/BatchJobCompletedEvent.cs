// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Threading;

using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation;

namespace Avanade.ACA.Batch.Instrumentation
{
	/// <summary>
	/// Summary description for BatchJobCompletedEvent.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class BatchJobCompletedEvent : BatchJobEvent
	{        
        private TimeSpan _executionDuration;
        /// <summary>BatchJobCompletedEvent</summary>
        /// <param name="requestKey_">System.Guid</param>
        /// <param name="requestName_">System.String</param>
        /// <param name="jobName_">System.String</param>
        /// <param name="sequence_">System.Int32</param>
        /// <param name="status_">System.String</param>
        /// <param name="startTime_">System.DateTime</param>
        public BatchJobCompletedEvent(Guid requestKey_,
            string requestName_,
            string jobName_,
            int sequence_,
            string status_,
            DateTime startTime_) : base(requestKey_, requestName_, jobName_, status_, sequence_)
        {
            DateTime endTime = DateTime.Now;
            this._executionDuration = endTime.Subtract(startTime_);
        }

		/// <summary>
		/// 
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


