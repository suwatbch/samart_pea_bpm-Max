// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Management.Instrumentation;
using Avanade.ACA.Batch.Instrumentation;

[assembly:Instrumented("root/ACABATCH")]

namespace Avanade.ACA.Batch.Instrumentation
{
	/// <summary>
	/// Summary description for ProjectInstaller.
	/// </summary>
	[RunInstaller(true)]
	public class ProjectInstaller : DefaultManagementProjectInstaller
	{
        /// <summary>AddPerformanceCountersToInstaller</summary>
        /// <returns>void</returns>
        private void AddPerformanceCountersToInstaller()
		{
			// The categoryInstaller is for one performance category only.
			// We have only one category per assembly
			PerformanceCounterInstaller categoryInstaller =	new PerformanceCounterInstaller();
			categoryInstaller.CategoryName = BatchArchitectureEvent.CounterCategory;
			categoryInstaller.CategoryHelp = BatchArchitectureEvent.CounterCategoryHelp;
			Installers.Add(categoryInstaller);
		}
        /// <summary>
        /// override install method
        /// </summary>
        /// <param name="stateSaver">State Saver</param>
        /// <returns>void</returns>
		public override void Install(IDictionary stateSaver)
		{
			// Delete the counter category if already exists
			if (PerformanceCounterCategory.Exists(BatchArchitectureEvent.CounterCategory))
			{
				PerformanceCounterCategory.Delete(BatchArchitectureEvent.CounterCategory);
			}
			AddPerformanceCountersToInstaller();
			base.Install (stateSaver);
		}
        /// <summary>
        /// override uninstall
        /// </summary>
        /// <param name="savedState">Saved State</param>
        /// <returns>void</returns>
		public override void Uninstall(IDictionary savedState)
		{
			if (PerformanceCounterCategory.Exists(BatchArchitectureEvent.CounterCategory))
			{
				AddPerformanceCountersToInstaller();
			}
			base.Uninstall (savedState);
		}

        /// <summary>ProjectInstaller</summary>
		public ProjectInstaller()
		{
			EventLogInstaller eventlogInstaller = new EventLogInstaller();
			eventlogInstaller.Log = "Application";
			eventlogInstaller.Source = BatchArchitectureEvent.EventSource;
			Installers.Add(eventlogInstaller);
		}
	}
}
