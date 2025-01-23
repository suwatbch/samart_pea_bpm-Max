// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Data;
using System.IO;
using Avanade.ACA.Batch.Configuration.IO;

namespace Avanade.ACA.Batch.IO
{

	/// <example>
	/// <code>
	/// FileDataReader reader = new CharacterSeparatedFileReader("d:\\data.txt", ',');
	/// try
	/// {
	///		reader.Open();
	///		
	///		while (reader.Read())
	///		{
	///			int id				= reader.GetInt32(0);
	///			string firstName	= reader.GetString(1);
	///			string lastName		= reader.GetString(2);
	///			
	///			string record = String.Format("{0}\t{1}\t(2}", id, firstName, lastName);
	///			Console.WriteLine(record);
	///		}
	///	finally
	///	{
	///		if (reader != null)
	///		{
	///			reader.Close()
	///		}
	///	}
	/// </code>
	/// </example>
	/// <summary>
	/// Represents a generic reader for row/column-based
	/// interface files.
	/// </summary>
	public abstract class FileDataReader : IDataReader, IDataRecord
	{
		private FileParameterData	_data;
		private int					_depth;
		private int					_fieldCount;
		private int					_recordsAffected;
		private bool				_isClosed;
		private int					_currentRecordIndex;
		private StreamReader		_streamReader;
		private object				_currentRecord;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileDataReader"/> class.
        /// </summary>
		public FileDataReader() : this(string.Empty)
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="FileDataReader"/> class
        /// using the specified file path.
        /// </summary>
        /// <param name="filePath">The path of the file to read</param>
		public FileDataReader(string filePath) : 
			this(new FileParameterData(string.Empty, filePath))
		{
		}

        /// <summary>
        /// Initializes the reader with the data describing the file path.
        /// </summary>
        /// <param name="fileParameterData">Avanade.ACA.Batch.Configuration.IO.FileParameterData</param>
		public FileDataReader(FileParameterData fileParameterData)
		{
			_data = fileParameterData;		
			_currentRecordIndex = -1;
		}

		/// <summary>
		/// Gets the data object for the reader.
		/// </summary>
		protected FileParameterData Data
		{
			get
			{
				return this._data;
			}
		}

		/// <summary>
		/// Gets or sets the path of the input file to read.
		/// </summary>
		public string FilePath
		{
			get
			{
				return _data.FullPath;
			}
			set
			{
				_data.FullPath = value;
			}
		}

		/// <summary>
		/// Gets or sets the index of the current
		/// record being read. This index is incremented
		/// automatically whenever the <see cref="Read"/>
		/// method is invoked.
		/// </summary>
		public int CurrentRecordIndex
		{
			get
			{
				return _currentRecordIndex;
			}
			set
			{
				_currentRecordIndex = value;
			}
		}

		/// <summary>
		/// Get the current record object.
		/// </summary>
		public object CurrentRecord
		{
			get
			{
				return _currentRecord;
			}
		}

        /// <summary>
        /// Set an object to be the current record.
        /// </summary>
        /// <param name="record">the object to be the current record.</param>
        /// <returns>void</returns>
		protected void SetCurrentRecord(object record)
		{
			_currentRecord = record;
		}

		/// <summary>
		/// Get the StreamReader.
		/// </summary>
		public StreamReader FileStreamReader
		{
			get
			{
				return _streamReader;
			}
		}

        /// <summary>
        /// Advances the reader from the current 
        /// file position to the position of 
        /// the record at the index specified
        /// by the current value of the 
        /// <see cref="CurrentRecordIndex"/> property.
        /// </summary>
        /// <returns>void</returns>
		public void MoveToCurrentRecord()
		{
			int temp = CurrentRecordIndex;

			try
			{
				for (int i = 0; i < temp; i++)
				{
					Read();
				}
			}
			finally
			{
				CurrentRecordIndex = temp;
			}
		}

        /// <summary>
        /// A utility function that throws
        /// a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <returns>void</returns>
		protected void NotSupported()
		{
			throw new System.NotSupportedException();
		}

        /// <summary>
        /// See <see cref="IDataReader.GetSchemaTable"/></summary>
        /// <returns>System.Data.DataTable</returns>
		DataTable IDataReader.GetSchemaTable() 
		{ 
			return null;
		}

        /// <summary>
        /// See <see cref="IDataReader.NextResult"/></summary>
        /// <returns>bool</returns>
		bool IDataReader.NextResult()
		{
			return false;	
		}

		/// <summary>
		/// See <see cref="IDataReader.Depth"/>
		/// </summary>
		int IDataReader.Depth 
		{
			get 
			{
				return _depth;
			}
		}

        /// <summary>
        /// Set the depth.
        /// </summary>
        /// <param name="depth">the depth.</param>
        /// <returns>void</returns>
		protected void SetDepth(int depth)
		{
			_depth = depth;
		}

		/// <summary>
		/// See <see cref="IDataReader.IsClosed"/>
		/// </summary>
		public bool IsClosed
		{
			get 
			{
				return _isClosed;
			}
		}

