// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// Delete that defines the signature of the TimingOutEventHandler
	/// </summary>
	public delegate void TimingOutEventHandler
		(
			object sender, 
			TimingOutEventArgs args
		);
}
