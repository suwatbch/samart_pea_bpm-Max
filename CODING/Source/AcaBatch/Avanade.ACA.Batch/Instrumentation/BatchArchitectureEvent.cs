// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Diagnostics;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation;

namespace Avanade.ACA.Batch.Instrumentation
{    
	/// <summary>
	/// The base class for all the Instruemented Events fired from ACA Batch.
	/// Used internally by the ACA Batch and should not be used by the application code.
	/// </summary>	
    public class BatchArchitectureEvent : BaseWmiEvent
	{
        internal const string					EventSource			= "ACA Batch";
        internal const string					CounterCategory		= "ACA Batch";
        internal const string					CounterCategoryHelp	= "ACA Batch Architecture performance counters.";        
        private static StringBuilder			BatchExecutionLogBuilder;
        private const string					ExecutionLogHeader = "\n\n-----  Batch Execution Log  -----\n";

        /// <summary>
        /// Used internally by the ACA Batch and should not be used by the application code.
        /// </summary>
        /// <param name="logMessage">string</param>
        /// <returns>void</returns>
        public static void AppendExecutionLog(string logMessage)
        {
            if (BatchExecutionLogBuilder != null)
            {
                lock (typeof(BatchArchitectureEvent))
                {
                    BatchExecutionLogBuilder.AppendFormat("{0}\n", logMessage);
                }
            }
        }

        /// <summary>
        /// This method is used by the ACA Batch Architecture and should not be used in the
        /// application code.
        /// </summary>
        /// <param name="errorMessage">string</param>
        /// <returns>string</returns>
        public static string AppendExecutionLogToErrorMessage(string errorMessage)
        {
            string msg = string.Empty;
            if (BatchExecutionLogBuilder != null)
            {
                lock (typeof(BatchArchitectureEvent))
                {
                    BatchExecutionLogBuilder.Append("\n");

                    msg = (errorMessage == null || errorMessage == string.Empty) ?
                        BatchExecutionLogBuilder.ToString() :
                        errorMessage + BatchExecutionLogBuilder.ToString();
                    BatchExecutionLogBuilder = new StringBuilder(ExecutionLogHeader);
                }
            }
            return msg;
        }
	}
}
