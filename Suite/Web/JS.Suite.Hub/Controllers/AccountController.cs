using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using JS.Suite.Hub.Models;
using JS.Suite.BusinessLogic.Security;
using JS.Suite.BusinessLogic.JSSuite;
using JS.Core.Foundation.ExtensionMethods;
using JS.Core.Web;
using JS.Suite.Hub.Models.Account;
using System.Globalization;
using JS.Suite.BusinessLogic.Resource;
using JS.Core.Web.Mvc.ActionResults;
using JS.Suite.Foundation.Constants;
using JS.Suite.BusinessLogic.Messaging;
using JS.Core.Foundation.Data;
using JS.Suite.DataAbstraction.JSSuite;
using JS.Suite.BusinessLogic.Messaging.Email;
using JS.Suite.BusinessLogic.Templating.Models.Email;
using JS.Core.Foundation.ErrorHandling;

namespace JS.Suite.Hub.Controllers
{
    /// <summary>
    /// Account Controller
    /// </summary>
    public class AccountController : BaseController
    {
        /// <summary>
        /// Logins the specified return URL.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl, ManageAccountMessageId? message)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (message == ManageAccountMessageId.Error)
            {
                ModelState.AddModelError("", Localization.LogInFailedWithExternalProvider);
            }
            else if (message == ManageAccountMessageId.SetPasswordSuccess)
            {
                ViewBag.StatusMessage = Localization.YourPasswordHasBeenSet;
            }

            return View();
        }

