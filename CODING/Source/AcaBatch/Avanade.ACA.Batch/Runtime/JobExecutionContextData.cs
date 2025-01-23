// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Avanade.ACA.Batch.Configuration;
using Avanade.ACA.Batch.Instrumentation;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// JobExecutionContextData represents the Batch job data during the execution.
	/// </summary>
	[Serializable]    
    public class JobExecutionContextData : NamedConfigurationElement, IBatchDBData
	{
		private Guid			_jobExecutionKey;
		private short			_sequence;
		
		private string			_description;
		private string			_jobTypeName;
		private string			_jobName;
		private DateTime		_startDate;
		private StatusCode		_statusCode;
		private DateTime		_lastCommitDate;
		private int				_workUnitCount;
		private int				_restartCount;
		private int				_commitCount;
		private Guid			_originalJobKey;
		private JobRestartBehavior	_restartBehavior;
		private int				_commitFrequency;
		private StatusCode		_lastExecutionStatus;
		private bool			_isLocked;


		private DateTime		_lastWorkUnitIncrementTime;

		private BatchExecutionContextData batchReference;
		private Database			batchDB;
		private Guid				batchKey;
		private string				displayName;
		private ParameterExecutionContextCollection parmCollection;
		private ExceptionCollection	exceptionCollection;
		private ArrayList			children;

        /// <summary>JobExecutionContextData</summary>
        public JobExecutionContextData()
        { 
        }

        /// <summary>
        /// Initializes object with its key and the <see cref="BatchExecutionContextData"/>
        /// that contains it.
        /// </summary>
        /// <param name="jobkey">The key of the job execution.</param>
        /// <param name="batch">The <see cref="BatchExecutionContextData"/> during the runtime.</param>
        public JobExecutionContextData(Guid jobkey, BatchExecutionContextData batch)
            : base(jobkey.ToString())
		{
			batchReference = batch;
			this._jobExecutionKey = jobkey;
			this.batchDB = batchReference.BatchDatabase;
			this.batchKey = batchReference.Key;
			parmCollection = new  ParameterExecutionContextCollection(batchDB, _jobExecutionKey,ParameterCategory.JobExecutionContextCurrent);
			exceptionCollection = new ExceptionCollection(batchDB, _jobExecutionKey);
			children = new ArrayList();

			_lastWorkUnitIncrementTime = DateTime.MaxValue;
			_commitFrequency = 10;
			_restartBehavior = JobRestartBehavior.Smart;
			_description = "";
		}

        /// <summary>
        /// Loads parameters and execptions, for the job execution, if there's any.
        /// </summary>
        /// <returns>void</returns>
		public void LoadChildren()
		{
			parmCollection.LoadParameters();
			exceptionCollection.LoadExceptions();
			children.Clear();
			children.Add(parmCollection);
			children.Add(exceptionCollection);
		}


		#region Custom Properties

		/// <summary>
		/// Gets the Key of the Job execution context.
		/// </summary>
		public Guid Key
		{
			get
			{
				return _jobExecutionKey;
			}
		}

		/// <summary>
		/// Gets or sets the Job type as a string.
		/// </summary>
		public string JobType
		{
			get
			{
				return _jobTypeName;
			}
			set
			{
				_jobTypeName = value;
			}
		}

		/// <summary>
		/// Gets or sets sequence number within the Batch for the job.
		/// </summary>
		public short Sequence
		{
			get
			{
				return _sequence;
			}

			set {this._sequence = value;}
		}
		
		/// <summary>
		/// Gets or sets the name of the jobs.
		/// </summary>
		public string JobName
		{
			get
			{
					return _jobName;
			}

			set
			{
				this._jobName = value;
			}
		}

		/// <summary>
		/// Gets or sets the description of the job.
		/// </summary>
		public string Description
		{
			get
			{
				return _description;
			}
			set
			{
				_description = value;
			}
		}	
	
		/// <summary>
		/// Gets or sets the <see cref="JobRestartBehavior"/> for the job.
		/// </summary>
		public JobRestartBehavior RestartBehavior
		{
			get
			{
				return _restartBehavior;
			}
			set
			{
				_restartBehavior = value;
			}
		}

		/// <summary>
		/// Gets or sets the job committing frequency of the job.
		/// </summary>
		public long CommitFrequency
		{
			get
			{
				return _commitFrequency;
			}
			set
			{
				_commitFrequency = (int)value;
			}
		}

		/// <summary>
		/// Gets or sets the number of commits the job has done so far.
		/// </summary>
		public int CommitCount
		{
			get
			{
				return _commitCount;
			}
			set
			{
				_commitCount = value;
			}
		}

		/// <summary>
		/// See <see cref="IBatchData.DisplayName"/>.
		/// </summary>
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.displayName = value;
			}
		}

		/// <summary>
		/// See <see cref="IBatchDBData.Children"/>.
		/// </summary>
		IList IBatchDBData.Children
		{
			get { return this.children;}
		}

        /// <summary>
        /// Increments the commit count for the job.
        /// </summary>
        /// <param name="i">The number of increments.</param>
        /// <returns>void</returns>
		public void IncrementCommitCount(int i)
		{
			_commitCount = _commitCount + i;
			_lastCommitDate = DateTime.Now;
		}

        /// <summary>
        /// Increments the commit count by 1.
        /// </summary>
        /// <returns>void</returns>
		public void IncrementCommitCount()
		{
			IncrementCommitCount(1);
		}

		/// <summary>
		/// Gets or sets the work unit count for the job execution.
		/// </summary>
		public int WorkUnitCount
		{
			get
			{
				return _workUnitCount;
			}
			set
			{
				_workUnitCount = value;
			}
		}

        /// <summary>
        /// Increments the number of work unit completes.
        /// </summary>
        /// <param name="i">The number to increment.</param>
        /// <returns>void</returns>
		public void IncrementWorkUnitCount(int i)
		{
			_workUnitCount = _workUnitCount + i;
			DateTime currentTime = DateTime.Now;
			if (_lastWorkUnitIncrementTime == DateTime.MaxValue) // increment before calling SetWorkUnitStart()
			{			 
                BatchInstrumentationProvider.Instance.FireJobWorkUnitCompletedEvent(TimeSpan.FromTicks(1), i);
			}
			else
			{
                BatchInstrumentationProvider.Instance.FireJobWorkUnitCompletedEvent(currentTime - _lastWorkUnitIncrementTime, i);
			}
			_lastWorkUnitIncrementTime = currentTime;
		}

        /// <summary>
        /// Increments the number of work unit completed by 1.
        /// </summary>
        /// <returns>void</returns>
		public void IncrementWorkUnitCount()
		{
			IncrementWorkUnitCount(1);
		}


        /// <summary>
        /// Set the start time for the current work unit.
        /// </summary>
        /// <returns>void</returns>
		public void SetWorkUnitStart()
		{
			_lastWorkUnitIncrementTime = DateTime.Now;
		}

		/// <summary>
		/// Gets or sets the status for the job execution.
		/// </summary>
		public StatusCode Status
		{
			get
			{
				return _statusCode;
			}
			set
			{
				_statusCode = value;
			}
		}

		/// <summary>
		/// Gets or sets the timestamp of when the job started.
		/// </summary>
		public DateTime StartDate
		{
			get
			{
				return _startDate;
			}
			set
			{
				this._startDate = value;
			}
		}

		/// <summary>
		/// Gets or sets the number of restarts the job has gone through.
		/// </summary>
		public int RestartCount
		{
			get
			{
				return _restartCount;
			}
			set {this._restartCount = value;}
		}

		/// <summary>
		/// Gets or sets the timestamp of when the last commit is done.
		/// </summary>
		public DateTime LastCommitDate
		{
			get
			{
				return _lastCommitDate;
			}

			set
			{
				_lastCommitDate = value;
			}
		}

		/// <summary>
		/// Gets or sets the original job key.  <see cref="JobDefinitionData.Key"/>.
		/// </summary>
		public Guid OriginalJobDefinitionKey
		{
			get
			{
				return _originalJobKey;
			}

			set
			{
				this._originalJobKey = value;
			}
		}

		/// <summary>
		/// Gets or sets the status of the previous execution, if this job is a restart.
		/// </summary>
		public StatusCode PreviousExecutionStatus
		{
			get
			{
				return _lastExecutionStatus;
			}
			set
			{
				this._lastExecutionStatus = value;
			}
		}

		/// <summary>
		/// Gets the execptions of the Job execution.
		/// </summary>
		public ExceptionCollection Exceptions
		{
			get
			{
				return this.exceptionCollection;
			}
		}

		/// <summary>
		/// Gets the parameters of the job execution.
		/// </summary>
		public ParameterCollection Parameters
		{
			get
			{
				return this.parmCollection;
			}
		}

		#endregion


		#region Loading and saving methods

        /// <summary>
        /// See <see cref="JobExecutionContext.UpdateStatus"/>.
        /// </summary>
        /// <returns>void</returns>
		public void UpdateStatus()
		{
			try
			{
				LockContext();
				Save();
			}
			finally
			{
				UnlockContext();
			}
		}

        /// <summary>
        /// See <see cref="JobExecutionContext.Save"/>.
        /// </summary>
        /// <returns>void</returns>
		public void Save()
		{

			RequestCommitHandle handle = batchReference.CommitHandle;
			
			if (ContextIsLocked)
			{

				if (handle == null)
				{
					// Check if the current transaction is usable
					if (batchReference.TransactionState == DbTransactionState.Finished || 
						batchReference.TransactionState == DbTransactionState.Empty)
					{
						// The current transaction isn't usable, so
						// dispose of it and begin a new one.
						batchReference.DisposeTransaction();
						batchReference.BeginTransaction();
					}

					// create a RequestCommitHandle object
					handle = DefaultBatchManager.RequestQueue
						.CreateCommitStatusHandle(this.batchDB, this.batchKey, batchReference.Transaction);

					// now that we have a handle, run the stored
					// procedure to update the status code in the database.
					try
					{
						// update the job status
						handle.UpdateJobStatus(Key, Status);
						// Committing the children with the same handle
						SaveChildren(handle);

						// commit the transaction
						handle.EndCommit(true);
					}
					catch
					{
						// rollback the transaction
						handle.EndCommit(false);
						throw;
					}
					finally
					{
						// dispose of the transaction
						batchReference.DisposeTransaction();
					}
				}
				else
				{
					// the handle has been created by the parent
					// BatchExecutionData. It will handle all exceptions
					// and committing logic, so just execute 
					// the stored procedure
					SaveChildren(handle);
					handle.UpdateJobStatus(Key, Status);
				}
			}
			else
			{
				SaveChildren(handle);
				handle.CommitJobStatus(Key, Status, WorkUnitCount, LastCommitDate, CommitCount);
			}
		}


        /// <summary>SaveChildren</summary>
        /// <param name="handle">Avanade.ACA.Batch.RequestCommitHandle</param>
        /// <returns>void</returns>
        private void SaveChildren(RequestCommitHandle handle)
		{
			parmCollection.SaveParameters(handle, ContextIsLocked);
			exceptionCollection.SaveExceptions(handle);
		}

		#endregion


		/// <summary>
		/// See <see cref="JobExecutionContext.IsLocked"/>.
		/// </summary>
		public bool ContextIsLocked
		{
			get
			{
				return _isLocked;
			}
		}

        /// <summary>
        /// See <see cref="JobExecutionContext.Lock"/>.
        /// </summary>
        /// <returns>void</returns>
		public void LockContext()
		{
			_isLocked = true;
		}

        /// <summary>
        /// See <see cref="JobExecutionContext.Lock"/>.
        /// </summary>
        /// <returns>void</returns>
		public void UnlockContext()
		{
			_isLocked = false;
		}

		internal class Snapshot
		{
			private JobExecutionContext _context;
			private int			_commitCount;
			private int			_workUnitCount;
			private DateTime	_lastCommitDate;
			private StatusCode  _statusCode;

			private ParameterCollection _parameters;

            /// <summary>
            /// Initializes the data from the <see cref="JobExecutionContextData"/>
            /// of its <see cref="JobExecutionContext"/> object.
            /// </summary>
            /// <param name="context">Avanade.ACA.Batch.JobExecutionContext</param>
			public Snapshot(JobExecutionContext context)
			{
				_context		= context;
				_commitCount	= context.CommitCount;
				_workUnitCount	= context.WorkUnitCount;
				_lastCommitDate = context.LastCommitDate;
				_statusCode		= context.Status;

				BatchExecutionRequest request = new BatchExecutionRequest(Guid.Empty);
				_parameters = request.Parameters;
			
				_parameters.AddRange(context.Parameters);
			}

            /// <summary>
            /// Restores the data from the <see cref="JobExecutionContextData"/>
            /// of its <see cref="JobExecutionContext"/> object.
            /// </summary>
            /// <returns>void</returns>
			public void Restore()
			{
				JobExecutionContextData node = _context.JobExecutionContextData;
				
				node._commitCount		= _commitCount;
				node._workUnitCount		= _workUnitCount;
				node._lastCommitDate	= _lastCommitDate;
				node._statusCode		= _statusCode;

                for (int i = 0; i < _parameters.Count; i++)
                {
                    string key = _parameters.Get(i).Key.ToString();
                    _context.Parameters.SetPrimitiveData(key, _parameters.Get(i).Value);
                }
			}

            /// <summary>
            /// Gets the parameters for the job execution snapshot by its name.
            /// </summary>
            /// <param name="name">The name of the parameter to find.</param>
            /// <returns>See
            /// <see cref="Avanade.ACA.Batch.Configuration.ParameterData"/> object.</returns>
			public object GetParameter(string name)
			{
				return _parameters[name];
			}

			/// <summary>
			/// See <see cref="JobExecutionContextData.CommitCount"/>.
			/// </summary>
			public int CommitCount
			{
				get
				{
					return _commitCount;
				}
			}

			/// <summary>
			/// See <see cref="JobExecutionContextData.WorkUnitCount"/>.
			/// </summary>
			public int WorkUnitCount
			{
				get
				{
					return _workUnitCount;
				}
			}

			/// <summary>
			/// See <see cref="JobExecutionContextData.LastCommitDate"/>.
			/// </summary>
			public DateTime LastCommitDate
			{
				get
				{
					return _lastCommitDate;
				}
			}

			/// <summary>
			/// The statis of the job execution snapshot.
			/// </summary>
			public StatusCode Status
			{
				get
				{
					return _statusCode;
				}
			}
		}
	}
}





