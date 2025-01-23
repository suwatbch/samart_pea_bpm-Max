// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Avanade.ACA.Batch.Configuration
{
	/// <summary>
	/// JobReferenceData represents a job's relationship to its Batch definition.
	/// With this object, the <see cref="Key"/> property is not used.
	/// </summary>    
    public class JobReferenceData : NamedConfigurationElement, IBatchDBData
	{
        //private string displayName; 
        private const string displayNameProperty = "name";
		private Guid jobDefinitionKey;
		private int sequenceNum;
		private int oldSequenceNum;

        /// <summary>
        /// Initializes the object with empty name and an empty key.<see cref="Key"/>.
        /// </summary>
		public JobReferenceData() : this(string.Empty)
		{
		}

        /// <summary>
        /// Initializes the object with a name and an empty key.<see cref="Key"/>.
        /// </summary>
        /// <param name="displayName">The display name.</param>
		public JobReferenceData(string displayName) : base(displayName)
		{
			//this.displayName = displayName;
            this[displayNameProperty] = displayName;
			jobDefinitionKey = Guid.Empty;
			sequenceNum = -1;
			oldSequenceNum = -1;
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
		/// Implements the <see cref="IBatchDBData.Key"/> property.  Gets or sets the key for the data.
		/// </summary>
		/// <value>a <see cref="System.Guid"/> instance as the key of the data object.</value>
		public Guid Key
		{
			get {return Guid.Empty;}
		}

		/// <summary>
		/// See <see cref="IBatchDBData.Children"/>.
		/// </summary>
		public IList Children
		{
			get {return new ArrayList();}
		}

		/// <summary>
		/// Gets or sets the job definition Key.
		/// </summary>
		public Guid JobDefinitionKey
		{
			get { return this.jobDefinitionKey;}
			set { this.jobDefinitionKey = value;}
		}

		/// <summary>
		/// Gets or sets the job's position within a Batch.
		/// </summary>
		public int SequenceNum
		{
			get { return this.sequenceNum; }
			set { this.sequenceNum = value; }
		}

		/// <summary>
		/// Gets or sets the original sequence of the same job within the Batch.
		/// </summary>
		public int OldSequenceNum
		{
			get { return this.oldSequenceNum; }
			set { this.oldSequenceNum = value; }
		}

	}
}
