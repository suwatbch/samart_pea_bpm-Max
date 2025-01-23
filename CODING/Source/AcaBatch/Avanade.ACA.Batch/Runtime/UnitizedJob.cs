// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using Avanade.ACA.Batch.Configuration;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// Represents a job that is separated into 
	/// discrete units of work.
	/// </summary>
	abstract public class UnitizedJob : Job
	{
		/// <summary>
		/// The name of the Job parameter that indicates if current
		/// UnitizedJob is transactional or not. If this key
		/// is not present in the job parameters collection then the 
		/// default value is false.
		/// </summary>
		/// <value>The string "UseDistributedTransaction".</value>
		public const string UseDistributedTransactionParameterName = 
				"UseDistributedTransaction";
		/// <summary>
		/// The name of the Job parameter that indicates if current
		/// UnitizedJob will automatically begin a transaction to
		/// by shared by work units within the same commit sequence. 
		/// If this key is not present in the job parameters 
		/// collection then the default value is true.
		/// </summary>
		/// <value>The string "AutoBeginTransaction".</value>
		public const string AutoBeginTransactionParameterName = 
				"AutoBeginTransaction";

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitizedJob"/> class.
        /// </summary>
		public UnitizedJob()
		{
		}

        /// <summary>
        /// Overriden. Loops through all the logical
        /// units of work, incrementing the work unit
        /// count and commiting the context per the 
        /// configured commit frequency.
        /// </summary>
        /// <returns>Avanade.ACA.Batch.StatusCode</returns>
		protected override StatusCode ExecuteImpl()
		{
			StatusCode status = StatusCode.Executing;

			while (status == StatusCode.Executing)
			{
				if (UseDistributedTransaction)
				{
					status = ProcessWorkUnitsInDistributedTransaction();
				}
				else
				{
					status = ProcessWorkUnits();
				}
			}

			return status;
		}

        /// <summary>ProcessWorkUnitsInDistributedTransaction</summary>
        /// <returns>Avanade.ACA.Batch.StatusCode</returns>
        private StatusCode ProcessWorkUnitsInDistributedTransaction()
		{
			WorkUnitsTransaction transaction = null;
			StatusCode status;
			try
			{
				// begin a new transaction
				transaction = CreateWorkUnitsTransaction();

				// enter the transaction context. All work units
				// will execute inside the transaction boundary. Execution
				// will leave the transaction boundary after a commit or 
				// an exception
				status = transaction.Execute(this);

				// if the status returned from the last Work Unit 
				// is 'Executing', then there are more work units.
				// continue processing in a new transaction context.
			}
			finally
			{
				if (transaction != null)
				{
					transaction.Dispose();
				}
			}

			if (Context.BatchExecutionContext.BatchExecutionContextData.ToPause)
			{
				Context.BatchExecutionContext.Pause();
			}

			return status;
		}

        /// <summary>
        /// Creates a <see cref="WorkUnitsTransaction"/> object, which is a 
        /// serviced component that manages the distributed transaction
        /// for work unit processing. This method is overridable so that
        /// custom classes with custom COM+ properties 
        /// deriving from <see cref="WorkUnitsTransaction"/>
        /// can be returned.
        /// </summary>
        /// <returns>An instance of the <see cref="WorkUnitsTransaction"/>
        /// class or an instance of a class that inherits from 
        /// <see cref="WorkUnitsTransaction"/>.</returns>
		protected virtual WorkUnitsTransaction CreateWorkUnitsTransaction()
		{
			return new WorkUnitsTransaction();
		}

        /// <summary>
        /// This method repeatedly invokes the <see cref="DoWorkUnit"/>
        /// method. It returns when the commit frequency is reached, 
        /// when the force commit indicator is true, or when
        /// the status code returned is a value other than
        /// 'Executing'.
        /// </summary>
        /// <returns>Avanade.ACA.Batch.StatusCode</returns>
		protected internal virtual StatusCode ProcessWorkUnits()
		{
			try
			{
				if (AutoBeginTransaction)
				{
					BatchExecutionContext batchContext = Context.BatchExecutionContext;

					if (batchContext.TransactionState == DbTransactionState.Empty
						|| batchContext.TransactionState == DbTransactionState.Finished)
					{
						batchContext.BeginTransaction();
					}
				}

				// loop until either enough work units have
				// been executed to cause a commit, or 
				// till the DoWorkUnit operation forces a commit
				while (true)
				{
					// set initial values 'forceCommit' flag.
					bool forceCommit = false;

					Context.JobExecutionContextData.SetWorkUnitStart();
					// invoke the DoWorkUnit method
					StatusCode status = DoWorkUnit(ref forceCommit);
				
					// If the status is 'Executing' then increment the 
					// WorkUnitCount and continue. Otherwise, break out of the 
					// loop and return control to the job
					if (status == StatusCode.Executing)
					{
						// increment the work unit count and commit if the 
						// commit frequency is reached
						Context.JobExecutionContextData.IncrementWorkUnitCount();
						
						long remainder = 
							Context.WorkUnitCount % Context.CommitFrequency;

						// check the work unit count against the commit frequency
						if (forceCommit || remainder == 0)
						{
							Commit();
							return StatusCode.Executing;
						}
						else
						{
							continue;
						}
					}
					else if (status == StatusCode.Success)
					{
						Context.JobExecutionContextData.IncrementWorkUnitCount();
						Commit();
						// there are not more work units to execute. 
						// Break out of the loop and return the status.
						return status;
					}
					else if (status == StatusCode.Failed
						|| status == StatusCode.FailedContinue
						|| status == StatusCode.FailedException)
					{
						HandleFailure();
						return status;
					}
				}
			}
			catch
			{
				HandleFailure();
				// let the exception bubble up so the transaction is rolled back.
				throw;
			}
		}

        /// <summary>
        /// This method is invoked when the commit frequency is reached, when
        /// the force commit indicator is false and when the status code
        /// returned from the work unit is 'Success'
        /// </summary>
        /// <returns>void</returns>
		protected virtual void Commit()
		{
			Context.Commit();
			Context.BatchExecutionContext.DisposeTransaction();
			DateTime now = DateTime.Now;
			int newCount = Context.JobExecutionContextData.WorkUnitCount;
		}

        /// <summary>HandleFailure</summary>
        /// <returns>void</returns>
        private void HandleFailure()
		{
			if (Context.BatchExecutionContext.TransactionState == 
				DbTransactionState.Started)
			{
				Context.BatchExecutionContext.Transaction.Rollback();
			}
			Context.BatchExecutionContext.DisposeTransaction();
			// Lock the Context so it can be read but not
			// committed
			Context.Lock();
		}

		/// <summary>
		/// Gets or sets a value indicating if a set of work units that are commited
		/// together share a transaction context managed by COM+ and the
		/// DTC.
		/// </summary>
		/// <value>True to use a distributed transaction context, otherwise
		/// false.</value>
		public virtual bool UseDistributedTransaction
		{
			get
			{
				// check the configured value of the pre-defined
				// "UseDistributedTransaction" parameter
				ParameterData transactionParameter = 
					JobParameters[UseDistributedTransactionParameterName];

				if (transactionParameter == null)
				{
					UseDistributedTransaction = false;
					return false;
				}
										
				return (bool)transactionParameter.Value;
			}
			set
			{
				JobParameters.SetPrimitiveData(UseDistributedTransactionParameterName, value);
			}
		}

		/// <summary>
		/// Gets or sets a value indicating if starting a 
		/// new transaction for the set of work
		/// units is handled automatically in the 
		/// base class.
		/// </summary>
		/// <value>True if the base class will automatically
		/// begin a transaction, otherwise false.</value>
		public virtual bool AutoBeginTransaction
		{
			get
			{
				// check the configured value of the pre-defined
				// "AutoBeginTransaction" parameter
				ParameterData autoParameter = 
					JobParameters[AutoBeginTransactionParameterName];

				if (autoParameter == null)
				{
					AutoBeginTransaction = true;
					return true;
				}
				return (bool)autoParameter.Value;
			}
			set
			{
				JobParameters.SetPrimitiveData(AutoBeginTransactionParameterName, value);
			}
		}

        /// <summary>
        /// Performs a logical unit of work. 
        /// </summary>
        /// <param name="forceCommit">bool</param>
        /// <returns>Avanade.ACA.Batch.StatusCode</returns>
		abstract protected StatusCode DoWorkUnit(ref bool forceCommit);

	}
}
