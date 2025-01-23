// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using Avanade.ACA.Batch.Configuration.IO;

namespace Avanade.ACA.Batch.IO
{
	/// <summary>
	/// Provides a means of reading a stream of rows from a file
	/// whose columns are delimited by a fixed character width.
	/// </summary>
	/// <example>
	/// <code>
	/// FileDataReader reader = new FixedWidthFileReader();
	/// reader.FilePath = "c:\\MyFile.txt";
	/// reader.ColumnWidths = new int[]{10, 15};
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
	public class FixedWidthFileDataReader : FlatFileDataReader
	{
        /// <summary>
        /// Constructor that initializes the reader with the cata object describing
        /// file path and an array of the widths of columns.
        /// </summary>
        /// <param name="data">Avanade.ACA.Batch.Configuration.IO.FixedWidthFileParameterData</param>
		public FixedWidthFileDataReader(FixedWidthFileParameterData data) : base(data)
		{
			SetFieldCount(FixedWidthData.ColumnWidths.Length);
			Columns			= new string[FieldCount];
		}

        /// <summary>
        /// Constructor that initializes the reader with the file path and an array of
        /// the widths of columns.
        /// </summary>
        /// <param name="filePath">the path to the file containing fixed-width data.</param>
        /// <param name="columnWidths">the width of each column.</param>
		public FixedWidthFileDataReader(string filePath, int[] columnWidths) : 
			this (new FixedWidthFileParameterData(string.Empty, filePath, columnWidths))
		{
		}

        /// <summary>
        /// The default constructor.
        /// </summary>
		public FixedWidthFileDataReader() : this(string.Empty, new int[]{})
		{
		}

		/// <summary>
		/// Gets the data describing the file path and the width of each column.
		/// </summary>
		private FixedWidthFileParameterData	FixedWidthData
		{
			get
			{
				return (FixedWidthFileParameterData)base.Data;
			}
		}

		/// <summary>
		/// Gets or sets the column widths in number
		/// of characters.
		/// </summary>
		public int[] ColumnWidths
		{
			get
			{
				return FixedWidthData.ColumnWidths;
			}
			set
			{
				FixedWidthData.ColumnWidths = value;
				SetFieldCount(value.Length);
			}
		}

        /// <summary>
        /// Read one line from the file and retain the data.
        /// </summary>
        /// <returns>true if the end of file is not reached; otherewise false.</returns>
		protected override bool ReadImpl() 
		{ 
			if (FileStreamReader != null)
			{
				bool eof = (FileStreamReader.Peek() == -1);
			
				if (!eof)
				{
					string record = FileStreamReader.ReadLine();
					SetCurrentRecord(record);
					Columns = null;
				}
				return !eof;
			}

			return false;
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
        /// <returns>string array for the strings in all the columns.</returns>
		public override string[] GetColumns()
		{
			if (Columns == null)
			{
				Columns = new String[FieldCount];

				int startIndex = 0;

				for (int i = 0; i < FieldCount; i++)
				{
					int width = FixedWidthData.ColumnWidths[i];
						
					string columnValue = 
						CurrentRecordText.Substring(startIndex, width);
					Columns[i] = columnValue.TrimEnd(); // trim off the spaces at the end.
						
					startIndex += width;
				}
			}
			return Columns;
		}
	}
}
