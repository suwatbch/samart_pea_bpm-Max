// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Xml.Serialization;

using Avanade.ACA.Batch;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Configuration;

namespace Avanade.ACA.Batch.Configuration
{
	/// <summary>
	/// RequestCommandData represents the data needed to enqueue a Batch request.
	/// </summary>    
    public class RequestCommandData : NamedConfigurationElement, IBatchData
	{
        private const string displayNameProperty = "name";
        private const string commandFilePathProperty = "commandFilePath";

        private string commandFilePath;
		private string commandFileDir;
		private string commandFileName;

        /// <summary>
        /// Initializes the object with an empty name.
        /// </summary>
        public RequestCommandData() : this(string.Empty)
        {        
        }

        /// <summary>
        /// Initializes the object with a name.
        /// </summary>
        /// <param name="displayName">The display name.</param>
		public RequestCommandData(string displayName)
		{
			this[displayNameProperty] = displayName;
			this[commandFilePathProperty] = String.Empty;
            commandFileDir = String.Empty;
            commandFileName = String.Empty;
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
		/// The path to the XML command file for the request excludes the file name.
		/// </summary>		
		public string CommandFileDir
		{
			get {return commandFileDir;}
			set {commandFileDir = value;}
		}

		/// <summary>
		/// The name of the XML command file for the request.
		/// </summary>		
		public string CommandFileName
		{
			get {return commandFileName;	}
			set {commandFileName = value;}
		}

		/// <summary>
		/// The path to the XML command file for the request.
		/// </summary>		
        [ConfigurationProperty(commandFilePathProperty)]
		public string CommandFilePath
		{
			get { return this[commandFilePathProperty].ToString();}
			set { this[commandFilePathProperty] = value;}			
		}
	}
}
