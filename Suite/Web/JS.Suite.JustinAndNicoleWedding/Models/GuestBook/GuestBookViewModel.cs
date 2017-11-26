using JS.Core.Web;
using JS.Suite.DataAbstraction.JSWedding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Suite.JustinAndNicoleWedding.Models.GuestBook
{
    /// <summary>
    /// Guest Book View Model
    /// </summary>
    public class GuestBookViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets or sets the wedding comments.
        /// </summary>
        /// <value>
        /// The wedding comments.
        /// </value>
        public IEnumerable<WeddingComment> WeddingComments { get; set; }

        /// <summary>
        /// The New Wedding Comment to be Added to the Database
        /// </summary>
        public WeddingComment NewWeddingComment { get; set; }
    }
}