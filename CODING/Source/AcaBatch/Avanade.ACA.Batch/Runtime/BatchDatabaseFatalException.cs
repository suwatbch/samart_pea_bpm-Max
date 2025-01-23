// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Runtime.Serialization;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// ACA.NET Batch Architecture Database exception that cannot be recovered from.
	/// </summary>
	[Serializable]
	public class BatchDatabaseFatalException : ACABatchFatalException
	{
        /// <summary>
        /// Initializes a new instance of the BatchDatabaseFatalException class.
        /// </summary>
		public BatchDatabaseFatalException() : base()
		{
		}

        /// <summary>
        /// Initializes a new instance of the BatchDatabaseFatalException class 
        /// with a specified error message.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
		public BatchDatabaseFatalException(string message) : base(message)
		{
		}

        /// <summary>
        /// Initializes a new instance of the BatchDatabaseFatalException class with a 
        /// specified error message and a reference to the inner exception that is 
        /// the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for 
        /// the exception.
        /// </param>
        /// <param name="exception">The exception that is the cause of the current 
        /// exception.  If the innerException parameter is not a null reference, 
        /// the current exception is raised in a catch block that handles the inner 
        /// exception.
        /// </param>
		public BatchDatabaseFatalException(string message, Exception exception) : 
			base(message, exception)
		{
		}

        /// <summary>
        /// Initializes a new instance of the BatchDatabaseFatalException class with 
        /// serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or 
        /// destination. </param>
		protected BatchDatabaseFatalException(SerializationInfo info, StreamingContext context) :
		base(info, context)
		{
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
			base.GetObjectData(info, context);
		}
	}
}

