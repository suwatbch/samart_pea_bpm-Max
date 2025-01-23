// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.ComponentModel;
using System.Globalization;
using System.Collections;

namespace Avanade.ACA.Batch
{

	internal class XmlSerializableTypeConverter : DefaultConverter
	{
        /// <summary>XmlSerializableTypeConverter</summary>
        public XmlSerializableTypeConverter()
            : base()
		{
		}

		// Overrides the CanConvertFrom method of TypeConverter.
		// The ITypeDescriptorContext interface provides the context for the
		// conversion. Typically this interface is used at design time to 
		// provide information about the design-time container.
        /// <summary>CanConvertFrom</summary>
        /// <param name="context">System.ComponentModel.ITypeDescriptorContext</param>
        /// <param name="sourceType">System.Type</param>
        /// <returns>bool</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, 
			Type sourceType) 
		{
			if (context is XmlSerializableTypeDescriptorContext
				&& sourceType == typeof(string)) 
			{
				return true;
			}
			return base.CanConvertFrom(context, sourceType);
		}
		// Overrides the ConvertFrom method of TypeConverter.
        /// <summary>ConvertFrom</summary>
        /// <param name="context">System.ComponentModel.ITypeDescriptorContext</param>
        /// <param name="culture">System.Globalization.CultureInfo</param>
        /// <param name="value">object</param>
        /// <returns>object</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, 
			CultureInfo culture, object value) 
		{
			if (value is string) 
			{
				Type t = (Type)context.Instance;
				object val = 
					SynchronizedXmlSerialization.Deserialize(t, (string)value);
				return val;
			}
			return base.ConvertFrom(context, culture, value);
		}
		// Overrides the ConvertTo method of TypeConverter.
        /// <summary>ConvertTo</summary>
        /// <param name="context">System.ComponentModel.ITypeDescriptorContext</param>
        /// <param name="culture">System.Globalization.CultureInfo</param>
        /// <param name="value">object</param>
        /// <param name="destinationType">System.Type</param>
        /// <returns>object</returns>
        public override object ConvertTo(ITypeDescriptorContext context, 
			CultureInfo culture, object value, Type destinationType) 
		{  
			if (context is XmlSerializableTypeDescriptorContext
				&& destinationType == typeof(string)) 
			{
				string serializedText = 
					SynchronizedXmlSerialization.SerializeToString(value);
				return serializedText;
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

        /// <summary>CreateInstance</summary>
        /// <param name="context">System.ComponentModel.ITypeDescriptorContext</param>
        /// <param name="properties">System.Collections.IDictionary</param>
        /// <returns>object</returns>
        public override object CreateInstance(ITypeDescriptorContext context,
												IDictionary properties)
		{
			if (context is XmlSerializableTypeDescriptorContext)
			{
				Type t = (Type)context.Instance;
				object o = Activator.CreateInstance(t);
				return o;
			}
			else
			{
				return null;
			}
		}

        /// <summary>GetCreateInstanceSupported</summary>
        /// <param name="context">System.ComponentModel.ITypeDescriptorContext</param>
        /// <returns>bool</returns>
        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			if (context is XmlSerializableTypeDescriptorContext)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

	}
}
