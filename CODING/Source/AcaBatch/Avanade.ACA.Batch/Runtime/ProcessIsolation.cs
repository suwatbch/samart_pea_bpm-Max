// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Diagnostics;


using Avanade.ACA.Batch.Instrumentation;
namespace Avanade.ACA.Batch
{
	/// <summary>
	/// Represents an <see cref="BatchExecutionIsolationStrategy"/>
	/// implementation, that starts up a new process for 
	/// processing the specified <see cref="BatchExecutionRequest"/>.
	/// </summary>
	public class ProcessIsolationStrategy : BatchExecutionIsolationStrategy
	{
		private const string PROCESS_FILE_NAME = "Avanade.ACA.Batch.BatchExecutionHost.exe";
		private const string BATCH_SERVER_COULD_NOT_START_EXEC_HOST = "The Batch Server failed to start the Batch Execution Host from path: ";

		private int						_exitCode;
		private DateTime				_exitTime;
		private string					_processFileName;
		private ExecutionPriorityLevel	_exePriorityLevel;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessIsolationStrategy"/>
        /// class.
        /// </summary>
		public ProcessIsolationStrategy() : this(PROCESS_FILE_NAME, ExecutionPriorityLevel.Normal)
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessIsolationStrategy"/>
        /// class.
        /// </summary>
        /// <param name="fileName">The file name that identifies the process.</param>
        /// <param name="exePriorityLevel">Avanade.ACA.Batch.ExecutionPriorityLevel</param>
		public ProcessIsolationStrategy(string fileName, ExecutionPriorityLevel exePriorityLevel)
		{
			// assume failure
			_exitCode = -1;
			_exitTime = DateTime.MinValue;
			_processFileName = fileName;
			_exePriorityLevel = exePriorityLevel;
		}

		/// <summary>
		/// Gets or Sets the process file name.
		/// </summary>
		public string ProcessFileName
		{
			get
			{
				return _processFileName;
			}
			set
			{
				_processFileName = value;
			}

		}

        /// <summary>
        /// Implements the code to launch a new process to run the batch
        /// execution request.
        /// </summary>
        /// <param name="requestKey">The key of the dequeued batch execution request.</param>
        /// <param name="configurationFilePath">The path of the configuration file for the request.</param>
        /// <returns>void</returns>
		protected override void ExecuteImpl(Guid requestKey, 
			string configurationFilePath)
		{
			Process process = null;
			try
			{
				string arguments = string.Format("\"{0}\" \"{1}\" \"{2}\"", requestKey.ToString(), configurationFilePath, _exePriorityLevel);
				// create the batch execution host process
				ProcessStartInfo startInfo = 
					new ProcessStartInfo(ProcessFileName, arguments);

				startInfo.WindowStyle = ProcessWindowStyle.Hidden;

				// start the process
				process = Process.Start(startInfo);
				process.WaitForExit();


				_exitCode = process.ExitCode;
				_exitTime = process.ExitTime;
				
			}
			catch (ACABatchException)
			{
				// the error is already instrumentated if the exeception is of the 
				// type ACABatchException
				throw;
			}
			catch (Exception e)
			{
				string message = BATCH_SERVER_COULD_NOT_START_EXEC_HOST + ProcessFileName;

                BatchInstrumentationProvider.Instance.FireBatchArchitectureFailedEvent(message, e);				

				throw new ACABatchFatalException(message, e);
			}
			finally
			{
				if (process != null)
				{
					process.Close();
				}
			}
		}

		/// <summary>
		/// Gets the exit code of the BatchExecutionHost process that ran
		/// the batch request.
		/// </summary>
		public int ExitCode
		{
			get
			{
				return _exitCode;
			}
		}

		/// <summary>
		/// Gets the time when the BatchExecutionHost process that ran
		/// the batch request exited.
		/// </summary>
		public DateTime ExitTime
		{
			get
			{
				return _exitTime;
			}
		}
	}
}
