// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Runtime.Serialization;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// ACABatchException represents the data for the exception occurs in ACA Batch.
	/// </summary>
	[ Serializable() ]
	[ System.Runtime.InteropServices.ComVisible(false) ]
	public class ACABatchException : Exception
	{
        /// <summary>
        /// Initializes a new instance of the ACABatchException class.
        /// </summary>
		public ACABatchException() : base()
		{
		}

        /// <summary>
        /// Initializes a new instance of the ACABatchException class with a specified error message.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
		public ACABatchException(string message) : base(message)
		{
		}

        /// <summary>
        /// Initializes a new instance of the ACABatchException class with a specified error 
        /// message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.
        /// </param>
        /// <param name="exception">The exception that is the cause of the current exception. 
        /// If the innerException parameter is not a null reference, the current exception 
        /// is raised in a catch block that handles the inner exception.
        /// </param>
		public ACABatchException(string message, Exception exception) : 
			base(message, exception)
		{
		}

        /// <summary>
        /// Initializes a new instance of the ACABatchException class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.
        /// </param>
		protected ACABatchException(SerializationInfo info, StreamingContext context) :
			base(info, context)
		{
		}
	}
}

