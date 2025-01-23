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
	/// Summary description for BatchRequestStartedEvent.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class BatchRequestStartedEvent : BatchRequestEvent
	{
        /// <summary>BatchRequestStartedEvent</summary>
        /// <param name="requestKey">System.Guid</param>
        /// <param name="requestName">System.String</param>
        /// <param name="status">System.String</param>
        public BatchRequestStartedEvent(Guid requestKey, string requestName, string status)
            : base(requestKey, requestName, status)
        {            
        }
    }
}

