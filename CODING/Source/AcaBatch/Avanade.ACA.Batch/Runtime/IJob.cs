// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// Represents the interface that a component that executes 
	/// a Job must implement.
	/// </summary>
	public interface IJob
	{
        /// <summary>
        /// Executes the job.
        /// </summary>
        /// <param name="context">The context associated with the job.</param>
        /// <returns>void</returns>
		void Execute(JobExecutionContext context);
	}
}
