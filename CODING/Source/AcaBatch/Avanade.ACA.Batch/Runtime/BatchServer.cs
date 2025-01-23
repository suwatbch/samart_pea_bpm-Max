// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Avanade.ACA.Batch.Configuration;
using Avanade.ACA.Batch.Instrumentation;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Avanade.ACA.Batch
{

	/// <summary>
	/// Represents an object that dequeues and
	/// processes <see cref="BatchExecutionRequest"/> objects.
	/// It considers the availability of
	/// CPU cycles and physical memory before
	/// processing a batch execution request.
	/// </summary>
	public class BatchServer : MarshalByRefObject
	{
		// Documentation says that Performance counters are thread-safe
		private static PerformanceCounter _availableMemoryCounter;
		private static PerformanceCounter _processorTimeCounter;

		private BatchServerSettingsData			_serverSettings;
		private IBatchExecutionRequestQueue	_queue;
		private Timer						_timer;
		private BatchServerState			_state;
		private	bool						_throttled;
		private string						_destinationCombined;

        /// <summary>
        /// Loads the performance counter objects
        /// </summary>
		static BatchServer()
		{
			string machineName = Environment.MachineName;

			_availableMemoryCounter	= 
				new PerformanceCounter("Memory",
				"Available MBytes",
				String.Empty,
				machineName);
			_processorTimeCounter = 
				new PerformanceCounter("Processor",
				"% Processor Time",
				"_Total",
				machineName);
		}

        /// <summary>
        /// Initializes a BatchServer object with an in-memory </summary>
        /// <param name="configurationSource">Microsoft.Practices.EnterpriseLibrary.Common.Configuration.IConfigurationSource</param>
        public BatchServer(IConfigurationSource configurationSource)
		{
            BatchSettings batchSettings = (BatchSettings)configurationSource.GetSection(BatchSettings.SectionName);
			string databaseName = batchSettings.BatchServerSettings.DatabaseInstanceName;

			DatabaseProviderFactory factory = new DatabaseProviderFactory(configurationSource);			
            Database database = factory.Create(databaseName);            
            Initialize(batchSettings.BatchServerSettings, database);
		}

        /// <summary>
        /// Initializes a new instance of the </summary>
        /// <param name="serverSettings">The </param>
		public BatchServer(BatchServerSettingsData serverSettings)
		{
			string databaseName = serverSettings.DatabaseInstanceName;
			Database database = DatabaseFactory.CreateDatabase(databaseName);
			
			Initialize(serverSettings, database);
		}

        /// <summary>Initialize</summary>
        /// <param name="serverSettings">Avanade.ACA.Batch.Configuration.BatchServerSettingsData</param>
        /// <param name="database">Microsoft.Practices.EnterpriseLibrary.Data.Database</param>
        /// <returns>void</returns>
		private void Initialize(BatchServerSettingsData	serverSettings, Database database)
		{   
			_destinationCombined = string.Empty;
			_serverSettings		= serverSettings;
			_queue			= new BatchExecutionRequestQueue(database);
			_state			= BatchServerState.Initialized;

			TimerCallback callback = new TimerCallback(OnPollIntervalElapsed);

			// initialize the timer, but don't
			// signal it to start
			_timer = new Timer(callback,
				null,
				Timeout.Infinite,
				Timeout.Infinite);

			_throttled = false;			
		}

        /// <summary>
        /// Signals the server to begin
        /// polling the queue for requests.
        /// </summary>
        /// <returns>void</returns>
		public virtual void Start()
		{
			lock (this)
			{
				_timer.Change(TimeSpan.Zero, PollInterval);
				_state = BatchServerState.Started;
				_throttled = false;
			}            
            BatchInstrumentationProvider.Instance.FireBatchServerHostEvent(DestinationString, true);
		}

        /// <summary>
        /// Stops the server so that it will not dequeue
        /// any more requests. Currently executing requests 
        /// will continue.
        /// </summary>
        /// <returns>void</returns>
		public virtual void Stop()
		{
			if (_throttled)
			{
				BatchServerThrottledEvent.ClearThrottled();
				_throttled = false;
			}

			lock (this)
			{
				_timer.Change(Timeout.Infinite, Timeout.Infinite);
				_state = BatchServerState.Stopped;
			}            
            BatchInstrumentationProvider.Instance.FireBatchServerHostEvent(DestinationString, false);
		}

		/// <summary>
		/// Gets the interval at which
		/// the server will dequeue requests.
		/// </summary>
		public TimeSpan PollInterval
		{
			get
			{
				return _serverSettings.PollInterval;
			}
			set
			{
				lock (this)
				{
					_serverSettings.PollInterval = value;
					_timer.Change(TimeSpan.Zero, value);
				}
			}
		}

		/// <summary>
		/// Gets a filter used in as a condition
		/// in the dequeuing algorithm that indicates
		/// the destination pools from which the current server
		/// will dequeue requests.
		/// </summary>
		public string[] Destinations
		{
            //get
            //{
            //    return _serverSettings.Destinations;
            //}
            //set
            //{
            //    _serverSettings.Destinations = value;
            //}
            get
            {
                return _serverSettings.Destinations.Split(',');
            }
            set
            {
                string[] destinations = value;
                foreach (string destination in destinations)
                    _serverSettings.Destinations += destination + ",";
            }
		}

        /// <summary>
        /// Dequeues a request and sets up the
        /// Batch Execution's isolation environment.
        /// </summary>
        /// <returns>void</returns>
		protected virtual void DequeueAndProcessRequest()
		{
			try
			{
				if (HasAvailableResources())
				{
					if (_throttled)
					{
						BatchServerThrottledEvent.ClearThrottled();
						_throttled = false;
					}

                    //False tolalance checking feature
                    string databaseName = _serverSettings.DatabaseInstanceName;
                    Database database = DatabaseFactory.CreateDatabase(databaseName);
                    BatchExecutionServerSwitcher batchServerSwitch = new BatchExecutionServerSwitcher(database, Destinations);
                    if (!batchServerSwitch.ExecuteServer())
                    {
                        return;
                    }

					// Get a request off of the queue
					// if the queue is not empty

					// specify a filter that allows 
					// the server to only dequeue batches
					// that are intended for it. This
					// fulfills the machine-affinity
					// requirement
					DequeuedBatchExecutionRequest request =
						Queue.Dequeue(Destinations, ServerName);

					// check if there is a request
					// on the queue that this instance can
					// process
					if (request != null)
					{
						// get an instance of the
						// requested isolation environment
						BatchExecutionIsolationStrategy isolationStrategy = 
							GetIsolationStrategy(request);

						isolationStrategy.Execute(request.Key, request.ConfigurationFilePath);
					}
				}
				else
				{
					if (!_throttled)
					{
						BatchServerThrottledEvent.SetThrottled();
						_throttled = true;
					}
				}
			}
			catch (Exception e)
			{                
                BatchInstrumentationProvider.Instance.FireBatchArchitectureFailedEvent("Batch Server failed to dequeue and process a request.", e);
			}
		}

        /// <summary>
        /// The delegate that is invoked on by
        /// a thread on the ThreadPool when
        /// the timer interval elapses.
        /// </summary>
        /// <param name="state">object</param>
        /// <returns>void</returns>
		private void OnPollIntervalElapsed(object state)
		{
			DequeueAndProcessRequest();
		}

		/// <summary>
		/// Gets the <see cref="Timer"/> object
		/// that signals the server to begin
		/// dequeueing and processing another
		/// request.
		/// </summary>
		protected Timer Timer
		{
			get
			{
				return _timer;
			}
		}

		/// <summary>
		/// Gets the current state of the 
		/// <see cref="BatchServer"/>.
		/// </summary>
		/// <value>One of the <see cref="BatchServerState"/> values.</value>
		public BatchServerState State
		{
			get
			{
				return _state;
			}
		}

		/// <summary>
		/// Gets the <see cref="IBatchExecutionRequestQueue"/>
		/// object that the server is using
		/// to dequeue requests.
		/// </summary>
		protected IBatchExecutionRequestQueue Queue
		{
			get
			{
				return _queue;
			}
		}

        /// <summary>
        /// Maps a <see cref="DequeuedBatchExecutionRequest"/>
        /// to a <see cref="BatchExecutionIsolationStrategy"/> object.
        /// </summary>
        /// <param name="request">Avanade.ACA.Batch.DequeuedBatchExecutionRequest</param>
        /// <returns>Avanade.ACA.Batch.BatchExecutionIsolationStrategy</returns>
		protected virtual BatchExecutionIsolationStrategy 
			GetIsolationStrategy(DequeuedBatchExecutionRequest request)
		{
			string executionHostPath =
				ServerNode.ExecutionHostFileName;

			BatchExecutionIsolationStrategy isolationStrategy = 
				new ProcessIsolationStrategy(executionHostPath, request.ExecutionPriority);
			
			return isolationStrategy;
		}

		/// <summary>
		/// Gets the <see cref="BatchServerSettings"/> that 
		/// contains the configuration properties for
		/// the current <see cref="BatchServer"/>.
		/// </summary>
		public BatchServerSettingsData ServerNode
		{
			get
			{
				return _serverSettings;
			}

		}

		/// <summary>
		/// Gets the symbolic name of the server.
		/// </summary>
		public string ServerName
		{
			get
			{
                return _serverSettings.ServerName;
			}
		}

        /// <summary>
        /// Takes a sample of the system's
        /// current CPU and memory usage. 
        /// If the sample is within the
        /// configured limits, then the
        /// method returns true. Otherwise it
        /// returns false;
        /// </summary>
        /// <returns>bool</returns>
		protected virtual bool HasAvailableResources()
		{
			float availableMemorySample	= 0;
			float processorTimeSample	= 0;

			// Get the time to finish the sample
			DateTime sampleCompleteDate =
				DateTime.Now + _serverSettings.SamplingInterval;

			float i = 0;

			while(DateTime.Now < sampleCompleteDate)
			{
				Thread.Sleep(_serverSettings.SamplingFrequency);

				// get the Memory utilization performance counter
				// and check that it's below the limit.
				availableMemorySample += _availableMemoryCounter.NextValue();

				// get the CPU utilization performance counter
				// and check that it's below the limit.
				processorTimeSample += _processorTimeCounter.NextValue();

				i++;
			}

			// if no samples were taken, then
			// return true.
			if (i == 0)
			{
				return true;
			}

			// average out the values
			float averageAvailableMemory = availableMemorySample / i;
			float averageProcessorTime	= processorTimeSample / i;

			// check if the average is within the configured limits
			return  (averageAvailableMemory >= RequiredAvailableMemory
				&& averageProcessorTime <= CpuLimit);
		}

		/// <summary>
		/// The percentage of CPU utilization that will cause the 
		/// server to stop proccessing requests.
		/// </summary>
		/// <value>A number from 1 to 100.</value>
		public short CpuLimit
		{
			get
			{
				return _serverSettings.CpuLimit;
			}
		}

		/// <summary>
		/// The amount of available system memory
		/// required in order for the server
		/// to continue processing requests.
		/// </summary>
		/// <value>An integer that represents memory in megabytes.</value>
		public int RequiredAvailableMemory
		{
			get
			{
				return _serverSettings.RequiredAvailableMemory;
			}
		}

		private string DestinationString
		{
			get
			{
				if (_destinationCombined == string.Empty)
				{                    
					foreach (string dest in _serverSettings.Destinations.Split(','))
					{
						_destinationCombined += dest + ";";
					}
				}
				return _destinationCombined;
			}
		}
	}
}
