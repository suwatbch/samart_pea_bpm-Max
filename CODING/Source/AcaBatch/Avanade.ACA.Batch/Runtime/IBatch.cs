// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// Interface that all Batch implementations must implement
	/// </summary>
	public interface IBatch
	{
        /// <summary>
        /// All Batch implementations must implement this method to
        /// handle the execution of the Batch
        /// </summary>
        /// <param name="context">The batch execution context</param>
        /// <returns>void</returns>
		void Execute(BatchExecutionContext context);
	}
}
