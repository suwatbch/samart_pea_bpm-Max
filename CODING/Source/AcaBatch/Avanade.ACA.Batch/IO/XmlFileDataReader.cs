// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System.Xml;
using System.ComponentModel;
using System.Xml.Serialization;
using System.IO;
using Avanade.ACA.Batch.Configuration.IO;

namespace Avanade.ACA.Batch.IO
{
	/// <summary>
	/// Provides a means of reading 
	/// a forward-only result set of
	/// XML nodes obtained by executing
	/// an XPath query against an XML document.
	/// This class is XML serializable.
	/// </summary>
	/// <example>
	/// <code>
	/// FileDataReader reader = new XmlFileReader();
	/// reader.FilePath = "c:\\MyFile.xml";
	/// reader.RecordsXPath = "//customers/customer";
	/// reader.ColumnXPaths = new string{"@companyName", "@customerId"};
	/// reader.Open();
	/// try
	/// {
	///		while (reader.Read())
	///		{
	///			// write out the value of the first column
	///			Console.WriteLine(reader.GetString(0));
	///			// write out the value of the second column
	///			Console.WriteLine(reader.GetInt32(1);
	///		}
	///	}
	///	finally
	///	{
	///		if (reader != null)
	///		{
	///			reader.Close();
	///		}
	///	}
	/// </code>
	/// </example>	
	public class XmlFileDataReader : FileDataReader
	{
		private XmlDocument	_document;
		private XmlNodeList _records;

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="XmlFileDataReader"/> class and
        /// sets its state with the specified
        /// file path and XPath expressions described in the data instance.
        /// </summary>
        /// <param name="data">Avanade.ACA.Batch.Configuration.IO.XmlFileParameterData</param>
		public XmlFileDataReader(XmlFileParameterData data) : base(data)
		{
			
		}

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="XmlFileDataReader"/> class and
        /// sets its state with the specified
        /// file path and XPath expressions.
        /// </summary>
        /// <param name="filePath">The path of the file to be read.</param>
        /// <param name="recordsXPath">An XPath expression that will 
        /// select the result set to be read.</param>
        /// <param name="columnXPaths">An array of
        /// XPath expressions</param>
		public XmlFileDataReader(string filePath, string recordsXPath, string[] columnXPaths)
			 : this( new XmlFileParameterData(string.Empty, filePath, recordsXPath, columnXPaths) )
		{
		}

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="XmlFileDataReader"/> class.
        /// </summary>
		public XmlFileDataReader() : this(string.Empty, string.Empty, new string[]{})
		{
		}

		private XmlFileParameterData XmlFileParmData
		{
			get
			{
				return (XmlFileParameterData)base.Data;
			}
		}

		/// <summary>
		/// Gets or sets the XPath expressions
		/// that determine the column values.
		/// </summary>
		[Description("An array of XPath expressions that select the column nodes")]
		public string[] ColumnXPaths
		{
			get
			{
				return XmlFileParmData.ColumnXPaths;
			}
			set
			{
				XmlFileParmData.ColumnXPaths = value;
			}
		}

		/// <summary>
		/// Gets the current XML node.
		/// </summary>
		[Browsable(false)]
		public XmlNode CurrentXmlNode
		{
			get
			{
				return (XmlNode)CurrentRecord;
			}
		}

		/// <summary>
		/// Gets or sets the XPath expression
		/// that selects the result set to be read.
		/// </summary>
		[Description("An XPath expression that will select " +
			 "the result set to be read.")]
		public string RecordsXPath
		{
			get
			{
				return XmlFileParmData.RecordsXPath;
			}
			set
			{
				XmlFileParmData.RecordsXPath = value;
			}
		}

		/// <summary>
		/// Gets the <see cref="XmlDocument"/> 
		/// loaded from the <see cref="FileDataReader.FilePath"/>.
		/// </summary>
		[XmlIgnore]
		[Browsable(false)]
		public XmlDocument Document
		{
			get
			{
				return _document;
			}
		}

		/// <summary>
		/// Gets the <see cref="XmlNodeList"/> result 
		/// set returned by the <see cref="RecordsXPath"/>
		/// query.
		/// </summary>
		[XmlIgnore]
		[Browsable(false)]
		public XmlNodeList Records
		{
			get
			{
				return _records;
			}
		}

        /// <summary>
        /// Advances the <see cref="XmlFileDataReader"/> to 
        /// the next record.
        /// </summary>
        /// <returns>bool</returns>
		protected override bool ReadImpl()
		{
			if (CurrentRecordIndex >= Records.Count)
			{
				SetCurrentRecord(null);
				return false;
			}
			else
			{
				XmlNode currentNode = Records[CurrentRecordIndex];
				SetCurrentRecord(currentNode);
				return true;
			}
		}

        /// <summary>
        /// Closes the <see cref="XmlFileDataReader"/> object.
        /// </summary>
        /// <returns>void</returns>
		public override void Close()
		{
			base.Close();
			_document		= null;
			_records		= null;
		}

        /// <summary>
        /// Opens the XML file and prepares
        /// it to be read.
        /// </summary>
        /// <param name="reader">System.IO.StreamReader</param>
        /// <returns>void</returns>
		public override void Open(StreamReader reader)
		{
			base.Open(reader);
			_document = new XmlDocument();
			_document.Load(reader);
			_records = _document.SelectNodes(RecordsXPath);
			// close the stream reader because we've loaded
			// the stream into memory via the XmlDocument
			reader.Close();
		}

        /// <summary>
        /// Gets the string value of the specified field.
        /// Because the field values are <see cref="XmlNode"/>
        /// objects, the value of their <see cref="XmlNode.InnerText"/>
        /// property is returned.
        /// </summary>
        /// <param name="columnIndex">The index of the field to find.</param>
        /// <returns>string</returns>
		public override string GetString(int columnIndex)
		{
			object value = GetValue(columnIndex);
			if (value is XmlNode)
			{
				XmlNode node = (XmlNode)value;
				return node.InnerText;
			}
			else
			{
				return value.ToString();
			}
		}

        /// <summary>
        /// Gets the <see cref="XmlNode"/> or
        /// <see cref="XmlNodeList"/> selected
        /// by the XPath expression for the specified
        /// column index.
        /// </summary>
        /// <param name="columnIndex">The index of the field to find.</param>
        /// <returns>object</returns>
		public override object GetValue(int columnIndex)
		{
			string xpath = ColumnXPaths[columnIndex];
			XmlNodeList nodes = CurrentXmlNode.SelectNodes(xpath);
			if (nodes.Count == 0)
			{
				return null;
			}
			else if (nodes.Count == 1)
			{
				return nodes[0];
			}
			else
			{
				return nodes;
			}
		}



	}
}
