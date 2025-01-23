// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.IO;
using System.Text;
using Avanade.ACA.Batch.Configuration.IO;

namespace Avanade.ACA.Batch.IO
{
	/// <summary>
	/// FileDataWrites writes one line composed of all the columns in a data row.  The width of each column
	/// is fixed, and, when written to the file, is padded with spaces if actual column data is shorter
	/// than the width of the column.
	/// </summary>
	public class FixedWidthFileDataWriter : IDataWriter
	{
		private FixedWidthFileParameterData	_data;
		private StreamWriter				_streamWriter;
		private string[]					_columns;


        /// <summary>
        /// Initializes a <see cref="FixedWidthFileDataWriter"/> object instance with
        /// a <see cref="FixedWidthFileParameterData"/> and whether to append
        /// the data at the end of file when writting the data to file.
        /// </summary>
        /// <param name="data">The data instance describing the file path and the 
        /// width of each column.</param>
        /// <param name="append">Determines whether data is to be appended to the file. If the 
        /// file exists and append is false, the file is overwritten. If the file exists 
        /// and append is true, the data is appended to the file. Otherwise, a new file is 
        /// created.</param>
		public FixedWidthFileDataWriter(FixedWidthFileParameterData data, bool append) : 
			this(data.FullPath,  data.ColumnWidths, append)
		{
			
		}

        /// <summary>
        /// Initializes a new instance of the FixedWidthFileDataWriter class for the 
        /// specified file on the specified path, using the default encoding. 
        /// If the file exists, it can be either overwritten or appended to. If the file 
        /// does not exist, this constructor creates a new file.
        /// </summary>
        /// <param name="filePath">The full path of the file to be written.</param>
        /// <param name="columnWidths">int[]</param>
        /// <param name="append">Determines whether data is to be appended to the file. If the 
        /// file exists and append is false, the file is overwritten. If the file exists 
        /// and append is true, the data is appended to the file. Otherwise, a new file is 
        /// created.</param>
		public FixedWidthFileDataWriter(string filePath, int[] columnWidths, bool append)
		{
			_data = new FixedWidthFileParameterData(string.Empty, filePath, columnWidths);
			_streamWriter = new StreamWriter(filePath, append);
			_columns = new string[columnWidths.Length];
			for (int i = 0; i < columnWidths.Length; i++)
			{
				_columns[i] = string.Empty;
			}
		}

		/// <summary>
		/// Gets or sets the data in the columns to be written to file.  
		/// </summary>
		/// <value>An array of strings, each one represents the associated column in
		/// the current row.</value>
		public string[] Columns
		{
			get { return _columns; }
			set { _columns = value; }
		}

        /// <summary>
        /// Write one line in file with string composed of all the columns in one row.
        /// </summary>
        /// <returns>void</returns>
		public virtual void Write()
		{
			StringBuilder builder = new StringBuilder();
			
			for (int i = 0; i < _columns.Length; i++)
			{
				string column	= _columns[i];
				int width	= _data.ColumnWidths[i];
				string paddedValue = column.PadRight(width);
				builder.Append(paddedValue, 0, width); // making sure not exceeding the width.
			}
			string record = builder.ToString();
			_streamWriter.WriteLine(record);
		}

        /// <summary>
        /// Closes the data writer.
        /// </summary>
        /// <returns>void</returns>
		public virtual void Close()
		{
			_streamWriter.Close();
		}

        /// <summary>
        /// Sets a <see cref="string"/> value to a specific column.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <param name="value">the <see cref="string"/> value to be set.</param>
        /// <returns>void</returns>
		public virtual void AddString(int columnIndex, string value)
		{
			AddValue(columnIndex, value);
		}

        /// <summary>
        /// Sets an <see cref="int"/> value to a column.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <param name="value">the <see cref="int"/> value to be set.</param>
        /// <returns>void</returns>
		public virtual void AddInt32(int columnIndex, int value)
		{
			AddValue(columnIndex, value);
		}

