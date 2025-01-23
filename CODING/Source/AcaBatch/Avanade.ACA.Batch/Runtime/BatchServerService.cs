// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================

using System;
using System.ServiceProcess;
using System.Configuration;
using Avanade.ACA.Batch;
using Avanade.ACA.Batch.Configuration;
using Avanade.ACA.Batch.Instrumentation;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// Represents a windows service that hosts
	/// and runs <see cref="BatchServer"/> objects.
	/// </summary>
	public class BatchServerService : ServiceBase
	{
		private const string NO_SERVERS = "No servers are "
											+ "configured for this service";
		private const string BATCH_SERVICE_FAILURE = "Batch Server service failed to start.";

		private const string ACA_BATCH_SERVER	= "ACA.NET Batch Server";

		private BatchServer server;

        /// <summary>
        /// Creates a new instance of the <see cref="BatchServerService"/>class.
        /// </summary>
		public BatchServerService()
		{
			// Set the name of the service
			ServiceName			= ACA_BATCH_SERVER;
			
			// Set the inherited properties
			CanPauseAndContinue	= true;
			CanShutdown			= true;
			AutoLog				= true;
			CanHandlePowerEvent = false;
			
		}

        /// <summary>
        /// Start the service.
        /// </summary>
        /// <param name="args">string[]</param>
        /// <returns>void</returns>
		protected override void OnStart(string[] args)
		{
			BatchSettings batchSettings = (BatchSettings) ConfigurationManager.GetSection(BatchSettings.SectionName);		
			BatchServerSettingsData serverSettings = batchSettings.BatchServerSettings;

			try
			{
				this.server = new BatchServer(serverSettings);
			}
			catch (ACABatchException)
			{
				throw;
			}
			catch (Exception e)
			{
                BatchInstrumentationProvider.Instance.FireBatchArchitectureFailedEvent(BATCH_SERVICE_FAILURE, e);				
				throw new ACABatchFatalException(BATCH_SERVICE_FAILURE, e);
			}
			// error handling logic is in OnContinue()
			this.OnContinue();
		}

        /// <summary>
        /// Stop the service.
        /// </summary>
        /// <returns>void</returns>
		protected override void OnStop()
		{
			try
			{
				// Stop polling the queue for requests
				//foreach (BatchServer server in _servers)
				//{
				server.Stop();
				//}
			}
			catch (ACABatchException)
			{
				throw;
			}
			catch (Exception e)
			{
                BatchInstrumentationProvider.Instance.FireBatchArchitectureFailedEvent(BATCH_SERVICE_FAILURE, e);
				throw new ACABatchFatalException(BATCH_SERVICE_FAILURE, e);
			}
		}

        /// <summary>
        /// Pause the service
        /// </summary>
        /// <returns>void</returns>
		protected override void OnPause()
		{
			OnStop();
		}

        /// <summary>
        /// Resume a paused service
        /// </summary>
        /// <returns>void</returns>
		protected override void OnContinue()
		{
			try
			{
				this.server.Start();
			}
			catch (ACABatchException)
			{
				throw;
			}
			catch (Exception e)
			{
                BatchInstrumentationProvider.Instance.FireBatchArchitectureFailedEvent(BATCH_SERVICE_FAILURE, e);				
				throw new ACABatchFatalException(BATCH_SERVICE_FAILURE, e);
			}
		}

        /// <summary>
        /// System is shutting down: stop the service
        /// </summary>
        /// <returns>void</returns>
		protected override void OnShutdown()
		{
			// Stop polling the queue for requests
			OnStop();
		}
	}
}
