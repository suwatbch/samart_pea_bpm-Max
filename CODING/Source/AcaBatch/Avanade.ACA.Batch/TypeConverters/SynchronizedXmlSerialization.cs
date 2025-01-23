// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Xml;
using System.Text;
using System.IO;
using System.Collections;
using System.Xml.Serialization;
using System.Runtime.CompilerServices;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// 
	/// </summary>
	internal class SynchronizedXmlSerialization
	{
		private static Hashtable _xmlSerializers;

        /// <summary>SynchronizedXmlSerialization</summary>
        static SynchronizedXmlSerialization()
		{
			_xmlSerializers = new Hashtable();
		}

        /// <summary>Initialize</summary>
        /// <param name="xmlSerializableTypes">System.Type[]</param>
        /// <returns>void</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
		public static void Initialize(Type[] xmlSerializableTypes)
		{
			AddXmlSerializers(xmlSerializableTypes);
		}

        /// <summary>Deserialize</summary>
        /// <param name="type">System.Type</param>
        /// <param name="serializedValue">string</param>
        /// <returns>object</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
		public static object Deserialize(Type type, string serializedValue)
		{
			XmlStringReader xmlReader = 
				new XmlStringReader(serializedValue);
			XmlSerializer serializer = GetSerializer(type);
			object o = serializer.Deserialize(xmlReader);
			return o;
		}

        /// <summary>SerializeToString</summary>
        /// <param name="serializableObject">object</param>
        /// <returns>string</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
		public static string SerializeToString(object serializableObject)
		{
			XmlStringWriter writer = 
				new XmlStringWriter();
			XmlSerializer serializer = GetSerializer(serializableObject.GetType());
			serializer.Serialize(writer, serializableObject);
			writer.Close();
			string xmlText = writer.GetXml();
			return xmlText;
		}

        /// <summary>AddXmlSerializer</summary>
        /// <param name="serializableType">System.Type</param>
        /// <returns>void</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
		public static void AddXmlSerializer(Type serializableType)
		{
			Type[] types = new Type[]{serializableType};
			AddXmlSerializers(types);
		}

        /// <summary>AddXmlSerializers</summary>
        /// <param name="serializableTypes">System.Type[]</param>
        /// <returns>void</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
		public static void AddXmlSerializers(Type[] serializableTypes)
		{
			XmlSerializer[] serializers = 
				XmlSerializer.FromTypes(serializableTypes);

			IEnumerator typesEnumerator = 
				serializableTypes.GetEnumerator();

			IEnumerator serializersEnumerator =
				serializers.GetEnumerator();

			while (typesEnumerator.MoveNext()
				&& serializersEnumerator.MoveNext())
			{
				Type t = (Type)typesEnumerator.Current;
				XmlSerializer serializer = 
					(XmlSerializer)serializersEnumerator.Current;
				_xmlSerializers.Add(t, serializer);

			}
		}

        /// <summary>GetSerializer</summary>
        /// <param name="serializableType">System.Type</param>
        /// <returns>System.Xml.Serialization.XmlSerializer</returns>
        private static XmlSerializer GetSerializer(Type serializableType)
		{
			XmlSerializer serializer =
				(XmlSerializer)_xmlSerializers[serializableType];
			
			if (serializer == null)
			{
				AddXmlSerializer(serializableType);

				serializer =
					(XmlSerializer)_xmlSerializers[serializableType];
			}

			return serializer;
		}

		public class XmlStringWriter : System.Xml.XmlTextWriter
		{
			private byte[] _bytes;

            /// <summary>XmlStringWriter</summary>
            public XmlStringWriter()
                : base(new MemoryStream(), Encoding.Unicode)
			{
				Formatting = Formatting.None;
			}

            /// <summary>WriteStartDocument</summary>
            /// <param name="standalone">bool</param>
            /// <returns>void</returns>
            public override void WriteStartDocument(bool standalone)
			{
				// do nothing
			}

            /// <summary>WriteStartDocument</summary>
            /// <returns>void</returns>
            public override void WriteStartDocument()
			{
				// do nothing
			}

            /// <summary>Close</summary>
            /// <returns>void</returns>
            public override void Close()
			{
				Flush();
				MemoryStream stream = (MemoryStream)BaseStream;
				_bytes = stream.ToArray();

				base.Close();
			}

            /// <summary>GetXml</summary>
            /// <returns>string</returns>
            public string GetXml()
			{
				Encoding encoding = Encoding.Unicode;
				string xml = encoding.GetString(_bytes, 2, _bytes.Length - 2);
				return xml;
			}


		}

		public class XmlStringReader : System.Xml.XmlTextReader
		{
            /// <summary>XmlStringReader</summary>
            /// <param name="xmlText">string</param>
            public XmlStringReader(string xmlText)
                : base(new StringReader(xmlText))
			{
			}
		}
	}
}
