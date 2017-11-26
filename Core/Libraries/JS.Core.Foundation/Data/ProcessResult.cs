using JS.Core.Foundation.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Core.Foundation.Data
{
    /// <summary>
    /// Process Result
    /// </summary>
    public class ProcessResult : IProcessResult
    {
        /// <summary>
        /// Creates a Process Result with the provided Result Code
        /// </summary>
        /// <param name="resultCode"></param>
        public ProcessResult(ResultCodes resultCode)
        {
            ResultCode = resultCode;
        }

        /// <summary>
        /// Return Code
        /// </summary>
        public ResultCodes ResultCode { get; set; }

        /// <summary>
        /// Determines whether this Process is success.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool IsSuccess()
        {
            return ResultCode == ResultCodes.Success;
        }
    }
}
