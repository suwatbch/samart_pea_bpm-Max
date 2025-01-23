// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System.ComponentModel;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// Summary description for DefaultConverter.
	/// </summary>
	internal class DefaultConverter : System.ComponentModel.TypeConverter
	{
        /// <summary>
        /// Summary description of DefaultConverter.
        /// </summary>
		public DefaultConverter() : base()
		{
		}

        /// <summary>
        /// Summary description of GetProperties.
        /// </summary>
        /// <param name="context">System.ComponentModel.ITypeDescriptorContext</param>
        /// <param name="value">object</param>
        /// <param name="attributes">System.Attribute[]</param>
        /// <returns>System.ComponentModel.PropertyDescriptorCollection</returns>
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, System.Attribute[] attributes) 
		{
			return System.ComponentModel.TypeDescriptor.GetProperties(value.GetType(), attributes);
		}


        /// <summary>
        /// Summary description of GetPropertiesSupported.
        /// </summary>
        /// <param name="context">System.ComponentModel.ITypeDescriptorContext</param>
        /// <returns>bool</returns>
		public override bool GetPropertiesSupported(ITypeDescriptorContext context) 
		{
			return true;
		}
	}
}
