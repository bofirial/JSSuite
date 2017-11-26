using JS.Core.Foundation.BaseClasses.Interfaces;
using JS.Core.Foundation.Constants;
using JS.Core.Foundation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Core.Foundation.BaseClasses
{
    /// <summary>
    /// Base class used to enable classes to be Serializable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SerializationBase<T> : ISerializationBase
        where T : class, new()
    {
        /// <summary>
        /// Deserializes the specified XML.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        public static T Deserialize(string xml)
        {
            return Deserialize(xml, SerializationTypes.Xml);
        }

        /// <summary>
        /// Deserializes the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="serializationType">Type of the serialization.</param>
        /// <returns></returns>
        public static T Deserialize(string data, SerializationTypes serializationType)
        {
            return SerializationHelper.Current.Deserialize<T>(data, serializationType);
        }

        /// <summary>
        /// Serializes this instance.
        /// </summary>
        /// <returns></returns>
        public string Serialize()
        {
            return Serialize(SerializationTypes.Xml);
        }

        /// <summary>
        /// Serializes this instance.
        /// </summary>
        /// <param name="serializationType">Type of the serialization.</param>
        /// <returns></returns>
        public string Serialize(SerializationTypes serializationType)
        {
            return SerializationHelper.Current.Serialize(this, serializationType);
        }
    }
}
