using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Core.Foundation.Constants
{
    /// <summary>
    /// Environments
    /// </summary>
    public enum Environments
    {
        /// <summary> Sandbox </summary>
        Sandbox = 1,
        /// <summary> Production </summary>
        Production = 2
    }

    /// <summary>
    /// Result Codes
    /// </summary>
    public enum ResultCodes
    {
        /// <summary> Success </summary>
        Success = 1,
        /// <summary> Failure </summary>
        Failure = 2
    }

    /// <summary>
    /// Serialization Types
    /// </summary>
    public enum SerializationTypes
    {
        /// <summary>
        /// XML
        /// </summary>
        Xml = 1,
        /// <summary>
        /// JSON
        /// </summary>
        Json = 2
    }
}
