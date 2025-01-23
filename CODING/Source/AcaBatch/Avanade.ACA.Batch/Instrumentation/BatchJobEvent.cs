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
	public class BatchJobEvent : BatchArchitectureEvent
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
		protected string					_jobName;

		/// <summary>
		/// 
		/// </summary>
		[CLSCompliantAttribute(false)]
		protected string					_status;

		/// <summary>
		/// 
		/// </summary>
		[CLSCompliantAttribute(false)]
		protected int						_sequence;

        /// <summary>BatchJobEvent</summary>
        /// <param name="requestKey">System.Guid</param>
        /// <param name="requestName">System.String</param>
        /// <param name="jobName">System.String</param>
        /// <param name="status">System.String</param>
        /// <param name="sequence">System.Int32</param>
        public BatchJobEvent(Guid requestKey, string requestName, string jobName, string status, int sequence)            
        {            
            _requestKey = requestKey;
            _requestName = requestName;
            _jobName = jobName;
            _status = status;
            _sequence = sequence;
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
		public string JobName
		{
			get
			{
				return _jobName;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public int Sequence
		{
			get
			{
				return _sequence;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string JobStatus
		{
			get
			{
				return _status;
			}
		}

		#endregion
	}
}

