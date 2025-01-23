// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
namespace Avanade.ACA.Batch
{
	/// <summary>
	/// Represents the interface that a component that handles
	/// mapping a batch status code to an operating-system
	/// return code must implement.
	/// </summary>
	public interface IProcessExitCodeStrategy
	{
        /// <summary>
        /// Maps a <cref>BatchProcessStatusCode</cref> to a
        /// Win32 Process Exit Code
        /// </summary>
        /// <param name="statusCodes">Avanade.ACA.Batch.BatchProcessStatusCode[]</param>
        /// <returns>int</returns>
		int Map(BatchProcessStatusCode[] statusCodes);
	}
}
