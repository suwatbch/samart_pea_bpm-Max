// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Xml.Serialization;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// Represents the Batch Request after it has been dequeued by
	/// a Batch Server.
	/// </summary>
	[Serializable]
	public class DequeuedBatchExecutionRequest : BatchExecutionRequest
	{
		private ExecutionIsolationLevel _isolationLevel;
		private ExecutionPriorityLevel	_priority;
		private string					_configurationFilePath;

        /// <summary>
        /// Creates a new instance of the <see cref="DequeuedBatchExecutionRequest"/>class.
        /// </summary>
        /// <param name="requestKey">The Key that identifies the request.</param>
        /// <param name="batchName">The friendly Batch Name</param>
        /// <param name="isolationLevel">Execution Isolation Level. The only value
        /// currently supported is <see cref="ExecutionIsolationLevel.Process"/></param>
        /// <param name="priority">
        /// The operating system specific execution priority. This can be any one
        /// of the values defined in the <see cref="ExecutionPriorityLevel"/>enumeration.
        /// </param>
        /// <param name="configurationFilePath">
        /// The configuraion file to be used when creating the Application Domain
        /// to run the batch request.
        /// </param>
		public DequeuedBatchExecutionRequest(Guid requestKey,
										string batchName, 
										ExecutionIsolationLevel isolationLevel,
										ExecutionPriorityLevel priority,
										string configurationFilePath)
								: base (batchName)
		{
			SetKey(requestKey);
			_isolationLevel			= isolationLevel;
			_priority				= priority;
			_configurationFilePath	= configurationFilePath;
		}


        /// <summary>
        /// Creates a new instance of the <see cref="DequeuedBatchExecutionRequest"/>class.
        /// </summary>
		protected DequeuedBatchExecutionRequest()
		{
		}

		/// <summary>
		/// Gets the isolation level of the Batch Request
		/// </summary>
		public ExecutionIsolationLevel IsolationLevel
		{
			get
			{
				return _isolationLevel;
			}
			set
			{
				_isolationLevel = value;
			}
		}

		/// <summary>
		/// Gets the Execution Priority of the Batch Request
		/// </summary>
		public ExecutionPriorityLevel ExecutionPriority
		{
			get
			{
				return _priority;
			}
			set
			{
				_priority = value;
			}
		}

		/// <summary>
		/// Gets the path of the Batch's configuration file
		/// </summary>
		public string ConfigurationFilePath
		{
			get
			{
				return _configurationFilePath;
			}
			set
			{
				_configurationFilePath = value;
			}
		}

        /// <summary>
        /// This method is meant for internal ACA.NET Batch Architecutre use only
        /// </summary>
        /// <returns>object</returns>
		protected override object CreateSerializeableRequest()
		{
			return new DequeuedRequestInternal(this);
		}


		/// <summary>
		/// This class is meant for internal ACA.NET Batch Architecutre use only
		/// </summary>
		public class DequeuedRequestInternal 
			: BatchExecutionRequest.RequestInternal
		{
			DequeuedBatchExecutionRequest _request;

            /// <summary>
            /// For internal ACA.NET Batch Architecutre use only
            /// </summary>
			public DequeuedRequestInternal()
			{
				_request = 
					new DequeuedBatchExecutionRequest();
				SetInnerRequest(_request);
			}

            /// <summary>
            /// For internal ACA.NET Batch Architecutre use only
            /// </summary>
            /// <param name="innerRequest">Avanade.ACA.Batch.DequeuedBatchExecutionRequest</param>
			public DequeuedRequestInternal
				(DequeuedBatchExecutionRequest innerRequest) : base (innerRequest)
			{
				_request = innerRequest;
			}

			/// <summary>
			/// For internal ACA.NET Batch Architecutre use only
			/// </summary>
			[XmlAttribute("isolationLevel")]
			public ExecutionIsolationLevel IsolationLevel
			{
				get
				{
					return _request.IsolationLevel;
				}
				set
				{
					_request.IsolationLevel = value;
				}
			}

			/// <summary>
			/// Gets a value indicating the priority
			/// that the current request will have in the 
			/// queue once it is enqueued.
			/// </summary>
			[XmlAttribute("configurationFilePath")]
			public string ConfigurationFilePath
			{
				get
				{
					return _request.ConfigurationFilePath;
				}
				set
				{
					_request.ConfigurationFilePath = value;
				}
			}

			/// <summary>
			/// Gets the name of the Destination
			/// that will dequeue the request and
			/// process it.
			/// </summary>
			[XmlAttribute("executionPriority")]
			public ExecutionPriorityLevel ExecutionPriority
			{
				get
				{
					return _request.ExecutionPriority;
				}
				set
				{
					_request.ExecutionPriority = value;
				}
			}
		}
	}
}
