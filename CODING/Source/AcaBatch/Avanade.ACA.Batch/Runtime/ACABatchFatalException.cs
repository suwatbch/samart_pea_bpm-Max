// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================

using System;
using System.Runtime.Serialization;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// ACA.NET Batch Architecture exception that cannot be recovered from.
	/// </summary>
	[ Serializable() ]
	[ System.Runtime.InteropServices.ComVisible(false) ]
	public class ACABatchFatalException : Exception
	{
        /// <summary>
        /// Initializes a new instance of the ACABatchFatalException class.
        /// </summary>
		public ACABatchFatalException() : base()
		{
		}

        /// <summary>
        /// Initializes a new instance of the ACABatchFatalException class 
        /// with a specified error message.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
		public ACABatchFatalException(string message) : base(message)
		{
		}

        /// <summary>
        /// Initializes a new instance of the ACABatchFatalException class with a 
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
		public ACABatchFatalException(string message, Exception exception) : 
			base(message, exception)
		{
		}

        /// <summary>
        /// Initializes a new instance of the ACABatchFatalException class with 
        /// serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or 
        /// destination.
        /// </param>
		protected ACABatchFatalException(SerializationInfo info, StreamingContext context) :
			base(info, context)
		{
		}
	}
}