        /// <summary>
        /// Sets a <see cref="short"/> value to a column.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <param name="value">the <see cref="short"/> value to be set.</param>
        /// <returns>void</returns>
		public virtual void AddInt16(int columnIndex, short value)
		{
			AddValue(columnIndex, value);
		}

        /// <summary>
        /// Setting a <see cref="long"/> value to a column.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <param name="value">the <see cref="long"/> value to be set.</param>
        /// <returns>void</returns>
		public virtual void AddInt64(int columnIndex, long value)
		{
			AddValue(columnIndex, value);
		}

        /// <summary>
        /// Setting a <see cref="bool"/> value to a column.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <param name="value">the <see cref="bool"/> value to be set.</param>
        /// <returns>void</returns>
		public virtual void AddBoolean(int columnIndex, bool value)
		{
			AddValue(columnIndex, value);
		}

        /// <summary>
        /// Setting a <see cref="byte"/> value to a column.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <param name="value">the <see cref="byte"/> value to be set.</param>
        /// <returns>void</returns>
		public virtual void AddByte(int columnIndex, byte value)
		{
			AddValue(columnIndex, value);
		}

        /// <summary>
        /// Setting a <see cref="byte"/> array value to a column.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <param name="value">the <see cref="byte"/> array value to be set.</param>
        /// <returns>void</returns>
		public virtual void AddBytes(int columnIndex, byte[] value)
		{
			string encodedBytes = Convert.ToBase64String(value);
			AddValue(columnIndex, encodedBytes);
		}

        /// <summary>
        /// Setting a <see cref="char"/> value to a column.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <param name="value">the <see cref="char"/> value to be set.</param>
        /// <returns>void</returns>
		public virtual void AddChar(int columnIndex, char value)
		{
			AddValue(columnIndex, value);
		}

        /// <summary>
        /// Setting a <see cref="char"/> array value to a column.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <param name="value">the <see cref="char"/> array value to be set.</param>
        /// <returns>void</returns>
		public virtual void AddChars(int columnIndex, char[] value)
		{
			string s = new string(value);
			AddValue(columnIndex, s);
		}

        /// <summary>
        /// Setting a <see cref="DateTime"/> value to a column.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <param name="value">the <see cref="DateTime"/> value to be set.</param>
        /// <returns>void</returns>
		public virtual void AddDateTime(int columnIndex, DateTime value)
		{
			AddValue(columnIndex, value);
		}

        /// <summary>
        /// Setting a <see cref="decimal"/> value to a column.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <param name="value">the <see cref="decimal"/> value to be set.</param>
        /// <returns>void</returns>
		public virtual void AddDecimal(int columnIndex, decimal value)
		{
			AddValue(columnIndex, value);
		}

        /// <summary>
        /// Setting a <see cref="double"/> value to a column.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <param name="value">the <see cref="double"/> value to be set.</param>
        /// <returns>void</returns>
		public virtual void AddDouble(int columnIndex, double value)
		{
			AddValue(columnIndex, value);
		}

        /// <summary>
        /// Setting a <see cref="float"/> value to a column.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <param name="value">the <see cref="float"/> value to be set.</param>
        /// <returns>void</returns>
		public virtual void AddFloat(int columnIndex, float value)
		{
			AddValue(columnIndex, value);
		}

        /// <summary>
        /// Setting a <see cref="Guid"/> value to a column.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <param name="value">the <see cref="Guid"/> value to be set.</param>
        /// <returns>void</returns>
		public virtual void AddGuid(int columnIndex, Guid value)
		{
			AddValue(columnIndex, value);
		}

        /// <summary>
        /// Setting a <see cref="object"/> value to a column.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <param name="value">the <see cref="object"/> value to be set.</param>
        /// <returns>void</returns>
		public virtual void AddValue(int columnIndex, object value)
		{
			Columns[columnIndex] = value.ToString();
		}

	}
}
