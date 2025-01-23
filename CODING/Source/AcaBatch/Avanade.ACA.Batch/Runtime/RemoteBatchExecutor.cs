// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using Avanade.ACA.Batch.Instrumentation;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// Represents a remotable object that
	/// enables sending and processing requests
	/// across <see cref="AppDomain"/> boundaries.
	/// </summary>
	public class RemoteBatchExecutor : MarshalByRefObject
	{
        /// <summary>
        /// Executes the the specified request in
        /// the <see cref="System.Threading.ThreadPool"/>
        /// of the current object's AppDomain.
        /// </summary>
        /// <param name="requestKey">The key for the dequeued batch execution request.</param>
        /// <param name="configurationFilePath">The name of the configuration file for the request.</param>
        /// <returns>void</returns>
		public void Execute(Guid requestKey,
			string configurationFilePath)
		{
			BatchArchitectureEvent.AppendExecutionLog(
				"RemoteBatchExecutor.Execute invoked.");

			ThreadIsolationStrategy isolation = new ThreadIsolationStrategy();
			isolation.Execute(requestKey, configurationFilePath);
		}

        /// <summary>
        /// Executes the the specified request in
        /// the <see cref="System.Threading.ThreadPool"/>
        /// of the current object's AppDomain asynchronously.
        /// </summary>
        /// <param name="requestKey">The key for the dequeued batch execution request.</param>
        /// <param name="configurationFilePath">The name of the configuration file for the request.</param>
        /// <returns>void</returns>
		public void ExecuteAsync(Guid requestKey,
			string configurationFilePath)
		{
			ThreadIsolationStrategy isolation = new ThreadIsolationStrategy();
			isolation.ExecuteAsync(requestKey, configurationFilePath, null, null);
		}

        /// <summary>
        /// Initiazes the lifetime.
        /// </summary>
        /// <returns>object</returns>
		public override object InitializeLifetimeService()
		{
			// give it an infinite lifetime
			return null;
		}
	}
}
