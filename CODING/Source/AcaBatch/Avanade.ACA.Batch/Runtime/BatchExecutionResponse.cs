// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// Encapsulates the response of a Batch Execution
	/// </summary>
	public class BatchExecutionResponse
	{
		private BatchExecutionRequest	_request;
		private BatchProcessStatusCode	_statusCode;
		private Database				_database;

        /// <summary>
        /// Creates a new instance of the <see cref="BatchExecutionResponse"/> class.
        /// </summary>
        /// <param name="database">The ACA.NET Batch Database</param>
        /// <param name="request">The batch request</param>
		public BatchExecutionResponse
			(
				Database database, 
				BatchExecutionRequest request
			)
		{
			_request = request;
			_database	= database;
			bool toPause;
			try
			{
				DefaultBatchManager.RequestQueue.CheckStatus
					(
					_database, 
					_request.Key, 
					out _statusCode, 
					out toPause
					);
			}
			catch(BatchRequestNotFoundException)
			{
				_statusCode = BatchProcessStatusCode.FailedCanceled;
				toPause = false;
			}
		}

		/// <summary>
		/// Gets the status of the batch execution. 
		/// Can be any one of the values defined in the
		/// <see cref="BatchProcessStatusCode"/> enumeration.
		/// </summary>
		public BatchProcessStatusCode Status
		{
			get
			{
				return _statusCode;
			}
		}

	}
}
