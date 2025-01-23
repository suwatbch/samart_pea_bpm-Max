// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Collections.Specialized;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Avanade.ACA.Batch.Instrumentation;
using Avanade.ACA.Batch.Configuration;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// Represents the Queue to which batch requests are submitted for execution.
	/// </summary>
	public class BatchExecutionRequestQueue : MarshalByRefObject, IBatchExecutionRequestQueue
	{
		private Database		_database;
		private const string	ENQUEUE_FAILURE = "Failed to enqueue batch request \"{0}\".";

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="BatchExecutionRequestQueue"/>
        /// class.
        /// </summary>
        /// <param name="database">The Batch Database</param>
		public BatchExecutionRequestQueue(Database database)
		{
			_database	= database;
		}

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="BatchExecutionRequestQueue"/>
        /// class.
        /// </summary>
        /// <param name="dbServiceKey">string</param>
		public BatchExecutionRequestQueue(string dbServiceKey) 
			: this(DatabaseFactory.CreateDatabase(dbServiceKey))
		{
		}

        /// <summary>
        /// Enqueues the specified request and returns once
        /// execution of the request has completed.
        /// </summary>
        /// <param name="request">Avanade.ACA.Batch.BatchExecutionRequest</param>
        /// <param name="responsePollingInterval">System.TimeSpan</param>
        /// <exception cref="BatchRequestNotFoundException"/>
        /// <returns>An instance of the <see cref="BatchExecutionResponse"/></returns>
		public BatchExecutionResponse 
			ProcessRequest(BatchExecutionRequest request,
							TimeSpan responsePollingInterval)
		{
			BatchExecutionRequestAsyncResult result =
				ProcessRequestAsync(request,
									responsePollingInterval);

			// Block until the asynchronous method returns.
			// The method will return once the batch status
			// is Succeeded or Failed.
			BatchExecutionResponse response = result.GetResponse();

			return response;
		}

        /// <summary>
        /// Submits a batch request for asynchronous execution and
        /// returns immediately without waiting for execution to
        /// complete.Use the returned <see cref="BatchExecutionRequestAsyncResult"/>
        /// to track status of the batch request.
        /// </summary>
        /// <param name="request">The batch execution request object</param>
        /// <param name="responsePollingInterval">Frequency at which the database should be polled for a response.</param>
        /// <returns>An instance of <see cref="BatchExecutionRequestAsyncResult"/></returns>
		public BatchExecutionRequestAsyncResult 
			ProcessRequestAsync(BatchExecutionRequest request,
								TimeSpan responsePollingInterval)
		{
			return ProcessRequestAsync(request, 
										responsePollingInterval,
										null, 
										null);
		}


        /// <summary>
        /// Submits a batch request for asynchronous execution and
        /// returns immediately without waiting for execution to
        /// complete.Use the returned <see cref="BatchExecutionRequestAsyncResult"/>
        /// to track status of the batch request.
        /// </summary>
        /// <param name="request">The batch execution request object</param>
        /// <param name="responsePollingInterval">Frequency at which the database should be polled for a response.</param>
        /// <param name="asyncCallback">Method to be called when response is ready.</param>
        /// <returns>An instance of <see cref="BatchExecutionRequestAsyncResult"/></returns>
		public BatchExecutionRequestAsyncResult 
			ProcessRequestAsync(BatchExecutionRequest request,
								TimeSpan responsePollingInterval,
								AsyncCallback asyncCallback)
		{
			return ProcessRequestAsync(request, 
										responsePollingInterval,
										asyncCallback, 
										null);
		}

        /// <summary>
        /// Submits a batch request for asynchronous execution and
        /// returns immediately without waiting for execution to
        /// complete.Use the returned <see cref="BatchExecutionRequestAsyncResult"/>
        /// to track status of the batch request.
        /// </summary>
        /// <param name="request">The batch execution request object</param>
        /// <param name="responsePollingInterval">Frequency at which the database should be polled for a response.</param>
        /// <param name="asyncCallback">Method to be called when response is ready.</param>
        /// <param name="asyncState">Any state you wish to store with the request.
        /// This state will passed back in callback method.
        /// </param>
        /// <returns>An instance of <see cref="BatchExecutionRequestAsyncResult"/></returns>
		public BatchExecutionRequestAsyncResult 
			ProcessRequestAsync(BatchExecutionRequest request,
								TimeSpan responsePollingInterval,
								AsyncCallback asyncCallback,
								object asyncState)
		{
			// enqueue the request
			Enqueue(request);

			return new BatchExecutionRequestAsyncResult
				(
					_database, 
					request,
					responsePollingInterval,
					asyncCallback, 
					asyncState
				);
		}


        /// <summary>
        /// Dequeues a batch request if it belongs to one of the
        /// specified destinations.
        /// </summary>
        /// <param name="destinations">Array of destinations. Only those requests
        /// that are targeted to one of the destinations in this array will be
        /// dequeued</param>
        /// <param name="serverName">The name of the Batch Server to write
        /// to the database when the request is dequeued.</param>
        /// <returns>An instance of <see cref="DequeuedBatchExecutionRequest"/></returns>
		public DequeuedBatchExecutionRequest Dequeue
			(
				string[] destinations,
				string serverName
			)
		{
				StringCollection nonEmptyDestination = new StringCollection();
				foreach (string dest in destinations)
				{
					string realDest = dest.Trim();
					if (realDest.Length > 0)
					{
						nonEmptyDestination.Add(realDest);
					}
				}
			
				StringBuilder builder = new StringBuilder();
				int length = nonEmptyDestination.Count;
				for (int i = 0; ; )
				{
					string destination = nonEmptyDestination[i];
					builder.Append(destination);
					
					i++;

					if (i != length)
					{
						builder.Append(',');
					}
					else
					{
						break;
					}
				}

				string destinationFilter = builder.ToString();
				
				// deque the next worker item
				// by calling the stored procedure
				// that gets the record from the
				// QUEUE table with the highest 
				// priority and oldest time

				Guid requestKey;
				string batchName;
				ExecutionIsolationLevel isolationLevel;
				ExecutionPriorityLevel priority;
				string configurationFile;

				if ( DefaultBatchManager.RequestQueue.Dequeue(_database,
					destinationFilter,
					serverName,
					out requestKey,
					out batchName,
					out isolationLevel,
					out priority,
					out configurationFile) )
				{
					DequeuedBatchExecutionRequest request = 
						new DequeuedBatchExecutionRequest(requestKey, 
						batchName,
						isolationLevel,
						priority,
						configurationFile);
					return request;
				}
				else
				{
					return null;
				}
			
		}

        /// <summary>
        /// Enqueues a request to the Batch Queue. You should call the
        /// <see cref="ProcessRequest"/> and <see cref="ProcessRequestAsync"/>
        /// methods to submit a batch request rather than calling this method
        /// directly.
        /// </summary>
        /// <param name="request">The reequest to be enqueued</param>
        /// <exception cref="BatchRequestNotFoundException"/>
        /// <returns>void</returns>
		public void Enqueue(BatchExecutionRequest request)
		{
			DateTime expiration = DateTime.Now + request.QueueTimeout;
			
			object destination = DBNull.Value;
			if (!request.UsePreDefinedDestination)
			{
				destination = request.Destination;
			}
			
			object queuePriority = DBNull.Value;
			if (!request.UsePreDefinedQueuePriority)
			{
				queuePriority = request.QueuePriority;
			}
			if (request.Key == Guid.Empty)
			{
				// generate a unique request 
				// execution id for this request
				Guid key = Guid.NewGuid();

				Guid parentRequestKey = Guid.Empty;
				if (request.ParentRequest != null)
				{
					parentRequestKey = request.ParentRequest.Key;
				}

				// insert a record in the QUEUE table
				RequestEnqueueHandle enqueueHandle = 
					DefaultBatchManager.RequestQueue.CreateRequestEnqueueHandle(
						_database,
						key,
						request.BatchKey, 
						request.BatchName,
						parentRequestKey );

				try
				{
					enqueueHandle.BeginEnqueue(
						queuePriority,
						expiration,
						destination,
						request.BatchClientName	);

					Guid parameterTypeKey = Guid.Empty;
					foreach (ParameterData parm in request.Parameters)
					{
						string parameterXml = parm.Serialize();
						enqueueHandle.AddRequestParameter(parm.DisplayName,parm.ValueType,parameterXml,ref parameterTypeKey);
						parameterTypeKey = Guid.Empty;
					}

					enqueueHandle.EndEnqueue( true );
					// if insert succeeds,
					// Set the field on the request
					request.SetKey(key);
				}
				catch (Exception e)
				{
					enqueueHandle.EndEnqueue( false );                    
                    BatchInstrumentationProvider.Instance.FireBatchArchitectureFailedEvent(string.Format(ENQUEUE_FAILURE, request.BatchName), e);
					throw;
				}
			}
			else
			{
				// this is a retry scenario
				Guid failedRequestKey = request.Key;
				Guid newRequestKey = Guid.NewGuid();
				
				DefaultBatchManager.RequestQueue.RestartFromFailure
					(
					_database, 
					failedRequestKey, 
					newRequestKey,
					queuePriority,
					expiration,
					destination
					);
				
				request.SetKey(newRequestKey);
			}

			
		}
	}

}
