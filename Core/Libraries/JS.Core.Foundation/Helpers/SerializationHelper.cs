using JS.Core.Foundation.BaseClasses;
using JS.Core.Foundation.Constants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace JS.Core.Foundation.Helpers
{
    /// <summary>
    /// Serialization Helper
    /// </summary>
    public class SerializationHelper : SingletonBase<SerializationHelper>
    {
        #region Xml Serializer Cache

        static Dictionary<Type, XmlSerializer> _serializers = new Dictionary<Type, XmlSerializer>();
        static readonly object _serializerLock = new object();

        private XmlSerializer GetXmlSerializer(Type type)
        {
            XmlSerializer serializer = null;

            if (_serializers.ContainsKey(type))
            {
                serializer = _serializers[type];
            }
            else
            {
                lock (_serializerLock)
                {
                    if (_serializers.ContainsKey(type))
                    {
                        serializer = _serializers[type];
                    }
                    else
                    {
                        serializer = new XmlSerializer(type);
                        _serializers.Add(type, serializer);
                    }
                }
            }

            return serializer;
        } 

        #endregion

        /// <summary>
        /// Deserializes the specified XML.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <param name="serializationType">Type of the serialization.</param>
        /// <returns></returns>
        public T Deserialize<T>(string data, SerializationTypes serializationType)
        {
            switch (serializationType)
            {
                case SerializationTypes.Json:
                    return JsonDeserialize<T>(data);
                case SerializationTypes.Xml:
                default:
                    return XmlDeserialize<T>(data);
            }
        }

        private T XmlDeserialize<T>(string data)
        {
            using (TextReader reader = new StringReader(data))
            {
                using (XmlTextReader xmlReader = new XmlTextReader(reader))
                {
                    xmlReader.Normalization = false;

                    XmlSerializer serializer = GetXmlSerializer(typeof(T));
                    return (T)serializer.Deserialize(xmlReader);
                }
            }
        }

        private T JsonDeserialize<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }

        /// <summary>
        /// Serializes the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="serializationType">Type of the serialization.</param>
        /// <returns></returns>
        public string Serialize(object obj, SerializationTypes serializationType)
        {
            switch (serializationType)
            {
                case SerializationTypes.Json:
                    return JsonSerialize(obj);
                case SerializationTypes.Xml:
                default:
                    return XmlSerialize(obj);
            }
        }

        private string JsonSerialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        private string XmlSerialize(object obj)
        {
            if (obj == null)
            {
                return String.Empty;
            }

            string serializedXml = String.Empty;

            using (StringWriter stringWriter = new StringWriter())
            {
                var xmlWriterSettings = new XmlWriterSettings()
                {
                    Indent = false,
                    NewLineHandling = NewLineHandling.None,
                    OmitXmlDeclaration = true
                };

                XmlSerializerNamespaces nameSpace = new XmlSerializerNamespaces();
                nameSpace.Add(String.Empty, String.Empty);

                using (var xmlWriter = XmlWriter.Create(stringWriter, xmlWriterSettings))
                {
                    var xmlSerializer = GetXmlSerializer(obj.GetType());
                    xmlSerializer.Serialize(xmlWriter, obj);

                    serializedXml = stringWriter.ToString();
                }
            }

            return serializedXml;
        }
    }
}
