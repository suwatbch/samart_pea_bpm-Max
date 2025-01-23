// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================

using System;
using System.Threading;

using Avanade.ACA.Batch.Instrumentation;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// Represents the default implementation of the <see cref="IBatch"/> 
	/// interface. This implementation handles executing jobs in a batch,
	/// tracking the status of the batch and updating the <see cref="BatchExecutionContext"/>
	/// as necessary. This imnlementation of the Batch should suffice in most cases.
	/// However, you may want to inherit from this class and override methods if 
	/// your Batch needs specialized behavior.
	/// </summary>
	[Serializable]
	public class Batch : IBatch
	{
		private const string INVALID_JOB_STATUS = 
			"The job with name {0} and execution key {1} " 
			+ "exited execution with an invalid status code.";

		private const string PROCESS_ID_KEY = "ProcessId";

		private BatchExecutionContext	_context;
		private Thread					_executionThread;
		private bool					_finishedExecuting;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Batch"/> class. All implementations
        /// of <see cref="IBatch"/> must expose a 
        /// parameterless constructor in order to
        /// work within the architecture.
        /// </summary>
		public Batch()
		{
		}

		/// <summary>
		/// Returns true if the Batch has completed execution.
		/// Otherwise returns false.
		/// </summary>
		/// <value><strong>True</strong> if the all
		/// off the <see cref="Job"/> objects in the 
		/// current <see cref="BatchExecutionContext"/>
		/// have finished executing, otherwise false.</value>
		protected bool FinishedExecuting
		{
			get
			{
				return _finishedExecuting;
			}
			set
			{
				_finishedExecuting = value;
			}
		}

        /// <summary>
        /// Called by the <see cref="BatchExecutionContext"/> when the time
        /// period allocated for the Batch's execution has elapsed.
        /// </summary>
        /// <param name="sender">The source of the event. In this case a 
        /// <see cref="BatchExecutionContext"/> object.</param>
        /// <param name="args">An empty <see cref="EventArgs"/> object.</param>
        /// <returns>void</returns>
		protected virtual void OnTimedOut(object sender, EventArgs args)
		{
			lock (this)
			{
				if (!FinishedExecuting)
				{
					_executionThread.Abort(this);
				}
			}
		}

        /// <summary>
        /// Determines the index in the list of jobs 
        /// at which to start executing. In the case of a restart
        /// the index will be on or after the index of the
        /// job that failed.
        /// </summary>
        /// <param name="jobContexts">An list of all of the 
        /// <see cref="JobExecutionContext"/> objects associated
        /// with the current batch execution</param>
        /// <returns>The index of the <see cref="JobExecutionContext"/>
        /// element in the array at which to begin processing.</returns>
		protected virtual int IndexOfFirstJob(JobExecutionContext[] jobContexts)
		{
			// assume the batch will start 
			// with the first job in the list
			int index = 0;

			// check if this is a restart execution.
			if (BatchContext.IsARestart)
			{
				// get the restart behavior
				BatchRestartBehavior restartBehavior = 
					BatchContext.RestartBehavior;

				// if it's not a 'Full' restart, then
				// find out which job failed.
				if (restartBehavior != BatchRestartBehavior.Full)
				{
					int numberOfJobs = jobContexts.Length;

					for (int i = 0; i < numberOfJobs; i++)
					{
						JobExecutionContext jobContext = jobContexts[i];

						StatusCode previousExecutionStatus =
							jobContext.PreviousExecutionStatus;

						// if this is the job that failed
						if (previousExecutionStatus == StatusCode.Failed 
							|| previousExecutionStatus == StatusCode.FailedException)
						{
							index = i;
							break;
						}
					}

					// check if the failed job should be skipped
					if (restartBehavior == 
						BatchRestartBehavior.SkipFailedJobAndContinue)
					{
						// increment the index by one
						index++;
					}
				}
			}			

			return index;
		}

		/// <summary>
		/// Returns the <see cref="BatchExecutionContext"/> for this Batch.
		/// </summary>
		/// <value>A <see cref="BatchExecutionContext"/> object.</value>
		public BatchExecutionContext BatchContext
		{
			get
			{
				return _context;
			}
		}

        /// <summary>
        /// Executes the specified batch context
        /// by iterating through each job in
        /// the batch and executing them one 
        /// after the other.
        /// </summary>
        /// <param name="batchContext">The 
        /// <see cref="BatchExecutionContext"/> context object
        /// for the current batch execution.</param>
        /// <returns>void</returns>
		public virtual void Execute(BatchExecutionContext batchContext)
		{
			FinishedExecuting = false;
			DateTime startTime = DateTime.Now;

			BatchArchitectureEvent.AppendExecutionLog("Batch.Execute is invoked.");

			try
			{
				BatchRequestInQueueTimeTracker.Start(batchContext.BatchExecutionContextData.QueuedDate);
								
                BatchInstrumentationProvider.Instance.FireRequestStartedEvent(
                        batchContext.BatchExecutionContextData.Key,
                        batchContext.BatchName,
                        BatchProcessStatusCode.Executing.ToString());

				Initialize(batchContext);
				
				JobExecutionContext[] jobContexts = batchContext.GetJobExecutionContexts();

				ExecuteJobs(jobContexts);

				if (batchContext.Status == BatchProcessStatusCode.Failed)
				{
					return;
				}
				else
				{
					// commit
					lock (this)
					{
						// after all of the jobs have executed,
						// update the overall batch status and commit
						batchContext.Status = BatchProcessStatusCode.Succeeded;
						if (!batchContext.BatchExecutionContextData.Archived)
						{
							batchContext.Commit();
						}
						FinishedExecuting = true;
					}
				}
			}
			catch (ThreadAbortException e)
			{
				lock (this)
				{
					// check if the thread was aborted
					// by the timeout event handler
					if (e.ExceptionState == this)
					{
						Thread.ResetAbort();
						BatchContext.Status = BatchProcessStatusCode.FailedTimedOut;
						BatchContext.UpdateStatus();
					}
					FinishedExecuting = true;
				}
			}
			catch (BatchRequestNotFoundException)
			{
				lock (this)
				{
					// The Batch Execution Request was deleted
					// from underneath the Batch Execution Host.
					// At this point we have no choice but to
					// kill the Batch Execution Host.
					// There is no need to give jobs a chance to cleanup
					// because their CleapupAfterFailure methods should
					// have already been called by this point
					System.Diagnostics.Process.GetCurrentProcess().Kill();
				}
			}
			catch (Exception e)
			{
				lock (this)
				{
					// if the batch context hasn't already been archived,
					// mark it as failed
					if (!batchContext.BatchExecutionContextData.Archived)
					{
						batchContext.Status = BatchProcessStatusCode.Failed;
						batchContext.UpdateStatus();
					}

					FinishedExecuting = true;

					if (e is BatchExecutionException)
					{
						throw;
					}
					else
					{
						// wrap it inside of a batch execution exception
						throw new BatchExecutionException(batchContext, e.Message, e);
					}
				}
			}
			finally
			{
				// log the end of the batch execution
				if (batchContext.Status == BatchProcessStatusCode.Failed || 
					batchContext.Status == BatchProcessStatusCode.FailedTimedOut)
				{	
                    BatchInstrumentationProvider.Instance.FireRequestFailedEvent(
                            batchContext.BatchExecutionContextData.Key,
                            batchContext.BatchName,
                            batchContext.Status.ToString());

				}				
                BatchInstrumentationProvider.Instance.FireRequestCompletedEvent(
                            batchContext.BatchExecutionContextData.Key,
                            batchContext.BatchName,
                            batchContext.Status.ToString(),
                            startTime);
                
				BatchRequestInQueueTimeTracker.RecordDuration(DateTime.Now - startTime);
			}
		}

        /// <summary>
        /// Initializes the current <see cref="Batch"/> object's
        /// state.
        /// </summary>
        /// <param name="batchContext">The <see cref="BatchExecutionContext"/>
        /// object associated with the current request.</param>
        /// <returns>void</returns>
		protected virtual void Initialize(BatchExecutionContext batchContext)
		{
			_context = batchContext;
			_executionThread = Thread.CurrentThread;

			batchContext.TimedOut += new EventHandler(OnTimedOut);

			// change the status to "Executing"
			batchContext.Status = BatchProcessStatusCode.Executing;

			// record the current process id
			System.Diagnostics.Process process = 
				System.Diagnostics.Process.GetCurrentProcess();

			batchContext.RequestParameters.SetPrimitiveData(PROCESS_ID_KEY, process.Id);
			batchContext.Commit();
		}

        /// <summary>
        /// Processes sequentially the specified <see cref="JobExecutionContext"/>
        /// objects by invoking the <see cref="IJob.Execute"/> method.
        /// </summary>
        /// <param name="jobContexts">The set of jobs that the 
        /// current <see cref="Batch"/> will execute.</param>
        /// <returns>void</returns>
		protected virtual void ExecuteJobs(JobExecutionContext[] jobContexts)
		{
			DateTime startTime;

			BatchArchitectureEvent.AppendExecutionLog("Batch.ExecuteJobs is invoked.");

			if (jobContexts.Length == 0)
			{
				// exit the batch and report success
				BatchContext.Status = BatchProcessStatusCode.Succeeded;
				BatchContext.Commit();
				FinishedExecuting = true;
				return;

			}
			int index = IndexOfFirstJob(jobContexts);

			// execute each job sequentially
			for (int i = index; i < jobContexts.Length; i++)
			{
				// since we hasn't start the job, set the start time to a default value.
				startTime = DateTime.MaxValue;

				JobExecutionContext jobContext = jobContexts[i];

				try
				{
					IJob job = jobContext.Job;

					startTime = DateTime.Now;					
                    BatchInstrumentationProvider.Instance.FireJobStartedEvent(
                                jobContext.BatchExecutionContext.BatchExecutionContextData.Key,
                                jobContext.BatchExecutionContext.BatchName,
                                jobContext.JobName,
                                StatusCode.Executing.ToString(),
                                jobContext.JobExecutionContextData.Sequence);

					// execute the job
					job.Execute(jobContext);

					// get the job status code
					StatusCode jobStatus = jobContext.Status;

					if (jobStatus == StatusCode.Failed || 
						jobStatus == StatusCode.FailedContinue ||
						jobStatus == StatusCode.FailedException)
					{                        
                        BatchInstrumentationProvider.Instance.FireJobFailedEvent(
                                    jobContext.BatchExecutionContext.BatchExecutionContextData.Key,
                                    jobContext.BatchExecutionContext.BatchName, jobContext.JobName,
                                    jobContext.JobExecutionContextData.Sequence,
                                    jobStatus.ToString(), null);
                        
					}
					// check the final status of the job
					// after execution
					if (jobStatus == StatusCode.Failed 
						|| jobStatus == StatusCode.FailedException)
					{
						lock (this)
						{						
							// exit the batch if the job reports a failure
							BatchContext.Status = BatchProcessStatusCode.Failed;
							BatchContext.Commit();
							FinishedExecuting = true;
							return;
						}
					}
					else if (jobStatus == StatusCode.FailedContinue || jobStatus == StatusCode.Success)
					{
						// move on to the next job if the current job
						// execution succeeded or failed in a non-critical
						// way.
						continue;
					}
					else
					{
						// all other status codes are invalid 
						// because a job must report success
						// or failure
						string message = String.Format
							(
							INVALID_JOB_STATUS,
							jobContext.JobName,
							jobContext.Key
							);

						throw new BatchExecutionException
							(
							BatchContext, 
							message
							);
					}
				}
				catch (Exception e)
				{
					lock (this)
					{
						// Add non-fatal exceptions
						// to the job context
						jobContext.AddException(e);
						// set the job context's
						// status to "FailedException" and commit
						jobContext.Status = StatusCode.FailedException;

						jobContext.Lock();
						jobContext.Save();

						// don't execute any remaining jobs
						FinishedExecuting = true;

						if (e is ThreadAbortException)
						{
							jobContext.Status = StatusCode.Terminated;
						}
						else
						{                            
                            BatchInstrumentationProvider.Instance.FireJobFailedEvent(
                                    jobContext.BatchExecutionContext.BatchExecutionContextData.Key,
                                    jobContext.BatchExecutionContext.BatchName,
                                    jobContext.JobName,
                                    jobContext.JobExecutionContextData.Sequence,
                                    jobContext.Status.ToString(),
                                    e);
   
							if (e is JobExecutionException || e is BatchRequestNotFoundException)
							{
								throw;
							}
							else
							{
								throw new JobExecutionException(jobContext, e.Message, e);
							}
						}
					}
				} // catch
				finally
				{
					if (startTime != DateTime.MaxValue)
					{                        
                        BatchInstrumentationProvider.Instance.FireJobCompletedEvent(
                                    jobContext.BatchExecutionContext.BatchExecutionContextData.Key,
                                    jobContext.BatchExecutionContext.BatchName,
                                    jobContext.JobName,
                                    jobContext.Status.ToString(),
                                    jobContext.JobExecutionContextData.Sequence,                                    
                                    startTime);

					}
				}
			}
		}
	}
}
