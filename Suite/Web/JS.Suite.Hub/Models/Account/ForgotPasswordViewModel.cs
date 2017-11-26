using JS.Suite.BusinessLogic.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JS.Suite.Hub.Models.Account
{
    /// <summary>
    /// Forgot Password View Model
    /// </summary>
    public class ForgotPasswordViewModel 
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required]
        [EmailAddress(ErrorMessage = null, ErrorMessageResourceName = "The_FieldIsNotAValidEmailAddress", ErrorMessageResourceType = typeof(Localization))]
        [RegularExpression(@"^[A-z0-9._%+-]+@[A-z0-9.-]+\.[A-z]{2,4}$", ErrorMessageResourceName = "The_FieldIsNotAValidEmailAddress", ErrorMessageResourceType = typeof(Localization))]
        [Display(Name = "Email", ResourceType = typeof(Localization))]
        public string Email { get; set; }
    }
}