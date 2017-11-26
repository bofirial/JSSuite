using JS.Suite.BusinessLogic.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JS.Suite.Hub.Models.Account
{
    /// <summary>
    /// Set Password View Model
    /// </summary>
    public class SetPasswordViewModel
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the confirmation token.
        /// </summary>
        /// <value>
        /// The confirmation token.
        /// </value>
        public string ConfirmationToken { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceName = "The_MustBeAtLeast_CharactersLong", ErrorMessageResourceType = typeof(Localization))]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword", ResourceType = typeof(Localization))]
        public virtual string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets the confirm password.
        /// </summary>
        /// <value>
        /// The confirm password.
        /// </value>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Localization))]
        [StringLength(100, ErrorMessageResourceName = "The_MustBeAtLeast_CharactersLong", ErrorMessageResourceType = typeof(Localization), MinimumLength = 6)]
        [Compare("NewPassword", ErrorMessageResourceName = "The_And_DoNotMatch", ErrorMessageResourceType = typeof(Localization))]
        public virtual string ConfirmPassword { get; set; }
    }
}