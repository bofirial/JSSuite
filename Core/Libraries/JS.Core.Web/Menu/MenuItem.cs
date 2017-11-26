using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Core.Web.Menu
{
    /// <summary>
    /// Menu Item
    /// </summary>
    public class MenuItem
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the child menu items.
        /// </summary>
        /// <value>
        /// The child menu items.
        /// </value>
        public List<MenuItem> ChildMenuItems { get; set; }
    }
}
