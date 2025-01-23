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
	/// Summary description for BatchServerHostEvent.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class BatchServerHostEvent : BatchArchitectureEvent
	{        
        private const  string DefaultServerHostName = "ACA.NET Batch Server";
     
        /// <summary>
		/// 
		/// </summary>
		[CLSCompliantAttribute(false)]
		private string _destination;

		/// <summary>
		/// 
		/// </summary>
		[CLSCompliantAttribute(false)]
		private bool _started;

        /// <summary>BatchServerHostEvent</summary>
        /// <param name="destination">System.String</param>
        /// <param name="started">System.Boolean</param>
        public BatchServerHostEvent(string destination, bool started)
        {
            _destination = destination;
            _started = started;
        }

		#region Public Properties
		/// <summary>
		/// 
		/// </summary>
		public string Destination
		{
			get
			{
				return _destination;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public bool Started
		{
			get
			{
				return _started;
			}
		}

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
				return DefaultServerHostName;
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


