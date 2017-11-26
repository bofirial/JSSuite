using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Suite.Foundation.Constants
{
    /// <summary>
    /// Email Types
    /// </summary>
    public enum EmailTypes
    {
        /// <summary> App Message </summary>
        AppMessage = 0,
        /// <summary> Confirm Email </summary>
        ConfirmEmail = 1,
        /// <summary> Reset Password </summary>
        ResetPassword = 2
    }

    /// <summary>
    /// Wedding Party Member Types
    /// </summary>
    public enum WeddingPartyMemberTypes
    {
        /// <summary> Person </summary>
        Person = 0,
        /// <summary> Couple </summary>
        Couple = 1
    }

    /// <summary>
    /// Manage Account Message Id
    /// </summary>
    public enum ManageAccountMessageId
    {
        /// <summary>
        /// The change password success
        /// </summary>
        ChangePasswordSuccess,
        /// <summary>
        /// The set password success
        /// </summary>
        SetPasswordSuccess,
        /// <summary>
        /// The remove login success
        /// </summary>
        RemoveLoginSuccess,
        /// <summary>
        /// The error
        /// </summary>
        Error,
        /// <summary>
        /// Added A Login Successfully
        /// </summary>
        AddedLoginSuccess
    }
}
