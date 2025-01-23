// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Threading;

using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation;

namespace Avanade.ACA.Batch.Instrumentation
{
	/// <summary>
	/// Summary description for BatchExecutionHostStartedEvent.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class BatchExecutionHostStartedEvent : BatchArchitectureEvent
	{
        		#region Public Properties
		/// <summary>
		/// 
		/// </summary>
		public string ApplicationBase
		{
			get
			{
				return AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string ApplicationName
		{
			get
			{
				return AppDomain.CurrentDomain.SetupInformation.ApplicationName;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string ConfigurationFile
		{
			get
			{
				return AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
			}
		}
		#endregion
	}
}
