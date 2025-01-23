// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;

using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation;

namespace Avanade.ACA.Batch.Instrumentation
{
	/// <summary>
	/// BatchRequestInQueueTimeTracker is to track the time in queue for the request.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class BatchRequestInQueueTimeTracker
	{
		#region Static Members

        /// <summary>BatchRequestInQueueTimeTracker</summary>
        static BatchRequestInQueueTimeTracker()
		{
            // IDC: Counters no longer used      
            //string[] counterNames = new string[] { 
            //                         BatchArchitectureEvent.Counters[(int)BatchArchitectureEvent.CounterID.AvgTimeInQueue].CounterName,
            //                         BatchArchitectureEvent.Counters[(int)BatchArchitectureEvent.CounterID.TotalRequestExecuted].CounterName,
            //};
            //InstrumentedEvent internalEvent = new InstrumentedEvent(BatchArchitectureEvent.CounterCategory, counterNames, true);
            //TicksSecondsCounter = internalEvent.GetPerformanceCounterInstances(
            //                    BatchArchitectureEvent.Counters[(int)BatchArchitectureEvent.CounterID.AvgTimeInQueue].CounterName);
            //NumberCounter = internalEvent.GetPerformanceCounterInstances(
            //    BatchArchitectureEvent.Counters[(int)BatchArchitectureEvent.CounterID.TotalRequestExecuted].CounterName);
		}

        /// <summary>
        /// Set the enqueueTime and increment the number of requests being tracked.
        /// </summary>
        /// <param name="requestEnqueueTime_">System.DateTime</param>
        /// <returns>void</returns>
		public static void Start(DateTime requestEnqueueTime_)
		{
            // IDC: Counters no longer used
            //long interval = DateTime.Now.Ticks - requestEnqueueTime_.Ticks;
            //if (TicksSecondsCounter != null)
            //{
            //    TicksSecondsCounter.IncrementBy(interval);
            //}
            //if (NumberCounter != null)
            //{
            //    NumberCounter.Increment();
            //}
		}

        /// <summary>
        /// Track the time elaps for a request.
        /// </summary>
        /// <param name="duration_">System.TimeSpan</param>
        /// <returns>void</returns>
		public static void RecordDuration(TimeSpan duration_)
		{
            // IDC: Counters no longer used
            //long interval = duration_.Ticks;
            //if (TicksSecondsCounter != null)
            //{
            //    TicksSecondsCounter.IncrementBy(interval);
            //}
		}
		#endregion
	}
}


