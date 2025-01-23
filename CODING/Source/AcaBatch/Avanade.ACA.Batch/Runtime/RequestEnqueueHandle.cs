// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Data;
using System.Collections;
using System.Collections.Specialized;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Avanade.ACA.Batch.Instrumentation;
using System.Data.Common;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// The RequestEnqueueHandle is the objec handle responsible for enqueuing a Batch Request.
	/// The handle wraps the transaction object and database connection object necessary for 
	/// performing multiple database operations needed to putting a Request in the ACA.NET 
	/// Batch Queue.
	/// </summary>
	/// <example>
	/// This following pseudo code shows how the RequestEnqueueHandle should be used.
	/// <code>
	/// RequestEnqueueHandle enqueueHandle = CreateRequestEnqueueHandle( Database database, 
	///		Guid requestKey, 
	///		Guid batchKey,
	///		string batchName,
	///		Guid parentRequestKey);
	///	
	///	try
	///	{	
	///		enqueueHandle.BeginEnqueue( null, // use the pre-defined queuePriorityLevel of the batch
	///			absoluteExpDate, 
	///			null,	// use the pre-defined destination of the batch
	///			"clientName" );	
	///		enqueueHandle.Queue
	///		
	///		// Adding parameters to the new requests
	///		// parameterList is some sort of a list containing a class that has the
	///		// properties of the parameter data
	///		foreach (parm in parameterList)
	///		{
	///			AddRequestParameter(
	///				parm.Name, 
	///				parm.TypeName,
	///				parm.Value,
	///				ref parm.TypeKey);
	///		}
	///		enqueueHandle.EndEnqueue( true );
	///	}
	///	catch
	///	{
	///		enqueueHandle.EndEnqueue( false );
	///	}
	/// </code>
	/// </example>
	public class RequestEnqueueHandle
	{
		private IDbConnection _connection;
        private DbTransaction  _transaction;
		private Database _database;
		private Guid _requestKey;
		private Guid _batchKey;
		private string _batchName;
		private Guid _parentRequestKey;

        /// <summary>
        /// The constructor for RequestEnqueueHandle.  Users do not need to call this
        /// directly.  Instead, they should call 
        /// <see cref="DefaultBatchManager.RequestQueue.CreateRequestEnqueueHandle"/> to get a handle.
        /// </summary>
        /// <param name="database">An ACA.NET Database object.</param>
        /// <param name="requestKey">A Guid used as the key for the new request.</param>
        /// <param name="batchKey">A Guid representing the key of the batch 
        /// to be put in the queue.</param>
        /// <param name="batchName">The name of the batch; only needed when the batchKey
        /// is not available to the caller.</param>
        /// <param name="parentRequestKey">The key of the parent request if the request is
        /// being put in queue as a result of executing another request.  This is used
        /// for load-balancing request.  Otherwise, use Guid.Empty.</param>
		public RequestEnqueueHandle(Database database, 
			Guid requestKey,
			Guid batchKey,
			string batchName,
			Guid parentRequestKey)
		{
			_connection = null;
			_transaction = null;
			_database = database;
			_requestKey = requestKey;
			_batchKey = batchKey;
			_batchName = batchName;
			_parentRequestKey = parentRequestKey;
		}

        /// <summary>
        /// Start enqueing a Request.
        /// </summary>
        /// <param name="queuePriorityLevel">The queuing priority of the Request.  Use
        /// DBNull.Value if not wishing to override the priority defined by the Batch.</param>
        /// <param name="absoluteExpDate">The expiration date for the Request in the Queue.</param>
        /// <param name="BatchDestName">The Destination of the Batch.  Use DBNull.Value if
        /// not wishing to override the pre-defined Destination of the Batch.</param>
        /// <param name="BatchClientName">string</param>
        /// <returns>void</returns>
		public void BeginEnqueue(object queuePriorityLevel,
			DateTime absoluteExpDate, 
			object BatchDestName,
			string BatchClientName)
		{

			try
			{
				DbCommand cw;
				const byte NULL_SUBSTITUTE_VALUE = 127;

				object priorityObject = queuePriorityLevel == DBNull.Value ? NULL_SUBSTITUTE_VALUE : queuePriorityLevel;
			
				if (_batchKey != Guid.Empty)
				{
					cw = _database.GetStoredProcCommand( 
						BatchDbConstants.SP.Q_ENQUEUE_W_KEY);

					_database.AddInParameter(cw, BatchDbConstants.Parm.Q_KEY, DbType.Guid, _requestKey );			
					_database.AddInParameter(cw, BatchDbConstants.Parm.Q_ORIG_BATCH_KEY , DbType.Guid, _batchKey );
					_database.AddInParameter(cw, BatchDbConstants.Parm.BATCH_QUEUE_PRIORITY, DbType.Byte, priorityObject );
					_database.AddInParameter(cw, BatchDbConstants.Parm.Q_ABS_EXP_DATE, DbType.DateTime, absoluteExpDate );
					_database.AddInParameter(cw, BatchDbConstants.Parm.DESTINATION_NAME, DbType.String, BatchDestName );
					_database.AddInParameter(cw, BatchDbConstants.Parm.Q_BATCH_CLIENT_NAME, DbType.String, BatchClientName );
				}
				else
				{
					cw = _database.GetStoredProcCommand( 
						BatchDbConstants.SP.Q_ENQUEUE_W_NAME);

					_database.AddInParameter(cw, BatchDbConstants.Parm.Q_KEY, DbType.Guid, _requestKey );			
					_database.AddInParameter(cw, BatchDbConstants.Parm.BATCH_NAME, DbType.String, _batchName );
					_database.AddInParameter(cw, BatchDbConstants.Parm.BATCH_QUEUE_PRIORITY, DbType.Byte, priorityObject );
					_database.AddInParameter(cw, BatchDbConstants.Parm.Q_ABS_EXP_DATE, DbType.DateTime, absoluteExpDate );
					_database.AddInParameter(cw, BatchDbConstants.Parm.DESTINATION_NAME, DbType.String, BatchDestName );
					_database.AddInParameter(cw, BatchDbConstants.Parm.Q_BATCH_CLIENT_NAME, DbType.String, BatchClientName );
					_database.AddOutParameter(cw,BatchDbConstants.Parm.Q_ORIG_BATCH_KEY, DbType.Guid, 16);
				}

				DefaultBatchManager.OpenTransaction( cw,
					_database,
					ref _transaction,
					ref _connection,
					IsolationLevel.ReadCommitted );

				// first, makes a request with the batch info.
				DefaultBatchManager.ExecuteCommand(_database, cw, _transaction);
				if (_batchKey == Guid.Empty)
				{
                    object key = _database.GetParameterValue(cw, BatchDbConstants.Parm.Q_ORIG_BATCH_KEY);
					if (key == DBNull.Value)
					{
						throw new ACABatchException(
							string.Format("Could not find batch definition '{0}'.",
							_batchName));
					}
					_batchKey = (Guid)key;
				}

				// if the parentRequestKey is not empty, set the parentRequestKey for the Request
				if (_parentRequestKey != Guid.Empty)
				{
					DbCommand cw2 = _database.GetStoredProcCommand( 
						BatchDbConstants.SP.Q_SET_PARENT_KEY);
					JoinTransaction( cw2 );
					_database.AddInParameter(cw2, BatchDbConstants.Parm.Q_KEY, DbType.Guid, _requestKey );			
					_database.AddInParameter(cw2, BatchDbConstants.Parm.Q_PARENT_KEY, DbType.Guid, _parentRequestKey );
					DefaultBatchManager.ExecuteCommand(_database, cw2, cw2.Transaction);
				}

				// copy the batch parameters to batch Execution context original parameters category
				DefaultBatchManager.Parameter.Copy(_database, 
					ref _connection, 
					ref _transaction, 
					_batchKey, 
					_requestKey,
					ParameterCategory.BatchDefinition, 
					ParameterCategory.RequestOriginal);

				// then, copy the jobs to job execution context 
				QueueJobs();
			}
			catch (Exception e)
			{                
                BatchInstrumentationProvider.Instance.FireBatchArchitectureFailedEvent("Failed to enqueue batch '" + _batchName + "'.", e);
				throw;
			}
		}

        /// <summary>
        /// Private methods to enqueue jobs for a request.
        /// </summary>
        /// <returns>void</returns>
		private void QueueJobs()
		{
			// first, list the jobs for the request.
			DbCommand cw = _database.GetStoredProcCommand( 
				BatchDbConstants.SP.BATCH_JOB_LIST);	
			
			JoinTransaction( cw );
			_database.AddInParameter(cw, BatchDbConstants.Parm.BATCH_KEY, DbType.Guid, _batchKey );

			IDataReader reader = null;
			ArrayList commands = new ArrayList();

			try
			{			
				reader = DefaultBatchManager.ExecuteReader(_database, cw, _transaction);

				while (reader.Read())
				{	
					// The columns are: BatchJobKey, TypeKey, BatchKey, BatchJobSequence,
					// BatchJobName, BatchJobDesc,	JobSmartRestart, BatchJobCmitFreq, TypeName,
					//  TypeClass.
					int seqNo = reader.GetInt16(3);
					Guid jobKey = reader.GetGuid(0);
					Guid typeKey = reader.GetGuid(1);
					Guid batchJobExecKey = Guid.NewGuid();
					string jobName = reader.GetString(4);
					string jobDesc = String.Empty;
					if (!reader.IsDBNull(5))
					{
						jobDesc = reader.GetString(5);
					}
					byte smartRestart = reader.GetByte(6);
					int cmitFreq = reader.GetInt32(7);
					string typeClass = reader.GetString(9);

					DbCommand cwWrite = _database.GetStoredProcCommand( 
						BatchDbConstants.SP.Q_ENQUEUE_JOB);

					if (cwWrite.Parameters.Count == 0)
					{
						_database.AddInParameter(cwWrite, BatchDbConstants.Parm.Q_JOB_KEY, DbType.Guid, batchJobExecKey );
                        _database.AddInParameter(cwWrite, BatchDbConstants.Parm.Q_KEY, DbType.Guid, _requestKey);
                        _database.AddInParameter(cwWrite, BatchDbConstants.Parm.JOB_NAME, DbType.String, jobName);
                        _database.AddInParameter(cwWrite, BatchDbConstants.Parm.JOB_DESC, DbType.String, jobDesc);
                        _database.AddInParameter(cwWrite, BatchDbConstants.Parm.Q_JOB_SEQ, DbType.Int32, seqNo);
                        _database.AddInParameter(cwWrite, BatchDbConstants.Parm.JOB_RESTART, DbType.Byte, smartRestart);
                        _database.AddInParameter(cwWrite, BatchDbConstants.Parm.Q_JOB_RESATRT_COUNT, DbType.Int32, 0);
                        _database.AddInParameter(cwWrite, BatchDbConstants.Parm.JOB_COMMIT_FREQ, DbType.Int32, cmitFreq);
                        _database.AddInParameter(cwWrite, BatchDbConstants.Parm.Q_JOB_TYPE_CLASS, DbType.String, typeClass);
                        _database.AddInParameter(cwWrite, BatchDbConstants.Parm.Q_JOB_ORIG_JOB_KEY, DbType.Guid, jobKey);
                        _database.AddInParameter(cwWrite, BatchDbConstants.Parm.Q_JOB_LAST_EXE_STATUS, DbType.Byte, 20);
					}
					else
					{
						_database.SetParameterValue(cwWrite, BatchDbConstants.Parm.Q_JOB_KEY, batchJobExecKey );
						_database.SetParameterValue(cwWrite, BatchDbConstants.Parm.Q_KEY, _requestKey );
						_database.SetParameterValue(cwWrite, BatchDbConstants.Parm.JOB_NAME, jobName );
						_database.SetParameterValue(cwWrite, BatchDbConstants.Parm.JOB_DESC, jobDesc );
						_database.SetParameterValue(cwWrite, BatchDbConstants.Parm.Q_JOB_SEQ, seqNo );
						_database.SetParameterValue(cwWrite, BatchDbConstants.Parm.JOB_RESTART, smartRestart );
						_database.SetParameterValue(cwWrite, BatchDbConstants.Parm.Q_JOB_RESATRT_COUNT, 0 );
						_database.SetParameterValue(cwWrite, BatchDbConstants.Parm.JOB_COMMIT_FREQ, cmitFreq );
						_database.SetParameterValue(cwWrite, BatchDbConstants.Parm.Q_JOB_TYPE_CLASS, typeClass );
						_database.SetParameterValue(cwWrite, BatchDbConstants.Parm.Q_JOB_ORIG_JOB_KEY, jobKey );
						_database.SetParameterValue(cwWrite, BatchDbConstants.Parm.Q_JOB_LAST_EXE_STATUS, 20 );
					}
					// save the command for later execution.
					commands.Add(cwWrite);
				}
			}
			finally
			{
				if( reader != null )
				{
					reader.Close();
				}
			}
			// going through the saved commands
			foreach (DbCommand cwWrite in commands)
			{
				JoinTransaction( cwWrite );

				Guid jobKey = (Guid)
					_database.GetParameterValue(cwWrite,BatchDbConstants.Parm.Q_JOB_ORIG_JOB_KEY);

				Guid batchJobExecKey = (Guid)
					_database.GetParameterValue(cwWrite,BatchDbConstants.Parm.Q_JOB_KEY);

				// add the job execution
				DefaultBatchManager.ExecuteCommand(_database, cwWrite, _transaction);

				// copy the jobs parameters to jobExec parameters
				// we need to make copy to both the original and current categories.
				ListDictionary categoryMappings = new ListDictionary();
				categoryMappings.Add(ParameterCategory.JobDefinition,
					new ParameterCategory[] { 
								ParameterCategory.JobExecutionContextOriginal,
								ParameterCategory.JobExecutionContextCurrent});
				DefaultBatchManager.RequestQueue.CopyParameters(
					_database, 
					ref _connection,
					ref _transaction,
					jobKey, 
					batchJobExecKey,
					categoryMappings);

			}
		}



        /// <summary>
        /// Adds a Request Parameter.  If the Request Parameter have the same name as the Batch
        /// Parameter, it will override the Batch Parameter.
        /// </summary>
        /// <param name="parmName">The name of the parameter provided by the caller.</param>
        /// <param name="parmTypeName">The name of the parameter type.  It is optional and is only 
        /// needed when parmTypeKey equal to Guid.Empty.</param>
        /// <param name="parmValue">The serialized value of the parameter.</param>
        /// <param name="parmTypeKey">By ref.  The key of the parameter type.  If the input value
        /// Guid.Empty, coming out of the call this will have the key to the Parameter Type for
        /// the Parameter object.</param>
        /// <remarks>The Request object will automatically inherits all the Parameters from its
        /// Batch definition.
        /// </remarks>
        /// <returns>The key of the new Request Parameter.  If a request Parameter already exists
        /// with the same name (as could happen when the Request get the Parameters from the Batch),
        /// the original key will be used instead and returned.</returns>
		public Guid AddRequestParameter(
			string parmName, 
			string parmTypeName,
			string parmValue,
			ref Guid parmTypeKey)
		{
			try
			{
				Guid requestParmKey = DefaultBatchManager.Parameter.SaveByName(
					_database,
					ref _connection,
					ref _transaction,
					parmName, 
					parmTypeName,
					ref parmTypeKey,
					_requestKey,
					parmValue, 
					ParameterCategory.RequestOriginal );

				return requestParmKey;
			}
			catch (Exception e)
			{                
                BatchInstrumentationProvider.Instance.FireBatchArchitectureFailedEvent(string.Format("Failed to added request parameter '{0}' to request '{1}'.", parmName, _batchName), e);
				throw;
			}
		}

        /// <summary>
        /// Finishes enqueue process.
        /// </summary>
        /// <param name="succeeded">A boolean indicating whether the entire queuing process
        /// is successful or not.</param>
        /// <remarks>If 'true' is passed in, the results will be committed to ACA.NET Batch
        /// Database.  Otherwise the result will be aborted.  The enqueuing is done as a
        /// transaction as a whole.</remarks>
        /// <returns>void</returns>
		public void EndEnqueue(bool succeeded)
		{
			if (succeeded)
			{
				try
				{
					// make copies of the request parameter from request original to the
					// other two categories.
					ListDictionary categoryMappings = new ListDictionary();
					categoryMappings.Add(ParameterCategory.RequestOriginal,
						new ParameterCategory[] { 
													ParameterCategory.BatchExecutionContextCurrent,
													ParameterCategory.BatchExecutionContextEndOfSucceededJob});

					DefaultBatchManager.RequestQueue.CopyParameters(
						_database, 
						ref _connection,
						ref _transaction,
						_requestKey, 
						_requestKey,
						categoryMappings);
                    					
                    BatchInstrumentationProvider.Instance.FireRequestEnqueuedEvent(
                            _requestKey,
                            _batchName,"Enqueued");

				}
				catch (Exception e)
				{
                    BatchInstrumentationProvider.Instance.FireBatchArchitectureFailedEvent("Failed to enqueue batch '" + _batchName + "'.", e);
					throw;
				}
			}
			DefaultBatchManager.EndTransaction( _transaction,
												_connection,
												succeeded );

		}

        /// <summary>
        /// A wrapper around the DefaultBatchManager.JoinTransaction
        /// </summary>
        /// <param name="cw">See </param>
        /// <returns>void</returns>
		private void JoinTransaction(DbCommand cw)
		{
			DefaultBatchManager.JoinTransaction( cw,
				_database,
				ref _transaction,
				ref _connection );
		}
	}
}

