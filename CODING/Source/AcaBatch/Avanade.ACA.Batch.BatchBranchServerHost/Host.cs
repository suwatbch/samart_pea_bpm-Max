// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.ServiceProcess;
using System.Diagnostics;

using Avanade.ACA.Batch.Instrumentation;
using Avanade.ACA.Batch;

namespace Avanade.ACA.Batch.BatchBranchServerHost
{
	/// <summary>
	/// The driver for the running the windows service
	/// </summary>
	class Host
	{
		private const string ACA_BATCH_ARCHITECTURE  = 
				"ACA.NET Batch Architecture";
		private const string LINE_SEPARATOR			 = 
				"-----------------------------------\n";

        /// <summary>
        /// The main entry point for the process
        /// </summary>
        /// <returns>void</returns>
 		static void Main()
		{
			try
			{
				RunService();
			}
			catch (ACABatchException)
			{
			}
			catch (Exception e)
			{
                BatchInstrumentationProvider.Instance.FireBatchArchitectureFailedEvent("Batch Server service failed.", e);				
			}
		}

        /// <summary>RunService</summary>
        /// <returns>void</returns>
        private static void RunService()
		{
			// Create a Service that will run the Batch Server
			BatchServerService service = new BatchServerService();
			// Run the service
			ServiceBase.Run(service);
		}
	}
}
