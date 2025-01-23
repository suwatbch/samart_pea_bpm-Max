// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Data;
using System.Collections;
using System.Runtime.Serialization;
using System.Xml;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.IO;
using Avanade.ACA.Batch.Configuration.IO;
using System.ComponentModel;

namespace Avanade.ACA.Batch.Configuration
{
	/// <summary>
	/// ParameterCollection is a collection of the parameter data.  Each element in the
	/// collection is of the type <see cref="ParameterData"/>.
	/// </summary>    
	[Serializable]
    public class ParameterCollection : NamedElementCollection<ParameterData>, IBatchDBData
	{
		private string displayName = "Parameters";

		/// <summary>
		/// The key for the object that owns the parameter collection.
		/// </summary>
		protected Guid parentKey = Guid.Empty;

        /// <summary>ParameterCollection</summary>
        /// <param name="info">System.Runtime.Serialization.SerializationInfo</param>
        /// <param name="context">System.Runtime.Serialization.StreamingContext</param>
        private ParameterCollection(SerializationInfo info, StreamingContext context)
            : base()
		{
		}

        /// <summary>
        /// Creates a new instance of the collection.
        /// </summary>
		public ParameterCollection() : base()
		{
		}

        /// <summary>
        /// Loads parameters from the specfied database
        /// </summary>
        /// <param name="batchDB">Instance of the Batch database</param>
        /// <param name="parentKey">Guid identifying parent instance</param>
        /// <param name="cat">The category of the parameter collection parent</param>
        /// <returns>void</returns>
		public void LoadParameters(Database batchDB, Guid parentKey, ParameterCategory cat)
		{            
			this.parentKey = parentKey;
			IDataReader rdr = null;

            try
            {
                rdr = DefaultBatchManager.Parameter.List(batchDB, parentKey, cat);

                while (rdr.Read())
                {
                    string stringval = String.Empty;
                    ParameterData data = null;
                    if (!rdr.IsDBNull(3))
                    {
                        stringval = rdr.GetString(3);
                        XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(stringval));
                        string typeName = string.Empty;
                        while (xmlTextReader.Read())
                        {
                            if (xmlTextReader.Name == "parameter")
                            {
                                typeName = xmlTextReader.GetAttribute("typeName");
                                break;
                            }
                        }
                        
                        switch (typeName)
                        {
                            case "FileParameterData":
                                data = ParameterData.Deserialize<FileParameterData, FileParameterDataProxy>(stringval);
                                break;
                            case "FileParameterData[]":
                                data = ParameterData.Deserialize<FileParameterArrayData, FileParameterArrayDataProxy>(stringval);
                                break;
                            case "CharacterSeparatedFileParameterData":
                                data = ParameterData.Deserialize<CharacterSeparatedFileParameterData, CharacterSeparatedFileParameterDataProxy>(stringval);
                                break;
                            case "FileSetParameterData":
                                data = ParameterData.Deserialize<FileSetParameterData, FileSetParameterDataProxy>(stringval);
                                break;
                            case "FixedWidthFileParameterData":
                                data = ParameterData.Deserialize<FixedWidthFileParameterData, FixedWidthFileParameterDataProxy>(stringval);
                                break;
                            case "XmlFileParameterData":
                                data = ParameterData.Deserialize<XmlFileParameterData, XmlFileParameterDataProxy>(stringval);
                                break;
                            default:
                                data = ParameterData.Deserialize<ParameterData, ParameterDataProxy>(stringval);
                                break;
                        }
                        data.DisplayName = data.Name;
                        data.Key = rdr.GetGuid(0);
                        data.ValueTypeKey = rdr.GetGuid(6);
                        this.Add(data);
                    }
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
        /// Saves parameters to the specified database
        /// </summary>
        /// <param name="batchDB">Instance of the Batch database</param>
        /// <param name="parentKey">Guid identifying parent instance</param>
        /// <param name="cat">The category of the parameter collection parent</param>
        /// <returns>void</returns>
		public void SaveParameters(Database batchDB, Guid parentKey, ParameterCategory cat)
		{
			IEnumerator enumer = this.GetEnumerator();
			while (enumer.MoveNext())
			{
				ParameterData data = (ParameterData) enumer.Current;
				Guid ikey = data.Key;
				Guid vkey = data.ValueTypeKey;
				DefaultBatchManager.Parameter.Save(batchDB,ref ikey,data.DisplayName,data.ValueType,ref vkey,parentKey,data.Serialize(), cat);
				data.Key = ikey;
				data.ValueTypeKey = vkey;
			}
		}

        /// <summary>
        /// Deletes the parameters from the Batch database whose 
        /// <see cref="IBatchDBData.Key"/> is in an array.
        /// </summary>
        /// <param name="batchDB">The Batch database instance.</param>
        /// <param name="removedKeys">The <see cref="ArrayList"/> containing the <see cref="IBatchDBData.Key"/> 
        /// of the parameters to be removed.</param>
        /// <returns>void</returns>
		public void DeleteParameters(Database batchDB, ArrayList removedKeys)
		{
			foreach(Guid key in removedKeys)
			{
				DefaultBatchManager.Parameter.Delete(batchDB, key);
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
		/// Implements the <see cref="IBatchDBData.Key"/> property.  Returns the key of the
		/// <see cref="IBatchDBData"/> instance this collection of parameters belongs to.
		/// </summary>
		public Guid Key
		{
			get {return this.parentKey;}
		}

		/// <summary>
		/// See <see cref="IBatchDBData.Children"/>.
		/// </summary>
		public IList Children
		{
			get
			{
				return new ArrayList(this);
			}
		}

		/// <summary>
		/// Gets the Parameter at the specified integer index.
		/// </summary>
        [Browsable(false)]
        public ParameterData this[int index]
        {
            get
            {
                return BaseGet(index) as ParameterData;
            }
        }

		/// <summary>
		/// Gets or sets a <see cref="ParameterData"/> in the collection by name.  
		/// If there's no entry exists in the collection, the getter returns null and 
		/// the setting adds a new entry with the name as the key.
		/// </summary>
		/// <exception cref="ArgumentNullException">If the name if null.</exception>
        [Browsable(false)]
		public new ParameterData this[string name]
		{
			get 
			{
				if (name == null)
				{
					throw new ArgumentNullException("name");
				}
				return BaseGet(name) as ParameterData;
			}
			set
			{
				if (name == null)
				{
					throw new ArgumentNullException("name");
				}                  
                BaseAdd(value);
			}
		}

        /// <summary>
        /// Adds a <see cref="ParameterData"/> configuration to the collection
        /// with the <see cref="IBatchData.DisplayName"/> as the key.
        /// </summary>
        /// <param name="data">The data instance to be added.</param>
        /// <exception cref="ArgumentNullException">The data is null.</exception>
        /// <exception cref="InvalidOperationException">The <see cref="IBatchData.DisplayName"/> is null.</exception>
        /// <returns>void</returns>
		public new void Add(ParameterData data)
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
        /// Sets the <see cref="ParameterData.Value"/> property of the parameter with the specified name.
        /// </summary>
        /// <param name="name">The name of the <see cref="ParameterData"/> to search.</param>
        /// <param name="val">the primitive values to set.</param>
        /// <remarks>See <see cref="ParameterData"/> for a list of the types for the primitive values.
        /// </remarks>
        /// <returns>void</returns>
		public void SetPrimitiveData(string name, object val)
		{
			ParameterData data = BaseGet(name) as ParameterData;
			if (data == null)
			{
				data = new ParameterData(name);
				data.ValueType = val.GetType().Name;
				data.Value = val;                
				BaseAdd(data);
			}
			else
			{
				data.ValueType = val.GetType().Name;
				data.Value = val;                
				BaseAdd(data);			
			}
		}


        /// <summary>
        /// Gets the value of the primitive parameter with a name.
        /// </summary>
        /// <param name="name">The name of the <see cref="ParameterData"/></param>
        /// <returns>The primitive value of the parameter; null if not found.</returns>
		public object GetPrimitiveData(string name)
		{
			ParameterData data = BaseGet(name) as ParameterData;
			if (data != null)
			{
				return data.Value;
			}
			return null;
		}

        /// <summary>
        /// Add a collection of the <see cref="ParameterData"/> to the existing collection.
        /// </summary>
        /// <param name="collection">the collection of the entries to add.</param>
        /// <exception cref="ArgumentNullException">Any object in collection
        /// is null.</exception>
        /// <exception cref="InvalidOperationException">The 
        /// <see cref="IBatchData.DisplayName"/> is null or the existing collection contains
        /// an entry with the same names as the ones in collection.
        /// </exception>
        /// <returns>void</returns>
		public void AddRange(ParameterCollection collection)
		{
			foreach(ParameterData data in collection)
			{
				this.Add(data);
			}
		}
	}
}
