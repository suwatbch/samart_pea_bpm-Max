// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Collections;
using System.Data;


using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;


namespace Avanade.ACA.Batch
{
	/// <summary>
	/// JobExecutionContextCollection is the collection of jobs execution data during runtime.
	/// </summary>    
    public class JobExecutionContextCollection : NamedElementCollection<JobExecutionContextData>, IBatchDBData
	{
		private BatchExecutionContextData parent;
		private Database batchDB;
		private Guid batchKey;

        /// <summary>
        /// Initialize a collection with the BatchExecutionContextData.  
        /// </summary>
        /// <param name="data">The <see cref="BatchExecutionContextData"/> containing
        /// the Batch database instance and the <see cref="JobExecutionContextData"/>.</param>
        public JobExecutionContextCollection(BatchExecutionContextData data)
            : base()
		{
			parent = data;
			batchDB = parent.BatchDatabase;
			batchKey = parent.Key;
		}

        /// <summary>
        /// Loads the <see cref="JobExecutionContextData"/> from the Batch database.
        /// </summary>
        /// <returns>bool</returns>
		public bool LoadJobs()
		{

			const string DISPLAYNAME_FORMAT = "{0}. {1}";

			//Make sure the database is available
			if( batchDB == null )
			{
				return false;
			}

			IDataReader reader = null;
			
			try
			{
				reader = DefaultBatchManager.RequestQueue.ListJobExecution(batchDB, batchKey);

				while( reader.Read() )
				{
	
					JobExecutionContextData data = new JobExecutionContextData(reader.GetGuid(0),this.parent);
			
					
					//					BatchJobName,
					data.JobName = reader.GetString(2);
					//					BatchJobDesc,
					if (reader.IsDBNull(3))
					{
						data.Description = String.Empty;
					}
					else
					{
						data.Description = reader.GetString(3);
					}
					//					JobSeq,
					data.Sequence = reader.GetInt16(4);
					string displayName = String.Format(DISPLAYNAME_FORMAT, data.Sequence, data.JobName);
					data.DisplayName = displayName;

					//					JobSmartRestart,
					data.RestartBehavior = (JobRestartBehavior)reader.GetByte(5);
					
					//					BatchJobCmitFreq,
					data.CommitFrequency = reader.GetInt32(6);

					//					JobTypeClass,
					data.JobType = reader.GetString(7);

					//					StartDate,
					if (reader.IsDBNull(8))
					{
						data.StartDate = DateTime.MinValue;
					}
					else
					{
						data.StartDate = reader.GetDateTime(8);
					}
					//					JobStatusCode,
					byte statusCode = reader.GetByte(9);
					data.Status = (StatusCode)statusCode;

					//					LastCommitDate,
					if (reader.IsDBNull(10))
					{
						data.LastCommitDate = new DateTime(1900, 1, 1);
					}
					else
					{
						data.LastCommitDate = reader.GetDateTime(10);
					}

					//					WorkUnitCount,
					data.WorkUnitCount = reader.GetInt32(11);
					//					RestartCount,
					data.RestartCount = reader.GetInt32(12);
					//					CommitCount,
					data.CommitCount = reader.GetInt32(13);
					//					OrigJobKey
					data.OriginalJobDefinitionKey = reader.GetGuid(14);
					//					LastExeStatus
					statusCode = reader.GetByte(15);
					data.PreviousExecutionStatus = (StatusCode)statusCode;

					this.Add(data);
				}
			}
			finally
			{
				if( reader != null )
				{
					reader.Close();
				}
			}

			LoadChildren();

			return true;
		}

        /// <summary>LoadChildren</summary>
        /// <returns>void</returns>
        private void LoadChildren()
		{
			IEnumerator enumer = this.GetEnumerator();

			while (enumer.MoveNext()) 
			{
				JobExecutionContextData data = (JobExecutionContextData) enumer.Current;
				data.LoadChildren();
			}
		}

        /// <summary>
        /// Saves all the JobExecutionContextData to the Batch database.
        /// </summary>
        /// <returns>void</returns>
		public void SaveJobs()
		{

			IEnumerator enumer = this.GetEnumerator();

			while (enumer.MoveNext()) 
			{
				JobExecutionContextData data = (JobExecutionContextData) enumer.Current;
				data.Save();
			}
		}

		/// <summary>
		/// See <see cref="IBatchData.DisplayName"/>.
		/// </summary>
		public string  DisplayName
		{
			get {return "Jobs";}
		}

		/// <summary>
		/// An Empty key.
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
		/// Gets or sets the <see cref="JobExecutionContextData"/> by its index.
		/// </summary>
		public JobExecutionContextData this[int index]
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
		/// Gets or sets a <see cref="JobExecutionContextData"/> in the collection by name.  
		/// If there's no entry exists in the collection, the getter returns null and 
		/// the setting adds a new entry with the name as the key.
		/// </summary>
		/// <exception cref="ArgumentNullException">If the name if null.</exception>
		public new JobExecutionContextData this[string name]
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
        /// Adds a <see cref="JobExecutionContextData"/> configuration to the collection with
        /// <see cref="IBatchData.DisplayName"/> as the key.
        /// </summary>
        /// <param name="data">The data instance to be added.</param>
        /// <exception cref="ArgumentNullException">The data is null.</exception>
        /// <exception cref="InvalidOperationException">The 
        /// <see cref="IBatchData.DisplayName"/> is null.</exception>
        /// <returns>void</returns>
		public new void Add(JobExecutionContextData data)
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
        /// Add a collection of the <see cref="JobExecutionContextData"/> to the existing collection./// </summary>
        /// <param name="collection">the collection of the entries to add.</param>
        /// <exception cref="ArgumentNullException">Any object in the collection
        /// is null.</exception>
        /// <exception cref="InvalidOperationException">The 
        /// <see cref="IBatchData.DisplayName"/> is null or the existing collection contains
        /// an entry with the same names as the ones in collection.
        /// </exception>
        /// <returns>void</returns>
		public void AddRange(JobExecutionContextCollection collection)
		{
			foreach(JobExecutionContextData data in collection)
			{
				this.Add(data);
			}
		}

        /// <summary>GetData</summary>
        /// <param name="index">int</param>
        /// <returns>Avanade.ACA.Batch.JobExecutionContextData</returns>
        private JobExecutionContextData GetData(int index)
		{
			return (JobExecutionContextData) BaseGet(index);
		}

        /// <summary>GetData</summary>
        /// <param name="name">string</param>
        /// <returns>Avanade.ACA.Batch.JobExecutionContextData</returns>
        private JobExecutionContextData GetData(string name)
		{
			return (JobExecutionContextData) BaseGet(name);
		}

        /// <summary>SetData</summary>
        /// <param name="index">int</param>
        /// <param name="data">Avanade.ACA.Batch.JobExecutionContextData</param>
        /// <returns>void</returns>
        private void SetData(int index, JobExecutionContextData data)
        {
            BaseAdd(index, data);
        }

        /// <summary>SetData</summary>
        /// <param name="name">string</param>
        /// <param name="data">Avanade.ACA.Batch.JobExecutionContextData</param>
        /// <returns>void</returns>
        private void SetData(string name, JobExecutionContextData data)
        {
            BaseAdd(data);
        }

	}
}

