// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Runtime.Serialization;
using Avanade.ACA.Batch;

namespace Avanade.ACA.Batch.Configuration
{
	/// <summary>
	/// This exception is thrown when a assigning a parameter
	/// value that the architecture does not know how to handle.
	/// </summary>
	[Serializable]
	public class ParameterTypeNotSupportedException : ACABatchException
	{
		private string _unsupportedType;

		private const string MESSAGE	= "The type '{0}' is not a supported parameter type.";
		private const string KEY		= "UnsupportedTypeName";

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ParameterTypeNotSupportedException"/> exception
        /// with the specified name of the type that is not supported.
        /// </summary>
        /// <param name="unsupportedTypeName">The name of the 
        /// type that is not supported by the batch architecture.</param>
		public ParameterTypeNotSupportedException(string unsupportedTypeName) 
			: base(FormatMessage(unsupportedTypeName))
		{
			_unsupportedType = unsupportedTypeName;
		}

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="ParameterTypeNotSupportedException"/> class.
        /// </summary>
        /// <param name="unsupportedType">System.Type</param>
		public ParameterTypeNotSupportedException(Type unsupportedType) 
			: base(FormatMessage(unsupportedType))
		{
			_unsupportedType = unsupportedType.AssemblyQualifiedName;
		}

		/// <summary>
		/// Gets the name of the type that caused the exception.
		/// </summary>
		public string UnsupportedType
		{
			get
			{
				return _unsupportedType;
			}
		}

        /// <summary>FormatMessage</summary>
        /// <param name="unsupportedTypeName">System.Type</param>
        /// <returns>string</returns>
        private static string FormatMessage(Type unsupportedTypeName)
		{
			return FormatMessage(unsupportedTypeName.AssemblyQualifiedName);
		}

        /// <summary>FormatMessage</summary>
        /// <param name="typeName">string</param>
        /// <returns>string</returns>
        private static string FormatMessage(string typeName)
		{
			return String.Format(MESSAGE, typeName);
		}
        /// <summary>
        /// Initializes a new instance of the BatchDatabaseFatalException class 
        /// with a specified error message.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        /// <param name="unsupportedType">System.Type</param>
		public ParameterTypeNotSupportedException(string message, Type unsupportedType) : base(message)
		{
			_unsupportedType = unsupportedType.AssemblyQualifiedName;
		}

        /// <summary>
        /// Initializes a new instance of the BatchDatabaseFatalException class with a 
        /// specified error message and a reference to the inner exception that is 
        /// the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for 
        /// the exception.
        /// </param>
        /// <param name="unsupportedType">System.Type</param>
        /// <param name="exception">The exception that is the cause of the current 
        /// exception.  If the innerException parameter is not a null reference, 
        /// the current exception is raised in a catch block that handles the inner 
        /// exception.
        /// </param>
		public ParameterTypeNotSupportedException(string message, Type unsupportedType, Exception exception) : 
			base(message, exception)
		{
			_unsupportedType = unsupportedType.AssemblyQualifiedName;
		}

        /// <summary>
        /// Initializes a new instance of the BatchDatabaseFatalException class with 
        /// serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or 
        /// destination. </param>
		protected ParameterTypeNotSupportedException(SerializationInfo info, StreamingContext context) :
		base(info, context)
		{
			_unsupportedType = info.GetString(KEY);
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
			info.AddValue(KEY, _unsupportedType);
			base.GetObjectData(info, context);
		}
	}
}
