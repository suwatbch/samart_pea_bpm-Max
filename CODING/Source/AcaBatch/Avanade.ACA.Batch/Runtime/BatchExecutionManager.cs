// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================

using System;
using Avanade.ACA.Batch.Instrumentation;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// This class implements the <see cref="IBatchExecutionManager"/> interface.
	/// </summary>
	[Serializable]
	public class BatchExecutionManager : IBatchExecutionManager
	{
		private const string MISSING_CONFIGURATION = 
				"The BatchExecutionNode has not been configured.";

        /// <summary>
        /// Creates a new instance of <see cref="BatchExecutionManager"/>.
        /// </summary>
		public BatchExecutionManager()
		{
		}

        /// <summary>
        /// Creates and returns the <see cref="BatchExecutionContext"/> for a given 
        /// <see cref="BatchExecutionRequest"/></summary>
        /// <param name="request">The request object</param>
        /// <returns>The <see cref="BatchExecutionContext"/> for the batch request</returns>
		public BatchExecutionContext 
			CreateBatchExecutionContext(BatchExecutionRequest request)
		{
			BatchArchitectureEvent.AppendExecutionLog("Got request data from database.");

			if (request == null)
			{
				throw new ArgumentNullException("The 'request' parameter cannot be null");
			}

			BatchExecutionData executionData = BatchExecutionData.Current;
			
			if (executionData == null)
			{                
                BatchInstrumentationProvider.Instance.FireBatchArchitectureConfigFailureEvent(MISSING_CONFIGURATION, null);
				
				throw new ACABatchConfigurationException(MISSING_CONFIGURATION);
			}

			BatchExecutionContextData batchData = 
				executionData.LoadRequestedBatch(request);

			BatchArchitectureEvent.AppendExecutionLog("BatchExecutionContextData is loaded from database.");

			BatchExecutionContext context = 
				new BatchExecutionContext(batchData, request);

			BatchArchitectureEvent.AppendExecutionLog("BatchExecutionContext is created successfully.");


			return context;
		}

	}


}
