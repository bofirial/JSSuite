using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Drop Down Field Data
    /// </summary>
    public class DropDownFieldData
    {
        /// <summary>
        /// Gets or sets the select list.
        /// </summary>
        /// <value>
        /// The select list.
        /// </value>
        public IEnumerable<SelectListItem> SelectList { get; set; }
    }
}
