// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// Represents the interface that components that create an instance
	/// of <see cref="BatchExecutionContext"/> must implement.
	/// </summary>
	public interface IBatchExecutionManager
	{

        /// <summary>
        /// Creates and returns an instance of <see cref="BatchExecutionContext"/>
        /// based on the inout <see cref="BatchExecutionRequest"/> object.
        /// </summary>
        /// <param name="request">Avanade.ACA.Batch.BatchExecutionRequest</param>
        /// <returns>Avanade.ACA.Batch.BatchExecutionContext</returns>
		BatchExecutionContext 
			CreateBatchExecutionContext(BatchExecutionRequest request);
	}
}
