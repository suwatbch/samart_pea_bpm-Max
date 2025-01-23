// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Threading;
using Avanade.ACA.Batch.Configuration;
using Avanade.ACA.Batch.Instrumentation;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// This type supports the ACA.NET Framework 
	/// infrastructure and is not intended to be 
	/// used directly from your code.
	/// </summary>
	public class ThreadIsolationStrategy : BatchExecutionIsolationStrategy
	{
        /// <summary>
        /// Creates a new instance of the <see cref="ThreadIsolationStrategy"/>class.
        /// </summary>
		public ThreadIsolationStrategy()
		{
		}

        /// <summary>
        /// Implements the code to start a batch request on a new thread.
        /// </summary>
        /// <param name="requestKey">The key of the dequeued batch execution request.</param>
        /// <param name="configurationFilePath">The path of the configuration file for the Batch execution.
        /// It is ignored in this class.</param>
        /// <returns>void</returns>
		protected override void ExecuteImpl(Guid requestKey, string configurationFilePath)
		{
			try
			{
				BatchArchitectureEvent.AppendExecutionLog("ThreadIsolation.ExecuteImpl invoked.");

				BatchExecutionManager manager = 
					new BatchExecutionManager();

				// creates a DequeuedBatchExecutionRequest
				DequeuedBatchExecutionRequest request = 
					CreateDequeuedBatchExecutionRequestFromKey(requestKey);

				BatchExecutionContext context = 
					manager.CreateBatchExecutionContext(request);

				ThreadPriority threadPriority =
					PriorityConverter.ToThreadPriorityLevel(request.ExecutionPriority);

				Thread.CurrentThread.Priority = threadPriority;

				context.Batch.Execute(context);
			}
			catch (ACABatchException)
			{
				throw;
			}
			catch (Exception e)
			{
				string errorMsg = "ThreadIsolationStrategy Failed to execute request.";
                BatchInstrumentationProvider.Instance.FireBatchArchitectureFailedEvent(errorMsg, e);				
				throw new ACABatchException(errorMsg, e);
			}
		}

        /// <summary>CreateDequeuedBatchExecutionRequestFromKey</summary>
        /// <param name="requestKey">System.Guid</param>
        /// <returns>Avanade.ACA.Batch.DequeuedBatchExecutionRequest</returns>
		private DequeuedBatchExecutionRequest CreateDequeuedBatchExecutionRequestFromKey
			(
			Guid requestKey
			)
		{
			BatchArchitectureEvent.AppendExecutionLog(
				"ThreadIsolation.CreateDequeuedBatchExecutionRequestFromKey invoked.");

			// deque the next worker item
			// by calling the stored procedure
			// that gets the record from the
			// QUEUE table with the highest 
			// priority and oldest time

			string batchName;
			string					description;
			BatchRestartBehavior	restart;
			long					relativeExpirationDateInMilli;
			ExecutionIsolationLevel isolationLevel;
			ExecutionPriorityLevel	exePriorityLevel;
			QueuePriorityLevel		queuePriorityLevel; 
			int						maxConcurrent;
			string					batchTypeName;
			string					destination;
			string					batchServerName;
			string					batchClientName;
			string					configFilePath;
			DateTime				queuedDate;
			DateTime				startDate;
			Guid					originalBatchKey;
			Guid					parentRequestKey;
			Guid					lastExecutionKey;
			Guid					nextExecutionKey;
			BatchProcessStatusCode	batchStatus;
			DateTime				lastUpdateDate;
			DateTime				absoluteExpirationDate;
			bool					toPause;

            BatchSettings batchSettings = (BatchSettings) ConfigurationManager.GetSection(BatchSettings.SectionName);		
			BatchExecutionSettingsData executionSettings = batchSettings.BatchExecutionSettings;
			BatchArchitectureEvent.AppendExecutionLog("Got BatchExecutionSettings.");			
			Database database = DatabaseFactory.CreateDatabase(executionSettings.DatabaseInstanceName);
			BatchArchitectureEvent.AppendExecutionLog("CreateDatabase succeeded.");
			try
			{
				DefaultBatchManager.RequestQueue.Get(database,
					requestKey,
					out batchName,
					out description,
					out restart,
					out configFilePath,
					out isolationLevel,
					out exePriorityLevel,
					out queuePriorityLevel,
					out maxConcurrent,
					out destination,
					out batchTypeName,
					out relativeExpirationDateInMilli,
					out absoluteExpirationDate,
					out batchServerName,
					out batchClientName,
					out queuedDate,
					out startDate,
					out lastUpdateDate,
					out batchStatus,
					out originalBatchKey,
					out lastExecutionKey,
					out toPause,
					out parentRequestKey,
					out nextExecutionKey);
			}
			catch (Exception e)
			{
				string errorMsg = string.Format("Failed to get request '{0}' from database.", requestKey.ToString());
                BatchInstrumentationProvider.Instance.FireBatchArchitectureFailedEvent(errorMsg, e);
				throw new ACABatchException(errorMsg, e);
			}
			BatchArchitectureEvent.AppendExecutionLog(string.Format("Got data for request '{0}' from database.", requestKey.ToString()));

			DequeuedBatchExecutionRequest request = 
				new DequeuedBatchExecutionRequest(requestKey, 
				batchName,
				isolationLevel,
				exePriorityLevel,
				configFilePath);

			return request;
		}
	}
}
