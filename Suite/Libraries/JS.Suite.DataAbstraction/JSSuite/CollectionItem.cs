using JS.Core.Foundation.Data;
using JS.Suite.DataAbstraction.JSSuite.Generated;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Suite.DataAbstraction.JSSuite
{
    /// <summary>
    /// Editable Class for the CollectionItem Table
    /// </summary>
    public class CollectionItem : CollectionItem_Generated, IQueryContextContainer
    {

        /// <summary>
        /// Gets or sets the query context.
        /// </summary>
        /// <value>
        /// The query context.
        /// </value>
        [DBIgnore]
        public QueryContext QueryContext { get; set; }

        /// <summary>
        /// Note
        /// </summary>
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public override string Note
        {
            get
            {
                return base.Note;
            }
            set
            {
                base.Note = value;
            }
        }

        /// <summary>
        /// Name
        /// </summary>
        [Required]
        public override string Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                base.Name = value;
            }
        }
        
        /// <summary>
        /// UDF Date Time 1
        /// </summary>
        [DataType(DataType.Date)]
        public override DateTime? UDFDateTime1
        {
            get
            {
                return base.UDFDateTime1;
            }
            set
            {
                base.UDFDateTime1 = value;
            }
        }
    }
}
