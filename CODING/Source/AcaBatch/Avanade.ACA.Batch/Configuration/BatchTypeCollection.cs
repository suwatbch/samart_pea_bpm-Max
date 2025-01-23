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
	/// BatchTypeCollection is a collection of the configured Batch types.  Each element in the
	/// collection is of the type <see cref="BatchTypeData"/>.
	/// </summary>    
    public class BatchTypeCollection : NamedElementCollection<BatchTypeData>, IBatchDBData
	{
		private string displayName = "Batch Types";
		private Database batchDB;

        /// <summary>
        /// Creates a new instance of the collection.
        /// </summary>
		public BatchTypeCollection()
		{
		}

        /// <summary>
        /// Loads batch types from the specfied database.  The <see cref="Database"/> instance will be
        /// cached internally.
        /// </summary>
        /// <param name="db">Instance of the Batch database</param>
        /// <returns>void</returns>
		public void LoadBatchTypes(Database db)
		{
			this.batchDB = db;
			this.Clear();

			IDataReader rdr = null;
			try
			{
				rdr = DefaultBatchManager.TypeDefinition.List(batchDB, DefaultBatchManager.TypeDefinition.TYPE_CATEGORY.CATEGORY_BATCH_TYPE);
				while( rdr.Read() )
				{
					BatchTypeData data = new BatchTypeData(rdr.GetString(2),rdr.GetGuid(0));
					data.TypeName = rdr.GetString(1);
					data.Description =	rdr.IsDBNull(3) ? String.Empty : rdr.GetString(3);
					this.Add(data);
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
        /// Saves the Batch types to the Batch database with the cached database instance set from invoking
        /// the <see cref="LoadBatchTypes"/>. 
        /// </summary>
        /// <returns>void</returns>
		public void SaveBatchTypes()
		{

			IEnumerator enumer = this.GetEnumerator();
			while (enumer.MoveNext())
			{
				BatchTypeData data = (BatchTypeData) enumer.Current;
				DefaultBatchManager.TypeDefinition.Save(batchDB, data.Key, data.DisplayName, data.Description, data.TypeName,DefaultBatchManager.TypeDefinition.TYPE_CATEGORY.CATEGORY_BATCH_TYPE);
			}
		}

        /// <summary>
        /// Deletes the Batch type from the database whose <see cref="IBatchDBData.Key"/> is in an array
        /// with the cached database instance set from invoking the <see cref="LoadBatchTypes"/> method. 
        /// </summary>
        /// <param name="removedKeys">The <see cref="ArrayList"/> containing the <see cref="IBatchDBData.Key"/> 
        /// of the Batch type to be removed.</param>
        /// <returns>void</returns>
		public void DeleteBatchTypes(ArrayList removedKeys)
		{
			foreach(Guid key in removedKeys)
			{
				DefaultBatchManager.TypeDefinition.Delete(batchDB, key);
			}
		}

		/// <summary>
		/// See <see cref="IBatchData.DisplayName"/>.
		/// </summary>
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.displayName = value;
			}
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
			get {return new ArrayList(this);}
		}

		/// <summary>
		/// Returns the BatchType at the specified integer index.
		/// </summary>
		public BatchTypeData this[int index]
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
		/// Gets or sets a <see cref="BatchTypeData"/> in the collection by name.  
		/// If there's no entry exists in the collection,
		/// the getter returns null and the setting adds a new entry with the name as the key.
		/// </summary>
		/// <exception cref="ArgumentNullException">If the name if null.</exception>
        public new BatchTypeData this[string name]
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
		/// Gets a <see cref="BatchTypeData"/> in the collection by a unique key.
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
        /// Adds a <see cref="BatchTypeData"/> configuration to the collection
        /// with the <see cref="IBatchData.DisplayName"/> as the key..
        /// </summary>
        /// <param name="data">The data instance to be added.</param>
        /// <exception cref="ArgumentNullException">The data is null.</exception>
        /// <exception cref="InvalidOperationException">The <see cref="IBatchData.DisplayName"/> is null.</exception>
        /// <returns>void</returns>
        public new void Add(BatchTypeData data)
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
        /// Add a collection of the <see cref="BatchTypeData"/> to the existing collection.
        /// </summary>
        /// <param name="collection">the collection of the entries to add.</param>
        /// <exception cref="ArgumentNullException">Any object in collection
        /// is null.</exception>
        /// <exception cref="InvalidOperationException">The 
        /// <see cref="IBatchData.DisplayName"/> is null or the existing collection contains
        /// an entry with the same names as the ones in collection.
        /// </exception>
        /// <returns>void</returns>
		public void AddRange(BatchTypeCollection collection)
		{
			foreach(BatchTypeData data in collection)
			{
				this.Add(data);
			}
		}

        /// <summary>GetData</summary>
        /// <param name="index">int</param>
        /// <returns>Avanade.ACA.Batch.Configuration.BatchTypeData</returns>
        private BatchTypeData GetData(int index)
		{
			return (BatchTypeData) BaseGet(index);
		}

        /// <summary>GetData</summary>
        /// <param name="name">string</param>
        /// <returns>Avanade.ACA.Batch.Configuration.BatchTypeData</returns>
        private BatchTypeData GetData(string name)
		{
			return (BatchTypeData) BaseGet(name);
		}

        /// <summary>SetData</summary>
        /// <param name="index">int</param>
        /// <param name="data">Avanade.ACA.Batch.Configuration.BatchTypeData</param>
        /// <returns>void</returns>
        private void SetData(int index, BatchTypeData data)
        {            
            BaseAdd(index, data);
        }

        /// <summary>SetData</summary>
        /// <param name="name">string</param>
        /// <param name="data">Avanade.ACA.Batch.Configuration.BatchTypeData</param>
        /// <returns>void</returns>
        private void SetData(string name, BatchTypeData data)
        {         
            BaseAdd(data);
        }
	}
}
