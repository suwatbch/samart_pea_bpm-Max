// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.ComponentModel;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// Summary description for XmlSerializableTypeDescriptorContext.
	/// </summary>
	internal class XmlSerializableTypeDescriptorContext : ITypeDescriptorContext
	{
		private object _instance;

        /// <summary>XmlSerializableTypeDescriptorContext</summary>
        /// <param name="instance">object</param>
        public XmlSerializableTypeDescriptorContext(object instance)
		{
			_instance = instance;
		}

		public IContainer Container 
		{
			get
			{
				return null;
			}
		}

		public object Instance
		{
			get
			{
				return _instance;
			}
		}

		public PropertyDescriptor PropertyDescriptor
		{
			get
			{
				return null;
			}
		}

        /// <summary>OnComponentChanged</summary>
        /// <returns>void</returns>
        public void OnComponentChanged()
		{
		}

        /// <summary>OnComponentChanging</summary>
        /// <returns>bool</returns>
        public bool OnComponentChanging()
		{
			return false;
		}

        /// <summary>GetService</summary>
        /// <param name="serviceType">System.Type</param>
        /// <returns>object</returns>
        public object GetService(Type serviceType)
		{
			return null;
		}
	}
}
