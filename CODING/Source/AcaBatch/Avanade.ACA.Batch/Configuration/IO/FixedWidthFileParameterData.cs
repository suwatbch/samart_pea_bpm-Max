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
	/// This class describes a file containing data of which each line represents a row
	/// of columns and the width of each column is fixed.
	/// </summary>
	public class FixedWidthFileParameterData : FileParameterData
	{
		private int[]			columnWidths;
        private FixedWidthFileParameterDataProxy fixedWidthFileParameterDataProxy = null;

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="FixedWidthFileParameterData"/> object.
        /// </summary>
        /// <param name="name">the display name of the data.</param>
        /// <param name="filePath">the path to the file containing fixed-width data.</param>
        /// <param name="columnWidths">the width of each column.</param>
		public FixedWidthFileParameterData(string name, string filePath, int[] columnWidths)
            : base(name, filePath, new FixedWidthFileParameterDataProxy(name, filePath, columnWidths))
		{
            fixedWidthFileParameterDataProxy = (FixedWidthFileParameterDataProxy)base.ParameterDataProxy;
			this.columnWidths	= columnWidths;
		}

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="FixedWidthFileParameterData"/> object with empty data.
        /// </summary>
		public FixedWidthFileParameterData() : this(string.Empty, string.Empty, new int[]{})
		{
			
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="FixedWidthFileParameterData"/> object.
        /// </summary>
        /// <param name="name">The display name.</param>
		public FixedWidthFileParameterData(string name) : this(name, string.Empty, new int[]{})
		{
		}

		/// <summary>
		/// Gets or sets the column widths in number
		/// of characters.
		/// </summary>
		/// <value>an array of <see cref="int"/>.  Each element in the array representing
		/// the width of the respective column.</value>
		public int[] ColumnWidths
		{
			get
			{
				return columnWidths;
			}
			set
			{
                columnWidths = fixedWidthFileParameterDataProxy.ColumnWidths = value;
			}
		}

		/// <summary>
		/// Overrides the  <seealso cref="ParameterData.ValueType"/> property.  Always returns
		/// the string "FixedWidthFileParameterData".
		/// </summary>
		/// <value></value>
		[ConfigurationProperty( "typeName" )]
		public override string ValueType
		{
			get 
			{ 
				return "FixedWidthFileParameterData";
			}
			set
			{
			}
		}

		/// <summary>
		/// Overrides the <seealso cref="ParameterData.ValueType"/> property.
		/// Always returns null.</summary>
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
        /// <returns>A new <see cref="FixedWidthFileDataReader"/> instance.
        /// </returns>
		public override FileDataReader CreateDataReader()
		{
			return new FixedWidthFileDataReader(this);
		}

        /// <summary>
        /// Create a <see cref="IDataWriter"/> class to write file to the path
        /// specified by its FullPath property.
        /// </summary>
        /// <param name="append">whether to append the data to the end of file.</param>
        /// <returns>a new <see cref="FixedWidthFileDataWriter"/>.</returns>
		public override IDataWriter CreateDataWriter(bool append)
		{
			return new FixedWidthFileDataWriter(this, append);
		}
    }

    #region - Proxy Class for Deserialization since ParameterData cannot be Deserialized as it inherits from NamedConfigurationElement (ConfigurationElement)-

    /// <summary>
    /// This class describes a file containing data of which each line represents a row
    /// of columns and the width of each column is fixed.
    /// </summary>
    public class FixedWidthFileParameterDataProxy : FileParameterDataProxy
    {
        private int[] columnWidths;

        /// <summary>
        /// Initializes a new instance of the 
        /// </summary>
        /// <param name="name">the display name of the data.</param>
        /// <param name="filePath">the path to the file containing fixed-width data.</param>
        /// <param name="columnWidths">the width of each column.</param>
        public FixedWidthFileParameterDataProxy(string name, string filePath, int[] columnWidths)
            : base(name, filePath)
        {
            this.columnWidths = columnWidths;
        }

        /// <summary>
        /// Initializes a new instance of the 
        /// </summary>
        public FixedWidthFileParameterDataProxy()
            : this(string.Empty, string.Empty, new int[] { })
        {

        }

        /// <summary>
        /// Initializes a new instance of the </summary>
        /// <param name="name">The display name.</param>
        public FixedWidthFileParameterDataProxy(string name)
            : this(name, string.Empty, new int[] { })
        {
        }

        /// <summary>
        /// Gets or sets the column widths in number
        /// of characters.
        /// </summary>
        /// <value>an array of <see cref="int"/>.  Each element in the array representing
        /// the width of the respective column.</value>
        public int[] ColumnWidths
        {
            get
            {
                return columnWidths;
            }
            set
            {
                columnWidths = value;
            }
        }

        /// <summary>
        /// Overrides the  <seealso cref="ParameterData.ValueType"/> property.  Always returns
        /// the string "FixedWidthFileParameterData".
        /// </summary>
        /// <value></value>
        [XmlAttribute("typeName")]
        public override string ValueType
        {
            get
            {
                return "FixedWidthFileParameterData";
            }
            set
            {
            }
        }

        /// <summary>
        /// Overrides the <seealso cref="ParameterData.ValueType"/> property.
        /// Always returns null.</summary>
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
