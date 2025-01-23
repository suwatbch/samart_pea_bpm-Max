// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Avanade.ACA.Batch.Configuration
{
	/// <summary>
	/// JobTypeData represents the configuration for a Batch job type.
	/// </summary>    
    public class JobTypeData : NamedConfigurationElement, IBatchDBData
	{
		//private string displayName;
        private const string displayNameProperty = "name";
		private string description;
		private string jobTypeName;
		private Guid instanceKey;

        /// <summary>
        /// Initializes the object with empty name and a newly generated <see cref="Key"/>.
        /// </summary>
		public JobTypeData() : this(string.Empty)
		{
		}

        /// <summary>
        /// Initializes the object with a name and a newly generated <see cref="Key"/>.
        /// </summary>
        /// <param name="displayName">The display name.</param>
		public JobTypeData(string displayName) : this(displayName, Guid.NewGuid())
		{
		}

        /// <summary>
        /// Initializes the object with a name and a <see cref="Key"/>.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="key">The key.</param>
		public JobTypeData(string displayName, Guid key) : base(displayName)
		{
			//this.displayName = displayName;
            this[displayNameProperty] = displayName;
			this.description = string.Empty;
			this.jobTypeName = string.Empty;
			this.instanceKey = key;
		}

		/// <summary>
		/// See <see cref="IBatchData.DisplayName"/>.
		/// </summary>
		public string DisplayName
		{
			get
			{
                return this[displayNameProperty].ToString();
			}
			set
			{
				this[displayNameProperty] = value;
			}
		}

		/// <summary>
		/// Gets or sets the description of the defined type.
		/// </summary>
		public string Description
		{
			get { return this.description; }
			set { this.description = value; }
		}

		/// <summary>
		/// Gets or sets the  name of the class that implements the Avanade.ACA.Batch.IJob interface.
		/// </summary>
		public string TypeName
		{
			get { return this.jobTypeName;}
			set { this.jobTypeName = value;}
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
		/// See <see cref="IBatchDBData.Children"/>.
		/// </summary>
		public System.Collections.IList Children
		{
			get{return new System.Collections.ArrayList();}
		}
	}
}
