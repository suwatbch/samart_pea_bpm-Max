// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using Avanade.ACA.Batch.Configuration.IO;
using Avanade.ACA.Batch.IO;

namespace Avanade.ACA.Batch.IO
{
	/// <summary>
	/// Provides a means of reading a stream of rows from a file
	/// whose columns are delimited by a common character.
	/// This is class supports XML serialization.
	/// </summary>
	/// <example>
	/// <code>
	/// FileDataReader reader = new CharacterSeparatedFileReader();
	/// reader.FilePath = "c:\\MyFile.txt";
	/// reader.Separator = ','; // comma delimited
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
	public class CharacterSeparatedFileDataReader : FlatFileDataReader
	{
        /// <summary>
        /// Initializes a new instance of the reader with a data object describing what
        /// file to be read and the column seperator.
        /// </summary>
        /// <param name="data">Avanade.ACA.Batch.Configuration.IO.CharacterSeparatedFileParameterData</param>
		public CharacterSeparatedFileDataReader(CharacterSeparatedFileParameterData data) : base(data)
		{		
		}

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="CharacterSeparatedFileDataReader"/></summary>
        /// <param name="filePath">The path of the file to read.</param>
        /// <param name="separator">The column delimiter character</param>
		public CharacterSeparatedFileDataReader(string filePath, char separator) : 
			base(new CharacterSeparatedFileParameterData(string.Empty, filePath, separator))
		{
		}

        /// <summary>CharacterSeparatedFileDataReader</summary>
		public CharacterSeparatedFileDataReader() : this(string.Empty, (char)0)
		{
		}

		private CharacterSeparatedFileParameterData CharSeparatedData
		{
			get
			{
				return (CharacterSeparatedFileParameterData)base.Data;
			}
		}


		/// <summary>
		/// Gets or sets the character that separates
		/// columns in the records of this file.
		/// </summary>
		public char Separator
		{
			get
			{
				return CharSeparatedData.Separator;
			}
			set
			{
				CharSeparatedData.Separator = value;
			}
		}

        /// <summary>GetColumns</summary>
        /// <returns>string[]</returns>
		public override string[] GetColumns()
		{
			if (Columns == null)
			{
				Columns = CurrentRecordText.Split(Separator);
			}
			return Columns;
		}
	}
}
