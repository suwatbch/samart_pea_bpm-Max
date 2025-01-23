// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Data;
using System.Collections;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Avanade.ACA.Batch.Configuration
{
	/// <summary>
	/// JobReferenceCollection represents the collection of jobs belongs to the same Batch.
	/// Each element in the collection is of <see cref="JobReferenceData"/> and numbered.
	/// </summary>    
    public class JobReferenceCollection : NamedElementCollection<JobReferenceData>, IBatchDBData
	{
		private string displayName = "Jobs";

        /// <summary>
        /// Creates a new instance of the collection.
        /// </summary>
		public JobReferenceCollection() : base()
		{
		}

        /// <summary>
        /// Loads th Batch job reference data from the specfied database
        /// </summary>
        /// <param name="batchDB">Instance of the Batch database</param>
        /// <param name="batchKey">Guid identifying parent batch instance</param>
        /// <returns>void</returns>
		public void LoadJobReferences(Database batchDB, Guid batchKey)
		{
			IDataReader rdr = null;
			
			try 
			{
				rdr = DefaultBatchManager.Batch.ListJobs(batchDB,batchKey);
				while( rdr.Read() )
				{
                    JobReferenceData data = new JobReferenceData(rdr.GetInt16(3) + ". " + rdr.GetString(4));
					int seqNo = rdr.GetInt16(3);
					data.SequenceNum = seqNo;
					data.OldSequenceNum = seqNo;
					//data.DisplayName = data.SequenceNum.ToString() + ". " + rdr.GetString(4);			
					this.Add(data);
					data.JobDefinitionKey = rdr.GetGuid(0);
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
        /// Saves the Batch job reference data to the Batch database.
        /// </summary>
        /// <param name="batchDB">Instance of the Batch database</param>
        /// <param name="batchKey">Guid identifying parent batch instance</param>
        /// <returns>void</returns>
		public void SaveJobReferences(Database batchDB, Guid batchKey)
		{
			// deletes all the job references from the database.
			DefaultBatchManager.Batch.CleanJobs(batchDB, batchKey);
			
			IEnumerator enumer = this.GetEnumerator();
			while (enumer.MoveNext())
			{
				JobReferenceData data = (JobReferenceData) enumer.Current;
				DefaultBatchManager.Batch.AddJob(batchDB, batchKey, data.JobDefinitionKey,
					data.SequenceNum, data.OldSequenceNum);
			}
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
		/// Returns the JobReference at the specified integer index.
		/// </summary>
		public JobReferenceData this[int index]
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
		/// Gets or sets a <see cref="JobReferenceData"/> in the collection by name.  
		/// If there's no entry exists in the collection, the getter returns null and 
		/// the setting adds a new entry with the name as the key.
		/// </summary>
		/// <exception cref="ArgumentNullException">If the name if null.</exception>
		public new JobReferenceData this[string name]
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
        /// Adds a <see cref="JobReferenceData"/> configuration to the collection
        /// with the <see cref="IBatchData.DisplayName"/> as the key.
        /// </summary>
        /// <param name="data">The data instance to be added.</param>
        /// <exception cref="ArgumentNullException">The data is null.</exception>
        /// <exception cref="InvalidOperationException">The <see cref="IBatchData.DisplayName"/> is null.</exception>
        /// <returns>void</returns>
		public new void Add(JobReferenceData data)
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
        /// Add a collection of the <see cref="JobReferenceData"/> to the existing collection.
        /// </summary>
        /// <param name="collection">the collection of the entries to add.</param>
        /// <exception cref="ArgumentNullException">Any object in collection
        /// is null.</exception>
        /// <exception cref="InvalidOperationException">The 
        /// <see cref="IBatchData.DisplayName"/> is null or the existing collection contains
        /// an entry with the same names as the ones in collection.
        /// </exception>
        /// <returns>void</returns>
		public void AddRange(JobReferenceCollection collection)
		{
			foreach(JobReferenceData data in collection)
			{
				this.Add(data);
			}
		}

        /// <summary>GetData</summary>
        /// <param name="index">int</param>
        /// <returns>Avanade.ACA.Batch.Configuration.JobReferenceData</returns>
        private JobReferenceData GetData(int index)
		{
			return (JobReferenceData) BaseGet(index);
		}

        /// <summary>GetData</summary>
        /// <param name="name">string</param>
        /// <returns>Avanade.ACA.Batch.Configuration.JobReferenceData</returns>
        private JobReferenceData GetData(string name)
		{
			return (JobReferenceData) BaseGet(name);
		}

        /// <summary>SetData</summary>
        /// <param name="index">int</param>
        /// <param name="data">Avanade.ACA.Batch.Configuration.JobReferenceData</param>
        /// <returns>void</returns>
        private void SetData(int index, JobReferenceData data)
        {            
            BaseAdd(index, data);
        }

        /// <summary>SetData</summary>
        /// <param name="name">string</param>
        /// <param name="data">Avanade.ACA.Batch.Configuration.JobReferenceData</param>
        /// <returns>void</returns>
        private void SetData(string name, JobReferenceData data)
        {
            BaseAdd(data);
        }
	}
}
