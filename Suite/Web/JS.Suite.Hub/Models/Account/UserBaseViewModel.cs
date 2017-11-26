using JS.Suite.BusinessLogic.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JS.Suite.Hub.Models.Account
{
    /// <summary>
    /// User Base View Model
    /// </summary>
    public class UserBaseViewModel
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [Required]
        [Display(Name = "UserName", ResourceType = typeof(Localization))]
        [Remote("UserNameAvailable", "Account", AdditionalFields = "UserId", ErrorMessageResourceName="This_IsUnavailable", ErrorMessageResourceType=typeof(Localization), HttpMethod="POST")]
        public virtual string UserName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [EmailAddress(ErrorMessage= null, ErrorMessageResourceName="The_FieldIsNotAValidEmailAddress", ErrorMessageResourceType=typeof(Localization))]
        [RegularExpression(@"^[A-z0-9._%+-]+@[A-z0-9.-]+\.[A-z]{2,4}$", ErrorMessageResourceName="The_FieldIsNotAValidEmailAddress", ErrorMessageResourceType=typeof(Localization))]
        [Display(Name = "Email", ResourceType = typeof(Localization))]
        public virtual string Email { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [email confirmed flag].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [email confirmed flag]; otherwise, <c>false</c>.
        /// </value>
        public bool EmailConfirmedFlag { get; set; }
    }
}