        /// <summary>
        /// Set whether the <see cref="FileDataReader"/> is closed.
        /// </summary>
        /// <param name="isClosed">bool</param>
        /// <returns>void</returns>
		protected void SetIsClosed(bool isClosed)
		{
			_isClosed = isClosed;
		}


		/// <summary>
		/// See <see cref="IDataReader.RecordsAffected"/>
		/// </summary>
		int IDataReader.RecordsAffected
		{
			get 
			{
				return _recordsAffected;
			}
		}

        /// <summary>SetRecordsAffected</summary>
        /// <param name="recordsAffected">int</param>
        /// <returns>void</returns>
		protected void SetRecordsAffected(int recordsAffected)
		{
			_recordsAffected = recordsAffected;
		}

		/// <summary>
		/// See <see cref="IDataRecord.FieldCount"/>
		/// </summary>
		public virtual int FieldCount
		{
			get 
			{
				return _fieldCount;
			}
		}

        /// <summary>
        /// See <see cref="IDataReader.Read"/></summary>
        /// <returns>true for not eof; false for eof.</returns>
		public virtual bool Read()
		{
			_currentRecordIndex++;
			bool notEof = ReadImpl();

			if (!notEof)
			{
				_currentRecordIndex--;
			}

			return notEof;
		}

        /// <summary>
        /// The implementation of the Read method.
        /// </summary>
        /// <returns>bool</returns>
		protected abstract bool ReadImpl();

        /// <summary>
        /// Opens the <see cref="FileStream"/> for reading.
        /// </summary>
        /// <returns>void</returns>
		public void Open()
		{
			FileStream stream = File.OpenRead(FilePath);
			Open(stream);
		}

        /// <summary>
        /// Opens the <see cref="FileStream"/> for reading.
        /// </summary>
        /// <param name="stream">the <see cref="FileStream"/> containing the data.</param>
        /// <returns>void</returns>
		public void Open(FileStream stream)
		{
			StreamReader streamReader = new StreamReader(stream);
			Open(streamReader);
		}

        /// <summary>
        /// Setting the <see cref="StreamReader"/> for the data.
        /// </summary>
        /// <param name="reader">a <see cref="StreamReader"/> that can access the data.</param>
        /// <returns>void</returns>
		public virtual void Open(StreamReader reader)
		{
			_streamReader = reader;
		}

        /// <summary>
        /// Close the <see cref="StreamReader"/> for reading.
        /// </summary>
        /// <returns>void</returns>
		public virtual void Close() 
		{
			_currentRecord = null;
			if (FileStreamReader != null)
			{
				FileStreamReader.Close();
			}
			SetIsClosed(true);	
		}

        /// <summary>
        /// See <see cref="IDataRecord.GetString"/>.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <returns>the string value for the column.</returns>
		public abstract string GetString(int columnIndex);

        /// <summary>
        /// See <see cref="IDataRecord.GetValue"/>.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <returns>the string value for the column.</returns>
		public abstract object GetValue(int columnIndex);

        /// <summary>
        /// See <see cref="IDataRecord.GetValues"/></summary>
        /// <param name="values">the pre-created object array to receive the values.</param>
        /// <returns>the size of the resulting array.</returns>
		public virtual int GetValues(object[] values)
		{
			for (int i = 0 ; i < FieldCount; i++)
			{
				values[i] = GetValue(i);
			}
			return values.Length;
		}

        /// <summary>
        /// See <see cref="IDataRecord.GetBoolean"/>.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <returns>the string value converted to a boolean.</returns>
		public virtual bool GetBoolean(int columnIndex) 
		{
			return Boolean.Parse(GetString(columnIndex));
		}

        /// <summary>
        /// See <see cref="IDataRecord.GetByte"/>.
        /// </summary>
        /// <param name="columnIndex">the index of the column.</param>
        /// <returns>the string value converted to one byte.</returns>
		public virtual byte GetByte(int columnIndex) 
		{
			return Byte.Parse(GetString(columnIndex));
		}

        /// <summary>
        /// See <see cref="IDataRecord.GetBytes"/></summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <param name="fieldoffset">the starting offset of the origial data buffer.</param>
        /// <param name="buffer">the pre-allocated byte array.</param>
        /// <param name="bufferoffset">the starting offset of the destination buffer.</param>
        /// <param name="length">the length of the byte array.</param>
        /// <returns>the length of the byte array, plus the fieldoffset.</returns>
		public virtual long GetBytes(int columnIndex, long fieldoffset, byte[] buffer, int bufferoffset, int length) 
		{
			string encodedBytes = GetString(columnIndex).Trim();
			byte[] bytes = Convert.FromBase64String(encodedBytes);
			
			long j = fieldoffset;

			for (int k = bufferoffset; k < bufferoffset + length; k++)
			{
				if (bytes.Length < j)
				{
					byte b = bytes[fieldoffset];
					buffer[k] = b;
					j++;
				}
			}

			return j;	
		}

