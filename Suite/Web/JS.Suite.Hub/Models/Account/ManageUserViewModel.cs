using JS.Suite.BusinessLogic.Resource;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JS.Suite.Hub.Models.Account
{
    /// <summary>
    /// Manage User View Model
    /// </summary>
    public class ManageUserViewModel : RegisterViewModel
    {
        /// <summary>
        /// Gets or sets the old password.
        /// </summary>
        /// <value>
        /// The old password.
        /// </value>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "CurrentPassword", ResourceType = typeof(Localization))]
        public string OldPassword { get; set; }

        /// <summary>
        /// Gets or sets the linked accounts.
        /// </summary>
        /// <value>
        /// The linked accounts.
        /// </value>
        public IList<UserLoginInfo> LinkedAccounts { get; set; }

        /// <summary>
        /// Gets or sets the available accounts.
        /// </summary>
        /// <value>
        /// The available accounts.
        /// </value>
        public IList<UserLoginInfo> AvailableAccounts { get; set; }
    }
}