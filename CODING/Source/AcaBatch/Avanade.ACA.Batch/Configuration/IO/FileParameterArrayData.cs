// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System.Collections;
using System.Xml.Serialization;
using System.Configuration;


namespace Avanade.ACA.Batch.Configuration.IO
{
	/// <summary>
	/// FileParameterArrayData represents an array of the <see cref="FileParameterData"/>.
	/// </summary>
	public class FileParameterArrayData : ParameterData
	{
		private FileParameterData[] items;
        private FileParameterArrayDataProxy fileParameterArrayDataProxy = null;
        /// <summary>
        /// Initializes a new instance of the <see cref="FileParameterArrayData"/> 
        /// with an empty string as name and an empty array.
        /// </summary>
		public FileParameterArrayData() : this(string.Empty, new FileParameterData[0])
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="FileParameterArrayData"/>
        /// with the empty array.
        /// </summary>
        /// <param name="name">the name of the data.</param>
		public FileParameterArrayData(string name) : this(name, new FileParameterData[0])
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="FileParameterArrayData"/></summary>
        /// <param name="name">the name of the data.</param>
        /// <param name="items">the <see cref="FileParameterData"/> items in the array</param>
        /// <remarks>The object in the <see cref="items"/> will be cloned and add to
        /// the new <see cref="FileParameterArrayData"/>.</remarks>
        public FileParameterArrayData(string name, FileParameterData[] items)
            : base(name, new FileParameterArrayDataProxy(name, items))
		{            
            fileParameterArrayDataProxy = (FileParameterArrayDataProxy)base.ParameterDataProxy;
            ParameterDataProxy[] parameterDataProxies = new ParameterDataProxy[items.Length];
            for (int j = 0; j < items.Length; j++)
            {
                FileParameterData child = items[j];
                FileParameterDataProxy fileParameterDataProxy = (FileParameterDataProxy)child.ParameterDataProxy;
                fileParameterArrayDataProxy.Items[j] = fileParameterDataProxy;
            }
            if (items.Length > 0 && items != null)
            {
                //fileParameterArrayDataProxy.Items = (FileParameterDataProxy[])parameterDataProxies;
            }
            this.items = (FileParameterData[])items.Clone();
		}

		/// <summary>
		/// Overrides the  <seealso cref="ParameterData.ValueType"/> property.
		/// always returns the string "FileParameter[]".
		/// </summary>
		[ConfigurationProperty( "typeName" )]
		public override string ValueType
		{
			get 
			{ 
				return "FileParameterData[]";
			}
			set
			{
			}
		}

		/// <summary>
		/// Gets or sets the array of <see cref="FileParameterData"/>.  When setting the
		/// array value, the array will be cloned.
		/// </summary>
		/// <value>an array of the <see cref="FileParameterData"/> objects.</value>
		public override object Value
		{
			get
			{
				return items;
			}
			set 
			{
				items = (FileParameterData[]) ((FileParameterData[])value).Clone();
                foreach (FileParameterData fileParameterData in items)
                {
                    if (fileParameterData == null)
                        return;
                }
                fileParameterArrayDataProxy.Items = ParameterData.GetProxy(items);
			}
		} 

		/// <summary>
		/// The array of <see cref="FileParameterData"/> stored in the
		/// instance of the <see cref="FileParameterArrayData"/>.
		/// </summary>
		/// <value>an array of the <see cref="FileParameterData"/> objects.</value>
		//[XmlIgnore]
		public FileParameterData[] Items
		{
			get
			{
                return items;                
			}
			set
			{
				items = (FileParameterData[])value.Clone();                
                foreach (FileParameterData fileParameterData in items)
                {
                    if (fileParameterData == null)                    
                        return;                    
                }
                fileParameterArrayDataProxy.Items = ParameterData.GetProxy(items);
			}
		}

        public FileParameterData[] ProxyItems
        {
            get
            {
                return items;
            }
            set
            {
                foreach (FileParameterData fileParameterData in items)
                {
                    if (fileParameterData == null)
                        return;
                }
                fileParameterArrayDataProxy.Items = ParameterData.GetProxy(items);
            }
        }

		/// <summary>
		/// See <see cref="IBatchDBData.Children"/>.
		/// </summary>
		//[XmlIgnore]
		public override IList Children
		{
			get
			{
				ArrayList list = new ArrayList();
				list.AddRange(items);
				return list;
			}
		}
    }

