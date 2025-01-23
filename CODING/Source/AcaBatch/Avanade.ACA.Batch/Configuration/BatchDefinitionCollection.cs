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
	/// Representing the collection of the Batch definitions in configuration.
	/// </summary>    
    public class BatchDefinitionCollection : NamedElementCollection<BatchDefinitionData>, IBatchDBData
	{
		private string displayName= "Batch Definitions";
		private Database batchDB;
		private BatchTypeCollection batchTypeCollection;
		private DestinationCollection destCollection;
		private JobDefinitionCollection jobDefCollection;

        /// <summary>
        /// Initialize the <see cref="BatchDefinitionCollection"/> instance with a
        /// <see cref="BatchTypeCollection"/>, <see cref="DestinationCollection"/>, 
        /// and <see cref="JobDefinitionCollection"/>.
        /// </summary>
        /// <param name="batchTypeColl">Avanade.ACA.Batch.Configuration.BatchTypeCollection</param>
        /// <param name="destColl">Avanade.ACA.Batch.Configuration.DestinationCollection</param>
        /// <param name="jobDefColl">Avanade.ACA.Batch.Configuration.JobDefinitionCollection</param>
		public BatchDefinitionCollection(BatchTypeCollection batchTypeColl, DestinationCollection destColl, JobDefinitionCollection jobDefColl) : base()
		{
			this.batchTypeCollection = batchTypeColl;
			this.destCollection = destColl;
			this.jobDefCollection = jobDefColl;
		}

        /// <summary>
        /// Loads the Batch definition from the Batch database.
        /// </summary>
        /// <param name="db">The <see cref="Database"/> instance to access the Batch database.</param>
        /// <remarks>Once the data is loaded, the databased is cached internally for 
        /// <see cref="SaveBatchDefinitions"/> unless the users setting it with the
        /// <see cref="BatchDB"/> property.</remarks>
        /// <returns>void</returns>
		public void LoadBatchDefinitions( Database db )
		{
			this.batchDB = db;
			this.Clear();
			IDataReader rdr = null;
			
			try
			{
				rdr = DefaultBatchManager.Batch.List(batchDB);

				while( rdr.Read() )
				{
					BatchDefinitionData data = new BatchDefinitionData(rdr.GetString(1), rdr.GetGuid(0));
					data.BatchDB = batchDB;
					data.Description = rdr.IsDBNull(2) ? String.Empty : rdr.GetString(2);
					data.RestartBehavior = (BatchRestartBehavior) rdr.GetByte(3);
					data.ExecutionPriorityLevel = (ExecutionPriorityLevel) rdr.GetByte(4);
					data.QueuePriorityLevel = (QueuePriorityLevel) rdr.GetByte(5);
					data.MaxConcurrent = rdr.GetInt32(6);
					data.ConfigurationFilePath = rdr.IsDBNull(7) ? String.Empty : rdr.GetString(7);
					data.RelativeExpirationDate = TimeSpan.FromMilliseconds(rdr.GetInt64(8));
					data.BatchDestinationKey = rdr.GetGuid(9);
					DestinationData destdata = this.destCollection[rdr.GetGuid(9)];
					if (destdata != null)
						data.Destination = destdata.DisplayName;
					data.IsolationLevel = (ExecutionIsolationLevel)rdr.GetByte(10);
					data.BatchTypeKey = rdr.GetGuid(11);
					BatchTypeData batchdata = this.batchTypeCollection[rdr.GetGuid(11)];
					if (batchdata != null)
						data.BatchType = batchdata.DisplayName;
					this.Add(data);
					data.LoadParameters();
					data.LoadJobs(/*this.jobDefCollection*/);
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
        /// Saves the Batch definitions in the Batch database with the cached
        /// <see cref="BatchDB"/> database instance.
        /// </summary>
        /// <returns>void</returns>
		public void SaveBatchDefinitions()
		{
			IEnumerator enumer = this.GetEnumerator();
			while (enumer.MoveNext())
			{
				BatchDefinitionData data = (BatchDefinitionData) enumer.Current;
				DefaultBatchManager.Batch.Save(batchDB, data.Key, data.DisplayName, data.Description, data.RestartBehavior, data.ExecutionPriorityLevel, data.QueuePriorityLevel, data.MaxConcurrent, data.ConfigurationFilePath, Convert.ToInt64(data.RelativeExpirationDate.TotalMilliseconds), data.BatchDestinationKey, data.IsolationLevel, data.BatchTypeKey);
			}
		}

        /// <summary>
        /// Delete the Batch definitions whose <see cref="BatchDefinitionData.Key"/> is
        /// in the <see cref="ArrayList"/> from the Batch database.
        /// </summary>
        /// <param name="removedKeys">The <see cref="ArrayList"/> containing the 
        /// <see cref="BatchDefinitionData.Key"/> of the <see cref="BatchDefinitionData"/> that
        /// needs to be removed.</param>
        /// <remarks>The cached <see cref="BatchDB"/> will be used to perform the action
        /// or removal.</remarks>
        /// <returns>void</returns>
		public void DeleteBatchDefinitions(ArrayList removedKeys)
		{
			foreach(Guid key in removedKeys)
			{
				DefaultBatchManager.Batch.Delete(batchDB, key);
			}
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
		/// Returns the BatchDefinition at the specified integer index.
		/// </summary>
		public BatchDefinitionData this[int index]
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
		/// Gets or sets a <see cref="BatchDefinitionData"/> in the collection by name.  
		/// If there's no entry exists in the collection,
		/// the getter returns null and the setting adds a new entry with the name as the key.
		/// </summary>
		/// <exception cref="ArgumentNullException">If the name if null.</exception>
		public new BatchDefinitionData this[string name]
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
		/// Gets a <see cref="BatchDefinitionData"/> in the collection by a unique key.
		/// Returns null if not entry is found.
		/// </summary>
		/// <value>A <see cref="System.Guid"/> as the key of the data instance.</value>
		public BatchTypeData this[Guid instanceKey]
		{
			get 
			{
				IEnumerator enumer = this.GetEnumerator();
				while (enumer.MoveNext())
				{
					BatchTypeData data = (BatchTypeData)enumer.Current;
					if (data.Key == instanceKey)
						return data;
				}
				return null;
			}
		}

        /// <summary>
        /// Adds a <see cref="BatchDefinitionData"/> configuration to the collection
        /// with the <see cref="IBatchData.DisplayName"/> as the key.
        /// </summary>
        /// <param name="data">The data instance to be added.</param>
        /// <exception cref="ArgumentNullException">The data is null.</exception>
        /// <exception cref="InvalidOperationException">The <see cref="IBatchData.DisplayName"/> is null.</exception>
        /// <returns>void</returns>
		public new void Add(BatchDefinitionData data)
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
        /// Add a collection of the <see cref="BatchDefinitionData"/> to the existing collection.
        /// </summary>
        /// <param name="collection">the collection of the entries to add.</param>
        /// <exception cref="ArgumentNullException">Any object in the collection
        /// is null.</exception>
        /// <exception cref="InvalidOperationException">The 
        /// <see cref="IBatchData.DisplayName"/> is null or the existing collection contains
        /// an entry with the same names as the ones in collection.
        /// </exception>
        /// <returns>void</returns>
		public void AddRange(BatchDefinitionCollection collection)
		{
			foreach(BatchDefinitionData data in collection)
			{
				this.Add(data);
			}
		}

		/// <summary>
		/// Implements the <see cref="IBatchDBData.Key"/> property.  Returns null for this class
		/// since it's not applicable for a collection.
		/// </summary>
		public Guid Key
		{
			get
			{
				return Guid.Empty;
			}
		}

		/// <summary>
		/// See <see cref="IBatchData.DisplayName"/>.
		/// </summary>
		string IBatchData.DisplayName
		{
			get { return this.displayName;}
		}

		/// <summary>
		/// See <see cref="IBatchDBData.Children"/>.
		/// </summary>
		public IList Children
		{
			get {return new ArrayList(this);}
		}

        /// <summary>GetData</summary>
        /// <param name="index">int</param>
        /// <returns>Avanade.ACA.Batch.Configuration.BatchDefinitionData</returns>
        private BatchDefinitionData GetData(int index)
		{
			return (BatchDefinitionData) BaseGet(index);
		}

        /// <summary>GetData</summary>
        /// <param name="name">string</param>
        /// <returns>Avanade.ACA.Batch.Configuration.BatchDefinitionData</returns>
        private BatchDefinitionData GetData(string name)
		{
			return (BatchDefinitionData) BaseGet(name);
		}

        /// <summary>SetData</summary>
        /// <param name="index">int</param>
        /// <param name="data">Avanade.ACA.Batch.Configuration.BatchDefinitionData</param>
        /// <returns>void</returns>
        private void SetData(int index, BatchDefinitionData data)
        {            
            BaseAdd(index, data);
        }

        /// <summary>SetData</summary>
        /// <param name="name">string</param>
        /// <param name="data">Avanade.ACA.Batch.Configuration.BatchDefinitionData</param>
        /// <returns>void</returns>
        private void SetData(string name, BatchDefinitionData data)
        {
            BaseAdd(data);
        }
	}
}
