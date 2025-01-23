// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================

using System;
using System.Runtime.Serialization;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// An exception that occurs in the context
	/// of a batch execution.
	/// </summary>
	[Serializable]
	public class BatchExecutionException : ACABatchException
	{
		private BatchExecutionContext _context;
        /// <summary>
        /// Initializes a new instance of the ACABatchFatalException class.
        /// </summary>
		public BatchExecutionException() : base()
		{
		}

        /// <summary>
        /// Initializes a new instance of the ACABatchFatalException class 
        /// with a specified error message.
        /// </summary>
        /// <param name="context">Avanade.ACA.Batch.BatchExecutionContext</param>
        /// <param name="message">A message that describes the error.</param>
		public BatchExecutionException(BatchExecutionContext context, string message) : base(message)
		{
			_context = context;
		}

        /// <summary>
        /// Initializes a new instance of the ACABatchFatalException class with a 
        /// specified error message and a reference to the inner exception that is 
        /// the cause of this exception.
        /// </summary>
        /// <param name="context">Avanade.ACA.Batch.BatchExecutionContext</param>
        /// <param name="message">The error message that explains the reason for 
        /// the exception.
        /// </param>
        /// <param name="exception">The exception that is the cause of the current 
        /// exception.  If the innerException parameter is not a null reference, 
        /// the current exception is raised in a catch block that handles the inner 
        /// exception.
        /// </param>
		public BatchExecutionException(BatchExecutionContext context, string message, Exception exception) : 
			base(message, exception)
		{
			_context = context;
		}

        /// <summary>
        /// Initializes a new instance of the ACABatchFatalException class with 
        /// serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or 
        /// destination.
        /// </param>
		protected BatchExecutionException(SerializationInfo info, StreamingContext context) :
			base(info, context)
		{
			_context = (BatchExecutionContext)
				info.GetValue("_context", typeof(BatchExecutionContext));
		}

        /// <summary>
        /// See <see cref="Exception.GetObjectData"/>.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or 
        /// destination.</param>
        /// <returns>void</returns>
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("_context", _context);
			base.GetObjectData(info, context);
		}

		/// <summary>
		/// The <see cref="BatchExecutionContext"/>
		/// at the time the exception was thrown.
		/// </summary>
		public BatchExecutionContext Context
		{
			get
			{
				return _context;
			}
		}
	}

	/// <summary>
	/// An exception that occurs during the execution of a 
	/// given job.
	/// </summary>
	[Serializable]
	public class JobExecutionException : BatchExecutionException
	{
		private JobExecutionContext _jobContext;

        /// <summary>
        /// Initializes a new instance of the JobExecutionException class.
        /// </summary>
		public JobExecutionException() : base()
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="JobExecutionException"/> 
        /// class with a specified error message.
        /// </summary>
        /// <param name="context">Avanade.ACA.Batch.JobExecutionContext</param>
        /// <param name="message">A message that describes the error.</param>
		public JobExecutionException
			(
				JobExecutionContext context, 
				string message
			) : base(context.BatchExecutionContext, message)
		{
			_jobContext = context;
		}

        /// <summary>
        /// Initializes a new instance of the ACABatchFatalException class with a 
        /// specified error message and a reference to the inner exception that is 
        /// the cause of this exception.
        /// </summary>
        /// <param name="context">Avanade.ACA.Batch.JobExecutionContext</param>
        /// <param name="message">The error message that explains the reason for 
        /// the exception.
        /// </param>
        /// <param name="exception">The exception that is the cause of the current 
        /// exception.  If the innerException parameter is not a null reference, 
        /// the current exception is raised in a catch block that handles the inner 
        /// exception.
        /// </param>
		public JobExecutionException
			(
				JobExecutionContext context,
				string message, 
				Exception exception
			) : 
			base(context.BatchExecutionContext, message, exception)
		{
			_jobContext = context;
		}

        /// <summary>
        /// Initializes a new instance of the ACABatchFatalException class with 
        /// serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or 
        /// destination.
        /// </param>
		protected JobExecutionException(SerializationInfo info, StreamingContext context) :
			base(info, context)
		{
			_jobContext = (JobExecutionContext)
				info.GetValue("_jobContext", typeof(JobExecutionContext));
		}

        /// <summary>
        /// See <see cref="Exception.GetObjectData"/>.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or 
        /// destination.</param>
        /// <returns>void</returns>
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("_jobContext", _jobContext);
			base.GetObjectData(info, context);
		}

		/// <summary>
		/// The <see cref="BatchExecutionContext"/>
		/// at the time the exception was thrown.
		/// </summary>
		public JobExecutionContext JobContext
		{
			get
			{
				return _jobContext;
			}
		}
	}
}
