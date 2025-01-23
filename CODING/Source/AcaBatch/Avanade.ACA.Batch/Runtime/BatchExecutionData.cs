// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Collections;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Avanade.ACA.Batch.Instrumentation;
using Avanade.ACA.Batch.Configuration;

namespace Avanade.ACA.Batch
{

	/// <summary>
	/// The parent of BatchExecutionContextData.
	/// </summary>
	public class BatchExecutionData : IBatchDBData 
	{
		
		private static BatchExecutionData instance;
		
		private string databaseInstanceName = String.Empty;
		private Database dataInstance;
		private ArrayList alChildren;


		private const string REQUEST_LOAD_FAILURE = "Failed to load batch request '{0}' from the Batch Database.";
        /// <summary>
        /// Intializes a new instance of the <see cref="BatchExecutionData"/>
        /// class.
        /// </summary>
		public BatchExecutionData()
		{
			alChildren = new ArrayList();
		}

        /// <summary>
        /// Construct a <see cref="BatchExecutionContextData"/> object from the database using
        /// the cached database instance.
        /// </summary>
        /// <param name="request">the <see cref="BatchExecutionRequest"/> instance.</param>
        /// <returns>a <see cref="BatchExecutionContextData"/> instance with data loaded from the batch database.</returns>
		public BatchExecutionContextData LoadRequestedBatch(BatchExecutionRequest request)
		{
			return LoadRequestedBatch(request.Key);
		}

        /// <summary>
        /// Construct a <see cref="BatchExecutionContextData"/> object from the database using
        /// the given database instance.
        /// </summary>
        /// <param name="database">a Batch database instance.</param>
        /// <param name="request">the <see cref="BatchExecutionRequest"/> instance.</param>
        /// <returns>a <see cref="BatchExecutionContextData"/> instance with data loaded from the batch database.</returns>
		public BatchExecutionContextData LoadRequestedBatch
			(
				Database database, 
				BatchExecutionRequest request
			)
		{
			return LoadRequestedBatch(database, request.Key);
		}

        /// <summary>
        /// Construct a <see cref="BatchExecutionContextData"/> object from the database with a key using
        /// the cached database instance.
        /// </summary>
        /// <param name="requestKey">the key for the request.</param>
        /// <returns>a <see cref="BatchExecutionContextData"/> instance with data loaded from the batch database.</returns>
		public BatchExecutionContextData LoadRequestedBatch(Guid requestKey)
		{
			return LoadRequestedBatch(null, requestKey);
		}

        /// <summary>
        /// Construct a <see cref="BatchExecutionContextData"/> object from the database with a key.
        /// </summary>
        /// <param name="database">the Batch database instance.</param>
        /// <param name="requestKey">the key for the request.</param>
        /// <returns>a <see cref="BatchExecutionContextData"/> instance with data loaded from the batch database.</returns>
		public BatchExecutionContextData LoadRequestedBatch(Database database, Guid requestKey)
		{
			try
			{
				if (database == null)
					database = this.dataInstance;

				BatchExecutionContextData node = 
					new BatchExecutionContextData(database, requestKey);

				if (!node.LoadFromDatabase())
				{
					string errorMsg = string.Format(REQUEST_LOAD_FAILURE, requestKey);                    
                    BatchInstrumentationProvider.Instance.FireBatchArchitectureConfigFailureEvent(errorMsg, null);
					return null;
				}
					 
				return node;
			}
			catch (ACABatchException)
			{
				throw;
			}
			catch (Exception e)
			{
				string errorMsg = string.Format(REQUEST_LOAD_FAILURE, requestKey);
                BatchInstrumentationProvider.Instance.FireBatchArchitectureConfigFailureEvent(errorMsg, e);	
				throw new ACABatchFatalException(errorMsg, e);
			}
			
		}

        /// <summary>LoadRequestedBatch</summary>
        /// <param name="database">Microsoft.Practices.EnterpriseLibrary.Data.Database</param>
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
        /// <returns>Avanade.ACA.Batch.BatchExecutionContextData</returns>
		public BatchExecutionContextData LoadRequestedBatch(
			Database				database, 
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
			try
			{
				BatchExecutionContextData node = 
					new BatchExecutionContextData(database, requestKey);
				
				node.LoadContext(requestKey,
					batchName,
					description,
					restart,
					configFilePath,
					isolationLevel,
					exePriorityLevel,
					queuePriorityLevel,
					maxConcurrent,
					destination,
					batchTypeName,
					relativeExpirationDateInMilli,
					absoluteExpirationDate,
					batchServerName,
					batchClientName,
					queuedDate,
					startDate,
					lastUpdateDate,
					batchStatus,
					originalBatchKey,
					lastExecutionKey,
					toPause,
					parentRequestKey,
					nextExecutionKey);

				alChildren.Add(node);
				node.LoadFromDatabase();

				return node;
			}
			catch (ACABatchException)
			{
				throw;
			}
			catch (Exception e)
			{
				string errorMsg = string.Format(REQUEST_LOAD_FAILURE, requestKey);                
                BatchInstrumentationProvider.Instance.FireBatchArchitectureConfigFailureEvent(errorMsg, e);					
				throw new ACABatchFatalException(errorMsg, e);
			}
		}
		
		/// <summary>
		/// The database instance used to load and save the data.
		/// </summary>
		public Database DatabaseInstance
		{
			get {return this.dataInstance;}
		}

		/// <summary>
		/// See <see cref="IBatchData.DisplayName"/>.  The valus is always "Batch".
		/// </summary>
		public string DisplayName
		{
			get
			{
				return "Batch";
			}
		}

		/// <summary>
		/// Implements the <see cref="IBatchDBData.Key"/> property.  Gets or sets the key for the data.
		/// </summary>
		/// <value>A <see cref="System.Guid"/> instance as the key of the data object.</value>
		public Guid Key
		{
			get {return Guid.Empty;}
		}

		/// <summary>
		/// See <see cref="IBatchDBData.Children"/>.
		/// </summary>
		public IList Children
		{
			get {return this.alChildren;}
		}
		
		/// <summary>
		/// 
		/// </summary>
		public static BatchExecutionData Current
		{
			get
			{
				if (instance == null) 
				{
					instance = GetNewInstance();
				}
				return instance;
			}
		}

        /// <summary>GetNewInstance</summary>
        /// <returns>Avanade.ACA.Batch.BatchExecutionData</returns>
		public static BatchExecutionData GetNewInstance()
		{            
			BatchSettings batchSettings = (Configuration.BatchSettings) ConfigurationManager.GetSection(Configuration.BatchSettings.SectionName);
			BatchExecutionSettingsData execSettings = batchSettings.BatchExecutionSettings;
			try
			{
				instance = new BatchExecutionData();
				instance.databaseInstanceName = execSettings.DatabaseInstanceName;
				instance.dataInstance = DatabaseFactory.CreateDatabase(execSettings.DatabaseInstanceName);
			}
			catch(Exception ex)
			{
				throw new Exception("Unable to access datatbase. Please check database settings.", ex);
			}
			return instance;

		}

	}
}

