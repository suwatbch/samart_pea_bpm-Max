// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// IBatchDBData is the interface for all the Batch data stored in the Batch database.
	/// </summary>
	public interface IBatchDBData : IBatchData
	{
		/// <summary>
		/// The key of the data element.
		/// </summary>
		Guid Key
		{
			get;
		}
		
		/// <summary>
		/// The logical children for the data.
		/// </summary>
		/// <remarks>This property is to support batch monitor and other client applications 
		/// that may have relied on this property to display the configuration or runtime data
		/// without being aware of the schema.</remarks>
		System.Collections.IList Children
		{
			get;
		}
	}

	/// <summary>
	/// Represents a configuration data may or may not stored in the Batch database.
	/// </summary>
	public interface IBatchData
	{
		/// <summary>
		/// The display name for the Batch configuration data.
		/// </summary>
		string DisplayName
		{
			get;
		}

		
	}
}
