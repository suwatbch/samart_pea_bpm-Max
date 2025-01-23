// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// A simple exit code mapping strategy that maps 
	/// BatchProcessStatusCode.Succeeded to zero and
	/// any other BatchProcessStatusCode to one
	/// </summary>
	public class DefaultProcessExitCodeStrategy : IProcessExitCodeStrategy
	{
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="DefaultProcessExitCodeStrategy"/> class.
        /// </summary>
		public DefaultProcessExitCodeStrategy()
		{
		}

        /// <summary>Map</summary>
        /// <param name="statusCodes">Avanade.ACA.Batch.BatchProcessStatusCode[]</param>
        /// <returns>int</returns>
		public virtual int Map(BatchProcessStatusCode[] statusCodes)
		{
			// If all commands succeeded return 0 to the calling process
			int exitCode = 0;

			if (statusCodes == null)
			{
				exitCode = -1;
			}
			else
			{
				foreach (BatchProcessStatusCode status in statusCodes)
				{
					if (status != BatchProcessStatusCode.Succeeded)
					{
						switch(status)
						{
							case BatchProcessStatusCode.FailedCanceled:
							{
								exitCode = 3;
								break;
							}
							case BatchProcessStatusCode.FailedTimedOut:
							{
								exitCode = 2;
								break;
							}

							case BatchProcessStatusCode.Failed:
							default:
							{
								exitCode = 1;
								break;
							}
						}

						if (exitCode != 0)
						{
							break;
						}
					}
				}
			}

			return exitCode;
		}
	}
}
