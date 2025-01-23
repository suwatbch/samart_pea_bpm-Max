// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Avanade.ACA.Batch.Configuration
{
	/// <summary>
	/// BatchTypeData represents the configurations for a Batch type.
	/// </summary>    
    public class BatchTypeData : NamedConfigurationElement, IBatchDBData
	{
		//private string displayName;
        private const string displayNameProperty = "name";
		private string description;
		private string batchTypeName;
		private Guid instanceKey;

        /// <summary>
        /// Initializes the object with empty name and a newly generated <see cref="Key"/>.
        /// </summary>
		public BatchTypeData() : this(string.Empty)
		{
		}

        /// <summary>
        /// Initializes the object with a name and a newly generated <see cref="Key"/>.
        /// </summary>
        /// <param name="displayName">The display name.</param>
		public BatchTypeData(string displayName) : this(displayName, Guid.NewGuid())
		{			
		}

        /// <summary>
        /// Initializes the object with a name and a <see cref="Key"/>.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="key">The key.</param>
		public BatchTypeData(string displayName, Guid key) : base(displayName)
		{
			//this.displayName = displayName;
            this[displayNameProperty] = displayName;
			this.instanceKey = key;
		}

		

		/// <summary>
		/// See <see cref="IBatchData.DisplayName"/>.
		/// </summary>
		public string DisplayName
		{
			get
			{
				//return displayName;
                return this[displayNameProperty].ToString();
			}
			set
			{
				//displayName = value;
                this[displayNameProperty] = value;
			}
		}

		/// <summary>
		/// The description of the defined type.
		/// </summary>
		public string Description
		{
			get { return this.description; }
			set { this.description = value; }
		}

		/// <summary>
		/// The name of the class that implements the Avanade.ACA.Batch.IJob interface.
		/// </summary>
		public string TypeName
		{
			get { return this.batchTypeName;}
			set { this.batchTypeName = value;}
		}

		/// <summary>
		/// Implements the <see cref="IBatchDBData.Key"/> property.  Gets or sets the key for the data.
		/// </summary>
		/// <value>a <see cref="System.Guid"/> instance as the key of the data object.</value>
		public Guid Key
		{
			get { return this.instanceKey;}
			set { this.instanceKey = value;}
		}


		/// <summary>
		/// Returns a new empty <see cref="System.Collections.ArrayList"/>.
		/// </summary>
		public System.Collections.IList Children
		{
			get 
			{
				return new System.Collections.ArrayList();
			}
		}
	}
}
