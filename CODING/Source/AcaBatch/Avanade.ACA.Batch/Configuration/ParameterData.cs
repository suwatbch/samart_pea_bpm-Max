// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.ComponentModel;
using Avanade.ACA.Batch;
using Avanade.ACA.Batch.Configuration.IO;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Configuration;
using System.Reflection;

namespace Avanade.ACA.Batch.Configuration
{
	/// <summary>
	/// ParameterData represents the configuration for a parameter.  It can be for 
	/// different types of data.  Please refer to <see cref="ParameterCategory"/> for its application.
	/// This class is also the base class for the more complex types of the parameters that need
	/// more than one value property to express its value fully.  Such as the 
	/// <see cref="FileParameterData"/> class.
	/// </summary>
	/// <remarks>The primitive value types include: <UL>
	/// <LI><see cref="Boolean"/></LI>
	/// <LI><see cref="Boolean"/>[]</LI>
	/// <LI><see cref="Double"/></LI>
	/// <LI><see cref="Double"/>[]</LI>
	/// <LI><see cref="Decimal"/></LI>
	/// <LI><see cref="Decimal"/>[]</LI>
	/// <LI><see cref="Single"/></LI>
	/// <LI><see cref="Single"/>[]</LI>
	/// <LI><see cref="Int16"/></LI>
	/// <LI><see cref="Int16"/>[]</LI>
	/// <LI><see cref="Int32"/></LI>
	/// <LI><see cref="Int32"/>[]</LI>
	/// <LI><see cref="Int64"/></LI>
	/// <LI><see cref="Int64"/>[]</LI>
	/// <LI><see cref="String"/></LI>
	/// <LI><see cref="String"/>[]</LI>
	/// <LI><see cref="Char"/></LI>
	/// <LI><see cref="Char"/>[]</LI> 
	/// <LI><see cref="DateTime"/></LI>
	/// <LI><see cref="DateTime"/>[]</LI>
	/// <LI><see cref="TimeSpan"/></LI>
	/// <LI><see cref="TimeSpan"/>[]</LI>
	/// <LI><see cref="Byte"/></LI>
	/// <LI><see cref="Byte"/>[]</LI> 
	/// </UL></remarks>    
    public class ParameterData : NamedConfigurationElement, IBatchDBData
	{
		#region Static Members
		private static Hashtable _types;
		private static string[] _typeKeys;
		private static XmlSerializer _serializer;        
		#endregion
        
		private string	displayName;
		private string	valueType = "String";
		private object	valueData = string.Empty;
		private Guid	instanceKey;
		private Guid	valueTypeKey;
        private const string typeNameProperty = "typeName";        
        private ParameterDataProxy parameterDataProxy = null;

        #region Static Methods
        /// <summary>ParameterData</summary>
        static ParameterData()
        {
            Type[] types = new Type[]
                {
                    typeof(Boolean),
                    typeof(Boolean[]),
                    typeof(Double),
                    typeof(Double[]),
                    typeof(Decimal),
                    typeof(Decimal[]),
                    typeof(Single),
                    typeof(Single[]),
                    typeof(Int16),
                    typeof(Int16[]),
                    typeof(Int32),
                    typeof(Int32[]),
                    typeof(Int64),
                    typeof(Int64[]),
                    typeof(String),
                    typeof(String[]),
                    typeof(Char),
                    typeof(Char[]),
                    typeof(DateTime),
                    typeof(DateTime[]),
                    typeof(TimeSpan),
                    typeof(TimeSpan[]),
                    typeof(Byte),
                    typeof(Byte[])
                };

            _types = new Hashtable();

            foreach (Type t in types)
            {
                _types.Add(t.Name, t);
            }

            int count = _types.Keys.Count;
            _typeKeys = new string[count];
            _types.Keys.CopyTo(_typeKeys, 0);

            ParameterDataProxy._typeKeys = _typeKeys;
            ParameterDataProxy._types = _types;
        }

        /// <summary>
        /// Returns the type of the primitive type matches the typeAlias.
        /// </summary>
        /// <param name="typeAlias">The type name.</param>
        /// <returns>A <see cref="Type"/> object; null if no match found.</returns>
        public static Type GetParameterType(string typeAlias)
        {
            return (Type)_types[typeAlias];
        }

