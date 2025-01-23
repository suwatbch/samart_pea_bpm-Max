// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System.Data;
using System.Xml.Serialization;
using Avanade.ACA.Batch.IO;
using System.Configuration;
using System;

namespace Avanade.ACA.Batch.Configuration.IO
{
	/// <summary>
	/// This class describes a file which contains text data.
	/// </summary>
	public class FileParameterData : ParameterData
	{
        private const string fullPathProperty = "fullPath";
        private const string typeNameProperty = "typeName";
        private FileParameterDataProxy fileParameterDataProxy = null;
        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="FileParameterData"/> class with empty display name and empty file path. 
        /// </summary>
		public FileParameterData() : this(string.Empty, string.Empty)
		{
		}

        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="FileParameterData"/> class with an empty file path. 
        /// </summary>
        /// <param name="name">The display name.</param>
		public FileParameterData(string name) : this(name, string.Empty)
		{
		}

        /// <summary>FileParameterData</summary>
        /// <param name="name">The display name.</param>
        /// <param name="fullPath">The full path for the file.</param>
        public FileParameterData(string name, string fullPath)
            : base(name, Guid.Empty, new FileParameterDataProxy(name, fullPath), "FileParameterData", null)
		{
            this.fileParameterDataProxy = (FileParameterDataProxy)base.ParameterDataProxy;
			this[fullPathProperty] = fullPath;
            this[typeNameProperty] = "FileParameterData";            
		}

        /// <summary>FileParameterData</summary>
        /// <param name="name">The display name.</param>
        /// <param name="fullPath">The full path for the file.</param>
        /// <param name="fileParameterDataProxy">Avanade.ACA.Batch.Configuration.IO.FileParameterDataProxy</param>
        public FileParameterData(string name, string fullPath, FileParameterDataProxy fileParameterDataProxy)
            : base(name, fileParameterDataProxy)
        {
            this.fileParameterDataProxy = fileParameterDataProxy;
            this[fullPathProperty] = fullPath;
            this[typeNameProperty] = "FileParameterData";
        }

		/// <summary>
		/// The full path of the file.
		/// </summary>
        [ConfigurationProperty(fullPathProperty)]
		public string FullPath
		{
			get
			{
				return this[fullPathProperty].ToString();
			}
			set
			{
                this[fullPathProperty] = fileParameterDataProxy.FullPath = value;                
			}
		}

		/// <summary>
		/// Overrides the  <seealso cref="ParameterData.ValueType"/> property.
		/// Always returns the string "FileParameterData".
		/// </summary>
        [ConfigurationProperty(typeNameProperty)]
		public override string ValueType
		{
			get 
			{ 
				return this[typeNameProperty].ToString();
			}
			set
			{
			}
		}

		/// <summary>
		/// Overrides the <seealso cref="ParameterData.ValueType"/> property.  Always returns null.
		/// </summary>
		public override object Value
		{
			get
			{
				return null;
			}
			set 
			{
			}
		}

        /// <summary>
        /// Create a reader class that implements the <see cref="IDataReader"/> interface 
        /// to read the file specified by its <see cref="FullPath"/> property.
        /// </summary>
        /// <returns>A new <see cref="FlatFileDataReader"/> instance
        /// that contains only one column per row.</returns>
		public virtual FileDataReader CreateDataReader()
		{
			return new FlatFileDataReader(this);
		}

        /// <summary>
        /// Create a <see cref="IDataWriter"/> class to write file to the path
        /// specified by its <see cref="FullPath"/> property.  Not implemented for the
        /// <see cref="ParameterData"/> class but its derived class, 
        /// <see cref="FixedWidthFileParameterData"/>.
        /// </summary>
        /// <param name="append">whether to append the data to the end of file.</param>
        /// <returns>a new <see cref="IDataWriter"/>.  However, in this case, it throws
        /// <see cref="System.NotImplementedException"/>.</returns>
		public virtual IDataWriter CreateDataWriter(bool append)
		{
			throw new System.NotImplementedException();
		}
    }

    #region - Proxy Class for Deserialization since ParameterData cannot be Deserialized as it inherits from NamedConfigurationElement (ConfigurationElement)-

    /// <summary>
    /// This class describes a file which contains text data.
    /// </summary>
    public class FileParameterDataProxy : ParameterDataProxy
    {
        private string fullPath;
        private FileParameterData fileParameterData = null;

        /// <summary>
        /// Initializes a new instance of 
        /// the </summary>
        public FileParameterDataProxy()
            : this(string.Empty, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of 
        /// the </summary>
        /// <param name="name">The display name.</param>
        public FileParameterDataProxy(string name)
            : this(name, string.Empty)
        {
        }


        /// <summary>FileParameterDataProxy</summary>
        /// <param name="name">The display name.</param>
        /// <param name="fullPath">The full path for the file.</param>
        public FileParameterDataProxy(string name, string fullPath)
            : base(name)
        {
            this.fullPath = fullPath;
        }

        /// <summary>
        /// The full path of the file.
        /// </summary>
        [XmlAttribute("fullPath")]
        public string FullPath
        {
            get
            {
                return fullPath;
            }
            set
            {
                fullPath = value;
            }
        }

        /// <summary>
        /// Overrides the  <seealso cref="ParameterData.ValueType"/> property.
        /// Always returns the string "FileParameterData".
        /// </summary>
        [XmlAttribute("typeName")]
        public override string ValueType
        {
            get
            {
                return "FileParameterData";
            }
            set
            {
            }
        }

        /// <summary>
        /// Overrides the <seealso cref="ParameterData.ValueType"/> property.  Always returns null.
        /// </summary>
        public override object Value
        {
            get
            {
                return null;
            }
            set
            {
            }
        }
    }
    #endregion
}
