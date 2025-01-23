// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System.IO;
using System.Xml.Serialization;
using Avanade.ACA.Batch.Configuration.IO;
using System.Configuration;

namespace Avanade.ACA.Batch.IO
{
	/// <summary>
	/// Represents a set of files located in a given directory
	/// that match a given search patter. This class supports XML serialization
	/// and can be used as a Batch or Job parameter.
	/// </summary>
	public class FileSetSearchHelper
	{
		private DirectoryInfo			_directory;
		private FileInfo[]				_files;
		private FileSetParameterData	_data;
        private const string searchPatternProperty = "searchPattern";
        private const string directoryPathProperty = "directoryPath";

        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="FileSetSearchHelper"/> class.
        /// </summary>
		public FileSetSearchHelper() : this(string.Empty,string.Empty)
		{
		}

        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="FileSetSearchHelper"/> class.
        /// </summary>
        /// <param name="directoryPath">the directory to search.</param>
        /// <param name="searchPattern">the search pattern for the file name.</param>
		public FileSetSearchHelper(string directoryPath, string searchPattern) :
			this(new FileSetParameterData(string.Empty, directoryPath, searchPattern))
		{
		}

        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="FileSetSearchHelper"/> class.
        /// </summary>
        /// <param name="data">The data describing the directory to search and
        /// the search pattern for the file name.</param>
		public FileSetSearchHelper(FileSetParameterData data)
		{
			this._data = data;
		}

		/// <summary>
		/// Gets or sets the directory path to do the searching.
		/// </summary>
        [ConfigurationProperty(directoryPathProperty)]
		public string DirectoryPath
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
		/// Gets or sets the search pattern of the files in the directory path.
		/// </summary>
        [ConfigurationProperty(searchPatternProperty)]
		public string SearchPattern
		{
			get
			{
				return _data.SearchPattern;
			}
			set
			{
				_data.SearchPattern = value;
			}
		}

		/// <summary>
		/// Gets all the files matching the search pattern in the directory.
		/// </summary>		
		public FileInfo[] Files
		{
			get
			{
				if (_files == null)
				{
					_files = Directory.GetFiles(SearchPattern);
				}
				return _files;
			}
		}

		/// <summary>
		/// Gets the <see cref="DirectoryInfo"/> of the <see cref="DirectoryPath"/>.
		/// </summary>		
		public DirectoryInfo Directory
		{
			get
			{
				if (_directory == null)
				{
					_directory = new DirectoryInfo(DirectoryPath);
				}
				return _directory;
			}
		}
	}
}
