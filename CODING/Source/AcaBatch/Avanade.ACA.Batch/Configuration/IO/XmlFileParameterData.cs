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
	/// This class describes a file containing text in XML.
	/// </summary>
	public class XmlFileParameterData : FileParameterData
	{
		private string[]	columnXPaths;
		private string		recordsXPath;
        private XmlFileParameterDataProxy xmlFileParameterDataProxy = null;
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="XmlFileParameterData"/> class.
        /// </summary>
        /// <param name="name">The display name of the data.</param>
        /// <param name="filePath">The path of the file to be read.</param>
        /// <param name="recordsXPath">An XPath expression that will 
        /// select the result set to be read.</param>
        /// <param name="columnXPaths">An array of
        /// XPath expressions</param>
		public XmlFileParameterData(string name, string filePath, string recordsXPath, string[] columnXPaths)
			: base(name, filePath, new XmlFileParameterDataProxy(name, filePath, recordsXPath, columnXPaths))
		{
            xmlFileParameterDataProxy = (XmlFileParameterDataProxy)base.ParameterDataProxy;
			this.recordsXPath = recordsXPath;
			this.columnXPaths = columnXPaths;
		}


        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="XmlFileParameterData"/> class.
        /// </summary>
        /// <param name="name">The display name of the data.</param>
		public XmlFileParameterData(string name) : this(name,  string.Empty, string.Empty, new string[0])
		{
		}


        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="XmlFileParameterData"/> class with empty data.
        /// </summary>
		public XmlFileParameterData() : this(string.Empty)
		{
			
		}

		/// <summary>
		/// Gets or sets the XPath expressions
		/// that determine the column values.
		/// </summary>
		/// <value>An <see cref="string"/> array, each item representing the
		/// XPath referencing the data for the respective column.</value>
		public string[] ColumnXPaths
		{
			get
			{
				return columnXPaths;
			}
			set
			{
                columnXPaths = xmlFileParameterDataProxy.ColumnXPaths = value;
			}
		}

		/// <summary>
		/// Gets or sets the XPath expression
		/// that selects the result set to be read.
		/// </summary>
		public string RecordsXPath
		{
			get
			{
				return recordsXPath;
			}
			set
			{
                recordsXPath = xmlFileParameterDataProxy.RecordsXPath = value;
			}
		}

		/// <summary>
		/// Overrides the  <seealso cref="ParameterData.ValueType"/> property.  Always returns
		/// the string "XmlFileParameterData".
		/// </summary>
		[ConfigurationProperty( "typeName" )]
		public override string ValueType
		{
			get 
			{ 
				return "XmlFileParameterData";
			}
			set
			{
			}
		}

		/// <summary>
		/// Overrides the <seealso cref="ParameterData.ValueType"/> property. Always returns
		/// null. 
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
        /// to read the file specified by its FullPath property.
        /// </summary>
        /// <returns>A new <see cref="XmlFileDataReader"/> instance
        /// that contains only one column per row.</returns>
		public override FileDataReader CreateDataReader()
		{
			return new XmlFileDataReader(this);
		}

        /// <summary>
        /// Create a <see cref="IDataWriter"/> class to write file to the path
        /// specified by its FullPath property.
        /// </summary>
        /// <param name="append">whether to append the data to the end of file.</param>
        /// <returns>a new <see cref="IDataWriter"/>.  However, in this case, it throws
        /// <see cref="System.NotImplementedException"/>.  The users can write the Xml file
        /// programatically with various .NET APIs.</returns>
		public override IDataWriter CreateDataWriter(bool append)
		{
			throw new System.NotImplementedException();
		}
    }

    #region - Proxy Class for Deserialization since ParameterData cannot be Deserialized as it inherits from NamedConfigurationElement (ConfigurationElement)-

    /// <summary>
    /// This class describes a file containing text in XML.
    /// </summary>
    public class XmlFileParameterDataProxy : FileParameterDataProxy
    {
        private string[] columnXPaths;
        private string recordsXPath;

        /// <summary>
        /// Initializes a new instance of the 
        /// </summary>
        /// <param name="name">The display name of the data.</param>
        /// <param name="filePath">The path of the file to be read.</param>
        /// <param name="recordsXPath">An XPath expression that will 
        /// select the result set to be read.</param>
        /// <param name="columnXPaths">An array of
        /// XPath expressions</param>
        public XmlFileParameterDataProxy(string name, string filePath, string recordsXPath, string[] columnXPaths)
            : base(name, filePath)
        {
            this.recordsXPath = recordsXPath;
            this.columnXPaths = columnXPaths;
        }


        /// <summary>
        /// Initializes a new instance of the 
        /// </summary>
        /// <param name="name">The display name of the data.</param>
        public XmlFileParameterDataProxy(string name)
            : this(name, string.Empty, string.Empty, new string[0])
        {
        }


        /// <summary>
        /// Initializes a new instance of the
        /// </summary>
        public XmlFileParameterDataProxy()
            : this(string.Empty)
        {

        }

        /// <summary>
        /// Gets or sets the XPath expressions
        /// that determine the column values.
        /// </summary>
        /// <value>An <see cref="string"/> array, each item representing the
        /// XPath referencing the data for the respective column.</value>
        public string[] ColumnXPaths
        {
            get
            {
                return columnXPaths;
            }
            set
            {
                columnXPaths = value;
            }
        }

        /// <summary>
        /// Gets or sets the XPath expression
        /// that selects the result set to be read.
        /// </summary>
        public string RecordsXPath
        {
            get
            {
                return recordsXPath;
            }
            set
            {
                recordsXPath = value;
            }
        }

        /// <summary>
        /// Overrides the  <seealso cref="ParameterData.ValueType"/> property.  Always returns
        /// the string "XmlFileParameterData".
        /// </summary>
        [XmlAttribute("typeName")]
        public override string ValueType
        {
            get
            {
                return "XmlFileParameterData";
            }
            set
            {
            }
        }

        /// <summary>
        /// Overrides the <seealso cref="ParameterData.ValueType"/> property. Always returns
        /// null. 
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
