// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Runtime.Serialization;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// ACA.NET Batch Architecture exception that should be thrown if a
	/// batch request is not found in the Batch Database.
	/// </summary>
	[Serializable]
	public class BatchRequestNotFoundException : ACABatchFatalException
	{
        /// <summary>
        /// Initializes a new instance of the BatchRequestNotFoundException class.
        /// </summary>
		public BatchRequestNotFoundException() : base()
		{
		}

        /// <summary>
        /// Initializes a new instance of the BatchRequestNotFoundException class 
        /// with a specified error message.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
		public BatchRequestNotFoundException(string message) : base(message)
		{
		}

        /// <summary>
        /// Initializes a new instance of the BatchRequestNotFoundException class with a 
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
		public BatchRequestNotFoundException(string message, Exception exception) : 
			base(message, exception)
		{
		}

        /// <summary>
        /// Initializes a new instance of the BatchRequestNotFoundException class with 
        /// serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or 
        /// destination. </param>
		protected BatchRequestNotFoundException(SerializationInfo info, StreamingContext context) :
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

