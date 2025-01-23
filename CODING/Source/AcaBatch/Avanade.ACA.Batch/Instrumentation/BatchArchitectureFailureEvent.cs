// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Diagnostics;
using System.Threading;

using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation;
using System.Management.Instrumentation;

namespace Avanade.ACA.Batch.Instrumentation
{
	/// <summary>
	/// Summary description for DataConnectionFailedEvent.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class BatchArchitectureFailureEvent :  BatchArchitectureEvent
	{
		/// <summary>
		/// 
		/// </summary>
		[CLSCompliantAttribute(false)]
		protected Exception				_exception;

		/// <summary>
		/// 
		/// </summary>
		[CLSCompliantAttribute(false)]
		protected string				_exceptionMessage;

		/// <summary>
		/// 
		/// </summary>
		[CLSCompliantAttribute(false)]
		protected string				_message;

        /// <summary>BatchArchitectureFailureEvent</summary>
        /// <param name="message">System.String</param>
        /// <param name="exception">System.Exception</param>

        protected BatchArchitectureFailureEvent(string message, Exception exception)
        {
            _exception = exception;
            _message = message;            
        }
        
		#region Public Properties
		/// <summary>
		/// 
		/// </summary>
		public string ExceptionStackTrace
		{
			get
			{
				return _exception == null ? string.Empty : _exception.StackTrace;
			}
		}

        /// <summary>
        /// 
        /// </summary>
        [IgnoreMember]
        public Exception Exception
        {
            get
            {
                return _exception;
            }
        }

		/// <summary>
		/// 
		/// </summary>
		public string ExceptionMessage
		{
			get
			{
				return _message;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string Message
		{
			get
			{
				return _message;
			}
		}

		#endregion
	}
}


