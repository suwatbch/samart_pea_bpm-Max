// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System.EnterpriseServices;

namespace Avanade.ACA.Batch
{

	/// <summary>
	///	Represents the transaction context for work units that are
	/// part of the same commit.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(true)]
	[System.Runtime.InteropServices.GuidAttribute("E6918C88-3EAA-4eb5-A4AA-439B507CAD63")]	
	[Transaction(TransactionOption.RequiresNew, Timeout=0)]
	public class WorkUnitsTransaction : ServicedComponent
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkUnitsTransaction"/> class.
        /// </summary>
		public WorkUnitsTransaction()
		{
		}

        /// <summary>
        /// Executes work units in a loop. All work units invoked inside
        /// this share a distributed transaction context. The loop exits
        /// after a Commit operation takes place, either because
        /// the commit frequency was reached or the work unit forced
        /// a commit.
        /// </summary>
        /// <param name="job">Avanade.ACA.Batch.UnitizedJob</param>
        /// <returns>Avanade.ACA.Batch.StatusCode</returns>
		public virtual StatusCode Execute(UnitizedJob job)
		{
			StatusCode status = StatusCode.Failed;

			// Effectively make this a SingleCall component
			ContextUtil.DeactivateOnReturn = true;

			// Initial vote is that the the transaction 
			// will rollback. This handles any "Failed" status
			// codes and unhandled exceptions. This vote will get
			// overriden if the status code is "Succeeded" or "Executing"
			ContextUtil.MyTransactionVote = TransactionVote.Abort;

			status = job.ProcessWorkUnits();

			if (status == StatusCode.Executing ||
				status == StatusCode.Success)
			{
				// change the tranasaction vote to commit
				ContextUtil.MyTransactionVote = TransactionVote.Commit;
			}

			return status;
		}
	}
}
