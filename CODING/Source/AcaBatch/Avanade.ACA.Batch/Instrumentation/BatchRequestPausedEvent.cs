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
	/// Summary description for BatchRequestPausedEvent.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class BatchRequestPausedEvent : BatchRequestEvent
	{
        /// <summary>BatchRequestPausedEvent</summary>
        /// <param name="requestKey">System.Guid</param>
        /// <param name="requestName">System.String</param>
        public BatchRequestPausedEvent(Guid requestKey, string requestName)
            : base(requestKey, requestName, string.Empty)
        {
        }
	}
}



