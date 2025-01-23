using System;

namespace Avanade.ACA.Batch.BatchMonitor
{
	/// <summary>
	/// Summary description for IRequestListControl.
	/// </summary>
	public interface IRequestListControl
	{
		void Refresh(Guid key);
	}
}
