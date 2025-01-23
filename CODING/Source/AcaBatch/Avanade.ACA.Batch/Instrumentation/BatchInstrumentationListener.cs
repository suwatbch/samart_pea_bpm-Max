// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2001 - 2006 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation;
using System.Diagnostics;

namespace Avanade.ACA.Batch.Instrumentation
{
    /// <summary>
    /// Summary for BatchInstrumentationListener class
    /// </summary>
    [HasInstallableResourcesAttribute]
    [PerformanceCountersDefinition(CounterCategoryName, "ACA Batch performance counters")]
    [EventLogDefinition("ACABatch", EventLogSourceName)]
    public class BatchInstrumentationListener : InstrumentationListener
    {
        static EnterpriseLibraryPerformanceCounterFactory factory = new EnterpriseLibraryPerformanceCounterFactory();
        /// <summary>
        /// CounterCategoryName constant 
        /// </summary>
        public const string CounterCategoryName = "ACA Batch";

        /// <summary>
        /// EventLogSourceName constant
        /// </summary>
        public const string EventLogSourceName = "ACA Batch";
        internal const string CounterCategoryHelp = "ACA Batch performance counters.";

        private const string RequestEnqueuedSecCounterName = "# of Requests Enqueued/Sec";
        private const string RequestDequeuedSecCounterName = "# of Requests Dequeued/Sec";
        private const string RequestStartedSecCounterName = "# of Request Started/Sec";
        private const string JobStartedSecCounterName = "# of Jobs Started/Sec";
        private const string RequestCompletedSecCounterName = "# of Requests Completed/Sec";
        private const string RequestFailedSecCounterName = "# of Requests Failed/Sec";
        private const string RequestCommitSecCounterName = "# of Requests Committed/Sec";
        private const string JobCompletedSecCounterName = "# of Jobs Completed/Sec";
        private const string JobFailedSecCounterName = "# of Jobs Failed/Sec";
        private const string JobCommitSecCounterName = "# of Jobs Committed/Sec";
        private const string AvgTimeRequestExecutionCounterName = "Average # of Time Requests Execution/Sec";
        private const string TotalRequestCompletedCounterName = "Total # of Requests Completed/Sec";
        private const string AvgTimeInQueueCounterName = "Average Time In Queue";
        private const string TotalRequestExecutedCounterName = "Total # of Requests Executed/Sec";
        private const string AvgTimeJobExecutionCounterName = "Avg # of Time Jobs Execution/Sec";
        private const string TotalJobCompletedCounterName = "Total # of Jobs Completed/Sec";
        private const string AvgTimeWorkUnitCounterName = "Average Time Work Unit";
        private const string TotalWorkUnitCounterName = "Total # of Work Units";
        private const string NumServerThrottledCounterName = "# of Servers Throttled/Sec";
        private const string RequestPausedCounterName = "# of Requests Paused";
        private const string RequestResumedCounterName = "# of Servers Resumed";
               
        [PerformanceCounter(RequestEnqueuedSecCounterName, "Number of requests enqueued per second", PerformanceCounterType.RateOfCountsPerSecond32)]
        private EnterpriseLibraryPerformanceCounter requestEnqueuedSecCounter;

        [PerformanceCounter(RequestDequeuedSecCounterName, "Number of requests dequeued per second", PerformanceCounterType.RateOfCountsPerSecond32)]
        private EnterpriseLibraryPerformanceCounter requestDequeuedSecCounter;

        [PerformanceCounter(RequestStartedSecCounterName, "Number of requests started per second", PerformanceCounterType.RateOfCountsPerSecond32)]
        private EnterpriseLibraryPerformanceCounter requestStartedSecCounter;

        [PerformanceCounter(JobStartedSecCounterName, "Number of jobs started per second", PerformanceCounterType.RateOfCountsPerSecond32)]
        private EnterpriseLibraryPerformanceCounter jobStartedSecCounter;

        [PerformanceCounter(RequestCompletedSecCounterName, "Number of requests completed per second", PerformanceCounterType.RateOfCountsPerSecond32)]
        private EnterpriseLibraryPerformanceCounter requestCompletedSecCounter;