        /// <summary>
        /// Gets the acceptable primitive type.  See <see cref="ParameterData"/> for details.
        /// </summary>
        /// <returns>A string array containing the type name of all the acceptable primitive type.</returns>
        public static string[] GetTypeKeys()
        {
            return (string[])_typeKeys.Clone();
        }

        /// <summary>
        /// An <see cref="XmlSerializer"/> is 
        /// an expensive object to create. An
        /// XmlSerializer for the Request type
        /// is created and saved in memory for 
        /// the life of the current AppDomain.
        /// </summary>
        private static XmlSerializer Serializer
        {
            [MethodImpl(MethodImplOptions.Synchronized)]	
            get
            {
                if (_serializer == null)
                {
                    _serializer = new XmlSerializer(typeof(ParameterDataProxy));	
                }
                return _serializer;
            }
        }

        /// <summary>GenericSerializer</summary>
        private static XmlSerializer GenericSerializer<T>()
        {            
            if (_serializer == null)
            {
                //_serializer = new XmlSerializer(typeof(T));
                _serializer = new XmlSerializer(typeof(ParameterDataProxy));
            }
            return _serializer;
        }
        #endregion

        /// <summary>
        /// Initializes the object with empty name and a empty <see cref="Key"/>.
        /// </summary>
		public ParameterData() : this(string.Empty, Guid.Empty)
		{
		}

        /// <summary>
        /// Initializes the object with a name and a empty <see cref="Key"/>.
        /// </summary>
        /// <param name="displayName">The display name.</param>
		public  ParameterData(string displayName) : this(displayName, Guid.Empty)
		{	
		}

        /// <summary>
        /// Initializes the object with a name and a <see cref="Key"/>.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="key">The key.</param>
		public ParameterData(string displayName, Guid key) : base(displayName)
		{            
            parameterDataProxy = new ParameterDataProxy(displayName, key);            
			this.displayName = displayName;
			this.instanceKey = key;
			this.valueTypeKey = Guid.Empty;;
		}

        /// <summary>
        /// Initializes the object with a name and a empty </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="parameterDataProxy">Avanade.ACA.Batch.Configuration.ParameterDataProxy</param>
        public ParameterData(string displayName, ParameterDataProxy parameterDataProxy)
            : this(displayName, Guid.Empty, parameterDataProxy)
        {
        }

        /// <summary>
        /// Initializes the object with a name and a </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="key">The key.</param>
        /// <param name="parameterDataProxy">Avanade.ACA.Batch.Configuration.ParameterDataProxy</param>
        public ParameterData(string displayName, Guid key, ParameterDataProxy parameterDataProxy)
            : base(displayName)
        {
            this.parameterDataProxy = parameterDataProxy;            
            this.displayName = displayName;
            this.instanceKey = key;
            this.valueTypeKey = Guid.Empty; ;
        }

        /// <summary>
        /// Initializes the object with a name and a </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="key">The key.</param>
        /// <param name="parameterDataProxy">Avanade.ACA.Batch.Configuration.ParameterDataProxy</param>
        /// <param name="valueType">System.String</param>
        /// <param name="valueData">System.Object</param>
        public ParameterData(string displayName, Guid key, ParameterDataProxy parameterDataProxy, string valueType, object valueData)
            : base(displayName)
        {
            this.parameterDataProxy = parameterDataProxy;
            this.displayName = displayName;
            this.instanceKey = key;
            this.valueTypeKey = Guid.Empty;
            this.valueData = valueData;
            this.valueType = valueType;
        }

        public ParameterDataProxy ParameterDataProxy
        {
            get { return this.parameterDataProxy;}
        }

