// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// Status codes for the processing 
	/// of a batch execution request
	/// </summary>
	public enum BatchProcessStatusCode : byte
	{
		/// <summary>
		/// The request has been enqueued.
		/// </summary>
		Enqueued = 0,
		/// <summary>
		/// A previously failed request has now been enqueued again.
		/// </summary>
		EnqueuedRestart = 10,
		/// <summary>
		/// The request has been dequeued by a batch server.
		/// </summary>
		Dequeued = 20,
		/// <summary>
		/// The request is currently executing.
		/// </summary>
		Executing = 30,
		/// <summary>
		/// The execution of the request has been paused.
		/// </summary>
		Paused = 32,
		/// <summary>
		/// The request has successfully completed execution.
		/// </summary>
		Succeeded = 40,
		/// <summary>
		/// Execution of the request failed.
		/// </summary>
		Failed = 50,
		/// <summary>
		/// Execution of the request timed out
		/// </summary>
		FailedTimedOut = 51,
		/// <summary>
		/// Execution of a request was cancelled.
		/// </summary>
        FailedCanceled = 55,
        /// <summary>
        /// Execution of a request was cancelled by System because system detected concurrent requests.
        /// </summary>
        FailedSystemCanceled = 56, //By Nick
		/// <summary>
		/// Status of the request is unknown.
		/// </summary>
		Unknown = 100
	}
}
