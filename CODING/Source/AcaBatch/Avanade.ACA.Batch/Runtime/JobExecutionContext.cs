// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;

using Avanade.ACA.Batch.Configuration;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// Represents a runtime, dynamic view of the parameters
	/// and properties that make up a given Job definition.
	/// </summary>
	[Serializable]
	public class JobExecutionContext
	{
		private string					_key;
		private ParameterCollection		_parameters;
		private BatchExecutionContext	_context;
		private IJob					_job;
		private SnapshotCollection		_snapshots;

		private JobExecutionContextData	_jobNode;

        /// <summary>
        /// Initializes a new instance 
        /// of the <see cref="JobExecutionContext"/> class.
        /// </summary>
        /// <param name="context">The 
        /// <see cref="BatchExecutionContext"/> object
        /// of which the <see cref="JobExecutionContext"/> object
        /// is a part.</param>
        /// <param name="jobNode">Avanade.ACA.Batch.JobExecutionContextData</param>
		public JobExecutionContext(BatchExecutionContext context,
									JobExecutionContextData jobNode)
		{
			_key			= Guid.NewGuid().ToString();
			_context		= context;
			_jobNode		= jobNode;
			_parameters		= _jobNode.Parameters;
			_snapshots		= new SnapshotCollection(this);
		}

		/// <summary>
		/// Gets or set a code indicating 
		/// how this job will behave when restarted.
		/// </summary>
		/// <value>One of the <see cref="JobRestartBehavior"/> values. The 
		/// default is <see cref="JobRestartBehavior.Full"/>.</value>
		public JobRestartBehavior RestartBehavior
		{
			get
			{
				return _jobNode.RestartBehavior;
			}
			set
			{
				_jobNode.RestartBehavior = value;
			}
		}

		/// <summary>
		/// Gets the friendly name of the 
		/// Job definition.
		/// </summary>
		public string JobName
		{
			get
			{
				return _jobNode.JobName;
			}
		}

		/// <summary>
		/// See <see cref="IBatchData.DisplayName"/>.
		/// </summary>
		public string DisplayName
		{
			get
			{
				return this.JobName;
			}
		}

		/// <summary>
		/// Gets a value that indicates
		/// how frequently the current
		/// <see cref="JobExecutionContext"/>
		/// should commit its state to the
		/// database.
		/// </summary>
		/// <value>A 64-bit integer.</value>
		/// <remarks></remarks>
		public long CommitFrequency
		{
			get
			{
				return _jobNode.CommitFrequency;
			}
			set
			{
				_jobNode.CommitFrequency = value;
			}
		}
		
		/// <summary>
		/// Gets the number of times the job context
		/// has commited its state to the database.
		/// </summary>
		public int CommitCount
		{
			get
			{
				return _jobNode.CommitCount;
			}
		}

		/// <summary>
		/// Gets the status of the execution
		/// previous to the current execution
		/// of the job.
		/// </summary>
		public StatusCode PreviousExecutionStatus
		{
			get
			{
				return _jobNode.PreviousExecutionStatus;
			}
		}

        /// <summary>CreateJob</summary>
        /// <returns>Avanade.ACA.Batch.IJob</returns>
        private IJob CreateJob()
		{
			if (_jobNode.JobType == null 
				|| _jobNode.JobType== null
				|| _jobNode.JobType == String.Empty)
			{
				throw new ACABatchFatalException("An IJob type is not"
					+ " configured for this"
					+ " job definition.");
			}

			Type jobType = Type.GetType(_jobNode.JobType, true);
			object job = Activator.CreateInstance(jobType);
			return (IJob)job;
		}

		/// <summary>
		/// Gets the <see cref="IJob"/> object
		/// associated with the current 
		/// <see cref="JobExecutionContext"/>.
		/// </summary>
		public IJob Job
		{
			get
			{
				if (_job == null)
				{
					_job = CreateJob();
				}
				return _job;
			}
		}

		/// <summary>
		/// Gets a collection of job-specific
		/// parameters indexed by a 
		/// friendly name.
		/// </summary>
		/// <remarks>
		/// 
		/// </remarks>
		public ParameterCollection Parameters
		{
			get
			{
				return _parameters;
			}
		}

		/// <summary>
		/// Gets the associated <see cref="BatchExecutionContext"/>
		/// object inside which the current 
		/// <see cref="JobExecutionContext"/> is running.
		/// </summary>
		public BatchExecutionContext BatchExecutionContext
		{
			get
			{
				return _context;
			}
		}

		/// <summary>
		/// Gets the status of the
		/// of the current job execution.
		/// </summary>
		public StatusCode Status
		{
			get
			{
				return _jobNode.Status;
			}
			set
			{
				_jobNode.Status = value;
			}
		}

		/// <summary>
		/// Gets the running count
		/// of work units the executing job
		/// has performed.
		/// </summary>
		public int WorkUnitCount
		{
			get
			{
				return _jobNode.WorkUnitCount;
			}

		}

		/// <summary>
		/// Gets the generated unique identifier for
		/// the current job execution.1
		/// </summary>
		public Guid Key
		{
			get
			{
				return _jobNode.Key;
			}
		}

		/// <summary>
		/// Gets the date and time at which
		/// the job began execution.
		/// </summary>
		public DateTime StartDate
		{
			get
			{
				return _jobNode.StartDate;
			}
		}

		/// <summary>
		/// Gets the date and time at 
		/// which the job execution commited
		/// its context to the database.
		/// </summary>
		public DateTime LastCommitDate
		{
			get
			{
				return _jobNode.LastCommitDate;
			}
		}

        /// <summary>
        /// Increments the value of the <see cref="CommitCount"/>
        /// property and saves the current property and
        /// parameter values of the context to the database.
        /// </summary>
        /// <returns>void</returns>
		public void Commit()
		{
			JobExecutionContextData.IncrementCommitCount();			
			Save();
		}

        /// <summary>
        /// Saves only the value of the <see cref="Status"/>
        /// property to the database.
        /// </summary>
        /// <returns>void</returns>
		public void UpdateStatus()
		{
			_jobNode.UpdateStatus();
		}

        /// <summary>
        /// Saves the current property values
        /// and parameter values to the database.
        /// </summary>
        /// <returns>void</returns>
		public void Save()
		{
			BatchExecutionContext.Commit();
		}

        /// <summary>
        /// Increments the work unit count
        /// by the number specified. If the
        /// resulting work unit count 
        /// crosses the commit frequency,
        /// the context will be commited and
        /// the commit count will be incremented.
        /// </summary>
        /// <param name="i">int</param>
        /// <returns>void</returns>
		public void IncrementWorkUnitCount(int i)
		{
			for (int j=0; j<i; ++j)
			{
				IncrementWorkUnitCount();
			}
		}

        /// <summary>
        /// Increments the work unit count by one
        /// and compares the resulting WorkUnitCount
        /// to the CommitFrequency to determine
        /// if it should commit the context.
        /// </summary>
        /// <returns>void</returns>
		public void IncrementWorkUnitCount()
		{
			_jobNode.IncrementWorkUnitCount(1);
			
			if (WorkUnitCount %
				CommitFrequency == 0)
			{
				Commit();
			}
		}


		/// <summary>
		/// Gets the underlying JobExecutionContextData object.
		/// </summary>
		/// <returns>A JobExecutionContextData</returns>
		public JobExecutionContextData JobExecutionContextData
		{
			get
			{
				return _jobNode;
			}
		}

        /// <summary>
        /// Adds an exception to the <see cref="ExceptionCollection"/>.
        /// </summary>
        /// <param name="e">The exception to be added.</param>
        /// <returns>void</returns>
		public void AddException(Exception e)
		{
			_jobNode.Exceptions.AddException(e);
		}

		/// <summary>
		/// Gets a value indicating whether or not the 
		/// <see cref="JobExecutionContext"/> object's
		/// properties are locked or not.
		/// </summary>
		/// <value>True if the context is locked, otherwise false.</value>
		public bool IsLocked
		{
			get
			{
				return _jobNode.ContextIsLocked;
			}
		}

        /// <summary>
        /// Puts the <see cref="JobExecutionContext"/>
        /// object in a state where only its <see cref="Status"/>
        /// property can be saved to the database. Its other
        /// properties and parameters can modified in memory
        /// but the <see cref="Save"/> and <see cref="Commit"/>
        /// operations will not persist those values to the database
        /// until the <see cref="JobExecutionContext"/> is unlocked.
        /// </summary>
        /// <remarks>The context is typically locked after an exception occurs
        /// during a commit, because the property values in memory
        /// may not be synched with the values in the database and 
        /// a subsequent commit would incorrectly overwrite the values
        /// in the database.</remarks>
        /// <returns>void</returns>
		public void Lock()
		{
			_jobNode.LockContext();
		}

        /// <summary>
        /// Puts the <see cref="JobExecutionContext"/>
        /// object in a state where all its properties and
        /// parameters can be saved to the batch database.
        /// </summary>
        /// <returns>void</returns>
		public void Unlock()
		{
			_jobNode.UnlockContext();
		}

		/// <summary>
		/// Gets a <see cref="Snapshot"/>
		/// collection that can be used to record
		/// and restore snaphots of the state of the 
		/// <see cref="JobExecutionContext"/>
		/// and a point in time during its execution.
		/// </summary>
		public SnapshotCollection Snapshots
		{
			get
			{
				return _snapshots;
			}
		}

	}
}
