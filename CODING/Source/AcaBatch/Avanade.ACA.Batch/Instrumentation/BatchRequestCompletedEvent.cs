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
	/// Summary description for BatchRequestCompletedEvent.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class BatchRequestCompletedEvent : BatchRequestEvent
	{
        private TimeSpan _executionDuration;
        /// <summary>BatchRequestCompletedEvent</summary>
        /// <param name="requestKey">System.Guid</param>
        /// <param name="requestName">System.String</param>
        /// <param name="status">System.String</param>
        /// <param name="startTime">System.DateTime</param>
        public BatchRequestCompletedEvent(Guid requestKey,
                            string requestName,
                            string status,
                            DateTime startTime)
            : base(requestKey, requestName, status)
        {            
            DateTime endTime = DateTime.Now;
            this._executionDuration = endTime.Subtract(startTime);
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


