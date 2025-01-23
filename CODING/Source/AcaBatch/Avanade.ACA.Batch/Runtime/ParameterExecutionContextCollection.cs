// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Collections;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Avanade.ACA.Batch.Configuration;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// Summary description for ParameterExecutionContextCollection.
	/// </summary>
	[Serializable]
	public class ParameterExecutionContextCollection : ParameterCollection
	{
		private Database	batchDB;
		private ParameterCategory parmCategory;


        /// <summary>ParameterExecutionContextCollection</summary>
        /// <param name="db">Microsoft.Practices.EnterpriseLibrary.Data.Database</param>
        /// <param name="key">System.Guid</param>
        /// <param name="cat">Avanade.ACA.Batch.ParameterCategory</param>
		public ParameterExecutionContextCollection(Database db, Guid key, ParameterCategory cat)
		{
			batchDB = db;
			parentKey = key;
			parmCategory = cat;
			this.DisplayName = "Parameters";
		}

        /// <summary>
        /// Loads parameters from the Batch database.
        /// </summary>
        /// <returns>bool</returns>
		public bool LoadParameters()
		{
			if( batchDB == null )
			{
				return false;
			}

			base.LoadParameters(batchDB,parentKey,parmCategory);

			return true;
		}


        /// <summary>
        /// Saves parameters to the Batch Database.
        /// </summary>
        /// <param name="commitHandle">Avanade.ACA.Batch.RequestCommitHandle</param>
        /// <param name="IsLocked">bool</param>
        /// <returns>void</returns>
		public void SaveParameters(RequestCommitHandle commitHandle, bool IsLocked)
		{

			IEnumerator enumer = this.GetEnumerator();

			while (enumer.MoveNext()) 
			{
				ParameterData data = (ParameterData) enumer.Current;
//				string convertedValue = data.ConvertValueToString(data.Value);
				string convertedValue = data.Serialize();

				if (this.parmCategory == ParameterCategory.BatchExecutionContextCurrent)
				{
					if (data.Key == Guid.Empty)
					{
						data.Key = Guid.NewGuid();
					}
					commitHandle.CommitBatchExecContextParameter(data.Key,
														this.parentKey, data.DisplayName,
														data.ValueTypeKey, data.ValueType, 
														convertedValue);
				}
				else if (this.parmCategory == ParameterCategory.JobExecutionContextCurrent)
				{		
					if (!IsLocked)
					{
						if (data.Key == Guid.Empty)
						{
							data.Key = Guid.NewGuid();
						}
						commitHandle.CommitJobExecContextParameter(
								data.Key,
								this.parentKey, data.DisplayName,
								data.ValueTypeKey, data.ValueType, convertedValue);
					}
				}
			}

		}

	}
}
