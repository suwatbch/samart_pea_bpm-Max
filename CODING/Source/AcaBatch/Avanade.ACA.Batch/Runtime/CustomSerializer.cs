using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Avanade.ACA.Batch
{
	public class CustomSerializer
	{
		protected MemoryStream ms;
		protected XmlTextWriter xtw;
		protected string xml;
		protected Hashtable arrays;

		public string Xml
		{
			get {return xml;}
		}

        /// <summary>Start</summary>
        /// <returns>void</returns>
		public void Start()
		{
			arrays=new Hashtable();
			ms=new MemoryStream();
			xtw = new XmlTextWriter(ms, Encoding.UTF8);
			xtw.Formatting=Formatting.Indented;
			xtw.Namespaces=false;
			xtw.WriteStartDocument();			
			//xtw.WriteStartElement("Objects");
		}

        /// <summary>Finish</summary>
        /// <param name="filePath">System.String</param>
        /// <returns>string</returns>
		public string Finish(string filePath)
		{
			// Trace.Assert(xtw != null, "Must call Serializer.Start() first.");

			//xtw.WriteEndElement();
			xtw.Flush();
			xtw.Close();
			Encoding e8=new UTF8Encoding();
			xml=e8.GetString(ms.ToArray(), 1, ms.ToArray().Length-1);
			arrays.Clear();
            if (filePath.Trim().Length > 0)
                WriteXmlToFile(filePath, xml);
			return xml;
		}

        /// <summary>WriteXmlToFile</summary>
        /// <param name="filePath">System.String</param>
        /// <param name="xml">System.String</param>
        /// <returns>void</returns>
        public void WriteXmlToFile(string filePath, string xml)
        {
            try
            {
                File.WriteAllText(filePath, xml);
                //StreamWriter streamWriter = new StreamWriter(filePath, false);
                //streamWriter.Write(xml);
                //streamWriter.Close();
            }
            catch (Exception ex)
            {
                ex = ex;
            }
        }

		// simple property serialization
        /// <summary>Serialize</summary>
        /// <param name="obj">System.Object</param>
        /// <param name="filePath">System.String</param>
        /// <returns>string</returns>
		public string Serialize(object obj, string filePath)
		{
            Start();
			// Trace.Assert(xtw != null, "Must call Serializer.Start() first.");
			// Trace.Assert(obj != null, "Cannot serialize a null object.");

			Type t=obj.GetType();
			xtw.WriteStartElement(t.Name);
			foreach(PropertyInfo pi in t.GetProperties())
			{
				Type propertyType=pi.PropertyType;
				
				// check if the item is an IList
				object val=pi.GetValue(obj, null);
				if (val is IList)
				{
					arrays[pi.Name]=val;
				}
				else
				{
					// with enum properties, IsPublic==false, even if marked public!
					if ( (propertyType.IsSerializable) && (!propertyType.IsArray) && (pi.CanWrite) && ( (propertyType.IsPublic) || (propertyType.IsEnum) ) )
					{
						if (val != null)
						{
							bool isDefaultValue=false;

							// look for a default value attribute.
							foreach(object attr in pi.GetCustomAttributes(false))
							{
								if (attr is DefaultValueAttribute)
								{
									// it exists--compare current value to default value
									DefaultValueAttribute dva=(DefaultValueAttribute)attr;
									isDefaultValue=val.Equals(dva.Value);
								}
							}

							// only non-default values or properties without a default value are serialized.
							if (!isDefaultValue)
							{
								// do a type conversion to a string, as this yields a deserializable value, rather than what ToString returns.
								TypeConverter tc=TypeDescriptor.GetConverter(propertyType);
								if (tc.CanConvertTo(typeof(string)))
								{
									val=tc.ConvertTo(val, typeof(string));
									xtw.WriteAttributeString(pi.Name, val.ToString());
								}
								else
								{
									// Trace.WriteLine("Cannot convert "+pi.Name+" to a string value.");
								}
							}
						}
						else
						{
							// null values not supported!
						}
					}
				}
			}

			// write out arrays as string values
			foreach(DictionaryEntry entry in arrays)
			{
				string name=(string)entry.Key;
				IList list=(IList)entry.Value;
				xtw.WriteStartElement(name);
				foreach(object item in list)
				{
					xtw.WriteStartElement("Item");
					xtw.WriteAttributeString("Val", item.ToString());
					xtw.WriteEndElement();
				}
				xtw.WriteEndElement();
			}

			xtw.WriteEndElement();
            return Finish(filePath);
		}
	}

	public class CustomDeserializer
	{
		protected XmlDocument doc;

        /// <summary>Start</summary>
        /// <param name="text">System.String</param>
        /// <returns>void</returns>
		public void Start(string text)
		{
			doc=new XmlDocument();
			doc.LoadXml(text);
		}

		// for completeness only
        /// <summary>Finish</summary>
        /// <returns>void</returns>
		public void Finish()
		{
		}

		// simple property deserialization
        /// <summary>Deserialize</summary>
        /// <param name="obj">System.Object</param>
        /// <param name="xml">System.String</param>
        /// <returns>object</returns>
		public object Deserialize(object obj, string xml)
		{
            Start(xml);
			// Trace.Assert(doc != null, "Must call Deserializer.Start() first.");
			// Trace.Assert(doc.ChildNodes.Count==2, "Incorrect xml format.");			
			// Trace.Assert(obj != null, "Cannot deserialize to a null object");

			// skip the encoding and comment, and get the indicated child in the Objects tag
            XmlNode node = doc.ChildNodes[1];
			Type t=obj.GetType();
			// Trace.Assert(t.Name==node.Name, "Object name does not match element tag.");

			// set all properties that have a default value and not overridden.
			foreach(PropertyInfo pi in t.GetProperties())
			{
				Type propertyType=pi.PropertyType;

				// look for a default value attribute.
				foreach(object attr in pi.GetCustomAttributes(false))
				{
					if (attr is DefaultValueAttribute)
					{
						// it has a default value
						DefaultValueAttribute dva=(DefaultValueAttribute)attr;
						if (node.Attributes[pi.Name] == null)
						{
							// assign the default value, as it's not being overridden.
							// this reverts the object's property back to the default
							pi.SetValue(obj, dva.Value, null);
						}
					}
				}
			}

			// now parse the xml attributes that are going to change property values
			foreach(XmlAttribute attr in node.Attributes)
			{
				string pname=attr.Name;
				string pvalue=attr.Value;
				PropertyInfo pi=t.GetProperty(pname);
				if (pi != null)
				{
					TypeConverter tc=TypeDescriptor.GetConverter(pi.PropertyType);
					if (tc.CanConvertFrom(typeof(string)))
					{
						try
						{
							object val=tc.ConvertFrom(pvalue);
							pi.SetValue(obj, val, null);
						}
						catch(Exception e)
						{
							// Trace.WriteLine("Setting "+pname+" failed:\r\n"+e.Message);
						}
					}
				}
			}

			// Process arrays of strings.
			foreach(XmlElement elem in node.ChildNodes)
			{
				// get the property
				PropertyInfo pi=t.GetProperty(elem.Name);

                try
                {
                    // get the list, which assumes that the array is read-only and already initialized
                    IList list = (IList)pi.GetValue(obj, null);

                    // for each child, get the value and add it to the list.
                    foreach (XmlElement item in elem.ChildNodes)
                    {
                        string val = item.Attributes["Val"].Value;
                        list.Add(val);
                    }
                }
                catch
                {
                    pi.SetValue(obj, elem.InnerText, null);
                }
			}
            Finish();
            return obj;            
		}        
	}
}
