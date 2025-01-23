// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Collections;
using System.Threading;
using System.Data;
using System.EnterpriseServices;

using Avanade.ACA.Batch.Instrumentation;
using Avanade.ACA.Batch.Configuration;
using System.Data.Common;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// Represents the context of the currently 
	/// executing batch. This context provides
	/// access to the request, batch definition properties
	/// and the 
	/// <see cref="JobExecutionContext"/> objects
	/// for the batch.
	/// </summary>
	[Serializable]
	public class BatchExecutionContext
	{
		private const double PAUSE_POLL_INTERVAL = 1; // timespan in minutes to
													  // poll for a change in 
													  // pausing
		private BatchExecutionContextData	_batchNode;
		private BatchExecutionRequest		_request;

		private IBatch						_batch;
		private bool						_isPaused;
		
		private EventHandler			_pausingEventHandler;
		private EventHandler			_resumingEventHandler;
		private EventHandler			_timedOutEventHandler;
		private TimingOutEventHandler	_timingOutEventHandler;

		private RelativeExpirationTimer	_executionTimeoutTimer;
		private bool	_timerStarted;
		private object	_sync;

		private const string ERROR_BATCH_TYPE_MISSING = 
								"An IBatch type is not configured for this batch definition.";
		private const string ERROR_BATCH_TYPE_LOADING = 
								"Failed to load type \"{0}\" for batch.";

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="BatchExecutionContext"/> class.
        /// </summary>
        /// <param name="batchNode">A configuration node
        /// that represents a batch definition.</param>
        /// <param name="request">A 
        /// <see cref="BatchExecutionRequest"/> object.</param>
		public BatchExecutionContext(BatchExecutionContextData batchNode, BatchExecutionRequest request)
		{
			_batchNode	= batchNode;
			_sync		= new object();
			_request	= request;
			_batch		= CreateBatch();
		}

        /// <summary>
        /// Called when the time period allocated for the execution
        /// of the batch has elapsed.
        /// </summary>
        /// <param name="state">object</param>
        /// <returns>void</returns>
		private void OnExecutionTimedout(object state)
		{
			TimingOutEventArgs args = new TimingOutEventArgs();

			// give subscribers a chance to extend the timeout
			RaiseTimingOutEvent(args);

			// if subscribers requested more time,
			// update the timer
			if (args.ExtendedTimeoutPeriod > TimeSpan.Zero)
			{
				_executionTimeoutTimer.Change(args.ExtendedTimeoutPeriod);

			}
			else
			{
				_executionTimeoutTimer.Dispose();
				// Broadcast that the execution has timed out
				// and must end.
				RaiseTimedOutEvent();
			}
		}


		/// <summary>
		/// Gets a collection of request-specific parameters
		/// indexed by friendly names.
		/// </summary>
		public ParameterCollection RequestParameters
		{
			get
			{
				return this._batchNode.RequestParameters;
			}
		}

		/// <summary>
		/// The friendly name of the batch definition.
		/// </summary>
		public string BatchName
		{
			get
			{
				return _batchNode.BatchName;
			}
		}

		/// <summary>
		/// Gets or Sets the Status of the Batch.
		/// The status can be any one of the values
		/// defined in the enumeration <see cref="BatchProcessStatusCode"/>
		/// </summary>
		public BatchProcessStatusCode Status
		{
			get
			{
				lock (_sync)
				{
					return _batchNode.BatchStatus;
				}
			}
			set
			{
				lock (this)
				{
					BatchProcessStatusCode previousStatus = Status;
				
					_batchNode.BatchStatus = value;

					// if the status has been updated 
					// to 'Executing', then start
					// the timer
					if (value == BatchProcessStatusCode.Executing
						&& previousStatus != value
						&& !(_timerStarted))
					{
						// setup the callback
						TimerCallback callback = 
							new TimerCallback(OnExecutionTimedout);

						// get the timeout period from the 
						// batch definition
						TimeSpan expiration = 
							BatchExecutionContextData.RelativeExpirationDate;

						// Start the timer
						_executionTimeoutTimer = 
							new RelativeExpirationTimer(callback, expiration);

						
						// update the flag, so it doesn't get started
						// twice
						_timerStarted = true;
					}
				}
			}
		}

		/// <summary>
		/// Gets a value indicating
		/// if the current batch execution
		/// represents a re-execution
		/// as the result of a previous
		/// failure.
		/// </summary>
		public bool IsARestart
		{
			get
			{
				return _batchNode.IsARestart;
			}
		}

		/// <summary>
		/// The underlying <see cref="BatchExecutionContextData"/>
		/// object that represents the current context.
		/// </summary>
		public BatchExecutionContextData BatchExecutionContextData
		{
			get
			{
				return _batchNode;
			}
		}

		/// <summary>
		/// Indicates if the Batch is currently in the Paused state
		/// </summary>
		private bool IsPaused
		{
			get
			{
				lock (_sync)
				{
					return _isPaused;
				}
			}
		}

		/// <summary>
		/// Gets the <see cref="BatchExecutionRequest"/>
		/// object that caused the current batch execution 
		/// to begin.
		/// </summary>
		public BatchExecutionRequest Request
		{
			get
			{
				return _request;
			}
		}


		/// <summary>
		/// Gets a value indicating if other instances
		/// of the currently executing batch can execute
		/// concurrently.
		/// </summary>
		/// <value>An <see cref="Int32"/> representing
		/// the maximum allowed number of concurrent executions.</value>
		public int AllowConcurrentExecution
		{
			get
			{
				return _batchNode.MaxConcurrentExecution;
			}
		}

		/// <summary>
		/// Gets a value indicating if the currently 
		/// executing batch should restart intelligently
		/// if the current execution fails and is 
		/// later re-executed.
		/// </summary>
		public BatchRestartBehavior RestartBehavior
		{
			get
			{
				return _batchNode.RestartBehavior;
			}
		}

        /// <summary>
        /// Commits to the database all of the current values in the context
        /// that have changed since the last commit.
        /// </summary>
        /// <remarks>
        /// This method should be called every time the 
        /// <see cref="JobExecutionContext.CommitFrequency"/>
        /// is reached.
        /// </remarks>
        /// <returns>void</returns>
		public void Commit()
		{
			lock (_sync)
			{
				// force a save of the batch execution context node
				_batchNode.Description = _batchNode.Description;
			
				_batchNode.Save();

				if (!IsPaused && _batchNode.ToPause && !ContextUtil.IsInTransaction)
				{
					Pause();
				}
			}
		}

        /// <summary>
        /// Saves the value of the <see cref="Status"/>
        /// property to the database.
        /// </summary>
        /// <returns>void</returns>
		public void UpdateStatus()
		{
			lock (_sync)
			{
				try
				{
					Lock();
					Commit();
				}
				finally
				{
					Unlock();
				}
			}
		}

		/// <summary>
		/// Gets a value indicating whether or not the 
		/// <see cref="BatchExecutionContext"/> object's
		/// properties are locked or not.
		/// </summary>
		/// <value>True if the context is locked, otherwise false.</value>
		public bool IsLocked
		{
			get
			{
				lock (_sync)
				{
					return _batchNode.ContextIsLocked;
				}
			}
		}

        /// <summary>
        /// Puts the <see cref="BatchExecutionContext"/>
        /// object in a state where only its <see cref="Status"/>
        /// property can be saved to the database. Its other
        /// properties and parameters can modified in memory
        /// but the <see cref="Commit"/>
        /// operation will not persist those values to the database
        /// until the <see cref="BatchExecutionContext"/> is unlocked.
        /// </summary>
        /// <returns>void</returns>
		public void Lock()
		{
			lock (_sync)
			{
				_batchNode.LockContext();
			}
		}

        /// <summary>
        /// Puts the <see cref="BatchExecutionContext"/>
        /// object in a state where all its properties and
        /// parameters can be saved to the batch database.
        /// </summary>
        /// <returns>void</returns>
		public void Unlock()
		{
			lock (_sync)
			{
				_batchNode.UnlockContext();
			}
		}


        /// <summary>
        /// Fires the <see cref="Pausing"/> event,
        /// puts the <see cref="BatchExecutionContext"/>
        /// into a Paused state and blocks until the 
        /// a resume is signaled, fires the <see cref="Resuming"/>
        /// event and returns.
        /// </summary>
        /// <returns>void</returns>
		public void Pause()
		{
			lock (_sync)
			{
				if (!_batchNode.ToPause)
				{
					DefaultBatchManager.RequestQueue.
						SetPause(_batchNode.BatchDatabase, _batchNode.Key, true);
				}
				// broadcast that the execution is preparing
				// to pause itself
				RaisePausingEvent();
					
				SetIsPaused(true);                
                BatchInstrumentationProvider.Instance.FireRequestPausedEvent(_batchNode.Key, _batchNode.BatchName);

				// block until the status has changed
				TimeSpan interval = 
					TimeSpan.FromMinutes(PAUSE_POLL_INTERVAL);
					
				_batchNode.Pause(interval);

				SetIsPaused(false);

				// broadcast that the execution can
				// now resume
				RaiseResumingEvent();
                BatchInstrumentationProvider.Instance.FireRequestResumedEvent(_batchNode.Key, _batchNode.BatchName);
			}
		}

        /// <summary>SetIsPaused</summary>
        /// <param name="value">bool</param>
        /// <returns>void</returns>
        private void SetIsPaused(bool value)
		{
			_isPaused = value;
		}

        /// <summary>CreateBatch</summary>
        /// <returns>Avanade.ACA.Batch.IBatch</returns>
        private IBatch CreateBatch()
		{
			if (_batchNode.BatchTypeName == null)
			{                
                BatchInstrumentationProvider.Instance.FireBatchArchitectureFailedEvent(ERROR_BATCH_TYPE_MISSING, null);
				Status = BatchProcessStatusCode.Failed;
				Commit();
				throw new ACABatchFatalException(ERROR_BATCH_TYPE_MISSING);
			}

			try
			{
				Type batchType = Type.GetType(_batchNode.BatchTypeName, true);

				BatchArchitectureEvent.AppendExecutionLog("Batch type is loaded successfully.");

				object batch = Activator.CreateInstance(batchType);

				BatchArchitectureEvent.AppendExecutionLog("Batch object instance is created successfully.");

				return (IBatch)batch;
			}
			catch(Exception e)
			{
				string errorMsg = string.Format(ERROR_BATCH_TYPE_LOADING, _batchNode.BatchTypeName);
                BatchInstrumentationProvider.Instance.FireBatchArchitectureFailedEvent(errorMsg, e);
				
				Status = BatchProcessStatusCode.Failed;
				Commit();
				throw new ACABatchFatalException(errorMsg, e);
			}
		}

		/// <summary>
		/// Gets the <see cref="IBatch"/> object
		/// that is controlling the
		/// current execution.
		/// </summary>
		/// <value>An instance of the configured
		/// <see cref="IBatch"/> object.</value>
		public IBatch Batch
		{
			get
			{
				return _batch;
			}
		}

		/// <summary>
		/// Gets the state of the current <see cref="Transaction"/>
		/// property.
		/// </summary>
		/// <value>One of the <see cref="TransactionState"/>
		/// enumerated values.</value>
		public DbTransactionState TransactionState
		{
			get
			{
				lock (_sync)
				{
					return _batchNode.TransactionState;
				}
			}
		}

		/// <summary>
		/// Gets an <see cref="DbTransaction"/>
		/// object that the <see cref="BatchExecutionContext"/>
		/// uses when committing it properties and parameters 
		/// to the database. It is exposed publicly so that 
		/// other database work that depends on the success or
		/// failure of the context committing can share its transaction.
		/// </summary>        
		public DbTransaction Transaction
		{
			get
			{
				lock (_sync)
				{
					return _batchNode.Transaction;
				}
			}
		}

        /// <summary>
        /// Opens a connection and 
        /// begins a new transaction in the batch 
        /// database. This transaction is exposed
        /// through the <see cref="Transaction"/> property
        /// and will be used when the <see cref="Commit"/>
        /// method is invoked.
        /// </summary>
        /// <returns>void</returns>
		public void BeginTransaction()
		{
			lock (_sync)
			{
				_batchNode.BeginTransaction();
			}
		}

        /// <summary>
        /// Sets the current <see cref="Transaction"/>
        /// to null. This should only be called when
        /// the <see cref="DbTransactionState"/> is
        /// <see cref="DbTransactionState.Finished"/>, and
        /// then only after the commit operation has been
        /// called. 
        /// </summary>
        /// <example><code>
        /// public class MyJob : Job
        /// {
        /// 	protected override StatusCode ExecuteImpl()
        /// 	{
        /// 		Context.BatchExecutionContext.BeginTransaction();
        /// 		IDbTransaction transaction = 
        /// 			Context.BatchExecutionContext.Transaction;
        /// 		
        /// 		// use the transaction to do database work 
        /// 		// until you're job is ready to commit its context.	
        /// 			
        /// 		Context.Commit();
        /// 		Context.BatchExecutionContext.DisposeTransaction();
        /// 		
        /// 	}
        /// }
        /// </code></example>
        /// <returns>void</returns>
		public void DisposeTransaction()
		{
			lock (_sync)
			{
				_batchNode.DisposeTransaction();
			}
		}

        /// <summary>
        /// Gets a copy of the 
        /// the <see cref="JobExecutionContext"/>
        /// objects associated with currently 
        /// executing batch.
        /// </summary>
        /// <returns>An array of JobExecutionContext objects</returns>
		public JobExecutionContext[] GetJobExecutionContexts()
		{
			BatchArchitectureEvent.AppendExecutionLog("BatchExecutionContext.GetJobExecutionContexts is invoked.");

			JobExecutionContextData[] jobs = _batchNode.JobExecutionContexts;
			ArrayList list = new ArrayList(jobs);
			
			JobExecutionContext[] contexts =
				new JobExecutionContext[list.Count];

			for(int i = 0; i < list.Count; i++)
			{
				JobExecutionContextData job = (JobExecutionContextData)list[i];

				JobExecutionContext context = 
					new JobExecutionContext(this, job);

				contexts[i] = context;
			}

			return contexts;
		}

		/// <summary>
		/// An Event raised when a request to pause the Batch is made.
		/// Allows subscribers (jobs) to prepare for pausing.
		/// 
		/// </summary>
		public event EventHandler Pausing
		{
			add
			{
				_pausingEventHandler += value;
			}
			remove
			{
				_pausingEventHandler -= value;
			}
		}

		/// <summary>
		/// An Event raised when a paused request is resuming execution.
		/// Allows subscribers (jobs) to prepare for resuming.
		/// </summary>
		public event EventHandler Resuming
		{
			add
			{
				_resumingEventHandler += value;
			}
			remove
			{
				_resumingEventHandler -= value;
			}
		}

		/// <summary>
		/// TimeOut Event. Raised when the allocated time period
		/// for the batch's execution has elapsed and the TimingOut
		/// event has been processed.
		/// </summary>
		public event EventHandler TimedOut
		{
			add
			{
				_timedOutEventHandler += value;
			}
			remove
			{
				_timedOutEventHandler -= value;
			}
		}

		/// <summary>
		/// TimingOut Event. Raised when the allocated time period
		/// for the batch's execution has elapsed.
		/// </summary>
		public event TimingOutEventHandler TimingOut
		{
			add
			{
				_timingOutEventHandler += value;
			}
			remove
			{
				_timingOutEventHandler -= value;
			}
		}

        /// <summary>RaisePausingEvent</summary>
        /// <returns>void</returns>
        private void RaisePausingEvent()
		{
			if (_pausingEventHandler != null)
			{
				_pausingEventHandler(this, EventArgs.Empty);
			}
		}

        /// <summary>RaiseResumingEvent</summary>
        /// <returns>void</returns>
        private void RaiseResumingEvent()
		{
			if (_resumingEventHandler != null)
			{
				_resumingEventHandler(this, EventArgs.Empty);
			}
		}

        /// <summary>RaiseTimedOutEvent</summary>
        /// <returns>void</returns>
        private void RaiseTimedOutEvent()
		{
			if (_timedOutEventHandler != null)
			{
				_timedOutEventHandler(this, EventArgs.Empty);
			}
		}

        /// <summary>RaiseTimingOutEvent</summary>
        /// <param name="args">Avanade.ACA.Batch.TimingOutEventArgs</param>
        /// <returns>void</returns>
        private void RaiseTimingOutEvent(TimingOutEventArgs args)
		{
			if (_timingOutEventHandler != null)
			{
				_timingOutEventHandler(this, args);
			}
		}

	}
}
