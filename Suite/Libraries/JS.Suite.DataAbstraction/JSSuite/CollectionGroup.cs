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
    /// Editable Class for the CollectionGroup Table
    /// </summary>
    public class CollectionGroup : CollectionGroup_Generated, IQueryContextContainer
    {
        /// <summary>
        /// Gets or sets the js user identifier.
        /// </summary>
        /// <value>
        /// The js user identifier.
        /// </value>
        [DBIgnore]
        public int JSUserId { get; set; }

        /// <summary>
        /// Gets or sets the number of collection items.
        /// </summary>
        /// <value>
        /// The number of collection items.
        /// </value>
        [DBIgnore]
        public int NumberOfCollectionItems { get; set; }

        /// <summary>
        /// Gets or sets the query context.
        /// </summary>
        /// <value>
        /// The query context.
        /// </value>
        [DBIgnore]
        public QueryContext QueryContext { get; set; }

        /// <summary>
        /// Gets or sets the name of the js user.
        /// </summary>
        /// <value>
        /// The name of the js user.
        /// </value>
        [DBIgnore]
        public string JSUserName { get; set; }

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
        /// UDFDateTime1Name
        /// </summary>
        [Display(Name="User Defined DateTime")]
        public override string UDFDateTime1Name
        {
            get
            {
                return base.UDFDateTime1Name;
            }
            set
            {
                base.UDFDateTime1Name = value;
            }
        }

        /// <summary>
        /// UDFLongText1Name
        /// </summary>
        [Display(Name="User Defined Large Textbox")]
        public override string UDFLongText1Name
        {
            get
            {
                return base.UDFLongText1Name;
            }
            set
            {
                base.UDFLongText1Name = value;
            }
        }

        /// <summary>
        /// UDFShortText1Name
        /// </summary>
        [Display(Name="User Defined Textbox #1")]
        public override string UDFShortText1Name
        {
            get
            {
                return base.UDFShortText1Name;
            }
            set
            {
                base.UDFShortText1Name = value;
            }
        }

        /// <summary>
        /// UDFShortText2Name
        /// </summary>
        [Display(Name="User Defined Textbox #2")]
        public override string UDFShortText2Name
        {
            get
            {
                return base.UDFShortText2Name;
            }
            set
            {
                base.UDFShortText2Name = value;
            }
        }

        /// <summary>
        /// UDFShortText3Name
        /// </summary>
        [Display(Name="User Defined Textbox #3")]
        public override string UDFShortText3Name
        {
            get
            {
                return base.UDFShortText3Name;
            }
            set
            {
                base.UDFShortText3Name = value;
            }
        }
    }
}
