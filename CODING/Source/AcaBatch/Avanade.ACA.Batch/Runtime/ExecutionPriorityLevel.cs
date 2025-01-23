// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// Enumeration of operating-system execution priorities
	/// </summary>
	public enum ExecutionPriorityLevel : byte
	{
		/// <summary>
		/// Lowest
		/// </summary>
		Lowest		= 0,
		/// <summary>
		/// Low
		/// </summary>
		Low			= 5,
		/// <summary>
		/// BelowNormal
		/// </summary>
		BelowNormal	= 10,
		/// <summary>
		/// Normal
		/// </summary>
		Normal		= 15,
		/// <summary>
		/// AboveNormal
		/// </summary>
		AboveNormal	= 20,
		/// <summary>
		/// High
		/// </summary>
		High		= 25,
		/// <summary>
		/// Highest
		/// </summary>
		Highest		= 30
	}
}
