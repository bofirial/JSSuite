using JS.Core.Foundation.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Core.Foundation.BaseClasses.Interfaces
{
    /// <summary>
    /// Interface for Serialization Base Classes
    /// </summary>
    public interface ISerializationBase
    {
        /// <summary>
        /// Serializes this instance.
        /// </summary>
        /// <returns></returns>
        string Serialize();

        /// <summary>
        /// Serializes this instance.
        /// </summary>
        /// <param name="serializationType">Type of the serialization.</param>
        /// <returns></returns>
        string Serialize(SerializationTypes serializationType);
    }
}
