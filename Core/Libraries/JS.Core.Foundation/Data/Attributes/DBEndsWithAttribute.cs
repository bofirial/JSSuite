using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Core.Foundation.Data
{
    /// <summary>
    /// Database Ends With Attribute
    /// </summary>
    public class DBEndsWithAttribute : DBLikeComparisonAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DBEndsWithAttribute" /> class.
        /// </summary>
        /// <param name="wildcard">The wildcard.</param>
        public DBEndsWithAttribute(string wildcard = null)
            : base(wildcard)
        {

        }
    }
}