        /// <summary>
        /// See <see cref="IDataRecord.GetChar"/></summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <returns>the string value converted to one char.</returns>
		public virtual char GetChar(int columnIndex) 
		{
			return Char.Parse(GetString(columnIndex));
		}

        /// <summary>
        /// See <see cref="IDataRecord.GetChars"/></summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <param name="fieldoffset">long</param>
        /// <param name="buffer">char[]</param>
        /// <param name="length">int</param>
        /// <param name="bufferoffset">int</param>
        /// <returns>Not supported funcationality.</returns>
		public virtual long GetChars(int columnIndex, long fieldoffset, char[] buffer, int length, int bufferoffset) 
		{
			NotSupported();
			return 0;	
		}

        /// <summary>
        /// See <see cref="IDataRecord.GetDateTime"/>.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <returns>the string value converted to a <see cref="DateTime"/> value.</returns>
		public virtual DateTime GetDateTime(int columnIndex) 
		{
			return DateTime.Parse(GetString(columnIndex));
		}

        /// <summary>
        /// See <see cref="IDataRecord.GetDecimal"/>.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <returns>the string value converted to a <see cref="decimal"/> value.</returns>
		public virtual decimal GetDecimal(int columnIndex) 
		{
			return Decimal.Parse(GetString(columnIndex));
		}

        /// <summary>
        /// See <see cref="IDataRecord.GetDouble"/></summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <returns>the string value converted to a <see cref="double"/> value.</returns>
		public virtual double GetDouble(int columnIndex) 
		{
			return Double.Parse(GetString(columnIndex));
		}

        /// <summary>
        /// See <see cref="IDataRecord.GetFloat"/></summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <returns>the string value converted to a <see cref="float"/> value.</returns>
		public virtual float GetFloat(int columnIndex) 
		{
			return float.Parse(GetString(columnIndex));
		}

        /// <summary>
        /// See <see cref="IDataRecord.GetGuid"/></summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <returns>the string value converted to a <see cref="Guid"/> value.</returns>
		public virtual Guid GetGuid(int columnIndex) 
		{
			return new Guid(GetString(columnIndex));
		}

        /// <summary>
        /// See <see cref="IDataRecord.GetInt16"/></summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <returns>the string value converted to a <see cref="short"/> value.</returns>
		public virtual short GetInt16(int columnIndex) 
		{
			return Int16.Parse(GetString(columnIndex));
		}

        /// <summary>
        /// See <see cref="IDataRecord.GetInt32"/></summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <returns>the string value converted to a <see cref="int"/> value.</returns>
		public virtual int GetInt32(int columnIndex) 
		{
			return Int32.Parse(GetString(columnIndex));
		}

        /// <summary>
        /// See <see cref="IDataRecord.GetInt64"/></summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <returns>the string value converted to a <see cref="long"/> value.</returns>
		public virtual long GetInt64(int columnIndex) 
		{
			return Int64.Parse(GetString(columnIndex));
		}

        /// <summary>
        /// Not supported.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <returns>null.</returns>
		string IDataRecord.GetName(int columnIndex) 
		{
			NotSupported();
			return null;
		}

        /// <summary>
        /// Not supported.
        /// </summary>
        /// <param name="name">string</param>
        /// <returns>null.</returns>
		int IDataRecord.GetOrdinal(string name) 
		{
			NotSupported();
			return 0;
		}

        /// <summary>
        /// Not supported.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <returns>null.</returns>
		IDataReader IDataRecord.GetData(int columnIndex) 
		{
			NotSupported();
			return null;
		}

        /// <summary>
        /// Not supported.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <returns>null.</returns>
		string IDataRecord.GetDataTypeName(int columnIndex) 
		{
			NotSupported();
			return null;
		}

        /// <summary>
        /// Not supported.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <returns>null.</returns>
		Type IDataRecord.GetFieldType(int columnIndex) 
		{
			NotSupported();
			return null;
		}

        /// <summary>
        /// Check if the Data is null.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <returns>always returns false.</returns>
		bool IDataRecord.IsDBNull(int columnIndex) 
		{
			return false;	
		}

        /// <summary>
        /// Setting the field count.
        /// </summary>
        /// <param name="fieldCount">the field count value to set.</param>
        /// <returns>void</returns>
		protected void SetFieldCount(int fieldCount)
		{
			_fieldCount = fieldCount;
		}

		/// <summary>
		/// Not supported.
		/// </summary>
		/// <exception cref="System.NotSupportedException"> got thrown if this method is invoked.</exception>
		public virtual object this[string name] 
		{
			get 
			{
				NotSupported();
				return null;
			}
		}

		/// <summary>
		/// See <see cref="GetValue"/>.
		/// </summary>
		public object this[int columnIndex] 
		{
			get 
			{
				return GetValue(columnIndex);	
			}
		}

        /// <summary>
        /// Close the <see cref="StreamReader"/> for reading.  See <see cref="Close"/>.
        /// </summary>
        /// <returns>void</returns>
		void IDisposable.Dispose()
		{
			Close();
		}
	}


}
