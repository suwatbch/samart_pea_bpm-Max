// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System.Data;
using System.Xml.Serialization;
using Avanade.ACA.Batch.IO;
using System.Configuration;

namespace Avanade.ACA.Batch.Configuration.IO
{
	/// <summary>
	/// This class contains the data enabling the user to perform search for files
	/// with names that match a certain pattern.
	/// </summary>
	public class FileSetParameterData : FileParameterData
	{
        private const string searchPatternProperty = "searchPattern";
        private const string typeNameProperty = "typeName";
        private FileSetParameterDataProxy fileSetParameterDataProxy = null;
        /// <summary>
        /// Initializes a new instance of the <see cref="FileSetParameterData"/> class
        /// with empty data.
        /// </summary>
		public FileSetParameterData() : this(string.Empty, string.Empty, string.Empty)
		{
			
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSetParameterData"/> class.
        /// </summary>
        /// <param name="name">The display name.</param>
		public FileSetParameterData(string name) : this(name, string.Empty, string.Empty)
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSetParameterData"/> class.
        /// </summary>
        /// <param name="name">The display name.</param>
        /// <param name="directoryPath">The directory path for the file search.</param>
        /// <param name="searchPattern">The search pattern for the files in the directory.</param>
		public FileSetParameterData(string name, string directoryPath, string searchPattern) :
            base(name, directoryPath, new FileSetParameterDataProxy(name, directoryPath, searchPattern))
		{
            fileSetParameterDataProxy = (FileSetParameterDataProxy)base.ParameterDataProxy;
            this[searchPatternProperty] = searchPattern;
            this[typeNameProperty] = "FileSetParameterData";
		}

		/// <summary>
		/// Gets or sets the search pattern of the files in the directory path.
		/// </summary>
        [ConfigurationProperty(searchPatternProperty)]
		public string SearchPattern
		{
			get
			{
				return this[searchPatternProperty].ToString();
			}
			set
			{
				this[searchPatternProperty] = fileSetParameterDataProxy.SearchPattern = value;
			}
		}

		/// <summary>
		/// Overrides the  <seealso cref="ParameterData.ValueType"/> property.  Always returns
		/// the string "FileSetParameterData".
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
		/// Overrides the <seealso cref="ParameterData.ValueType"/> property.  Always return null.
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
        /// Override the same method in its base class <see cref="ParameterData"/>
        /// so that it throwns <see cref="System.NotImplementedException"/>.
        /// </summary>
        /// <remarks>The class <see cref="FileSetSearchHelper"/> class uses the
        /// <see cref="FileSetParameterData"/> class to search the directory.  It
        /// is not a <see cref="IDataReader"/> class, though.</remarks>
        /// <returns>throws <see cref="System.NotImplementedException"/>.</returns>
		public override FileDataReader CreateDataReader()
		{
			throw new System.NotImplementedException();
		}

        /// <summary>
        /// Create a <see cref="IDataWriter"/> class to write file to the path
        /// specified by its FullPath property.  Not implemented for
        /// this class.</summary>
        /// <param name="append">whether to append the data to the end of file.</param>
        /// <returns>a new <see cref="IDataWriter"/>.  However, in this case, it throws
        /// <see cref="System.NotImplementedException"/>.</returns>
		public override IDataWriter CreateDataWriter(bool append)
		{
			throw new System.NotImplementedException();
		}
    }

    #region - Proxy Class for Deserialization since ParameterData cannot be Deserialized as it inherits from NamedConfigurationElement (ConfigurationElement)-

    /// <summary>
    /// This class contains the data enabling the user to perform search for files
    /// with names that match a certain pattern.
    /// </summary>
    public class FileSetParameterDataProxy : FileParameterDataProxy
    {
        private string searchPattern;

        /// <summary>
        /// Initializes a new instance of the </summary>
        public FileSetParameterDataProxy()
            : this(string.Empty, string.Empty, string.Empty)
        {

        }

        /// <summary>
        /// Initializes a new instance of the </summary>
        /// <param name="name">The display name.</param>
        public FileSetParameterDataProxy(string name)
            : this(name, string.Empty, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the </summary>
        /// <param name="name">The display name.</param>
        /// <param name="directoryPath">The directory path for the file search.</param>
        /// <param name="searchPattern">The search pattern for the files in the directory.</param>
        public FileSetParameterDataProxy(string name, string directoryPath, string searchPattern)
            : base(name, directoryPath)
        {
            this.searchPattern = searchPattern;
        }

        /// <summary>
        /// Gets or sets the search pattern of the files in the directory path.
        /// </summary>
        [XmlAttribute("searchPattern")]
        public string SearchPattern
        {
            get
            {
                return searchPattern;
            }
            set
            {
                searchPattern = value;
            }
        }

        /// <summary>
        /// Overrides the  <seealso cref="ParameterData.ValueType"/> property.  Always returns
        /// the string "FileSetParameterData".
        /// </summary>
        [XmlAttribute("typeName")]
        public override string ValueType
        {
            get
            {
                return "FileSetParameterData";
            }
            set
            {
            }
        }

        /// <summary>
        /// Overrides the <seealso cref="ParameterData.ValueType"/> property.  Always return null.
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
