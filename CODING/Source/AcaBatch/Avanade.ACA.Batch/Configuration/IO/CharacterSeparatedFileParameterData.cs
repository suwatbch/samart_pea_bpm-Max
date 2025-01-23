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
	/// of columns separated by a character.
	/// </summary>
	public class CharacterSeparatedFileParameterData : FileParameterData
	{
		private char separator;
        private CharacterSeparatedFileParameterDataProxy characterSeparatedFileParameterDataProxy = null;
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="CharacterSeparatedFileParameterData"/> object.
        /// </summary>
        /// <param name="name">The display name of the data.</param>
        /// <param name="filePath">The path of the file to read.</param>
        /// <param name="separator">The column delimiter character</param>
		public CharacterSeparatedFileParameterData(string name, string filePath, char separator) :
			base(name, filePath, new CharacterSeparatedFileParameterDataProxy(name, filePath, separator))
		{
            this.characterSeparatedFileParameterDataProxy = (CharacterSeparatedFileParameterDataProxy)base.ParameterDataProxy;
			this.separator = separator;			
		}

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="CharacterSeparatedFileParameterData"/> object.
        /// </summary>
        /// <param name="name">The display name of the data.</param>
		public CharacterSeparatedFileParameterData(string name) : this(name, string.Empty, (char)0)
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterSeparatedFileParameterData"/>
        /// object with an empty name and file path.
        /// </summary>
		public CharacterSeparatedFileParameterData() : this(string.Empty, string.Empty, (char)0)
		{
		}

		/// <summary>
		/// Gets or sets the character that separates
		/// columns in the records of this file.
		/// </summary>
        [ConfigurationProperty("separator")]
		public char Separator
		{
			get
			{
				return separator;
			}
			set
			{
                separator = characterSeparatedFileParameterDataProxy.Separator = value;
			}
		}

		/// <summary>
		/// Overrides the  <seealso cref="ParameterData.ValueType"/> property.
		/// Always returns the string "CharacterSeparatedFileParameter".
		/// </summary>
        [ConfigurationProperty("typeName")]
		public override string ValueType
		{
			get 
			{ 
				return "CharacterSeparatedFileParameterData";
			}
			set
			{
			}
		}

		/// <summary>
		/// Overrides the <seealso cref="ParameterData.ValueType"/> property.  Always
		/// returns null.
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
        /// <returns>A new <see cref="CharacterSeparatedFileDataReader"/> instance.</returns>
		public override FileDataReader CreateDataReader()
		{
			return new CharacterSeparatedFileDataReader(this);
		}

        /// <summary>
        /// Create a <see cref="IDataWriter"/> class to write file to the path
        /// specified by its FullPath property.  Not implemented for this class.
        /// </summary>
        /// <param name="append">whether to append the data to the end of file.</param>
        /// <returns>a new <see cref="IDataWriter"/>.  However, with this case, it throws
        /// <see cref="System.NotImplementedException"/>.</returns>
		public override IDataWriter CreateDataWriter(bool append)
		{
			throw new System.NotImplementedException();
		}
    }

    #region - Proxy Class for Deserialization since ParameterData cannot be Deserialized as it inherits from NamedConfigurationElement (ConfigurationElement)-
    /// <summary>
    /// This class describes a file containing data of which each line represents a row
    /// of columns separated by a character.
    /// </summary>
    public class CharacterSeparatedFileParameterDataProxy : FileParameterDataProxy
    {
        private char separator;

        /// <summary>
        /// Initializes a new instance of the 
        /// </summary>
        /// <param name="name">The display name of the data.</param>
        /// <param name="filePath">The path of the file to read.</param>
        /// <param name="separator">The column delimiter character</param>
        public CharacterSeparatedFileParameterDataProxy(string name, string filePath, char separator)
            : base(name, filePath)
        {
            this.separator = separator;
        }

        /// <summary>
        /// Initializes a new instance of the 
        /// </summary>
        /// <param name="name">The display name of the data.</param>
        public CharacterSeparatedFileParameterDataProxy(string name)
            : this(name, string.Empty, (char)0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the </summary>
        public CharacterSeparatedFileParameterDataProxy()
            : this(string.Empty, string.Empty, (char)0)
        {
        }

        /// <summary>
        /// Gets or sets the character that separates
        /// columns in the records of this file.
        /// </summary>
        [XmlAttribute("separator")]
        public char Separator
        {
            get
            {
                return separator;
            }
            set
            {
                separator = value;
            }
        }

        /// <summary>
        /// Overrides the  <seealso cref="ParameterData.ValueType"/> property.
        /// Always returns the string "CharacterSeparatedFileParameter".
        /// </summary>
        [XmlAttribute("typeName")]
        public override string ValueType
        {
            get
            {
                return "CharacterSeparatedFileParameterData";
            }
            set
            {
            }
        }

        /// <summary>
        /// Overrides the <seealso cref="ParameterData.ValueType"/> property.  Always
        /// returns null.
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