		/// <summary>
		/// See <see cref="IBatchData.DisplayName"/>.
		/// </summary>
        [ConfigurationProperty(nameProperty)]
		public string DisplayName
		{
			get
			{
				return displayName;
			}
			set
			{
                base.Name = displayName = value;
                parameterDataProxy.DisplayName = value;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="Type"/> of the value noted that after setting this property,
		/// the <see cref="Value"/> will be set to the default value of the type.
		/// </summary>
        [ConfigurationProperty(typeNameProperty)]        
		public virtual string ValueType
		{
			get 
			{ 
				return valueType;
			}
			set
			{
                valueType = value;
                valueData = CreateDefaultValue(value);
                valueTypeKey = Guid.Empty;                
                parameterDataProxy.ValueType = value;
			}
		}

		/// <summary>
		/// Gets or sets the primitive value of the parameter data.  After setting this property, the 
		/// <see cref="ValueType"/> will be affected.
		/// </summary>
		public virtual object Value
		{
			get
			{
				return valueData;
			}
			set 
			{
				if (valueData.GetType() != value.GetType())
                    valueType = value.GetType().Name;
                valueData = value;
                parameterDataProxy.Value = value;
			}
		}

		/// <summary>
		/// Implements the <see cref="IBatchDBData.Key"/> property.  Gets or sets the key for the data.
		/// </summary>
		/// <value>a <see cref="System.Guid"/> instance as the key of the data object.</value>
		/// <remarks>This property is used only for JobDefiniton/BatchDefinition parameters</remarks>		
		public Guid Key
		{
			get
			{
				return this.instanceKey;
			}
			set
			{                
                this.instanceKey = value;
			}
		}

		/// <summary>
		/// See <see cref="IBatchDBData.Children"/>.
		/// </summary>		
		public virtual IList Children
		{
			get
			{
				return new ArrayList();
			}
		}

		/// <summary>
		/// Gets or sets the unique identifier of the parameter value type.  When this property is
		/// Guid.Empty, it means the <see cref="ParameterData"/> instance has not sync up with the
		/// Batch database.
		/// </summary>		
		public Guid ValueTypeKey
		{
			get
			{
				return this.valueTypeKey;
			}
			set
			{
				this.valueTypeKey = value;
                parameterDataProxy.ValueTypeKey = value;
			}
		}

        /// <summary>CreateDefaultValue</summary>
        /// <param name="valueType">string</param>
        /// <returns>object</returns>
        private object CreateDefaultValue(string valueType)
		{
			object defaultValue;

			Type t = GetParameterType(valueType);
			
			if (t == typeof(String))
			{
				defaultValue = String.Empty;
			}
			else
			{
				TypeConverter converter = TypeDescriptor.GetConverter(t);

				if (converter is XmlSerializableTypeConverter)
				{
					XmlSerializableTypeDescriptorContext context
						= new XmlSerializableTypeDescriptorContext(t);

					defaultValue = converter.CreateInstance(context, null);
				}
				else
				{
					bool supported = converter.GetCreateInstanceSupported();

					if (t.IsArray)
					{
						Type elementType = t.GetElementType();
						defaultValue = Array.CreateInstance(elementType, 0);
					}
					else if (supported)
					{
						defaultValue = converter.CreateInstance(null);
					}
					else
					{
						defaultValue = Activator.CreateInstance(t);	
					}
				}
			}            
			return defaultValue;
		}

        /// <summary>
        /// Performs XML serialization on the <see cref="ParameterData"/> instance.
        /// </summary>
        /// <returns>The serialized Xml text as the results of the XML serialization.</returns>
        public string Serialize()
        {
            StringBuilder parameterXml = new StringBuilder();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(new StringWriter(parameterXml, CultureInfo.InvariantCulture));
            xmlTextWriter.Formatting = Formatting.None;
            Serializer.Serialize(xmlTextWriter, this.parameterDataProxy);
            xmlTextWriter.Close();
            return parameterXml.ToString();
        }


        /// <summary>
        /// Performs deserialization on an XML string and constructs a new </summary>
        /// <param name="xmlString">The string build from XML-serialization of an instance of
        /// the </param>
        /// <returns>Avanade.ACA.Batch.Configuration.ParameterData</returns>
        public static ParameterData Deserialize<T1, T2>(string xmlString) 
            where T1: ParameterData
            where T2 : ParameterDataProxy
        {            
            XmlTextReader xmlReader = new XmlTextReader(new StringReader(xmlString));
            T2 parameterDataProxy = (T2)GenericSerializer<T2>().Deserialize(xmlReader);
            return GetParameterData<T1, T2>(parameterDataProxy);                        
        }

        /// <summary>GetParameterData</summary>
        /// <param name="proxy">Avanade.ACA.Batch.Configuration.ParameterDataProxy</param>
        /// <returns>Avanade.ACA.Batch.Configuration.ParameterData</returns>
        private static ParameterData GetParameterData<T1, T2>(T2 proxy) where T1: ParameterData where T2: ParameterDataProxy
        {
            T1 parameterData = Activator.CreateInstance<T1>();
            Clone(proxy, parameterData);
            return parameterData;
        }

        /// <summary>Clone</summary>
        /// <param name="baseObject">T1</param>
        /// <param name="resultantObject">T2</param>
        public static void Clone<T1, T2>(T1 baseObject, T2 resultantObject)
            where T1 : ParameterDataProxy
            where T2 : ParameterData
        {
            foreach (PropertyInfo propInfo in baseObject.GetType().GetProperties())
            {
                PropertyInfo propertyInfo = resultantObject.GetType().GetProperty(propInfo.Name);
                if (propertyInfo.CanWrite)
                {
                    if (resultantObject.GetType() == typeof(FileParameterArrayData) && (propertyInfo.Name == "Value" || propertyInfo.Name == "Items"))
                        resultantObject.Value = GetValue(resultantObject, baseObject.Value as FileParameterDataProxy[]);                    
                    else
                        propertyInfo.SetValue(resultantObject, propInfo.GetValue(baseObject, null), null);
                }
            }
        }

        /// <summary>GetValue</summary>
        /// <param name="parameterData">Avanade.ACA.Batch.Configuration.ParameterData</param>
        /// <param name="items">Avanade.ACA.Batch.Configuration.IO.FileParameterDataProxy[]</param>
        /// <returns>Avanade.ACA.Batch.Configuration.IO.FileParameterData[]</returns>
        public static FileParameterData[] GetValue(ParameterData parameterData, FileParameterDataProxy[] items)
        {
            FileParameterData[] fileParameters = new FileParameterData[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].GetType() == typeof(CharacterSeparatedFileParameterDataProxy))
                {
                    CharacterSeparatedFileParameterDataProxy parameterDataProxy = (CharacterSeparatedFileParameterDataProxy)items[i];
                    fileParameters[i] = new CharacterSeparatedFileParameterData(parameterDataProxy.DisplayName, parameterDataProxy.FullPath, parameterDataProxy.Separator);
                }
                else if (items[i].GetType() == typeof(FileParameterDataProxy))
                {
                    FileParameterDataProxy parameterDataProxy = (FileParameterDataProxy)items[i];
                    fileParameters[i] = new FileParameterData(parameterDataProxy.DisplayName, parameterDataProxy.FullPath);
                }
                else if (items[i].GetType() == typeof(FileSetParameterDataProxy))
                {
                    FileSetParameterDataProxy parameterDataProxy = (FileSetParameterDataProxy)items[i];
                    fileParameters[i] = new FileSetParameterData(parameterDataProxy.DisplayName, parameterDataProxy.FullPath, parameterDataProxy.SearchPattern);
                }
                else if (items[i].GetType() == typeof(FixedWidthFileParameterDataProxy))
                {
                    FixedWidthFileParameterDataProxy parameterDataProxy = (FixedWidthFileParameterDataProxy)items[i];
                    fileParameters[i] = new FixedWidthFileParameterData(parameterDataProxy.DisplayName, parameterDataProxy.FullPath, parameterDataProxy.ColumnWidths);
                }
                else if (items[i].GetType() == typeof(XmlFileParameterDataProxy))
                {
                    XmlFileParameterDataProxy parameterDataProxy = (XmlFileParameterDataProxy)items[i];
                    fileParameters[i] = new XmlFileParameterData(parameterDataProxy.DisplayName, parameterDataProxy.FullPath, parameterDataProxy.RecordsXPath, parameterDataProxy.ColumnXPaths);
                }
            }
            return fileParameters;
        }

