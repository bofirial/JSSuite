using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Core.Foundation.ErrorHandling
{
    /// <summary>
    /// Core Exception
    /// </summary>
    public class CoreException : Exception
    {
        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        public string Label { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreException"/> class.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="message">The message.</param>
        public CoreException(string label, string message) : base(message)
        {
            Label = label;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreException"/> class.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public CoreException(string label, string message, Exception innerException) : base(message, innerException)
        {
            Label = label;
        }
    }
}
