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
	/// Summary description for BatchJobStartedEvent.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class BatchJobStartedEvent : BatchJobEvent
	{
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requestKey">System.Guid</param>
        /// <param name="requestName">System.String</param>
        /// <param name="jobName">System.String</param>
        /// <param name="status">System.String</param>
        /// <param name="sequence">System.Int32</param>
        public BatchJobStartedEvent(Guid requestKey, string requestName, string jobName, string status, int sequence)
            : base(requestKey, requestName, jobName, status, sequence)
        {
        }
	}
}

