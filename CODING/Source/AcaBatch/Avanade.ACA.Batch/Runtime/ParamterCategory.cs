// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// An enumeration of parameter categories
	/// </summary>
	public enum ParameterCategory
	{
		/// <summary>
		/// Unknown category parameters.
		/// </summary>
		Unknown = 0,
		/// <summary>
		/// Job Definition parameters.
		/// </summary>
		JobDefinition = 1,
		/// <summary>
		/// Batch Definition parameters.
		/// </summary>
		BatchDefinition = 2,
		/// <summary>
		/// Original Request parameters; including the resolved Request Command parameters over
		/// Batch definition Parameters.
		/// </summary>
		RequestOriginal = 3,
		/// <summary>
		/// The last committed Request parameters in BatchExecutionContext.
		/// </summary>
		BatchExecutionContextCurrent = 4,
		/// <summary>
		/// Original JobExecutionContext parameters.
		/// </summary>
		JobExecutionContextOriginal = 5,
		/// <summary>
		/// The Request parameters in BatchExecutionContext at the end of executing the last succeeded job.
		/// </summary>
		BatchExecutionContextEndOfSucceededJob = 6,
		/// <summary>
		/// The last committed JobExecutionContext parameters.
		/// </summary>
		JobExecutionContextCurrent = 7,
	}
}
