// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using Avanade.ACA.Batch.Configuration.IO;

namespace Avanade.ACA.Batch.IO
{
	/// <summary>
	/// Represents a generic reader for row/column-based text files.
	/// </summary>
	public class FlatFileDataReader : FileDataReader
	{
		private string[]		_columns;

        /// <summary>
        /// Initializes a new instance of the FlatFileDataReader class with the
        /// neccessary data.
        /// </summary>
        /// <param name="data">The <see cref="FileParameterData"/> describing where the
        /// file is.</param>
		public FlatFileDataReader(FileParameterData data) : base(data)
		{
			
		}

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="FlatFileDataReader"/> class
        /// based on the specified file path.
        /// </summary>
        /// <param name="filePath">The path of the file to be read.</param>
		public FlatFileDataReader(string filePath) : base(filePath)
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="FlatFileDataReader"/> class.
        /// </summary>
		public FlatFileDataReader() : this(string.Empty)
		{
		}

        /// <summary>
        /// Read one line from the file using its <see cref="FileDataReader.FileStreamReader"/>.
        /// </summary>
        /// <returns>true if not eof; otherwise false.</returns>
		protected override bool ReadImpl() 
		{
			if (FileStreamReader != null)
			{
				bool eof = (FileStreamReader.Peek() == -1);
			
				if (!eof)
				{
					string currentRecordText = FileStreamReader.ReadLine();	
					SetCurrentRecord(currentRecordText);
					_columns = null;
					SetFieldCount(GetColumns().Length);
				}
				return !eof;
			}

			return false;
		}

		/// <summary>
		/// Current record.
		/// </summary>
		public string CurrentRecordText
		{
			get
			{
				return (string)CurrentRecord;
			}
		}

        /// <summary>
        /// Get string by index.
        /// </summary>
        /// <param name="columnIndex">the column index.</param>
        /// <returns>the string value of the column.</returns>
		public override string GetString(int columnIndex) 
		{
			return GetColumns()[columnIndex];
		}

        /// <summary>
        /// Get string value by index.
        /// </summary>
        /// <param name="columnIndex">the column index.</param>
        /// <returns>the string value of the column.</returns>
		public override object GetValue(int columnIndex)
		{
			return GetString(columnIndex);
		}

        /// <summary>
        /// Get the string array for all the columns.
        /// </summary>
        /// <returns>string[]</returns>
		public virtual string[] GetColumns()
		{
			return new string[] { (string)base.CurrentRecord };
		}

		/// <summary>
		/// The string array for all the columns.
		/// </summary>
		protected string[] Columns
		{
			get
			{
				return _columns;
			}
			set
			{
				_columns = value;
			}
		}
	}
}
