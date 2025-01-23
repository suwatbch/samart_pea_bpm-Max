// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// Represents the abstract class to be inherited from by a component
	/// that executes a <see cref="BatchExecutionRequest"/>
	/// in an isolated environment.
	/// </summary>
	abstract public class BatchExecutionIsolationStrategy
	{
		private delegate 
			void ExecuteDelegate(/*DequeuedBatchExecutionRequest request*/Guid requestKey,
			string configurationFilePath);

        /// <summary>
        /// Creates a new instance of the 
        /// <see cref="BatchExecutionIsolationStrategy"/> class.
        /// </summary>
		public BatchExecutionIsolationStrategy()
		{
		}

        /// <summary>
        /// Executes the specified request asynchronously.
        /// </summary>
        /// <param name="requestKey">the key for the request object to execute.</param>
        /// <param name="configurationFilePath">The path of the configuration file for the BatchExecutionHost.</param>
        /// <param name="callback">
        /// The delegate to call when the asynchronous invoke is complete. 
        /// If callback is a null reference (Nothing in Visual Basic), the delegate is not called. 
        /// Can be <b>null</b> 
        /// (<b>Nothing</b> in Visual Basic).</param>
        /// <param name="state">Extra information supplied by the caller. 
        /// Can be <b>null</b> 
        /// (<b>Nothing</b> in Visual Basic).</param>
        /// <returns> An <see cref="IAsyncResult"/>
        /// for tracking when execution has completed.</returns>
		public IAsyncResult ExecuteAsync(/*DequeuedBatchExecutionRequest request*/Guid requestKey, 
										string configurationFilePath,
										AsyncCallback callback, 
										object state)
		{
			ExecuteDelegate execute = new ExecuteDelegate(Execute);
			
			IAsyncResult result = 
				execute.BeginInvoke(requestKey, configurationFilePath, callback, state);
			
			return result;
		}

        /// <summary>
        /// Processes the request in an environment with
        /// some level of isolation. 
        /// </summary>
        /// <param name="requestKey">the key for the request to execute</param>
        /// <param name="configurationFilePath">The path of the configuration file for the BatchExecutionHost.</param>
        /// <remarks>This is a template method. It wraps
        /// logging around the actual implementation.</remarks>
        /// <returns>void</returns>
		public void Execute(Guid requestKey,
			string configurationFilePath)
		{

			ExecuteImpl(requestKey, configurationFilePath);
		}

        /// <summary>
        /// Derived class specific implementation of the strategy. Derived
        /// classes must provide an implementation for this method.
        /// </summary>
        /// <param name="requestKey">the key for the request to execute</param>
        /// <param name="configurationFilePath">The path of the configuration file for the BatchExecutionHost.</param>
        /// <returns>void</returns>
		abstract protected void ExecuteImpl(/*DequeuedBatchExecutionRequest request*/Guid requestKey,
			string configurationFilePath);

	}
}