        [PerformanceCounter(RequestFailedSecCounterName, "Number of requests failed per second", PerformanceCounterType.RateOfCountsPerSecond32)]
        private EnterpriseLibraryPerformanceCounter requestFailedSecCounter;

        [PerformanceCounter(RequestCommitSecCounterName, "Number of requests committed per second", PerformanceCounterType.RateOfCountsPerSecond32)]
        private EnterpriseLibraryPerformanceCounter requestCommitSecCounter;

        [PerformanceCounter(JobCompletedSecCounterName, "Number of jobs completed per second", PerformanceCounterType.RateOfCountsPerSecond32)]
        private EnterpriseLibraryPerformanceCounter jobCompletedSecCounter;

        [PerformanceCounter(JobFailedSecCounterName, "Number of jobs failed per second", PerformanceCounterType.RateOfCountsPerSecond32)]
        private EnterpriseLibraryPerformanceCounter jobFailedSecCounter;

        [PerformanceCounter(JobCommitSecCounterName, "Number of jobs committed per second", PerformanceCounterType.RateOfCountsPerSecond32)]
        private EnterpriseLibraryPerformanceCounter jobCommitSecCounter;

        [PerformanceCounter(AvgTimeRequestExecutionCounterName, "Average time request execution", PerformanceCounterType.RateOfCountsPerSecond32)]
        private EnterpriseLibraryPerformanceCounter avgTimeRequestExecutionCounter;

        [PerformanceCounter(TotalRequestCompletedCounterName, "Total number of requests completed per second", PerformanceCounterType.RateOfCountsPerSecond32)]
        private EnterpriseLibraryPerformanceCounter totalRequestCompletedCounter;

        [PerformanceCounter(AvgTimeInQueueCounterName, "Average time in queue", PerformanceCounterType.RateOfCountsPerSecond32)]
        private EnterpriseLibraryPerformanceCounter avgTimeInQueueCounter;

        [PerformanceCounter(TotalRequestExecutedCounterName, "Total number of requests executed per second", PerformanceCounterType.RateOfCountsPerSecond32)]
        private EnterpriseLibraryPerformanceCounter totalRequestExecutedCounter;

        [PerformanceCounter(AvgTimeJobExecutionCounterName, "Average job execution time", PerformanceCounterType.RateOfCountsPerSecond32)]
        private EnterpriseLibraryPerformanceCounter avgTimeJobExecutionCounter;

        [PerformanceCounter(TotalJobCompletedCounterName, "Total number of jobs completed per second", PerformanceCounterType.RateOfCountsPerSecond32)]
        private EnterpriseLibraryPerformanceCounter totalJobCompletedCounter;

        [PerformanceCounter(AvgTimeWorkUnitCounterName, "Average work unit time", PerformanceCounterType.RateOfCountsPerSecond32)]
        private EnterpriseLibraryPerformanceCounter avgTimeWorkUnitCounter;

        [PerformanceCounter(TotalWorkUnitCounterName, "Total number of work units", PerformanceCounterType.RateOfCountsPerSecond32)]
        private EnterpriseLibraryPerformanceCounter totalWorkUnitCounter;

        [PerformanceCounter(NumServerThrottledCounterName, "Number of servers throttled per second", PerformanceCounterType.RateOfCountsPerSecond32)]
        private EnterpriseLibraryPerformanceCounter numServerThrottledCounter;

        [PerformanceCounter(RequestPausedCounterName, "Number of requests paused", PerformanceCounterType.RateOfCountsPerSecond32)]
        private EnterpriseLibraryPerformanceCounter requestPausedCounter;

        [PerformanceCounter(RequestResumedCounterName, "Number of requests resumed", PerformanceCounterType.RateOfCountsPerSecond32)]
        private EnterpriseLibraryPerformanceCounter requestResumedCounter;