    #region - Proxy Class for Deserialization since ParameterData cannot be Deserialized as it inherits from NamedConfigurationElement (ConfigurationElement)-
    /// <summary>
    /// FileParameterArrayData represents an array of the <see cref="FileParameterData"/>.
    /// </summary>
    public class FileParameterArrayDataProxy : ParameterDataProxy
    {
        private FileParameterDataProxy[] items;

        /// <summary>
        /// Initializes a new instance of the </summary>
        public FileParameterArrayDataProxy()
            : this(string.Empty, new FileParameterData[0])
        {
        }

        /// <summary>
        /// Initializes a new instance of the </summary>
        /// <param name="name">the name of the data.</param>
        public FileParameterArrayDataProxy(string name)
            : this(name, new FileParameterData[0])
        {
        }

        /// <summary>
        /// Initializes a new instance of the </summary>
        /// <param name="name">the name of the data.</param>
        /// <param name="items">the </param>
        public FileParameterArrayDataProxy(string name, FileParameterData[] items)            
            : base(name)
        {
            this.items = new FileParameterDataProxy[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].GetType() == typeof(CharacterSeparatedFileParameterData))
                {
                    CharacterSeparatedFileParameterData parameterData = (CharacterSeparatedFileParameterData)items[i];
                    this.items[i] = new CharacterSeparatedFileParameterDataProxy(parameterData.DisplayName, parameterData.FullPath, parameterData.Separator);
                }
                else if (items[i].GetType() == typeof(FileParameterData))
                {
                    FileParameterData parameterData = (FileParameterData)items[i];
                    this.items[i] = new FileParameterDataProxy(parameterData.DisplayName, parameterData.FullPath);
                }
                else if (items[i].GetType() == typeof(FileSetParameterData))
                {
                    FileSetParameterData parameterData = (FileSetParameterData)items[i];
                    this.items[i] = new FileSetParameterDataProxy(parameterData.DisplayName, parameterData.FullPath, parameterData.SearchPattern);
                }
                else if (items[i].GetType() == typeof(FixedWidthFileParameterData))
                {
                    FixedWidthFileParameterData parameterData = (FixedWidthFileParameterData)items[i];
                    this.items[i] = new FixedWidthFileParameterDataProxy(parameterData.DisplayName, parameterData.FullPath, parameterData.ColumnWidths);
                }
                else if (items[i].GetType() == typeof(XmlFileParameterData))
                {
                    XmlFileParameterData parameterData = (XmlFileParameterData)items[i];
                    this.items[i] = new XmlFileParameterDataProxy(parameterData.DisplayName, parameterData.FullPath, parameterData.RecordsXPath, parameterData.ColumnXPaths);
                }                
            }
        }

        /// <summary>
        /// Overrides the  <seealso cref="ParameterData.ValueType"/> property.
        /// always returns the string "FileParameter[]".
        /// </summary>
        [XmlAttribute("typeName")]
        public override string ValueType
        {
            get
            {
                return "FileParameterData[]";
            }
            set
            {
            }
        }

        /// <summary>
        /// Gets or sets the array of <see cref="FileParameterData"/>.  When setting the
        /// array value, the array will be cloned.
        /// </summary>
        /// <value>an array of the <see cref="FileParameterData"/> objects.</value>
        public override object Value
        {
            get
            {
                return items;
            }
            set
            {
                items = (FileParameterDataProxy[])((FileParameterDataProxy[])value).Clone();
            }
        }

        /// <summary>
        /// The array of <see cref="FileParameterData"/> stored in the
        /// instance of the <see cref="FileParameterArrayData"/>.
        /// </summary>
        /// <value>an array of the <see cref="FileParameterData"/> objects.</value>
        [XmlIgnore]
        public FileParameterDataProxy[] Items
        {
            get
            {
                return items;
            }
            set
            {
                items = (FileParameterDataProxy[])value.Clone();
            }
        }

        /// <summary>
        /// See <see cref="IBatchDBData.Children"/>.
        /// </summary>
        [XmlIgnore]
        public override IList Children
        {
            get
            {
                ArrayList list = new ArrayList();
                list.AddRange(items);
                return list;
            }
        }
    }
    #endregion
}
