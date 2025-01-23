// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Diagnostics;
using System.Threading;
using Avanade.ACA.Batch;
using Avanade.ACA.Batch.Instrumentation;
using System.Runtime.CompilerServices;

namespace Avanade.ACA.Batch.BatchExecutionHost
{
	/// <summary>
	/// Represents an out-of-process 
	/// execution environment for isolating
	/// batch executions.
	/// </summary>
	class App
	{

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">A single command
        /// line argument is expected. This argument
        /// must be the path to a serialized
        /// <see cref="BatchExecutionRequest"/> object.</param>
        /// <returns>int</returns>
		static int Main(string[] args)
		{
            //-----------------------------
            {
                
                #if (DEBUG)
                    #region Write Log
                    //string sSource = "ACA Batch - Testing";
                    //string sLog = "Application";
                    //string sEvent = "Excution_Host was called";

                    //if (!EventLog.SourceExists(sSource))
                    //    EventLog.CreateEventSource(sSource, sLog);

                    //EventLog.WriteEntry(sSource, sEvent);
                    ////EventLog.WriteEntry(sSource, sEvent, EventLogEntryType.Warning, 234);
                    #endregion
                    Thread.Sleep(10000);
                #endif

            }
            //-----------------------------

			RegisterUnhandledExceptionEvent();
			return Execute(args);
		}

        /// <summary>RegisterUnhandledExceptionEvent</summary>
        /// <returns>void</returns>
        internal static void RegisterUnhandledExceptionEvent()
		{
			AppDomain.CurrentDomain.UnhandledException += 
				new UnhandledExceptionEventHandler(BatchExecutionHostUnhandledExceptionHandler);
			
		}

        /// <summary>Execute</summary>
        /// <param name="args">string[]</param>
        /// <returns>int</returns>
        internal static int Execute(string[] args)
		{
			try
			{
                //System.Diagnostics.Debugger.Break();

				BatchArchitectureEvent.AppendExecutionLog("BatchExecutionHost application started.");

				// log the start of batch execution
                BatchInstrumentationProvider.Instance.FireBatchExecutionHostStartedEvent();
				
				string configurationFilePath;
				Guid requestKey;
				ProcessPriorityClass processPriorityClass;

				ParseArguments(args, out requestKey, out configurationFilePath, out processPriorityClass);
				BatchArchitectureEvent.AppendExecutionLog("Argument parsing completed successfully.");

				Process.GetCurrentProcess().PriorityClass = processPriorityClass;

				// use an AppDomainIsolationStrategy in order to
				// dynamically set the configuration file.
				AppDomainIsolationStrategy isolation = 
					new AppDomainIsolationStrategy();
				
				// wait for execution to complete 
				// before exiting the current process
				isolation.Execute(requestKey, configurationFilePath);

			}
			catch (ACABatchException e)
			{
				return -1;
			}
			catch (Exception e)
			{
                BatchInstrumentationProvider.Instance.FireBatchArchitectureFailedEvent("BatchExecutionHost failed.", e);
				return -1;			
			}			
			return 0;
		}

        /// <summary>ParseArguments</summary>
        /// <param name="args">string[]</param>
        /// <param name="requestKey">System.Guid</param>
        /// <param name="configurationFilePath">string</param>
        /// <param name="processPriorityClass">System.Diagnostics.ProcessPriorityClass</param>
        /// <returns>void</returns>
        private static void ParseArguments(string[] args,
			out Guid requestKey,
			out string configurationFilePath,
			out ProcessPriorityClass processPriorityClass) 
		{
			if (args.Length < 2)
			{
				string errorMessage = string.Format("Wrong number of arguments.  It should have 3 arguments but only got {0} ", args.Length);
					
				switch (args.Length)
				{
					case 1:
						errorMessage += string.Format(": '{0}'", args[0]);
						break;
					case 2:
						errorMessage += string.Format(": '{0}', '{1}'", args[0], args[1]);
						break;
				}
				throw new ArgumentException(errorMessage);
			}

			try
			{
				requestKey = new Guid(args[0]);
			}
			catch (Exception e)
			{
				throw new ArgumentException("Invalid requestKey", "arg[0]", e);
			}

			try
			{
				configurationFilePath = args[1];
				System.IO.File.Exists(configurationFilePath);
			}
			catch (Exception e)
			{
				throw new ArgumentException(
					string.Format("Invalid configurationFilePath or file does not exist: '{0}'", args[1]), 
					"arg[1]", e);
			}	

			try
			{
				ExecutionPriorityLevel exePriorityLevel = (ExecutionPriorityLevel)ExecutionPriorityLevel.Parse(typeof(ExecutionPriorityLevel), args[2]);		
				processPriorityClass = PriorityConverter.ToProcessPriorityClass(exePriorityLevel);
			}
			catch (Exception e)
			{
				throw new ArgumentException("Invalid exePriorityLevel", "arg[2]", e);
			}
		}

        /// <summary>BatchExecutionHostUnhandledExceptionHandler</summary>
        /// <param name="sender">object</param>
        /// <param name="args">System.UnhandledExceptionEventArgs</param>
        /// <returns>void</returns>
        private static void BatchExecutionHostUnhandledExceptionHandler(
			object sender, UnhandledExceptionEventArgs args)
		{
			Exception e = (Exception) args.ExceptionObject;

			if (e is ThreadAbortException)
			{
				return;
			}
			try
			{
				if (e != null)
				{
                    BatchInstrumentationProvider.Instance.FireBatchArchitectureFailedEvent("BatchExecutionHost unhandled event caught.", e);					
				}
				else
				{                 
                    BatchInstrumentationProvider.Instance.FireBatchArchitectureFailedEvent("BatchExecutionHost unhandled event caught.", e);					
				}
			}
			catch (Exception ex)
			{
				string errorMsg = "Batch Execution event unhandled exception occurred: " + ex.Message + "\n\n" + e.Message;
				errorMsg = BatchArchitectureEvent.AppendExecutionLogToErrorMessage(errorMsg);
				EventLog.WriteEntry("ACA Batch", errorMsg);
			}			
		}
	}
}
