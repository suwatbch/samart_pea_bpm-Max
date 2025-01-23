// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Collections;
using System.Data;
using Avanade.ACA.Batch.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
namespace Avanade.ACA.Batch
{
	/// <summary>
	/// This is the root node for Batch configurations.
	/// </summary>    
    public class ExceptionCollection : NamedElementCollection<ExceptionData>, IBatchDBData
	{
		private Database batchDB;
		private Guid jobExecutionKey;

        /// <summary>ExceptionCollection</summary>
        /// <param name="db">Microsoft.Practices.EnterpriseLibrary.Data.Database</param>
        /// <param name="key">System.Guid</param>
		

		public ExceptionCollection(Database db, Guid key) 
		{
			batchDB = db;
			jobExecutionKey = key;
		}

        /// <summary>
        /// Loads the exceptions from the Batch database.
        /// </summary>
        /// <returns>void</returns>
		public void LoadExceptions()
		{

			if( batchDB == null )
			{
				return;
			}

			
			IDataReader reader = null;
			
			try
			{
				reader = DefaultBatchManager.RequestQueue.ListExceptions(batchDB,this.jobExecutionKey);

				Hashtable keyToExceptionTable = new Hashtable();

				while( reader.Read() )
				{
					ExceptionData data = new ExceptionData(reader.GetGuid(0));

				
					data.ExceptionType = String.Empty;
					if (!reader.IsDBNull(1))
					{
						data.ExceptionType = reader.GetString(1);
					}
					data.Message = String.Empty;
					if (!reader.IsDBNull(2))
					{
						data.Message = reader.GetString(2);
					}
					data.Source = String.Empty;
					if (!reader.IsDBNull(3))
					{
						data.Source = reader.GetString(3);
					}
					data.Target = String.Empty;
					if (!reader.IsDBNull(4))
					{
						data.Target = reader.GetString(4);
					}
					data.StackTrace = String.Empty;
					if (!reader.IsDBNull(5))
					{
						data.StackTrace = reader.GetString(5);
					}
					data.OuterExceptionId = Guid.Empty;
					if (!reader.IsDBNull(6))
					{
						data.OuterExceptionId = reader.GetGuid(6);
						data.OuterException = (ExceptionData) keyToExceptionTable[data.OuterExceptionId];
					}
					
					this.Add(data);
					keyToExceptionTable.Add(data.Key, data);
				}
			}
			finally
			{
				if( reader != null )
				{
					reader.Close();
				}
			}

		}


        /// <summary>
        /// Saves the exception instances associated with a <see cref="JobExecutionContextData"/> 
        /// to the Batch database.
        /// </summary>
        /// <param name="commitHandle">The handle containing the current 
        /// <see cref="System.Data.IDbTransaction"/> used for committing the exception data.</param>
        /// <returns>void</returns>
		public void SaveExceptions(RequestCommitHandle commitHandle)
		{
			IEnumerator enumer = this.GetEnumerator();

			while (enumer.MoveNext()) 
			{
				ExceptionData data = (ExceptionData) enumer.Current;

				Guid outerExceptionKey = Guid.Empty;

				if (data.OuterException != null)
				{
					outerExceptionKey = data.OuterException.Key;
				}

				commitHandle.CommitJobException(data.Key, this.jobExecutionKey, data.ExceptionType,
								data.Message, data.Source, data.Target, data.StackTrace, outerExceptionKey);
			}

		}

        /// <summary>AddException</summary>
        /// <param name="e">System.Exception</param>
        /// <param name="outerException">Avanade.ACA.Batch.ExceptionData</param>
        /// <returns>Avanade.ACA.Batch.ExceptionData</returns>
        private ExceptionData AddException(Exception e, ExceptionData outerException)
		{
			ExceptionData curException = new ExceptionData();
			curException.ExceptionObject = e;
			curException.OuterException = outerException;
			this.Add(curException);

			Exception inner = e.InnerException;
			
			if (inner != null)
			{
				ExceptionData innerExceptionData = this.AddException(inner, curException);
//				curException.InnerException = innerExceptionData;
			}
			return curException;
		}


