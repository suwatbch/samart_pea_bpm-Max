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

namespace Avanade.ACA.Batch.Configuration
{
	/// <summary>
	/// Base class for <see cref="Microsoft.Practices.EnterpriseLibrary.Logging.Filters.ILogFilter"/> configuration objects.
	/// </summary>
	/// <remarks>
	/// This class should be made abstract, but in order to use it in a NameTypeConfigurationElementCollection
	/// it must be public and have a no-args constructor.
	/// </remarks>
    public class BatchExecutionSettingsData : ConfigurationElement
	{
        private const string databaseInstanceNameProperty = "databaseInstanceName";

        /// <summary>BatchExecutionSettingsData</summary>
        public BatchExecutionSettingsData() : this(string.Empty)
        {
        }

        /// <summary>BatchExecutionSettingsData</summary>
        /// <param name="databaseInstanceName">System.String</param>
        public BatchExecutionSettingsData(string databaseInstanceName) : base()
        {
            this[databaseInstanceNameProperty] = databaseInstanceName;
        }

        [ConfigurationProperty(databaseInstanceNameProperty, IsRequired = true)]
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
	}
}
