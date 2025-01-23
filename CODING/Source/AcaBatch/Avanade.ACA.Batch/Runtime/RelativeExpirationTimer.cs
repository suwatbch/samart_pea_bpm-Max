// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Threading;

namespace Avanade.ACA.Batch
{
	[Serializable]
	internal class RelativeExpirationTimer
	{
		private static TimeSpan defaultInterval = TimeSpan.FromSeconds(30);
		private Timer timer;
		private TimerCallback callback;
		private DateTime absoluteExpiration;
		private bool expired;
		private object sync;

        /// <summary>RelativeExpirationTimer</summary>
        /// <param name="callback">System.Threading.TimerCallback</param>
        /// <param name="relativeTimeout">System.TimeSpan</param>
        public RelativeExpirationTimer(TimerCallback callback, TimeSpan relativeTimeout)
            : this(callback, relativeTimeout, defaultInterval)
		{
		}

        /// <summary>RelativeExpirationTimer</summary>
        /// <param name="callback">System.Threading.TimerCallback</param>
        /// <param name="relativeTimeout">System.TimeSpan</param>
        /// <param name="interval">System.TimeSpan</param>
        public RelativeExpirationTimer(TimerCallback callback, TimeSpan relativeTimeout, TimeSpan interval)
		{
			this.sync = new object();
			this.expired = false;
			this.absoluteExpiration = DateTime.Now.Add(relativeTimeout);
			this.callback = callback;
			this.timer = new Timer(new TimerCallback(CheckExpiration), null, interval, interval);
		}

        /// <summary>Change</summary>
        /// <param name="relativeExpiration">System.TimeSpan</param>
        /// <returns>void</returns>
        public void Change(TimeSpan relativeExpiration)
		{
			lock (sync)
			{
				this.expired = false;
				this.absoluteExpiration = DateTime.Now.Add(relativeExpiration);	
			}
		}

        /// <summary>Dispose</summary>
        /// <returns>void</returns>
        public void Dispose()
		{
			lock (sync)
			{
				this.expired = true;
				this.timer.Dispose();
			}
		}

        /// <summary>CheckExpiration</summary>
        /// <param name="state">object</param>
        /// <returns>void</returns>
        private void CheckExpiration(object state)
		{
			lock (sync)
			{
				if (!expired && this.absoluteExpiration < DateTime.Now)
				{
					this.expired = true;
					this.callback(null);
				}
			}
		}
	}
}
