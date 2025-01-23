// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// An enumeration that defines the different valyes for
	/// batch restart behaviour.
	/// </summary>
	public enum BatchRestartBehavior : byte
	{
		/// <summary>
		/// Fulll
		/// </summary>
		Full						= 0,
		/// <summary>
		/// RestartAtFailedJob
		/// </summary>
		RestartAtFailedJob			= 1,
		/// <summary>
		/// SkipFailedJobAndContinue
		/// </summary>
		SkipFailedJobAndContinue	= 2
	}
}
