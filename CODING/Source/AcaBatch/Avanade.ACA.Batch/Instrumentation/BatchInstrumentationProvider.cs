// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2001 - 2006 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Avanade.ACA.Batch.Instrumentation
{
    /// <summary>
    /// Summary for BatchInstrumentationProvider class
    /// </summary>
    [InstrumentationListener(typeof(BatchInstrumentationListener))]
    public class BatchInstrumentationProvider
    {
        private static BatchInstrumentationProvider instance;
        private static object sync = new object();

        /// <summary>
        /// Gets Instance
        /// </summary>
        public static BatchInstrumentationProvider Instance
        {
            get
            {
                lock (sync)
                {
                    if (instance == null)
                    {
                        instance = new BatchInstrumentationProvider();
                        new InstrumentationAttachmentStrategy().AttachInstrumentation(instance, new SystemConfigurationSource(), new ConfigurationReflectionCache());
                    }
                    return instance;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [InstrumentationProvider("RequestEnqueued")]
        public event EventHandler<BatchRequestEventArgs> RequestEnqueued;

        /// <summary>
        ///
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [InstrumentationProvider("RequestDequeued")]
        public event EventHandler<BatchRequestEventArgs> RequestDequeued;

        /// <summary>
        ///
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [InstrumentationProvider("RequestStarted")]
        public event EventHandler<BatchRequestEventArgs> RequestStarted;

        /// <summary>
        ///
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [InstrumentationProvider("JobStarted")]
        public event EventHandler<BatchJobEventArgs> JobStarted;

        /// <summary>
        ///
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [InstrumentationProvider("RequestCompleted")]
        public event EventHandler<BatchRequestCompletedEventArgs> RequestCompleted;

        /// <summary>
        ///
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [InstrumentationProvider("RequestFailed")]
        public event EventHandler<BatchRequestEventArgs> RequestFailed;

        /// <summary>
        ///
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [InstrumentationProvider("RequestCommit")]
        public event EventHandler<EventArgs> RequestCommit;

        /// <summary>
        ///
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [InstrumentationProvider("JobCompleted")]
        public event EventHandler<BatchJobCompletedEventArgs> JobCompleted;

        /// <summary>
        ///
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [InstrumentationProvider("JobFailed")]
        public event EventHandler<BatchJobFailedEventArgs> JobFailed;

        /// <summary>
        ///
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [InstrumentationProvider("JobCommit")]
        public event EventHandler<BatchJobCommittedEventArgs> JobCommit;

        /// <summary>
        ///
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [InstrumentationProvider("JobWorkUnitCompleted")]
        public event EventHandler<BatchJobWorkUnitCompletedEventArgs> JobWorkUnitCompleted;

        /// <summary>
        ///
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [InstrumentationProvider("AvgTimeRequestExecution")]
        public event EventHandler<EventArgs> AvgTimeRequestExecution;

        /// <summary>
        ///
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [InstrumentationProvider("TotalRequestCompleted")]
        public event EventHandler<EventArgs> TotalRequestCompleted;

        /// <summary>
        ///
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [InstrumentationProvider("AvgTimeInQueue")]
        public event EventHandler<EventArgs> AvgTimeInQueue;

        /// <summary>
        ///
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [InstrumentationProvider("TotalRequestExecuted")]
        public event EventHandler<EventArgs> TotalRequestExecuted;

        /// <summary>
        ///
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [InstrumentationProvider("AvgTimeJobExecution")]
        public event EventHandler<EventArgs> AvgTimeJobExecution;

        /// <summary>
        ///
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [InstrumentationProvider("TotalJobCompleted")]
        public event EventHandler<EventArgs> TotalJobCompleted;

        /// <summary>
        ///
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [InstrumentationProvider("AvgTimeWorkUnit")]
        public event EventHandler<EventArgs> AvgTimeWorkUnit;

        /// <summary>
        ///
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [InstrumentationProvider("TotalWorkUnit")]
        public event EventHandler<EventArgs> TotalWorkUnit;

        /// <summary>
        ///
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [InstrumentationProvider("NumServerThrottled")]
        public event EventHandler<EventArgs> NumServerThrottled;

        /// <summary>
        ///
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [InstrumentationProvider("RequestPaused")]
        public event EventHandler<BatchRequestPausedEventArgs> RequestPaused;
        
        /// <summary>
        ///
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [InstrumentationProvider("RequestResumed")]
        public event EventHandler<BatchRequestResumedEventArgs> RequestResumed;

        /// <summary>
        ///
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [InstrumentationProvider("BatchServerHosted")]
        public event EventHandler<BatchServerHostEventArgs> BatchServerHosted;

        /// <summary>
        ///
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [InstrumentationProvider("BatchArchitectureConfigFailed")]
        public event EventHandler<BatchArchitectureFailureEventArgs> BatchArchitectureConfigFailed;

        /// <summary>
        ///
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [InstrumentationProvider("BatchArchitectureFailed")]
        public event EventHandler<BatchArchitectureFailureEventArgs> BatchArchitectureFailed;

        /// <summary>
        ///
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [InstrumentationProvider("BatchExecutionHostStarted")]
        public event EventHandler<EventArgs> BatchExecutionHostStarted;

        /// <summary>FireRequestEnqueuedEvent</summary>
        /// <param name="requestKey">System.Guid</param>
        /// <param name="requestName">System.String</param>
        /// <param name="status">System.String</param>
        /// <returns>void</returns>
        public void FireRequestEnqueuedEvent(Guid requestKey, string requestName, string status)
        {
            if (RequestEnqueued != null)
            {
                RequestEnqueued(this, new BatchRequestEventArgs(requestKey, requestName, status));
            }
        }

        /// <summary>FireRequestDequeuedEvent</summary>
        /// <param name="requestKey">System.Guid</param>
        /// <param name="requestName">System.String</param>
        /// <param name="status">System.String</param>
        /// <returns>void</returns>
        public void FireRequestDequeuedEvent(Guid requestKey, string requestName, string status)
        {
            if (RequestDequeued != null)
            {
                RequestDequeued(this, new BatchRequestEventArgs(requestKey, requestName, status));
            }
        }

        /// <summary>FireRequestStartedEvent</summary>
        /// <param name="requestKey">System.Guid</param>
        /// <param name="requestName">System.String</param>
        /// <param name="status">System.String</param>
        /// <returns>void</returns>
        public void FireRequestStartedEvent(Guid requestKey, string requestName, string status)
        {
            if (RequestStarted != null)
            {
                RequestStarted(this, new BatchRequestEventArgs(requestKey, requestName, status));
            }
        }

        /// <summary>FireJobStartedEvent</summary>
        /// <param name="requestKey">System.Guid</param>
        /// <param name="requestName">System.String</param>
        /// <param name="jobName">System.String</param>
        /// <param name="status">System.String</param>
        /// <param name="sequence">System.Int32</param>
        /// <returns>void</returns>
        public void FireJobStartedEvent(Guid requestKey, string requestName, string jobName, string status, int sequence)
        {
            if (JobStarted != null)
            {
                JobStarted(this, new BatchJobEventArgs(requestKey, requestName, jobName, status, sequence));
            }
        }

        /// <summary>FireRequestCompletedEvent</summary>
        /// <param name="requestKey">System.Guid</param>
        /// <param name="requestName">System.String</param>
        /// <param name="status">System.String</param>
        /// <param name="startTime">System.DateTime</param>
        /// <returns>void</returns>
        public void FireRequestCompletedEvent(Guid requestKey, string requestName, string status, DateTime startTime)
        {
            if (RequestCompleted != null)
            {
                RequestCompleted(this, new BatchRequestCompletedEventArgs(requestKey, requestName, status, startTime));
            }
        }

        /// <summary>FireRequestFailedEvent</summary>
        /// <param name="requestKey">System.Guid</param>
        /// <param name="requestName">System.String</param>
        /// <param name="status">System.String</param>
        /// <returns>void</returns>
        public void FireRequestFailedEvent(Guid requestKey, string requestName, string status)
        {
            if (RequestFailed != null)
            {
                RequestFailed(this, new BatchRequestEventArgs(requestKey, requestName, status));
            }
        }

        /// <summary>FireRequestCommitEvent</summary>
        /// <returns>void</returns>
        public void FireRequestCommitEvent()
        {
            if (RequestCommit != null)
            {
                RequestCommit(this, new EventArgs());
            }
        }

        /// <summary>FireJobCompletedEvent</summary>
        /// <param name="requestKey">System.Guid</param>
        /// <param name="requestName">System.String</param>
        /// <param name="jobName">System.String</param>
        /// <param name="status">System.String</param>
        /// <param name="sequence">System.Int32</param>
        /// <param name="startTime">System.DateTime</param>
        /// <returns>void</returns>
        public void FireJobCompletedEvent(Guid requestKey, string requestName, string jobName, string status, int sequence, DateTime startTime)
        {
            if (JobCompleted != null)
            {
                JobCompleted(this, new BatchJobCompletedEventArgs(requestKey, requestName, jobName, status, sequence, startTime));
            }
        }

        /// <summary>FireJobFailedEvent</summary>
        /// <param name="requestKey">System.Guid</param>
        /// <param name="requestName">System.String</param>
        /// <param name="jobName">System.String</param>
        /// <param name="sequence">System.Int32</param>
        /// <param name="status">System.String</param>
        /// <param name="exception">System.Exception</param>
        /// <returns>void</returns>
        public void FireJobFailedEvent(Guid requestKey, string requestName, string jobName, int sequence, string status, Exception exception)
        {
            if (JobFailed != null)
            {
                JobFailed(this, new BatchJobFailedEventArgs(requestKey, requestName, jobName, sequence, status, exception));
            }
        }

        /// <summary>FireJobCommitEvent</summary>
        /// <param name="countIncrement">System.Int64</param>
        /// <returns>void</returns>
        public void FireJobCommitEvent(long countIncrement)
        {
            if (JobCommit != null)
            {
                JobCommit(this, new BatchJobCommittedEventArgs(countIncrement));
            }
        }

        /// <summary>FireJobWorkUnitCompletedEvent</summary>
        /// <param name="duration">System.TimeSpan</param>
        /// <param name="workUnits">System.Int32</param>
        /// <returns>void</returns>
        public void FireJobWorkUnitCompletedEvent(TimeSpan duration, int workUnits)
        {
            if (JobWorkUnitCompleted != null)
            {
                JobWorkUnitCompleted(this, new BatchJobWorkUnitCompletedEventArgs(duration, workUnits));
            }
        }

        /// <summary>FireAvgTimeRequestExecutionEvent</summary>
        /// <returns>void</returns>
        public void FireAvgTimeRequestExecutionEvent()
        {
            if (AvgTimeRequestExecution != null)
            {
                AvgTimeRequestExecution(this, new EventArgs());
            }
        }

        /// <summary>FireTotalRequestCompletedEvent</summary>
        /// <returns>void</returns>
        public void FireTotalRequestCompletedEvent()
        {
            if (TotalRequestCompleted != null)
            {
                TotalRequestCompleted(this, new EventArgs());
            }
        }

        /// <summary>FireAvgTimeInQueueEvent</summary>
        /// <returns>void</returns>
        public void FireAvgTimeInQueueEvent()
        {
            if (AvgTimeInQueue != null)
            {
                AvgTimeInQueue(this, new EventArgs());
            }
        }

        /// <summary>FireTotalRequestExecutedEvent</summary>
        /// <returns>void</returns>
        public void FireTotalRequestExecutedEvent()
        {
            if (TotalRequestExecuted != null)
            {
                TotalRequestExecuted(this, new EventArgs());
            }
        }

        /// <summary>FireAvgTimeJobExecutionEvent</summary>
        /// <returns>void</returns>
        public void FireAvgTimeJobExecutionEvent()
        {
            if (AvgTimeJobExecution != null)
            {
                AvgTimeJobExecution(this, new EventArgs());
            }
        }

        /// <summary>FireTotalJobCompletedEvent</summary>
        /// <returns>void</returns>
        public void FireTotalJobCompletedEvent()
        {
            if (TotalJobCompleted != null)
            {
                TotalJobCompleted(this, new EventArgs());
            }
        }

        /// <summary>FireAvgTimeWorkUnitEvent</summary>
        /// <returns>void</returns>
        public void FireAvgTimeWorkUnitEvent()
        {
            if (AvgTimeWorkUnit != null)
            {
                AvgTimeWorkUnit(this, new EventArgs());
            }
        }

        /// <summary>FireTotalWorkUnitEvent</summary>
        /// <returns>void</returns>
        public void FireTotalWorkUnitEvent()
        {
            if (TotalWorkUnit != null)
            {
                TotalWorkUnit(this, new EventArgs());
            }
        }

        /// <summary>FireNumServerThrottledEvent</summary>
        /// <returns>void</returns>
        public void FireNumServerThrottledEvent()
        {
            if (NumServerThrottled != null)
            {
                NumServerThrottled(this, new EventArgs());
            }
        }

        /// <summary>FireRequestResumedEvent</summary>
        /// <param name="requestKey">System.Guid</param>
        /// <param name="requestName">System.String</param>
        /// <returns>void</returns>
        public void FireRequestResumedEvent(Guid requestKey, string requestName)
        {
            if (RequestResumed != null)
            {
                RequestResumed(this, new BatchRequestResumedEventArgs(requestKey, requestName));
            }
        }

        /// <summary>FireRequestPausedEvent</summary>
        /// <param name="requestKey">System.Guid</param>
        /// <param name="requestName">System.String</param>
        /// <returns>void</returns>
        public void FireRequestPausedEvent(Guid requestKey, string requestName)
        {
            if (RequestPaused != null)
            {
                RequestPaused(this, new BatchRequestPausedEventArgs(requestKey, requestName));
            }
        }

        /// <summary>FireBatchServerHostEvent</summary>
        /// <param name="destination">System.String</param>
        /// <param name="started">System.Boolean</param>
        /// <returns>void</returns>
        public void FireBatchServerHostEvent(string destination, bool started)
        {
            if (BatchServerHosted != null)
            {
                BatchServerHosted(this, new BatchServerHostEventArgs(destination, started));
            }
        }

        /// <summary>FireBatchExecutionHostStartedEvent</summary>
        /// <returns>void</returns>
        public void FireBatchExecutionHostStartedEvent()
        {
            if (BatchExecutionHostStarted != null)
            {
                BatchExecutionHostStarted(this, new EventArgs());
            }
        }

        /// <summary>FireBatchArchitectureConfigFailureEvent</summary>
        /// <param name="message">System.String</param>
        /// <param name="exception">System.Exception</param>
        /// <returns>void</returns>
        public void FireBatchArchitectureConfigFailureEvent(string message, Exception exception)
        {
            if (BatchArchitectureConfigFailed != null)
            {
                BatchArchitectureConfigFailed(this, new BatchArchitectureFailureEventArgs(message, exception));
            }
        }

        /// <summary>FireBatchArchitectureFailedEvent</summary>
        /// <param name="message">System.String</param>
        /// <param name="exception">System.Exception</param>
        /// <returns>void</returns>
        public void FireBatchArchitectureFailedEvent(string message, Exception exception)
        {
            if (BatchArchitectureFailed != null)
            {
                BatchArchitectureFailed(this, new BatchArchitectureFailureEventArgs(message, exception));
            }
        }
    }
}