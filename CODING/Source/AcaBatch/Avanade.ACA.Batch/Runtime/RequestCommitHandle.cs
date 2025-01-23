// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Data;
using System.Collections.Specialized;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Avanade.ACA.Batch.Instrumentation;
using System.Data.Common;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// RequestCommitHandle is responsible for commit the Batch status, Batch Execution
	/// Context (Request Parameters), Job status, and Job Execution Context (Job Execution
	/// Parameters) as a transaction package.  Callers must first obtain this handle by
	/// Invoking DefaultBatchManager.RequestQueue.CreateRequestCommitHandle().  
	/// For how to obtain a handle, see 
	/// <see cref="DefaultBatchManager.RequestQueue.CreateCommitStatusHandle"/>.
	/// </summary>    
	public class RequestCommitHandle
	{
		private IDbConnection			_connection;
		private DbTransaction			_transaction;
		private Database				_database;
		private Guid					_requestKey;
		private StatusCode				_jobStatusCode;
		private BatchProcessStatusCode	_requestStatusCode;
		private int						_jobCommitCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestCommitHandle"/>
        /// class and internally starts a database transaction for executing its commands.
        /// </summary>
        /// <param name="database">The database instance of ACA.NET Batch Database.</param>
        /// <param name="requestKey">The key for the request whose status and jobs are
        /// to be committed.</param>
		public RequestCommitHandle(Database database, Guid requestKey) 
			: this (database, requestKey, null)
		{
			_jobCommitCount = 0;
		}

        /// <summary>
        /// Initializes a new instance of the </summary>
        /// <param name="database">The database instance of ACA.NET Batch Database.</param>
        /// <param name="requestKey">The key for the request whose status and jobs are
        /// to be committed.</param>
        /// <param name="transaction">The transaction that all of the commit
        /// handle's commands will use.</param>
		public RequestCommitHandle
			(
			Database database, 
			Guid requestKey, 
			DbTransaction transaction
			) 
		{
			_connection		= null;
			_transaction	= transaction;
			_database		= database;
			_requestKey		= requestKey;
			_jobStatusCode	= StatusCode.New;
			_requestStatusCode = BatchProcessStatusCode.Unknown;
			_jobCommitCount = 0;


			if (_transaction != null)
			{
				_connection	= transaction.Connection;
			}			
		}

        /// <summary>
        /// Finishes committing the series of Request and Job status and context data.
        /// Must be called for each RequestCommitHandle at the end of status commit.
        /// </summary>
        /// <param name="succeeded">True for committing the results; false for rolling 
        /// them back.</param>
        /// <remarks>EndCommit also makes a backup copy of the Request Parameters, if
        /// one if the Job Status committed is "Success".  If the copying failed,
        /// the entire results get rolled back and false gets returned.</remarks>
        /// <returns>True if the transcation committed successfully.  Otherwise, false.</returns>
		public bool EndCommit(bool succeeded)
		{
			if (succeeded)
			{
				// if the status code is success, copy the batch current context to its last
				// successful job context. Don't copy the context if the request status
				// code is succeeded, because it will cause archived parameters to
				// be copied back from the history into the parameter table. See Defect
				// 12299 for details. And there's no need to copy parameters once
				// the batch has succeeded. The execution sequence will guarantee that
				// they've already been saved correctly in a previous transaction.
				if (_requestStatusCode != BatchProcessStatusCode.Succeeded
					&& _jobStatusCode == StatusCode.Success)
				{
					ListDictionary categoryMappings = new ListDictionary();
					categoryMappings.Add(ParameterCategory.BatchExecutionContextCurrent,
						new ParameterCategory[] { 
									ParameterCategory.BatchExecutionContextEndOfSucceededJob});

					try
					{
						DefaultBatchManager.RequestQueue.CopyParameters(
							_database, 
							ref _connection,
							ref _transaction,
							_requestKey, 
							_requestKey,
							categoryMappings);
					}
					catch (Exception)
					{
						DefaultBatchManager.EndTransaction( _transaction,
							_connection,
							false );
						return false;
					}
				}
			}
			DefaultBatchManager.EndTransaction( _transaction,
				_connection,
				succeeded );

			if (succeeded)
			{
				//BatchRequestCommittedEvent.Fire();
				//BatchJobCommittedEvent.Fire((long)_jobCommitCount);
                BatchInstrumentationProvider.Instance.FireRequestCommitEvent();
                BatchInstrumentationProvider.Instance.FireJobCommitEvent(_jobCommitCount);
			}
			return true;
		}

        /// <summary>
        /// Add the Request status to the results being committed.
        /// </summary>
        /// <param name="statusCode">The request status code.</param>
        /// <remarks>This method does not automatically move the request from the active
        /// batch queue to the archive.  The caller must call the 
        /// <seealso cref="DefaultBatchManager.RequestQueue.Delete"/> method to archive
        /// the request if the status of the request is either 
        /// <see href="BatchProcessStatusCode.Succeeded"/>, <see href="BatchProcessStatusCode.Failed"/>,
        /// <see href="BatchProcessStatusCode.FailedCanceled"/>, or
        ///  <see href="BatchProcessStatusCode.FailedTimedOut"/>.
        /// </remarks>
        /// <returns>A boolean for whether the request needs to be paused</returns>
		public bool CommitStatus( BatchProcessStatusCode statusCode )
		{
			try
			{
				_requestStatusCode = statusCode;
				DbCommand cw = _database.GetStoredProcCommand(BatchDbConstants.SP.Q_UPDATE_STATUS);

				JoinTransaction( cw );

				_database.AddInParameter(cw, BatchDbConstants.Parm.Q_KEY, DbType.Guid, _requestKey );
				_database.AddInParameter(cw, BatchDbConstants.Parm.Q_STATUS_CODE, DbType.Byte, statusCode );
				_database.AddOutParameter(cw, BatchDbConstants.Parm.Q_TO_PAUSE, DbType.Boolean, 1 );

				DefaultBatchManager.ExecuteCommand(_database, cw, _transaction);

				object obj = _database.GetParameterValue(cw, BatchDbConstants.Parm.Q_TO_PAUSE );
				bool commitStatus = Convert.ToBoolean(obj);

				return commitStatus;
			}
			catch (InvalidCastException)
			{
				// because we know that if this exception happens, the request is being removed from the queue
				throw new BatchRequestNotFoundException();
			}
			catch (Exception ex)
			{
				throw new BatchDatabaseFatalException( "Failed to update Batch Status.", ex );
			}
		}

        /// <summary>
        /// Adds the Request Parameters to the set of results being committed.
        /// </summary>
        /// <param name="parmKey">The key of the parameter object.</param>
        /// <param name="requestKey">The key of the request.</param>
        /// <param name="parmName">The name of the request parameter.</param>
        /// <param name="parmTypeKey">The key of the parameter type.  If not known,
        /// use Guid.Empty.</param>
        /// <param name="parmTypeName">The name of the parameter type.  Only required
        /// if the parmTypeKey is Guid.Empty.</param>
        /// <param name="parmValue">The serialized value of the parameter.</param>
        /// <returns>The key of the parameter.</returns>
		public Guid CommitBatchExecContextParameter( 
			Guid parmKey,
			Guid requestKey,
			string parmName, 
			Guid parmTypeKey,
			string parmTypeName,
			string parmValue)
		{

			return CommitParameter
				(
				parmKey,
				requestKey,
				parmName,
				parmTypeKey,
				parmTypeName,
				parmValue,
				ParameterCategory.BatchExecutionContextCurrent 
				);
		}

        /// <summary>CommitParameter</summary>
        /// <param name="parmKey">System.Guid</param>
        /// <param name="requestKey">System.Guid</param>
        /// <param name="parmName">string</param>
        /// <param name="parmTypeKey">System.Guid</param>
        /// <param name="parmTypeName">string</param>
        /// <param name="parmValue">string</param>
        /// <param name="parmCategory">Avanade.ACA.Batch.ParameterCategory</param>
        /// <returns>System.Guid</returns>
        private Guid CommitParameter( 
			Guid parmKey,
			Guid requestKey,
			string parmName, 
			Guid parmTypeKey,
			string parmTypeName,
			string parmValue,
			ParameterCategory parmCategory)
		{
			try
			{
				DbCommand cw = _database.GetStoredProcCommand( 
					BatchDbConstants.SP.PARM_SAVE_BY_NAME);

				JoinTransaction( cw );

				if (parmTypeKey == Guid.Empty)
				{
					IDbConnection connection = cw.Connection;                    
					DbTransaction transaction = cw.Transaction;

					parmTypeKey = DefaultBatchManager.Parameter.SaveParamType
						(
						_database, 
						ref connection,
						ref transaction,
						Guid.NewGuid(),
						parmTypeName, 
						String.Empty
						);
				}

				_database.AddInParameter(cw, BatchDbConstants.Parm.PARAM_KEY, DbType.Guid, parmKey );
				_database.AddInParameter(cw, BatchDbConstants.Parm.PARAM_EXT_KEY, DbType.Guid, requestKey );
				_database.AddInParameter(cw, BatchDbConstants.Parm.PARAM_TYPE_KEY, DbType.Guid, parmTypeKey );
				_database.AddInParameter(cw, BatchDbConstants.Parm.PARAM_NAME, DbType.String, parmName );
				_database.AddInParameter(cw, BatchDbConstants.Parm.PARAM_VAL, DbType.String, parmValue );
				_database.AddInParameter(cw, BatchDbConstants.Parm.PARAM_CATEGORY, DbType.Int16, parmCategory);
				_database.AddOutParameter(cw, BatchDbConstants.Parm.PARAM_NEW_KEY, DbType.Guid, 16);
				DefaultBatchManager.ExecuteCommand(_database, cw, _transaction);
				// if a parameter already exists for the same name, use the existing key instead.
                parmKey = (Guid) _database.GetParameterValue(cw, BatchDbConstants.Parm.PARAM_NEW_KEY);
				return parmKey;
			}
			catch (Exception ex)
			{
				throw new BatchDatabaseFatalException( "Failed to commit parameter.", ex );
			}			
		}

        /// <summary>
        /// Add the Job execution status of the Request to the results being committed.
        /// </summary>
        /// <param name="jobExecKey">The key for the Job of the Request.</param>
        /// <param name="statusCode">The job status code.</param>
        /// <param name="workUnitCount">Number of the user-defined work unit completed.</param>
        /// <param name="lastCommitDate">The timestamp of the last time the job
        /// status has been committed.</param>
        /// <param name="commitCount">Number of times this job has been commit
        /// so far.</param>
        /// <returns>void</returns>
		public void CommitJobStatus(Guid jobExecKey,
			StatusCode statusCode,
			int workUnitCount,
			DateTime lastCommitDate,
			int commitCount)
		{
			try
			{
				DbCommand cw = _database.GetStoredProcCommand( 
					BatchDbConstants.SP.JOB_EXEC_UPDATE_STATUS);
		
				JoinTransaction( cw );

				_database.AddInParameter(cw,BatchDbConstants.Parm.Q_JOB_KEY, DbType.Guid, jobExecKey);
				_database.AddInParameter(cw,BatchDbConstants.Parm.Q_JOB_STATUS_CODE, DbType.Byte, statusCode);
				_database.AddInParameter(cw,BatchDbConstants.Parm.Q_JOB_WORK_UNIT_COUNT, DbType.Int32, workUnitCount);
				_database.AddInParameter(cw,BatchDbConstants.Parm.Q_JOB_COMMIT_COUNT, DbType.Int32, commitCount);
				_database.AddInParameter(cw,BatchDbConstants.Parm.Q_JOB_LAST_COMMIT, DbType.DateTime, lastCommitDate);
				// commit the status
				DefaultBatchManager.ExecuteCommand(_database, cw, _transaction);
				_jobStatusCode = statusCode;
				_jobCommitCount++;

			}
			catch (InvalidCastException)
			{
				// GetParamValue will cause InvalidCastException if the job is no longer active
				throw new BatchRequestNotFoundException();
			}
			catch (Exception ex)
			{
				throw new BatchDatabaseFatalException( "Failed to commit Job status.", ex );
			}
		}

        /// <summary>
        /// Updates the status code of the job record with the
        /// specified key.
        /// </summary>
        /// <param name="jobExecKey">The unique identifier of the 
        /// record in the database for the job.</param>
        /// <param name="statusCode">The value of the status code
        /// to record in the job row's status column.</param>
        /// <returns>void</returns>
		public void UpdateJobStatus(Guid jobExecKey, StatusCode statusCode)
		{
			try
			{
				DbCommand cw = _database.GetStoredProcCommand( 
					"ACA_BA_JobE_Stat");
		
				JoinTransaction( cw );

				_database.AddInParameter(cw,BatchDbConstants.Parm.Q_JOB_KEY, DbType.Guid, jobExecKey);
				_database.AddInParameter(cw,BatchDbConstants.Parm.Q_JOB_STATUS_CODE, DbType.Byte, statusCode);
				// commit the status
				DefaultBatchManager.ExecuteCommand(_database, cw, _transaction);
				_jobStatusCode = statusCode;

				_jobCommitCount++;
			}
			catch (InvalidCastException)
			{
				// GetParamValue will cause InvalidCastException if the job is no longer active
				throw new BatchRequestNotFoundException();
			}
			catch (Exception ex)
			{
				throw new BatchDatabaseFatalException( "Failed to commit Job status.", ex );
			}
		}

        /// <summary>
        /// Adds a Job Execution Parameter to the set of results being committed.
        /// </summary>
        /// <param name="parmKey">The key of the Job Execution Parameter.</param>
        /// <param name="jobExecKey">The key of the Job execution of the request.</param>
        /// <param name="parmName">The name of the request parameter.</param>
        /// <param name="parmTypeKey">The key of the parameter type.  If not known,
        /// use Guid.Empty.</param>
        /// <param name="parmTypeName">The name of the parameter type.  Only required
        /// if the parmTypeKey is Guid.Empty.</param>
        /// <param name="parmValue">The serialized value of the parameter.</param>
        /// <returns>The key of the parameter.</returns>
		public Guid CommitJobExecContextParameter( 
			Guid parmKey,
			Guid jobExecKey,
			string parmName, 
			Guid parmTypeKey,
			string parmTypeName,
			string parmValue)
		{
			return CommitParameter
				(
					parmKey,
					jobExecKey, 
					parmName, 
					parmTypeKey, 
					parmTypeName, 
					parmValue,
					ParameterCategory.JobExecutionContextCurrent
				);
		}


        /// <summary>
        /// Adds the job execution exception to the set of results being committed.
        /// </summary>
        /// <param name="exceptionKey">The key of the exception instance</param>
        /// <param name="jobExpKey">The key of the Job execution of the request.</param>
        /// <param name="expType">Type of the exception.</param>
        /// <param name="expMessge">Message of the exception.</param>
        /// <param name="expSource">Source of the exception.</param>
        /// <param name="expTarget">Target of the exception.</param>
        /// <param name="expStackTrace">Stack trace of the exception.</param>
        /// <param name="innerExceptionKey">The key to the inner exception.</param>
        /// <returns>The key of the exception.  Can be used as the innerExceptionKey
        /// for another exception for the same job execution.</returns>
		public Guid CommitJobException(
			Guid exceptionKey,
			Guid jobExpKey,
			string expType,
			string expMessge,
			string expSource,
			string expTarget,
			string expStackTrace,
			Guid innerExceptionKey)
		{
			try
			{
				DbCommand cw = _database.GetStoredProcCommand( 
					BatchDbConstants.SP.EXCEPTION_SAVE);

				JoinTransaction( cw );

				bool isNullable = false;
				byte precision	= 0;
				byte scale		= 0;

				ParameterDirection	direction	= ParameterDirection.Input;
				DataRowVersion		rowVersion	= DataRowVersion.Default;
				string				sourceColumn= String.Empty;

				const string EmptyValue = "(none)";
				
				if (expType == null ||
					expType.Length == 0)
				{
					expType = EmptyValue;
				}

				if (expMessge == null ||
					expMessge.Length == 0)
				{
					expMessge = EmptyValue;
				}

				if (expSource == null ||
					expSource.Length == 0)
				{
					expSource = EmptyValue;
				}

				if (expTarget == null ||
					expTarget.Length == 0)
				{
					expTarget = EmptyValue;
				}

				if (expStackTrace == null ||
					expStackTrace.Length == 0)
				{
					expStackTrace = EmptyValue;
				}

				const int StackTraceSize = 2225;
				
				if (expStackTrace.Length > StackTraceSize)
				{
					expStackTrace = 
						expStackTrace.Substring(0, StackTraceSize - 1);
				}

				_database.AddInParameter(cw, BatchDbConstants.Parm.EXP_KEY, DbType.Guid, exceptionKey );
				_database.AddInParameter(cw, BatchDbConstants.Parm.Q_JOB_KEY, DbType.Guid, jobExpKey );
                _database.AddParameter(cw, BatchDbConstants.Parm.EXP_TYPE, 
						DbType.String, 255, direction, isNullable, precision,
						scale, sourceColumn, rowVersion, expType );
				_database.AddParameter(cw,BatchDbConstants.Parm.EXP_MESSAGE, 
					DbType.String, 1000, direction, isNullable, precision,
					scale, sourceColumn, rowVersion, expMessge);
				_database.AddParameter(cw,BatchDbConstants.Parm.EXP_SOURCE, 
					DbType.String, 255, direction, isNullable, precision,
					scale, sourceColumn, rowVersion, expSource);
				_database.AddParameter(cw,BatchDbConstants.Parm.EXP_TRAGET_SITE, 
					DbType.String, 255, direction, isNullable, precision,
					scale, sourceColumn, rowVersion, expTarget);
				_database.AddParameter(cw,BatchDbConstants.Parm.EXP_STACK_TRACE, 
					DbType.String, StackTraceSize, direction, isNullable, precision,
					scale, sourceColumn, rowVersion, expStackTrace);
				_database.AddInParameter(cw, BatchDbConstants.Parm.EXP_INNER_EXP_KEY, DbType.Guid, innerExceptionKey );

				DefaultBatchManager.ExecuteCommand(_database, cw, _transaction);

				return exceptionKey;
			}
			catch (Exception ex)
			{
				throw new BatchDatabaseFatalException( "Failed to commit Job exceptions.", ex );
			}
		}

        /// <summary>JoinTransaction</summary>
        /// <param name="cw">System.Data.Common.DbCommand</param>
        /// <returns>void</returns>
		private void JoinTransaction(DbCommand cw)
		{
			// open the transaction with ReadUnCommitted isolation level to
			// allow the copying of the uncommitted request parameters.
			if (_connection == null)
			{
				DefaultBatchManager.OpenTransaction( cw,
					_database,
					ref _transaction,
					ref _connection,
					IsolationLevel.ReadCommitted);
			}
			else
			{
				DefaultBatchManager.JoinTransaction( cw,
					_database,
					ref _transaction,
					ref _connection );
			}
		}
	}
}
