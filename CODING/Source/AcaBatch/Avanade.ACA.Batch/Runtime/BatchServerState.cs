// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// Enumeration of the states of a Batch Server
	/// </summary>
	public enum BatchServerState
	{
		/// <summary>
		/// The Batch Server has been initialized.
		/// </summary>
		Initialized,
		/// <summary>
		/// The Batch Server is stopped.
		/// </summary>
		Stopped,
		/// <summary>
		/// The Batch Server is running.
		/// </summary>
		Started,
		/// <summary>
		/// The Batch Server has been paused.
		/// </summary>
		Paused
	}
}
