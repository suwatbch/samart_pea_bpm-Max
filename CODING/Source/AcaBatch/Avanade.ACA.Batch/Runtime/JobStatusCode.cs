// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// Status Codes for a job execution
	/// </summary>
	public enum StatusCode : byte
	{
		/// <summary>
		/// Indicates that the job failed
		/// and that the batch should
		/// not process any remaining
		/// jobs.
		/// </summary>
		Failed = 0,
		/// <summary>
		/// Indicates that an unhandled exception
		/// occurred while executing the job
		/// and that the batch should not
		/// process any remaining jobs.
		/// </summary>
		FailedException = 5,
		/// <summary>
		/// Indicates that the job
		/// failed but that the batch
		/// should continue executing
		/// the remaining jobs.
		/// </summary>
		FailedContinue = 10,
		/// <summary>
		/// Indicates that the job
		/// has not yet begun to execute.
		/// </summary>
		New = 20,
		/// <summary>
		/// Indicates that the job is
		/// executing.
		/// </summary>
		Executing = 30,
		/// <summary>
		/// Indicates that the job has been
		/// paused in the middle of its 
		/// execution.
		/// </summary>
		Paused = 40,
		/// <summary>
		/// Indicates that the job has been
		/// terminated in the middle of its
		/// execution.
		/// </summary>
		Terminated = 50,
		/// <summary>
		/// Indicates that the job
		/// executed successfully.
		/// </summary>
		Success = 60
	}
}
