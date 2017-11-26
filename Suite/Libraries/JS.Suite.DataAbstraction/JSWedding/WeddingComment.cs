using JS.Core.Foundation.Data;
using JS.Suite.DataAbstraction.JSWedding.Generated;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Suite.DataAbstraction.JSWedding
{
    /// <summary>
    /// Editable Class for the WeddingComment Table
    /// </summary>
    public class WeddingComment : WeddingComment_Generated
    {
        /// <summary>
        /// UserName
        /// </summary>
        [DisplayName("Name")]
        [Required]
        [DataType(DataType.PhoneNumber)]
        public override string UserName
        {
            get
            {
                return base.UserName;
            }
            set
            {
                base.UserName = value;
            }
        }

        /// <summary>
        /// Email
        /// </summary>
        [DisplayName("Email (Optional)")]
        public override string Email
        {
            get
            {
                return base.Email;
            }
            set
            {
                base.Email = value;
            }
        }

        /// <summary>
        /// Comment
        /// </summary>
        [DisplayName("Comment (Optional)")]
        [DataType(DataType.MultilineText)]
        public override string Comment
        {
            get
            {
                return base.Comment;
            }
            set
            {
                base.Comment = value;
            }
        }
    }
}
