// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Diagnostics;
using System.Threading;

using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation;

namespace Avanade.ACA.Batch.Instrumentation
{
	/// <summary>
	/// Summary description for BatchArchitectureConfigFailureEvent.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class BatchArchitectureConfigFailureEvent : BatchArchitectureFailureEvent
	{
        private new string _message;
        /// <summary>BatchArchitectureConfigFailureEvent</summary>
        /// <param name="message">System.String</param>
        /// <param name="exception">System.Exception</param>
        public BatchArchitectureConfigFailureEvent(string message, Exception exception) 
            : base (message, exception)
        {
        }

		/// <summary>
		/// The path of the pricipal configuration file.
		/// </summary>
		public string ConfigurationFilePath
		{
			get
			{
				return AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
			}
		}		
	}
}

