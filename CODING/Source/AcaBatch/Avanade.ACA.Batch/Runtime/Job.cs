// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using Avanade.ACA.Batch.Configuration;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// Represents a template for 
	/// executing a job.
	/// </summary>
	abstract public class Job : IJob
	{
		private JobExecutionContext _context;

        /// <summary>
        /// The default constructor. 
        /// </summary>
		public Job()
		{
		}

        /// <summary>
        /// Executes the job by calling primitive operations
        /// implemented in the derived class.
        /// </summary>
        /// <param name="context">Avanade.ACA.Batch.JobExecutionContext</param>
        /// <returns>void</returns>
		public virtual void Execute(JobExecutionContext context)
		{
			_context = context;

			EventHandler onPausing = new EventHandler(OnPausing);
			EventHandler onResuming = new EventHandler(OnResuming);
			// subscribe to the Batch Pausing event
			_context.BatchExecutionContext.Pausing += onPausing;
			// subscribe to the Batch Resuming event
			_context.BatchExecutionContext.Resuming += onResuming;
			
			try
			{
				// change the status to Executing
				_context.Status = StatusCode.Executing;
				_context.UpdateStatus();
				
				// call the common start routine
				Setup();

				// check if this is a restart and
				// if the job is configured to support
				// an intelligent restart.
				if (_context.BatchExecutionContext.IsARestart
					&& _context.RestartBehavior != JobRestartBehavior.Full)
				{
					SetupForRestart();
				}
				else
				{
					SetupForStart();
				}
				
				// run the main execution
				_context.Status = ExecuteImpl();
			}
			catch (Exception e)
			{
				// lock the context so that the in-memory values
				// won't be saved to the database
				_context.Lock();
				// change the status to FailedException
				_context.Status = StatusCode.FailedException;
				_context.UpdateStatus();
				_context.AddException(e);
			}
			finally
			{
				_context.Save();
			}

			// Invoke the clean up logic in a separate
			// try catch block because we don't want 
			// unhandled exceptions during clean up
			// to affect the status of the execution.
			try
			{
				if (_context.Status == StatusCode.Success)
				{
					CleanUpAfterSuccess();
					// finally invoke the common clean up logic
					CleanUp();
				}
				else if(_context.Status == StatusCode.Failed
					|| _context.Status == StatusCode.FailedContinue
					|| _context.Status == StatusCode.FailedException)
				{
					CleanUpAfterFailure();
					// finally invoke the common clean up logic
					CleanUp();
				}
			}
			catch (Exception e)
			{
				// if an exception occurs during CleanUp
				// just log it, don't change the status
				context.AddException(e);
			}
			finally
			{
				_context.Save();
				// lock the context
				Context.Lock();
				// un-subscribe from the Batch Pausing event
				_context.BatchExecutionContext.Pausing -= onPausing;
				// un-subscribe from the Batch Resuming event
				_context.BatchExecutionContext.Resuming -= onResuming;
			}
		}

		/// <summary>
		/// Gets the <see cref="JobExecutionContext"/>
		/// for the currently executing job.
		/// </summary>
		public JobExecutionContext Context
		{
			get
			{
				return _context;
			}
		}

		/// <summary>
		/// Gets the parameters specific to this job.
		/// </summary>
		public ParameterCollection JobParameters
		{
			get
			{
				return _context.Parameters;
			}
		}


		/// <summary>
		/// Gets the parameters that apply to the current request.
		/// </summary>
		public ParameterCollection RequestParameters
		{
			get
			{
				return _context.BatchExecutionContext.RequestParameters;
			}
		}

        /// <summary>
        /// Executes common startup logic. This 
        /// is the first method that will be
        /// invoked on the derived class.
        /// </summary>
        /// <returns>void</returns>
		protected virtual void Setup()
		{
			// no - op
		}

        /// <summary>
        /// Prepares the job to restart itself.
        /// This method will be invoked on the 
        /// derived class after 
        /// the <see cref="Setup"/> method returns, unless
        /// the job is not executing in Restart mode.
        /// In that case, this method will not
        /// be invoked. <see cref="Job.SetupForStart"/>
        /// will be invoked instead.
        /// </summary>
        /// <returns>void</returns>
		protected virtual void SetupForRestart()
		{
			// no - op
		}

        /// <summary>
        /// Prepares the job for execution.
        /// This is the second method that will be
        /// invoked on the derived class, unless
        /// the job is not executing in Restart mode.
        /// In that case, this method will not
        /// be invoked. <see cref="SetupForRestart"/>
        /// will be invoked instead.
        /// </summary>
        /// <returns>void</returns>
		protected virtual void SetupForStart()
		{
			// no - op
		}

        /// <summary>
        /// Executes the actual work the job needs
        /// to perform.
        /// </summary>
        /// <returns>Avanade.ACA.Batch.StatusCode</returns>
		abstract protected StatusCode ExecuteImpl();

        /// <summary>
        /// Tear down logic specific to a 
        /// successful execution goes here.
        /// </summary>
        /// <returns>void</returns>
		protected virtual void CleanUpAfterSuccess()
		{
			// no - op
		}

        /// <summary>
        /// Tear down logic specific to a
        /// failed execution goes here.
        /// </summary>
        /// <returns>void</returns>
		protected virtual void CleanUpAfterFailure()
		{
			// no - op
		}

        /// <summary>
        /// Common tear down logic goes here.
        /// </summary>
        /// <returns>void</returns>
		protected virtual void CleanUp()
		{
			// no - op
		}

        /// <summary>
        /// Prepares the job and its context
        /// to be paused.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="args">System.EventArgs</param>
        /// <returns>void</returns>
		protected virtual void OnPausing(object sender, EventArgs args)
		{
			// no - op
		}

        /// <summary>
        /// Prepares the job and its context
        /// to resume.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="args">System.EventArgs</param>
        /// <returns>void</returns>
		protected virtual void OnResuming(object sender, EventArgs args)
		{
			// no - op
		}

	}
}
