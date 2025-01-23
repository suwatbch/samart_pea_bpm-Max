// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Data;
using System.Collections;

using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Avanade.ACA.Batch.Configuration
{
	/// <summary>
	/// Summary description for JobDefinitionCollection.
	/// </summary>    
    public class JobDefinitionCollection : NamedElementCollection<JobDefinitionData>, IBatchDBData
	{
		private string displayName= "Job Definitions";
		private Database batchDB;
		private JobTypeCollection jobTypeCollection;

        /// <summary>
        /// Creates a new instance of the collection.
        /// </summary>
        /// <param name="jobTypeColl">Avanade.ACA.Batch.Configuration.JobTypeCollection</param>
		public JobDefinitionCollection(JobTypeCollection jobTypeColl)
		{
			this.jobTypeCollection = jobTypeColl;
		}

        /// <summary>
        /// Loads the Batch job definition from the specfied database and populates the collection.  
        /// The <see cref="Database"/> instance will be cached internally for later use.
        /// </summary>
        /// <param name="db">Instance of the Batch database</param>
        /// <returns>void</returns>
		public void LoadJobDefinitions(Database db)
		{
			this.batchDB = db;
			this.Clear();
			IDataReader rdr = null;
			
			try 
			{
				rdr = DefaultBatchManager.Job.List(batchDB);

				while( rdr.Read() )
				{
					JobDefinitionData data = new JobDefinitionData(rdr.GetString(2), rdr.GetGuid(0));
					data.BatchDB = this.batchDB;
					data.Description =	rdr.IsDBNull(3) ? String.Empty : rdr.GetString(3);
					data.RestartBehavior = (JobRestartBehavior)rdr.GetByte(4);
					data.CommitFrequency = rdr.GetInt32(5);
					data.JobTypeKey = rdr.GetGuid(1);
					JobTypeData jobdata = this.jobTypeCollection[rdr.GetGuid(1)];
					data.JobType = jobdata.DisplayName;
					this.Add(data);
					data.LoadParameters();
				}
			}
			finally
			{
				if( rdr != null )
				{
					rdr.Close();
				}
			}
		}

        /// <summary>
        /// Saves the Batch job definitions types to the Batch database with the cached database instance set from invoking
        /// the <see cref="LoadJobDefinitions"/>. 
        /// </summary>
        /// <returns>void</returns>
		public void SaveJobDefinitions()
		{
			IEnumerator enumer = this.GetEnumerator();
			while (enumer.MoveNext())
			{
				JobDefinitionData data = (JobDefinitionData) enumer.Current;
				DefaultBatchManager.Job.Save(batchDB, data.Key, data.DisplayName, data.Description,data.JobTypeKey,data.RestartBehavior,data.CommitFrequency);
			}
		}

        /// <summary>
        /// Deletes the parameters for the Batch whose <see cref="IBatchDBData.Key"/> is in an array.
        /// </summary>
        /// <param name="removedKeys">The <see cref="ArrayList"/> containing the <see cref="IBatchDBData.Key"/> 
        /// of the Batch parameters to be removed.</param>
        /// <returns>void</returns>
		public void DeleteJobDefinitions(ArrayList removedKeys)
		{
			foreach(Guid key in removedKeys)
			{
				DefaultBatchManager.Job.Delete(batchDB, key);
			}
		}

		/// <summary>
		/// Gets the <see cref="Database"/> instance for accessing the Batch database.
		/// </summary>
		public Database BatchDB
		{
			get { return this.batchDB;}
		}

		/// <summary>
		/// See <see cref="IBatchData.DisplayName"/>.
		/// </summary>
		public string DisplayName
		{
			get { return this.displayName;}
			set {this.displayName = value;}
		}

		/// <summary>
		/// Implements the <see cref="IBatchDBData.Key"/> property.  Returns null for this class
		/// since it's not applicable for a collection.
		/// </summary>
		public Guid Key
		{
			get {return Guid.Empty;}
		}

		/// <summary>
		/// See <see cref="IBatchDBData.Children"/>.
		/// </summary>
		public IList Children
		{
			get { return new ArrayList(this);}
		}

		/// <summary>
		/// Returns the JobDefinition at the specified integer index.
		/// </summary>
		public JobDefinitionData this[int index]
		{
			get 
			{
				return this.GetData(index);
			}
			set
			{
				this.SetData(index, value);
			}
		}

		/// <summary>
		/// Gets or sets a <see cref="JobDefinitionData"/> in the collection by name.  
		/// If there's no entry exists in the collection, the getter returns null and 
		/// the setting adds a new entry with the name as the key.
		/// </summary>
		/// <exception cref="ArgumentNullException">If the name if null.</exception>
		public new JobDefinitionData this[string name]
		{
			get 
			{
				if (name == null)
				{
					throw new ArgumentNullException("name");
				}
				return this.GetData(name);
			}
			set
			{
				if (name == null)
				{
					throw new ArgumentNullException("name");
				}
				this.SetData(name, value);
			}
		}

		/// <summary>
		/// Gets a <see cref="JobDefinitionData"/> in the collection by a unique key.
		/// Returns null if not entry is found.
		/// </summary>
		/// <value>A <see cref="System.Guid"/> as the key of the data instance.</value>
		public JobDefinitionData this[Guid instanceKey]
		{
			get 
			{
				IEnumerator enumer = this.GetEnumerator();
				while (enumer.MoveNext())
				{
					JobDefinitionData data = (JobDefinitionData)enumer.Current;
					if (data.Key == instanceKey)
						return data;
				}
				return null;
			}
		}

        /// <summary>
        /// Adds a <see cref="JobDefinitionData"/> configuration to the collection with the <see cref="IBatchData.DisplayName"/> as the key.
        /// </summary>
        /// <param name="data">The data instance to be added.</param>
        /// <exception cref="ArgumentNullException">The data is null.</exception>
        /// <exception cref="InvalidOperationException">The <see cref="IBatchData.DisplayName"/> is null.</exception>
        /// <returns>void</returns>
		public new void Add(JobDefinitionData data)
		{
			if (data == null)
			{
				throw new ArgumentNullException( "data" );
			}

			if (data.DisplayName == null)
			{
				throw new InvalidOperationException( "Empty Name" );
			}
                        
            BaseAdd(data);
		}

        /// <summary>
        /// Add a collection of the <see cref="JobDefinitionData"/> to the existing collection.
        /// </summary>
        /// <param name="collection">the collection of the entries to add.</param>
        /// <exception cref="ArgumentNullException">Any object in collection
        /// is null.</exception>
        /// <exception cref="InvalidOperationException">The 
        /// <see cref="IBatchData.DisplayName"/> is null or the existing collection contains
        /// an entry with the same names as the ones in collection.
        /// </exception>
        /// <returns>void</returns>
		public void AddRange(JobDefinitionCollection collection)
		{
			foreach(JobDefinitionData data in collection)
			{
				this.Add(data);
			}
		}

        /// <summary>GetData</summary>
        /// <param name="index">int</param>
        /// <returns>Avanade.ACA.Batch.Configuration.JobDefinitionData</returns>
        private JobDefinitionData GetData(int index)
		{
			return (JobDefinitionData) BaseGet(index);
		}

        /// <summary>GetData</summary>
        /// <param name="name">string</param>
        /// <returns>Avanade.ACA.Batch.Configuration.JobDefinitionData</returns>
        private JobDefinitionData GetData(string name)
		{
			return (JobDefinitionData) BaseGet(name);
		}

        /// <summary>SetData</summary>
        /// <param name="index">int</param>
        /// <param name="data">Avanade.ACA.Batch.Configuration.JobDefinitionData</param>
        /// <returns>void</returns>
        private void SetData(int index, JobDefinitionData data)
        {            
            BaseAdd(index, data);
        }

        /// <summary>SetData</summary>
        /// <param name="name">string</param>
        /// <param name="data">Avanade.ACA.Batch.Configuration.JobDefinitionData</param>
        /// <returns>void</returns>
        private void SetData(string name, JobDefinitionData data)
        {
            BaseAdd(data);
        }
	}
}
