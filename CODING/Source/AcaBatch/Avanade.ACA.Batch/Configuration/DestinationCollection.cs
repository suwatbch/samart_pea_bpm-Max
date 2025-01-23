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
	/// DestinationCollection is a collection of the configured Batch destinations.  Each element in the
	/// collection is of the type <see cref="DestinationData"/>.
	/// </summary>    
    public class DestinationCollection : NamedElementCollection<DestinationData>, IBatchDBData
	{
		private string displayName = "Batch Destinations";
		private Database batchDB;

        /// <summary>
        /// Creates a new instance of the collection.
        /// </summary>
		public DestinationCollection()
		{
		}

        /// <summary>
        /// Loads the Batch destinations from the specfied database and populates the collection.  
        /// The <see cref="Database"/> instance will be cached internally for later use.
        /// </summary>
        /// <param name="db">Instance of the Batch database</param>
        /// <returns>void</returns>
		public void LoadDestinations(Database db)
		{
			this.batchDB = db;
			this.Clear();
			IDataReader rdr = null;
			
			try 
			{
				rdr = DefaultBatchManager.Destination.List(batchDB);

				while( rdr.Read() )
				{
					DestinationData data = new DestinationData(rdr.GetString(1), rdr.GetGuid(0));
					data.Description = rdr.IsDBNull(2) ? string.Empty : rdr.GetString(2);

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
        /// Saves the Batch destinations to the Batch database with the cached database instance set from invoking
        /// the <see cref="LoadDestinations"/>. 
        /// </summary>
        /// <returns>void</returns>
		public void SaveDestinations()
		{
			IEnumerator enumer = this.GetEnumerator();
			while (enumer.MoveNext())
			{
				DestinationData data = (DestinationData) enumer.Current;
				DefaultBatchManager.Destination.Save(batchDB, data.Key, data.DisplayName, data.Description );
			}
		}

        /// <summary>
        /// Deletes the parameters for the Batch whose <see cref="IBatchDBData.Key"/> is in an array.
        /// </summary>
        /// <param name="removedKeys">The <see cref="ArrayList"/> containing the <see cref="IBatchDBData.Key"/> 
        /// of the Batch parameters to be removed.</param>
        /// <returns>void</returns>
		public void DeleteDestinations(ArrayList removedKeys)
		{
			foreach(Guid key in removedKeys)
			{
				DefaultBatchManager.Destination.Delete(batchDB, key);
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
		/// Returns the Destination at the specified integer index.
		/// </summary>
		public DestinationData this[int index]
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
		/// Gets or sets a <see cref="DestinationData"/> in the collection by name.  
		/// If there's no entry exists in the collection,
		/// the getter returns null and the setting adds a new entry with the name as the key.
		/// </summary>
		/// <exception cref="ArgumentNullException">If the name if null.</exception>
		public new DestinationData this[string name]
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
		/// Gets a <see cref="DestinationData"/> in the collection by a unique key.
		/// Returns null if not entry is found.
		/// </summary>
		/// <value>A <see cref="System.Guid"/> as the key of the data instance.</value>
		public DestinationData this[Guid instanceKey]
		{
			get 
			{
				IEnumerator enumer = this.GetEnumerator();
				while (enumer.MoveNext())
				{
					DestinationData data = (DestinationData)enumer.Current;
					if (data.Key == instanceKey)
						return data;
				}
				return null;
			}
		}

        /// <summary>
        /// Adds a <see cref="DestinationData"/> configuration to the collection
        /// with the <see cref="IBatchData.DisplayName"/> as the key.
        /// </summary>
        /// <param name="data">The data instance to be added.</param>
        /// <exception cref="ArgumentNullException">The data is null.</exception>
        /// <exception cref="InvalidOperationException">The <see cref="IBatchData.DisplayName"/> is null.</exception>
        /// <returns>void</returns>
		public new void Add(DestinationData data)
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
        /// Add a collection of the <see cref="DestinationData"/> to the existing collection.
        /// </summary>
        /// <param name="collection">the collection of the entries to add.</param>
        /// <exception cref="ArgumentNullException">Any object in collection
        /// is null.</exception>
        /// <exception cref="InvalidOperationException">The 
        /// <see cref="IBatchData.DisplayName"/> is null or the existing collection contains
        /// an entry with the same names as the ones in collection.
        /// </exception>
        /// <returns>void</returns>
		public void AddRange(DestinationCollection collection)
		{
			foreach(DestinationData data in collection)
			{
				this.Add(data);
			}
		}

        /// <summary>GetData</summary>
        /// <param name="index">int</param>
        /// <returns>Avanade.ACA.Batch.Configuration.DestinationData</returns>
        private DestinationData GetData(int index)
		{
			return (DestinationData) BaseGet(index);
		}

        /// <summary>GetData</summary>
        /// <param name="name">string</param>
        /// <returns>Avanade.ACA.Batch.Configuration.DestinationData</returns>
        private DestinationData GetData(string name)
		{
			return (DestinationData) BaseGet(name);
		}

        /// <summary>SetData</summary>
        /// <param name="index">int</param>
        /// <param name="data">Avanade.ACA.Batch.Configuration.DestinationData</param>
        /// <returns>void</returns>
        private void SetData(int index, DestinationData data)
        {            
            BaseAdd(index, data);
        }

        /// <summary>SetData</summary>
        /// <param name="name">string</param>
        /// <param name="data">Avanade.ACA.Batch.Configuration.DestinationData</param>
        /// <returns>void</returns>
        private void SetData(string name, DestinationData data)
        {
            BaseAdd(data);
        }
	}
}
