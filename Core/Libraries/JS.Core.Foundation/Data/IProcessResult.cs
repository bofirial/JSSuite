using JS.Core.Foundation.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Core.Foundation.Data
{
    /// <summary>
    /// Interface for Process Results
    /// </summary>
    public interface IProcessResult
    {
        /// <summary>
        /// Gets the return code.
        /// </summary>
        /// <value>
        /// The return code.
        /// </value>
        ResultCodes ResultCode { get; }

        /// <summary>
        /// Determines whether this Process is success.
        /// </summary>
        /// <returns></returns>
        bool IsSuccess();
    }
}
