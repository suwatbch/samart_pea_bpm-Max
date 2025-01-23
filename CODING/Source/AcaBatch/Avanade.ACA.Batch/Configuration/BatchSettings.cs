// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================

using System.ComponentModel;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Avanade.ACA.Batch.Configuration
{
    public class BatchSettings : SerializableConfigurationSection
    {
        private const string batchServerSettingsProperty = "batchServerSettings";
        private const string batchExecutionSettingsProperty = "batchExecutionSettings";
        private const string batchClientSettingsProperty = "batchClientSettings";
        private const string batchDatabaseSettingsProperty = "batchDatabaseSettings";

        public const string SectionName = "avanade.aca.batch";

        /// <summary>
        /// Creates a new instance of <see cref="BatchSettings"/></summary>
        public BatchSettings()
        {            
        }

        /// <summary>
        /// Gets the </summary>
        /// <param name="configurationSource">The </param>
        /// <returns>Avanade.ACA.Batch.Configuration.BatchSettings</returns>
        public static BatchSettings GetBatchSettings(IConfigurationSource configurationSource)
        {
            return (BatchSettings)configurationSource.GetSection(BatchSettings.SectionName);
        }

        /// <summary>
        /// Gets the Batch Client Settings.
        /// </summary>
        [ConfigurationProperty(batchServerSettingsProperty)]
        public BatchServerSettingsData BatchServerSettings
        {
            get
            {
                return (BatchServerSettingsData)this[batchServerSettingsProperty];
            }
            set
            {
                this[batchServerSettingsProperty] = value;
            }
        }

        /// <summary>
        /// Gets the Batch Client Settings.
        /// </summary>
        [ConfigurationProperty(batchExecutionSettingsProperty)]
        public BatchExecutionSettingsData BatchExecutionSettings
        {
            get
            {
                return (BatchExecutionSettingsData) this[batchExecutionSettingsProperty];
            }
            set
            {
                this[batchExecutionSettingsProperty] = value;
            }
        }
        
        /// <summary>
        /// Gets the Batch Client Settings.
        /// </summary>
        [ConfigurationProperty(batchClientSettingsProperty)]
        public BatchClientSettingsData BatchClientSettings
        {
            get
            {
                return (BatchClientSettingsData)this[batchClientSettingsProperty];
            }
            set
            {
                this[batchClientSettingsProperty] = value;
            }
        }

        /// <summary>
        /// Gets the Batch Database Settings.
        /// </summary>
        [ConfigurationProperty(batchDatabaseSettingsProperty)]
        public BatchDatabaseSettingsData BatchDatabaseSettings
        {
            get
            {
                return (BatchDatabaseSettingsData)this[batchDatabaseSettingsProperty];
            }
            set
            {
                this[batchDatabaseSettingsProperty] = value;
            }
        }
    }
}
