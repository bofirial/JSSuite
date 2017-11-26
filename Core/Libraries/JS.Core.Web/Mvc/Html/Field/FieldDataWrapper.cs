using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Field Data Wrapper
    /// </summary>
    public class FieldDataWrapper
    {
        /// <summary>
        /// Gets or sets the field data.
        /// </summary>
        /// <value>
        /// The field data.
        /// </value>
        public FieldData FieldData { get; set; }

        /// <summary>
        /// Gets or sets the drop down field data.
        /// </summary>
        /// <value>
        /// The drop down field data.
        /// </value>
        public DropDownFieldData DropDownFieldData { get; set; }
    }
}
