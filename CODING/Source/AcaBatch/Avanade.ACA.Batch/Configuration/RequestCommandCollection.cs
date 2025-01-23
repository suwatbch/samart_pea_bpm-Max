// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Avanade.ACA.Batch.Configuration
{
	/// <summary>
	/// RequestCommandCollection represents a collection of <see cref="RequestCommandData"/>
	/// used for enqueuing a Batch request.
	/// </summary>    
    public class RequestCommandCollection : NamedElementCollection<RequestCommandData>
	{
        /// <summary>
        /// Creates a new instance of the RequestCommandCollection.
        /// </summary>
		public RequestCommandCollection()
		{
		}

		/// <summary>
		/// Returns the RequestCommand at the specified integer index.
		/// </summary>
		public RequestCommandData this[int index]
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
		/// Gets or sets a <see cref="RequestCommandData"/> in the collection by name.  
		/// If there's no entry exists in the collection, the getter returns null and 
		/// the setting adds a new entry with the name as the key.
		/// </summary>
		/// <exception cref="ArgumentNullException">If the name if null.</exception>
		public new RequestCommandData this[string name]
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
        /// Adds a <see cref="RequestCommandData"/> configuration to the collection
        /// with the <see cref="IBatchData.DisplayName"/> as the key.
        /// </summary>
        /// <param name="data">The data instance to be added.</param>
        /// <exception cref="ArgumentNullException">The data is null.</exception>
        /// <exception cref="InvalidOperationException">The <see cref="IBatchData.DisplayName"/> is null.</exception>
        /// <returns>void</returns>
		public new void Add(RequestCommandData data)
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
        /// Add a collection of the <see cref="RequestCommandData"/> to the existing collection.
        /// </summary>
        /// <param name="collection">the collection of the entries to add.</param>
        /// <exception cref="ArgumentNullException">Any object in collection
        /// is null.</exception>
        /// <exception cref="InvalidOperationException">The 
        /// <see cref="IBatchData.DisplayName"/> is null or the existing collection contains
        /// an entry with the same names as the ones in collection.
        /// </exception>
        /// <returns>void</returns>
		public void AddRange(RequestCommandCollection collection)
		{
			foreach(RequestCommandData data in collection)
			{
				this.Add(data);
			}
		}

        /// <summary>GetData</summary>
        /// <param name="index">int</param>
        /// <returns>Avanade.ACA.Batch.Configuration.RequestCommandData</returns>
        private RequestCommandData GetData(int index)
		{
			return (RequestCommandData) BaseGet(index);
		}

        /// <summary>GetData</summary>
        /// <param name="name">string</param>
        /// <returns>Avanade.ACA.Batch.Configuration.RequestCommandData</returns>
        private RequestCommandData GetData(string name)
		{
			return (RequestCommandData) BaseGet(name);
		}

        /// <summary>SetData</summary>
        /// <param name="index">int</param>
        /// <param name="data">Avanade.ACA.Batch.Configuration.RequestCommandData</param>
        /// <returns>void</returns>
        private void SetData(int index, RequestCommandData data)
        {
            BaseAdd(index, data);
        }

        /// <summary>SetData</summary>
        /// <param name="name">string</param>
        /// <param name="data">Avanade.ACA.Batch.Configuration.RequestCommandData</param>
        /// <returns>void</returns>
        private void SetData(string name, RequestCommandData data)
        {
            BaseAdd(data);
        }
	}
}
