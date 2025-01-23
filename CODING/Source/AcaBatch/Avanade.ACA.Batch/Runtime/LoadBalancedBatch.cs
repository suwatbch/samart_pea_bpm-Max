// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Threading;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// Abstract class that implements the template for a load balanaced batch.
	/// Concrete classes that implement a load balanced batch may inherit from
	/// this class.
	/// </summary>
	[Serializable]
	public abstract class LoadBalancedBatch : Batch, ILoadBalancedBatch
	{
        /// <summary>
        /// Creates a new instance of the <see cref="LoadBalancedBatch"/>class.
        /// </summary>
		public LoadBalancedBatch()
		{
		}

        /// <summary>
        /// Executes jobs in the batch.
        /// </summary>
        /// <param name="jobContexts">Avanade.ACA.Batch.JobExecutionContext[]</param>
        /// <returns>void</returns>
		protected override void ExecuteJobs(JobExecutionContext[] jobContexts)
		{
			ProcessChildRequests();
		}

        /// <summary>
        /// Process child batch requests by enqueueing them back into the 
        /// batch queue and waiting for them to complete execution.
        /// </summary>
        /// <returns>void</returns>
		protected virtual void ProcessChildRequests()
		{
			TimeSpan responsePollingInterval;
			double defaultPollingIntervalInSeconds = 30;
			
			string childBatchName = 
				(string)BatchContext.RequestParameters.GetPrimitiveData("ChildBatchName");

			// get the configured polling interval
			object o = BatchContext.RequestParameters["ResponsePollingInterval"];

			try
			{
				string pollingIntervalAsString = string.Empty;
				if (o != null)
				{
					pollingIntervalAsString = (string)o;
				}

				responsePollingInterval = TimeSpan.Parse(pollingIntervalAsString);
			}
			catch(FormatException)
			{
				responsePollingInterval = TimeSpan.FromSeconds(defaultPollingIntervalInSeconds);
			}
			catch(OverflowException)
			{
				responsePollingInterval = TimeSpan.FromSeconds(defaultPollingIntervalInSeconds);
			}

			// let the derived class create additional requests
			BatchExecutionRequest[] childRequests = 
				CreateChildRequests(childBatchName);

			// create a new queue object for processing the child requests
			Database database = 
				BatchContext.BatchExecutionContextData.BatchDatabase;

			BatchExecutionRequestQueue queue = 
				new BatchExecutionRequestQueue(database);

			BatchExecutionRequestAsyncResult[] asyncResults = 
				new BatchExecutionRequestAsyncResult[childRequests.Length];

			WaitHandle[] waitHandles = new WaitHandle[asyncResults.Length];

			for (int i = 0; i < childRequests.Length; i++)
			{
				BatchExecutionRequest childRequest = childRequests[i];
				childRequest.ParentRequest = BatchContext.Request;

				BatchExecutionRequestAsyncResult asyncResult =
					queue.ProcessRequestAsync(childRequest,
											responsePollingInterval);

				asyncResults[i] = asyncResult;
				waitHandles[i]	= asyncResult.AsyncWaitHandle;
			}

			// For a normal Batch, the first job that commits
			// causes the batch context to commit. However, the
			// LoadBalancedBatch may not have any jobs of its own.
			// Therefore, when we have fired the child requests, it
			// is time to commit the status of the LoadBalancedBatch
			// to reflect that it is executing.
			BatchContext.Commit();

			// set the status of the batch
			BatchContext.Status = WaitForCompletion(asyncResults, waitHandles);
			BatchContext.Commit();
			
		}

        /// <summary>
        /// Creates a list of child requests to be load balanced
        /// across the available batch execution destinations.
        /// </summary>
        /// <param name="childBatchName">string</param>
        /// <returns>Avanade.ACA.Batch.BatchExecutionRequest[]</returns>
		abstract protected BatchExecutionRequest[] CreateChildRequests
			(
				string childBatchName
			);

        /// <summary>
        /// Waits for child batch requests to complete
        /// </summary>
        /// <param name="results">
        /// Array of <see cref="BatchExecutionRequestAsyncResult"/> objects
        /// that represent the child requests' async results.
        /// </param>
        /// <param name="waitHandles">
        /// An array of wait handles for the child batch requests.
        /// </param>
        /// <returns>Avanade.ACA.Batch.BatchProcessStatusCode</returns>
		protected virtual BatchProcessStatusCode 
			WaitForCompletion
			(
				BatchExecutionRequestAsyncResult[] results, 
				WaitHandle[] waitHandles
			)
		{
			WaitHandle.WaitAll(waitHandles);

			// assume they all succeeded
			BatchProcessStatusCode overallStatus = 
				BatchProcessStatusCode.Succeeded;

			// check each individual child request status
			foreach (BatchExecutionRequestAsyncResult result in results)
			{
				BatchProcessStatusCode status = result.Response.Status;
				// if one failed, then they ruin the whole batch
				if (status != BatchProcessStatusCode.Succeeded)
				{
					overallStatus = BatchProcessStatusCode.Failed;
				}
			}

			return overallStatus;

		}

	}
}
