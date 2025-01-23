// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Threading;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Avanade.ACA.Batch.Configuration;
using Avanade.ACA.Batch.Instrumentation;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Design;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// Represents the class that knows 
	/// how to parse the command line arguments
	/// for Batch Execution.
	/// </summary>
	public class Requester
	{
		private const string EXIT_CODE_STRATEGY_FAILURE = "Failed to instanciate exit code strategy.";
		private const string BATCH_DATABASE_FAILURE = "Error(s) occurs in parsing the Batch Database configuration of the Batch Client application.";
		private const string PROCESS_REQUEST_FAILURE = "Batch Client failed to process batch request failed.";
		private string[]					_requestFilePaths;
		private IBatchExecutionRequestQueue _queue;
		private TimeSpan					_pollInterval;
		private IProcessExitCodeStrategy	_exitCodeStrategy;

        /// <summary>
        /// Creates a new instance of the <see cref="Requester"/> class.
        /// </summary>
        /// <param name="commandLineArguments">A string array consisting of the
        /// command line arguments.
        /// </param>
		public Requester(string[] commandLineArguments)
		{
			Parse(commandLineArguments);
		}

		/// <summary>
		/// Gets the <see cref="IProcessExitCodeStrategy"/>
		/// object for mapping the <see cref="BatchProcessStatusCode"/>
		/// to an exit code.
		/// </summary>
		public IProcessExitCodeStrategy ProcessExitCodeStrategy
		{
			get
			{
				return _exitCodeStrategy;
			}
		}
        /// <summary>
        /// Creates a new instance of the <see cref="Requester"/> class.
        /// </summary>
        /// <param name="requestFilePaths">An array of request file paths</param>
        /// <param name="queue">The batch execution request queue</param>
		public Requester(string[] requestFilePaths, 
			IBatchExecutionRequestQueue queue)
		{
			_requestFilePaths = requestFilePaths;
			_queue = queue;
		}



        /// <summary>Parse</summary>
        /// <param name="args">string[]</param>
        /// <returns>void</returns>
        private void Parse(string[] args)
		{
			// Parse and validate the command line arguments
			_requestFilePaths = args;

			// Get a handle to the client configuration 
			UnhandledExceptionHandlerCollection handlers
				= new UnhandledExceptionHandlerCollection();
			handlers.Enable();            
			BatchSettings batchSettings = ConfigurationManager.GetSection(BatchSettings.SectionName) as BatchSettings;
            BatchClientSettingsData clientNode = batchSettings.BatchClientSettings;
			// Get the polling interval
			_pollInterval = clientNode.PollInterval;

			try
			{
				// Get the Exit Code Mapping Strategy;
				Type exitCodeStrategyType = Type.GetType(
					clientNode.ExitCodeStrategyTypeName,
					true);

				_exitCodeStrategy = (IProcessExitCodeStrategy)Activator.CreateInstance(exitCodeStrategyType);
			}
			catch (Exception e)
			{
                BatchInstrumentationProvider.Instance.FireBatchArchitectureConfigFailureEvent(EXIT_CODE_STRATEGY_FAILURE, e);
				throw new ACABatchFatalException(EXIT_CODE_STRATEGY_FAILURE, e);
			}

			try
			{
				// load the database
				string databaseName = clientNode.DatabaseInstanceName;
				Database database = DatabaseFactory.CreateDatabase(databaseName);
				// create the queue object
				_queue = new BatchExecutionRequestQueue(database);
			}
			catch (Exception e)
			{
                BatchInstrumentationProvider.Instance.FireBatchArchitectureConfigFailureEvent(BATCH_DATABASE_FAILURE, e);				
				throw new ACABatchFatalException(BATCH_DATABASE_FAILURE, e);
			}
		}


        /// <summary>
        /// Processes the requests with which this instance was created.
        /// </summary>
        /// <returns>The exit code to be returned to the operating-system.</returns>
		public int ProcessRequests()
		{
			try
			{
				// get the number of requests to
				// be processed
				int numberOfRequests = _requestFilePaths.Length;

				// create arrays to hold the request objects
				// and the wait handle objects
				BatchExecutionRequest[] requests =
					new BatchExecutionRequest[numberOfRequests];

				WaitHandle[] waitHandles = new WaitHandle[numberOfRequests];

				BatchExecutionRequestAsyncResult[] results = 
					new BatchExecutionRequestAsyncResult[numberOfRequests];

				// Deserialize and process each request
				for (int i = 0; i < numberOfRequests; i++)
				{
					string filePath = _requestFilePaths[i];
				
					// deserialize the request object
					// from the XML file
					requests[i] = BatchExecutionRequest.Deserialize(filePath);

					// process the request asyncronously
					// and store the async result
					results[i] = _queue.ProcessRequestAsync(requests[i], _pollInterval);

					// store the wait handle
					waitHandles[i] = results[i].AsyncWaitHandle;
				}
			
				// Wait for all batches to finish
				// executing
				WaitHandle.WaitAll(waitHandles);

				BatchProcessStatusCode[] statusCodes = new BatchProcessStatusCode[numberOfRequests];

				int count=0;
				foreach (BatchExecutionRequestAsyncResult result in results)
				{
					statusCodes[count++] = result.GetResponse().Status;
				}
				return _exitCodeStrategy.Map(statusCodes);
			}
			catch (Exception e)
			{				
                BatchInstrumentationProvider.Instance.FireBatchArchitectureFailedEvent(PROCESS_REQUEST_FAILURE, e);	
				BatchProcessStatusCode[] statusCodes = {BatchProcessStatusCode.Failed};
				return _exitCodeStrategy.Map(statusCodes);
			}		
		}
	}
}
