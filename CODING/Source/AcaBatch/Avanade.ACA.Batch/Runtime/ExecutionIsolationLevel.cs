// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// Enumeration of isolation level values
	/// </summary>
	public enum ExecutionIsolationLevel : byte
	{
		/// <summary>
		/// Run the Batch on a new thread
		/// </summary>
		Thread		= 0,
		/// <summary>
		/// Run the Batch in its own process
		/// </summary>
		Process		= 10,
		/// <summary>
		/// Run the Batch in a new application domain
		/// </summary>
		AppDomain	= 20
	}
}
