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
	/// Summary description for BatchRequestEvent.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class BatchRequestEvent : BatchArchitectureEvent
	{
		/// <summary>
		/// The message for the event.
		/// </summary>
		[CLSCompliantAttribute(false)]
		protected Guid						_requestKey;

		/// <summary>
		/// 
		/// </summary>
		[CLSCompliantAttribute(false)]
		protected string					_requestName;

		/// <summary>
		/// 
		/// </summary>
		[CLSCompliantAttribute(false)]
		protected string					_status;

        /// <summary>BatchRequestEvent</summary>
        /// <param name="requestKey">System.Guid</param>
        /// <param name="requestName">System.String</param>
        /// <param name="status">System.String</param>
        public BatchRequestEvent(Guid requestKey, string requestName, string status)            
        {            
            _requestKey = requestKey;
            _requestName = requestName;
            _status = status;
        }

		#region Public Properties
		/// <summary>
		/// 
		/// </summary>
		public string RequestName
		{
			get
			{
				return _requestName;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string RequestKey
		{
			get
			{
				return _requestKey.ToString();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string Status
		{
			get
			{
				return _status;
			}
		}
		#endregion
	}
}

