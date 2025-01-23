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
	/// Summary description for JobDefinitionData.
	/// </summary>    
    public class JobDefinitionData : NamedConfigurationElement, IBatchDBData
	{
		private ParameterCollection parameterCollection;

		private Database batchDB;
		//private string displayName;
        private const string displayNameProperty = "name";
		private string description;
		private Guid instanceKey;
		private Guid jobTypeKey;
		private string jobTypeName;
		private int	commitFrequency;
		private JobRestartBehavior	restartBehavior;

        /// <summary>
        /// Initializes the object with empty name and a newly generated <see cref="Key"/>.
        /// </summary>
		public JobDefinitionData() : this(string.Empty)
		{
		}

        /// <summary>
        /// Initializes the object with a name and a newly generated <see cref="Key"/>.
        /// </summary>
        /// <param name="displayName">The display name.</param>
		public JobDefinitionData(string displayName) : this(displayName,Guid.NewGuid())
		{
		}

        /// <summary>
        /// Initializes the object with a name and a <see cref="Key"/>.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="key">The key.</param>
		public JobDefinitionData(string displayName, Guid key) : base(displayName)
		{
			batchDB = null;
			//this.displayName = displayName;
            this[displayNameProperty] = displayName;
			this.description = string.Empty;
			this.instanceKey = key;
			jobTypeKey = Guid.Empty;
			jobTypeName = string.Empty;
			parameterCollection = new ParameterCollection();
			commitFrequency = 10;
			restartBehavior = JobRestartBehavior.Full;
		}

        /// <summary>
        /// Loads the Job definition parameters from the Batch database using the database instance specified
        /// in the <see cref="BatchDB"/> proptery.
        /// </summary>
        /// <returns>void</returns>
		public void LoadParameters()
		{
			this.ParameterCollection.LoadParameters(batchDB, this.instanceKey, ParameterCategory.JobDefinition);
		}

        /// <summary>
        /// Saves the Job definition parameters to the Batch database using the database instance specified
        /// in the <see cref="BatchDB"/> proptery.
        /// </summary>
        /// <returns>void</returns>
		public void SaveParameters()
		{
			this.ParameterCollection.SaveParameters(batchDB, this.instanceKey, ParameterCategory.JobDefinition);
		}

        /// <summary>
        /// Deletes the Job definition parameters from the Batch database whose <see cref="IBatchDBData.Key"/> is in an array
        /// using the database instance specified in the <see cref="BatchDB"/> proptery.
        /// </summary>
        /// <param name="removedKeys">The <see cref="ArrayList"/> containing the <see cref="IBatchDBData.Key"/> 
        /// of the Job definition parameters to be removed.</param>
        /// <returns>void</returns>
		public void DeleteParameters(ArrayList removedKeys)
		{
			this.ParameterCollection.DeleteParameters(batchDB, removedKeys);
		}

		
		/// <summary>
		/// Gets or sets the <see cref="Database"/> instance for accessing the Batch database.
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
                //return displayName; 
                return this[displayNameProperty].ToString();
            }
			set
            { 
                //displayName = value; 
                this[displayNameProperty] = value;
            }
		}

		/// <summary>
		/// The description of the defined JobDefinition.
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
		/// See <see cref="IBatchDBData.Children"/>.
		/// </summary>
		public IList Children
		{
			get { return new ArrayList(this.ParameterCollection);}
		}

		/// <summary>
		/// Gets or sets the key of the Job type instance in the configuration.
		/// </summary>
		public Guid JobTypeKey
		{
			get { return this.jobTypeKey;}
			set { this.jobTypeKey = value;}
		}

		/// <summary>
		/// Gets or sets the Job type of the Job definition. 
		/// </summary>
		public string JobType
		{
			get { return this.jobTypeName;}
			set { this.jobTypeName = value;}
		}

		/// <summary>
		/// Gets or sets the  number of work units to finish between committing the status of the Batch job.
		/// </summary>
		public int CommitFrequency
		{
			get { return this.commitFrequency; }
			set { this.commitFrequency = value; }
		}

		/// <summary>
		/// Gets or sets the <see cref="RestartBehavior"/> of the Batch job.
		/// </summary>
		public JobRestartBehavior RestartBehavior
		{
			get { return this.restartBehavior; }
			set { this.restartBehavior = value; }
		}

		/// <summary>
		/// Gets or sets the collection of parameters of the Batch job.
		/// </summary>
		public ParameterCollection ParameterCollection
		{
			get { return this.parameterCollection;}
		}

	}
}
