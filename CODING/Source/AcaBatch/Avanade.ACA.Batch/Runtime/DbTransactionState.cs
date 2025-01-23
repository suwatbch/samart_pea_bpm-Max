// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// Indicates the state of the current 
	/// <see cref="BatchExecutionContext.Transaction"/> object.
	/// </summary>
	/// <example>
	/// <code>
	/// DbTransactionState state = BatchExecutionContext.TransactionState;
	/// if (state == DbTransactionState.Empty)
	/// {
	///		BatchExecutionContext.BeginTransaction();
	/// }
	/// else if (state == DbTransactionState.Finished)
	/// {
	///		// set the current transaction in the context
	///		// to null, and begin a new transaction.
	///		BatchExecutionContext.DisposeTransaction();
	///		BatchExecutionContext.BeginTransaction();
	/// }
	/// 
	/// DbTransaction transaction = BatchExecutionContext.Transaction;
	/// // use the transaction to execute database commands.
	/// </code>
	/// </example>
	public enum DbTransactionState
	{
		/// <summary>
		/// The transaction object is null.
		/// </summary>
		Empty,
		/// <summary>
		/// The transaction object is not null and it
		/// is active meaning <see cref="System.Data.DbTransaction.Connection"/>
		/// object is open and the transaction has already begun
		/// but has not yet been rolled back or committed.
		/// </summary>
		Started,
		/// <summary>
		/// The transaction object has been either rolled
		/// back or committed.
		/// </summary>
		Finished
	}
}
