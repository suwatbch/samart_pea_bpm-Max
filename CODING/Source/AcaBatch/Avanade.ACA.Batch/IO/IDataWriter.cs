// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;

namespace Avanade.ACA.Batch.IO
{
	/// <summary>
	/// The IDataWriter interface.
	/// </summary>
	public interface IDataWriter
	{
        /// <summary>
        /// Write data.
        /// </summary>
        /// <returns>void</returns>
		void Write();

        /// <summary>
        /// Read data.
        /// </summary>
        /// <returns>void</returns>
		void Close();

        /// <summary>
        /// Sets a <see cref="string"/> value to a specific column.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <param name="value">the <see cref="string"/> value to be set.</param>
        /// <returns>void</returns>
		void AddString(int columnIndex, string value);

        /// <summary>
        /// Sets an <see cref="int"/> value to a column.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <param name="value">the <see cref="int"/> value to be set.</param>
        /// <returns>void</returns>
		void AddInt32(int columnIndex, int value);

        /// <summary>
        /// Sets a <see cref="short"/> value to a column.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <param name="value">the <see cref="short"/> value to be set.</param>
        /// <returns>void</returns>
		void AddInt16(int columnIndex, short value);

        /// <summary>
        /// Setting a <see cref="long"/> value to a column.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <param name="value">the <see cref="long"/> value to be set.</param>
        /// <returns>void</returns>
		void AddInt64(int columnIndex, long value);

        /// <summary>
        /// Setting a <see cref="bool"/> value to a column.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <param name="value">the <see cref="bool"/> value to be set.</param>
        /// <returns>void</returns>
		void AddBoolean(int columnIndex, bool value);

        /// <summary>
        /// Setting a <see cref="byte"/> value to a column.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <param name="value">the <see cref="byte"/> value to be set.</param>
        /// <returns>void</returns>
		void AddByte(int columnIndex, byte value);

        /// <summary>
        /// Setting a <see cref="byte"/> array value to a column.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <param name="value">the <see cref="byte"/> array value to be set.</param>
        /// <returns>void</returns>
		void AddBytes(int columnIndex, byte[] value);

        /// <summary>
        /// Setting a <see cref="char"/> value to a column.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <param name="value">the <see cref="char"/> value to be set.</param>
        /// <returns>void</returns>
		void AddChar(int columnIndex, char value);

        /// <summary>
        /// Setting a <see cref="char"/> array value to a column.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <param name="value">the <see cref="char"/> array value to be set.</param>
        /// <returns>void</returns>
		void AddChars(int columnIndex, char[] value);

        /// <summary>
        /// Setting a <see cref="DateTime"/> value to a column.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <param name="value">the <see cref="DateTime"/> value to be set.</param>
        /// <returns>void</returns>
		void AddDateTime(int columnIndex, DateTime value);

        /// <summary>
        /// Setting a <see cref="decimal"/> value to a column.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <param name="value">the <see cref="decimal"/> value to be set.</param>
        /// <returns>void</returns>
		void AddDecimal(int columnIndex, decimal value);

        /// <summary>
        /// Setting a <see cref="double"/> value to a column.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <param name="value">the <see cref="double"/> value to be set.</param>
        /// <returns>void</returns>
		void AddDouble(int columnIndex, double value);

        /// <summary>
        /// Setting a <see cref="float"/> value to a column.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <param name="value">the <see cref="float"/> value to be set.</param>
        /// <returns>void</returns>
		void AddFloat(int columnIndex, float value);

        /// <summary>
        /// Setting a <see cref="Guid"/> value to a column.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <param name="value">the <see cref="Guid"/> value to be set.</param>
        /// <returns>void</returns>
		void AddGuid(int columnIndex, Guid value);

        /// <summary>
        /// Setting a <see cref="object"/> value to a column.
        /// </summary>
        /// <param name="columnIndex">the index for the column.</param>
        /// <param name="value">the <see cref="object"/> value to be set.</param>
        /// <returns>void</returns>
		void AddValue(int columnIndex, object value);
	}
}
