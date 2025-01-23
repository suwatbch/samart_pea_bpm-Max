// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// Represents the interface that an exception handler must implement
	/// </summary>
	public interface IExceptionHandler
	{
        /// <summary>
        /// Handle the given exception
        /// </summary>
        /// <param name="e">The exception to be handled</param>
        /// <returns>void</returns>
		void Handle(Exception e);
		
		/// <summary>
		/// Gets the CLR Type of the exception
		/// </summary>
		Type ExceptionType
		{
			get;
		}
	}
}
