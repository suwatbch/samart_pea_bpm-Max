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
	/// Summary description for BatchJobWorkUnitCompletedEvent.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]    
	public class BatchJobWorkUnitCompletedEvent : BaseWmiEvent
	{
        private long[] _counterIncrements;
        /// <summary>BatchJobWorkUnitCompletedEvent</summary>
        /// <param name="duration_">System.TimeSpan</param>
        /// <param name="workUnits_">System.Int32</param>
        public BatchJobWorkUnitCompletedEvent(TimeSpan duration_, int workUnits_)
        {
            long[] counterIncrements = new long[] { duration_.Ticks, (long)workUnits_ };
            _counterIncrements = counterIncrements;
        }

        public long[] CounterIncrements
        {
            get { return _counterIncrements; }
        }
	}
}
