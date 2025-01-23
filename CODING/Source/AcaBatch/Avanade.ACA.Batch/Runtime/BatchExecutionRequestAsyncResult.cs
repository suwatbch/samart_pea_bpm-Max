// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Threading;

using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// Encapsulates the results of an asynchronous batch execution
	/// on an asynchronous delegate
	/// </summary>
	[Serializable]
	public class BatchExecutionRequestAsyncResult : IAsyncResult
	{

		private IAsyncResult			_innerResult;
		private BatchExecutionRequest	_request;
		private ThreadStart				_pollResponse;
		private Thread					_pollingThread;
		private TimeSpan				_responseDueTime;
		private TimeSpan				_responsePollingInterval;
		private Database				_database;
		private BatchExecutionResponse	_response;

        /// <summary>
        /// This constructor in intented for internal
        /// ACA.NET framework use only.
        /// </summary>
        /// <param name="database">Microsoft.Practices.EnterpriseLibrary.Data.Database</param>
        /// <param name="request">Avanade.ACA.Batch.BatchExecutionRequest</param>
        /// <param name="responsePollingInterval">System.TimeSpan</param>
        /// <param name="asyncCallback">System.AsyncCallback</param>
        /// <param name="asyncState">object</param>
		internal BatchExecutionRequestAsyncResult(
			Database database,
			BatchExecutionRequest request,
			TimeSpan responsePollingInterval,
			AsyncCallback asyncCallback,
			object asyncState)
		{
			_database					= database;
			_request					= request;
			_responseDueTime			= request.StartPollingForResultAfter;
			_responsePollingInterval	= responsePollingInterval;

			// asynchronously invoke the method
			// that will poll for status of the 
			// request execution.
			 _pollResponse
				= new ThreadStart(PollResponse);

			// return an asynchronous result
			// object so the client can wait check
			// if the work has completed.
			_innerResult =
				_pollResponse.BeginInvoke(asyncCallback, asyncState );
		}

		/// <summary>
		/// Gets the <see cref="BatchExecutionRequest"/>
		/// </summary>
		public BatchExecutionRequest Request
		{
			get
			{
				return _request;
			}
		}

        /// <summary>
        /// Waits for the batch execution to complete and
        /// returns the <see cref="BatchExecutionResponse"/> object.
        /// </summary>
        /// <returns>Avanade.ACA.Batch.BatchExecutionResponse</returns>
		public BatchExecutionResponse GetResponse()
		{
				_pollResponse.EndInvoke(_innerResult);

			return Response;
		}

		/// <summary>
		/// Gets the state stored in the internal <see cref="IAsyncResult"/>
		/// </summary>
		public object AsyncState
		{
			get
			{
				return _innerResult.AsyncState;
			}
		}

		/// <summary>
		/// Indicates if the batch completed synchronously
		/// or not.
		/// </summary>
		public bool CompletedSynchronously
		{
			get
			{
				return _innerResult.CompletedSynchronously;
			}
		}

		/// <summary>
		/// Gets the <see cref="WaitHandle"/> for the asynchronous operation
		/// </summary>
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				return _innerResult.AsyncWaitHandle;
			}
		}

		/// <summary>
		/// Indicates if the batch execution has completed or not.
		/// </summary>
		public bool IsCompleted
		{
			get
			{
				return _innerResult.IsCompleted;
			}
		}

		/// <summary>
		/// Gets the <see cref="ThreadState"/> for the polling thread.
		/// </summary>
		public ThreadState PollingThreadState
		{
			get
			{
				if (_pollingThread != null)
				{
					return _pollingThread.ThreadState;
				}
				else
				{
					return ThreadState.Unstarted;
				}
			}
		}

        /// <summary>PollResponse</summary>
        /// <returns>void</returns>
        private void PollResponse()
		{
			_pollingThread = Thread.CurrentThread;

			TimeSpan dueTime = EstimatedResponseTime;
			// sleep for the specified time span
			// and then begin polling
			Thread.Sleep(dueTime);
			
			BatchProcessStatusCode statusCode = 
				BatchProcessStatusCode.Enqueued;

			while (true)
			{
				_response = 
					new BatchExecutionResponse(_database, Request);

				// check the status code
				statusCode = _response.Status;
				
				if (statusCode == BatchProcessStatusCode.Succeeded
					|| statusCode == BatchProcessStatusCode.Failed
					|| statusCode == BatchProcessStatusCode.FailedCanceled
					|| statusCode == BatchProcessStatusCode.FailedTimedOut)
				{
					break;
				}

				// wait for the poll interval to elapse.
				Thread.Sleep(ResponsePollingInterval);

				// check if the due time has been changed
				if (dueTime != EstimatedResponseTime)
				{
					dueTime = EstimatedResponseTime;
					Thread.Sleep(dueTime);
				}

			}
		}

		/// <summary>
		/// Gets the interval at which the 
		/// client will poll for a response
		/// after enqueueing the current request.
		/// </summary>
		public TimeSpan ResponsePollingInterval
		{
			get
			{
				return _responsePollingInterval;
			}
			set
			{
				_responsePollingInterval = value;
			}
		}

		/// <summary>
		/// Gets the time interval after which a response
		/// is expected.
		/// </summary>
		public TimeSpan EstimatedResponseTime
		{
			get
			{
				return _responseDueTime;
			}
			set
			{
				_responseDueTime = value;
			}
		}

		/// <summary>
		/// Gets the <see cref="BatchExecutionResponse"/>
		/// </summary>
		public BatchExecutionResponse Response
		{
			get
			{
				return _response;
			}
		}
	}
}
