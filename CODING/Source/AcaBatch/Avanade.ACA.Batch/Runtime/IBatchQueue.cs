// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// Represents the interface that a component that implements
	/// a Batch Request Queue must implement.
	/// </summary>
	public interface IBatchExecutionRequestQueue
	{
        /// <summary>
        /// Enqueue the request
        /// </summary>
        /// <param name="request">The request object to be enqueued.</param>
        /// <returns>void</returns>
		void Enqueue(BatchExecutionRequest request);

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
		DequeuedBatchExecutionRequest Dequeue(string[] destinations,
												string serverName);

        /// <summary>
        /// Enqueues the specified request and returns once
        /// execution of the request has completed.
        /// </summary>
        /// <param name="request">Avanade.ACA.Batch.BatchExecutionRequest</param>
        /// <param name="responsePollingInterval">System.TimeSpan</param>
        /// <exception cref="BatchRequestNotFoundException"/>
        /// <returns>An instance of the <see cref="BatchExecutionResponse"/></returns>
		BatchExecutionResponse ProcessRequest(BatchExecutionRequest request,
												TimeSpan responsePollingInterval);

        /// <summary>
        /// Submits a batch request for asynchronous execution and
        /// returns immediately without waiting for execution to
        /// complete.Use the returned <see cref="BatchExecutionRequestAsyncResult"/>
        /// to track status of the batch request.
        /// </summary>
        /// <param name="request">The batch execution request object</param>
        /// <param name="responsePollingInterval">Frequency at which the database should be polled for a response.</param>
        /// <returns>An instance of <see cref="BatchExecutionRequestAsyncResult"/></returns>
		BatchExecutionRequestAsyncResult 
			ProcessRequestAsync(BatchExecutionRequest request,
								TimeSpan responsePollingInterval);

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
		BatchExecutionRequestAsyncResult 
			ProcessRequestAsync(BatchExecutionRequest request,
			TimeSpan responsePollingInterval,
			AsyncCallback asyncCallback);


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
		BatchExecutionRequestAsyncResult 
			ProcessRequestAsync(BatchExecutionRequest request,
			TimeSpan responsePollingInterval,
			AsyncCallback asyncCallback,
			object asyncState);
	}
}
