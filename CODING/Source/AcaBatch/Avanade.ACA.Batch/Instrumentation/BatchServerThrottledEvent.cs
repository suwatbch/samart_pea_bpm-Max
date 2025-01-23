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
	/// Summary description for BatchServerThrottledEvent.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class BatchServerThrottledEvent : BatchArchitectureEvent
	{         
        #region Static Members
        private static BatchServerThrottledEvent	Ev;
        private static ReaderWriterLock				Lock;
        /// <summary>BatchServerThrottledEvent</summary>
        static BatchServerThrottledEvent()
        {
            string[] counterNames = new string[] { string.Empty };         
            Ev = new BatchServerThrottledEvent();
            Lock = new ReaderWriterLock();
        }

        /// <summary>SetThrottled</summary>
        /// <returns>void</returns>
        public static void SetThrottled()
        {
            Ev.SetThrottle();
        }

        /// <summary>ClearThrottled</summary>
        /// <returns>void</returns>
        public static void ClearThrottled()
        {
            Ev.ClearThrottle();
        }
        #endregion

		private bool _isThrottled;

        /// <summary>SetThrottle</summary>
        /// <returns>void</returns>
        private void SetThrottle()
		{
            // TODO: Provide new implementation
            //Lock.AcquireWriterLock(-1);
            //_isThrottled = true;
            //InstrumentedEvent.FireWmiEvent(this);	// fire WMI event
            //Lock.ReleaseWriterLock();

            //if (_throttledCounter != null)
            //{
            //    _throttledCounter.Increment();
            //}
		}

        /// <summary>ClearThrottle</summary>
        /// <returns>void</returns>
        private void ClearThrottle()
		{
            // TODO: Provide new implementation
            //Lock.AcquireWriterLock(-1);
            //_isThrottled = false;
            //InstrumentedEvent.FireWmiEvent(this);	// fire WMI event
            //Lock.ReleaseWriterLock();
            //if (_throttledCounter != null)
            //{
            //    _throttledCounter.Decrement();
            //}
		}


		/// <summary>
		/// 
		/// </summary>
		public bool IsThrottled
		{
			get
			{
				return _isThrottled;
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
				return AppDomain.CurrentDomain.SetupInformation.ApplicationName;
			}
		}
	}
}



