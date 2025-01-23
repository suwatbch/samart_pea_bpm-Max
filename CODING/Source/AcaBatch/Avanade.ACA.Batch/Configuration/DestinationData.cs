// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Configuration;

namespace Avanade.ACA.Batch.Configuration
{
	/// <summary>
	/// DestinationData representing the configurations for a Batch destination definition.
	/// </summary>    
	public class DestinationData : NamedConfigurationElement, IBatchDBData
	{
		//private string displayName;
        private const string displayNameProperty = "name";
		private string description;
		private Guid instanceKey;

        /// <summary>
        /// Initializes the object with empty name and a newly generated <see cref="Key"/>.
        /// </summary>
		public DestinationData() : this(string.Empty)
		{
		}

        /// <summary>
        /// Initializes the object with a name and a newly generated <see cref="Key"/>.
        /// </summary>
        /// <param name="displayName">The display name.</param>
		public DestinationData(string displayName) : this(displayName, Guid.NewGuid())
		{			
		}

        /// <summary>
        /// Initializes the object with a name and a <see cref="Key"/>.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="key">The key.</param>
		public DestinationData(string displayName, Guid key) : base(displayName)
		{
			this[displayNameProperty] = displayName;
			this.instanceKey = key;
			this.description = string.Empty;
		}

		/// <summary>
		/// See <see cref="IBatchData.DisplayName"/>.
		/// </summary>
        [ConfigurationProperty(displayNameProperty, IsRequired = true)]
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
		/// The description of the defined destination.
		/// </summary>
		public string Description
		{
			get { return this.description; }
			set { this.description = value; }
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
		public IList Children
		{
			get 
			{
				return new System.Collections.ArrayList();
			}
		}
	}
}
