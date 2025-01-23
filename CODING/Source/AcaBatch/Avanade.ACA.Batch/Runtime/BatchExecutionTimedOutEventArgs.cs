// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// Encapsulates the arguments passed to the Timing Out Event that
	/// is raised when a batch request times out its allocated execution time period.
	/// </summary>
	public class TimingOutEventArgs : EventArgs
	{
		private TimeSpan _extendedTimeoutPeriod;

        /// <summary>
        /// Creates a new instance of the <see cref="TimingOutEventArgs"/>class.
        /// </summary>
		public TimingOutEventArgs()
		{
			_extendedTimeoutPeriod = TimeSpan.Zero;
		}

		/// <summary>
		/// Gets the cumulative time period by which the timeout period
		/// should be extended as requested by subsribers of the timing out event.
		/// </summary>
		public TimeSpan ExtendedTimeoutPeriod
		{
			get
			{
				return _extendedTimeoutPeriod;
			}
			set
			{
				_extendedTimeoutPeriod = value;
			}
		}

	}
}
