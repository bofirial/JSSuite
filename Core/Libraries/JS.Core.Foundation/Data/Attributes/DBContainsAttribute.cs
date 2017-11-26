using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Core.Foundation.Data
{
    /// <summary>
    /// Database Contains Attribute
    /// </summary>
    public class DBContainsAttribute: DBLikeComparisonAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DBContainsAttribute" /> class.
        /// </summary>
        /// <param name="wildcard">The wildcard.</param>
        public DBContainsAttribute(string wildcard = null)
            : base(wildcard)
        {

        }
    }
}
