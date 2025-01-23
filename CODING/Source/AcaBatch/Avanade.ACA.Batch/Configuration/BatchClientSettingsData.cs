using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Avanade.ACA.Batch.Configuration
{
    public class BatchClientSettingsData : ConfigurationElement
    {
        private const string databaseInstanceNameProperty = "databaseInstanceName";
        private const string executablePathProperty = "executablePath";
        private const string exitCodeStrategyProperty = "exitCodeStrategy";
        private const string pollIntervalProperty = "pollInterval";
        private const string requestCommandProperty = "requestCommands";

        /// <summary>BatchClientSettingsData</summary>
        public BatchClientSettingsData() : this(string.Empty)
        {            
        }

        /// <summary>BatchClientSettingsData</summary>
        /// <param name="databaseInstanceName">System.String</param>
        public BatchClientSettingsData(string databaseInstanceName) : base()
        {
            this[databaseInstanceNameProperty] = databaseInstanceName;
        }

        /// <summary>
        /// The name of the configured instance for accessing the Batch database.
        /// </summary>
        [ConfigurationProperty(databaseInstanceNameProperty)]
        public string DatabaseInstanceName
        {
            get { return this[databaseInstanceNameProperty].ToString(); }
            set { this[databaseInstanceNameProperty] = value; }
        }

        /// <summary>
        /// The path to the executable (.exe) file that enqueues the Batch request.
        /// </summary>
        [ConfigurationProperty(executablePathProperty)]
        public string ExecutablePath
        {
            get { return this[executablePathProperty].ToString(); }
            set { this[executablePathProperty] = value; }
        }

        /// <summary>
        /// The type name of the class that derives from the 
        /// <see cref="IProcessExitCodeStrategy"/> class to be used.
        /// </summary>
        [ConfigurationProperty(exitCodeStrategyProperty)]
        public string ExitCodeStrategyTypeName
        {
            get { return this[exitCodeStrategyProperty].ToString(); }
            set { this[exitCodeStrategyProperty] = value; }
        }

        /// <summary>
        /// The polling interval as a string for the Batch client for the execution results.
        /// </summary>
        public TimeSpan PollInterval
        {            
            get
            {
                if (this[pollIntervalProperty].ToString().Trim() != string.Empty)
                {
                    string[] split = this[pollIntervalProperty].ToString().Split(':');
                    return new TimeSpan(Convert.ToInt32(split[0]), Convert.ToInt32(split[1]), Convert.ToInt32(split[2]));
                }
                else 
                {
                    return new TimeSpan(0);
                }
            }
            set { this[pollIntervalProperty] = value; }
        }

        /// <summary>
        /// The polling interval as a string for the Batch client for the execution results.
        /// </summary>
        [ConfigurationProperty(pollIntervalProperty)]
        public string PollIntervalString
        {
            get { return this[pollIntervalProperty].ToString(); }
            set { this[pollIntervalProperty] = TimeSpan.Parse(value); }
        }

        /// <summary>
        /// The set of <see cref="RequestCommandData"/> structures.
        /// </summary>		
        [ConfigurationProperty(requestCommandProperty)]
        public RequestCommandCollection RequestCommandCollection
        {
            get { return (RequestCommandCollection)this[requestCommandProperty]; }
            set { this[requestCommandProperty] = value; }
        }
    }
}
