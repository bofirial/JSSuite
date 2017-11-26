using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Core.Foundation.ErrorHandling
{
    /// <summary>
    /// SQL Exception
    /// </summary>
    public class SQLException : CoreException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SQLException"/> class.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="message">The message.</param>
        public SQLException(string label, string message)
            : base(label, message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLException"/> class.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public SQLException(string label, string message, Exception innerException)
            : base(label, message, innerException)
        {
        }
    }
}