        /// <summary>
        /// Logins the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await JSUserBusinessManager.Current.UserManager.FindByNameAsync(model.UserName);
                if (user != null 
                    && JSUserBusinessManager.Current.UserManager.CheckPassword(user, model.Password) 
                    && !JSUserBusinessManager.Current.UserManager.IsLockedOut(user.JSUserId))
                {
                    await Task.WhenAll(SignInAsync(user, model.RememberMe),
                        JSUserBusinessManager.Current.UserManager.ResetAccessFailedCountAsync(user.JSUserId),
                        SendAppMessage(user, MessageTypes.UserLoggedIn));

                    JSUserBusinessManager.Current.UserManager.SetLockoutEndDate(user.JSUserId, DateTimeOffset.MinValue);

                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    string errorMessage = Localization.InvalidUserNameOrPassword;

                    if (user != null)
                    {
                        if (JSUserBusinessManager.Current.UserManager.IsLockedOut(user.JSUserId))
                        {
                            errorMessage = Localization.ThisAccountHasBeenLockedOut;
                        }
                        else
                        {
                            await JSUserBusinessManager.Current.UserManager.AccessFailedAsync(user.JSUserId);

                            if (JSUserBusinessManager.Current.UserManager.IsLockedOut(user.JSUserId))
                            {
                                errorMessage = Localization.ThisAccountHasBeenLockedOut;

                                await AppMessenger.Current.Send(MessageTypes.UserLockedOut,
                                    String.Format("{0} has been locked out of his/her account.", user.Name),
                                    "A user has been locked out of his/her account.");
                            }
                        } 
                    }

                    ModelState.AddModelError("", errorMessage);
                    
                    ClearLoginViewModelPassword(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>
        /// Registers this instance.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new JSUser() { Name = model.UserName };
                var result = await JSUserBusinessManager.Current.UserManager.CreateAsync(user, model.NewPassword);
                if (result.Succeeded)
                {
                    await SignInAsync(user, isPersistent: false);

                    await SendAppMessage(user, MessageTypes.NewUserAccountRegistered);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AddErrors(result);

                    ClearRegisterViewModelPassword(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>
        /// Checks if a UserName is available
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> UserNameAvailable(UserBaseViewModel model)
        {
            JSUser jsUser;

            if (model.UserId != null)
            {
                jsUser = await JSUserBusinessManager.Current.UserManager.FindByIdAsync((int)model.UserId);

                if (jsUser.UserName == model.UserName)
                {
                    return Json(true);
                }
            }

            jsUser = await JSUserBusinessManager.Current.UserManager.FindByNameAsync(model.UserName);

            return Json(jsUser == null);
        }

        /// <summary>
        /// Returns the Forgot Password View
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        /// <summary>
        /// Sends a Password Reset Email
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                JSUser user = await JSUserBusinessManager.Current.UserManager.FindByEmailAsync(model.Email);
                if (user == null || !(await JSUserBusinessManager.Current.UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    ModelState.AddModelError("", Localization.TheUserEitherDoesNotExistOrIsNotConfirmed);
                    return View();
                }

                string confirmationToken = JSUserBusinessManager.Current.UserManager.GeneratePasswordResetToken(user.JSUserId);

                ConfirmationTokenModel emailModel = new ConfirmationTokenModel()
                {
                    User = user,
                    ConfirmationToken = confirmationToken
                };

                EmailHelper.Current.Send(EmailTypes.ResetPassword, emailModel, new List<string>() { user.Email });

                await AppMessenger.Current.Send(MessageTypes.PasswordResetEmailSent,
                                    String.Format("{0} has sent a password reset email to his/her email account ({1})", user.UserName, user.Email),
                                    "A password reset email has been sent.");

                ViewBag.StatusMessage = Localization.PleaseCheckYourEmailToResetYourPassword;
                return View(model);
            }
            return View(model);
        }

        /// <summary>
        /// Sets the password.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        /// <exception cref="CoreException">Missing UserId or Confirmation Token;Missing UserId or Confirmation Token</exception>
        [AllowAnonymous]
        public ActionResult SetPassword(int userId, string code)
        {
            if (userId == default(int) || code == null)
            {
                throw new CoreException("Missing UserId or Confirmation Token", "Missing UserId or Confirmation Token");
            }

            SetPasswordViewModel model = new SetPasswordViewModel()
            {
                UserId = userId,
                ConfirmationToken = code
            };

            return View(model);
        }


        /// <summary>
        /// Sets the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        /// <exception cref="CoreException">Missing UserId or Confirmation Token;Missing UserId or Confirmation Token</exception>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                JSUser user = await JSUserBusinessManager.Current.UserManager.FindByIdAsync(model.UserId);

                if (user == null)
                {
                    throw new CoreException("Missing UserId or Confirmation Token", "Missing UserId or Confirmation Token");
                }

                IdentityResult result = await JSUserBusinessManager.Current.UserManager.ResetPasswordAsync(user.Id, model.ConfirmationToken, model.NewPassword);
                if (result.Succeeded)
                {
                    await AppMessenger.Current.Send(MessageTypes.PasswordReset,
                                        String.Format("{0} has reset his/her password.", user.UserName),
                                        "A user has reset his/her password.");

                    return RedirectToAction("Login", "Account", new { message = ManageAccountMessageId.SetPasswordSuccess });
                }
                else
                {
                    AddErrors(result);
                    return View();
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>
        /// Disassociates the specified login provider.
        /// </summary>
        /// <param name="loginProvider">The login provider.</param>
        /// <param name="providerKey">The provider key.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string provider)
        {
            ManageAccountMessageId? message = null;
            
            IdentityResult result = await JSUserBusinessManager.Current.UserManager.RemoveLoginAsync(User.Identity.GetUserId<int>(), new UserLoginInfo(provider, String.Empty));
            if (result.Succeeded)
            {
                message = ManageAccountMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageAccountMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        /// <summary>
        /// Manages the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public ActionResult Manage(ManageAccountMessageId? message = null)
        {
            switch (message)
            {
                case ManageAccountMessageId.ChangePasswordSuccess:
                    ViewBag.StatusMessage = Localization.YourPasswordHasBeenChanged;
                    break;
                case ManageAccountMessageId.SetPasswordSuccess:
                    ViewBag.StatusMessage = Localization.YourPasswordHasBeenSet;
                    break;
                case ManageAccountMessageId.RemoveLoginSuccess:
                    ViewBag.StatusMessage = Localization.TheExternalLogInWasRemoved;
                    break;
                case ManageAccountMessageId.Error:
                    ModelState.AddModelError("", Localization.AnErrorHasOccured);
                    break;
                case ManageAccountMessageId.AddedLoginSuccess:
                    ViewBag.StatusMessage = Localization.TheExternalLogInWasAdded;
                    break;
                default:
                    break;
            }

            ViewBag.HasLocalPassword = HasPassword();

            JSUser jsUser = JSUserBusinessManager.Current.UserManager.FindById(Int32.Parse(User.Identity.GetUserId()));

            if (!String.IsNullOrEmpty(jsUser.Email))
            {
                JSUser emailUser = JSUserBusinessManager.Current.UserManager.FindByEmail(jsUser.Email);

                if (emailUser != null && emailUser.JSUserId != jsUser.JSUserId)
                {
                    ViewBag.EmailAlreadyConfirmedElsewhere = true;
                }
            }

            IList<UserLoginInfo> linkedAccounts = JSUserBusinessManager.Current.UserManager.GetLogins(User.Identity.GetUserId<int>());

            ManageUserViewModel model = new ManageUserViewModel()
            {
                UserId = jsUser.JSUserId,
                UserName = jsUser.UserName,
                LinkedAccounts = linkedAccounts,
                AvailableAccounts = HttpContext.GetOwinContext().Authentication.GetExternalAuthenticationTypes()
                                        .Where(at => linkedAccounts.FirstOrDefault(la => la.LoginProvider == at.AuthenticationType) == null)
                                        .Select(at => new UserLoginInfo(at.AuthenticationType, String.Empty))
                                        .ToList(),
                Email = jsUser.Email,
                EmailConfirmedFlag = jsUser.EmailConfirmedFlag
            };

            return View("Manage", model);
        }

        /// <summary>
        /// Manages the User Account
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(UserBaseViewModel model)
        {
            if (ModelState.IsValid)
            {
                JSUser updatedUser = new JSUser() { 
                    JSUserId = Int32.Parse(User.Identity.GetUserId()), 
                    UserName = model.UserName};

                JSUser storedUser = JSUserBusinessManager.Current.UserManager.FindById(Int32.Parse(User.Identity.GetUserId()));

                if (String.IsNullOrEmpty(storedUser.Email))
	            {
                    updatedUser.Email = model.Email;
	            }
                    
                JSUserBusinessManager.Current.UserManager.Update(updatedUser);

                ViewBag.NewUserName = model.UserName;

                await SignInAsync(updatedUser, false);
            }

            return Manage();
        }

        /// <summary>
        /// Manages the password on the user account
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ManagePassword(ManageUserViewModel model)
        {
            //User Name is not required for Adding / Changing a Password
            ModelState state = ModelState["UserName"];
            if (state != null)
            {
                state.Errors.Clear();
            }

            if (HasPassword())
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await JSUserBusinessManager.Current.UserManager.ChangePasswordAsync(User.Identity.GetUserId<int>(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageAccountMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);

                        ClearManageUserViewModelPassword(model);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await JSUserBusinessManager.Current.UserManager.AddPasswordAsync(User.Identity.GetUserId<int>(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageAccountMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);

                        ClearManageUserViewModelPassword(model);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return Manage();
        }

        /// <summary>
        /// Removes the email.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveEmail(UserBaseViewModel model)
        {
            if (ModelState.IsValid && model.UserId != null)
            {
                JSUserBusinessManager.Current.UserManager.SetEmail(model.UserId ?? 0, null);
            }

            return Manage();
        }

        /// <summary>
        /// Sends the Confirmation Email
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ActionResult SendConfirmationEmail(UserBaseViewModel model)
        {
            if (ModelState.IsValid && model.UserId != null)
            {
                JSUser user = JSUserBusinessManager.Current.UserManager.FindById(model.UserId ?? 0);

                string confirmationToken = JSUserBusinessManager.Current.UserManager.GenerateEmailConfirmationToken(model.UserId ?? 0);

                ConfirmationTokenModel emailModel = new ConfirmationTokenModel() {
                    User = user,
                    ConfirmationToken = confirmationToken
                };

                EmailHelper.Current.Send(EmailTypes.ConfirmEmail, emailModel, new List<string>() { user.Email });

                AppMessenger.Current.Send(MessageTypes.ConfirmationEmailSent,
                                    String.Format("{0} has sent a confirmation email to his/her email account ({1})", user.UserName, user.Email),
                                    "A confirmation email has been sent.");

                ViewBag.ConfirmationEmailSent = true;
            }

            return Manage();
        }

        /// <summary>
        /// Confirms the email.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        /// <exception cref="CoreException">Missing UserId or Confirmation Token;Missing UserId or Confirmation Token</exception>
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(int userId, string code)
        {
            if (userId == default(int) || code == null)
            {
                throw new CoreException("Missing UserId or Confirmation Token", "Missing UserId or Confirmation Token");
            }

            JSUser user = await JSUserBusinessManager.Current.UserManager.FindByIdAsync(userId);

            JSUser emailUser = await JSUserBusinessManager.Current.UserManager.FindByEmailAsync(user.Email);

            if (emailUser != null)
            {
                ModelState.AddModelError("", Localization.AnotherUserHasAlreadyConfirmedThisEmailAddress);

                return Manage();
            }

            IdentityResult result = await JSUserBusinessManager.Current.UserManager.ConfirmEmailAsync(userId, code);

            if (!result.Succeeded)
            {
                AddErrors(result);

                return Manage();
            }

            await AppMessenger.Current.Send(MessageTypes.EmailConfirmed,
                                String.Format("{0} has confirmed his/her email account ({1})", user.UserName, user.Email),
                                "An email address has been confirmed.");

            return RedirectToAction("Manage");
        }

        /// <summary>
        /// Externals the login.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        /// <summary>
        /// Externals the login callback.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login", new { ReturnUrl = returnUrl, ErrorMessage = ManageAccountMessageId.Error });
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await JSUserBusinessManager.Current.UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);

                await SendAppMessage(user, MessageTypes.UserLoggedIn, loginInfo.Login.LoginProvider);

                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new UserBaseViewModel { UserName = loginInfo.DefaultUserName });
            }
        }

        /// <summary>
        /// Links the login.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        /// <summary>
        /// Links the login callback.
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(ChallengeResult.XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageAccountMessageId.Error });
            }
            var result = await JSUserBusinessManager.Current.UserManager.AddLoginAsync(User.Identity.GetUserId<int>(), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage", new { Message = ManageAccountMessageId.AddedLoginSuccess });
            }
            return RedirectToAction("Manage", new { Message = ManageAccountMessageId.Error });
        }

        /// <summary>
        /// Externals the login confirmation.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(UserBaseViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return RedirectToAction("Login", new { ReturnUrl = returnUrl, ErrorMessage = ManageAccountMessageId.Error }); 
                }
                var user = new JSUser() { Name = model.UserName };
                var result = await JSUserBusinessManager.Current.UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await JSUserBusinessManager.Current.UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: false);

                        await SendAppMessage(user, MessageTypes.NewUserAccountRegistered, info.Login.LoginProvider);

                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        private static Task<IProcessResult> SendAppMessage(JSUser user, MessageTypes messageType, string provider = null)
        {
            string subjectFormat;
            string messageFormat;
            string loginMethod = "password";

            if (messageType == MessageTypes.NewUserAccountRegistered)
            {
                subjectFormat = "A user registered with his/her {0}.";
                messageFormat = "{1} registered with his/her {0}.";
            }
            else 
            {
                subjectFormat = "A user signed in with his/her {0}.";
                messageFormat = "{1} signed in with his/her {0}.";
            }

            if (!String.IsNullOrEmpty(provider))
            {
                loginMethod = String.Format("{0} account", provider);
            }

            return AppMessenger.Current.Send(messageType,
                                        String.Format(messageFormat, loginMethod, user.Name),
                                        String.Format(subjectFormat, loginMethod));
        }

        /// <summary>
        /// Logs the off.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        #region Helpers

        /// <summary>
        /// Gets the authentication manager.
        /// </summary>
        /// <value>
        /// The authentication manager.
        /// </value>
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        /// <summary>
        /// Signs the in asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="isPersistent">if set to <c>true</c> [is persistent].</param>
        /// <returns></returns>
        private async Task SignInAsync(JSUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await JSUserBusinessManager.Current.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        /// <summary>
        /// Adds the errors.
        /// </summary>
        /// <param name="result">The result.</param>
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        /// <summary>
        /// Determines whether this instance has password.
        /// </summary>
        /// <returns></returns>
        private bool HasPassword()
        {
            var user = JSUserBusinessManager.Current.UserManager.FindById(User.Identity.GetUserId<int>());
            if (user != null)
            {
                return user.Password != null;
            }
            return false;
        }

        /// <summary>
        /// Redirects to local.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private void ClearLoginViewModelPassword(LoginViewModel model)
        {
            if (ModelState.IsValidField("Password"))
            {
                ModelState["Password"].Value = new ValueProviderResult(String.Empty, String.Empty, CultureInfo.CurrentCulture);
                model.Password = String.Empty;
            }
        }

        private void ClearRegisterViewModelPassword(RegisterViewModel model)
        {
            if (ModelState.IsValidField("NewPassword"))
            {
                ModelState["NewPassword"].Value = new ValueProviderResult(String.Empty, String.Empty, CultureInfo.CurrentCulture);
                model.NewPassword = String.Empty;
            }

            if (ModelState.IsValidField("ConfirmPassword"))
            {
                ModelState["ConfirmPassword"].Value = new ValueProviderResult(String.Empty, String.Empty, CultureInfo.CurrentCulture);
                model.ConfirmPassword = String.Empty;
            }
        }

        private void ClearManageUserViewModelPassword(ManageUserViewModel model)
        {
            if (ModelState.IsValidField("OldPassword"))
            {
                ModelState["OldPassword"].Value = new ValueProviderResult(String.Empty, String.Empty, CultureInfo.CurrentCulture);
                model.OldPassword = String.Empty;
            }

            ClearRegisterViewModelPassword((RegisterViewModel)model);
        }
        #endregion
    }
}