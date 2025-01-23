//===============================================================================
// Microsoft patterns & practices Enterprise Library
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Configuration;
using System.Collections.Specialized;

namespace Avanade.ACA.Batch.Configuration
{
	/// <summary>
	/// Base class for <see cref="Microsoft.Practices.EnterpriseLibrary.Logging.Filters.ILogFilter"/> configuration objects.
	/// </summary>
	/// <remarks>
	/// This class should be made abstract, but in order to use it in a NameTypeConfigurationElementCollection
	/// it must be public and have a no-args constructor.
	/// </remarks>
	public class BatchServerSettingsData : ConfigurationElement
	{
        /// <summary>
        /// Value of 60.
        /// </summary>
        public const short DefaultCpuLimit = 80;
        /// <summary>
        /// Value of 24.
        /// </summary>
        public const int DefaultAvailableMemory = 24;
        /// <summary>
        /// Sets to 1 minute.
        /// </summary>
        public const double DefaultPollIntervalMin = 1;
        /// <summary>
        /// Sets to 5 second.
        /// </summary>
        public const double DefaultSamplingIntervalSec = 5;
        /// <summary>
        /// Sets to 500 m. sec.
        /// </summary>
        public const double DefaultSamplingFrequencyMSec = 500;

        private const string serverNameProperty = "serverName";
        private const string databaseInstanceNameProperty = "databaseInstanceName";
        private const string requiredAvailableMemoryProperty = "requiredAvailableMemory";
        private const string destinationsProperty = "destinations";
        private const string executionHostProperty = "executionHost";
        private const string cpuLimitProperty = "cpuLimit";
        private const string pollIntervalProperty = "pollInterval";
        private const string samplingIntervalProperty = "samplingInterval";
        private const string samplingFrequencyProperty = "samplingFrequency";

        /// <summary>
        /// Initializes a new instance of </summary>
		public BatchServerSettingsData() : this(string.Empty)
		{
		}

        /// <summary>
        /// Initializes a new instance of </summary>
        /// <param name="serverName">System.String</param>
        public BatchServerSettingsData(string serverName) : base()
        {
            this[serverNameProperty] = serverName;
            this[cpuLimitProperty] = DefaultCpuLimit;
            this[requiredAvailableMemoryProperty] = DefaultAvailableMemory;
            this[pollIntervalProperty] = TimeSpan.FromMinutes(DefaultPollIntervalMin);
            this[samplingIntervalProperty] = TimeSpan.FromSeconds(DefaultSamplingIntervalSec);
            this[samplingFrequencyProperty] = TimeSpan.FromMilliseconds(DefaultSamplingFrequencyMSec);
        }

        /// <summary>
        /// Initializes a new instance of </summary>
        /// <param name="serverName">System.String</param>
        /// <param name="cpulimit">System.Int16</param>
        /// <param name="databaseInstanceName">System.String</param>
        /// <param name="requiredAvailableMemory">System.Int32</param>
        /// <param name="destinations">string[]</param>
        /// <param name="executionHost">System.String</param>
        /// <param name="pollInterval">System.TimeSpan</param>
        /// <param name="samplingInterval">System.TimeSpan</param>
        /// <param name="samplingFrequency">System.TimeSpan</param>
        public BatchServerSettingsData(string serverName, short cpulimit, string databaseInstanceName, int requiredAvailableMemory,
        string[] destinations, string executionHost, TimeSpan pollInterval, TimeSpan samplingInterval, TimeSpan samplingFrequency)
            : base()
        {
            this[serverNameProperty] = serverName;
            this[databaseInstanceNameProperty] = databaseInstanceName;
            this[requiredAvailableMemoryProperty] = requiredAvailableMemory;
            this[destinationsProperty] = destinations;
            this[executionHostProperty] = executionHost;
            this[cpuLimitProperty] = cpulimit;
            this[pollIntervalProperty] = pollInterval;
            this[samplingIntervalProperty] = samplingInterval;
            this[samplingFrequencyProperty] = samplingFrequency;

        }

        /// <summary>
        /// Gets or sets the configured database instance for the Batch database.
        /// </summary>
        [ConfigurationProperty(serverNameProperty)]
        public string ServerName
        {
            get
            {
                return this[serverNameProperty].ToString();
            }
            set
            {
                this[serverNameProperty] = value;
            }
        }
		
		/// <summary>
        /// Gets or sets the configured database instance for the Batch database.
        /// </summary>
        [ConfigurationProperty(databaseInstanceNameProperty)]
        public string DatabaseInstanceName
        {
            get
            {
                return this[databaseInstanceNameProperty].ToString();
            }
            set
            {
                this[databaseInstanceNameProperty] = value;
            }
        }
		
        /// <summary>
        /// Gets or sets the available memory, when the system exceeds, for the Batch server
        /// becomes throttled.
        /// </summary>
        [ConfigurationProperty(requiredAvailableMemoryProperty)]
        public int RequiredAvailableMemory
        {
            get 
            { 
                return (int)this[requiredAvailableMemoryProperty]; 
            }
            set 
            { 
                this[requiredAvailableMemoryProperty] = value; 
            }
        }

        /// <summary>
        /// Gets or sets the symbolic destination for the Batch server.  It is used as a filter
        /// for the requests in the queue.
        /// </summary>
        [ConfigurationProperty(destinationsProperty)]
        public string Destinations
        {
            get 
            {
                return this[destinationsProperty].ToString(); 
            }
            set 
            {
                this[destinationsProperty] = value; 
            }
        }

        /// <summary>
        /// Gets or sets the full path name of the BatchExecutionHost application.
        /// </summary>
        [ConfigurationProperty(executionHostProperty)]
        public string ExecutionHostFileName
        {
            get 
            {
                return this[executionHostProperty].ToString(); 
            }
            set 
            { 
                this[executionHostProperty] = value; 
            }
        }

        /// <summary>
        /// Gets or sets the CPU usage limit, when exceeds, for the Batch server to become
        /// throttle.
        /// </summary>
        [ConfigurationProperty(cpuLimitProperty)]
        public short CpuLimit
        {
            get 
            { 
                return (short)this[cpuLimitProperty]; 
            }
            set 
            { 
                this[cpuLimitProperty] = value; 
            }
        }

        /// <summary>
        /// Gets or sets the polling interval in a string format for the Batch server to try to dequeue
        /// the request.
        /// </summary>
        [ConfigurationProperty(pollIntervalProperty)]
        public TimeSpan PollInterval
        {
            get
            {
                return (TimeSpan)this[pollIntervalProperty]; 
            }
            set 
            { 
                this[pollIntervalProperty] = value;
            }
        }

        /// <summary>
        /// Gets or sets the time interval in a string format for the Batch server to measure the system's
        /// performance counter in terms of available memory and CPU usage in able
        /// to determine whether to get throttled. 
        /// </summary>
        [ConfigurationProperty(samplingIntervalProperty)]
        public TimeSpan SamplingInterval
        {
            get 
            {
                return (TimeSpan)this[samplingIntervalProperty]; 
            }
            set 
            {
                this[samplingIntervalProperty] = value; 
            }
        }

        /// <summary>
        /// Gets or sets the frequency in a string format for the Batch server to measure the system's
        /// performance counter in terms of available memory and CPU usage in able
        /// to determine whether to get throttled. 
        /// </summary>
        [ConfigurationProperty(samplingFrequencyProperty)]
        public TimeSpan SamplingFrequency
        {
            get 
            {
                return (TimeSpan)this[samplingFrequencyProperty]; 
            }
            set 
            { 
                this[samplingFrequencyProperty] = value; 
            }
        }
	}
}
