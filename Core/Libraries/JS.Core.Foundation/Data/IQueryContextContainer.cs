using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Core.Foundation.Data
{
    /// <summary>
    /// Interface for a Query Context Container
    /// </summary>
    public interface IQueryContextContainer
    {
        /// <summary>
        /// Gets or sets the query context.
        /// </summary>
        /// <value>
        /// The query context.
        /// </value>
        QueryContext QueryContext { get; set; }
    }
}
