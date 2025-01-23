// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================

using System;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using Avanade.ACA.Batch.Configuration;
using System.Xml;
using System.Reflection;
using Avanade.ACA.Batch.Configuration.IO;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// This class encapsulates a batch execution request
	/// </summary>
	[Serializable]
	public class BatchExecutionRequest
	{
		private const int DEFAULT_QUEUE_TIMEOUT_IN_DAYS = 365;
		private const int DEFAULT_START_POLLING_AFTER_MINS = 3;

		private string					_batchName;
		private Guid					_batchKey;
		private string					_batchClientName;

		private QueuePriorityLevel		_queuePriority;
		private bool					_usePreDefinedQueuePriority;

		private TimeSpan				_queueTimeout;
		
		private string					_destination;
		private bool					_usePreDefinedDestination;

		private ParameterCollection		_parameters;
		private DateTime				_timeStamp;
		private TimeSpan				_startPollingAfter;
		private Guid					_key;

		private BatchExecutionRequest	_parentRequest;

		private static XmlSerializer _serializer;
		private static object sync = new object();

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="BatchExecutionRequest"/> class.
        /// </summary>
        /// <param name="batchName">The Batch Name</param>
        /// <param name="queueTimeout">Time interval after which the request should be discarded from the queue.</param>
        /// <param name="startPollingAfter">Time interval after which the client should start polling for results</param>
        /// <param name="queuePriority">Priority of the batch in the Queue. 
        /// This can be any one of the values defined in <see cref="QueuePriorityLevel"/></param>
        /// <param name="destination">Batch Destination</param>
        /// <param name="parameters">A collection of parameters for the request.</param>
		protected BatchExecutionRequest(string batchName, 
										TimeSpan queueTimeout,
										TimeSpan startPollingAfter,
										QueuePriorityLevel queuePriority, 
										string destination,
										ParameterCollection parameters) : this()
		{
			_batchName				= batchName;
			_queuePriority			= queuePriority;
			_destination			= destination;
			_parameters				= parameters;
			_key					= Guid.Empty;
			_queueTimeout			= queueTimeout;
			_startPollingAfter		= startPollingAfter;
			_timeStamp				= DateTime.Now;
		}

        /// <summary>
        /// Creates a new instance of <see cref="BatchExecutionRequest"/> 
        /// with the specified batch key.
        /// </summary>
        /// <param name="batchKey">The batch key Guid</param>
		public BatchExecutionRequest(Guid batchKey) : this()
		{
			_batchKey = batchKey;
		}

        /// <summary>
        /// Creates a new instance of <see cref="BatchExecutionRequest"/> 
        /// with the specified batch name.
        /// </summary>
        /// <param name="batchName">The name of the Batch</param>
		public BatchExecutionRequest(string batchName)
									: this ()
		{
			_batchName = batchName;
		}

        /// <summary>
        /// Creates a new instance of <see cref="BatchExecutionRequest"/> 
        /// with the specified batch name and parent request. 
        /// </summary>
        /// <param name="batchName">The name of the Batch</param>
        /// <param name="parentRequest">The parent request object</param>
		public BatchExecutionRequest
			(
				string batchName, 
				BatchExecutionRequest parentRequest
			) : this(batchName)
		{
			_parentRequest = parentRequest;
		}

        /// <summary>
        /// Creates a new instance of <see cref="BatchExecutionRequest"/> 
        /// with the specified batch key and parent request. 
        /// </summary>
        /// <param name="batchKey">The batch key Guid</param>
        /// <param name="parentRequest">The parent request object</param>
		public BatchExecutionRequest
			(
				Guid batchKey, 
				BatchExecutionRequest parentRequest
			) : this(batchKey)
		{
			_parentRequest = parentRequest;
		}

        /// <summary>
        /// Default protected constructor. This constructor is invoked
        /// by the other public constructors of this class
        /// </summary>
		protected BatchExecutionRequest()
		{
			_destination						= String.Empty;
			_batchClientName					= Dns.GetHostName();
			_queueTimeout						= 
				TimeSpan.FromDays(DEFAULT_QUEUE_TIMEOUT_IN_DAYS);

			_startPollingAfter					=
				TimeSpan.FromMinutes(DEFAULT_START_POLLING_AFTER_MINS);

			_usePreDefinedDestination			= true;
			_usePreDefinedQueuePriority			= true;

			_key		= Guid.Empty;
			_timeStamp	= DateTime.Now;

			/*RootApplicationConfigurationNode rootNode =
				new RootApplicationConfigurationNode("test");

			ClientRequestParametersNode parameters 
				= new ClientRequestParametersNode();

			parameters.Initialize(rootNode);
			rootNode.AddChild(parameters);*/

			_parameters = new ParameterCollection();//parameters);
		}

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="BatchExecutionRequest"/> object
        /// that can be used to restart a failed batch.
        /// </summary>
        /// <param name="failedRequestKey">
        /// The unique identifier of the failed Batch
        /// Execution Request.</param>
        /// <returns>Avanade.ACA.Batch.BatchExecutionRequest</returns>
		public static BatchExecutionRequest ForRestart(Guid failedRequestKey)
		{
			BatchExecutionRequest request = 
				new BatchExecutionRequest();
			request.SetKey(failedRequestKey);
			return request;
		}

        /// <summary>
        /// Creates a new instance of the <see cref="BatchExecutionRequest"/>
        /// class based on the given instance of <see cref="BatchExecutionRequest"/></summary>
        /// <param name="origRequest">The original request object that is to be cloned.</param>
        /// <returns>The cloned request object</returns>
		public static BatchExecutionRequest Clone(BatchExecutionRequest origRequest)
		{
			BatchExecutionRequest newRequest = new BatchExecutionRequest(
				origRequest._batchName,
				origRequest._queueTimeout,
				origRequest._startPollingAfter,
				origRequest._queuePriority,
				origRequest._destination,
				origRequest._parameters);
			newRequest._usePreDefinedDestination = origRequest._usePreDefinedDestination;
			newRequest._usePreDefinedQueuePriority = origRequest._usePreDefinedQueuePriority;
			newRequest._batchClientName = origRequest._batchClientName;
			newRequest._key = origRequest._key;
			return newRequest;
		}

		/// <summary>
		/// The Name of the Batch Definition
		/// to execute.
		/// </summary>
		public string BatchName
		{
			get
			{
				return _batchName;
			}
			set
			{
				_batchName = value;
			}
		}

		/// <summary>
		/// The Key of the Batch Definition
		/// to execute.
		/// </summary>
		public Guid BatchKey
		{
			get
			{
				return _batchKey;
			}
			set
			{
				_batchKey = value;
			}
		}

		/// <summary>
		/// The name of the client 
		/// that is issuing the request. Typically,
		/// this is the friendly machine name.
		/// </summary>
		public string BatchClientName
		{
			get
			{
				return _batchClientName;
			}
			set
			{
				_batchClientName = value;
			}
		}

		/// <summary>
		/// The amount of time the request will remain in the queue
		/// before it expires and is dropped. 
		/// Specify <see cref="TimeSpan.MaxValue"/>
		/// to guarantee that the request will never expire.
		/// </summary>
		public TimeSpan QueueTimeout
		{
			get
			{
				return _queueTimeout;
			}
			set
			{
				_queueTimeout = value;
			}
		}

		/// <summary>
		/// The time interval after which the
		/// client should start polling the database for results.
		/// </summary>
		public TimeSpan StartPollingForResultAfter
		{
			get
			{
				return _startPollingAfter;
			}
			set
			{
				_startPollingAfter = value;
			}
		}

		/// <summary>
		/// A value indicating the priority
		/// that the current request will have in the 
		/// queue once it is enqueued.
		/// </summary>
		public QueuePriorityLevel QueuePriority
		{
			get
			{
				return _queuePriority;
			}
			set
			{
				_queuePriority = value;
				UsePreDefinedQueuePriority = false;
			}
		}

		/// <summary>
		/// A value indicating if
		/// the request will get the queue
		/// priority from the batch definition.
		/// </summary>
		public bool UsePreDefinedQueuePriority
		{
			get
			{
				return _usePreDefinedQueuePriority;
			}
			set
			{
				_usePreDefinedQueuePriority = value;
			}
		}

		/// <summary>
		/// Gets the name of the Destination
		/// that will dequeue the request and
		/// process it.
		/// </summary>
		public string Destination
		{
			get
			{
				return _destination;
			}
			set
			{
				_destination = value;
				UsePreDefinedDestination = false;
			}
		}

		/// <summary>
		/// Indicates whether or not to use a predefined
		/// destination. Returns false if the <see cref="Destination"/>
		/// property is set to a non-empty string.
		/// </summary>
		public bool UsePreDefinedDestination
		{
			get
			{
				return _usePreDefinedDestination;
			}
			set
			{
				_usePreDefinedDestination = value;
			}
		}

		/// <summary>
		/// Returns the parent request if there is one.
		/// Returns null otherwise.
		/// </summary>
		public BatchExecutionRequest ParentRequest
		{
			get
			{
				return _parentRequest;
			}
			set
			{
				_parentRequest = value;
			}
		}

		/// <summary>
		/// Gets the unique ID for the requests. 
		/// This is a unique identifier for an instance of
		/// a given batch execution.
		/// </summary>
		public Guid Key
		{
			get
			{
				return _key;
			}
		}

        /// <summary>
        /// Sets the current <see cref="BatchExecutionRequest"/>
        /// object's unique identifier.
        /// </summary>
        /// <param name="key">System.Guid</param>
        /// <returns>void</returns>
		protected internal virtual void SetKey(Guid key)
		{
			_key = key;
		}

		/// <summary>
		/// Gets the initial timestamp of the
		/// current request.
		/// </summary>
		public DateTime TimeStamp
		{
			get
			{
				return _timeStamp;
			}
		}

		/// <summary>
		/// Gets or sets a collection of parameters specific
		/// to the execution of the current request.
		/// </summary>
		public ParameterCollection Parameters
		{
			get
			{
				return _parameters;
			}
			set
			{
				_parameters = value;
			}
		}

        /// <summary>
        /// Serializes the request to the specified file
        /// </summary>
        /// <param name="filePath">Path of file where request should be saved.
        /// </param>
        /// <returns>void</returns>
		public virtual void Serialize(string filePath)
		{
            lock (sync)
            {
                Stream stream = null;
                try
                {
                    stream = File.Create(filePath);
                    XmlSerializer serializer = null;

                    if (this.UsePreDefinedDestination &&
                        this.UsePreDefinedQueuePriority)
                    {
                        serializer = OverrideSerializer;
                    }
                    else if (this.UsePreDefinedDestination)
                    {
                        serializer = IgnoreDestinationSerializer;
                    }
                    else if (this.UsePreDefinedQueuePriority)
                    {
                        serializer = IgnoreQueuePrioritySerializer;
                    }
                    else
                    {
                        serializer = Serializer;
                    }

                    object temp = CreateSerializeableRequest();
                    serializer.Serialize(stream, temp);
                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Close();
                    }
                }
            }
		}

        /// <summary>
        /// Creates the serializable representation of the 
        /// current <see cref="BatchExecutionRequest"/> object.
        /// </summary>
        /// <returns>object</returns>
		protected virtual object CreateSerializeableRequest()
		{
			return new RequestInternal(this);
		}

        /// <summary>
        /// Serializes the specified request
        /// object to the file located at the specified 
        /// path
        /// </summary>
        /// <param name="filePath">string</param>
        /// <param name="request">Avanade.ACA.Batch.BatchExecutionRequest</param>
        /// <returns>void</returns>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static void Serialize(string filePath, 
									BatchExecutionRequest request)
		{
			request.Serialize(filePath);
		}

        /// <summary>Ignore</summary>
        /// <param name="propertyName">string</param>
        /// <param name="overrides">System.Xml.Serialization.XmlAttributeOverrides</param>
        /// <returns>void</returns>
        private static void Ignore(string propertyName, 
			XmlAttributeOverrides overrides)
		{
			Type requestType = typeof(RequestInternal);
			XmlAttributes attributes = new XmlAttributes();
			attributes.XmlIgnore = true;
			overrides.Add(requestType, propertyName, attributes);
		}

        /// <summary>
        /// Deserializes the Request object
        /// from the XML file located at the specified path.
        /// </summary>
        /// <param name="filePath">string</param>
        /// <returns>Avanade.ACA.Batch.BatchExecutionRequest</returns>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static BatchExecutionRequest Deserialize(string filePath)
		{
            Stream stream = null;

            try
            {
                stream = File.OpenRead(filePath);

                RequestInternal request =
                    (RequestInternal)Serializer.Deserialize(stream);                
               
                return request.InnerRequest;
            }            
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
		}

        /// <summary>
		/// An <see cref="XmlSerializer"/> is 
		/// an expensive object to create. An
		/// XmlSerializer for the Request type
		/// is created and saved in memory for 
		/// the life of the current AppDomain.
		/// </summary>
		private static XmlSerializer Serializer
		{
			[MethodImpl(MethodImplOptions.Synchronized)]	
			get
			{
				if (_serializer == null)
				{
					_serializer = new XmlSerializer(typeof(RequestInternal));	
				}
				return _serializer;
			}
		}

		private static XmlSerializer overrideSerializer;

		private static XmlSerializer OverrideSerializer
		{
			[MethodImpl(MethodImplOptions.Synchronized)]	
			get
			{
				if (overrideSerializer == null)
				{
					XmlAttributeOverrides overrides = new XmlAttributeOverrides();
					Ignore("Destination", 
						overrides);
					Ignore("QueuePriority", 
						overrides);
					overrideSerializer = new XmlSerializer(typeof(RequestInternal), overrides);	
				}
				return overrideSerializer;
			}
		}

		private static XmlSerializer ignoreDestinationSerializer;

		private static XmlSerializer IgnoreDestinationSerializer
		{
			[MethodImpl(MethodImplOptions.Synchronized)]	
			get
			{
				if (ignoreDestinationSerializer == null)
				{
					XmlAttributeOverrides overrides = new XmlAttributeOverrides();
					Ignore("Destination", 
						overrides);
					ignoreDestinationSerializer = new XmlSerializer(typeof(RequestInternal), overrides);	
				}
				return ignoreDestinationSerializer;
			}
		}

		private static XmlSerializer ignoreQueuePrioritySerializer;

		private static XmlSerializer IgnoreQueuePrioritySerializer
		{
			[MethodImpl(MethodImplOptions.Synchronized)]	
			get
			{
				if (ignoreQueuePrioritySerializer == null)
				{
					XmlAttributeOverrides overrides = new XmlAttributeOverrides();
					Ignore("QueuePriority", 
						overrides);
					ignoreQueuePrioritySerializer = new XmlSerializer(typeof(RequestInternal), overrides);	
				}
				return ignoreQueuePrioritySerializer;
			}
		}


		/// <summary>
		/// This type supports the ACA Batch Framework 
		/// infrastructure and is not intended to be 
		/// used directly from your code.
		/// </summary>
		[XmlRoot("batchExecutionRequest")]
		[XmlInclude(typeof(DequeuedBatchExecutionRequest.DequeuedRequestInternal))]
		public class RequestInternal
		{
			private BatchExecutionRequest	_innerRequest;

            /// <summary>
            /// For internal ACA Batch framework use only.
            /// </summary>
			public RequestInternal()
			{
				_innerRequest = 
					new BatchExecutionRequest();
			}

            /// <summary>
            /// For internal ACA.NET framework use only.
            /// </summary>
            /// <param name="innerRequest">Avanade.ACA.Batch.BatchExecutionRequest</param>
			public RequestInternal(BatchExecutionRequest innerRequest)
			{
				_innerRequest = innerRequest;
			}

			/// <summary>
			/// Property for Xml serialization purpose.
			/// </summary>
            //[XmlArray("parameters")]
            //[XmlArrayItem("parameter")]
            [XmlIgnore]
			public ParameterData[] Parameters
			{
				get
				{
					ParameterData[] parameters;
					ParameterCollection parmColl = _innerRequest.Parameters;
					if (parmColl != null)
					{
						parameters = new ParameterData[parmColl.Count];	
						parmColl.CopyTo(parameters, 0);
					}
					else
					{
						parameters = new ParameterData[0];
					}
					return parameters;
				}
				set
				{
					if (value != null)
					{
						InnerRequest.Parameters = new ParameterCollection();
						foreach (ParameterData data in value)
						{
							InnerRequest.Parameters.Add(data);
						}
					}
				}
			}

            /// <summary>
            /// Property for Xml serialization purpose.
            /// </summary>
            [XmlArray("parameters")]
            [XmlArrayItem("parameter")]            
            public ParameterDataProxy[] ParameterProxies
            {
                get
                {
                    ParameterData[] parameters = this.Parameters;
                    ParameterDataProxy[] parameterDataProxies = new ParameterDataProxy[parameters.Length];
                    for (int i = 0; i < parameters.Length; i++ )
                    {
                        ParameterData parameterData = parameters[i];
                        parameterDataProxies[i] = parameterData.ParameterDataProxy;
                        if (parameterData.GetType() == typeof(FileParameterArrayData))
                        {
                            FileParameterData[] children = ((FileParameterArrayData)parameters[i]).Items;
                            FileParameterArrayDataProxy fileParameterArrayDataProxy = (FileParameterArrayDataProxy)parameterDataProxies[i];
                            fileParameterArrayDataProxy.Items = new FileParameterDataProxy[children.Length];
                            for (int j = 0; j < children.Length; j++)
                            {
                                FileParameterData child = children[j];
                                FileParameterDataProxy fileParameterDataProxy = (FileParameterDataProxy)child.ParameterDataProxy;                                   
                                fileParameterArrayDataProxy.Items[j] = fileParameterDataProxy;
                            }                            
                        }
                    }
                    return parameterDataProxies;
                }
                set
                {
                    if (value != null)
                    {
                        ParameterData[] parameters = new ParameterData[value.Length];
                        for (int i = 0; i < value.Length; i++)
                        {
                            ParameterDataProxy proxy = value[i];
                            ParameterData parameterData = null;
                            switch (proxy.GetType().Name)
                            {
                                case "FileParameterDataProxy":
                                    parameterData = new FileParameterData();
                                    ParameterData.Clone(proxy, parameterData);
                                    break;
                                case "CharacterSeparatedFileParameterDataProxy":
                                    parameterData = new CharacterSeparatedFileParameterData();
                                    ParameterData.Clone(proxy, parameterData);
                                    break;
                                case "FileParameterArrayDataProxy":
                                    parameterData = new FileParameterArrayData();
                                    ParameterData.Clone(proxy, parameterData);
                                    break;
                                case "FileSetParameterDataProxy":
                                    parameterData = new FileSetParameterData();
                                    ParameterData.Clone(proxy, parameterData);
                                    break;
                                case "FixedWidthFileParameterDataProxy":
                                    parameterData = new FixedWidthFileParameterData();
                                    ParameterData.Clone(proxy, parameterData);
                                    break;
                                case "XmlFileParameterDataProxy":
                                    parameterData = new XmlFileParameterData();
                                    ParameterData.Clone(proxy, parameterData);
                                    break;
                                default:
                                    parameterData = new ParameterData();
                                    ParameterData.Clone(proxy, parameterData);
                                    break;
                            }
                            parameters[i] = parameterData;
                        }
                        this.Parameters = parameters;
                    }
                }
            }

			/// <summary>
			/// For internal ACA.NET framework use only.
			/// </summary>
			[XmlIgnore]
			public BatchExecutionRequest InnerRequest
			{
				get
				{
					return _innerRequest;
				}
			}

            /// <summary>
            /// For internal ACA.NET framework use only.
            /// </summary>
            /// <param name="request">Avanade.ACA.Batch.BatchExecutionRequest</param>
            /// <returns>void</returns>
			protected internal void SetInnerRequest(BatchExecutionRequest request)
			{
				_innerRequest = request;
			}

			/// <summary>
			/// For internal ACA.NET framework use only.
			/// </summary>
			[XmlAttribute("batchId")]
			public string BatchId
			{
				get
				{
					return _innerRequest.Key.ToString();
				}
				set
				{
					Guid key = new Guid(value);
					_innerRequest.SetKey(key);
				}
			}

			/// <summary>
			/// Gets or sets the name of the batch client.
			/// </summary>
			[XmlAttribute("batchClientName")]
			public string BatchClientName
			{
				get
				{
					return _innerRequest.BatchClientName;
				}
				set
				{
					_innerRequest.BatchClientName = value;
				}
			}

			/// <summary>
			/// For internal ACA.NET framework use only.
			/// </summary>
			[XmlAttribute("batchName")]
			public string BatchName
			{
				get
				{
					return _innerRequest.BatchName;
				}
				set
				{
					_innerRequest._batchName = value;
				}
			}

			/// <summary>
			/// The amount of time the request will remain in the queue
			/// before it expires and is dropped. 
			/// Specify <see cref="TimeSpan.MaxValue"/>
			/// to guarantee that the request will never expire.
			/// </summary>
			[XmlAttribute("queueTimeout")]
			public string QueueTimeout
			{
				get
				{
					return _innerRequest.QueueTimeout.ToString();
				}
				set
				{
					_innerRequest.QueueTimeout = TimeSpan.Parse(value);
				}
			}

			/// <summary>
			/// Gets a value indicating the priority
			/// that the current request will have in the 
			/// queue once it is enqueued.
			/// </summary>
			[XmlAttribute("queuePriority")]
			public QueuePriorityLevel QueuePriority
			{
				get
				{
					return _innerRequest.QueuePriority;
				}
				set
				{
					_innerRequest.QueuePriority = value;
				}
			}

			/// <summary>
			/// Gets the name of the Destination
			/// that will dequeue the request and
			/// process it.
			/// </summary>
			[XmlAttribute("destination")]
			public string Destination
			{
				get
				{
					return _innerRequest.Destination;
				}
				set
				{
					_innerRequest.Destination = value;
				}
			}

			/// <summary>
			/// For internal ACA.NET framework use only.
			/// </summary>
			[XmlAttribute("startPollingAfterTime")]
			public long StartPollingAfterTime
			{
				get
				{
					return _innerRequest.StartPollingForResultAfter.Ticks;
				}
				set
				{
					_innerRequest.StartPollingForResultAfter = TimeSpan.FromTicks(value);
				}
			}

			/// <summary>
			/// Gets the interval at which the 
			/// client will poll for a response
			/// after enqueueing the current request.
			/// </summary>
			[XmlAttribute("requestId")]
			public string Id
			{
				get
				{
					return _innerRequest.Key.ToString();
				}
				set
				{
					_innerRequest.SetKey(new Guid(value));
				}
			}
		}
	}
}
