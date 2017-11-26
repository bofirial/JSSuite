using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Core.Foundation.Data
{
    /// <summary>
    /// Database Starts With Comparison Attribute
    /// </summary>
    public class DBStartsWithAttribute : DBLikeComparisonAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DBStartsWithAttribute" /> class.
        /// </summary>
        /// <param name="wildcard">The wildcard.</param>
        public DBStartsWithAttribute(string wildcard = null)
            : base(wildcard)
        {

        }
    }
}
