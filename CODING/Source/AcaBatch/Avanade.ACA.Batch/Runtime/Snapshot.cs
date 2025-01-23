// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// Represents the saved values a
	/// <see cref="JobExecutionContext"/> object's
	/// properties at a given point in time.
	/// </summary>
	[Serializable]
	public class Snapshot
	{
		private JobExecutionContextData.Snapshot	_snapshot;
		private string								_name;
		private SnapshotCollection					_collection;

        /// <summary>Snapshot</summary>
        /// <param name="context">Avanade.ACA.Batch.JobExecutionContext</param>
        /// <param name="name">string</param>
        /// <param name="collection">Avanade.ACA.Batch.SnapshotCollection</param>
        internal Snapshot
			(
			JobExecutionContext context, 
			string name, 
			SnapshotCollection collection
			)
		{
			_collection = collection;
			_name		= name;
			_snapshot	= new JobExecutionContextData.Snapshot(context);
		}

        /// <summary>
        /// Replaces the current values of the JobExecutionContext object's
        /// properties with the values of this snapshot.
        /// </summary>
        /// <returns>void</returns>
		public void Restore()
		{
			_snapshot.Restore();
		}

        /// <summary>
        /// Gets the value of the job parameter with the
        /// specified name
        /// at the time the snapshot was taken.
        /// </summary>
        /// <param name="name">string</param>
        /// <returns>object</returns>
		public object GetParameter(string name)
		{
			return _snapshot.GetParameter(name);
		}

		/// <summary>
		/// Gets the <see cref="LastCommitDate"/> of the JobExecutionContext
		/// at the time the snapshot was taken.
		/// </summary>
		public int CommitCount
		{
			get
			{
				return _snapshot.CommitCount;
			}
		}


		/// <summary>
		/// Gets the <see cref="LastCommitDate"/> of the JobExecutionContext
		/// at the time the snapshot was taken.
		/// </summary>
		public int WorkUnitCount
		{
			get
			{
				return _snapshot.WorkUnitCount;
			}
		}

		/// <summary>
		/// Gets the <see cref="LastCommitDate"/> of the JobExecutionContext
		/// at the time the snapshot was taken.
		/// </summary>
		public DateTime LastCommitDate
		{
			get
			{
				return _snapshot.LastCommitDate;
			}
		}

		/// <summary>
		/// Gets the <see cref="StatusCode"/> of the JobExecutionContext
		/// at the time the snapshot was taken.
		/// </summary>
		public StatusCode Status
		{
			get
			{
				return _snapshot.Status;
			}
		}

		/// <summary>
		/// Gets the Index of the current <see cref="Snapshot"/>
		/// in the <see cref="JobExecutionContext.Snapshots"/>
		/// collection.
		/// </summary>
		public int Index
		{
			get
			{
				return _collection.IndexOf(this);
			}
		}

		/// <summary>
		/// Gets the name of the current <see cref="Snapshot"/>.
		/// </summary>
		public string Name
		{
			get
			{
				return _name;
			}
		}
	}


}
