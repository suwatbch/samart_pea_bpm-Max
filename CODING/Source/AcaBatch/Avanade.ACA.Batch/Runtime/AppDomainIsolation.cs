// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================

using System;
using System.Threading;
using System.IO;

using Avanade.ACA.Batch.Instrumentation;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// An isolated environment for executing <see cref="Batch"/> objects.
	/// This strategy makes sure that <see cref="Batch"/> objects run
	/// isolated in their own application domain.
	/// </summary>
	/// <remarks></remarks>
	public class AppDomainIsolationStrategy : BatchExecutionIsolationStrategy
	{
		#region constants
		private const string BIN		= "bin";
		#endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="AppDomainIsolationStrategy"/> class.
        /// </summary>
		public AppDomainIsolationStrategy()
		{
		}

        /// <summary>
        /// Creates a new <see cref="AppDomain"/> object,
        /// sets its configuration file, 
        /// and processes the request in it.
        /// </summary>
        /// <param name="requestKey">The <see cref="BatchExecutionRequest"/>
        /// to execute.</param>
        /// <param name="configurationFilePath">The path of the configuration file for the Batch execution.</param>
        /// <returns>void</returns>
		protected override void ExecuteImpl(
			Guid requestKey, string configurationFilePath)
		{
			WaitHandle	handle = null;
			AppDomain	domain = null;

			try
			{
				BatchArchitectureEvent.AppendExecutionLog(
							"AppDomainIsolationStrategy.ExecuteImpl invoked.");

				string friendlyName = requestKey.ToString();
				Type remotableType	= typeof(RemoteBatchExecutor);
				string assemblyName = GetType().Assembly.FullName;
				string typeName		= remotableType.FullName;

				// Create the new AppDomain
				AppDomainSetup current = AppDomain.CurrentDomain.SetupInformation;
				AppDomainSetup setup = new AppDomainSetup();
			
				setup.ApplicationBase = current.ApplicationBase;
				setup.PrivateBinPath = BIN;

				if (configurationFilePath != String.Empty)
				{
					FileInfo info = new FileInfo(configurationFilePath);
					setup.ConfigurationFile = info.FullName;
					setup.ApplicationName = "BatchExecutionHost_" + requestKey.ToString();
					setup.DynamicBase = info.Directory.FullName;
				}
				else
				{
					setup.ConfigurationFile = current.ConfigurationFile;					
				}

				domain = AppDomain.CreateDomain(friendlyName, 
					AppDomain.CurrentDomain.Evidence, 
					setup);

				BatchArchitectureEvent.AppendExecutionLog(
					"New AppDomain created.");
			
				// Create an executor object in the new AppDomain
				RemoteBatchExecutor executor = (RemoteBatchExecutor)
					domain.CreateInstanceAndUnwrap(assemblyName,
					typeName);
			
				// Process the Batch in the new AppDomain
				executor.Execute(requestKey, configurationFilePath);
			}		
			finally
			{
				if (handle != null)
				{
					handle.Close();
				}
				if (domain != null)
				{
					AppDomain.Unload(domain);
				}
			}
		}
		
	}
}