        private EventLogEntryFormatter eventLogEntryFormatter;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="performanceCountersEnabled">System.Boolean</param>
        /// <param name="eventLogEnabled">System.Boolean</param>
        /// <param name="wmiEnabled">System.Boolean</param>
        public BatchInstrumentationListener(bool performanceCountersEnabled, bool eventLogEnabled, bool wmiEnabled)
            : this("ACA Batch", performanceCountersEnabled, eventLogEnabled, wmiEnabled, new NoPrefixNameFormatter())
        {
            this.eventLogEntryFormatter = new EventLogEntryFormatter("ACA Batch");
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="instanceName">System.String</param>
        /// <param name="performanceCountersEnabled">System.Boolean</param>
        /// <param name="eventLogEnabled">System.Boolean</param>
        /// <param name="wmiEnabled">System.Boolean</param>
        /// <param name="nameFormatter">Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation.IPerformanceCounterNameFormatter</param>
        public BatchInstrumentationListener(string instanceName, bool performanceCountersEnabled, bool eventLogEnabled, bool wmiEnabled, IPerformanceCounterNameFormatter nameFormatter)
            : base(instanceName, performanceCountersEnabled, eventLogEnabled, wmiEnabled, nameFormatter)
        {
            this.eventLogEntryFormatter = new EventLogEntryFormatter("ACA Batch");
        }

        /// <summary>RequestEnqueued</summary>
        /// <param name="sender">System.Object</param>
        /// <param name="e">Avanade.ACA.Batch.Instrumentation.BatchRequestEventArgs</param>
        /// <returns>void</returns>
        [InstrumentationConsumer("RequestEnqueued")]
        public void RequestEnqueued(object sender, BatchRequestEventArgs e)
        {
            if (WmiEnabled)
            {
                System.Management.Instrumentation.Instrumentation.Fire(new BatchRequestEnqueuedEvent(e.RequestKey, e.RequestName, e.Status));
            }
            if (EventLoggingEnabled)
            {
                string errorMsg = string.Format(
                    "Batch Request Enqueued. RequestKey = '{0}', RequestName = '{1}', Status = {2}.",
                    e.RequestKey, e.RequestName, e.Status);
                EventLog.WriteEntry(GetEventSourceName(), errorMsg, EventLogEntryType.Information);
            }
            if (PerformanceCountersEnabled)
            {
                requestEnqueuedSecCounter.Increment();
            }
        }

        /// <summary>RequestDequeued</summary>
        /// <param name="sender">System.Object</param>
        /// <param name="e">Avanade.ACA.Batch.Instrumentation.BatchRequestEventArgs</param>
        /// <returns>void</returns>
        [InstrumentationConsumer("RequestDequeued")]
        public void RequestDequeued(object sender, BatchRequestEventArgs e)
        {
            if (WmiEnabled)
            {
                System.Management.Instrumentation.Instrumentation.Fire(new BatchRequestDequeuedEvent(e.RequestKey, e.RequestName, e.Status));
            }
            if (EventLoggingEnabled)
            {
                string errorMsg = string.Format(
                    "Batch Request Dequeued. RequestKey = '{0}', RequestName = '{1}', Status = {2}.",
                    e.RequestKey, e.RequestName, e.Status);
                EventLog.WriteEntry(GetEventSourceName(), errorMsg, EventLogEntryType.Information);
            }
            if (PerformanceCountersEnabled)
            {
                requestDequeuedSecCounter.Increment();
            }
        }

        /// <summary>RequestStarted</summary>
        /// <param name="sender">System.Object</param>
        /// <param name="e">Avanade.ACA.Batch.Instrumentation.BatchRequestEventArgs</param>
        /// <returns>void</returns>
        [InstrumentationConsumer("RequestStarted")]
        public void RequestStarted(object sender, BatchRequestEventArgs e)
        {
            if (WmiEnabled)
            {
                System.Management.Instrumentation.Instrumentation.Fire(new BatchRequestStartedEvent(e.RequestKey, e.RequestName, e.Status));
            }
            if (EventLoggingEnabled)
            {
                string errorMsg = string.Format(
                    "Batch Request Started. RequestKey = '{0}', RequestName = '{1}', Status = {2}.",
                    e.RequestKey, e.RequestName, e.Status);
                EventLog.WriteEntry(GetEventSourceName(), errorMsg, EventLogEntryType.Information);
            }
            if (PerformanceCountersEnabled)
            {
                requestStartedSecCounter.Increment();
            }
        }

        /// <summary>JobStarted</summary>
        /// <param name="sender">System.Object</param>
        /// <param name="e">Avanade.ACA.Batch.Instrumentation.BatchJobEventArgs</param>
        /// <returns>void</returns>
        [InstrumentationConsumer("JobStarted")]
        public void JobStarted(object sender, BatchJobEventArgs e)
        {
            if (WmiEnabled)
            {
                System.Management.Instrumentation.Instrumentation.Fire(new BatchJobStartedEvent(e.RequestKey, e.RequestName, e.JobName, e.Status, e.Sequence));
            }
            if (EventLoggingEnabled)
            {
                string errorMsg = string.Format(
                    "Batch Job Started. RequestKey = '{0}', RequestName = '{1}', JobName = '{2}', Status = '{3}', Sequence = '{4}'.",
                    e.RequestKey, e.RequestName, e.JobName, e.Status, e.Sequence);
                EventLog.WriteEntry(GetEventSourceName(), errorMsg, EventLogEntryType.Information);
            }
            if (PerformanceCountersEnabled)
            {
                jobStartedSecCounter.Increment();
            }
        }

        /// <summary>RequestCompleted</summary>
        /// <param name="sender">System.Object</param>
        /// <param name="e">Avanade.ACA.Batch.Instrumentation.BatchRequestCompletedEventArgs</param>
        /// <returns>void</returns>
        [InstrumentationConsumer("RequestCompleted")]
        public void RequestCompleted(object sender, BatchRequestCompletedEventArgs e)
        {
            if (WmiEnabled)
            {
                System.Management.Instrumentation.Instrumentation.Fire(new BatchRequestCompletedEvent(e.RequestKey, e.RequestName, e.Status, e.StartTime));
            }
            if (EventLoggingEnabled)
            {
                string errorMsg = string.Format(
                    "Batch Request Completed. RequestKey = '{0}', RequestName = '{1}', Status = '{2}', ExecutionDuration = '{3}'.",
                    e.RequestKey, e.RequestName, e.Status, e.ExecutionDuration);
                EventLog.WriteEntry(GetEventSourceName(), errorMsg, EventLogEntryType.Information);
            }
            if (PerformanceCountersEnabled)
            {
                requestCompletedSecCounter.Increment();
            }
        }

        /// <summary>RequestFailed</summary>
        /// <param name="sender">System.Object</param>
        /// <param name="e">Avanade.ACA.Batch.Instrumentation.BatchRequestEventArgs</param>
        /// <returns>void</returns>
        [InstrumentationConsumer("RequestFailed")]
        public void RequestFailed(object sender, BatchRequestEventArgs e)
        {
            if (WmiEnabled)
            {
                System.Management.Instrumentation.Instrumentation.Fire(new BatchRequestFailedEvent(e.RequestKey, e.RequestName, e.Status));
            }
            if (EventLoggingEnabled)
            {
                string errorMsg = string.Format(
                    "Batch Request Failed. RequestKey = '{0}', RequestName = '{1}', Status = '{2}'.",
                    e.RequestKey, e.RequestName, e.Status);
                EventLog.WriteEntry(GetEventSourceName(), errorMsg, EventLogEntryType.Error);
            }
            if (PerformanceCountersEnabled)
            {
                requestFailedSecCounter.Increment();
            }
        }

        /// <summary>RequestCommitted</summary>
        /// <param name="sender">System.Object</param>
        /// <param name="e">System.EventArgs</param>
        /// <returns>void</returns>
        [InstrumentationConsumer("RequestCommitted")]
        public void RequestCommitted(object sender, EventArgs e)
        {
            if (PerformanceCountersEnabled)
            {
                requestCommitSecCounter.Increment();
            }
        }

        /// <summary>JobCompleted</summary>
        /// <param name="sender">System.Object</param>
        /// <param name="e">Avanade.ACA.Batch.Instrumentation.BatchJobCompletedEventArgs</param>
        /// <returns>void</returns>
        [InstrumentationConsumer("JobCompleted")]
        public void JobCompleted(object sender, BatchJobCompletedEventArgs e)
        {
            if (WmiEnabled)
            {
                System.Management.Instrumentation.Instrumentation.Fire(new BatchJobCompletedEvent(e.RequestKey, e.RequestName, e.JobName, e.Sequence, e.Status, e.StartTime));
            }
            if (EventLoggingEnabled)
            {
                string errorMsg = string.Format(
                    "Batch Job Completed. RequestKey = '{0}', RequestName = '{1}', JobName = '{2}', Status = '{3}', Sequence = '{4}', ExceutionDuration = '{5}'.",
                    e.RequestKey, e.RequestName, e.JobName, e.Status, e.Sequence, e.ExecutionDuration);
                EventLog.WriteEntry(GetEventSourceName(), errorMsg, EventLogEntryType.Information);
            }
            if (PerformanceCountersEnabled)
            {
                jobCompletedSecCounter.Increment();
            }
        }

        /// <summary>JobFailed</summary>
        /// <param name="sender">System.Object</param>
        /// <param name="e">Avanade.ACA.Batch.Instrumentation.BatchJobFailedEventArgs</param>
        /// <returns>void</returns>
        [InstrumentationConsumer("JobFailed")]
        public void JobFailed(object sender, BatchJobFailedEventArgs e)
        {
            if (WmiEnabled)
            {
                System.Management.Instrumentation.Instrumentation.Fire(new BatchJobFailedEvent(e.RequestKey, e.RequestName, e.JobName, e.Sequence, e.Status, e.Exception));
            }
            if (EventLoggingEnabled)
            {
                string errorMsg = string.Format(
                    "Batch Job Failed. RequestKey = '{0}', RequestName = '{1}', JobName = '{2}', Sequence = '{3}', Status = '{4}', ExceptionMessage = '{5}', ExceptionStackTrace = '{6}'.",
                    e.RequestKey, e.RequestName, e.JobName, e.Status, e.Sequence, e.ExceptionMessage, e.ExceptionStackTrace);
                EventLog.WriteEntry(GetEventSourceName(), errorMsg, EventLogEntryType.Error);
            }
            if (PerformanceCountersEnabled)
            {
                jobFailedSecCounter.Increment();
            }
        }

        /// <summary>JobCommitted</summary>
        /// <param name="sender">System.Object</param>
        /// <param name="e">System.EventArgs</param>
        /// <returns>void</returns>
        [InstrumentationConsumer("JobCommitted")]
        public void JobCommitted(object sender, EventArgs e)
        {
            if (PerformanceCountersEnabled)
            {
                jobCommitSecCounter.Increment();
            }
        }

        /// <summary>AvgTimeRequest</summary>
        /// <param name="sender">System.Object</param>
        /// <param name="e">System.EventArgs</param>
        /// <returns>void</returns>
        [InstrumentationConsumer("AvgTimeRequest")]
        public void AvgTimeRequest(object sender, EventArgs e)
        {
            if (PerformanceCountersEnabled)
            {
                avgTimeRequestExecutionCounter.Increment();
            }
        }

        /// <summary>TotalRequestCompleted</summary>
        /// <param name="sender">System.Object</param>
        /// <param name="e">System.EventArgs</param>
        /// <returns>void</returns>
        [InstrumentationConsumer("TotalRequestCompleted")]
        public void TotalRequestCompleted(object sender, EventArgs e)
        {
            if (PerformanceCountersEnabled)
            {
                totalRequestCompletedCounter.Increment();
            }
        }

        /// <summary>AvgTimeInQueue</summary>
        /// <param name="sender">System.Object</param>
        /// <param name="e">System.EventArgs</param>
        /// <returns>void</returns>
        [InstrumentationConsumer("AvgTimeInQueue")]
        public void AvgTimeInQueue(object sender, EventArgs e)
        {
            if (PerformanceCountersEnabled)
            {
                avgTimeInQueueCounter.Increment();
            }
        }

        /// <summary>TotalRequestExecuted</summary>
        /// <param name="sender">System.Object</param>
        /// <param name="e">System.EventArgs</param>
        /// <returns>void</returns>
        [InstrumentationConsumer("TotalRequestExecuted")]
        public void TotalRequestExecuted(object sender, EventArgs e)
        {
            if (PerformanceCountersEnabled)
            {
                totalRequestExecutedCounter.Increment();
            }
        }

        /// <summary>AvgTimeJobExecution</summary>
        /// <param name="sender">System.Object</param>
        /// <param name="e">System.EventArgs</param>
        /// <returns>void</returns>
        [InstrumentationConsumer("AvgTimeJobExecution")]
        public void AvgTimeJobExecution(object sender, EventArgs e)
        {
            if (PerformanceCountersEnabled)
            {
                avgTimeJobExecutionCounter.Increment();
            }
        }

        /// <summary>TotalJobCompleted</summary>
        /// <param name="sender">System.Object</param>
        /// <param name="e">System.EventArgs</param>
        /// <returns>void</returns>
        [InstrumentationConsumer("TotalJobCompleted")]
        public void TotalJobCompleted(object sender, EventArgs e)
        {
            if (PerformanceCountersEnabled)
            {
                totalJobCompletedCounter.Increment();
            }
        }

        /// <summary>AvgTimeWorkUnit</summary>
        /// <param name="sender">System.Object</param>
        /// <param name="e">System.EventArgs</param>
        /// <returns>void</returns>
        [InstrumentationConsumer("AvgTimeWorkUnit")]
        public void AvgTimeWorkUnit(object sender, EventArgs e)
        {
            if (PerformanceCountersEnabled)
            {
                avgTimeWorkUnitCounter.Increment();
            }
        }

        /// <summary>TotalWorkUnit</summary>
        /// <param name="sender">System.Object</param>
        /// <param name="e">System.EventArgs</param>
        /// <returns>void</returns>
        [InstrumentationConsumer("TotalWorkUnit")]
        public void TotalWorkUnit(object sender, EventArgs e)
        {
            if (PerformanceCountersEnabled)
            {
                totalWorkUnitCounter.Increment();
            }
        }

        /// <summary>NumServerThrottled</summary>
        /// <param name="sender">System.Object</param>
        /// <param name="e">System.EventArgs</param>
        /// <returns>void</returns>
        [InstrumentationConsumer("NumServerThrottled")]
        public void NumServerThrottled(object sender, EventArgs e)
        {
            if (PerformanceCountersEnabled)
            {
                numServerThrottledCounter.Increment();
            }
        }

        /// <summary>RequestPaused</summary>
        /// <param name="sender">System.Object</param>
        /// <param name="e">Avanade.ACA.Batch.Instrumentation.BatchRequestPausedEventArgs</param>
        /// <returns>void</returns>
        [InstrumentationConsumer("RequestPaused")]
        public void RequestPaused(object sender, BatchRequestPausedEventArgs e)
        {
            if (WmiEnabled)
            {
                System.Management.Instrumentation.Instrumentation.Fire(new BatchRequestPausedEvent(e.RequestKey, e.RequestName));
            }
            if (EventLoggingEnabled)
            {
                string errorMsg = string.Format(
                    "Batch Request Paused. RequestKey = '{0}', RequestName = '{1}'.",
                    e.RequestKey, e.RequestName);
                EventLog.WriteEntry(GetEventSourceName(), errorMsg, EventLogEntryType.Information);
            }
            if (PerformanceCountersEnabled)
            {
                requestPausedCounter.Increment();
            }
        }

        /// <summary>RequestResumed</summary>
        /// <param name="sender">System.Object</param>
        /// <param name="e">Avanade.ACA.Batch.Instrumentation.BatchRequestResumedEventArgs</param>
        /// <returns>void</returns>
        [InstrumentationConsumer("RequestResumed")]
        public void RequestResumed(object sender, BatchRequestResumedEventArgs e)
        {
            if (WmiEnabled)
            {
                System.Management.Instrumentation.Instrumentation.Fire(new BatchRequestResumedEvent(e.RequestKey, e.RequestName));
            }
            if (EventLoggingEnabled)
            {
                string errorMsg = string.Format(
                    "Batch Request Resumed. RequestKey = '{0}', RequestName = '{1}'.",
                    e.RequestKey, e.RequestName);
                EventLog.WriteEntry(GetEventSourceName(), errorMsg, EventLogEntryType.Information);
            }
            if (PerformanceCountersEnabled)
            {
                requestResumedCounter.Increment();
            }
        }

        /// <summary>BatchServerHosted</summary>
        /// <param name="sender">System.Object</param>
        /// <param name="e">Avanade.ACA.Batch.Instrumentation.BatchServerHostEventArgs</param>
        /// <returns>void</returns>
        [InstrumentationConsumer("BatchServerHosted")]
        public void BatchServerHosted(object sender, BatchServerHostEventArgs e)
        {
            if (WmiEnabled)
            {
                System.Management.Instrumentation.Instrumentation.Fire(new BatchServerHostEvent(e.Destination, e.Started));
            }
            if (EventLoggingEnabled)
            {
                string errorMsg = string.Empty;
                if (e.Started)
                {
                    errorMsg = string.Format("ACA.NET Batch Server has been started. Destination: " + e.Destination);
                }
                else
                {
                    errorMsg = string.Format("ACA.NET Batch Server has been stopped. Destination: " + e.Destination);
                }

                EventLog.WriteEntry(GetEventSourceName(), errorMsg, EventLogEntryType.Information);
            }
        }

        /// <summary>FireAuxEvent</summary>
        /// <param name="p">System.Object</param>
        /// <returns>void</returns>
        private void FireAuxEvent(object p)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// CreatePerformanceCounters method
        /// </summary>
        /// <param name="instanceNames">string[]</param>
        /// <returns>void</returns>
        protected override void CreatePerformanceCounters(string[] instanceNames)
        {
            requestEnqueuedSecCounter = factory.CreateCounter(CounterCategoryName, RequestEnqueuedSecCounterName, instanceNames);
            requestDequeuedSecCounter = factory.CreateCounter(CounterCategoryName, RequestDequeuedSecCounterName, instanceNames);
            requestStartedSecCounter = factory.CreateCounter(CounterCategoryName, RequestStartedSecCounterName, instanceNames);
            jobStartedSecCounter = factory.CreateCounter(CounterCategoryName, JobStartedSecCounterName, instanceNames);
            requestCompletedSecCounter = factory.CreateCounter(CounterCategoryName, RequestCompletedSecCounterName, instanceNames);
            requestFailedSecCounter = factory.CreateCounter(CounterCategoryName, RequestFailedSecCounterName, instanceNames);
            requestCommitSecCounter = factory.CreateCounter(CounterCategoryName, RequestCommitSecCounterName, instanceNames);
            jobCompletedSecCounter = factory.CreateCounter(CounterCategoryName, JobCompletedSecCounterName, instanceNames);
            jobFailedSecCounter = factory.CreateCounter(CounterCategoryName, JobFailedSecCounterName, instanceNames);
            jobCommitSecCounter = factory.CreateCounter(CounterCategoryName, JobCommitSecCounterName, instanceNames);
            avgTimeRequestExecutionCounter = factory.CreateCounter(CounterCategoryName, AvgTimeRequestExecutionCounterName, instanceNames);
            totalRequestCompletedCounter = factory.CreateCounter(CounterCategoryName, TotalRequestCompletedCounterName, instanceNames);
            avgTimeInQueueCounter = factory.CreateCounter(CounterCategoryName, AvgTimeInQueueCounterName, instanceNames);
            totalRequestExecutedCounter = factory.CreateCounter(CounterCategoryName, TotalRequestExecutedCounterName, instanceNames);
            avgTimeJobExecutionCounter = factory.CreateCounter(CounterCategoryName, AvgTimeJobExecutionCounterName, instanceNames);
            totalJobCompletedCounter = factory.CreateCounter(CounterCategoryName, TotalJobCompletedCounterName, instanceNames);
            avgTimeWorkUnitCounter = factory.CreateCounter(CounterCategoryName, AvgTimeWorkUnitCounterName, instanceNames);
            totalWorkUnitCounter = factory.CreateCounter(CounterCategoryName, TotalWorkUnitCounterName, instanceNames);
            numServerThrottledCounter = factory.CreateCounter(CounterCategoryName, NumServerThrottledCounterName, instanceNames);
            requestResumedCounter = factory.CreateCounter(CounterCategoryName, RequestResumedCounterName, instanceNames);
            requestPausedCounter = factory.CreateCounter(CounterCategoryName, RequestPausedCounterName, instanceNames);
        }
    }
}