        /// <summary>GetProxy</summary>
        /// <param name="items">Avanade.ACA.Batch.Configuration.IO.FileParameterData[]</param>
        public static FileParameterDataProxy[] GetProxy(FileParameterData[] items)
        {
            FileParameterDataProxy[] proxyParameters = new FileParameterDataProxy[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].GetType() == typeof(CharacterSeparatedFileParameterData))
                {
                    CharacterSeparatedFileParameterData parameterData = (CharacterSeparatedFileParameterData)items[i];
                    proxyParameters[i] = new CharacterSeparatedFileParameterDataProxy(parameterData.DisplayName, parameterData.FullPath, parameterData.Separator);
                }
                else if (items[i].GetType() == typeof(FileParameterData))
                {
                    FileParameterData parameterData = (FileParameterData)items[i];
                    proxyParameters[i] = new FileParameterDataProxy(parameterData.DisplayName, parameterData.FullPath);
                }
                else if (items[i].GetType() == typeof(FileSetParameterData))
                {
                    FileSetParameterData parameterData = (FileSetParameterData)items[i];
                    proxyParameters[i] = new FileSetParameterDataProxy(parameterData.DisplayName, parameterData.FullPath, parameterData.SearchPattern);
                }
                else if (items[i].GetType() == typeof(FixedWidthFileParameterData))
                {
                    FixedWidthFileParameterData parameterData = (FixedWidthFileParameterData)items[i];
                    proxyParameters[i] = new FixedWidthFileParameterDataProxy(parameterData.DisplayName, parameterData.FullPath, parameterData.ColumnWidths);
                }
                else if (items[i].GetType() == typeof(XmlFileParameterData))
                {
                    XmlFileParameterData parameterData = (XmlFileParameterData)items[i];
                    proxyParameters[i] = new XmlFileParameterDataProxy(parameterData.DisplayName, parameterData.FullPath, parameterData.RecordsXPath, parameterData.ColumnXPaths);
                }
            }
            return proxyParameters;
        }
    }

    #region - Proxy Class for Deserialization since ParameterData cannot be Deserialized as it inherits from NamedConfigurationElement (ConfigurationElement)-
    [XmlType("parameter", IncludeInSchema = false)]
    [XmlInclude(typeof(CharacterSeparatedFileParameterDataProxy))]
    [XmlInclude(typeof(FileParameterDataProxy))]
    [XmlInclude(typeof(FileParameterDataProxy[]))]
    [XmlInclude(typeof(FileSetParameterDataProxy))]
    [XmlInclude(typeof(FixedWidthFileParameterDataProxy))]
    [XmlInclude(typeof(XmlFileParameterDataProxy))]
    [XmlInclude(typeof(FileParameterArrayDataProxy))]
    [XmlInclude(typeof(Boolean[]))]
    [XmlInclude(typeof(Double[]))]
    [XmlInclude(typeof(Decimal[]))]
    [XmlInclude(typeof(Single[]))]
    [XmlInclude(typeof(Int16[]))]
    [XmlInclude(typeof(Int32[]))]
    [XmlInclude(typeof(Int64[]))]
    [XmlInclude(typeof(String[]))]
    [XmlInclude(typeof(Char[]))]
    [XmlInclude(typeof(DateTime[]))]
    [XmlInclude(typeof(TimeSpan[]))]
    [XmlInclude(typeof(Byte[]))]
    public class ParameterDataProxy
    {
        private string displayName;
        private string valueType = "String";
        private object valueData = string.Empty;
        private Guid instanceKey;
        private Guid valueTypeKey;        
        [XmlIgnore]
        internal static Hashtable _types;
        [XmlIgnore]
        internal static string[] _typeKeys;

        /// <summary>ParameterDataProxy</summary>
        static ParameterDataProxy()
        {
            Type[] types = new Type[]
                {
                    typeof(Boolean),
                    typeof(Boolean[]),
                    typeof(Double),
                    typeof(Double[]),
                    typeof(Decimal),
                    typeof(Decimal[]),
                    typeof(Single),
                    typeof(Single[]),
                    typeof(Int16),
                    typeof(Int16[]),
                    typeof(Int32),
                    typeof(Int32[]),
                    typeof(Int64),
                    typeof(Int64[]),
                    typeof(String),
                    typeof(String[]),
                    typeof(Char),
                    typeof(Char[]),
                    typeof(DateTime),
                    typeof(DateTime[]),
                    typeof(TimeSpan),
                    typeof(TimeSpan[]),
                    typeof(Byte),
                    typeof(Byte[])
                };

            _types = new Hashtable();

            foreach (Type t in types)
            {
                _types.Add(t.Name, t);
            }

            int count = _types.Keys.Count;
            _typeKeys = new string[count];
            _types.Keys.CopyTo(_typeKeys, 0);
        }

        /// <summary>
        /// Initializes the object with empty name and a empty </summary>
        /// <param name="displayName">System.String</param>
        public ParameterDataProxy(string displayName)
            : this(displayName, Guid.Empty)
        {
        }

        /// <summary>
        /// Initializes the object with empty name and a empty </summary>
        public ParameterDataProxy()
            : this(string.Empty, Guid.Empty)
        {
        }

        /// <summary>
        /// Initializes the object with a name and a </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="key">The key.</param>
        public ParameterDataProxy(string displayName, Guid key)
        {
            this.displayName = displayName;
            this.instanceKey = key;
            this.valueTypeKey = Guid.Empty; ;
        }

        /// <summary>
        /// See <see cref="IBatchData.DisplayName"/>.
        /// </summary>
        [XmlAttribute("name")]
        public string DisplayName
        {
            get
            {
                return displayName;
            }
            set
            {
                displayName = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Type"/> of the value noted that after setting this property,
        /// the <see cref="Value"/> will be set to the default value of the type.
        /// </summary>
        [XmlAttribute("typeName")]
        public virtual string ValueType
        {
            get
            {
                return valueType;
            }
            set
            {
                valueType = value;
                valueData = CreateDefaultValue(value);
                valueTypeKey = Guid.Empty;
            }
        }

        /// <summary>
        /// Gets or sets the primitive value of the parameter data.  After setting this property, the 
        /// <see cref="ValueType"/> will be affected.
        /// </summary>
        public virtual object Value
        {
            get
            {
                return valueData;
            }
            set
            {
                if (valueData.GetType() != value.GetType())
                    valueType = value.GetType().Name;
                valueData = value;
            }
        }

        /// <summary>
        /// Implements the <see cref="IBatchDBData.Key"/> property.  Gets or sets the key for the data.
        /// </summary>
        /// <value>a <see cref="System.Guid"/> instance as the key of the data object.</value>
        /// <remarks>This property is used only for JobDefiniton/BatchDefinition parameters</remarks>
        [XmlIgnore]
        public Guid Key
        {
            get
            {
                return this.instanceKey;
            }
            set
            {
                this.instanceKey = value;
            }
        }

        /// <summary>
        /// See <see cref="IBatchDBData.Children"/>.
        /// </summary>
        [XmlIgnore]
        public virtual IList Children
        {
            get
            {
                return new ArrayList();
            }
        }

        /// <summary>
        /// Gets or sets the unique identifier of the parameter value type.  When this property is
        /// Guid.Empty, it means the <see cref="ParameterData"/> instance has not sync up with the
        /// Batch database.
        /// </summary>
        [XmlIgnore]
        public Guid ValueTypeKey
        {
            get
            {
                return this.valueTypeKey;
            }
            set
            {
                this.valueTypeKey = value;
            }
        }

        /// <summary>CreateDefaultValue</summary>
        /// <param name="valueType">System.String</param>
        /// <returns>object</returns>
        private object CreateDefaultValue(string valueType)
        {
            object defaultValue;

            Type t = GetParameterType(valueType);

            if (t == typeof(String))
            {
                defaultValue = String.Empty;
            }
            else
            {
                TypeConverter converter = TypeDescriptor.GetConverter(t);

                if (converter is XmlSerializableTypeConverter)
                {
                    XmlSerializableTypeDescriptorContext context
                        = new XmlSerializableTypeDescriptorContext(t);

                    defaultValue = converter.CreateInstance(context, null);
                }
                else
                {
                    bool supported = converter.GetCreateInstanceSupported();

                    if (t.IsArray)
                    {
                        Type elementType = t.GetElementType();
                        defaultValue = Array.CreateInstance(elementType, 0);
                    }
                    else if (supported)
                    {
                        defaultValue = converter.CreateInstance(null);
                    }
                    else
                    {
                        defaultValue = Activator.CreateInstance(t);
                    }
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// Returns the type of the primitive type matches the typeAlias.
        /// </summary>
        /// <param name="typeAlias">The type name.</param>
        /// <returns>System.Type</returns>
        public static Type GetParameterType(string typeAlias)
        {
            return (Type)_types[typeAlias];
        }
    }
    #endregion
}
