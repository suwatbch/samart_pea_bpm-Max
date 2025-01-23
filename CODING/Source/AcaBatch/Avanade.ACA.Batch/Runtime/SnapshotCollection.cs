// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Collections;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// Represents a collection of <see cref="Snapshot"/>
	/// objects for a given <see cref="JobExecutionContext"/> object.
	/// </summary>
	[Serializable]
	public class SnapshotCollection : IEnumerable, ICollection
	{
		private JobExecutionContext _context;

		private Hashtable	_table;
		private ArrayList	_list;

        /// <summary>
        /// Initializes a new instance of the <see cref="SnapshotCollection"/>
        /// class. The collection will store snapshots of the specified
        /// <see cref="JobExecutionContext"/> object's state.
        /// </summary>
        /// <param name="context">A JobExecutionContext object.</param>
		public SnapshotCollection(JobExecutionContext context)
		{
			_context	= context;
			_list		= new ArrayList();
			_table		= new Hashtable();
		}

		/// <summary>
		/// Gets an <see cref="ICollection"/> containing
		/// the keyed names of all the <see cref="Snapshot"/> objects.
		/// </summary>
		public ICollection Keys
		{
			get
			{
				return _table.Keys;
			}
		}

		/// <summary>
		/// Gets an <see cref="ICollection"/> containing
		/// the values in the <see cref="SnapshotCollection"/>.
		/// </summary>
		public ICollection Values
		{
			get
			{
				return _table.Values;
			}
		}

		/// <summary>
		/// See <see cref="ICollection.SyncRoot"/>.
		/// </summary>
		/// <value>An object that can be used 
		/// to synchronize access to the collection.
		/// </value>
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>
		/// See <see cref="ICollection.IsSynchronized"/>.
		/// This collection is not thread-safe.  Always returns false.
		/// </summary>
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

        /// <summary>
        /// Copies the items in the collection
        /// to the specified array.
        /// </summary>
        /// <param name="array">System.Array</param>
        /// <param name="arrayIndex">int</param>
        /// <returns>void</returns>
		public void CopyTo(Array array, int arrayIndex)
		{
			_list.CopyTo(array, arrayIndex);
		}

		/// <summary>
		/// Gets the number of items in the collection.
		/// </summary>
		public int Count
		{
			get
			{
				return _table.Count;
			}
		}

        /// <summary>
        /// Takes a snapshot of JobExecutionContext
        /// objects memory.
        /// </summary>
        /// <returns>Avanade.ACA.Batch.Snapshot</returns>
		public Snapshot Add()
		{
			string name = GetUniqueName();
			return Add(name);
		}
		
		/// <summary>
		/// See <see cref="IList.IsFixedSize"/>.
		/// </summary>
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>
		/// See <see cref="IList.IsReadOnly"/>.  Always returns false.
		/// </summary>
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

        /// <summary>GetEnumerator</summary>
        /// <returns>System.Collections.IEnumerator</returns>
		public IEnumerator GetEnumerator()
		{
			return _list.GetEnumerator();
		}

        /// <summary>Contains</summary>
        /// <param name="name">string</param>
        /// <returns>bool</returns>
		public bool Contains(string name)
		{
			return _table.Contains(name);
		}

        /// <summary>Contains</summary>
        /// <param name="snapshot">Avanade.ACA.Batch.Snapshot</param>
        /// <returns>bool</returns>
		public bool Contains(Snapshot snapshot)
		{
			return _list.Contains(snapshot);
		}

        /// <summary>
        /// Removes all <see cref="Snapshot"/> objects
        /// from the current <see cref="SnapshotCollection"/>.
        /// </summary>
        /// <returns>void</returns>
		public void Clear()
		{
			_table.Clear();
			_list.Clear();
		}

        /// <summary>
        /// Removes the specified <see cref="Snapshot"/> 
        /// from the <see cref="SnapshotCollection"/>.
        /// </summary>
        /// <param name="name">The name of the <see cref="Snapshot"/> 
        /// object to remove.</param>
        /// <returns>void</returns>
		public void Remove(string name)
		{
			Snapshot snapshot = Get(name);
			_table.Remove(name);
			_list.Remove(snapshot);
		}

        /// <summary>
        /// Removes the Snapshot located at the specified index.
        /// </summary>
        /// <param name="index">The zer0-based index of the Snapshot
        /// to remove.</param>
        /// <returns>void</returns>
		public void RemoveAt(int index)
		{
			Snapshot snapshot = Get(index);
			_table.Remove(snapshot.Name);
			_list.RemoveAt(index);
		}

        /// <summary>
        /// Creates a named snapshot of the current <see cref="JobExecutionContext"/>
        /// and adds it to the end of the <see cref="SnapshotCollection"/>.
        /// </summary>
        /// <param name="name">The name by which the <see cref="Snapshot"/> will be indexed.</param>
        /// <returns>The name of the newly created <see cref="Snapshot"/></returns>
		public Snapshot Add(string name)
		{
			Snapshot snapshot = new Snapshot(_context, name, this);
			_table.Add(name, snapshot);
			_list.Add(snapshot);
			return snapshot;
		}

        /// <summary>
        /// Returns an individual <see cref="Snapshot"/> 
        /// object from the <see cref="SnapshotCollection"/>. 
        /// This property is overloaded to allow retrieval 
        /// of modules by either name or numerical index.
        /// </summary>
        /// <param name="name">The key of the item to be retrieved</param>
        /// <returns>The <see cref="Snapshot"/> member specified by <i>name</i>.</returns>
		public Snapshot Get(string name)
		{
			return (Snapshot)_table[name];
		}

        /// <summary>
        /// Gets the <see cref="Snapshot"/> at the specified index.
        /// </summary>
        /// <param name="index">Index of the <see cref="Snapshot"/>object to return from the collection</param>
        /// <returns>The <see cref="Snapshot"/>member specified by <i>index</i>.</returns>
		public Snapshot Get(int index)
		{
			return (Snapshot)_list[index];
		}

		/// <summary>
		/// Gets a snapshot indexed by the specified name
		/// </summary>
		/// <value>The friendly name of the snapshot to retrieve.</value>
		public Snapshot this[string name]
		{
			get
			{
				return Get(name);
			}
		}

        /// <summary>GetUniqueName</summary>
        /// <returns>string</returns>
        private string GetUniqueName()
		{
			string name = "Snapshot";
			int i = 1;
			while (_table.ContainsKey(name + i.ToString()))
			{
				i++;
			}
			return name + i.ToString();
		}

        /// <summary>
        /// Gets the index of the specified <see cref="Snapshot"/> in the collection, 
        /// if it exists in the collection.
        /// </summary>
        /// <param name="snapshot">The <see cref="Snapshot"/> to locate.</param>
        /// <returns>The index of the specified 
        /// <see cref="Snapshot"/>in the SnapshotCollection, if found; otherwise, -1.
        /// </returns>
		public int IndexOf(Snapshot snapshot)
		{
			return _list.IndexOf(snapshot);
		}
	}
}