        /// <summary>
        /// Add an exception object to the collection.
        /// </summary>
        /// <param name="e">The exception as the root of the occurence.</param>
        /// <returns>The <see cref="ExceptionData"/> containing the exception instance.</returns>
		public ExceptionData AddException(Exception e)
		{
			return AddException(e, null);
		}

		/// <summary>
		/// See <see cref="IBatchData.DisplayName"/>.  The value is always "Exceptions".
		/// </summary>
		public string DisplayName
		{
			get
			{
				return "Exceptions";
			}
		}

		/// <summary>
		/// Gets the key for the object that owns the parameter collection.
		/// </summary>
		public Guid Key
		{
			get
			{
				return Guid.Empty;
			}
		}

		/// <summary>
		/// See <see cref="IBatchDBData.Children"/>.
		/// </summary>
		public IList Children
		{
			get
			{
				return new ArrayList(this);
			}
		}

		/// <summary>
		/// Gets or sets the data by index.
		/// </summary>

		public ExceptionData this[int index]
		{
			get 
			{
				return this.GetData(index);
			}
			set
			{
				this.SetData(index, value);
			}
		}

//		/// <summary>
//		/// Gets or sets a <see cref="ExceptionData"/> in the collection by name.  
//		/// If there's no entry exists in the collection, the getter returns null and 
//		/// the setting adds a new entry with the name as the key.
//		/// </summary>
//		/// <exception cref="ArgumentNullException">If the name if null.</exception>
//		public ExceptionData this[Guid key]
//		{
//			get 
//			{
//				if (key == Guid.Empty)
//				{
//					throw new ArgumentNullException("key");
//				}
//				return this.GetData(key.ToString());
//			}
//			set
//			{
//				if (key == Guid.Empty)
//				{
//					throw new ArgumentNullException("key");
//				}
//				this.SetData(key.ToString(), value);
//			}
//		}

        /// <summary>
        /// Adds a <see cref="ExceptionData"/> to the collection with the unique identifier as
        /// the key.
        /// </summary>
        /// <param name="data">The data instance to be added.</param>
        /// <exception cref="ArgumentNullException">The data is null.</exception>
        /// <exception cref="InvalidOperationException">The <see cref="ExceptionData.Key"/> is 
        /// <see cref="System.Guid.Empty"/>.</exception>
        /// <returns>void</returns>
        public new void Add(ExceptionData data)
		{
			if (data == null)
			{
				throw new ArgumentNullException( "data" );
			}
			if (data.Key == Guid.Empty)
			{
				throw new InvalidOperationException("data.Key");
			}

            BaseAdd(data);
		}


        /// <summary>
        /// Add a collection of the <see cref="ExceptionData"/> to the existing collection.
        /// </summary>
        /// <param name="collection">the collection of the entries to add.</param>
        /// <exception cref="ArgumentNullException">Any object in collection
        /// is null.</exception>
        /// <exception cref="InvalidOperationException">The 
        /// <see cref="ExceptionData.Key"/> is <see cref="System.Guid.Empty"/> or the 
        /// existing collection contains an entry with the same key as the ones in 
        /// collection.
        /// </exception>
        /// <returns>void</returns>
		public void AddRange(ExceptionCollection collection)
		{
			foreach(ExceptionData data in collection)
			{
				this.Add(data);
			}
		}

        /// <summary>GetData</summary>
        /// <param name="index">int</param>
        /// <returns>Avanade.ACA.Batch.ExceptionData</returns>
        private ExceptionData GetData(int index)
		{
			return (ExceptionData) BaseGet(index);
		}

        /// <summary>SetData</summary>
        /// <param name="index">int</param>
        /// <param name="data">Avanade.ACA.Batch.ExceptionData</param>
        /// <returns>void</returns>
        private void SetData(int index, ExceptionData data)
        {
            BaseAdd(index, data);
        }
	}
}

