// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// ExceptionData is the wrapper for an <see cref="Exception"/> occurred during the Batch
	/// runtime.  Each exception instance is assigned a <see cref="Guid"/> as key to be
	/// uniquely stored in the database.
	/// </summary>    
    public class ExceptionData : NamedConfigurationElement, IBatchDBData
	{
		
		private Exception	_exception;
		private Guid		_instanceKey;
		private string		_exceptionType;
		private string		_message;
		private string		_source;
		private string		_target;
		private string		_stackTrace;
		private Guid		_outerException;
		private ExceptionData _outerExceptionData;

        /// <summary>
        /// Initializes the data with a new <see cref="Guid"/>.
        /// </summary>
		public ExceptionData() : this(Guid.NewGuid())
		{
		}


        /// <summary>
        /// Initializes a new instance with a <see cref="Guid"/>.
        /// </summary>
        /// <param name="key">System.Guid</param>
		public ExceptionData(Guid key)
		{
			this._instanceKey = key;
		}


		#region Custom Properties
		

		/// <summary>
		/// Implements the <see cref="IBatchDBData.Key"/> property.  Gets or sets the key for the data.
		/// </summary>
		/// <value>A <see cref="System.Guid"/> instance as the key of the data object.</value>
		public Guid Key
		{
			get {return this._instanceKey;}
			set {this._instanceKey = value;}
		}

		/// <summary>
		/// See <see cref="IBatchData.DisplayName"/>.  The value is always "Exception".
		/// </summary>
		public string DisplayName
		{
			get
			{
				return "Exception";
			}
		}

		/// <summary>
		/// See <see cref="IBatchDBData.Children"/>.
		/// </summary>
		public IList Children
		{
			get
			{
				return new ArrayList();
			}
		}

		/// <summary>
		/// The type of the Exception as a string.
		/// </summary>
		public string ExceptionType
		{
			get
			{
				return _exceptionType;
			}
			set
			{
				this._exceptionType = value;
			}
		}	

		/// <summary>
		/// Gets or sets the message of the exception.
		/// </summary>
		public string Message
		{
			get
			{
				return _message;
			}
			set
			{
				this._message = value;
			}
		}	

		/// <summary>
		/// Gets or sets the exception source.
		/// </summary>
		public string Source
		{
			get
			{
				return _source;
			}
			set
			{
				this._source = value;
			}
		}	

		/// <summary>
		/// Gets or sets the target of the exception.  
		/// </summary>
		public string Target
		{
			get
			{
				return _target;
			}
			set
			{
				this._target = value;
			}
		}	
		
		/// <summary>
		/// Gets or sets the stack trace of the exception.
		/// </summary>
		public string StackTrace
		{
			get
			{
				return _stackTrace;
			}
			set
			{
				this._stackTrace = value;
			}
		}	
		
		
		/// <summary>
		/// The <see cref="Exception"/> object represented by the <see cref="ExceptionData"/> instance.
		/// </summary>
		public Exception ExceptionObject
		{
			get
			{
				return _exception;
			}
			set
			{
				
				_exception = value;
				_exceptionType = _exception.GetType().AssemblyQualifiedName;
				_message = _exception.Message;
				_source = _exception.Source;
				_stackTrace = _exception.StackTrace;
				if (_exception.TargetSite != null)
				{
					_target = _exception.TargetSite.Name;
				}
			}
		}
	
		/// <summary>
		/// Gets or sets the instance key of the <see cref="ExceptionData"/> whose <see cref="ExceptionObject"/>
		/// has this current exception as its inner exception.
		/// The value is <see cref="Guid.Empty"/> if there's no outter exception.
		/// </summary>
		public Guid OuterExceptionId
		{
			get
			{
				return _outerException;
			}
			set
			{
				this._outerException = value;
			}
		}
		
		/// <summary>
		/// Gets or sets the exception which has this current exception
		/// as the inner exception.  The value is <see cref="Guid.Empty"/> if
		/// there's no outter exception.
		/// </summary>
		public ExceptionData OuterException
		{
			get
			{
				return _outerExceptionData;
			}
			set
			{
				_outerExceptionData = value;
			}
		}

		#endregion

	}
}
