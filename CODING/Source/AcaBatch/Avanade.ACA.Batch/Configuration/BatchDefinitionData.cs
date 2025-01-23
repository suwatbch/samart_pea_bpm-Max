// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Collections;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Avanade.ACA.Batch.Configuration
{
	/// <summary>
	/// BatchDefinitionData represents the configuration data for a Batch during the design-time.
	/// </summary>    
    public class BatchDefinitionData : NamedConfigurationElement, IBatchDBData
	{
		private ParameterCollection parameterCollection;
		private JobReferenceCollection jobReferenceCollection;

		private Database batchDB;
		private Guid instanceKey;
		private int maxConcurrent;
		private string configFilePath;
		private Guid batchTypeKey;
		private string batchTypeName;
		private const string displayNameProperty = "name";
		private string description;
		private Guid batchDestinationKey;
		private string batchDestinationName;
		private TimeSpan relativeExpDate;
		private BatchRestartBehavior restartBehavior;
		private ExecutionIsolationLevel isolationLevel;
		private ExecutionPriorityLevel executionPriorityLevel;
		private QueuePriorityLevel queuePriorityLevel;

        /// <summary>
        /// Initializes a new instance with empty name.
        /// </summary>
		public BatchDefinitionData() : this(string.Empty)
		{
		}

        /// <summary>
        /// Initializes a new instance with a name.
        /// </summary>
        /// <param name="displayName">The name of the Batch</param>
		public BatchDefinitionData(string displayName) : this(displayName, Guid.NewGuid())
		{
		}

        /// <summary>
        /// Initializes a new instance with a name and an unique identifier.
        /// </summary>
        /// <param name="displayName">The name of the Batch.</param>
        /// <param name="key">The key for the Batch definition.</param>
		public BatchDefinitionData(string displayName, Guid key) : base(displayName)
		{
			//this.displayName = displayName;
            this[displayNameProperty] = displayName;
			this.instanceKey = key;
			parameterCollection = new ParameterCollection();
			jobReferenceCollection = new JobReferenceCollection();

			restartBehavior = BatchRestartBehavior.SkipFailedJobAndContinue;
			executionPriorityLevel = ExecutionPriorityLevel.Normal;
			queuePriorityLevel = QueuePriorityLevel.Normal;
			maxConcurrent = 1;
			configFilePath = "";
			relativeExpDate = TimeSpan.FromDays(1);
			isolationLevel = ExecutionIsolationLevel.Process;
		}

        /// <summary>
        /// Loads the <see cref="ParameterData"/> from the Batch database and caches them in <see cref="ParameterCollection"/>.
        /// </summary>
        /// <returns>void</returns>
		public void LoadParameters()
		{
			this.ParameterCollection.LoadParameters(batchDB, this.instanceKey, ParameterCategory.BatchDefinition);
		}

        /// <summary>
        /// Loads the <see cref="JobReferenceData"/> from the Batch database and caches
        /// them in <see cref="JobReferenceCollection"/>.
        /// </summary>
        /// <returns>void</returns>
		public void LoadJobs()
		{
			this.JobReferenceCollection.LoadJobReferences(batchDB, this.instanceKey);
		}

        /// <summary>
        /// Saves the <see cref="ParameterData"/> from the <see cref="ParameterCollection"/> cache
        /// to the Batch database.  It does not delete the existing Parameters.  
        /// </summary>
        /// <returns>void</returns>
		public void SaveParameters()
		{
			this.ParameterCollection.SaveParameters(batchDB, this.instanceKey, ParameterCategory.BatchDefinition);
		}

        /// <summary>
        /// Saves the <see cref="JobReferenceData"/> from the <see cref="JobReferenceCollection"/> cache
        /// to the Batch database.  It does not delete the existing Jobs.
        /// </summary>
        /// <returns>void</returns>
		public void SaveJobs()
		{
			this.JobReferenceCollection.SaveJobReferences(batchDB, this.instanceKey);
		}

        /// <summary>
        /// Deletes the parameters for the Batch whose <see cref="IBatchDBData.Key"/> is in an array.
        /// </summary>
        /// <param name="removedKeys">The <see cref="ArrayList"/> containing the <see cref="IBatchDBData.Key"/> 
        /// of the Batch parameters to be removed.</param>
        /// <returns>void</returns>
		public void DeleteParameters(ArrayList removedKeys)
		{
			foreach(Guid key in removedKeys)
			{
				DefaultBatchManager.Parameter.Delete(batchDB, key);
			}
		}

		/// <summary>
		/// Gets or sets the cached <see cref="Database"/> instance for loading and saving the Batch definition.
		/// </summary>
		public Database BatchDB
		{
			get {return this.batchDB;}
			set { this.batchDB = value;}
		}
		
		/// <summary>
		/// See <see cref="IBatchData.DisplayName"/>.
		/// </summary>
		public string DisplayName
		{
			get 
            { 
                return this[displayNameProperty].ToString();
            }
			set 
            { 
                this[displayNameProperty] = value;
            }
		}

		/// <summary>
		/// Gets or sets description of the defined BatchDefinition.
		/// </summary>
		public string Description
		{
			get { return this.description; }
			set { this.description = value; }
		}

		/// <summary>
		/// Implements the <see cref="IBatchDBData.Key"/> property.  Gets or sets the key for the data.
		/// </summary>
		/// <value>a <see cref="System.Guid"/> instance as the key of the data object.</value>
		public Guid Key
		{
			get { return this.instanceKey;}
			set { this.instanceKey = value;}
		}

		/// <summary>
		/// Gets or sets the key of a
		/// <see cref="BatchTypeData"/> for the Batch. 
		/// </summary>
		/// <value>The key of the Batch type in the Batch database.</value>
		public Guid BatchTypeKey
		{
			get { return batchTypeKey; }
			set { batchTypeKey = value; }
		}

		/// <summary>
		/// Gets or sets the type name of the batch.
		/// </summary>
		public string BatchType
		{
			get { return this.batchTypeName;}
			set { this.batchTypeName = value;}
		}

		/// <summary>
		/// Gets or sets the key references to a <see cref="DestinationData"/> object.
		/// </summary>
		public Guid BatchDestinationKey
		{
			get { return batchDestinationKey; }
			set { batchDestinationKey = value; }
		}

		/// <summary>
		/// Gets or sets the name of the batch destination the batch is set to execute.
		/// </summary>
		public string Destination
		{
			get { return this.batchDestinationName;}
			set { this.batchDestinationName = value;}
		}

		/// <summary>
		/// Gets or sets the configuration file path for the Batch to execute with.
		/// </summary>
		public string ConfigurationFilePath
		{
			get { return this.configFilePath; }
			set { this.configFilePath = value; }
		}

		/// <summary>
		/// Gets or sets the maximum occurrences of the Batch.
		/// </summary>
		public int MaxConcurrent
		{
			get { return this.maxConcurrent; }
			set { this.maxConcurrent = value; }
		}

		/// <summary>
		/// Gets or sets the allowed time span the batch has to execute.  
		/// The batch will be terminated if the execution takes longer than the value.
		/// </summary>
		public TimeSpan RelativeExpirationDate
		{
			get { return this.relativeExpDate; }
			set { this.relativeExpDate = value; }
		}

		/// <summary>
		/// Gets or sets the <see cref="BatchRestartBehavior"/> for the batch.
		/// </summary>
		public BatchRestartBehavior RestartBehavior
		{
			get { return this.restartBehavior; }
			set { this.restartBehavior = value; }
		}

		/// <summary>
		/// Gets or sets the <see cref="ExecutionIsolationLevel"/>  for the batch.
		/// </summary>
		public ExecutionIsolationLevel IsolationLevel
		{
			get { return this.isolationLevel; }
			set { this.isolationLevel = value; }
		}

		/// <summary>
		/// Gets or sets the <see cref="ExecutionPriorityLevel"/> for the batch.
		/// </summary>
		public ExecutionPriorityLevel ExecutionPriorityLevel
		{
			get { return this.executionPriorityLevel; }
			set { this.executionPriorityLevel = value; }
		}

		/// <summary>
		/// Gets or sets the <see cref="QueuePriorityLevel"/> for the batch.
		/// </summary>
		public QueuePriorityLevel QueuePriorityLevel
		{
			get { return this.queuePriorityLevel; }
			set { this.queuePriorityLevel = value; }
		}

		/// <summary>
		/// Gets or sets the <see cref="ParameterCollection"/> for the Batch.
		/// </summary>
		public ParameterCollection ParameterCollection
		{
			get { return this.parameterCollection;}
			set { this.parameterCollection = value;}
		}

		/// <summary>
		/// Gets or sets the <see cref="JobReferenceCollection"/> for the Batch.
		/// </summary>
		public JobReferenceCollection JobReferenceCollection
		{
			get { return this.jobReferenceCollection;}
			set { this.jobReferenceCollection = value;}
		}

		/// <summary>
		/// See <see cref="IBatchDBData.Children"/>.
		/// </summary>
		public IList Children
		{
			get
			{
				ArrayList allChildren = new ArrayList(2);
				allChildren.Add(jobReferenceCollection);
				allChildren.Add(parameterCollection);
				return allChildren;
			}
		}

	}
}
