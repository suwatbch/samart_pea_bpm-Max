// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Avanade.ACA.Batch.BatchCenterServerHost
{
	/// <summary>
	/// The installer for installing the ACA Batch Server Host Service.
	/// </summary>
	[RunInstaller(true)]
	public class ProjectInstaller : Installer
	{
		private const string ACA_BATCH_SERVER	= "ACA.NET Batch Center Server 4.1";

		private System.ServiceProcess.ServiceProcessInstaller _serviceProcessInstaller;
		private System.ServiceProcess.ServiceInstaller _installer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectInstaller"/> class.
        /// </summary>
		public ProjectInstaller()
		{
			_serviceProcessInstaller	= new ServiceProcessInstaller();
			_installer					= new ServiceInstaller();

            _serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
			_serviceProcessInstaller.Password = null;
			_serviceProcessInstaller.Username = null;

			_installer.DisplayName = ACA_BATCH_SERVER;
			_installer.ServiceName = ACA_BATCH_SERVER;
			_installer.StartType = ServiceStartMode.Automatic;


			Installers.Add(_serviceProcessInstaller);
			Installers.Add(_installer);
		}

	}
}
