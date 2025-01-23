// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Threading;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Avanade.ACA.Batch.Instrumentation;
using System.Data.Common;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// The BatchExecutionContextData represents a Batch request object.
	/// </summary>
	[Serializable]
	public class BatchExecutionContextData : IBatchDBData
	{
		private const string 
			CONTEXT_ALREADY_ARCHIVED = "Failed to update the context, because "
			+ "it has already been archived. "
			+ "It was previously updated "
			+ "with a status of 'Succeeded'. ";

		/// <summary>
		/// The label.
		/// </summary>
		public const string LabelLiteral = "Execution Context";
		/// <summary>
		/// The path.
		/// </summary>

		//Private fields
		private string					_description;
		private BatchRestartBehavior	_restartBehavior;
		private ExecutionIsolationLevel _isolationLevel;
		private ExecutionPriorityLevel	_exePriorityLevel; 
		private QueuePriorityLevel		_queuePriorityLevel; 
		private int						_maxConcurrent;
		private string					_batchTypeName;
		private string					_batchName;
		private string					_configFilePath;
		private TimeSpan				_relativeExpDate;
		private string					_destination;
		private Guid					_instanceKey;
		private string					_batchServerName;
		private string					_batchClientName;
		private DateTime				_queuedDate;
		private DateTime				_startDate;
		private Guid					_originalBatchKey;
		private Guid					_parentRequestKey;
		private Guid					_lastExecutionKey;
		private Guid					_nextExecutionKey;
		private Guid					_requestKey;
		private BatchProcessStatusCode	_batchStatus;
		private DateTime				_lastUpdateDate;
		private DateTime				_absoluteExpirationDate;
		private bool					_toPause;
		private bool					_archived;

		private int						_childRequestCount;
		private RequestCommitHandle		_commitHandle;        
		private DbTransaction			_transaction;
		private readonly Database		_database;
		private bool					_isLocked;

		private JobExecutionContextCollection	jobCollection;
		private ParameterExecutionContextCollection	parmCollection;
		private ArrayList alChildren;

        /// <summary>
        /// The constructor that allows setting of database.
        /// </summary>
        /// <param name="database">Microsoft.Practices.EnterpriseLibrary.Data.Database</param>
        /// <param name="requestKey">System.Guid</param>
		public BatchExecutionContextData(Database database, Guid requestKey)
		{
			Initialize();
			_requestKey = requestKey;
			_archived	= false;
			_database = database;
			jobCollection = new JobExecutionContextCollection(this);//database,requestKey);
			parmCollection = new ParameterExecutionContextCollection(database, requestKey, ParameterCategory.BatchExecutionContextCurrent);
			alChildren = new ArrayList(2);
		}


		/// <summary>
		/// See <see cref="BatchExecutionContext.IsLocked"/>.
		/// </summary>
		public bool ContextIsLocked
		{
			get
			{
				return _isLocked;
			}
		}

        /// <summary>
        /// See <see cref="BatchExecutionContext.Lock"/></summary>
        /// <returns>void</returns>
		public void LockContext()
		{
			_isLocked = true;
		}

        /// <summary>
        /// See <see cref="BatchExecutionContext.Unlock"/>.
        /// </summary>
        /// <returns>void</returns>
		public void UnlockContext()
		{
			_isLocked = false;
		}


		#region Loading and saving methods
        /// <summary>LoadFromDatabase</summary>
        /// <returns>bool</returns>
		public bool LoadFromDatabase()
		{
			
			//Make sure the database is available
			if( _database == null )
			{
				return false;
			}

			BatchRestartBehavior	restart;
			long					relativeExpirationDateInMilli;

			DefaultBatchManager.RequestQueue.Get(_database,
				_requestKey,
				out _batchName,
				out _description,
				out restart,
				out _configFilePath,
				out _isolationLevel,
				out _exePriorityLevel,
				out _queuePriorityLevel,
				out _maxConcurrent,
				out _destination,
				out _batchTypeName,
				out relativeExpirationDateInMilli,
				out _absoluteExpirationDate,
				out _batchServerName,
				out _batchClientName,
				out _queuedDate,
				out _startDate,
				out _lastUpdateDate,
				out _batchStatus,
				out _originalBatchKey,
				out _lastExecutionKey,
				out _toPause,
				out _parentRequestKey,
				out _nextExecutionKey);

			_relativeExpDate = TimeSpan.FromMilliseconds(relativeExpirationDateInMilli);
			_restartBehavior = (BatchRestartBehavior)Convert.ToByte(restart);

			return LoadChildren();
		}

        /// <summary>LoadChildren</summary>
        /// <returns>bool</returns>
        private bool LoadChildren()
		{
			bool loaded = jobCollection.LoadJobs() && parmCollection.LoadParameters();

			alChildren.Clear();
			alChildren.Add(jobCollection);
			alChildren.Add(parmCollection);

			return loaded;
		}

		/// <summary>
		/// 
		/// </summary>
		public DbTransactionState TransactionState
		{
			get
			{
					if (Transaction == null)
					{
						return DbTransactionState.Empty;
					}
					else if (Transaction.Connection == null
						|| (Transaction.Connection.State & ConnectionState.Open) == 0)
					{
						return DbTransactionState.Finished;
					}
					else
					{
						return DbTransactionState.Started;
					}
			}
		}

		/// <summary>
		/// See <see cref="BatchExecutionContext.Transaction"/>.
		/// </summary>        
		public DbTransaction Transaction
		{
			get
			{
				return _transaction;
			}
		}

		/// <summary>
		/// Gets the Batch databse instance.
		/// </summary>
		public Database BatchDatabase
		{
			get {return this._database;}
		}


        /// <summary>
        /// See <see cref="BatchExecutionContext.DisposeTransaction"/>.
        /// </summary>
        /// <returns>void</returns>
		public void DisposeTransaction()
		{
			
				if (_transaction != null)
				{
					_transaction.Dispose();
					_transaction = null;
				}
		}

        /// <summary>
        /// See <see cref="BatchExecutionContext.BeginTransaction"/>.
        /// </summary>
        /// <returns>void</returns>
		public void BeginTransaction()
		{
			BeginTransactionUnsynchronized();
		}

        /// <summary>BeginTransactionUnsynchronized</summary>
        /// <returns>void</returns>
        private void BeginTransactionUnsynchronized()
		{
			if (!System.EnterpriseServices.ContextUtil.IsInTransaction)
			{                
				IDbConnection connection = _database.CreateConnection();
				connection.Open();

				_transaction = 
					(DbTransaction) connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
			}
		}

        /// <summary>Save</summary>
        /// <returns>void</returns>
		public void Save()
		{
			if (_archived)
			{                
                BatchInstrumentationProvider.Instance.FireBatchArchitectureFailedEvent(CONTEXT_ALREADY_ARCHIVED, null);
				throw new ACABatchException(CONTEXT_ALREADY_ARCHIVED);
			}

			// The 'Finished' state indicates that the transaction was rolled back
			if (TransactionState == DbTransactionState.Finished)
			{
				return;
			}

			_commitHandle = null;
			
			try
			{
				if (TransactionState == DbTransactionState.Empty)
				{
					BeginTransactionUnsynchronized();
				}

				
				_commitHandle = DefaultBatchManager.RequestQueue.CreateCommitStatusHandle(_database, _requestKey, Transaction);

				if (!ContextIsLocked)
				{
					// Save all of the children first,
					// then update the status
					SaveChildren();
				}

				// update the status
				_toPause = CommitHandle.CommitStatus(_batchStatus);

				_commitHandle.EndCommit(true);


			}
			catch
			{
				if (_commitHandle != null)
				{
					_commitHandle.EndCommit(false);
				}
				throw;
			}
			finally
			{
				DisposeTransaction();
				_commitHandle = null;
			}
			if (_batchStatus == BatchProcessStatusCode.Succeeded ||
				_batchStatus == BatchProcessStatusCode.Failed ||
				_batchStatus == BatchProcessStatusCode.FailedCanceled ||
				_batchStatus == BatchProcessStatusCode.FailedTimedOut)
			{
				DefaultBatchManager.RequestQueue.Delete(_database, _requestKey);
				_archived = true;
			}
		}

        /// <summary>SaveChildren</summary>
        /// <returns>void</returns>
        private void SaveChildren()
		{
			this.parmCollection.SaveParameters(_commitHandle,false);
			this.jobCollection.SaveJobs();
		}

		/// <summary>
		/// 
		/// </summary>
		internal RequestCommitHandle CommitHandle
		{
			get
			{
				return _commitHandle;
			}
		}

        /// <summary>
        /// Summary of InitializeImpl.
        /// </summary>
        /// <returns>void</returns>
		protected void Initialize()
		{
			_instanceKey = Guid.NewGuid();
			_requestKey = _instanceKey;
			_description = "";
			_restartBehavior = BatchRestartBehavior.SkipFailedJobAndContinue;
			_exePriorityLevel = ExecutionPriorityLevel.Normal;
			_queuePriorityLevel = QueuePriorityLevel.Normal;
			_maxConcurrent = 1;
			_configFilePath = "";
			// default to one day
			_relativeExpDate = new TimeSpan(1, 0, 0, 0, 0);
			_isolationLevel = ExecutionIsolationLevel.Process;
			_childRequestCount = -1;
		}
		#endregion


		#region Custom Properties
		/// <summary>
		/// Batch name plus the queued date.
		/// </summary>
		public string RequestName
		{
			get
			{
				return _batchName + "-" + _queuedDate;
			}
		}

		/// <summary>
		/// See <see cref="IBatchData.DisplayName"/>.
		/// </summary>
		public string DisplayName
		{
			get
			{
				return this.RequestName;
			}
		}

		/// <summary>
		/// Implements the <see cref="IBatchDBData.Key"/> property.  Gets or sets the key for the data.
		/// </summary>
		/// <value>A <see cref="System.Guid"/> instance as the key of the data object.</value>
		public Guid Key
		{
			get
			{
				return _requestKey;
			}
		}

		/// <summary>
		/// See <see cref="IBatchDBData.Children"/>.
		/// </summary>
		public IList Children
		{
			get { return this.alChildren;}
		}

		/// <summary>
		/// 
		/// </summary>
		public Guid ParentRequestKey
		{
			get
			{
				return _parentRequestKey;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public int ChildRequestCount
		{
			get 
			{
				IDataReader reader = null;
				
				try
				{
					if (_childRequestCount != -1)
					{
						return this._childRequestCount;
					}
					reader = 
						DefaultBatchManager.RequestQueue.ListByParent(_database, _requestKey);
					int i = 0;
					while (reader.Read())
					{
						i++;
					}
					_childRequestCount = i;
					return i;
				}
				finally
				{
					if (reader != null)
					{
						reader.Close();
					}
				}
			}
		}

		/// <summary>
		/// Gets the parameters for the request.
		/// </summary>
		/// <value>A <see cref="ParameterExecutionContextCollection"/> containing the 
		/// <see cref="Avanade.ACA.Batch.Configuration.ParameterData"/>.
		/// </value>
		public ParameterExecutionContextCollection RequestParameters
		{
			get{return this.parmCollection;}
		}

		
		/// <summary>
		/// Gets the type name of the Batch for the request.
		/// </summary>
		public string BatchTypeName
		{
			get
			{
				return _batchTypeName;
			}
//			set
//			{
//				_batchTypeName = value;
//			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string BatchName
		{
			get
			{
				return _batchName;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public DateTime QueuedDate
		{
			get 
			{
				return _queuedDate;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public DateTime StartDate
		{
			get 
			{
				return _startDate;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		public DateTime LastUpdateDate
		{
			get 
			{
				return _lastUpdateDate;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string BatchClientName
		{
			get
			{
				return _batchClientName;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string BatchServerName
		{
			get
			{
				return _batchServerName;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public bool ToPause
		{
			get
			{
				return _toPause;
			}
			set
			{
				_toPause = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public bool Archived
		{
			get
			{
				return _archived;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public Guid LastExecutionKey
		{
			get
			{
				return _lastExecutionKey;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public Guid NextExecutionKey
		{
			get
			{
				return _nextExecutionKey;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public bool IsARestart
		{
			get
			{
				return (_lastExecutionKey != Guid.Empty);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public bool HasBeenRestarted
		{
			get
			{
				return (_nextExecutionKey != Guid.Empty);
			}
		}


		/// <summary>
		/// Gets or sets a value indicating
		/// whether multiple executions of this 
		/// batch can
		/// overlap each other.
		/// </summary>
		/// <value><b>True</b> if multiple
		/// executions are allowed, otherwise <b>false</b>.
		/// The default is <b>false</b>.</value>
		public int MaxConcurrentExecution
		{
			get
			{
				return _maxConcurrent;
			}
			set
			{
				_maxConcurrent = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public BatchProcessStatusCode BatchStatus
		{
			get
			{
				return _batchStatus;
			}
			set
			{
				_batchStatus = value;
			}
		}

        /// <summary>Pause</summary>
        /// <param name="checkStatusInterval">System.TimeSpan</param>
        /// <returns>void</returns>
		public void Pause(TimeSpan checkStatusInterval)
		{
			// update the status code to Paused
			BatchProcessStatusCode revert = BatchStatus;
			BatchStatus = BatchProcessStatusCode.Paused;
			
			Save();

			// loop until the status has changed
			while (true)
			{

				DefaultBatchManager.RequestQueue.CheckStatus
					(
						_database, 
						Key, 
						out _batchStatus, 
						out _toPause
					);

				// keep polling until the ToPause
				// flag has been set back to false
				if (!_toPause)
				{
					BatchStatus = revert;
					Save();
					break;
				}

				// wait before checking again
				Thread.Sleep(checkStatusInterval);
			}
		}

		/// <summary>
		/// The job data for the request.
		/// </summary>
		/// <value>An array of <see cref="JobExecutionContextData"/>.</value>
		public JobExecutionContextData[] JobExecutionContexts
		{
			get
			{

				JobExecutionContextData[] jobs;
				
				if (this.jobCollection.Count >= 0)
				{
					jobs = new JobExecutionContextData[jobCollection.Count];
					for (int i = 0; i < jobCollection.Count; i++)
					{
						JobExecutionContextData data = jobCollection[i];
						jobs[i] = data;
					}
				}
				else
				{
					jobs = new JobExecutionContextData[0];
				}
				return jobs;
			}
		}
		
		/// <summary>
		/// 
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
		/// Gets or sets a value indicating whether 
		/// the Batch can intelligently resart after
		/// a failed execution.
		/// </summary>
		/// <value><b>Intelligent</b> if the 
		/// batch can restart itself at the point of
		/// failure, otherwise <b>Full</b>. <b>Intelligent</b>
		/// is the default.</value>
		public BatchRestartBehavior RestartBehavior
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
		/// Gets or sets a value indicating the execution priority level of the
		/// Batch.
		/// </summary>
		/// <value>The priority levels are <b>Lowest</b>, <b>BelowNormal</b>, <b>Normal</b>,
		/// <b>AboveNormal</b>, <b>High</b>, <b>Highest</b>. <b>Normal</b>
		/// is the default.</value>
		
		public ExecutionPriorityLevel ExecutionPriorityLevel
		{
			get
			{
				return _exePriorityLevel;
			}
			set
			{
				_exePriorityLevel = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating the execution priority level of the
		/// Batch.
		/// </summary>
		/// <value>The priority levels are <b>Lowest</b>, <b>BelowNormal</b>, <b>Normal</b>,
		/// <b>AboveNormal</b>, <b>High</b>, <b>Highest</b>. <b>Normal</b>
		/// is the default.</value>
		[Description("A value indicating the execution priority level of the batch.")]
		public QueuePriorityLevel QueuePriorityLevel
		{
			get
			{
				return _queuePriorityLevel;
			}
			set
			{
				_queuePriorityLevel = value;
			}
		}

		/// <summary>
		/// Batch Execution Isolation
		/// </summary>
		public ExecutionIsolationLevel IsolationLevel
		{
			get
			{
				return _isolationLevel;
			}
		}

		/// <summary>
		/// The path to the main configuration file when the batch is running under AppDomain
		/// isolation or Process isolation.
		/// </summary>

		public string ConfigurationFilePath
		{
			get
			{
				return _configFilePath;
			}
			set
			{
				_configFilePath = value;
			}
		}

		/// <summary>
		/// The expiration date of the batch relative to the time it gets put into the request queue.
		/// </summary>
		public TimeSpan	RelativeExpirationDate
		{
			get
			{
				return _relativeExpDate;
			}
			set
			{
				_relativeExpDate = value;
			}
		}

		/// <summary>
		/// The expiration date of the batch relative to the time it gets put into the request queue.
		/// </summary>
		
		public DateTime	AbsoluteExpirationDate
		{
			get
			{
				return _absoluteExpirationDate;
			}
			set
			{
				_absoluteExpirationDate = value;
			}
		}

		/// <summary>
		/// The symbolic destination for the batch to be executed.
		/// </summary>
		
		public string Destination
		{
			get
			{
				return _destination;
			}
			set
			{
				_destination = value;
			}
		}

		
		/// <summary>
		/// Gets or sets the BatchType as a string.
		/// </summary>
		public string TypeName
		{
			get
			{
				return _batchTypeName;
			}
			set
			{
				_batchTypeName = value;
			}
		}

		#endregion
		

		/// <summary>
		/// 
		/// </summary>
		public int ParameterCount
		{
			get
			{
				return parmCollection.Count;
			}
		}



        /// <summary>
        /// For initializing when all the properties are already read from database.
        /// </summary>
        /// <param name="requestKey">System.Guid</param>
        /// <param name="batchName">string</param>
        /// <param name="description">string</param>
        /// <param name="restart">Avanade.ACA.Batch.BatchRestartBehavior</param>
        /// <param name="configFilePath">string</param>
        /// <param name="isolationLevel">Avanade.ACA.Batch.ExecutionIsolationLevel</param>
        /// <param name="exePriorityLevel">Avanade.ACA.Batch.ExecutionPriorityLevel</param>
        /// <param name="queuePriorityLevel">Avanade.ACA.Batch.QueuePriorityLevel</param>
        /// <param name="maxConcurrent">int</param>
        /// <param name="destination">string</param>
        /// <param name="batchTypeName">string</param>
        /// <param name="relativeExpirationDateInMilli">long</param>
        /// <param name="absoluteExpirationDate">System.DateTime</param>
        /// <param name="batchServerName">string</param>
        /// <param name="batchClientName">string</param>
        /// <param name="queuedDate">System.DateTime</param>
        /// <param name="startDate">System.DateTime</param>
        /// <param name="lastUpdateDate">System.DateTime</param>
        /// <param name="batchStatus">Avanade.ACA.Batch.BatchProcessStatusCode</param>
        /// <param name="originalBatchKey">System.Guid</param>
        /// <param name="lastExecutionKey">System.Guid</param>
        /// <param name="toPause">bool</param>
        /// <param name="parentRequestKey">System.Guid</param>
        /// <param name="nextExecutionKey">System.Guid</param>
        /// <returns>void</returns>
		public void LoadContext(
			Guid					requestKey,
			string					batchName,
			string					description,
			BatchRestartBehavior	restart,
			string					configFilePath,
			ExecutionIsolationLevel	isolationLevel,
			ExecutionPriorityLevel	exePriorityLevel,
			QueuePriorityLevel		queuePriorityLevel,
			int						maxConcurrent,
			string					destination,
			string					batchTypeName,
			long					relativeExpirationDateInMilli,
			DateTime				absoluteExpirationDate,
			string					batchServerName,
			string					batchClientName,
			DateTime				queuedDate,
			DateTime				startDate,
			DateTime				lastUpdateDate,
			BatchProcessStatusCode	batchStatus,
			Guid					originalBatchKey,
			Guid					lastExecutionKey,
			bool					toPause,
			Guid					parentRequestKey,
			Guid					nextExecutionKey)
		{
			_requestKey = requestKey;
			_batchName = batchName;
			_description = description;
			_configFilePath = configFilePath;
			_isolationLevel = isolationLevel;
			_exePriorityLevel = exePriorityLevel;
			_queuePriorityLevel = queuePriorityLevel;
			_maxConcurrent = maxConcurrent;
			_destination = destination;
			_batchTypeName = batchTypeName;
			_absoluteExpirationDate = absoluteExpirationDate;
			_batchServerName = batchServerName;
			_batchClientName = batchClientName;
			_queuedDate = queuedDate;
			_startDate = startDate;
			_lastUpdateDate = lastUpdateDate;
			_batchStatus = batchStatus;
			_originalBatchKey = originalBatchKey;
			_lastExecutionKey = lastExecutionKey;
			_nextExecutionKey = nextExecutionKey;
			_toPause = toPause;
			_parentRequestKey = parentRequestKey;

			_relativeExpDate = TimeSpan.FromMilliseconds(relativeExpirationDateInMilli);
			//DisplayNameUnsynchronized = _requestKey.ToString();
			_restartBehavior = (BatchRestartBehavior)Convert.ToByte(restart);
			// set it to unknown
			_childRequestCount = -1;
		}
	}
}
